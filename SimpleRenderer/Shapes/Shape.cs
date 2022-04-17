namespace SimpleRenderer.Shapes;

public class Model
{
    public Model(List<Triangle> triangles)
    {
        Triangles = triangles;
    }

    public List<Triangle> Triangles { get; }
    public TransformationData TransformationData { get; } = new();
}