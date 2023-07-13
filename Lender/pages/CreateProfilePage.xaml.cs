using Lender.Models;
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

namespace Lender.pages
{
    /// <summary>
    /// Interaction logic for CreateProfile.xaml
    /// </summary>
    public partial class CreateProfile : Page
    {
        private string borrowerName = "";
        private double principalAmount = 0;
        private double sanctionedAmount = 0;
        private double interestRate = 0;
        private DateTime startDate = DateTime.UtcNow;
        private int timePeriod = 0;

        private bool borrowerNameError = true;
        private bool principalAmountError = true;
        private bool sanctionedAmountError = true;
        private bool interestRateError = true;
        private bool startDateError = true;
        private bool timePeriodError = true;

        public CreateProfile()
        {
            InitializeComponent();
            if (StartDate != null) StartDate.SelectedDate = startDate;
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BorrowerName_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                borrowerName = BorrowerName.Text.Trim();
                if (borrowerName.Length < 1) throw new Exception();
                if (BorrowerNameError != null) BorrowerNameError.Content = "";
                borrowerNameError = false;
                return;
            }
            catch
            {
                BorrowerNameError.Content = "Enter Borrower Name";
            }
            borrowerNameError = true;
        }

        private void PrincipalAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                principalAmount = double.Parse(PrincipalAmount.Text.Trim());
                if (principalAmount > 1e20) throw new OverflowException();
                if (PrincipalAmountError != null) PrincipalAmountError.Content = "";
                principalAmountError = false;
                return;
            }
            catch (ArgumentNullException)
            {
                PrincipalAmountError.Content = "Enter Principal Amount";
            }
            catch (FormatException)
            {
                PrincipalAmountError.Content = "Enter Valid Principal Amount";
            }
            catch (OverflowException)
            {
                PrincipalAmountError.Content = "Principal Amount Too High";
            }
            principalAmountError = true;
        }

        private void InterestRate_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                interestRate = double.Parse(InterestRate.Text.Trim());
                if (interestRate > 100) throw new OverflowException();
                if (InterestRateError != null) InterestRateError.Content = "";
                interestRateError = false;
                return;
            }
            catch (ArgumentNullException)
            {
                InterestRateError.Content = "Enter Interest Rate";
            }
            catch (OverflowException)
            {
                InterestRateError.Content = "Interest Rate Too High";
            }
            catch
            {
                InterestRateError.Content = "Enter Valid Interest Rate";
            }
            interestRateError = true;
        }

        private void SanctionedAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                sanctionedAmount = double.Parse(SanctionedAmount.Text.Trim());
                if (sanctionedAmount > principalAmount) throw new OverflowException();
                if (SanctionedAmountError != null) SanctionedAmountError.Content = "";
                sanctionedAmountError = false;
                return;
            }
            catch (ArgumentNullException)
            {
                SanctionedAmountError.Content = "Enter Sanctioned Amount";
            }
            catch (FormatException)
            {
                SanctionedAmountError.Content = "Enter Valid Sanctioned Amount";
            }
            catch (OverflowException)
            {
                SanctionedAmountError.Content = "Sanctioned Amount Too High";
            }
            sanctionedAmountError = true;
        }

        private void StartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (StartDate.SelectedDate == null)
                    throw new ArgumentNullException(nameof(e));
                else
                    startDate = (DateTime)StartDate.SelectedDate;

                if (sanctionedAmount > principalAmount) throw new OverflowException();
                if (StartDateError != null) StartDateError.Content = "";
                startDateError = false;
                return;
            }
            catch (ArgumentNullException)
            {
                StartDateError.Content = "Enter Start Date";
            }
            catch (OverflowException)
            {
                StartDateError.Content = "Start Date too High";
            }
            catch
            {
                StartDateError.Content = "Enter Valid Start Date";
            }
            startDateError = true;
        }

        private void TimePeriod_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                timePeriod = int.Parse(TimePeriod.Text.Trim());
                if (timePeriod < 1) throw new InvalidOperationException();
                if (TimePeriodError != null) TimePeriodError.Content = "";
                timePeriodError = false;
                return;
            }
            catch (ArgumentNullException)
            {
                TimePeriodError.Content = "Enter Time Period";
            }
            catch (OverflowException)
            {
                TimePeriodError.Content = "Time Period Too High";
            }
            catch (InvalidOperationException)
            {
                TimePeriodError.Content = "Time Period Too Less";
            }
            catch
            {
                TimePeriodError.Content = "Enter Valid Time Period";
            }
            timePeriodError = true;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (borrowerNameError || principalAmountError || sanctionedAmountError || interestRateError || startDateError || timePeriodError)
                    throw new Exception();
                if (CreateProfileError != null) CreateProfileError.Content = "";

                ProfileManager.Instance.CreateProfile(borrowerName, principalAmount, sanctionedAmount, interestRate, startDate, timePeriod);
                NavigationService.GoBack();
                MainWindow.Instance?.NotifySuccess($"Created New Profile", new(0, 0, 2));
            }
            catch
            {
                CreateProfileError.Content = "Please Resolve Errors First";
            }
        }
    }
}
