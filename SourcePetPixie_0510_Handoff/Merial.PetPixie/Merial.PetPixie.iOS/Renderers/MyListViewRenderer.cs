using Merial.PetPixie;
using Merial.PetPixie.iOS;
using System;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Merial.PetPixie.Core.Views;


[assembly: ExportRenderer(typeof(MyListView), typeof(MyListViewRenderer))]
namespace Merial.PetPixie.iOS
    {
        public class MyListViewRenderer : ListViewRenderer
        {



            protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
            {
                base.OnElementPropertyChanged(sender, e);
                this.Control.Bounces = false;


          //  this.UIControlContentVerticalAlignment = new UIControlContentVerticalAlignment.Top();
                if (e.PropertyName == "ItemsSource")
                {
                    var control = (UITableView)Control;

                   

                    foreach (var cell in control.VisibleCells)
                    {
                    cell.BackgroundColor = UIColor.Clear;// UIColor.FromRGB(255, 0, 0);
                    cell.SelectionStyle = UITableViewCellSelectionStyle.None;

                    }
                }


            if (e.PropertyName == "SelectedItem")
            {
                var control = (UITableView)Control;

                foreach (var cell in control.VisibleCells)
                {
                    cell.BackgroundColor = UIColor.Clear;// UIColor.FromRGB(255, 0, 0);
                    cell.SelectionStyle = UITableViewCellSelectionStyle.None;
                }
            }

            }
        }


}

