using maui_sample.Models;

namespace maui_sample;

public class BrownianDrawable : IDrawable
{
    public IReadOnlyList<BrownianGraphData>? BrownianDataList { get; set; }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.SaveState();

        canvas.FillColor = Colors.Transparent;
        canvas.FillRectangle(dirtyRect);

        if (BrownianDataList is null || BrownianDataList.Count == 0 || BrownianDataList.All(s => s.Prices is null || s.Prices.Length < 2))
        {
            canvas.RestoreState();
            return;
        }

        double min = double.PositiveInfinity, max = double.NegativeInfinity;
        foreach (var s in BrownianDataList)
        {
            if (s.Prices is null || s.Prices.Length < 2) continue;
            min = Math.Min(min, s.Prices.Min());
            max = Math.Max(max, s.Prices.Max());
        }
        if (!double.IsFinite(min) || !double.IsFinite(max))
        {
            canvas.RestoreState();
            return;
        }
        if (Math.Abs(max - min) < 1e-12) max = min + 1e-6;

        float left = 60f, right = 12f, top = 20f, bottom = 40f;
        var plot = new RectF(left, top, dirtyRect.Width - left - right, dirtyRect.Height - top - bottom);

        canvas.StrokeColor = Colors.Black;
        canvas.StrokeSize = 1f;
        canvas.DrawLine(plot.Left, plot.Bottom, plot.Right, plot.Bottom); // X
        canvas.DrawLine(plot.Left, plot.Top, plot.Left, plot.Bottom);     // Y

        // TODO: Passar como parametro para configuração do usuario !
        int gridSteps = 8;
        canvas.StrokeColor = Colors.Gray;
        canvas.StrokeSize = 1f;
        canvas.FontSize = 12;
        for (int i = 0; i <= gridSteps; i++)
        {
            float t = i / (float)gridSteps;
            float y = plot.Top + plot.Height * t;

            canvas.DrawLine(plot.Left, y, plot.Right, y);

            double value = max - (max - min) * t;
            string text = value.ToString("0.##");

            var size = canvas.GetStringSize(text, Microsoft.Maui.Graphics.Font.Default, 12);
            float xText = plot.Left - 6f - size.Width;
            float yText = y - size.Height / 2f;

            canvas.FontColor = Colors.Gray;
            canvas.DrawString(text, xText, yText, HorizontalAlignment.Left);
        }

        foreach (var s in BrownianDataList)
        {
            if (s.Prices is null || s.Prices.Length < 2) continue;

            canvas.StrokeColor = s.Color;
            canvas.StrokeSize = 2f;

            int n = s.Prices.Length;
            for (int i = 1; i < n; i++)
            {
                float x1 = plot.Left + plot.Width * (i - 1) / (n - 1);
                float x2 = plot.Left + plot.Width * i / (n - 1);

                float y1 = plot.Bottom - (float)((s.Prices[i - 1] - min) / (max - min)) * plot.Height;
                float y2 = plot.Bottom - (float)((s.Prices[i] - min) / (max - min)) * plot.Height;

                canvas.DrawLine(x1, y1, x2, y2);
            }
        }

        canvas.FontSize = 14;
        canvas.FontColor = Colors.White;
        canvas.DrawString("Simulação de Movimento Browniano (Preço / Dia)", plot, HorizontalAlignment.Center, VerticalAlignment.Top);

        canvas.RestoreState();
    }
}
