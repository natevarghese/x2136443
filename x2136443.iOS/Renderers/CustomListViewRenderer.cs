using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using Foundation;
using x2136443.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ListView), typeof(CustomListViewRenderer))]
namespace x2136443.iOS.Renderers
{
    public class CustomListViewRenderer : ListViewRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {

            }

            if (e.NewElement != null)
            {
                Control.TableFooterView = new UIView(CGRect.Empty);
            }
        }
    }
}
