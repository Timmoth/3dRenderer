using System.Numerics;
using System.Runtime.CompilerServices;

namespace SimpleRenderer.Shapes;

public class TransformationData
{
    public Vector3? Translation { get; set; }
    public float? XRotation { get; set; }
    public float? YRotation { get; set; }
    public float? ZRotation { get; set; }
    public Vector3? Scale { get; set; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Matrix4x4 CreateTransformationMatrix()
    {
        var matrix = Matrix4x4.Identity;
        if (Scale.HasValue)
        {
            matrix = Matrix4x4.Multiply(matrix, Matrix4x4.CreateScale(Scale.Value));
        }

        if (XRotation.HasValue)
        {
            matrix = Matrix4x4.Multiply(matrix, Matrix4x4.CreateRotationX(XRotation.Value));
        }

        if (YRotation.HasValue)
        {
            matrix = Matrix4x4.Multiply(matrix, Matrix4x4.CreateRotationY(YRotation.Value));
        }

        if (ZRotation.HasValue)
        {
            matrix = Matrix4x4.Multiply(matrix, Matrix4x4.CreateRotationZ(ZRotation.Value));
        }

        if (Translation.HasValue)
        {
            matrix = Matrix4x4.Multiply(matrix, Matrix4x4.CreateTranslation(Translation.Value));
        }

        return matrix;
    }
}