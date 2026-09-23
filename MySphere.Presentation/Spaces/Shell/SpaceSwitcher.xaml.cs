using MySphere.Presentation.Framework.Foundation;
using MySphere.Presentation.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows;
using MySphere.Presentation.Utilities;

namespace MySphere.Presentation.Spaces.Shell;

public enum SwitcherState
{
    Showing,
    Showed,
    Sliding,
    Idle,
    Hiding,
    Hidden
}

public partial class SpaceSwitcher : UserControl
{
    public SwitcherState State { get; private set; } = SwitcherState.Hidden;

    private readonly List<SpacePreviewCard> _cards = new();

    private int _currentCardIndex = 0;

    private readonly CubicEase _easing = new()
    {
        EasingMode = EasingMode.EaseOut,
    };

    private readonly TimeSpan _animationDuration = TimeSpan.FromSeconds(0.4);

    private readonly DoubleAnimation _scaleAnimationIn;
    private readonly DoubleAnimation _scaleAnimationOut;

    private readonly ShellWindow _shell;

    public SpaceSwitcher()
    {
        InitializeComponent();

        _shell = (ShellWindow)App.Current.MainWindow;

        this.Visibility = Visibility.Collapsed;

        _scaleAnimationIn = new DoubleAnimation()
        {
            To = 0.5,
            Duration = _animationDuration,
            EasingFunction = _easing
        };

        _scaleAnimationOut = new DoubleAnimation()
        {
            To = 1,
            Duration = _animationDuration / 2,
            EasingFunction = new ExponentialEase
            {
                EasingMode = EasingMode.EaseInOut
            }
        };
    }

    public void Capture(BitmapSource preview, ViewModelBase space)
    {
        SpacePreviewCard c = Contain(space);

        if (c is null || _cards.Count == 0)
        {
            _currentCardIndex = 0;

            SpacePreviewCard card = new SpacePreviewCard();

            card.Set(preview, space);

            _cards.Insert(0, card);
            PART_SpacesContainer.Children.Add(card);
            PART_PreviewImage.Source = preview;
            UpdateCards();
            return;
        }

        c.Preview = preview;

        PART_PreviewImage.Source = preview;
    }

    public void Toogle(BitmapSource preview, ViewModelBase space)
    {

        if (State == SwitcherState.Hidden)
            Show(preview, space);

        if (State == SwitcherState.Showed)
            Hide();

        return;
    }

    private void Show(BitmapSource preview, ViewModelBase space)
    {
        PART_TranslateTransform.BeginAnimation(TranslateTransform.XProperty, new DoubleAnimation()
        {
            To = 0,
            Duration = TimeSpan.Zero,
        });

        _currentCardIndex = 0;
        Capture(preview, space);
        State = SwitcherState.Showing;

        UpdateCards();

        this.Visibility = Visibility.Visible;

        PART_ScaleTrasform.BeginAnimation(ScaleTransform.ScaleXProperty, _scaleAnimationIn);
        PART_ScaleTrasform.BeginAnimation(ScaleTransform.ScaleYProperty, _scaleAnimationIn);

        TimerFactory.Run(() =>
        {
            PART_PreviewImage.Visibility = Visibility.Collapsed;
            State = SwitcherState.Showed;
        }, 
        _animationDuration.TotalSeconds);
    }

    private SpacePreviewCard? Contain(ViewModelBase viewModel)
    {
        foreach(SpacePreviewCard card in _cards)
            if(card.Space == viewModel)
                return card;

        return null;
    }

    private void Hide()
    {
        State = SwitcherState.Hiding;

        PART_PreviewImage.Source = _cards[_currentCardIndex].Preview;

        PART_PreviewImage.Visibility = Visibility.Visible;

        _shell.ReplaceWithTo(_cards[_currentCardIndex].Space);

        PART_ScaleTrasform.BeginAnimation(ScaleTransform.ScaleXProperty, _scaleAnimationOut);
        PART_ScaleTrasform.BeginAnimation(ScaleTransform.ScaleYProperty, _scaleAnimationOut);

        TimerFactory.Run(() =>
        {
            this.Visibility = Visibility.Hidden;
            State = SwitcherState.Hidden;
        },
        _animationDuration.TotalSeconds / 2);

        SpacePreviewCard card = _cards[_currentCardIndex];

        _cards.RemoveAt(_currentCardIndex);
        _cards.Insert(0, card);
    }

    private void UpdateCards()
    {
        double width = this.ActualWidth;

        for (int index = 0; index < _cards.Count; index++)
        {
            _cards[index].Margin = new Thickness(-index * width, 0, 0, 0);
            _cards[index].SetValue(Panel.ZIndexProperty, _cards.Count - index);

            if (index == _currentCardIndex) continue;

            UpdateCardsVisual(_cards[index], 0.8);
        }

        UpdateCardsVisual(_cards[_currentCardIndex], 1);
    }

    private void UpdateCardsVisual(SpacePreviewCard card, double scale)
    {
        card.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, new DoubleAnimation()
        {
            To = scale,
            Duration = TimeSpan.Zero,
        });

        card.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, new DoubleAnimation()
        {
            To = scale,
            Duration = TimeSpan.Zero,
        });
    }

    private void SlideSpaces(object sender, MouseWheelEventArgs mouse)
    {
        if (State != SwitcherState.Showed)
            return;

        State = SwitcherState.Sliding;

        CircleEase ease = new CircleEase()
        {
            EasingMode = EasingMode.EaseInOut
        };

        AnimateScale(_cards[_currentCardIndex].Scale, 0.8, 0.4, ease);
        AnimateScale(_cards[_currentCardIndex].Scale, 0.8, 0.4, ease);

        _currentCardIndex += mouse.Delta > 0 ? 1 : -1;

        if(_currentCardIndex < 0)
        {
            _currentCardIndex = 0;
        }

        if(_currentCardIndex >= _cards.Count)
        {
            _currentCardIndex = _cards.Count - 1;
        }

        AnimateScale(_cards[_currentCardIndex].Scale, 1, 0.4, ease);
        AnimateScale(_cards[_currentCardIndex].Scale, 1, 0.4, ease);

        PART_TranslateTransform.BeginAnimation(TranslateTransform.XProperty, new DoubleAnimation()
        {
            To = _currentCardIndex * (this.ActualWidth / 2),
            Duration = TimeSpan.FromSeconds(0.4),
            EasingFunction = new CubicEase()
            {
                EasingMode = EasingMode.EaseInOut
            }
        });

        TimerFactory.Run(() => State = SwitcherState.Showed, 0.4);
    }

    private void AnimateScale(ScaleTransform transform, double to, double duration, EasingFunctionBase easing)
    {
        transform.BeginAnimation(ScaleTransform.ScaleXProperty, new DoubleAnimation()
        {
            To = to,
            Duration = TimeSpan.FromSeconds(duration),
            EasingFunction = easing
        });

        transform.BeginAnimation(ScaleTransform.ScaleYProperty, new DoubleAnimation()
        {
            To = to,
            Duration = TimeSpan.FromSeconds(duration),
            EasingFunction = easing
        });
    }  
}