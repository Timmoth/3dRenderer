namespace SimpleRenderer;

public static class Colors
{
    public static (byte R, byte G, byte B) White => new(255, 255, 255);
    public static (byte R, byte G, byte B) Red => new(255, 0, 0);
    public static (byte R, byte G, byte B) Green => new(0, 255, 0);
    public static (byte R, byte G, byte B) Blue => new(0, 0, 255);

    public static (byte R, byte G, byte B) Shade((byte R, byte G, byte B) color, double shade)
    {
        var power = 2.4;
        var oneOverPower = 1.0 / power;

        var redLinear = Math.Pow(color.R, power) * shade;
        var greenLinear = Math.Pow(color.G, power) * shade;
        var blueLinear = Math.Pow(color.B, power) * shade;

        var red = (byte)Math.Pow(redLinear, oneOverPower);
        var green = (byte)Math.Pow(greenLinear, oneOverPower);
        var blue = (byte)Math.Pow(blueLinear, oneOverPower);

        return (red, green, blue);
    }
}