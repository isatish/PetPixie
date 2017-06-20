using System;
using System.Collections.Generic;
using System.Reflection;
using Merial.PetPixie.Core;
using Merial.PetPixie.Core.Models.Enums;
using Merial.PetPixie.Core.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using MvvmCross.Platform;
using MvvmCross.Platform.iOS.Platform;
using MvvmCross.Platform.Platform;
using UIKit;

namespace Merial.PetPixie.iOS
{
	public class CustomPresenter : MvxViewPresenter {

		public CustomPresenter()
		{
		}


		public override void ChangePresentation(MvxPresentationHint hint)
		{
			
		}

		public override void Show(MvxViewModelRequest request)
		{

			if (request.PresentationValues != null)
			{
				if (request.PresentationValues.ContainsKey(Config.MvxPresentation.PresentationNavigation))
				{
					if (request.PresentationValues[Config.MvxPresentation.PresentationNavigation] ==
						Config.MvxPresentation.NavigationClearStack)
					{
					}
					if (request.PresentationValues[Config.MvxPresentation.PresentationNavigation] ==
						Config.MvxPresentation.NavigationClearTop)
					{

					}

					if (request.PresentationValues[Config.MvxPresentation.PresentationNavigation] ==
						Config.MvxPresentation.ReloadScreen)
					{

					}
				}
			}

			Show(request);
			return;
		}
	}
}

