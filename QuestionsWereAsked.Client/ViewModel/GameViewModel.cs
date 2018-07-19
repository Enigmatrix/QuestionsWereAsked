using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MaterialDesignThemes.Wpf;
using QuestionsWereAsked.Common;

namespace QuestionsWereAsked.Client.ViewModel
{
    public class GameViewModel : ViewModelBase
    {
        private GameState _currentState;

        public GameState CurrentState
        {
            get => _currentState;
            set
            {
                _currentState = value;
                if(_currentState.LastAnswer != null)
                    MessageQueue.Enqueue(_currentState.LastAnswer);
                RaisePropertyChanged();
            }
        }

        public GameViewModel()
        {
            AnswerClickCommand = new RelayCommand<string>(AnswerClick);
            MessageQueue = new SnackbarMessageQueue();
        }

        public RelayCommand<string> AnswerClickCommand { get; set; }

        private void AnswerClick(string obj)
        {
            var svc = ServiceLocator.Current.GetInstance<MainViewModel>().GameService;
            svc.SendAnswer(int.Parse(obj));
        }

        public SnackbarMessageQueue MessageQueue { get; set; }
    }
}
