using Merial.PetPixie.Core.Helpers;
using MvvmCross.Core.ViewModels;
using Xamarin.Forms;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Core;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Merial.PetPixie.Core.ViewModels;
using System.IO;
//using Merial.PetPixie.Core.Helpers;
//using MvvmCross.Core.ViewModels;

namespace Merial.PetPixie.Core.Views
{
    public partial class SharePage : ContentPage
    {

        public SharePage()
        {
            InitializeComponent();
            SetNavigationBarProperties();
            ImageTest.Source    = ImageSource.FromStream(() => new MemoryStream(App.FunPictureUpdatedBytes));
            imagePreview.Source = ImageSource.FromStream(() => new MemoryStream(App.FunPictureUpdatedBytes));

            //         using (var memoryStream = new MemoryStream())
            //{
            //    file.GetStream().CopyTo(memoryStream);
            //    file.Dispose();
            //    return memoryStream.ToArray();
            //}
        }
        public void SetNavigationBarProperties()
        {

                NavigationPage.SetHasNavigationBar(this, false);
                navBar.ShowHeaderText("share");
                navBar.ShowBackCommand();
              //  navBar.ShowAcceptCommand();


        }
    }
}
