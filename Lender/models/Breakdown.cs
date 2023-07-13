using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Lender.Models
{
    public class Amortization
    {
        public double DisbursedAmount { get; set; }

        public double RepaidAmount { get; set; }

        public double InterestAmount { get; set; }

        public double OutstandingAmount { get; set; }

        public DateTime StartDate { get; }

        public DateTime EndDate { get; set; }

        public Amortization(DateTime startDate)
        {
            StartDate = startDate;
        }
    }

    public enum AmortizationFrequency
    {
        Monthy = 1, Quaterly = 3, SemiAnnually = 6, Annually = 12, Bullet = 0
    }

    public class Breakdown : INotifyPropertyChanged
    {
        private double totalInterestValue = 0;
        public double TotalInterestValue
        {
            get
            {
                return totalInterestValue;
            }
            set
            {
                totalInterestValue = value;
                TotalInterestAmount = $"{value:C}";
                OnPropertyChanged(nameof(TotalInterestValue));
            }
        }

        private double finalOutstanding = 0;
        public double FinalOutstanding
        {
            get
            {
                return finalOutstanding;
            }
            set
            {
                finalOutstanding = value;
                FinalOutstandingAmount = $"{value:C}";
                OnPropertyChanged(nameof(FinalOutstanding));
            }
        }

        public string TotalInterestAmount { get; set; } = "Generate Breakdowns";

        public string FinalOutstandingAmount { get; set; } = "Generate Breakdowns";

        private Dictionary<DateTime, List<Payment>> PaymentsByDate { get; set; } = new();

        public ObservableCollection<Amortization> Amortizations { get; set; } = new();

        public void GeneratePaymentsByDate(Profile profile)
        {
            PaymentsByDate = new();

            foreach (Payment payment in profile.Payments)
            {
                if (PaymentsByDate.ContainsKey(payment.Date))
                {
                    PaymentsByDate[payment.Date].Add(payment);
                    continue;
                }
                PaymentsByDate.Add(payment.Date, new() { payment });
            }
        }

        public void GenerateAmortization(AmortizationFrequency frequency, Profile profile, int DCCValue = 365)
        {
            DateTime endDate = profile.LoanStartDate.AddYears(profile.Overview.TimePeriod);
            DateTime frequencyPeriodEnd = frequency == AmortizationFrequency.Bullet ? endDate : profile.LoanStartDate.AddMonths((int)frequency).AddDays(-1);

            Amortizations.Clear();

            double disbursedAmount = 0;
            double pastRepaidAmount = 0;
            double repaidAmount = 0;
            double interestAmountByFrequency = 0;

            totalInterestValue = 0;

            double dailyInterestRate = profile.Overview.InterestRate / DCCValue;

            Amortization currentAmortization = new(profile.LoanStartDate);

            GeneratePaymentsByDate(profile);

            for (var currentDate = profile.LoanStartDate; currentDate < endDate; currentDate = currentDate.AddDays(1))
            {
                if (PaymentsByDate.ContainsKey(currentDate))
                    PaymentsByDate[currentDate].ForEach(payment =>
                    {
                        switch (payment.Type)
                        {
                            case PaymentType.Disbursement: disbursedAmount += payment.Amount; break;
                            case PaymentType.Repayment: repaidAmount += payment.Amount; break;
                        };
                    });

                double interestAmount = (disbursedAmount - pastRepaidAmount) * dailyInterestRate / 100F;
                interestAmountByFrequency += interestAmount;
                totalInterestValue += interestAmount;

                if (currentDate == frequencyPeriodEnd || currentDate.AddDays(1) == endDate)
                {
                    currentAmortization.DisbursedAmount = disbursedAmount;
                    currentAmortization.RepaidAmount = pastRepaidAmount;
                    currentAmortization.InterestAmount = interestAmountByFrequency;
                    currentAmortization.OutstandingAmount = disbursedAmount - repaidAmount + totalInterestValue;
                    currentAmortization.EndDate = currentDate;
                    Amortizations.Add(currentAmortization);

                    interestAmountByFrequency = 0;

                    frequencyPeriodEnd = frequencyPeriodEnd.AddMonths((int)frequency);

                    currentAmortization = new(currentDate.AddDays(1));
                }

                pastRepaidAmount = repaidAmount;
            }

            TotalInterestValue = totalInterestValue;
            FinalOutstanding = disbursedAmount + totalInterestValue - pastRepaidAmount;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
