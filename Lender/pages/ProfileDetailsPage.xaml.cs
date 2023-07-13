using Lender.Models;
using Lender.windows;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for ProfileDetails.xaml
    /// </summary>
    public partial class ProfileDetails : Page, INotifyPropertyChanged
    {
        private Profile profile;
        public Profile Profile
        {
            get { return profile; }
            set
            {
                profile = value;
                OnPropertyChanged(nameof(Profile));
            }
        }

        public Breakdown Breakdown { get; set; }

        private AmortizationFrequency amortizationFrequency = AmortizationFrequency.Monthy;
        public AmortizationFrequency AmortizationFrequency
        {
            get { return amortizationFrequency; }
            set
            {
                amortizationFrequency = value;
                EnableBreakdownGeneration = true;
                DisableAmortizationFrequencyButton(value);
                OnPropertyChanged(nameof(AmortizationFrequency));
            }
        }

        private bool enableBreakdownGeneration = true;
        public bool EnableBreakdownGeneration
        {
            get { return enableBreakdownGeneration; }
            set
            {
                enableBreakdownGeneration = value;
                OnPropertyChanged(nameof(EnableBreakdownGeneration));
            }
        }

        public ProfileDetails(Profile profile)
        {
            this.profile = profile;
            profile.Payments.CollectionChanged -= OnPaymentsChanged;
            profile.Payments.CollectionChanged += OnPaymentsChanged;
            Breakdown = new Breakdown();

            Breakdown.Amortizations.CollectionChanged -= OnAmoritzationsChanged;
            Breakdown.Amortizations.CollectionChanged += OnAmoritzationsChanged;

            InitializeComponent();

            if (profile.Payments.Count > 0 && AddPaymentMessage != null)
                AddPaymentMessage.Visibility = Visibility.Collapsed;
        }

        #region Click Events

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            if (PopupWindow.OpenWindows.ContainsKey("Add Payment"))
                PopupWindow.OpenWindows["Add Payment"].Close(this, e);

            NavigationService.GoBack();
        }

        private void AddNewPayment_Click(object sender, RoutedEventArgs e)
        {
            if (PopupWindow.OpenWindows.ContainsKey("Add Payment"))
            {
                PopupWindow.OpenWindows["Add Payment"].WindowState = WindowState.Normal;
                PopupWindow.OpenWindows["Add Payment"].Activate();
                return;
            }

            PopupWindow popup = new("Add Payment");
            popup.Show(new AddPaymentPage(profile));
        }

        private void DeletePayment_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button element) return;
            if (element.Tag is not string tag) return;
            try
            {
                profile.RemovePayment(tag);
                MainWindow.Instance?.NotifyInformation("Deleted Payment", new(0, 0, 2));
            }
            catch (PaymentError error)
            {
                MainWindow.Instance?.NotifyError(error.Message, new(0, 0, 2));
            }
        }

        private void ChangeAmortizationFrequency_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button element) return;
            if (element.Tag is not string tag) return;
            if (!Enum.TryParse(tag, out AmortizationFrequency frequency)) return;
            AmortizationFrequency = frequency;
        }

        private void GenerateBreakdowns_Click(object sender, RoutedEventArgs e)
        {
            Breakdown.GenerateAmortization(AmortizationFrequency, Profile);
            EnableBreakdownGeneration = false;
        }

        #endregion Click Events

        #region Utilitity Events

        private void DisableAmortizationFrequencyButton(AmortizationFrequency value)
        {
            new List<Button>()
            {
                FMonthy, FQuaterly, FSemiAnnually, FAnnually, FBullet
            }.ForEach(button => 
            { 
                if (Enum.TryParse(button.Tag as string, out AmortizationFrequency frequency)) 
                    button.IsEnabled = frequency != value;
            });
        }
        private void OnPaymentsChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (profile.Payments.Count > 0)
                AddPaymentMessage.Visibility = Visibility.Collapsed;
            else
                AddPaymentMessage.Visibility = Visibility.Visible;
            EnableBreakdownGeneration = true;
        }

        private void OnAmoritzationsChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (profile.Payments.Count > 0)
                GenerateBreakdownsMessage.Visibility = Visibility.Collapsed;
            else
                GenerateBreakdownsMessage.Visibility = Visibility.Visible;
        }

        #endregion Utilitity Events

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
