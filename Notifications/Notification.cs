using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Notifications
{
    public class Notification : INotifyPropertyChanged
    {
        public enum TimeOptions
        {
            _0, _30, _60
        }
        private TimeOptions _selectedOption = TimeOptions._0;
        private bool clicked = false;
        private bool hasOptions = false;
        public bool HasOptions
        {
            get { return hasOptions; }
            set { hasOptions = value; }
        }
        public bool Clicked
        {
            get { return clicked; }
            set { clicked = value;
            OnPropertyChanged("Clicked");
            }
        }
        public TimeOptions SelectedOption
        {
            get { return _selectedOption; }
            set { _selectedOption = value; }
        }
        private string message;
        public string Message
        {
            get { return message; }

            set
            {
                if (message == value) return;
                message = value;
                OnPropertyChanged("Message");
            }
        }

        private int id;
        public int Id
        {
            get { return id; }

            set
            {
                if (id == value) return;
                id = value;
                OnPropertyChanged("Id");
            }
        }

        private string imageUrl;
        public string ImageUrl
        {
            get { return imageUrl; }

            set
            {
                if (imageUrl == value) return;
                imageUrl = value;
                OnPropertyChanged("ImageUrl");
            }
        }

        private string title;
        public string Title
        {
            get { return title; }

            set
            {
                if (title == value) return;
                title = value;
                OnPropertyChanged("Title");
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class Notifications : ObservableCollection<Notification> { }
}