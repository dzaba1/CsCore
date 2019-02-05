namespace Dzaba.Mvvm
{
    public interface ILongOperationPopup
    {
        bool IsLongOperationExecuting { get; }
        string LongOperationMessage { get; }

        void Open(string message);
        void Close();
    }
}
