using System.Runtime.CompilerServices;

namespace SimpleRenderer;

public static class Colors
{
    public static (byte R, byte G, byte B) White => new(255, 255, 255);
    public static (byte R, byte G, byte B) Red => new(255, 0, 0);
    public static (byte R, byte G, byte B) Green => new(0, 255, 0);
    public static (byte R, byte G, byte B) Blue => new(0, 0, 255);

    private static readonly float power = 2.4f;
    private static readonly float oneOverPower = 1.0f / 2.4f;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Shade((byte R, byte G, byte B) color, double shade, int[]pixelData, int i)
    {
        var redLinear = Math.Pow(color.R, power) * shade;
        var greenLinear = Math.Pow(color.G, power) * shade;
        var blueLinear = Math.Pow(color.B, power) * shade;

        pixelData[i] =
        (255 << 24) |    // alpha
        ((byte)Math.Pow(blueLinear, oneOverPower) << 16) |    // blue
        ((byte)Math.Pow(greenLinear, oneOverPower) << 8) |    // green
         (byte)Math.Pow(redLinear, oneOverPower);            // red
    }
}