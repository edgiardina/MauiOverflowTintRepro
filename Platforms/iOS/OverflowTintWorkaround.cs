#if APPLY_WORKAROUND
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform.Compatibility;
using UIKit;

namespace MauiOverflowTintRepro;

// App-side workaround, compiled only with -p:ApplyWorkaround=true.
// MAUI 10.0.130+ hosts the "..." overflow button in a UIButton custom view. MAUI sets its tint to
// null, so it inherits the navigation bar's tint, while the plain bar button items next to it
// don't. This gives custom views the label color, which is what the plain items draw in.
public class WorkaroundShellRenderer : ShellRenderer
{
	protected override IShellPageRendererTracker CreatePageRendererTracker() => new WorkaroundPageRendererTracker(this);

	class WorkaroundPageRendererTracker(IShellContext context) : ShellPageRendererTracker(context)
	{
		protected override void UpdateToolbarItems()
		{
			base.UpdateToolbarItems();

			if (!OperatingSystem.IsIOSVersionAtLeast(26) || ViewController?.NavigationItem?.RightBarButtonItems is not { } items)
				return;

			foreach (var item in items)
			{
				if (item.CustomView is { } customView)
					customView.TintColor = UIColor.Label;
			}
		}
	}
}
#endif
