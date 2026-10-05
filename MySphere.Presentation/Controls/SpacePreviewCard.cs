using MySphere.Framework.Effects;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace MySphere.Presentation.Controls;

internal struct CardPosition
{
    public double XOffsetModifier { get; set; }
    public Point TopLeft { get; set; }
    public Point TopRight { get; set; }
    public Point BottomLeft { get; set; }
    public Point BottomRight { get; set; } 
    public Color MaskColor { get; set; }
}

public sealed class SpacePreviewCard : Button
{
    private static Dictionary<int, CardPosition> _cardPositions = new()
    {
        {
            -2,
            new()
            {
                XOffsetModifier = 0.5,
                TopLeft = new(0, 0),
                TopRight = new(1, 0.2),
                BottomLeft = new(0, 1),
                BottomRight = new(1, 0.8),
                MaskColor = Color.FromArgb(0, 255, 255, 255)
            }
        },
        {
            -1,
            new()
            {
                XOffsetModifier = 0.5,
                TopLeft = new(0, 0),
                TopRight = new(1, 0.1),
                BottomLeft = new(0, 1),
                BottomRight = new(1, 0.9),
                MaskColor = Color.FromArgb(0, 255, 255, 255)
            }
        },
        {
            0,
            new()
            {
                XOffsetModifier = 0,
                TopLeft = new(0, 0),
                TopRight = new(1, 0),
                BottomLeft = new(0, 1),
                BottomRight = new(1, 1),
                MaskColor = Color.FromArgb(255, 255, 255, 255)
            }
        },
        {
            1,
            new()
            {
                XOffsetModifier = 1,
                TopLeft = new(0, 0.1),
                TopRight = new(1, 0),
                BottomLeft = new(0, 0.9),
                BottomRight = new(1, 1),
                MaskColor = Color.FromArgb(255, 255, 255, 255)
            }
        },
        {
            2,
            new()
            {
                XOffsetModifier = 1,
                TopLeft = new(0, 0.2),
                TopRight = new(1, 0),
                BottomLeft = new(0, 0.8),
                BottomRight = new(1, 1),
                MaskColor = Color.FromArgb(255, 255, 255, 255)
            }
        }
    };

    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(
            nameof(Title),
            typeof(string),
            typeof(SpacePreviewCard));

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly DependencyProperty PreviewImageProperty = DependencyProperty.Register(
        nameof(PreviewImage),
        typeof(BitmapSource),
        typeof(SpacePreviewCard),
        new PropertyMetadata(null));

    public BitmapSource PreviewImage
    {
        get => (BitmapSource)GetValue(PreviewImageProperty);
        set
        {
            SetValue(PreviewImageProperty, value);
        }
    }

    public static readonly DependencyProperty IsIndexSmallerZeroProperty =
        DependencyProperty.Register(
            nameof(IsIndexSmallerZero),
            typeof(bool),
            typeof(SpacePreviewCard));

    public bool IsIndexSmallerZero
    {
        get => (bool)GetValue(IsIndexSmallerZeroProperty);
        set => SetValue(IsIndexSmallerZeroProperty, value);
    }

    public static readonly DependencyProperty PositionIndexProperty =
    DependencyProperty.Register(
        nameof(PositionIndex),
        typeof(int),
        typeof(SpacePreviewCard),
        new PropertyMetadata(0, OnPositionIndexChanged));

    public int PositionIndex
    {
        get => (int)GetValue(PositionIndexProperty);
        set => SetValue(PositionIndexProperty, value);
    }

    private static void OnPositionIndexChanged(
        DependencyObject d,
        DependencyPropertyChangedEventArgs e)
    {
        var card = (SpacePreviewCard)d;
        var index = (int)e.NewValue;

        card.SetValue(Panel.ZIndexProperty, 100 + index);

        SetCardOffset(card, index);
        SetCardPerspective(card, index);

        card.IsIndexSmallerZero = index < 0;

        CardPosition cardPosition = GetCardPosition(index);

        card.BeginAnimation(MaskColorProperty, new ColorAnimation()
        {
            To = cardPosition.MaskColor,
            Duration = TimeSpan.FromMilliseconds(400),
            EasingFunction = new SineEase() { EasingMode = EasingMode.EaseOut }
        });
    }

    private static void SetCardOffset(SpacePreviewCard card, int index)
    {
        CardPosition cardPosition = GetCardPosition(index);

        double offset = index * card.Width * cardPosition.XOffsetModifier;

        TranslateTransform? translateTransform = card.RenderTransform as TranslateTransform;

        ArgumentNullException.ThrowIfNull(translateTransform, nameof(translateTransform));

        translateTransform.BeginAnimation(TranslateTransform.XProperty, new DoubleAnimation()
        {
            To = offset,
            Duration = TimeSpan.FromMilliseconds(400),
            EasingFunction = new SineEase() { EasingMode = EasingMode.EaseInOut }
        });
    }

    private static CardPosition GetCardPosition(int index)
    {
        if (index > 2)
        {
            return _cardPositions[2];
        }

        if (index < -2)
        {
            return _cardPositions[-2];
        }

        return _cardPositions[index];
    }

    private static void SetCardPerspective(SpacePreviewCard card, int index)
    {
        CardPosition cardPosition = GetCardPosition(index);

        PerspectiveEffect? effect = card.Effect as PerspectiveEffect;
        ArgumentNullException.ThrowIfNull(effect, nameof(effect));

        effect.BeginAnimation(PerspectiveEffect.TopRightProperty, new PointAnimation()
        {
            To = cardPosition.TopRight,
            Duration = TimeSpan.FromMilliseconds(400),
            EasingFunction = new SineEase() { EasingMode = EasingMode.EaseInOut }
        });

        effect.BeginAnimation(PerspectiveEffect.BottomRightProperty, new PointAnimation()
        {
            To = cardPosition.BottomRight,
            Duration = TimeSpan.FromMilliseconds(400),
            EasingFunction = new SineEase() { EasingMode = EasingMode.EaseInOut }
        });

        effect.BeginAnimation(PerspectiveEffect.TopLeftProperty, new PointAnimation()
        {
            To = cardPosition.TopLeft,
            Duration = TimeSpan.FromMilliseconds(400),
            EasingFunction = new SineEase() { EasingMode = EasingMode.EaseInOut }
        });

        effect.BeginAnimation(PerspectiveEffect.BottomLeftProperty, new PointAnimation()
        {
            To = cardPosition.BottomLeft,
            Duration = TimeSpan.FromMilliseconds(400),
            EasingFunction = new SineEase() { EasingMode = EasingMode.EaseInOut }
        });
    }

    public static readonly DependencyProperty XOffsetProperty = DependencyProperty.Register(
        nameof(XOffset),
        typeof(double),
        typeof(SpacePreviewCard),
        new PropertyMetadata(0.0));

    public double XOffset
    {
        get => (double)GetValue(XOffsetProperty);
        set => SetValue(XOffsetProperty, value);
    }

    public static readonly DependencyProperty MaskOpacityProperty
        = DependencyProperty.Register(
            nameof(MaskOpacity),
            typeof(double),
            typeof(SpacePreviewCard));

    public double MaskOpacity
    {
        get => (double)GetValue(MaskOpacityProperty);
        set => SetValue(MaskOpacityProperty, value);
    }

    public static readonly DependencyProperty MaskColorProperty
        = DependencyProperty.Register(
            nameof(MaskColor),
            typeof(Color),
            typeof(SpacePreviewCard));

    public Color MaskColor
    {
        get => (Color)GetValue(MaskColorProperty);
        set => SetValue(MaskColorProperty, value);
    }

    public static readonly DependencyProperty PerspectiveTopRightProperty = DependencyProperty.Register(
        nameof(PerspectiveTopRight),
        typeof(Point),
        typeof(SpacePreviewCard),
        new PropertyMetadata(new Point(1, 0)));

    public Point PerspectiveTopRight
    {
        get => (Point)GetValue(PerspectiveTopRightProperty);
        set => SetValue(PerspectiveTopRightProperty, value);
    }

    public static readonly DependencyProperty PerspectiveBottomRightProperty = DependencyProperty.Register(
        nameof(PerspectiveBottomRight),
        typeof(Point),
        typeof(SpacePreviewCard),
        new PropertyMetadata(new Point(1, 0)));

    public Point PerspectiveBottomRight
    {
        get => (Point)GetValue(PerspectiveBottomRightProperty);
        set => SetValue(PerspectiveBottomRightProperty, value);
    }

    public SpacePreviewCard()
    {
        this.RenderTransform = new TranslateTransform();
        this.Effect = new PerspectiveEffect();
    }
}