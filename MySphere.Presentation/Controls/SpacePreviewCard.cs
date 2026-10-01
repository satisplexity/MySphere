using MySphere.Framework.Effects;
using System.Diagnostics;
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
                BottomRight = new(1, 0.8)
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
                BottomRight = new(1, 0.9)
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
                BottomRight = new(1, 1)
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
                BottomRight = new(1, 1)
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
                BottomRight = new(1, 1)
            }
        }
    };

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

            SetValue(WidthProperty, value.Width / 2.0);
            SetValue(HeightProperty, value.Height / 2.0);
        }
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
        Debug.WriteLine($"PositionIndex changed to {e.NewValue}");

        var card = (SpacePreviewCard)d;
        var index = (int)e.NewValue;

        Panel.SetZIndex(card, 100 + index);

        SetCardOffset(card, index);
        SetCardPerspective(card, index);
    }

    private static void SetCardOffset(SpacePreviewCard card, int index)
    {
        CardPosition cardPosition = GetCardPosition(index);

        double offset = index * card.Width * cardPosition.XOffsetModifier;

        TranslateTransform? translateTransform = card.RenderTransform as TranslateTransform;

        ArgumentNullException.ThrowIfNull(translateTransform, nameof(translateTransform));

        translateTransform.X = offset;

        translateTransform.BeginAnimation(TranslateTransform.XProperty, new DoubleAnimation()
        {
            To = offset,
            Duration = TimeSpan.FromMilliseconds(300),
            EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseInOut }
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
            Duration = TimeSpan.FromMilliseconds(300),
            EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseInOut }
        });

        effect.BeginAnimation(PerspectiveEffect.BottomRightProperty, new PointAnimation()
        {
            To = cardPosition.BottomRight,
            Duration = TimeSpan.FromMilliseconds(300),
            EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseInOut }
        });

        effect.BeginAnimation(PerspectiveEffect.TopLeftProperty, new PointAnimation()
        {
            To = cardPosition.TopLeft,
            Duration = TimeSpan.FromMilliseconds(300),
            EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseInOut }
        });

        effect.BeginAnimation(PerspectiveEffect.BottomLeftProperty, new PointAnimation()
        {
            To = cardPosition.BottomLeft,
            Duration = TimeSpan.FromMilliseconds(300),
            EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseInOut }
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