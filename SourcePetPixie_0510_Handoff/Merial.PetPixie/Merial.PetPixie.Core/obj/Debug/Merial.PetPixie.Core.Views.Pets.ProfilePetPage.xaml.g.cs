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
    
    
    public partial class ProfilePetPage : global::Xamarin.Forms.ContentPage {
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Merial.PetPixie.Core.Views.TopNavBar navBar;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Xamarin.Forms.StackLayout MainContentsStack;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Xamarin.Forms.RowDefinition TopNav;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Xamarin.Forms.RowDefinition PetImage;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Xamarin.Forms.RowDefinition Name;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Xamarin.Forms.RowDefinition Breed;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Xamarin.Forms.RowDefinition Birthday;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Xamarin.Forms.DatePicker ExpDate;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Xamarin.Forms.StackLayout stackLoader;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private void InitializeComponent() {
            this.LoadFromXaml(typeof(ProfilePetPage));
            navBar = this.FindByName <global::Merial.PetPixie.Core.Views.TopNavBar>("navBar");
            MainContentsStack = this.FindByName <global::Xamarin.Forms.StackLayout>("MainContentsStack");
            TopNav = this.FindByName <global::Xamarin.Forms.RowDefinition>("TopNav");
            PetImage = this.FindByName <global::Xamarin.Forms.RowDefinition>("PetImage");
            Name = this.FindByName <global::Xamarin.Forms.RowDefinition>("Name");
            Breed = this.FindByName <global::Xamarin.Forms.RowDefinition>("Breed");
            Birthday = this.FindByName <global::Xamarin.Forms.RowDefinition>("Birthday");
            ExpDate = this.FindByName <global::Xamarin.Forms.DatePicker>("ExpDate");
            stackLoader = this.FindByName <global::Xamarin.Forms.StackLayout>("stackLoader");
        }
    }
}
