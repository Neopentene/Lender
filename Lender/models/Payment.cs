using System;
using System.Linq;
using System.Windows.Media;
using System.Xml.Linq;

namespace Lender.Models
{
    public enum PaymentType
    {
        Disbursement, Repayment
    }

    public abstract class Payment : IEquatable<Payment>, IComparable<Payment>
    {
        public abstract XElement XElement { get; set; }

        public string PaymentId { get; private set; }

        public DateTime Date { get; }

        public double Amount { get; }

        public PaymentType Type { get; }

        public virtual SolidColorBrush BrushColor { get; } = new SolidColorBrush(Color.FromArgb(255, 47, 56, 68));

        public Payment(string paymentId, DateTime date, double amount, PaymentType type)
        {
            PaymentId = paymentId;
            Date = date;
            Amount = amount;
            Type = type;
        }

        public bool Equals(Payment? other)
        {
            if (other == null) return false;
            return other.PaymentId == PaymentId;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Payment);
        }

        public override int GetHashCode()
        {
            return PaymentId.GetHashCode();
        }

        public int CompareTo(Payment? other)
        {
            if (other == null) return -1;
            int result = Date.CompareTo(other.Date);
            if (result == 0)
                return other.Type switch
                {
                    PaymentType.Disbursement => Type == PaymentType.Repayment ? 1 : Amount.CompareTo(other.Amount),
                    PaymentType.Repayment => Type == PaymentType.Disbursement ? -1 : Amount.CompareTo(other.Amount),
                    _ => Amount.CompareTo(other.Amount),
                };
            return result;
        }
    }

    public class Repayment : Payment
    {
        public override XElement XElement { get; set; }

        public override SolidColorBrush BrushColor { get; } = new SolidColorBrush(Color.FromArgb(255, 47, 68, 59));
        public Repayment(string paymentId, DateTime date, double amount) : base(paymentId, date, amount, PaymentType.Repayment)
        {
            XElement = new XElement(nameof(Payment),
                new XElement(nameof(PaymentId), paymentId),
                new XElement(nameof(DateTime), date),
                new XElement(nameof(Amount), amount),
                new XElement(nameof(PaymentType), PaymentType.Repayment));
        }

        public Repayment(DateTime date, double amount) : this(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString(), date, amount) { }
        public Repayment(double amount) : this(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString(), DateTime.UtcNow, amount) { }
    }

    public class Disbursement : Payment
    {
        public override XElement XElement { get; set; }

        public override SolidColorBrush BrushColor { get; } = new SolidColorBrush(Color.FromArgb(255, 68, 47, 56));

        public Disbursement(string paymentId, DateTime date, double amount) : base(paymentId, date, amount, PaymentType.Disbursement)
        {
            XElement = new XElement(nameof(Payment),
                new XElement(nameof(PaymentId), paymentId),
                new XElement(nameof(DateTime), date),
                new XElement(nameof(Amount), amount),
                new XElement(nameof(PaymentType), PaymentType.Disbursement));
        }
        public Disbursement(DateTime date, double amount) : this(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString(), date, amount) { }

        public Disbursement(double amount) : this(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString(), DateTime.UtcNow, amount) { }
    }

    public class PaymentTypeResolver
    {
        public static Payment FromXElement(XElement element)
        {
            if (element.Name != nameof(Payment)) throw new PaymentError("Not a Payment.");

            if (element.Descendants("PaymentId").FirstOrDefault()?.Value is not string paymentId)
                throw new PaymentError("Could not resolve Payment Id.");

            if (!Enum.TryParse(element.Descendants(nameof(PaymentType)).FirstOrDefault()?.Value, out PaymentType type))
                throw new PaymentError("Could not resolve Payment Type.");

            if (!DateTime.TryParse(element.Descendants(nameof(DateTime)).FirstOrDefault()?.Value, out DateTime date))
                throw new PaymentError("Could not resolve Date.");

            if (!double.TryParse(element.Descendants("Amount").FirstOrDefault()?.Value, out double amount))
                throw new PaymentError("Could not resolve Amount.");

            Payment payment = type switch
            {
                PaymentType.Repayment => new Repayment(paymentId, date, amount),
                PaymentType.Disbursement => new Disbursement(paymentId, date, amount),
                _ => throw new PaymentError("Missing Type."),
            };

            payment.XElement = element;

            return payment;
        }
    }

    public class PaymentError : Exception
    {
        public PaymentError(string message) : base(message) { }
    }
}
