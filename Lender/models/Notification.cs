using System;
using System.Windows.Media;

namespace Lender.Models
{
    public enum NotificationType
    {
        Error, Warning, Infomation, Success
    }

    public class NotificationColor
    {
        public SolidColorBrush Background { get; set; }
        public SolidColorBrush Foreground { get; set; }
        public SolidColorBrush Border { get; set; }

        public NotificationColor(Color background, Color foreground, Color border)
        {
            Background = new SolidColorBrush(background);
            Foreground = new SolidColorBrush(foreground);
            Border = new SolidColorBrush(border);
        }
    }

    public class Notification
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public NotificationType Type { get; set; }

        public NotificationColor ColorScheme { get; set; }

        public Notification(string id, NotificationType type, string text)
        {
            Id = id;
            Type = type;
            Text = text;
            ColorScheme = GetNotificationColor(type);
        }

        public static NotificationColor GetNotificationColor(NotificationType type)
        {
            return type switch
            {
                NotificationType.Error => new(Color.FromArgb(255, 193, 18, 32), Color.FromArgb(255, 253, 240, 213), Color.FromArgb(255, 120, 0, 0)),
                NotificationType.Warning => new(Color.FromArgb(255, 255, 238, 50), Color.FromArgb(255, 51, 53, 51), Color.FromArgb(255, 255, 209, 0)),
                NotificationType.Infomation => new(Color.FromArgb(255, 244, 242, 238), Color.FromArgb(255, 1, 22, 39), Color.FromArgb(255, 128, 128, 128)),
                NotificationType.Success => new(Color.FromArgb(255, 167, 201, 87), Color.FromArgb(255, 62, 54, 63), Color.FromArgb(255, 106, 153, 78)),
                _ => throw new NotificationError("Invalid Notifcation Type"),
            };
        }
    }

    public class NotificationError : Exception
    {
        public NotificationError(string message) : base(message) { }
    }
}
