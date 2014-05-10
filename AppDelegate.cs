using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace TechDemo2
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		private MainViewController viewController;

		// class-level declarations
		public override UIWindow Window {
			get;
			set;
		}

		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			// create a new window instance based on the screen size
			this.Window = new UIWindow (UIScreen.MainScreen.Bounds);

			viewController = new MainViewController ();
			this.Window.RootViewController = viewController;
			this.Window.MakeKeyAndVisible ();

			return true;
		}
	}
}

