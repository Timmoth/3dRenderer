using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace SimpleRenderer.Output;

public class GifOutput : IOutput
{
    private readonly int _frameDelay = 10;
    private readonly Image<Rgba32> _gif;
    private readonly int _height;
    private readonly int _width;

    public GifOutput(int width, int height)
    {
        _width = width;
        _height = height;
        // Create empty image.
        _gif = new Image<Rgba32>(width, height, Color.Black);

        // Set animation loop repeat count to 5.
        var gifMetaData = _gif.Metadata.GetGifMetadata();
        gifMetaData.RepeatCount = 5;

        // Set the delay until the next image is displayed.
        var metadata = _gif.Frames.RootFrame.Metadata.GetGifMetadata();
        metadata.FrameDelay = _frameDelay;
    }

    public void Set((byte R, byte G, byte B)[][] pixelData)
    {
        // Create a color image, which will be added to the gif.
        using Image<Rgba32> image = new(_width, _height);

        image.ProcessPixelRows(accessor =>
        {
            for (var y = 0; y < accessor.Height; y++)
            {
                var pixelRow = accessor.GetRowSpan(y);
                var rowPixelData = pixelData[y];

                for (var x = 0; x < pixelRow.Length; x++)
                {
                    var pixelData = rowPixelData[x];
                    ref var pixel = ref pixelRow[x];
                    pixel = new Rgba32(pixelData.R, pixelData.G, pixelData.B);
                }
            }
        });

        // Set the delay until the next image is displayed.
        var metadata = image.Frames.RootFrame.Metadata.GetGifMetadata();
        metadata.FrameDelay = _frameDelay;

        // Add the color image to the gif.
        _gif.Frames.AddFrame(image.Frames.RootFrame);
    }

    public void Save()
    {
        // Save the final result.
        _gif.SaveAsGif("./output.gif");
    }

    public void Dispose()
    {
        _gif.Dispose();
    }
}