using System.Numerics;

namespace SimpleRenderer;

public static class TransformationExtensions
{
    public static Vector3 Multiply(this Matrix4x4 matrices, Vector3 vector4)
    {
        var x = matrices.M11 * vector4.X + matrices.M21 * vector4.Y + matrices.M31 * vector4.Z + matrices.M41;
        var y = matrices.M12 * vector4.X + matrices.M22 * vector4.Y + matrices.M32 * vector4.Z + matrices.M42;
        var z = matrices.M13 * vector4.X + matrices.M23 * vector4.Y + matrices.M33 * vector4.Z + matrices.M43;

        return new Vector3(x, y, z);
    }
}