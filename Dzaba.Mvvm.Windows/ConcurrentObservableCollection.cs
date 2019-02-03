using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Dzaba.Mvvm.Windows
{
    public class ConcurrentObservableCollection<T> : ObservableCollection<T>
    {
        public ConcurrentObservableCollection()
        {
            
        }

        public ConcurrentObservableCollection(IEnumerable<T> collection)
            : base(collection)
        {
            
        }

        protected override void ClearItems()
        {
            Application.Current.Dispatcher.Invoke(ClearItems);
        }

        protected override void InsertItem(int index, T item)
        {
            Application.Current.Dispatcher.Invoke(() => base.InsertItem(index, item));
        }

        protected override void MoveItem(int oldIndex, int newIndex)
        {
            Application.Current.Dispatcher.Invoke(() => base.MoveItem(oldIndex, newIndex));
        }

        protected override void RemoveItem(int index)
        {
            Application.Current.Dispatcher.Invoke(() => base.RemoveItem(index));
        }

        protected override void SetItem(int index, T item)
        {
            Application.Current.Dispatcher.Invoke(() => base.SetItem(index, item));
        }
    }
}
