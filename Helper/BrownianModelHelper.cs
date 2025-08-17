namespace maui_sample.Helper;

public static class BrownianModelHelper
{
    // Função fornecida (mantida e levemente adaptada para null-checks)
    public static double[] GenerateBrownianMotion(double sigma, double mean, double initialPrice, int numDays)
    {
        // Security guarantee add by me
        if (numDays < 1) numDays = 1;

        Random rand = new();
        double[]? prices = new double[numDays];
        prices[0] = initialPrice;

        for (int i = 1; i < numDays; i++)
        {
            double u1 = 1.0 - rand.NextDouble();
            double u2 = 1.0 - rand.NextDouble();
            double z = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);

            double retornoDiario = mean + sigma * z;

            prices[i] = prices[i - 1] * Math.Exp(retornoDiario);
        }

        return prices;
    }
}
