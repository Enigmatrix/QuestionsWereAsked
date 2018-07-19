using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MaterialDesignThemes.Wpf.Transitions;

namespace QuestionsWereAsked.Client.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private string _hostName;
        private string _nickName;

        public string HostName
        {
            get => _hostName;
            set
            {
                _hostName = value;
                RaisePropertyChanged();
            }
        }

        public string NickName
        {
            get => _nickName;
            set
            {
                _nickName = value;
                RaisePropertyChanged();
            }
        }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login);
        }

        public RelayCommand LoginCommand { get; }

        public void Login()
        {
            var vm = ServiceLocator.Current.GetInstance<MainViewModel>();
            vm.Login(HostName, NickName);
        }
    }
}
