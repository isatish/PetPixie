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
using Merial.PetPixie.Core.Models.Enums;

namespace Merial.PetPixie.Core.Views.Reminder
{
    public partial class PetReminderOtherDetailPage : MasterDetailPage
    {

        public PetReminderOtherDetailPage()
        {
            InitializeComponent();
            SetNavigationProperties();
        }

        public void SetNavigationProperties()
        {
            SetNavigationBarProperties();
       //     FABHelper.SetFABProperties(navBar, StackDeails, ContentContainer);

            Master = new LeftNavBar() { Title = "Feed" };
            IsPresented = false;

            MessagingCenter.Subscribe<ViewModelBase>(this, "ShowNavigation", (sender) =>
                {
                    IsPresented = true;
                });

        }

        public void SetNavigationBarProperties()
        {

            NavigationPage.SetHasNavigationBar(this, false);
            navBar.ShowHeaderText("reminders");
            navBar.ShowBackCommand();
         

                        //if (BaseViewModel)this.is

                        //if (PetReminderModel)

                        //                                Reminder = reminderModel,
                        //PetReminderModel = reminderModel.PetReminderModel,



              //          if (EntityMode == EntityMode.Edit)
                        {
            //                       navBar.ShowAddReminderCommand();
                        }
             //           else
                        {
                                   navBar.ShowEditReminderCommand();

                        }
                        navBar.ShowDeleteReminderCommand();
                   
        }





        void EntrySearch_Completed(object sender, System.EventArgs e)
        {

        }
    }
}
