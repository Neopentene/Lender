using System.Windows;

namespace Lender
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Exit += Lender.MainWindow.Application_Closing;
        }
    }
}
