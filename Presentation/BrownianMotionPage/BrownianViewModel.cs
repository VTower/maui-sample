using maui_sample.Helper;
using maui_sample.Models;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace maui_sample.ViewModels;

public partial class BrownianViewModel : ObservableObject
{
    [ObservableProperty] private double initialPrice = 100.0;
    [ObservableProperty] private double sigma = 0.02;
    [ObservableProperty] private double mean = 0.0005;
    [ObservableProperty] private int numDays = 252;

    public ObservableCollection<BrownianGraphData> BrownianDataList { get; } = [];

    private readonly Color[] _palette =
    [
        Colors.DodgerBlue, Colors.OrangeRed, Colors.SeaGreen, Colors.MediumPurple,
        Colors.Chocolate, Colors.DeepPink, Colors.Teal, Colors.Goldenrod,
        Colors.Crimson, Colors.SlateBlue
    ];

    private Color GetColorForIndex(int idx) => _palette[idx % _palette.Length];

    // Actions
    public event Action<IReadOnlyList<BrownianGraphData>>? BrownianDataListUpdated;

    // Commands 
    [RelayCommand]
    private void AddSimulation()
    {
        if (InitialPrice <= 0) InitialPrice = 1;
        if (Sigma < 0) Sigma = 0;
        if (NumDays < 2) NumDays = 2;

        double[]? prices = BrownianModelHelper.GenerateBrownianMotion(Sigma, Mean, InitialPrice, NumDays);

        // TODO: Adicionar configuração pelo usuario
        Color? color = GetColorForIndex(BrownianDataList.Count);
        // TODO: Adicionar no Gráfico o nome
        string? name = $"{BrownianDataList.Count} (μ={Mean:0.####}, σ={Sigma:0.####})";

        BrownianDataList.Add(new BrownianGraphData(name, color, prices));
        BrownianDataListUpdated?.Invoke(BrownianDataList);
    }

    [RelayCommand]
    private void ClearAll()
    {
        BrownianDataList.Clear();
        BrownianDataListUpdated?.Invoke(BrownianDataList);
    }

    [RelayCommand]
    private void RemoveLast()
    {
        if (BrownianDataList.Count == 0) return;
        BrownianDataList.RemoveAt(BrownianDataList.Count - 1);
        BrownianDataListUpdated?.Invoke(BrownianDataList);
    }

    [RelayCommand]
    private async Task NavigateToHomePage()
    {
        await Shell.Current.GoToAsync(nameof(MainViewModel));
    }

    public void InitializeIfEmpty()
    {
        if (BrownianDataList.Count == 0)
            AddSimulation();
    }
}
