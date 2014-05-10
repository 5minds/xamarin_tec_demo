using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;

namespace TechDemo2
{
	public partial class MainViewController : UIViewController
	{
		#region private Fields

		private UIImageView backgroundImageView;
		private UIImageView foregroundImageView;
		private UIScrollView scrollView;

		UIPanGestureRecognizer panGesture;
		//UIPinchGestureRecognizer pinchGesture;


		//private UIScrollView scrollView;
		private UIImageView imageView;
        private float dx = 0;
        private float dy = 0;

		#endregion

		#region Constructor

		public MainViewController () : base ("MainViewController", null)
		{
		}

		#endregion

		#region Public Functions

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Background image
			this.backgroundImageView = new UIImageView (UIImage.FromBundle("Background.jpg"));
			this.backgroundImageView.Frame = new RectangleF (0, 0, View.Frame.Width, View.Frame.Height);
			View.AddSubview (this.backgroundImageView);

			// Foreground image
			this.foregroundImageView = new UIImageView (UIImage.FromBundle("5Minds.png"));
			var factor = this.foregroundImageView.Image.Size.Height / this.foregroundImageView.Image.Size.Width;
			var imageWidth = View.Frame.Width;
			var imageHeight = View.Frame.Width * factor;
			this.foregroundImageView.Frame = new RectangleF (0, 0, imageWidth, imageHeight);
			this.scrollView = new UIScrollView (new RectangleF (0, 0, View.Frame.Width, View.Frame.Height));
			//this.scrollView.ContentSize = new SizeF (View.Frame.Width, this.foregroundImageView.Frame.Height * factor);
			this.scrollView.MaximumZoomScale = 3f;
			this.scrollView.MinimumZoomScale = 1.5f;
			this.scrollView.UserInteractionEnabled = true;
			this.scrollView.AddSubview (this.foregroundImageView);
			this.scrollView.ViewForZoomingInScrollView += (UIScrollView sv) => { return this.foregroundImageView; };

			//this.foregroundImageView.UserInteractionEnabled = true;
			View.AddSubview (this.scrollView);

			// Zoom in
			this.scrollView.SetZoomScale (2f, true);
			this.scrollView.SetContentOffset (new PointF (imageWidth / 2f, imageHeight / 1.35f), true);

			this.panGesture = new UIPanGestureRecognizer ((pg) => 
			{
				if ((pg.State == UIGestureRecognizerState.Began || pg.State == UIGestureRecognizerState.Changed) && (pg.NumberOfTouches == 1)) 
				{
					var p0 = pg.LocationInView (View);

					if (dx == 0)
						dx = p0.X - this.scrollView.Center.X;

					if (dy == 0)
						dy = p0.Y - this.scrollView.Center.Y;

					var p1 = new PointF (p0.X - dx, p0.Y - dy);

					this.scrollView.Center = p1;
				}
				else if (pg.State == UIGestureRecognizerState.Ended) 
				{
					dx = 0;
					dy = 0;
				}
			});
					
			this.foregroundImageView.AddGestureRecognizer (this.panGesture);
		}

		#endregion
	}
}

