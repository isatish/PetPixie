// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.42000
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace Merial.PetPixie.Core.Views.Reminder {
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    
    
    public partial class PetReminderProductDetailPage : global::Xamarin.Forms.MasterDetailPage {
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Xamarin.Forms.ContentPage ContentContainer;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Merial.PetPixie.Core.Views.TopNavBar navBar;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Xamarin.Forms.StackLayout StackDeails;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private global::Xamarin.Forms.DatePicker ReminderStartDate;
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "0.0.0.0")]
        private void InitializeComponent() {
            this.LoadFromXaml(typeof(PetReminderProductDetailPage));
            ContentContainer = this.FindByName <global::Xamarin.Forms.ContentPage>("ContentContainer");
            navBar = this.FindByName <global::Merial.PetPixie.Core.Views.TopNavBar>("navBar");
            StackDeails = this.FindByName <global::Xamarin.Forms.StackLayout>("StackDeails");
            ReminderStartDate = this.FindByName <global::Xamarin.Forms.DatePicker>("ReminderStartDate");
        }
    }
}
