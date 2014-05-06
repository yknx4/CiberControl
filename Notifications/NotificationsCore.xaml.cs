using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Notifications
{
    public partial class NotificationsCore
    {
        private const double topOffset = 20;
        private const double leftOffset = 400;
        private const byte MAX_NOTIFICATIONS = 4;
        private int count;
        public Notifications Notifications = new Notifications();
        private readonly Notifications buffer = new Notifications();
        

        public NotificationsCore()
        {
            InitializeComponent();
            NotificationsControl.DataContext = Notifications;
            this.Top = SystemParameters.WorkArea.Top + topOffset;
            this.Left = SystemParameters.WorkArea.Left + SystemParameters.WorkArea.Width - leftOffset;
        }

        public void AddNotification(Notification notification)
        {
            notification.Id = count++;
            if (Notifications.Count + 1 > MAX_NOTIFICATIONS)
                buffer.Add(notification);
            else
                Notifications.Add(notification);
            
            //Show window if there're notifications
            if (Notifications.Count > 0 && !IsActive)
                Show();
        }

        public void RemoveNotification(Notification notification)
        {
            if (Notifications.Contains(notification))
                Notifications.Remove(notification);
            
            if (buffer.Count > 0)
            {
                Notifications.Add(buffer[0]);
                buffer.RemoveAt(0);
            }
            
            //Close window if there's nothing to show
            if (Notifications.Count < 1)
                Hide();
        }

        private void NotificationWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var element = sender as Grid;
            Notification firstNotif = Notifications.First(n => n.Id == Int32.Parse(element.Tag.ToString()));
            if (e.NewSize.Height != 0.0 && !firstNotif.Clicked)
                return;
            
            
            RemoveNotification(firstNotif);
        }

        private void handleNotificationClick(object sender, RoutedEventArgs e)
        {

            var butSource = sender as Control;
            var element = (StackPanel)butSource.Parent;
            var objSource = Notifications.First(n => n.Id == Int32.Parse(element.Tag.ToString()));
            int selectedOption = int.Parse(butSource.Tag.ToString());
            //butSource = butSource;
            
            switch (selectedOption)
            {
                case 0:
                    objSource.SelectedOption = Notification.TimeOptions._0;
                    break;
                case 30:
                    objSource.SelectedOption = Notification.TimeOptions._30;
                    break;
                case 60:
                    objSource.SelectedOption = Notification.TimeOptions._60;
                    break;
            }
            objSource.Clicked = true;
        }

        private void notificationCreated(object sender, RoutedEventArgs e)
        {
            
            
        }
    }
}
