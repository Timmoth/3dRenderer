namespace SimpleRenderer.Output;

public interface IOutput : IDisposable
{
    void Set((byte R, byte G, byte B)[][] pixelData);
    void Save();
}