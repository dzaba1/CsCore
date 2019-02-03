using System;
using System.Windows;

namespace Dzaba.Mvvm.Windows
{
    internal sealed class MessageBoxInteractionService : IInteractionService
    {
        public void ShowError(Exception ex)
        {
            ShowError(ex.Message);
        }

        public void ShowError(string msg)
        {
            ShowError(msg, "Error");
        }

        public void ShowError(Exception ex, string title)
        {
            ShowError(ex.Message, title);
        }

        public void ShowError(string msg, Exception ex)
        {
            ShowError(msg + Environment.NewLine + ex.Message, "Error");
        }

        public void ShowError(string msg, string title)
        {
            MessageBox.Show(msg, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void ShowError(string msg, Exception ex, string title)
        {
            ShowError(msg + Environment.NewLine + ex.Message, title);
        }

        public void ShowInfo(string msg)
        {
            ShowInfo(msg, "Information");
        }

        public void ShowInfo(string msg, string title)
        {
            MessageBox.Show(msg, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void ShowWarning(string msg)
        {
            ShowWarning(msg, "Warning");
        }

        public void ShowWarning(string msg, string title)
        {
            MessageBox.Show(msg, title, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public bool? YesNoCancelQuestion(string question)
        {
            return YesNoCancelQuestion(question, "Question");
        }

        public bool? YesNoCancelQuestion(string question, string title)
        {
            var result = MessageBox.Show(question, title, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            switch (result)
            {
                case MessageBoxResult.Cancel:
                    return null;
                case MessageBoxResult.No:
                    return false;
                case MessageBoxResult.Yes:
                    return true;
                default: throw new ArgumentException($"Unknown MessageBoxResult value: {result}");
            }
        }

        public bool YesNoQuestion(string question)
        {
            return YesNoQuestion(question, "Question");
        }

        public bool YesNoQuestion(string question, string title)
        {
            return MessageBox.Show(question, title, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
        }
    }
}
