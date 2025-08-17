
using maui_sample.ViewModels;

namespace maui_sample;

public partial class BrownianPage : ContentPage
{
    private readonly BrownianDrawable _drawable = new();

    public BrownianPage(BrownianViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;

        ChartView.Drawable = _drawable;

        vm.BrownianDataListUpdated += series =>
        {
            _drawable.BrownianDataList = series;
            ChartView.Invalidate();
        };

        // First Graph
        vm.InitializeIfEmpty();
    }
}
