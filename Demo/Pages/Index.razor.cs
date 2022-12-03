using Aptacode.BlazorCanvas;
using Microsoft.AspNetCore.Components;
using SimpleRenderer.Shapes;
using System.Net.Http;
using System.Numerics;
using static System.Net.WebRequestMethods;

namespace Demo.Pages
{
    public class IndexPageBase : ComponentBase
    {
        protected BlazorCanvas Canvas { get; set; }
        protected int Width { get; set; } = 500;
        protected int Height { get; set; } = 500;
        protected override async Task OnInitializedAsync()
        {
            var renderer = new SimpleRenderer.Renderer(Width, Height);

            var pixelDataLength = Width * Height;
            var pixelData = new int[pixelDataLength];
            var model = ModelLoader.GenerateBall(20);
            renderer.Add(model);
            var xr = 0f;
            var yr = 0f;
            var zr = 0f;

            while(Canvas is not { Ready: true })
            {
                await Task.Delay(10);
            }

            Canvas.SetImageBuffer(pixelData);

            while (true)
            {
                xr += 0.4f;
                yr += 0.2f;
                zr += 0.7f;
                model.TransformationData.Translation = new Vector3(250, 250, 1);
                model.TransformationData.XRotation = xr;
                model.TransformationData.YRotation = yr;
                model.TransformationData.ZRotation = zr;

                renderer.Render(pixelData);

                Canvas.DrawImageBuffer(0, 0, Width, Height);

                await Task.Delay(10);
            }
        }
    }
}
