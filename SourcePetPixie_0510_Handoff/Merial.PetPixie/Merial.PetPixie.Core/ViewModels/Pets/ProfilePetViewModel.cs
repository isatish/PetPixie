using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using KinveyXamarin;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Models.Enums;
using Merial.PetPixie.Core.Models.Kinvey;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Core;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.PictureChooser;

using MvvmValidation;
using Plugin.Media;
using RestSharp.Extensions;
using Splat;
using Xamarin.Forms;

namespace Merial.PetPixie.Core.ViewModels
{
        public class ProfilePetViewModel : ValidationFormViewModelBase<ProfilePetParameter>
        {
                #region Private Properties

                private readonly IMvxPictureChooserTask _pictureChooserTask;
                private PetModel _profile;
                private readonly IPetService _petService;
                private readonly IBreedService _breedService;
                private readonly IUploadService _uploadService;
                private readonly IBitmapService _bitmapService;
                private static string _breedsString;
                private static List<KBreed> _selectedBreeds = new List<KBreed>();
                private static EntityMode _entityMode = EntityMode.Edit;
                private static List<KBreed> _breeds = new List<KBreed>();

                #endregion

                #region  Constructeurs

                public ProfilePetViewModel(IMvxPictureChooserTask pictureChooserTask, IBreedService breedService, IPetService petService, IUploadService uploadService, IBitmapService bitmapService)
                {
                        _pictureChooserTask = pictureChooserTask;
                        _breedService = breedService;
                        _petService = petService;
                        _uploadService = uploadService;
                        _bitmapService = bitmapService;
                        Profile = new PetModel();

                }

                #endregion

                #region Commands

                public IMvxCommand ValidateCommand => new SafeMvxCommandAsync(async () =>
                {

                        if (!Validate())
                        {
                                ShowValidationErrors();
                                return;
                        }
                        Profile.BreedIds = SelectedBreeds.Select(b => b.Id).ToArray();
                        IsFetchingData = true;

                        try
                        {
                                MessagingCenter.Send(this, "SavingPet");
                                await _petService.Save(Profile);
                        }
                        catch (Exception e)
                        {
                                IsFetchingData = false;
                                Logger.Error(e);
                                PopupService.DisplayMessage("An unexpected error occured. Please try again", "Error");
                                return;
                        }

                        IsFetchingData = false;
                        Close(this);
                });



                public IMvxCommand TakePictureFromGalleryCommand => new SafeMvxCommand(() =>
                {
                        SelectPhoto(PictureAvailableAfterCrop, AssumeCancelled);


                        //	_pictureChooserTask.ChoosePictureFromLibrary(800, 100, PictureAvailable, AssumeCancelled);
                });

                public IMvxCommand TakePictureFromCameraCommand => new SafeMvxCommand(() =>
                {
                        //	_pictureChooserTask.TakePicture(800, 100, PictureAvailable, AssumeCancelled);

                        SelectPhoto(PictureAvailableAfterCrop, AssumeCancelled);
                        var a = "abc";
                });

                public IMvxCommand SelectBreedCommand => new SafeMvxCommand(() =>
                {
                        ShowViewModel<PetBreedSelectorViewModel>();

                });
                #endregion

                #region Private Methods
                private void AssumeCancelled()
                {


                }




                public async void TakePhoto(Action<FileMetaData> photoAfterSelect, Action assumeCancelled)
                {
                        await CrossMedia.Current.Initialize();


                        if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                        {

                                PopupService.DisplayMessage("No Camera", $"Camera is unavailable");
                                return;
                        }
                        else
                        {

                                var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                                {
                                        //PhotoSize= Plugin.Media.Abstractions.PhotoSize.Medium,
                                        //Directory = "Sample",
                                        //Name = "test.jpg"
                                });

                                if (file == null)
                                        return;

                                var fileMetaData = new FileMetaData() { downloadURL = file.Path };

                                photoAfterSelect.BeginInvoke(fileMetaData, null, null);

                                Settings.CurrentFunImageSource = file.Path;

                                var input = file.GetStream();


                                Mvx.Resolve<IUserDialogs>().ShowLoading("Updating Pet Profile");

                                using (MemoryStream ms = new MemoryStream())
                                {
                                        input.CopyTo(ms);
                                        // App.FunPictureBytes = ms.ToArray();
                                        var uploadMS = await _uploadService.UploadImage(ms.ToArray());

                                        Profile.FileMetaData = uploadMS;
                                        await _petService.Save(Profile);
                                }
                                Mvx.Resolve<IUserDialogs>().HideLoading();






                                App.FunPictureBytes = StreamHelper.ReadFully(input);


                                // var metaData =  UploadPicture(file.Path).Result;

                                //   var uploadedData = await _uploadService.UploadImage(App.FunPictureBytes);

                                //var Content = new Image
                                //{
                                //    Source = ImageSource.FromResource(Settings.CurrentFunImageSource),
                                //    VerticalOptions = LayoutOptions.Center,
                                //    HorizontalOptions = LayoutOptions.Center
                                //};


                                //     ShowViewModel<CropperViewModel>();
                        }
                }

                public async void SelectPhoto(Action<FileMetaData> photoAfterSelect, Action assumeCancelled)
                {
                        await CrossMedia.Current.Initialize();


                        var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                        {
                                //PhotoSize= Plugin.Media.Abstractions.PhotoSize.Medium,
                                //Directory = "Sample",
                                //Name = "test.jpg"
                        });

                        if (file == null)
                                return;

                        var fileMetaData = new FileMetaData() { downloadURL = file.Path };

                        photoAfterSelect.BeginInvoke(fileMetaData, null, null);

                        Settings.CurrentFunImageSource = file.Path;

                        var input = file.GetStream();
                        App.FunPictureBytes = StreamHelper.ReadFully(input);


                        //   Mvx.Resolve<IUserDialogs>().ShowLoading("Updating Pet Profile");

                        using (MemoryStream ms = new MemoryStream())
                        {
                                input.CopyTo(ms);
                                // App.FunPictureBytes = ms.ToArray();
                                var uploadMS = await _uploadService.UploadImage(ms.ToArray());

                                Profile.FileMetaData = uploadMS;
                                //  await _petService.Save(Profile);
                                     ValidateCommand.Execute();
                        }


              //          ValidateCommand.Execute();
                        //  Mvx.Resolve<IUserDialogs>().HideLoading();









                        // var metaData =  UploadPicture(file.Path).Result;

                        //   var uploadedData = await _uploadService.UploadImage(App.FunPictureBytes);

                        //var Content = new Image
                        //{
                        //    Source = ImageSource.FromResource(Settings.CurrentFunImageSource),
                        //    VerticalOptions = LayoutOptions.Center,
                        //    HorizontalOptions = LayoutOptions.Center
                        //};


                        //     ShowViewModel<CropperViewModel>();

                }


                public async Task<FileMetaData> UploadPicture(string filePath)
                {
                        if (filePath == null)
                                return null;

                        try
                        {
                                using (var bitmap = _bitmapService.GetBitmap(filePath)) //(FileMetaData.downloadURL))
                                {
                                        if (bitmap == null)
                                                return null;

                                        using (var stream = new MemoryStream())
                                        {
                                                await bitmap.Save(CompressedBitmapFormat.Png, 0, stream);
                                                return await _uploadService.UploadImage(stream.ToArray());
                                        }
                                }
                        }
                        catch (Exception e)
                        {
                                Logger.Error(e);
                                return null;
                        }
                }



                private void PictureAvailable(Stream stream)
                {
                        // update image on Kinvey
                        Mvx.Resolve<IImageService>().CropPicture(stream, PictureAvailableAfterCrop, AssumeCancelled);

                }

                private void PictureAvailableAfterCrop(FileMetaData fileMetaData)
                {
                        IsFetchingData = true;

                        Profile.FileMetaData = fileMetaData;
                        PetImageURL = fileMetaData.downloadURL;
                        IsFetchingData = false;


                }
                #endregion

                #region public Properties

                public static DateTime PetBirthDay()
                {
                        return new DateTime(2010, 3, 4);
                }

                public static void RefreshSelectedBreed()
                {

                        var breeds = SelectedBreeds.Any() ? String.Join(", ", SelectedBreeds.Select(b => b.Name)) : "";

                }


                private string _petImageURL = "petImagePlaceholder.png";

                public string PetImageURL
                {
                        get
                        {


                                return _petImageURL;
                        }
                        set
                        {


                                _petImageURL = value;
                                RaisePropertyChanged(() => PetImageURL);
                        }
                }





                public EntityMode DataEntityMode
                {
                        get
                        {


                                return _entityMode;
                        }
                        set
                        {


                                _entityMode = value;
                                MessagingCenter.Send<ProfilePetViewModel, EntityMode>(this, "EntityMode", value);
                                RaisePropertyChanged(() => DataEntityMode);
                        }
                }



                public PetModel Profile
                {
                        get { return _profile; }
                        set
                        {


                                _profile = value;
                                RaisePropertyChanged(() => Profile);

                                BreedsString = "";
                        }
                }



                public static List<KBreed> SelectedBreeds
                {
                        get { return _selectedBreeds; }
                        set
                        {
                                _selectedBreeds = value;

                        }
                }



                public static List<KBreed> KBreeds
                {
                        get { return _breeds; }
                        set
                        {
                                _breeds = value;

                        }
                }



                //  private static string _breedsString = "";

                public string BreedsString
                {
                        get
                        {

                                return _breedsString;
                        }
                        set
                        {
                                var a = value;
                                _breedsString = SelectedBreeds.Any() ? String.Join(", ", SelectedBreeds.Select(b => b.Name)) : "";
                                RaisePropertyChanged(() => BreedsString);
                        }
                }










                #endregion


                #region Proctected Methods
                protected override async Task<bool> LoadDataAsync()
                {

                        return await base.LoadDataAsync();
                }



                public override void Started()
                {
                        base.Started();
                        RaisePropertyChanged(() => BreedsString);
                }

                #endregion





                public async Task<bool> LoadBreeds()
                {
                        List<KBreed> breeds = null;

                        if (KBreeds == null || KBreeds.Count == 0)
                        {
                                breeds = await _breedService.GetBreeds();
                                if (breeds == null || !breeds.Any())
                                {
                                        await PopupService.DisplayMessageAsync("Error", "No breed available, please retry later");
                                        Close(this);
                                        return true;
                                }

                                if (SelectedBreeds != null && SelectedBreeds.Any())
                                {
                                        foreach (var selectedBreed in SelectedBreeds)
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
                                KBreeds = new List<KBreed>(breeds);
                        }

                        return await base.LoadDataAsync();
                }

                protected override void RealInit(ProfilePetParameter parameter)
                {
                        SelectedBreeds = new List<KBreed>();
                        InitBreeds();
                    

                        switch (parameter.EntityMode)
                        {
                                case EntityMode.New:
                                        Profile = new PetModel();
                                        Profile.Type = parameter.TypePet.ToString();
                                        BreedsString = "<Select your dogs breed>";
                                        DataEntityMode = EntityMode.New;

                                        break;
                                case EntityMode.Edit:
                                        Profile = parameter.PetModel;
                                        DataEntityMode = EntityMode.Edit;
                                        InitSelectedBreed();
                                        break;

                                default:
                                        throw new ArgumentOutOfRangeException();
                        }
                }



                private async Task<bool> InitBreeds()
                {

                        await LoadBreeds();
                        return true;
                }

                private void InitSelectedBreed()
                {
                        MessagingCenter.Unsubscribe<PetBreedSelectorViewModel>(this, "BreedSelected");
                        MessagingCenter.Subscribe<PetBreedSelectorViewModel>(this, "BreedSelected", (sender) =>
                        {
                                var selectedBreed = new KBreed();

                                BreedsString = "abcde";
                        });


                        if (Profile.BreedIds != null && Profile.BreedIds.Any())
                        {
                                //This causes it to CRASH!
                                //  SelectedBreeds.AddRange(_breedService.GetListBreedName(Profile.BreedIds));

                                var breeds = _breedService.GetListBreedName(Profile.BreedIds);
                                foreach (var breed in breeds)
                                {
                                        SelectedBreeds.Add(new KBreed()
                                        {
                                                Id = breed.Id,
                                                Name = breed.Name
                                        });
                                }


                                ////   BreedsString = SelectedBreeds.Any() ? String.Join(", ", SelectedBreeds.Select(b => b.Name)) : "";
                                //   foreach (var breedId in Profile.BreedIds)
                                //{
                                //    SelectedBreeds.Add(new KBreed()
                                //    {
                                //        Id = breedId, Name=
                                //    });
                                //}
                                BreedsString = "string to trigger a property change";
                        }
                }

                public override void AddValidationFields(ValidationHelper helper)
                {
                        helper.AddRequiredRule((() => Profile.Name), "Required");
                        helper.AddRule(() => Profile.Name, () => RuleResult.Assert(Profile.Name.Length >= 4, "Minimum 4 characters"));

                }
        }

        public class ProfilePetParameter
        {
                public PetModel PetModel { get; set; }

                public EntityMode EntityMode { get; set; }

                public TypePet TypePet { get; set; }


        }
}