using GalaSoft.MvvmLight;
using QuestionsWereAsked.Common;

namespace QuestionsWereAsked.Client.ViewModel
{
    public class ResultViewModel : ViewModelBase
    {
        private PlayerState _otherState;
        private bool _victory;
        private PlayerState _yourState;

        public PlayerState YourState
        {
            get => _yourState;
            set
            {
                _yourState = value;
                RaisePropertyChanged();
            }
        }

        public PlayerState OtherState
        {
            get => _otherState;
            set
            {
                _otherState = value;
                RaisePropertyChanged();
            }
        }

        public bool Victory
        {
            get => _victory;
            set
            {
                _victory = value;
                RaisePropertyChanged();
            }
        }
    }
}