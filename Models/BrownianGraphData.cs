namespace maui_sample.Models;

public sealed class BrownianGraphData(string name, Color color, double[] prices)
{
    public string Name { get; } = name;
    public Color Color { get; } = color;
    public double[] Prices { get; } = prices;
}