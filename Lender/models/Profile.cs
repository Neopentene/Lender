using Lender.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;

namespace Lender.Models
{
    public class Profile : INotifyPropertyChanged
    {
        public XElement XElement { get; set; }

        public string LoanId { get; set; }

        public string BorrowerName { get; set; }

        public Overview Overview { get; set; }

        public ObservableCollection<Payment> Payments { get; private set; }

        public DateTime LoanStartDate { get; set; }

        public Profile(XElement element)
        {
            if (element.Name != nameof(Profile)) throw new InvalidProfile("No profile found.");
            XElement loanId = element.Descendants(nameof(LoanId)).FirstOrDefault() ?? throw new InvalidProfile("Profile missing loan id.");
            XElement borrowerName = element.Descendants(nameof(BorrowerName)).FirstOrDefault() ?? throw new InvalidProfile("Profile missing borrower name.");
            XElement overview = element.Descendants(nameof(Overview)).FirstOrDefault() ?? throw new InvalidProfile("Profile missing overiview.");
            XElement payments = element.Descendants(nameof(Payments)).FirstOrDefault() ?? throw new InvalidProfile("Profile missing payments.");
            XElement loanStartDate = element.Descendants(nameof(LoanStartDate)).FirstOrDefault() ?? throw new InvalidProfile("Profile missing start date.");


            LoanId = loanId.Value;
            BorrowerName = borrowerName.Value;
            Overview = new Overview(overview);
            Payments = new(payments.Elements(nameof(Payment)).Select(payment => PaymentTypeResolver.FromXElement(payment)));
            LoanStartDate = DateTime.Parse(loanStartDate.Value);

            SortPayments();

            XElement = element;
        }

        public Profile(string name, double principalAmount, double sanctionedAmount, double interestRate, DateTime startDate, int timePeriod)
        {
            BorrowerName = name;
            LoanId = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
            Payments = new();
            LoanStartDate = startDate;

            Overview = new(principalAmount, sanctionedAmount, interestRate, timePeriod);

            XElement = new XElement(nameof(Profile),
                new XElement(nameof(LoanId), LoanId),
                new XElement(nameof(BorrowerName), name),
                new XElement(nameof(LoanStartDate), startDate),
                new XElement(nameof(Overview), Overview.XElement),
                new XElement(nameof(Payments)));
        }

        private void AddPaymentInOrder(Payment payment)
        {
            for (int i = 0; i < Payments.Count; i++)
            {
                if (payment.CompareTo(Payments[i]) < 0)
                {
                    Payments.Insert(i, payment);
                    return;
                }
            }
            Payments.Add(payment);
        }

        public void SortPayments()
        {
            List<Payment> payments = Payments.ToList();
            payments.Sort((a, b) => a.CompareTo(b));
            Payments = new(payments);
        }

        public void AddDisbursementNow(double amount)
        {
            AddDisbursement(DateTime.UtcNow, amount);
        }

        public void AddRepaymentNow(double amount)
        {
            AddRepayment(DateTime.UtcNow, amount);
        }

        public void AddDisbursement(DateTime date, double amount)
        {
            Disbursement disbursement = new(date, amount);
            Overview.Disburse(amount);
            AddPaymentInOrder(disbursement);
            XElement.Descendants(nameof(Payments)).FirstOrDefault()?.Add(disbursement.XElement);
            OnPropertyChanged(nameof(Payments));
        }

        public void AddRepayment(DateTime date, double amount)
        {
            Repayment repayment = new(date, amount);
            Overview.Repay(amount);
            AddPaymentInOrder(repayment);
            XElement.Descendants(nameof(Payments)).FirstOrDefault()?.Add(repayment.XElement);
            OnPropertyChanged(nameof(Payments));
        }

        public double GetDisbursedAmountTillDate(DateTime date)
        {
            double amount = 0;
            foreach(var payment in Payments)
            {
                if (payment.Date > date) break;
                if (payment is not Disbursement disbursement) continue;
                amount += disbursement.Amount;
            }
            return amount;
        }

        public void RemovePayment(string paymentId)
        {
            double disbursement = 0;
            double repayment = 0;

            for (int i = 0; i < Payments.Count; i++)
                if (Payments[i].PaymentId == paymentId)
                {
                    if (Payments[i].Type == PaymentType.Disbursement)
                    {
                        //if (repayment > disbursement - Payments[i].Amount) throw new PaymentError("Disbursement has been repaid");
                        Overview.RemoveDisbursement(Payments[i].Amount);
                    }
                    else
                    {
                        Overview.RemoveRepayment(Payments[i].Amount);
                    }

                    if (Payments[i].XElement.Parent is not null) Payments[i].XElement.Remove();
                    Payments.RemoveAt(i);
                    break;
                }
                else
                {
                    switch(Payments[i].Type)
                    {
                        case PaymentType.Disbursement: disbursement += Payments[i].Amount; break;
                        case PaymentType.Repayment: repayment += Payments[i].Amount; break;
                    }
                }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ProfileManager : DataStore.IReadable<List<Profile>>, DataStore.IWritable
    {

        #region Singleton Logic

        private static ProfileManager? instance;

        private static readonly object semaphore = new();

        public static ProfileManager Instance
        {
            get
            {
                lock (semaphore)
                {
                    instance ??= new ProfileManager();
                    return instance;
                }
            }
        }

        private ProfileManager() { }

        #endregion Singleton Logic

        public const string DEFAULT_FILE_NAME = "profiles.xml";

        public ObservableCollection<Profile> Profiles { get; private set; } = new();

        public void CreateProfile(Profile profile)
        {
            Profiles.Add(profile);
        }

        public void CreateProfile(string name, double principalAmount, double sanctionedAmount, double interestRate, DateTime startDate, int timePeriod)
        {
            Profile newProfile = new(name, principalAmount, sanctionedAmount, interestRate, startDate, timePeriod);
            Profiles.Add(newProfile);
        }

        public Profile? GetProfile(string loanId)
        {
            return Profiles.FirstOrDefault(profiles => profiles.LoanId.Equals(loanId));
        }

        public void RemoveProfile(string loanId)
        {
            for (int i = 0; i < Profiles.Count; i++)
            {
                if (Profiles[i].LoanId.Equals(loanId))
                {
                    if (Profiles[i].XElement.Parent != null)
                        Profiles[i].XElement.Remove();

                    Profiles.RemoveAt(i);
                    break;
                }
            }
        }

        public List<Profile> Read(in XDocument document)
        {
            if (document.Root == null) throw new DataStoreError("Document Root not found");
            if (!document.Root.Name.LocalName.Equals("Document")) throw new DataStoreError("Root not named Document");

            Profiles.Clear();

            List<Profile> result = document.Root.Elements(nameof(Profile)).Select(x => new Profile(x)).ToList();
            result.ForEach(Profiles.Add);

            return result;
        }

        public void Write(in XDocument document)
        {
            foreach (var profile in Profiles)
                if (profile.XElement.Parent == null)
                    document.Root?.Add(profile.XElement);
        }
    }

    public class InvalidProfile : Exception
    {
        public InvalidProfile(string message) : base(message) { }
    }
}
