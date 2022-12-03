namespace SimpleRenderer.Shapes;

public class Model
{
    public Model(Triangle[] triangles)
    {
        Triangles = triangles;
    }

    public Triangle[] Triangles { get; }
    public TransformationData TransformationData { get; } = new();
}