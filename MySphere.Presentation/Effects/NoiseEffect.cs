using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace MySphere.Presentation.Effects;

public class NoiseEffect : ShaderEffect
{

    public NoiseEffect()
    {
        PixelShader = new PixelShader()
        {
            UriSource = new Uri("/MySphere.Presentation;component/Resources/Shaders/Noise.ps", UriKind.Relative)
        };

        UpdateShaderValue(InputProperty);
        UpdateShaderValue(TimeProperty);
        UpdateShaderValue(IntensityProperty);
        UpdateShaderValue(ScaleProperty);
        UpdateShaderValue(SpeedProperty);
    }

    public static readonly DependencyProperty InputProperty =
        RegisterPixelShaderSamplerProperty(
            "Input",
            typeof(NoiseEffect),
            0);

    public Brush Input
    {
        get => (Brush)GetValue(InputProperty);
        set => SetValue(InputProperty, value);
    }

    public static readonly DependencyProperty TimeProperty =
        DependencyProperty.Register(
            nameof(Time),
            typeof(float),
            typeof(NoiseEffect),
            new UIPropertyMetadata(
                0f,
                PixelShaderConstantCallback(0)));

    public float Time
    {
        get => (float)GetValue(TimeProperty);
        set => SetValue(TimeProperty, value);
    }

    public static readonly DependencyProperty IntensityProperty =
        DependencyProperty.Register(
            nameof(Intensity),
            typeof(float),
            typeof(NoiseEffect),
            new UIPropertyMetadata(
                0.025f,
                PixelShaderConstantCallback(1)));

    public float Intensity
    {
        get => (float)GetValue(IntensityProperty);
        set => SetValue(IntensityProperty, value);
    }

    public static readonly DependencyProperty ScaleProperty =
        DependencyProperty.Register(
            nameof(Scale),
            typeof(float),
            typeof(NoiseEffect),
            new UIPropertyMetadata(
                400f,
                PixelShaderConstantCallback(2)));

    public float Scale
    {
        get => (float)GetValue(ScaleProperty);
        set => SetValue(ScaleProperty, value);
    }

    public static readonly DependencyProperty SpeedProperty =
        DependencyProperty.Register(
            nameof(Speed),
            typeof(float),
            typeof(NoiseEffect),
            new UIPropertyMetadata(
                0.05f,
                PixelShaderConstantCallback(3)));

    public float Speed
    {
        get => (float)GetValue(SpeedProperty);
        set => SetValue(SpeedProperty, value);
    }
}