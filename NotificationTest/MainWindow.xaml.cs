using Notifications;
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

namespace NotificationTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        readonly Notifications.NotificationsCore _Notifications = new Notifications.NotificationsCore();
        public MainWindow()
        {

            InitializeComponent();
            
        }

        private void wButtonClick(object sender, RoutedEventArgs e)
        {
            Notification test = new Notification { Title = "Aviso de Tiempo", ImageUrl = "pack://application:,,,/Resources/notification-icon.png", Message = "Estimado usuario.\nLe quedan 30 minutos.",HasOptions=true };
            test.PropertyChanged += (Esender, Ee) =>
            {
                if (Ee.PropertyName == "Clicked")
                    MessageBox.Show(test.SelectedOption.ToString());
            };
            _Notifications.AddNotification(test);
        }

        private void woButtonsClick(object sender, RoutedEventArgs e)
        {
            Notification test = new Notification { Title = "Aviso de Tiempo", ImageUrl = "pack://application:,,,/Resources/notification-icon.png", Message = "Estimado usuario.\nLe quedan 30 minutos." };
            test.PropertyChanged += (Esender, Ee) =>
            {
                if (Ee.PropertyName == "Clicked")
                    MessageBox.Show(test.SelectedOption.ToString());
            };
            _Notifications.AddNotification(test);
        }
    }
}
