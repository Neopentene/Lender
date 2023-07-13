using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;

namespace Lender.Models
{
    public class Overview : INotifyPropertyChanged
    {
        public XElement XElement { get; set; }

        public double SanctionedAmount { get; }

        public double InterestRate { get; }

        public double PrincipalAmount { get; }

        public int TimePeriod { get; }

        private double _totalDisbursedAmount;
        public double TotalDisbusedAmount
        {
            get { return _totalDisbursedAmount; }
            private set
            {
                if (value > SanctionedAmount) throw new PaymentError("Disbursement cannot exceed Sanctioned Amount.");
                _totalDisbursedAmount = value;
                XElement.Descendants(nameof(TotalDisbusedAmount)).FirstOrDefault()?.SetValue(value);
                OnPropertyChanged(nameof(TotalDisbusedAmount));
            }
        }

        private double _totalRepaidAmount;
        public double TotalRepaidAmount
        {
            get { return _totalRepaidAmount; }
            private set
            {
                if (value > _totalDisbursedAmount) throw new PaymentError("Cannot repay more than disbursement.");
                _totalRepaidAmount = value;
                XElement.Descendants(nameof(TotalRepaidAmount)).FirstOrDefault()?.SetValue(value);
                OnPropertyChanged(nameof(TotalRepaidAmount));
            }
        }

        private double _totalInterestAmount;
        public double TotalInterestAmount
        {
            get { return _totalInterestAmount; }
            set
            {
                _totalInterestAmount = value;
                XElement.Descendants(nameof(TotalInterestAmount)).FirstOrDefault()?.SetValue(value);
                OnPropertyChanged(nameof(TotalInterestAmount));
            }
        }

        private double _finalOutstanding = 0;
        public double FinalOutstanding
        {
            get { return _finalOutstanding; }
            set
            {
                _finalOutstanding = value;
                XElement.Descendants(nameof(FinalOutstanding)).FirstOrDefault()?.SetValue(value);
                OnPropertyChanged(nameof(FinalOutstanding));
            }
        }

        private bool _finalized = false;
        public bool Finalized
        {
            get { return _finalized; }
            private set
            {
                _finalized = value;
                XElement.Descendants(nameof(Finalized)).FirstOrDefault()?.SetValue(value);
                OnPropertyChanged(nameof(Finalized));
            }
        }

        public Overview(XElement element)
        {
            SanctionedAmount = double.Parse(element.Descendants(nameof(SanctionedAmount)).FirstOrDefault()?.Value ?? "0");
            InterestRate = double.Parse(element.Descendants(nameof(InterestRate)).FirstOrDefault()?.Value ?? "0");
            PrincipalAmount = double.Parse(element.Descendants(nameof(PrincipalAmount)).FirstOrDefault()?.Value ?? "0");
            TimePeriod = int.Parse(element.Descendants(nameof(TimePeriod)).FirstOrDefault()?.Value ?? "1");

            _totalInterestAmount = double.Parse(element.Descendants(nameof(TotalInterestAmount)).FirstOrDefault()?.Value ?? "0");
            _totalDisbursedAmount = double.Parse(element.Descendants(nameof(TotalDisbusedAmount)).FirstOrDefault()?.Value ?? "0");
            _totalRepaidAmount = double.Parse(element.Descendants(nameof(TotalRepaidAmount)).FirstOrDefault()?.Value ?? "0");
            _finalOutstanding = double.Parse(element.Descendants(nameof(FinalOutstanding)).FirstOrDefault()?.Value ?? "0");

            _finalized = bool.Parse(element.Descendants(nameof(Finalized)).FirstOrDefault()?.Value ?? "false");

            XElement = element;
        }

        public Overview(double principalAmount, double sanctionedAmount, double interestRate, int timePeriod)
        {
            SanctionedAmount = sanctionedAmount;
            InterestRate = interestRate;
            PrincipalAmount = principalAmount;
            TimePeriod = timePeriod;

            XElement = new XElement(nameof(Overview),
                new XElement(nameof(SanctionedAmount), SanctionedAmount),
                new XElement(nameof(PrincipalAmount), PrincipalAmount),
                new XElement(nameof(InterestRate), InterestRate),
                new XElement(nameof(TimePeriod), TimePeriod),
                new XElement(nameof(TotalDisbusedAmount), TotalDisbusedAmount),
                new XElement(nameof(TotalRepaidAmount), TotalRepaidAmount),
                new XElement(nameof(TotalInterestAmount), TotalInterestAmount),
                new XElement(nameof(FinalOutstanding), FinalOutstanding),
                new XElement(nameof(Finalized), Finalized));
        }

        public void Disburse(double amount)
        {
            if (amount < 0) throw new PaymentError("Negative amount not allowed");
            TotalDisbusedAmount += amount;
        }

        public void Repay(double amount)
        {
            if (amount < 0) throw new PaymentError("Negative amount not allowed");
            TotalRepaidAmount += amount;
        }

        public void RemoveDisbursement(double amount)
        {
            if (amount < 0) throw new PaymentError("Negative amount not allowed");
            if (TotalDisbusedAmount - amount < TotalRepaidAmount) throw new PaymentError("Disbursement has been Repaid");
            if (TotalDisbusedAmount - amount < 0) throw new PaymentError("Amount Exceeds Disbursed Amount");
            TotalDisbusedAmount -= amount;
        }

        public void RemoveRepayment(double amount)
        {
            if (amount < 0) throw new PaymentError("Negative amount not allowed");
            if (TotalRepaidAmount - amount < 0) throw new PaymentError("Amount Exceeds Repaid Amount");
            TotalRepaidAmount -= amount;
        }

        public void Finalize(double finalOutstanding, double totalInterestRate)
        {
            FinalOutstanding = finalOutstanding;
            TotalInterestAmount = totalInterestRate;
            Finalized = true;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
