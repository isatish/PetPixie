// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.42000
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace Merial.PetPixie.Core.Views {
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    
    
    public partial class ProfileDetailPage : global::Xamarin.Forms.ContentPage {
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Merial.PetPixie.Core.Views.TopNavBar navBar;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Xamarin.Forms.StackLayout MainContentsStack;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Merial.PetPixie.Core.Controls.AwesomeWrappanel WrapItems;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Merial.PetPixie.Core.Controls.AwesomeWrappanel WrapPicsGrid;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Merial.PetPixie.Core.Controls.AwesomeWrappanel WrapPicsList;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Xamarin.Forms.ListView listItems;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Xamarin.Forms.ListView listPetPictureGrid;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Xamarin.Forms.ListView listPetPictureList;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private void InitializeComponent() {
            this.LoadFromXaml(typeof(ProfileDetailPage));
            navBar = this.FindByName <global::Merial.PetPixie.Core.Views.TopNavBar>("navBar");
            MainContentsStack = this.FindByName <global::Xamarin.Forms.StackLayout>("MainContentsStack");
            WrapItems = this.FindByName <global::Merial.PetPixie.Core.Controls.AwesomeWrappanel>("WrapItems");
            WrapPicsGrid = this.FindByName <global::Merial.PetPixie.Core.Controls.AwesomeWrappanel>("WrapPicsGrid");
            WrapPicsList = this.FindByName <global::Merial.PetPixie.Core.Controls.AwesomeWrappanel>("WrapPicsList");
            listItems = this.FindByName <global::Xamarin.Forms.ListView>("listItems");
            listPetPictureGrid = this.FindByName <global::Xamarin.Forms.ListView>("listPetPictureGrid");
            listPetPictureList = this.FindByName <global::Xamarin.Forms.ListView>("listPetPictureList");
        }
    }
}
