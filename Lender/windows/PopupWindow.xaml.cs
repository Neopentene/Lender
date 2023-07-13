using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lender.windows
{
    // TODO In future Define a custom pages class for popup window

    /// <summary>
    /// Interaction logic for PopupWindow.xaml
    /// </summary>
    public partial class PopupWindow : Window
    {
        public static readonly Dictionary<string, PopupWindow> OpenWindows = new();

        public PopupWindow(string title)
        {
            InitializeComponent();
            Title = title;
            OpenWindows.Add(title, this);
        }

        #region Control Bar Functions

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr sizeHandle, int Msg, int wparam, int lparam);

        private void ControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender == null) return;
            WindowInteropHelper helper = new(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }

        private void ControlBar_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender == null) return;
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void CloseButtonControl_Click(object sender, RoutedEventArgs e)
        {
            if (sender == null) return;
            OpenWindows.Remove(Title);
            Close();
        }

        private void MinimizeButtonControl_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        #endregion Control Bar Functions

        public void Show<T>(T content) where T : Page, IClosable
        {
            content.CloseWindow += Close;
            ContentFrame.Content = content;
            Show();
        }

        public void Close(object? sender, EventArgs e)
        {
            if(sender == null) return;
            OpenWindows.Remove(Title);
            Close();
        }
    }

    public interface IClosable
    {
        public event EventHandler CloseWindow;
    }
}
