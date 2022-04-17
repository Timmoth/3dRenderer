using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace SimpleRenderer.Output;

public class BitmapOutput : IOutput
{
    public (byte R, byte G, byte B)[][] _pixelData;

    public void Set((byte R, byte G, byte B)[][] pixelData)
    {
        _pixelData = pixelData;
    }

    public void Save()
    {
        var width = _pixelData[0].Length;
        var height = _pixelData.Length;
        using var image = new Image<Rgba32>(width, height);

        image.ProcessPixelRows(accessor =>
        {
            for (var y = 0; y < accessor.Height; y++)
            {
                var pixelRow = accessor.GetRowSpan(y);

                // pixelRow.Length has the same value as accessor.Width,
                // but using pixelRow.Length allows the JIT to optimize away bounds checks:
                for (var x = 0; x < pixelRow.Length; x++)
                {
                    // Get a reference to the pixel at position x
                    ref var pixel = ref pixelRow[x];
                    var pixelData = _pixelData[y][x];
                    pixel = new Rgba32(pixelData.R, pixelData.G, pixelData.B);
                }
            }
        });

        image.SaveAsBmp("./image.bmp");
    }

    public void Dispose()
    {
    }
}