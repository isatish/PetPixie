using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Merial.PetPixie.Core.Helpers;
using Merial.PetPixie.Core.ViewModels.Core;

namespace Merial.PetPixie.Core.ViewModels
{
    public class WebViewViewModel : ViewModelBase<WebViewParameter>
    {
        public ICommand BackCommand => new SafeMvxCommand(() => Close(this));

        private string _uri;

        public string Uri
        {
            get { return _uri; }
            set
            {
                _uri = value;
                RaisePropertyChanged(() => Uri);
            }
        }

        public string Title { get; set; }

        protected override void RealInit(WebViewParameter parameter)
        {
            if (string.IsNullOrWhiteSpace(parameter?.Uri))
            {
                Close(this);
                return;
            }
            Uri = parameter.Uri;
            Title = parameter.Title;

        }
    }

    public class WebViewParameter
    {
        public string Uri { get; set; }
        public string Title { get; set; }
    }


}
