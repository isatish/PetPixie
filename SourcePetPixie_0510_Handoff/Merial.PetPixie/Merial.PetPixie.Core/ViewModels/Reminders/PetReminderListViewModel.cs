using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Models.Enums;
using Merial.PetPixie.Core.Models.Kinvey;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Core;
using Merial.PetPixie.Core.ViewModels.Interfaces;
using Merial.PetPixie.Core.ViewModels.Reminder.Core;
using Merial.PetPixie.Core.ViewModels.Reminders.Core;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Xamarin.Forms;

namespace Merial.PetPixie.Core.ViewModels.Reminder
{
    public class PetReminderListViewModel : ViewModelBase<PetReminderListParameter>//, ISwipeListViewModel
    {
        #region Fields

        private readonly IReminderService _reminderService;
         private readonly IUserService _userService;

        private PetReminderModel _petReminderModel;
        private ObservableCollection<EntityBase> _reminderList;
        private List<ReminderTypeModel> _reminderTypes;
        private ObservableCollection<PetModel> _userPets;
                
        private PetReminderItemViewModel _selectedItem;
        private ObservableCollection<PetReminderItemViewModel> _items;
        private bool _isRefreshing;
        private bool _isLoading;

        #endregion

        #region Constructors

        public PetReminderListViewModel(IReminderService reminderService, IUserService userService)
        {
            _reminderService = reminderService;
            _userService = userService;

            LoadDataAsync();
        }

        #endregion

        #region Public Properties

        public PetReminderModel PetReminderModel
        {
            get { return this._petReminderModel; }
            set
            {
                if (this._petReminderModel == value) return;
                this.SetProperty(ref this._petReminderModel, value);
            }
        }


        public static PetReminderListViewModel SelectedItem
        {
            get; set;

        }



              
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                if (value == true)
                {
                    _isLoading = value;
                    RaisePropertyChanged(() => IsLoading);

                    try
                    {
                        InvokeOnMainThread(() =>
                        {
                            Mvx.Resolve<IUserDialogs>().ShowLoading("Loading your feed images");
                        });
                    }
                    catch (Exception exc)
                    {
                        string except = exc.Message;
                    }

                }
                else
                {
                    Mvx.Resolve<IUserDialogs>().HideLoading();
                     MessagingCenter.Send(this, "StopLoading");
                }



            }
        }



        public virtual bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                RaisePropertyChanged(() => IsRefreshing);
            }
        }




        public ObservableCollection<PetReminderItemViewModel> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged(() => Items);
            }
        }


        public List<KPetWithReminders> UserPets
        {
            get {

               

                return Settings.CurrentUserProfile.Pets.ToList(); }
        }

        public ObservableCollection<EntityBase> PetList
        {
            get { return this._reminderList; }
            set
            {
                
                if (_reminderList == value) return;
                this.SetProperty(ref this._reminderList, value);
                this.RaisePropertyChanged(() => this.ListIsEmpty);

                this._reminderList.CollectionChanged += (sender, args) => this.RaisePropertyChanged(() => this.ListIsEmpty);
            }
        }


        public ObservableCollection<EntityBase> EntityList
        {
            get { return this._reminderList; }
            set
            {
                
                if (_reminderList == value) return;
                this.SetProperty(ref this._reminderList, value);
                this.RaisePropertyChanged(() => this.ListIsEmpty);

                this._reminderList.CollectionChanged += (sender, args) => this.RaisePropertyChanged(() => this.ListIsEmpty);
            }
        }

        public bool ListIsEmpty => EntityList == null || !EntityList.Any();

        #endregion

        #region MvxCommands

        public ICommand BackCommand => new SafeMvxCommand(() => Close(this));

        public IMvxCommand AddReminderCommand => new SafeMvxCommandAsync(async () => await EnsureIsConnected(AddReminderCommandExecute));

        private async Task AddReminderCommandExecute()
        {
            EnsureIsBusy(AddReminderCoreAction, false);
        }

        private async Task AddReminderCoreAction()
        {
            ShowViewModel<PetReminderSelectTypeViewModel, PetReminderSelectTypeParameter>(
                new PetReminderSelectTypeParameter
                {
                    ReminderTypes = _reminderTypes,
                    PetReminderModel = PetReminderModel
                });
        }



        public IMvxCommand OpenReminderCommand => new SafeMvxCommand<ReminderModel>(OpenReminderCommandExecute);

        private void OpenReminderCommandExecute(ReminderModel reminderModel)
        {
            if (reminderModel.TypeModel.SubType == ReminderSubType.Product && !reminderModel.ProductModel.IsOtherType)
            {
                ShowValidationViewModel<PetReminderProductDetailViewModel, PetReminderBaseDetailParameter>(new PetReminderBaseDetailParameter
                {
                    Reminder = reminderModel,
                    PetReminderModel = this.PetReminderModel,
                    EntityMode = EntityMode.Edit

                });
            }
            else
            {
                ShowValidationViewModel<PetReminderOtherDetailViewModel, PetReminderBaseDetailParameter>(new PetReminderBaseDetailParameter
                {
                    Reminder = reminderModel,
                    PetReminderModel = PetReminderModel,
                    EntityMode = EntityMode.Edit
                });
            }
        }





        public ICommand ShowProfileCommand => new SafeMvxCommand(ShowProfileCommandExecute);
      
        private void ShowProfileCommandExecute()
        {
 
            ShowViewModel<PetReminderSelectTypeViewModel, PetReminderSelectTypeParameter>(
           new PetReminderSelectTypeParameter
           {
               ReminderTypes = _reminderTypes,
               PetReminderModel = PetReminderModel
           });
        }










        #endregion

        #region Swipe Methods

        public bool CanSwipeItemAtPosition(int index)
        {
            return true;
        }

        public void ItemSwiped(int index)
        {
            DeleteReminderAt(index);
        }

        #endregion

        #region Methods and Operators


        protected async Task<bool> LoadDataAsync()
        {

            //    Mvx.Resolve<IUserDialogs>().ShowLoading("Loading your feed");
            if (IsLoading == true)
                return false;

            var userPets = Settings.CurrentUserProfile.Pets;
                   
            IsLoading = true;
            if (userPets != null && userPets.Any())
            {

                Items = new ObservableCollection<PetReminderItemViewModel>(
                    userPets.Select(reminders => new PetReminderItemViewModel(_userService, reminders, _reminderService)).ToList());
                //paKPetWithReminders.CreateFrom(reminders))).ToList());
                            
            }


//            var list = await _reminderService.GetAllReminder();
//            IsLoading = true;
//            if (list != null && list.Any())
//            {
//
//              //ds  Items = new ObservableCollection<PetReminderItemViewModel>(
//              //ds      list.Select(reminders => new PetReminderItemViewModel(KPetWithReminders.CreateFrom(reminders))).ToList());

//            }

            IsLoading = false;
            return true;
        }



        protected override void OnLoadDataError(Exception exception)
        {
            base.OnLoadDataError(exception);
            PopupService.DisplayMessage("An error is occured, you will have some troubles with this functionnality","Error");
        }

        protected override void RealInit(PetReminderListParameter parameter)
        {
            this.PetReminderModel = parameter.PetReminderModel;
        }



        private async void DeleteReminderAt(int index)
        {
            if (!CanSwipeItemAtPosition(index)) return;
            IsFetchingData = true;

            await this._reminderService.Delete(this.PetReminderModel.PetId, ((ReminderModel)EntityList[index]) );
           
            EntityList.RemoveAt(index);
            IsFetchingData = false;
        }

        #endregion
    }
        
    public class PetReminderListParameter
    {
        public PetReminderModel PetReminderModel;
       
    }
}
