namespace Dzaba.Mvvm
{
    public interface ILongOperationPopup
    {
        bool IsLongOperationExecuting { get; }
        string LongOperationMessage { get; }

        void OpenLongOperationPopup(string message);
        void CloseLongOperationPopup();
    }
}
