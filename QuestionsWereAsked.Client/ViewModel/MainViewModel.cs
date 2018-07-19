using System;
using System.Windows.Threading;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using MaterialDesignThemes.Wpf;
using QuestionsWereAsked.Common;
using QuestionsWereAsked.Common.Messages;

namespace QuestionsWereAsked.Client.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
        }

        public GameService GameService { get; set; }

        private int _screenIdx = 0;

        public int ScreenIdx
        {
            get => _screenIdx;
            set
            {
                _screenIdx = value;
                RaisePropertyChanged();
            }
        }

        public void Login(string hostName, string nickName)
        {
            GameService = new GameService(nickName, hostName);
            GameService.GameStateChanged += StateChanged;
        }

        private void StateChanged(GameStateMessage obj)
        {
            var gameVm = ServiceLocator.Current.GetInstance<GameViewModel>();
            var resVm = ServiceLocator.Current.GetInstance<ResultViewModel>();
            if(obj.NewState.Tag == GameTag.Started){
                ScreenIdx = 1;
            }
            else if(obj.NewState.Tag == GameTag.Ended)
            {
                GameService.GameStateChanged -= StateChanged;
                GameService = null;
                resVm.YourState = obj.NewState.ThisPlayer;
                resVm.OtherState = obj.NewState.OtherPlayer;
                resVm.Victory = resVm.YourState.Score >= resVm.OtherState.Score;
                ScreenIdx = 2;
            }
            gameVm.CurrentState = obj.NewState;
        }
    }
}