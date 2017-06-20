// Helpers/Settings.cs
using System.Collections.Generic;
using KinveyXamarin;
using Merial.PetPixie.Core.Models.Kinvey;
using Newtonsoft.Json;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Merial.PetPixie.Core.Helpers
{
	/// <summary>
	///     This is the Settings static class that can be used in your Core solution or in any
	///     of your client applications. All settings are laid out the same exact way with getters
	///     and setters.
	/// </summary>
	public static class Settings
    {
        #region Setting Constants

        private const string SettingsKey = "settings_key";
        private const string Email_Key = "Email_Key";
        private const string ShowLtoR_Key = "ShowLtoR_Key";
        private const string ShowGetStarted_Key = "ShowGetStarted_Key";
        private const string Password_Key = "Password_Key";
        private const string User_Key = "User_Key";
        private const string UserProfile_Key = "UserProfile_Key";
        private const string MapSettings_Key = "MapSettings_Key";
        private const string Breed_Key = "Breed_Key";
        private const string CurrentFunImageSource_Key = "CurrentFunImageSource_Key";

        private static readonly string SettingsDefault = string.Empty;
        private static readonly bool ShowLtoRDefault = false;
        #endregion

        private static ISettings m_Settings;

		private static ISettings AppSettings {
			get {
				return m_Settings ?? (m_Settings = CrossSettings.Current);
			}
		}


		public static string GeneralSettings {
			get { return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault); }
			set { AppSettings.AddOrUpdateValue(SettingsKey, value ?? SettingsDefault); }
		}




public static bool ShowGetStarted
{
	get { return AppSettings.GetValueOrDefault(ShowGetStarted_Key, true); }
	set
	{
		AppSettings.AddOrUpdateValue(ShowGetStarted_Key, value);
	}
}


        public static bool ShowLtoR
        {
            get { return AppSettings.GetValueOrDefault(ShowLtoR_Key, ShowLtoRDefault); }
            set
            {
                AppSettings.AddOrUpdateValue(ShowLtoR_Key, value);
            }
        }


        public static string CurrentFunImageSource
        {
            get { return AppSettings.GetValueOrDefault(CurrentFunImageSource_Key, SettingsDefault); }
            set
            {
                AppSettings.AddOrUpdateValue(CurrentFunImageSource_Key, value);
            }
        }



                
        public static byte[] CurrentFunImage
        {
            get { return AppSettings.GetValueOrDefault<byte[]>(CurrentFunImageSource_Key, null); }
            set
            {
                AppSettings.AddOrUpdateValue(CurrentFunImageSource_Key, value);
            }



            //get { return _image; }
            //set
            //{
            //    _image = value;
            //    RaisePropertyChanged(() => Image);
            //}
        }




		public static string Email {
			get { return AppSettings.GetValueOrDefault(Email_Key, SettingsDefault); }
			set {
				AppSettings.AddOrUpdateValue(Email_Key, value ?? SettingsDefault);
			}
		}

		public static string Password {
			get { return AppSettings.GetValueOrDefault(Password_Key, SettingsDefault); }
			set { AppSettings.AddOrUpdateValue(Password_Key, value ?? SettingsDefault); }
		}

		public static User CurrentUser {
			get {
				var json = AppSettings.GetValueOrDefault(User_Key, SettingsDefault);
				return JsonConvert.DeserializeObject<User>(json);
			}
			set {
				if (value != null) {
					var json = JsonConvert.SerializeObject(value);
					AppSettings.AddOrUpdateValue(User_Key, json);
				}
				else {
					AppSettings.AddOrUpdateValue(User_Key, SettingsDefault);
				}
			}
		}


        public static List<KBreed> Breeds
        {
            get
            {
                var json = AppSettings.GetValueOrDefault(Breed_Key, SettingsDefault);
                return JsonConvert.DeserializeObject<List<KBreed>>(json);
            }
            set
            {
                if (value != null)
                {
                    var json = JsonConvert.SerializeObject(value);
                    AppSettings.AddOrUpdateValue(Breed_Key, json);
                }
                else {
                    AppSettings.AddOrUpdateValue(Breed_Key, SettingsDefault);
                }
            }
        }
        public static KProfile CurrentUserProfile {
			get {
				var json = AppSettings.GetValueOrDefault(UserProfile_Key, SettingsDefault);
			    if (json == default(string))
			    {
			        return null;
			    }
				return JsonConvert.DeserializeObject<KProfile>(json);
			}
			set {
				if (value != null) {
					var json = JsonConvert.SerializeObject(value);
					AppSettings.AddOrUpdateValue(UserProfile_Key, json);
				}
				else {
					AppSettings.AddOrUpdateValue(UserProfile_Key, SettingsDefault);
				}
			}
		}

		public static MapSettings MapSettings {
			get {
				var json = AppSettings.GetValueOrDefault(MapSettings_Key, SettingsDefault);
				return JsonConvert.DeserializeObject<MapSettings>(json);
			}
			set {
				if (value != null) {
					var json = JsonConvert.SerializeObject(value);
					AppSettings.AddOrUpdateValue(MapSettings_Key, json);
				}
				else {
					AppSettings.AddOrUpdateValue(MapSettings_Key, SettingsDefault);
				}
			}
		}

		public static void RemoveUserInformation() {
			AppSettings.Remove(UserProfile_Key);
			AppSettings.Remove(User_Key);
			AppSettings.Remove(Password_Key);
			AppSettings.Remove(Email_Key);
		}
	}
}