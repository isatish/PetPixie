using Xamarin.Forms;
using System;
using System.Windows.Input;
using Merial.PetPixie.Core.ViewModels;
using Merial.PetPixie.Core.Models;

namespace Merial.PetPixie.Core.Views
{
    public partial class UserRating : ContentView
    {
        public event EventHandler Clicked;
        public DiscoverItemViewModel _viewModel;

        public Image imageMain;

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(Button), null);

        public ICommand Command
        {
            get
            {
                return (ICommand)GetValue(CommandProperty);
            }
            set
            {
                SetValue(CommandProperty, value);
            }
        }

        //public static readonly BindableProperty ButtonBackgroundColorProperty =
        //    BindableProperty.Create(nameof(ButtonBackgroundColor), typeof(Color), typeof(ImageButton), Color.Transparent);

        //public Color ButtonBackgroundColor
        //{
        //    get
        //    {
        //        return (Color)GetValue(ButtonBackgroundColorProperty);
        //    }
        //    set
        //    {
        //        SetValue(ButtonBackgroundColorProperty, value);
        //    }
        //}

        //public static readonly BindableProperty TextProperty =
        //    BindableProperty.Create(nameof(Text), typeof(string), typeof(ImageButton), null);

        //public string Text
        //{
        //    get
        //    {
        //        return (string)GetValue(TextProperty);
        //    }
        //    set
        //    {
        //        SetValue(TextProperty, value);
        //    }
        //}

        //public static readonly BindableProperty SourceProperty =
        //    BindableProperty.Create(nameof(Source), typeof(FileImageSource), typeof(ImageButton), null);

        //public FileImageSource Source
        //{
        //    get
        //    {
        //        return (FileImageSource)GetValue(SourceProperty);
        //    }
        //    set
        //    {
        //        SetValue(SourceProperty, value);
        //    }
        //}

        public UserRating()
        {
            InitializeComponent();
            //   BindingContext = this;
        }

        public UserRating(DiscoverItemViewModel vm, FeedModel likeContext, string imageSource, int likeImageNumber)
        {
            InitializeComponent();

            _viewModel = vm;
            this.BindingContext = _viewModel;
          //  this.imageLike.BindingContext = likeContext;// _viewModel.Image1;
          //  this.labelLike.BindingContext = likeContext;//  _viewModel.Image1;

            LoadContents(imageSource, likeContext, likeImageNumber);
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            _viewModel.DetailsCommand1.Execute(1);

            //((DiscoverItemViewModel)BindingContext).ShowProfileCommand.Execute();


            //ShowViewModel<ProfileDetailViewModel, ProfileDetailParameter>(new ProfileDetailParameter
            //{
            //    ProfileId = comment.FromProfileId
            //});




        }

        void Handle_ImageTapped(object sender, System.EventArgs e)
        {
            _viewModel.ShowProfileCommand.Execute();

            //         ((DiscoverItemViewModel)BindingContext).ShowProfileCommand.Execute(image2);
        }

        public void LoadContents(string imageSource, FeedModel imageContext, int likeImageNumber)
        {

            if (imageSource != null)
            {
                stackContents.Children.Clear();
                imageMain = new Image() { Source = imageSource, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand, HeightRequest = this.HeightRequest, WidthRequest = this.WidthRequest };
                imageMain.BindingContext = imageContext;
                imageMain.GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = new Command(() =>
                    {
                        var currFeed = (FeedModel)imageMain.BindingContext;
                        ((DiscoverItemViewModel)BindingContext).ShowProfileCommand.Execute(currFeed);
                    }),
                    NumberOfTapsRequired = 1

                });



                //stackSmallerPics.Children.Add(new Image() { Source = vm.Image2.ImageSrcFeed, WidthRequest = 100, HeightRequest = 100 });// { Source = vm.Image1;);// new image vm.Image1);
                stackContents.Children.Add(imageMain);

                int? numberOfLikes = 0;
               // GridLikes.IsVisible = false;

                //if (_viewModel.Image1.NbLikes != null)
                //{
                //    numberOfLikes = imageContext.NbLikes;
                //}

                //if (numberOfLikes > 0)
                //{
                //    GridLikes.IsVisible = true;
                //    labelLike.Text = numberOfLikes.ToString();
                //    imageLike.GestureRecognizers.Add(new TapGestureRecognizer
                //    {
                //        Command = new Command(() =>
                //        {

                //            _viewModel.GoLikersCommand1.Execute(likeImageNumber);

                //        }),
                //        NumberOfTapsRequired = 1

                //    });



                //    labelLike.GestureRecognizers.Add(new TapGestureRecognizer
                //    {
                //        Command = new Command(() =>
                //        {

                //            _viewModel.GoLikersCommand1.Execute(likeImageNumber);
                //        }),
                //        NumberOfTapsRequired = 1

                //    });


                //}

            }

        }


        async void HandleClick(object sender, EventArgs e)
        {
            Clicked.Invoke(this, e);

            //ds await root.ScaleTo(1.2, 100);
            //ds await root.ScaleTo(1, 100);
        }
    }
}