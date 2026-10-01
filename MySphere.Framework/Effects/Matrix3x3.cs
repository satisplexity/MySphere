namespace MySphere.Framework.Effects;

public readonly struct Matrix3x3
{
    public Matrix3x3(
        double m11,
        double m12,
        double m13,
        double m21,
        double m22,
        double m23,
        double m31,
        double m32,
        double m33)
    {
        M11 = m11;
        M12 = m12;
        M13 = m13;

        M21 = m21;
        M22 = m22;
        M23 = m23;

        M31 = m31;
        M32 = m32;
        M33 = m33;
    }

    public double M11 { get; }
    public double M12 { get; }
    public double M13 { get; }

    public double M21 { get; }
    public double M22 { get; }
    public double M23 { get; }

    public double M31 { get; }
    public double M32 { get; }
    public double M33 { get; }
}
