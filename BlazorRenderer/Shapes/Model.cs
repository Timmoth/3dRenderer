using System.Numerics;
using static System.Net.WebRequestMethods;

namespace SimpleRenderer.Shapes;

public static class ModelLoader
{
    private static readonly Random _rand = new();

    public static Model Load(string path)
    {
        var triangles = new List<Triangle>();
        var modelLines = new string[0];

        var v1 = Vector3.Zero;
        var v2 = Vector3.Zero;

        var index = 0;
        foreach (var modelLine in modelLines)
        {
            if (!modelLine.Contains("vertex"))
            {
                continue;
            }

            var components = modelLine.Split(" ");
            var x = float.Parse(components[3]);
            var y = float.Parse(components[4]);
            var z = float.Parse(components[5]);

            switch (index % 3)
            {
                case 0:
                    v1 = new Vector3(x, y, z);
                    break;
                case 1:
                    v2 = new Vector3(x, y, z);
                    break;
                case 2:
                    var v3 = new Vector3(x, y, z);
                    triangles.Add(new Triangle(v1, v2, v3,
                        ((byte)(255 * _rand.NextDouble()),
                            (byte)(255 * _rand.NextDouble()),
                            (byte)(255 * _rand.NextDouble()))));
                    break;
            }

            index++;
        }

        return new Model(triangles.ToArray());
    }

    public static Model GenerateBall(int radius)
    {
        var triangles = new List<Triangle>();
        triangles.Add(new Triangle(new Vector3(radius, radius, radius),
            new Vector3(-radius, -radius, radius),
            new Vector3(-radius, radius, -radius),
            Colors.White));
        triangles.Add(new Triangle(new Vector3(radius, radius, radius),
            new Vector3(-radius, -radius, radius),
            new Vector3(radius, -radius, -radius),
            Colors.Red));
        triangles.Add(new Triangle(new Vector3(-radius, radius, -radius),
            new Vector3(radius, -radius, -radius),
            new Vector3(radius, radius, radius),
            Colors.Green));
        triangles.Add(new Triangle(new Vector3(-radius, radius, -radius),
            new Vector3(radius, -radius, -radius),
            new Vector3(-radius, -radius, radius),
            Colors.Blue));

        triangles = triangles.Inflate(radius).Inflate(radius).Inflate(radius).Inflate(radius);

        return new Model(triangles.ToArray());
    }

    public static Model GeneratePrism(int radius)
    {
        var triangles = new List<Triangle>();
        triangles.Add(new Triangle(new Vector3(radius, radius, radius),
            new Vector3(-radius, -radius, radius),
            new Vector3(-radius, radius, -radius),
            Colors.White));
        triangles.Add(new Triangle(new Vector3(radius, radius, radius),
            new Vector3(-radius, -radius, radius),
            new Vector3(radius, -radius, -radius),
            Colors.Red));
        triangles.Add(new Triangle(new Vector3(-radius, radius, -radius),
            new Vector3(radius, -radius, -radius),
            new Vector3(radius, radius, radius),
            Colors.Green));
        triangles.Add(new Triangle(new Vector3(-radius, radius, -radius),
            new Vector3(radius, -radius, -radius),
            new Vector3(-radius, -radius, radius),
            Colors.Blue));

        return new Model(triangles.ToArray());
    }
}