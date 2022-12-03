using System.Numerics;
using SimpleRenderer.Shapes;

namespace SimpleRenderer;

public class Renderer
{
    private readonly List<Model> _models = new();
    private readonly float[] ZBuffer;

    public Renderer(int width, int height)
    {
        Width = width;
        Height = height;
        ZBuffer = new float[Width * Height];
    }

    public int Width { get; init; }
    public int Height { get; init; }

    public void Add(Model model)
    {
        _models.Add(model);
    }

    public void Render(int[] pixelData)
    {
        //Clear pixel data
        var pixelDataLength = Width * Height;
        Array.Clear(pixelData, 0, pixelDataLength);

        //initialize array with extremely far away depths
        Span<float> zBufferAsSpan = ZBuffer;
        for (var q = 0; q < ZBuffer.Length; q++)
        {
            zBufferAsSpan[q] = float.NegativeInfinity;
        }

        //Render each shape
        foreach (var shape in _models)
        {
            var matrix = shape.TransformationData.CreateTransformationMatrix();

            Span<Triangle> triangles = shape.Triangles;
            for(int i = 0; i < triangles.Length; i++)
            {
                var t = triangles[i];
                //Transform
                var v1 = matrix.Multiply(t.V1);
                var v2 = matrix.Multiply(t.V2);
                var v3 = matrix.Multiply(t.V3);

                var ab = v2 - v1;
                var ac = v3 - v1;

                var norm = Vector3.Normalize(new Vector3(
                    ab.Y * ac.Z - ab.Z * ac.Y,
                    ab.Z * ac.X - ab.X * ac.Z,
                    ab.X * ac.Y - ab.Y * ac.X
                ));

                var minX = (int)Math.Max(0, Math.Ceiling(Math.Min(v1.X, Math.Min(v2.X, v3.X))));
                var maxX = (int)Math.Min(Width - 1, Math.Floor(Math.Max(v1.X, Math.Max(v2.X, v3.X))));
                var minY = (int)Math.Max(0, Math.Ceiling(Math.Min(v1.Y, Math.Min(v2.Y, v3.Y))));
                var maxY = (int)Math.Min(Height - 1, Math.Floor(Math.Max(v1.Y, Math.Max(v2.Y, v3.Y))));

                var triangleArea = (v1.Y - v3.Y) * (v2.X - v3.X) + (v2.Y - v3.Y) * (v3.X - v1.X);
                var angleCos = Math.Abs(norm.Z);

                for (var Y = minY; Y <= maxY; Y++)
                {
                    for (var X = minX; X <= maxX; X++)
                    {
                        var b1 = ((Y - v3.Y) * (v2.X - v3.X) + (v2.Y - v3.Y) * (v3.X - X)) / triangleArea;
                        var b2 = ((Y - v1.Y) * (v3.X - v1.X) + (v3.Y - v1.Y) * (v1.X - X)) / triangleArea;
                        var b3 = ((Y - v2.Y) * (v1.X - v2.X) + (v1.Y - v2.Y) * (v2.X - X)) / triangleArea;
                        if (b1 is < 0 or > 1 || b2 is < 0 or > 1 || b3 is < 0 or > 1)
                        {
                            continue;
                        }

                        var depth = b1 * v1.Z + b2 * v2.Z + b3 * v3.Z;
                        var ZIndeX = Y * Width + X;
                        if (zBufferAsSpan[ZIndeX] < depth)
                        {
                            zBufferAsSpan[ZIndeX] = depth;
                            Colors.Shade(t.Color, angleCos, pixelData, (Y * Width ) + (X ));
                        }
                    }
                }
            }
        }
    }
}