using KinveyXamarin;
using Merial.PetPixie.Core.Models.Kinvey;
using Merial.PetPixie.Core.Services.Contracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Common.Exceptions;
using Merial.PetPixie.Core.Helpers;

namespace Merial.PetPixie.Core.Services
{
    public enum UserType
    {
        Disconected,
        Kinvey,
        Merial,
        Facebook,
        Twitter,
        ForgotPassword
    }

    public class UserService : KinveyServiceBase, IUserService
    {
        #region Fields

        private readonly IKinveyService _kinveyService;

        private string _profileId = null;
        private ConnexionState _state;
        private Task<User> tsc_connection;

        private Dictionary<string, string> _emailToUsernameDictionary = new Dictionary<string, string>();

        #endregion

        #region Constructors

        public UserService(IKinveyService kinveyService) : base(kinveyService)
        {
            _kinveyService = kinveyService;

            this.EnsureConnected();
        }

        #endregion

        #region Sign Methods

        public async Task<bool> EmailExistAsync(string email)
        {
            KExists response = new KExists { Exists = true };

            try
            {
                var userTypeResult = await this.GetUserType(email);
                response.Exists = (userTypeResult.Item1 != "none");
            }
            catch (Exception e)
            {
                _logger.Error(e);
                response = new KExists { Exists = true };
            }
            return response?.Exists ?? false;
        }

        public Task<User> LoginAutomatedAsync(string username, string password)
        {
            if (this._state == ConnexionState.ConnectingWithUser)
                return this.tsc_connection;

            var tsc = new TaskCompletionSource<User>();

            var unknownType =
                !username.StartsWith(Constants.Connection.KinveyUsername)
                && !username.StartsWith(Constants.Connection.FacebookUsername)
                && !username.StartsWith(Constants.Connection.TwitterUsername);

            if (unknownType && _emailToUsernameDictionary.ContainsKey(username))
            {
                username = _emailToUsernameDictionary[username];
                unknownType = false;
            }

            Task.Run(async () =>
            {
                try
                {
                    if (unknownType)
                    {
                        var oldEmail = username;
                        var userResult = this.GetUserType(username).Result;
                        switch (userResult.Item1)
                        {
                            case "Kinvey":
                                username = Constants.Connection.KinveyUsername + userResult.Item2;
                                break;
                            case "Facebook":
                                username = Constants.Connection.FacebookUsername + userResult.Item2;
                                break;
                            case "Twitter":
                                username = Constants.Connection.TwitterUsername + userResult.Item2;
                                break;
                        }
                        this._emailToUsernameDictionary.Add(oldEmail, username);
                    }
                    else if (this._state == ConnexionState.ConnectingWithForgotPassword)
                        await this.tsc_connection;

                    this.Logout();

                    this._state = ConnexionState.ConnectingWithUser;
                    var user = await this.ConnectWithUser(username, password);

                    JToken profileIdToken = string.Empty;
                    if (user.Attributes.TryGetValue("profile_id", out profileIdToken))
                        this._profileId = profileIdToken.ToString();
                    
                    tsc.SetResult(user);
                }
                catch (Exception e)
                {
                    _logger.Error(e);
                    tsc.SetException(e);
                }
            });

            return tsc.Task;
        }

        public Task<User> SignupAsync(string username, string password, string email)
        {
            var tsc = new TaskCompletionSource<User>();

            Task.Run(async ()  =>
            {
                if (await this.UserExistAsync(username))
                {
                    tsc.SetException(new UsernameAlreadyExistsException());
                    return;
                }

                var attributes = new Dictionary<string, JToken>();
                attributes.Add("email", email);

                this.Logout();

                this._state = ConnexionState.ConnectingWithUser;
                _kinveyService.GetClient().User().Create(username, password, new KinveyDelegate<User>
                {
                    onSuccess = user =>
                    {
                        this._state = ConnexionState.Connected;
                        tsc.SetResult(user);
                    },
                    onError = error =>
                    {
                        this._state = ConnexionState.NotConnected;
                        tsc.SetException(error);
                    }
                },
                attributes);

            });
            
            return tsc.Task;
        }

        public void Logout()
        {
            _kinveyService.GetClient().User().Logout();
            Settings.RemoveUserInformation();
            this._state = ConnexionState.NotConnected;
            this.UserType = UserType.Disconected;
        }

        public async Task<bool> FacebookUserAlreadyExists(string facebookId)
        {
            try
            {
                await this.EnsureConnected();
                var foundUsers = await base.PostRPCAsync<List<KProfile>>("FindFriends", new { facebook_ids = new List<string> { facebookId } });
                return foundUsers.Any();
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return false;
            }
        }

        #endregion

        #region Properties

        public UserType UserType { get; private set; }

        #endregion

        #region Public Methods

        public string GetProfileId()
        {
            if (_profileId == null)
                _profileId = Settings.CurrentUserProfile?.Id;
            return this._profileId;
        }

        public async Task<bool> SendResetPasswordInstructionsAsync(string email)
        {
            var ret = false;
            try
            {
                var usertypeDetected = await this.GetUserType(email);

                switch (usertypeDetected.Item1)
                {
                    case "Merial":
                        await PostRPCAsync<object>("MerialPasswordReset", new { email });
                        ret = true;
                        break;
                    case "Kinvey":
                        await _kinveyService.GetClient().User().ResetPasswordAsync(usertypeDetected.Item2);
                        ret = true;
                        break;
                }
            }
            catch (KinveyJsonResponseException ex)
            {
                var userexists = ex.Details?.Debug.Contains("UserDefinedError");
                if (userexists.HasValue && userexists.Value)
                {
                    ret = false;
                }
                else
                {
                    _logger.Error(ex);
                }
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
            return ret;
        }

        public Task<User> ChangeEmailAsync(User updatedUser, string newMail)
        {
            updatedUser.Attributes["email"] = newMail;

            return _kinveyService.GetClient()
                .User()
                .UpdateAsync(updatedUser);
        }
        
        public Task<KinveyDeleteResponse> DeleteMeAsync()
        {
            return this.KinveyService.GetClient().User().DeleteAsync(this.GetCurrentUserId(), true);
        }

        public string GetCurrentUserId()
        {
            return _kinveyService.GetClient().User().Id;
        }
        
        public bool UserConnected()
        {
            return _kinveyService.GetClient().User().isUserLoggedIn()
                && !this.ActiveKcsUserIsForgotPasswordUser();
        }

        #endregion

        #region Private Methods

        private Task<Tuple<string, string>> GetUserType(string email)
        {
            var tsc = new TaskCompletionSource<Tuple<string,string>>();
            
            this.EnsureConnected().ContinueWith(async t =>
            {
                if (t.IsFaulted)
                    tsc.SetException(t.Exception);
                // TODO : t.IsCanceled
                else if (t.IsCompleted)
                {
                    try
                    {
                        var usertypeDetected = await PostRPCAsync<KUserTypeDetection>("detectUserType", new { email });
                        tsc.SetResult(new Tuple<string, string>(usertypeDetected.UserType, usertypeDetected.UserName));
                    }
                    catch (Exception e)
                    {
                        _logger.Error(e);
                        tsc.SetException(e);
                    }
                }
            });

            return tsc.Task;
        }

        private async Task<bool> UserExistAsync(string username)
        {
            KExists response = null;
            try
            {
                await this.EnsureConnected();
                response = await PostRPCAsync<KExists>("profileUsernameTaken", new { display_username = username });
            }
            catch (Exception e)
            {
                _logger.Error(e);
                response = new KExists { Exists = false };
            }

            return response?.Exists ?? false;
        }

        private Task<User> EnsureConnected()
        {
            if (this._state == ConnexionState.NotConnected && _kinveyService.GetClient().User().isUserLoggedIn())
                this._state = ConnexionState.Connected;

            switch (this._state)
            {
                case ConnexionState.NotConnected:
                    this.tsc_connection = ConnectWithForgotPassword();
                    break;
            }
            
            if (tsc_connection == null)
            {
                var tsc = new TaskCompletionSource<User>();
                tsc.SetResult(_kinveyService.GetClient().User());
                this.tsc_connection = tsc.Task;
            }

            return tsc_connection;
        }

        private Task<User> ConnectWithForgotPassword()
        {
            this._state = ConnexionState.ConnectingWithForgotPassword;
            return this.ConnectWithUser(Constants.PasswordReset.User, Constants.PasswordReset.Password);
        }

        private Task<User> ConnectWithUser(string username, string password)
        {
            var tsc = new TaskCompletionSource<User>();

            this.KinveyService.GetClient().User().LoginWithAuthorizationCodeAPI(
                username,
                password,
                Constants.Kinvey.RedirectUri,
                new KinveyDelegate<User>
                {
                    onSuccess = user =>
                    {
                        this._state = ConnexionState.Connected;
                        this.SetUserType(username);
                        tsc.SetResult(user);
                    },
                    onError = error =>
                    {
                        this._state = ConnexionState.NotConnected;
                        tsc.SetException(error);
                    }
                });

            return tsc.Task;
        }

        private bool ActiveKcsUserIsForgotPasswordUser()
        {
            try
            {
                if (!_kinveyService.GetClient().User().isUserLoggedIn())
                    return false;


                // TODO : Find an other solution to let it works when internet is not connected
                //var currentUser = _kinveyService.GetClient().User().RetrieveAsync().Result;

                //JToken socialIdentityToken = string.Empty;
                //if (currentUser.Attributes.TryGetValue("_socialIdentity", out socialIdentityToken))
                //{
                //    var socialIdUsername = socialIdentityToken["kinveyAuth"]["id"];
                //    return socialIdUsername.ToString() == Constants.PasswordReset.User;
                //}
                return Settings.CurrentUser == null || string.IsNullOrEmpty(Settings.CurrentUserProfile?.Id);
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }

            return false;
        }

        private void SetUserType(string username)
        {
            if (username == Constants.PasswordReset.User)
                this.UserType = UserType.ForgotPassword;
            else if (username.StartsWith(Constants.Connection.KinveyUsername))
                this.UserType = UserType.Kinvey;
            else if (username.StartsWith(Constants.Connection.FacebookUsername))
                this.UserType = UserType.Facebook;
            else if (username.StartsWith(Constants.Connection.TwitterUsername))
                this.UserType = UserType.Twitter;
            else
                this.UserType = UserType.Merial;
        }

        #endregion

        private enum ConnexionState
        {
            NotConnected,
            ConnectingWithForgotPassword,
            ConnectingWithUser,
            Connected,
        }
    }
}