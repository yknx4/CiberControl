using Notifications;
using System;
using System.ComponentModel;
using System.Windows;

namespace CiberControlCore
{
    public class ControlCore : INotifyPropertyChanged
    {
        private static ResourceDictionary resource = new ResourceDictionary
        {
            Source = new Uri(@"\Resources\Icons.xaml", UriKind.Relative)
        };

        public void DispatchIfNecessary(Action action)
        {
            if (!Application.Current.Dispatcher.CheckAccess())
                Application.Current.Dispatcher.Invoke(action);
            else
                action.Invoke();
        }

        private NotificationsCore _notify = new NotificationsCore();
        private Session currentSession;

        public Session CurrentSession
        {
            set
            {
                currentSession = value;
                OnPropertyChanged("CurrentSession");
            }
            get { return currentSession; }
        }

        private ControlCore()
        {
            Notification test2 = new Notification { Title = "Ciber Control", ImageUrl = "pack://application:,,,/Resources/notification-icon.png", Message = "Control de ciber inicializado.", HasOptions = false };
            notify(test2);
            currentSession = new Session(30);
            currentSession.PropertyChanged += (obj, earg) =>
            {
                var source = obj as Session;
                if (earg.PropertyName == "MinutesLeft")
                {
                    switch (source.MinutesLeft)
                    {
                        case 10:
                            Notification test = new Notification { Title = "Aviso de Tiempo", ImageUrl = "pack://application:,,,/Resources/warning2.png", Message = "Estimado usuario.\nLe quedan 10 minutos.", HasOptions = false };
                            notify(test);
                            break;

                        case 5:
                            Notification test3 = new Notification { Title = "Aviso de Tiempo", ImageUrl = "pack://application:,,,/Resources/exclamation3.png", Message = "Estimado usuario.\nLe quedan 5 minutos. Desea mas tiempo?", HasOptions = true };
                            test3.PropertyChanged += (Esender, Ee) =>
                            {
                                if (Ee.PropertyName == "Clicked")
                                {
                                    switch (test3.SelectedOption)
                                    {
                                        case Notification.TimeOptions._30:
                                            currentSession.addMinutes(30);
                                            notifyNewTime(30);
                                            break;

                                        case Notification.TimeOptions._60:
                                            currentSession.addMinutes(60);
                                            notifyNewTime(60);
                                            break;
                                    }
                                }
                            };

                            notify(test3);

                            break;
                    }
                }
            };
        }

        private void notifyNewTime(int minutes)
        {
            Notification test3 = new Notification { Title = "Aviso de Tiempo", ImageUrl = "pack://application:,,,/Resources/exclamation3.png", Message = "Estimado usuario.\nUsted ha añadido " + minutes.ToString() + " minutos.\n Gracias por su preferencia.", HasOptions = false };
            notify(test3);
        }

        private void notify(Notification toNotify)
        {
            DispatchIfNecessary(() =>
            {
                _notify.AddNotification(toNotify);
            });
        }

        private static ControlCore _singleton = null;

        public static ControlCore getInstance()
        {
            if (_singleton == null) _singleton = new ControlCore();
            return _singleton;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}