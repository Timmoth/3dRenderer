using System.Numerics;

namespace SimpleRenderer.Shapes;

public record struct Triangle(Vector3 V1, Vector3 V2, Vector3 V3, (byte R, byte G, byte B) Color);