using System;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Models.Kinvey;
using Merial.PetPixie.Core.ViewModels.Core;
using MvvmCross.Core.ViewModels;

namespace Merial.PetPixie.Core.ViewModels
{
    public class CommentItemViewModel : ViewModelBase
    {
        #region Fields

        private string _text;
        private string _fromProfileId;
        private string _fromDisplayUsername;
        private FeedItemViewModel _parent;

        #endregion

        #region Constructors

        public CommentItemViewModel(KComment kinveyComment, FeedItemViewModel parent)
        {
            this.Text = kinveyComment.Text;
            this.FromProfileId = kinveyComment.From.Id;
            this.FromDisplayUsername = kinveyComment.From.DisplayUsername;
            this._parent = parent;
        }

        #endregion

        #region Properties

        public string Text
        {
            get { return _text; }
            set
            {
                this.RaiseAndSetIfChanged(ref this._text, value);
            }
        }

        public string FromProfileId
        {
            get { return _fromProfileId; }
            set { this.RaiseAndSetIfChanged(ref _fromProfileId, value); }
        }

        public string FromDisplayUsername
        {
            get { return _fromDisplayUsername; }
            set { this.RaiseAndSetIfChanged(ref this._fromDisplayUsername, value); }
        }


                       
        public string CreatedAgoDisplay
        {

             get {



                ////   var value =  Feed.CreatedAgo;

                //   var timeAgo = DateTime.UtcNow;
                //   if (timeAgo.Days > 365)
                //   {
                //       int years = (timeAgo.Days / 365);
                //       if (timeAgo.Days % 365 != 0)
                //           years += 1;
                //       return $"{years} y";
                //   }
                //   if (timeAgo.Days > 30)
                //   {
                //       int months = (timeAgo.Days / 30);
                //       if (timeAgo.Days % 31 != 0)
                //           months += 1;
                //       return $"{months} M";
                //   }
                //   if (timeAgo.Days > 0)
                //       return $"{timeAgo.Days} d";
                //   if (timeAgo.Hours > 0)
                //       return $"{timeAgo.Hours} h";
                //   if (timeAgo.Minutes > 0)
                //       return $"{timeAgo.Minutes} m";
                //   if (timeAgo.Seconds > 5)
                //       return $"{timeAgo.Seconds} s";
                //   if (timeAgo.Seconds <= 5)
                //       return "just now";
                //   return string.Empty;

                return "5m";
            }

        }

        #endregion

        #region Commands

        public IMvxCommand ShowProfileCommand => new SafeMvxCommand(ShowProfileCommandExecute);
        
        public IMvxCommand ShowAllCommentCommand => new SafeMvxCommand(ShowAllCommentCommandExecute);

        private void ShowProfileCommandExecute()
        {
            ShowViewModel<ProfileDetailViewModel, ProfileDetailParameter>(new ProfileDetailParameter
            {
                ProfileId = FromProfileId
            });
        }

        private void ShowAllCommentCommandExecute()
        {
            FeedViewModel.SelectedItem = _parent;
            ShowViewModel<CommentListViewModel>(new { PostId = _parent.Feed.PostId });
        }

        #endregion
    }
}
