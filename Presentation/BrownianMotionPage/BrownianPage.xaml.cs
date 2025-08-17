
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

    private void OnlyNumberAComma(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(e.NewTextValue))
            return;

        string validText = new(e.NewTextValue
            .Where(c => char.IsDigit(c) || c == ',')
            .ToArray());

        int firstComma = validText.IndexOf(',');
        if (firstComma >= 0)
        {
            validText = validText.Substring(0, firstComma + 1) +
                        validText.Substring(firstComma + 1).Replace(",", "");
        }

        if (validText != e.NewTextValue)
            ((Entry)sender).Text = validText;
    }

    private void OnlyNumbers(object sender, TextChangedEventArgs e)
    {
        Entry entry = (Entry)sender;

        string onlyCharNumbers = new(entry.Text?.Where(char.IsDigit).ToArray());

        if (entry.Text != onlyCharNumbers)
            entry.Text = onlyCharNumbers;
    }
}
