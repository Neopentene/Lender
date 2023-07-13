using Lender.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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

namespace Lender.pages
{
    /// <summary>
    /// Interaction logic for ProfilesPage.xaml
    /// </summary>
    public partial class ProfilesPage : Page
    {
        public ObservableCollection<Profile> Profiles
        {
            get
            {
                return ProfileManager.Instance.Profiles;
            }
        }

        public ProfilesPage()
        {
            InitializeComponent();

            AddProfileMessage.Visibility = 
                ProfileManager.Instance.Profiles.Count > 0 ? Visibility.Collapsed : Visibility.Visible;

            ProfileManager.Instance.Profiles.CollectionChanged -= OnProfileCountChanged;
            ProfileManager.Instance.Profiles.CollectionChanged += OnProfileCountChanged;
        }

        private void OnProfileCountChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (ProfileManager.Instance.Profiles.Count > 0)
                AddProfileMessage.Visibility = Visibility.Collapsed;
            else
                AddProfileMessage.Visibility = Visibility.Visible;
        }

        private void CreateNewProfileButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreateProfile());
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button element) return;
            if (element.Tag is not string loanId) return;
            ProfileManager.Instance.RemoveProfile(loanId);
            MainWindow.Instance?.NotifyError($"Deleted Loan Id {loanId}", new(0, 0, 2));
        }

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button element) return;
            if (element.Tag is not string loanId) return;
            Profile? profile = ProfileManager.Instance.GetProfile(loanId);

            if(profile == null) return;

            NavigationService.Navigate(new ProfileDetails(profile));
            MainWindow.Instance?.NotifyInformation($"Opened Profile {profile.BorrowerName}", new(0, 0, 2));
        }
    }
}
