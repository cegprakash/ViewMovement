// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace ViewMovement
{
	[Register ("ViewMovementViewController")]
	partial class ViewMovementViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView AppView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField TextBox1 { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField TextBox2 { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (AppView != null) {
				AppView.Dispose ();
				AppView = null;
			}
			if (TextBox1 != null) {
				TextBox1.Dispose ();
				TextBox1 = null;
			}
			if (TextBox2 != null) {
				TextBox2.Dispose ();
				TextBox2 = null;
			}
		}
	}
}
