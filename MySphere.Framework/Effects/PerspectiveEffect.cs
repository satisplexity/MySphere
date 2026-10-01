using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace MySphere.Framework.Effects;

public class PerspectiveEffect : ShaderEffect
{
    private readonly PixelShader _pixelShader = new()
    {
        UriSource = new Uri(
        "/MySphere.Framework;component/Shaders/PerspectiveShader.ps",
        UriKind.Relative)
    };

    public PerspectiveEffect()
    {
        PixelShader = _pixelShader;

        UpdateShaderValue(InputProperty);

        UpdateShaderValue(M11Property);
        UpdateShaderValue(M12Property);
        UpdateShaderValue(M13Property);

        UpdateShaderValue(M21Property);
        UpdateShaderValue(M22Property);
        UpdateShaderValue(M23Property);

        UpdateShaderValue(M31Property);
        UpdateShaderValue(M32Property);
        UpdateShaderValue(M33Property);

        UpdateMatrix();
    }

    public static readonly DependencyProperty InputProperty =
        RegisterPixelShaderSamplerProperty(
            nameof(Input),
            typeof(PerspectiveEffect),
            0,
            SamplingMode.Bilinear);

    public Brush Input
    {
        get => (Brush)GetValue(InputProperty);
        set => SetValue(InputProperty, value);
    }

    public static readonly DependencyProperty TopLeftProperty =
        DependencyProperty.Register(
            nameof(TopLeft),
            typeof(Point),
            typeof(PerspectiveEffect),
            new UIPropertyMetadata(
                new Point(0, 0),
                OnCornerChanged));

    public Point TopLeft
    {
        get => (Point)GetValue(TopLeftProperty);
        set => SetValue(TopLeftProperty, value);
    }

    public static readonly DependencyProperty TopRightProperty =
        DependencyProperty.Register(
            nameof(TopRight),
            typeof(Point),
            typeof(PerspectiveEffect),
            new UIPropertyMetadata(
                new Point(1, 0),
                OnCornerChanged));

    public Point TopRight
    {
        get => (Point)GetValue(TopRightProperty);
        set => SetValue(TopRightProperty, value);
    }

    public static readonly DependencyProperty BottomRightProperty =
        DependencyProperty.Register(
            nameof(BottomRight),
            typeof(Point),
            typeof(PerspectiveEffect),
            new UIPropertyMetadata(
                new Point(1, 1),
                OnCornerChanged));

    public Point BottomRight
    {
        get => (Point)GetValue(BottomRightProperty);
        set => SetValue(BottomRightProperty, value);
    }

    public static readonly DependencyProperty BottomLeftProperty =
        DependencyProperty.Register(
            nameof(BottomLeft),
            typeof(Point),
            typeof(PerspectiveEffect),
            new UIPropertyMetadata(
                new Point(0, 1),
                OnCornerChanged));

    public Point BottomLeft
    {
        get => (Point)GetValue(BottomLeftProperty);
        set => SetValue(BottomLeftProperty, value);
    }

    private static void OnCornerChanged(
        DependencyObject d,
        DependencyPropertyChangedEventArgs e)
    {
        ((PerspectiveEffect)d).UpdateMatrix();
    }

    private static readonly DependencyProperty M11Property =
        RegisterConstant(nameof(M11), 0);

    private static readonly DependencyProperty M12Property =
        RegisterConstant(nameof(M12), 1);

    private static readonly DependencyProperty M13Property =
        RegisterConstant(nameof(M13), 2);

    private static readonly DependencyProperty M21Property =
        RegisterConstant(nameof(M21), 3);

    private static readonly DependencyProperty M22Property =
        RegisterConstant(nameof(M22), 4);

    private static readonly DependencyProperty M23Property =
        RegisterConstant(nameof(M23), 5);

    private static readonly DependencyProperty M31Property =
        RegisterConstant(nameof(M31), 6);

    private static readonly DependencyProperty M32Property =
        RegisterConstant(nameof(M32), 7);

    private static readonly DependencyProperty M33Property =
        RegisterConstant(nameof(M33), 8);

    private double M11
    {
        get => (double)GetValue(M11Property);
        set => SetValue(M11Property, value);
    }

    private double M12
    {
        get => (double)GetValue(M12Property);
        set => SetValue(M12Property, value);
    }

    private double M13
    {
        get => (double)GetValue(M13Property);
        set => SetValue(M13Property, value);
    }

    private double M21
    {
        get => (double)GetValue(M21Property);
        set => SetValue(M21Property, value);
    }

    private double M22
    {
        get => (double)GetValue(M22Property);
        set => SetValue(M22Property, value);
    }

    private double M23
    {
        get => (double)GetValue(M23Property);
        set => SetValue(M23Property, value);
    }

    private double M31
    {
        get => (double)GetValue(M31Property);
        set => SetValue(M31Property, value);
    }

    private double M32
    {
        get => (double)GetValue(M32Property);
        set => SetValue(M32Property, value);
    }

    private double M33
    {
        get => (double)GetValue(M33Property);
        set => SetValue(M33Property, value);
    }

    private static DependencyProperty RegisterConstant(
        string name,
        int register)
    {
        return DependencyProperty.Register(
            name,
            typeof(double),
            typeof(PerspectiveEffect),
            new UIPropertyMetadata(
                0.0,
                PixelShaderConstantCallback(register)));
    }

    private void UpdateMatrix()
    {
        var matrix = Homography.CreateInverse(
            TopLeft,
            TopRight,
            BottomRight,
            BottomLeft);

        M11 = matrix.M11;
        M12 = matrix.M12;
        M13 = matrix.M13;

        M21 = matrix.M21;
        M22 = matrix.M22;
        M23 = matrix.M23;

        M31 = matrix.M31;
        M32 = matrix.M32;
        M33 = matrix.M33;
    }
}