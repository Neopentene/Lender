using Lender.Models;
using Lender.windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for AddPaymentPage.xaml
    /// </summary>
    public partial class AddPaymentPage : Page, IClosable
    {
        private readonly Profile profile;

        private double amount;
        private bool amountError = true;

        private double disbursementTillDate = 0;

        public event EventHandler? CloseWindow;

        public AddPaymentPage(Profile profile)
        {
            
            this.profile = profile;
            InitializeComponent();
            if (PaymentDate != null)
            { 
                PaymentDate.DisplayDateStart = profile.LoanStartDate;
                PaymentDate.DisplayDateEnd = profile.LoanStartDate.AddYears(profile.Overview.TimePeriod);
                PaymentDate.SelectedDate = profile.LoanStartDate;
            }
        }

        private void CreatePaymentButton_Click(object sender, RoutedEventArgs e)
        {
            if(amountError)
            {
                CreateButtonError.Content = "Please Resolve Errors";
                return;
            }

            if(amount == 0)
            {
                CreateButtonError.Content = "Please Enter Amount Greater Than Zero";
                return;
            }

            switch (PaymentTypeComboBox.Text)
            {
                case "Disbursement":
                    {
                        if (PaymentDate.SelectedDate is DateTime date) profile.AddDisbursement(date, amount);
                        else profile.AddDisbursementNow(amount);
                        CloseWindow?.Invoke(this, new EventArgs());
                        MainWindow.Instance?.NotifySuccess("Disbursement Accquired", new(0, 0, 4));
                        return;
                    }
                case "Repayment":
                    {
                        if(disbursementTillDate > amount) { CreateButtonError.Content = "Amount is Invalid"; }
                        if (PaymentDate.SelectedDate is DateTime date) profile.AddRepayment(date, amount);
                        else profile.AddRepaymentNow(amount);
                        CloseWindow?.Invoke(this, new EventArgs());
                        MainWindow.Instance?.NotifySuccess("Repayment Complete", new(0, 0, 4));
                        return;
                    }

            }
        }

        private void PaymentAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                amount = double.Parse(PaymentAmount.Text.Trim());

                if (PaymentTypeComboBox.Text.Equals("Repayment"))
                    if (disbursementTillDate < amount)
                        throw new OverflowException("Repayment");

                if (PaymentTypeComboBox.Text.Equals("Disbursement"))
                    if (amount > profile.Overview.SanctionedAmount - profile.Overview.TotalDisbusedAmount)
                        throw new OverflowException("Disbursement");

                if (PaymentAmountError != null) PaymentAmountError.Content = "";
                amountError = false;
                return;
            }
            catch (ArgumentNullException)
            {
                PaymentAmountError.Content = "Enter Amount";
            }
            catch (FormatException)
            {
                PaymentAmountError.Content = "Enter Valid Amount";
            }
            catch (OverflowException oe)
            {
                if(oe.Message == "Disbursement" || oe.Message == "Repayment")
                    PaymentAmountError.Content = $"{oe.Message} Too High";
                else
                    PaymentAmountError.Content = "Amount Too High";
            }

            amountError = true;
        }

        private void PaymentDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is null) return;
            if (PaymentDate.SelectedDate is not DateTime date) return;
            disbursementTillDate = profile.GetDisbursedAmountTillDate(date);
        }
    }
}
