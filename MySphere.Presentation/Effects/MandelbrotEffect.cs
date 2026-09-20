using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace MySphere.Presentation.Effects;

public class MandelbrotEffect : ShaderEffect
{
    private static readonly PixelShader Shader = new()
    {
        UriSource = new Uri(
        "pack://application:,,,/MySphere.Presentation;component/Resources/Shaders/Mandelbrot.ps",
        UriKind.Absolute)
    };

    static MandelbrotEffect()
    {
        PixelShaderProperty.OverrideMetadata(
            typeof(MandelbrotEffect),
            new UIPropertyMetadata(Shader));
    }

    public MandelbrotEffect()
    {
        // s0
        Input = new VisualBrush();

        // Чтобы shader мог работать как Effect на элементе.
        PaddingTop = 0;
        PaddingBottom = 0;
        PaddingLeft = 0;
        PaddingRight = 0;
    }

    // =====================================================
    // Input
    // =====================================================

    public Brush Input
    {
        get => (Brush)GetValue(InputProperty);
        set => SetValue(InputProperty, value);
    }

    public static readonly DependencyProperty InputProperty =
        RegisterPixelShaderSamplerProperty(
            nameof(Input),
            typeof(MandelbrotEffect),
            0);

    // =====================================================
    // c0 : CenterHi
    // =====================================================

    public Point CenterHi
    {
        get => (Point)GetValue(CenterHiProperty);
        set => SetValue(CenterHiProperty, value);
    }

    public static readonly DependencyProperty CenterHiProperty =
        DependencyProperty.Register(
            nameof(CenterHi),
            typeof(Point),
            typeof(MandelbrotEffect),
            new UIPropertyMetadata(
                new Point(0, 0),
                PixelShaderConstantCallback(0)));

    // =====================================================
    // c1 : CenterLo
    // =====================================================

    public Point CenterLo
    {
        get => (Point)GetValue(CenterLoProperty);
        set => SetValue(CenterLoProperty, value);
    }

    public static readonly DependencyProperty CenterLoProperty =
        DependencyProperty.Register(
            nameof(CenterLo),
            typeof(Point),
            typeof(MandelbrotEffect),
            new UIPropertyMetadata(
                new Point(0, 0),
                PixelShaderConstantCallback(1)));

    // =====================================================
    // c2 : ScaleHi
    // =====================================================

    public float ScaleHi
    {
        get => (float)GetValue(ScaleHiProperty);
        set => SetValue(ScaleHiProperty, value);
    }

    public static readonly DependencyProperty ScaleHiProperty =
        DependencyProperty.Register(
            nameof(ScaleHi),
            typeof(float),
            typeof(MandelbrotEffect),
            new UIPropertyMetadata(
                1.0f,
                PixelShaderConstantCallback(2)));

    // =====================================================
    // c3 : ScaleLo
    // =====================================================

    public float ScaleLo
    {
        get => (float)GetValue(ScaleLoProperty);
        set => SetValue(ScaleLoProperty, value);
    }

    public static readonly DependencyProperty ScaleLoProperty =
        DependencyProperty.Register(
            nameof(ScaleLo),
            typeof(float),
            typeof(MandelbrotEffect),
            new UIPropertyMetadata(
                0.0f,
                PixelShaderConstantCallback(3)));

    // =====================================================
    // c4 : Aspect
    // =====================================================

    public float Aspect
    {
        get => (float)GetValue(AspectProperty);
        set => SetValue(AspectProperty, value);
    }

    public static readonly DependencyProperty AspectProperty =
        DependencyProperty.Register(
            nameof(Aspect),
            typeof(float),
            typeof(MandelbrotEffect),
            new UIPropertyMetadata(
                1.0f,
                PixelShaderConstantCallback(4)));

    // =====================================================
    // c5 : BackgroundColor
    // =====================================================

    public Color BackgroundColor
    {
        get => (Color)GetValue(BackgroundColorProperty);
        set => SetValue(BackgroundColorProperty, value);
    }

    public static readonly DependencyProperty BackgroundColorProperty =
        DependencyProperty.Register(
            nameof(BackgroundColor),
            typeof(Color),
            typeof(MandelbrotEffect),
            new UIPropertyMetadata(
                Colors.Black,
                PixelShaderConstantCallback(5)));

    // =====================================================
    // c6 : EscapeColor
    // =====================================================

    public Color EscapeColor
    {
        get => (Color)GetValue(EscapeColorProperty);
        set => SetValue(EscapeColorProperty, value);
    }

    public static readonly DependencyProperty EscapeColorProperty =
        DependencyProperty.Register(
            nameof(EscapeColor),
            typeof(Color),
            typeof(MandelbrotEffect),
            new UIPropertyMetadata(
                Colors.White,
                PixelShaderConstantCallback(6)));

    // =====================================================
    // c7 : SetColor
    // =====================================================

    public Color SetColor
    {
        get => (Color)GetValue(SetColorProperty);
        set => SetValue(SetColorProperty, value);
    }

    public static readonly DependencyProperty SetColorProperty =
        DependencyProperty.Register(
            nameof(SetColor),
            typeof(Color),
            typeof(MandelbrotEffect),
            new UIPropertyMetadata(
                Colors.Black,
                PixelShaderConstantCallback(7)));
}
