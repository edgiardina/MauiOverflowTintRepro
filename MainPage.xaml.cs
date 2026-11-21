namespace MauiOverflowTintRepro;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();

		// Shown on screen so every screenshot records the MAUI and iOS versions it came from.
		var mauiVersion = typeof(Shell).Assembly
			.GetCustomAttributes(typeof(System.Reflection.AssemblyInformationalVersionAttribute), false)
			.OfType<System.Reflection.AssemblyInformationalVersionAttribute>()
			.FirstOrDefault()?.InformationalVersion?.Split('+')[0];

#if APPLY_WORKAROUND
		const string workaround = " / workaround ON";
#else
		const string workaround = "";
#endif
		VersionLabel.Text = $"MAUI {mauiVersion} / {DeviceInfo.Platform} {DeviceInfo.VersionString}{workaround}";
	}
}
