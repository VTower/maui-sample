using maui_sample.ViewModels;

namespace maui_sample;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		// Rote App Register 
		Routing.RegisterRoute(nameof(MainViewModel), typeof(MainPage));
		Routing.RegisterRoute(nameof(BrownianViewModel), typeof(BrownianPage));
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new AppShell());
	}
}