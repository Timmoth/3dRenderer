using System.Numerics;

namespace SimpleRenderer.Shapes;

public static class BallExtensions
{
    public static List<Triangle> Inflate(this IEnumerable<Triangle> triangles, float radius)
    {
        List<Triangle> result = new();
        foreach (var t in triangles)
        {
            var m1 = new Vector3((t.V1.X + t.V2.X) / 2, (t.V1.Y + t.V2.Y) / 2, (t.V1.Z + t.V2.Z) / 2);
            var m2 = new Vector3((t.V2.X + t.V3.X) / 2, (t.V2.Y + t.V3.Y) / 2, (t.V2.Z + t.V3.Z) / 2);
            var m3 = new Vector3((t.V1.X + t.V3.X) / 2, (t.V1.Y + t.V3.Y) / 2, (t.V1.Z + t.V3.Z) / 2);
            result.Add(CreateTriangle(t.V1, m1, m3, t.Color, radius));
            result.Add(CreateTriangle(t.V2, m1, m2, t.Color, radius));
            result.Add(CreateTriangle(t.V3, m2, m3, t.Color, radius));
            result.Add(CreateTriangle(m1, m2, m3, t.Color, radius));
        }

        return result;
    }

    private static Triangle CreateTriangle(Vector3 V1, Vector3 V2, Vector3 V3, (byte R, byte G, byte B) color,
        float radius)
    {
        var r = Math.Sqrt(3 * radius * radius);

        var v1 = V1 / (float)(Math.Sqrt(V1.X * V1.X + V1.Y * V1.Y + V1.Z * V1.Z) / r);
        var v2 = V2 / (float)(Math.Sqrt(V2.X * V2.X + V2.Y * V2.Y + V2.Z * V2.Z) / r);
        var v3 = V3 / (float)(Math.Sqrt(V3.X * V3.X + V3.Y * V3.Y + V3.Z * V3.Z) / r);

        return new Triangle(v1, v2, v3, color);
    }
}