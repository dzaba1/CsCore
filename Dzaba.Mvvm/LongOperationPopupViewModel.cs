namespace Dzaba.Mvvm
{
    public class LongOperationPopupViewModel : BaseViewModel, ILongOperationPopup
    {
        private bool _isLongOperationExecuting;
        public bool IsLongOperationExecuting
        {
            get { return _isLongOperationExecuting; }
            private set
            {
                _isLongOperationExecuting = value;
                RaisePropertyChanged();
            }
        }

        private string _longOperationMessage;
        public string LongOperationMessage
        {
            get { return _longOperationMessage; }
            private set
            {
                _longOperationMessage = value;
                RaisePropertyChanged();
            }
        }

        public void Close()
        {
            IsLongOperationExecuting = false;
        }

        public void Open(string message)
        {
            LongOperationMessage = message;
            IsLongOperationExecuting = true;
        }
    }
}
