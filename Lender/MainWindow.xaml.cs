using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using Lender.Utils;
using Lender.Models;
using Lender.pages;
using System.Collections.ObjectModel;

namespace Lender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow? Instance { get; private set; }

        public MainWindow()
        {
            Instance ??= this;
            InitializeComponent();
            ContentFrame.Navigate(new ProfilesPage());

            DataStore.OnReading += () => NotifyInformation("Reading Local Data");
            _ = DataStore.Reader.Read(ProfileManager.DEFAULT_FILE_NAME, ProfileManager.Instance);

        }

        #region Control Bar Functions

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr sizeHandle, int Msg, int wparam, int lparam);

        private void ControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender == null) return;
            WindowInteropHelper helper = new(this);
            SendMessage(helper.Handle, 161, 2, 0);
            UpdateRestoreIcons(WindowState);
        }

        private void ControlBar_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender == null) return;
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void CloseButtonControl_Click(object sender, RoutedEventArgs e)
        {
            if (sender == null) return;
            Application.Current.Shutdown();
        }

        private void MinimizeButtonControl_Click(object sender, RoutedEventArgs e)
        {
            if (sender == null) return;
            WindowState = WindowState.Minimized;
        }

        private void RestoreControlButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender == null) return;
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
                UpdateRestoreIcons(WindowState);
            }
            else if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
                UpdateRestoreIcons(WindowState);
            }
        }

        private void UpdateRestoreIcons(WindowState state)
        {
            if (state == WindowState.Normal)
            {
                RestoreMaximizeIcon.Visibility = Visibility.Visible;
                RestoreMinimizeIcon.Visibility = Visibility.Collapsed;
            }
            else if (state == WindowState.Maximized)
            {
                RestoreMaximizeIcon.Visibility = Visibility.Collapsed;
                RestoreMinimizeIcon.Visibility = Visibility.Visible;
            }
        }

        #endregion Control Bar Functions

        #region Notification Control and Logic

        public ObservableCollection<Notification> NotificationQueue { get; set; } = new();

        public void NotifyError(string message, TimeSpan? delay = null) => Notify(message, NotificationType.Error, delay);

        public void NotifyWarning(string message, TimeSpan? delay = null) => Notify(message, NotificationType.Warning, delay);

        public void NotifyInformation(string message, TimeSpan? delay = null) => Notify(message, NotificationType.Infomation, delay);

        public void NotifySuccess(string message, TimeSpan? delay = null) => Notify(message, NotificationType.Success , delay);

        private async void Notify(string message, NotificationType type, TimeSpan? delay = null)
        {
            Notification notification = new(NotificationQueue.Count.ToString(), type, message);

            NotificationQueue.Add(notification);

            await Task.Delay(delay ?? new(0, 0, 3));

            NotificationQueue.Remove(notification);
        }

        #endregion Notification Control and Logic

        #region Window and Application Events

        // Add Application Events in the App.xaml.cs file
        public static async void Application_Closing(object sender, ExitEventArgs e)
        {
            await DataStore.Writer.Write(ProfileManager.Instance);
        }

        #endregion Window Application Events
    }
}
