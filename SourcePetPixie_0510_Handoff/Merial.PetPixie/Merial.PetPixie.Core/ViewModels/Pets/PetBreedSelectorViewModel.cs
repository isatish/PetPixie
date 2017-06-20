using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Models.Kinvey;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Core;
using MvvmCross.Core.ViewModels;
using Xamarin.Forms;

namespace Merial.PetPixie.Core.ViewModels
{
        public class PetBreedSelectorViewModel : ViewModelBase
        {



                public Action<string> breedsAfterSelect;

                private readonly IBreedService _breedService;

                #region Private properties

                private ObservableCollection<string> _breeds;

                private ObservableCollection<KBreed> _kBreeds;

                #endregion


                #region Public Properties

                public List<KBreed> SelectedBreeds
                {
                        get
                        {
                                return ProfilePetViewModel.SelectedBreeds;
                        }

                }



                #endregion

                #region Commands

                public IMvxCommand OnBreedClickCommand => new SafeMvxCommand<KBreed>(OnBreedClickExecute);

                private void OnBreedClickExecute(KBreed kBreed)
                {
                        var breed = KBreeds.FirstOrDefault(b => b.Id == kBreed.Id);
                        if (breed != null)
                        {

                                breed.IsSelected = !kBreed.IsSelected;
                                RaisePropertyChanged(() => KBreeds);
                                if (breed.IsSelected)
                                {
                                        if (!ProfilePetViewModel.SelectedBreeds.Contains(breed))
                                        {
                                                ProfilePetViewModel.SelectedBreeds.Add(breed);

                                        }
                                }
                                else
                                {
                                        ProfilePetViewModel.SelectedBreeds.RemoveAll(b => b.Id == breed.Id);
                                }


                                MessagingCenter.Send(this, "BreedSelected");





                        }


                }

                #endregion

                #region Constructeurs


                public PetBreedSelectorViewModel(IBreedService breedService)
                {
                        _breedService = breedService;
                        LoadDataAsync();

                }

                #endregion


                #region Public Methods

                public ObservableCollection<string> Breeds
                {
                        get { return _breeds; }
                        set
                        {
                                _breeds = value;
                                RaisePropertyChanged(() => Breeds);
                        }
                }

                public ObservableCollection<KBreed> KBreeds
                {
                        get { return _kBreeds; }
                        set
                        {
                                _kBreeds = value;
                                RaisePropertyChanged(() => KBreeds);
                        }
                }

                #endregion

                protected override async Task<bool> LoadDataAsync()
                {
                        if (KBreeds == null)
                        {
                                var breeds = await _breedService.GetBreeds();
                                if (breeds == null || !breeds.Any())
                                {
                                        await PopupService.DisplayMessageAsync("Error", "No breed available, please retry later");
                                        Close(this);
                                        return true;
                                }
                                if (ProfilePetViewModel.SelectedBreeds != null && ProfilePetViewModel.SelectedBreeds.Any())
                                {
                                        foreach (var selectedBreed in ProfilePetViewModel.SelectedBreeds)
                                        {
                                                var breed = breeds.FirstOrDefault(b => b.Id == selectedBreed.Id);
                                                if (breed != null)
                                                {
                                                        breed.IsSelected = true;
                                                        selectedBreed.Name = breed.Name;

                                                }
                                                else
                                                {
                                                        breed.IsSelected = false;
                                                }

                                        }
                                }
                                else
                                {
                                        foreach (var kBreed in breeds)
                                        {
                                                kBreed.IsSelected = false;

                                        }

                                }
                                KBreeds = new ObservableCollection<KBreed>(breeds);
                        }

                        return await base.LoadDataAsync();
                }
        }
}