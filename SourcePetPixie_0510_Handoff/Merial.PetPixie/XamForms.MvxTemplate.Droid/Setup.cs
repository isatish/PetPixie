using Android.Content;
using MvvmCross.Platform;
using MvvmCross.Droid.Platform;
using MvvmCross.Droid.Views;
using MvvmCross.Forms.Presenter.Droid;
using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using System;

namespace XamForms.MvxTemplate.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext)
            : base(applicationContext)
        {
        }

        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

         //ds   Mvx.RegisterSingleton<Merial.PetPixie.Core.Services.Contracts.ILocalizeService>(new Services.LocalizeService());
        }

        //protected override IMvxApplication CreateApp()
        //{
        // //ds   return new Merial.PetPixie.Core.App();
        //}

        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            var presenter = new MvxFormsDroidPagePresenter();
            Mvx.RegisterSingleton<IMvxViewPresenter>(presenter);

            return presenter;
        }

        protected override IMvxApplication CreateApp()
        {
            throw new NotImplementedException();
        }



        //protected override IMvxApplication CreateApp()
        //{
        //    throw new NotImplementedException();
        //}
    }
}