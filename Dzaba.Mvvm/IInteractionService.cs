using System;

namespace Dzaba.Mvvm
{
    public interface IInteractionService
    {
        void ShowInfo(string msg);
        void ShowInfo(string msg, string title);
        void ShowWarning(string msg);
        void ShowWarning(string msg, string title);
        void ShowError(string msg);
        void ShowError(string msg, string title);
        void ShowError(Exception ex);
        void ShowError(Exception ex, string title);
        void ShowError(string msg, Exception ex);
        void ShowError(string msg, Exception ex, string title);
        bool YesNoQuestion(string question);
        bool YesNoQuestion(string question, string title);
        bool? YesNoCancelQuestion(string question);
        bool? YesNoCancelQuestion(string question, string title);
    }
}
