using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.Models;
using Merial.PetPixie.Core.Models.Kinvey;
using Merial.PetPixie.Core.Plugins;
using Merial.PetPixie.Core.Services.Contracts;
using Merial.PetPixie.Core.ViewModels.Core;
using Merial.PetPixie.Core.ViewModels.Interfaces;
using MvvmCross.Core.ViewModels;

namespace Merial.PetPixie.Core.ViewModels
{
    public class CommentListViewModel : ViewModelBase //, ISwipeListViewModel
    {
        #region Constants

        private const int MaxLenghtComment = 140;
        private readonly ICommentService _commentService;
        private readonly IDeviceHelper _deviceHelper;

        #endregion

        #region Fields

        private string _postId;
        private string _addCommentText;
        private ObservableCollection<EntityBase> _comments;
        private bool _isLoading;
        private string _errorComment;
        private bool _hasComments = false;
        private bool _noComments = true;

        #endregion

        #region Constructors

        public CommentListViewModel(ICommentService commentService, IDeviceHelper deviceHelper)
        {
            _commentService = commentService;
            _deviceHelper = deviceHelper;
            _comments = new ObservableCollection<EntityBase>();
           
        }

        #endregion

        #region Public Properties

        public string PostId
        {
            get { return _postId; }
            set
            {
                _postId = value;
                RaisePropertyChanged(() => PostId);
            }
        }

        public bool HasComments
        {
            get { return _hasComments; }
            set
            {
                _hasComments = value;
                RaisePropertyChanged(() => HasComments);
            }
        }

                
        public bool NoComments
        {
            get { return _noComments; }
            set
            {
                _noComments = value;
                RaisePropertyChanged(() => NoComments);
            }
        }



        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                RaisePropertyChanged(() => IsLoading);
            }
        }

        public string AddCommentText
        {
            get { return _addCommentText; }
            set
            {
                if (value.Length > MaxLenghtComment)
                {
                    ErrorComment = $"{MaxLenghtComment} chars max";
                }
                else
                {
                    ErrorComment = null;
                    _addCommentText = value;
                }

                RaisePropertyChanged(() => AddCommentText);
            }
        }

        public string ErrorComment
        {
            get { return _errorComment; }
            set
            {
                _errorComment = value;
                RaisePropertyChanged(() => ErrorComment);
            }
        }

        public ObservableCollection<EntityBase> EntityList
        {
            get { return _comments; }
            set
            {
                _comments = value;
                RaisePropertyChanged(() => EntityList);
            }
        }

        #endregion

        #region MvxCommands

        public IMvxCommand AddCommentCommand => new SafeMvxCommandAsync(async () => await EnsureIsConnected(AddCommentCore));

        private async Task AddCommentCore()
        {
            this.IsLoading = true;
            _deviceHelper.HideKeyBoard();
            var newComment = default(KComment);
            if (PostId != null && !string.IsNullOrWhiteSpace(AddCommentText))
                newComment= await _commentService.Add(PostId, AddCommentText);

            if (newComment == default(KComment))
            {
                PopupService.DisplayMessage(
                    $"Your woof is not sent,{Environment.NewLine}please try again later", "Error");
                this.IsLoading = false;
            }
            else
            {
                try
                {
                    await LoadData();
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    OnLoadDataError(ex);
                }
            }

            AddCommentText = string.Empty;
        }
        
        public IMvxCommand ShowProfileCommand => new SafeMvxCommand<CommentModel>(OnShowProfilekCore);

        private void OnShowProfilekCore(CommentModel comment)
        {          
            ShowViewModel<ProfileDetailViewModel, ProfileDetailParameter>(new ProfileDetailParameter
            {
                ProfileId = comment.FromProfileId
            });
        }

        #endregion

        #region SwipeList Methods

        public bool CanSwipeItemAtPosition(int index)
        {
            return ((CommentModel) EntityList[index]).IsCreatedByConnectedUser;
        }

        public void ItemSwiped(int index)
        {
            this.DeleteCommentAt(index);
        }

        #endregion

        #region Lyfe Cicle Methods

        public async Task LoadData()
        {
            this.IsLoading = true;

            var cmts = await _commentService.Get(PostId);

            HasComments = false;
            NoComments = true;


            EntityList.Clear();
            if (cmts != null)
            {
                EntityList = new ObservableCollection<EntityBase>(cmts.Select(CommentModel.CreateFrom));
                FeedViewModel.SelectedItem.Feed.Comments = new ObservableCollection<KComment>(cmts.Take(2));
                FeedViewModel.SelectedItem.Feed.CommentsCount = cmts.Count;


                if (cmts.Count > 0)
                {
                    HasComments = true;
                    NoComments = false;

                }
            }
            this.IsLoading = false;
        }

        public async void Init(CommentParameters parameter)
        {
            IsLoading = true;

            PostId = parameter.PostId;
            await LoadDataAsync();
         
            IsLoading = false;
        }

        protected async override Task<bool> LoadDataAsync()
        {
            await LoadData();
            return true;
        }

        #endregion

        #region Methods

        protected override void OnLoadDataError(Exception exception)
        {
            base.OnLoadDataError(exception);
            PopupService.DisplayMessage(
                EntityList == null
                    ? "Unable to load comments, please retry later"
                    : "Unable to update comments, please retry later", "Error");

            this.IsLoading = false;
        }

        public void DeleteCommentAt(int index)
        {
            if (!CanSwipeItemAtPosition(index)) return;
            IsLoading = true;
            this._commentService.Delete(PostId, ((CommentModel)EntityList[index]).Id);
            EntityList.RemoveAt(index);
            FeedViewModel.SelectedItem.Feed.Comments = new ObservableCollection<KComment>(EntityList.Select(c => ((CommentModel)c).KComment).Take(2));
            FeedViewModel.SelectedItem.Feed.CommentsCount = EntityList.Count;
            IsLoading = false;
        }

        #endregion
    }

    public class CommentParameters
    {
        public string PostId { get; set; }
    }
}
