using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace ViewMovement {

	class ViewHelper{
		private float movedDistance;
		public ViewHelper(){
			movedDistance = 0;
		}

		public static UITapGestureRecognizer ExitOnTapGesture(UIView view){
			return new UITapGestureRecognizer (() => view.EndEditing (true));
		}



		public static bool resignTextBoxResignKeyboard(UITextField textField){
			textField.ResignFirstResponder();
			return true; 
		}

		public void onKeyBoardNotification(UIView AppView, RectangleF textBoxFrame, NSNotification keyBoardNotification, bool isViewLoaded, UIInterfaceOrientation orientation){
			var visible = keyBoardNotification.Name == UIKeyboard.WillShowNotification;

			UIView.BeginAnimations ("AnimateForKeyboard");
			UIView.SetAnimationBeginsFromCurrentState (true);
			UIView.SetAnimationDuration (UIKeyboard.AnimationDurationFromNotification (keyBoardNotification));
			UIView.SetAnimationCurve ((UIViewAnimationCurve)UIKeyboard.AnimationCurveFromNotification (keyBoardNotification));


			var keyboardFrame = visible
				? UIKeyboard.FrameEndFromNotification(keyBoardNotification)
				: UIKeyboard.FrameBeginFromNotification(keyBoardNotification);
				
			moveView(AppView, textBoxFrame, keyBoardNotification, keyboardFrame.Height, orientation);

			UIView.CommitAnimations ();



		}


		private void moveView (UIView AppView, RectangleF textBoxFrame, NSNotification keyBoardNotification, float keyboardHeight, UIInterfaceOrientation orientation)
		{
			RectangleF currentFrame = AppView.Frame;

			if (keyBoardNotification.Name == UIKeyboard.WillShowNotification) {

				currentFrame.Y += movedDistance;	//undo last movement


				float appBottom = AppView.Frame.Y + AppView.Frame.Height;
				float distanceFromBottom = appBottom - (textBoxFrame.Y+textBoxFrame.Height);

				if (distanceFromBottom > keyboardHeight)
					return;

				float moveDistance = keyboardHeight - distanceFromBottom + Math.Min(20,distanceFromBottom);

				currentFrame.Y -= moveDistance;
				movedDistance = moveDistance;
			}

			else if (keyBoardNotification.Name == UIKeyboard.WillHideNotification) {
				currentFrame.Y += movedDistance;
				movedDistance = 0;
			}

			AppView.Frame = currentFrame;
		}
	}

	public partial class ViewMovementViewController : UIViewController {


		ViewHelper viewHelper;


		public ViewMovementViewController (IntPtr handle) : base (handle) {
			viewHelper = new ViewHelper ();
		}
		
		public override void DidReceiveMemoryWarning () {
			base.DidReceiveMemoryWarning ();
		}
			
			
		protected virtual void RegisterForKeyboardNotifications()
		{
			NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, OnKeyboardNotification);
			NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillShowNotification, OnKeyboardNotification);
		}

		public override void ViewDidLoad () {
			base.ViewDidLoad ();
			RegisterForKeyboardNotifications ();
			AppView.AddGestureRecognizer(ViewHelper.ExitOnTapGesture(AppView));
			TextBox1.ShouldReturn += ViewHelper.resignTextBoxResignKeyboard;
			TextBox2.ShouldReturn += ViewHelper.resignTextBoxResignKeyboard;
		}
			

		RectangleF getFirstResponderFrame(){		//returns the rectangle of active textBox
			if (TextBox1.IsFirstResponder)
				return TextBox1.Frame;
			else if (TextBox2.IsFirstResponder)
				return TextBox2.Frame;
			else
				return new RectangleF ();
		}
	

		private void OnKeyboardNotification (NSNotification notification)
		{
			viewHelper.onKeyBoardNotification (AppView, getFirstResponderFrame (), notification, IsViewLoaded, InterfaceOrientation);
		}

		public override void ViewWillAppear (bool animated) {
			base.ViewWillAppear (animated);
		}
		
		public override void ViewDidAppear (bool animated) {
			base.ViewDidAppear (animated);
		}
		
		public override void ViewWillDisappear (bool animated) {
			base.ViewWillDisappear (animated);
		}
		
		public override void ViewDidDisappear (bool animated) {
			base.ViewDidDisappear (animated);
		}

	}
}