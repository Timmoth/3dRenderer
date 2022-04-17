using System.Numerics;
using SimpleRenderer;
using SimpleRenderer.Output;
using SimpleRenderer.Shapes;

var width = 1000;
var height = 1000;
var renderer = new Renderer(width, height);

var model = ModelLoader.Load("./model.stl");
model.TransformationData.Translation = new Vector3(500, 500, 1);
model.TransformationData.XRotation = 0.4f;
model.TransformationData.YRotation = 1.4f;
model.TransformationData.ZRotation = 1.2f;
model.TransformationData.Scale = new Vector3(20, 20, 20);
renderer.Add(model);

var pixelData = new (byte R, byte G, byte B)[height][];
for (var y = 0; y < height; y++)
{
    pixelData[y] = new (byte R, byte G, byte B)[width];
    for (var x = 0; x < width; x++)
    {
        pixelData[y][x] = new ValueTuple<byte, byte, byte>(0, 0, 0);
    }
}

var output = new GifOutput(width, height);
for (var i = 0; i < 100; i++)
{
    model.TransformationData.XRotation += 0.01f;
    model.TransformationData.YRotation += 0.02f;
    model.TransformationData.ZRotation += 0.03f;
    renderer.Render(pixelData);
    output.Set(pixelData);
}

output.Save();