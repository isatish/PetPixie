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
    
    
    public partial class VetMapPage : global::Xamarin.Forms.ContentPage {
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Merial.PetPixie.Core.Views.TopNavBar navBar;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Xamarin.Forms.ContentView ContentMap;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Merial.PetPixie.Core.Views.CustomMap MapVets;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Xamarin.Forms.Entry EntrySearch;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Xamarin.Forms.ScrollView scrollVets;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Xamarin.Forms.StackLayout stackVets;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Xamarin.Forms.Label LabelHeader;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Xamarin.Forms.ListView listItems;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private void InitializeComponent() {
            this.LoadFromXaml(typeof(VetMapPage));
            navBar = this.FindByName <global::Merial.PetPixie.Core.Views.TopNavBar>("navBar");
            ContentMap = this.FindByName <global::Xamarin.Forms.ContentView>("ContentMap");
            MapVets = this.FindByName <global::Merial.PetPixie.Core.Views.CustomMap>("MapVets");
            EntrySearch = this.FindByName <global::Xamarin.Forms.Entry>("EntrySearch");
            scrollVets = this.FindByName <global::Xamarin.Forms.ScrollView>("scrollVets");
            stackVets = this.FindByName <global::Xamarin.Forms.StackLayout>("stackVets");
            LabelHeader = this.FindByName <global::Xamarin.Forms.Label>("LabelHeader");
            listItems = this.FindByName <global::Xamarin.Forms.ListView>("listItems");
        }
    }
}