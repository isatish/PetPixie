using System.Threading.Tasks;
using Acr.UserDialogs;
using Merial.PetPixie.Core.Helpers;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;
using MvvmValidation;

namespace Merial.PetPixie.Core.ViewModels.Core
{
        public class ValidationFormViewModelBase : ViewModelBase
        {
                #region Fields

                private ObservableDictionary<string, string> _errors;
                private bool _isLoading;

                #endregion

                #region Public Properties

                public bool IsLoading
                {
                        get { return _isLoading; }
                        set
                        {
                                _isLoading = value;
                                RaisePropertyChanged(() => IsLoading);
                                if (value == false)
                                {
                                        Mvx.Resolve<IUserDialogs>().HideLoading();
                                }
                        }
                }

                public ObservableDictionary<string, string> Errors
                {
                        get { return _errors; }
                        set
                        {
                                _errors = value;
                                RaisePropertyChanged(() => Errors);
                        }
                }

                #endregion

                #region Public Methods

                public bool Validate()
                {
                        var validator = new ValidationHelper();

                        AddValidationFields(validator);

                        var result = validator.ValidateAll();

                        Errors = result.AsObservableDictionary();

                        return result.IsValid;
                }

                public ObservableDictionary<string, string> ValidationErrorMessages()
                {
                        var validator = new ValidationHelper();

                        AddValidationFields(validator);

                        var result = validator.ValidateAll();

                        Errors = result.AsObservableDictionary();

                        return Errors;
                }

                public async Task ShowValidationErrors()
                {
                        var validationErrors = ValidationErrorMessages();
                        var vals = Errors;
                        string valError = "";
                        foreach (var error in Errors)
                        {
                                if (error.Value.ToLower() == "required")
                                {
                                        valError += error.Key + " is required";
                                }
                                else
                                {
                                        valError += error.Value;
                                }
                                valError += System.Environment.NewLine;
                        }
                        await PopupService.DisplayMessageAsync("Please correct the following", System.Environment.NewLine + valError);

                }

                public virtual void AddValidationFields(ValidationHelper helper)
                {

                }

                #endregion
        }

        public abstract class ValidationFormViewModelBase<TInit> : ValidationFormViewModelBase
        {
                public void Init(string parameter)
                {
                        var deserialized = Mvx.Resolve<IMvxJsonConverter>().DeserializeObject<TInit>(parameter);
                        RealInit(deserialized);
                }

                protected abstract void RealInit(TInit parameter);
        }
}