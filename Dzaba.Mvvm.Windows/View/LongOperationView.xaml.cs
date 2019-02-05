using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Dzaba.Mvvm.Windows.View
{
    /// <summary>
    /// Interaction logic for LongOperationView.xaml
    /// </summary>
    public partial class LongOperationView : UserControl, ILongOperationPopup
    {
        public LongOperationView()
        {
            InitializeComponent();
        }

        [Bindable(true, BindingDirection.TwoWay)]
        public bool Show
        {
            get { return (bool)GetValue(ShowProperty); }
            set { SetValue(ShowProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Show.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowProperty =
            DependencyProperty.Register("Show", typeof(bool), typeof(LongOperationView), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        [Bindable(true, BindingDirection.TwoWay)]
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public bool IsLongOperationExecuting => Show;

        public string LongOperationMessage => Message;

        // Using a DependencyProperty as the backing store for Message.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(LongOperationView), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public void Open(string message)
        {
            Message = message;
            Show = true;
        }

        public void Close()
        {
            Show = false;
        }
    }
}
