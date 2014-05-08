using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace CiberControlCore
{
    /// <summary>
    /// This class represent a current session running on a lent PC
    /// </summary>
    public class Session : INotifyPropertyChanged
    {
        bool _active = false;
        int _minutes = 0;
        int _minutesLeft = 0;
        DateTime _startTime = new DateTime();
        Timer currentTimer = new Timer();
        Guid uid = new Guid();

        public Session(int minutes)
        {
            _minutes = _minutesLeft = minutes;
            currentTimer.Interval = 60000;
#if DEBUG
            currentTimer.Interval /= 10;
#endif

            currentTimer.AutoReset = true;
            currentTimer.Elapsed += minuteElapsed;
            currentTimer.Start();
            StartTime = DateTime.Now;
        }

        public delegate void SessionFinishedHandler(object sender, EventArgs e);

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<Session> SessionFinished;

        public bool Active
        {
            get { return _active; }
            set
            {
                _active = value;
                OnPropertyChanged("Active");
            }
        }

        public int Minutes
        {
            get { return _minutes; }
            private set
            {
                _minutes = value;
                OnPropertyChanged("Minutes");
            }
        }

        public int MinutesLeft
        {
            get { return _minutesLeft; }
            private set
            {
                _minutesLeft = value;
                OnPropertyChanged("MinutesLeft");
            }
        }

        public System.DateTime StartTime
        {
            get { return _startTime; }
            set
            {
                _startTime = value;
                OnPropertyChanged("StartTime");
            }
        }
        public System.DateTime EndTime
        {
            get { return _startTime.AddMinutes(Minutes); }
            
        }
        public void addMinutes(int howMany)
        {
            Minutes += howMany;
            MinutesLeft += howMany;
            OnPropertyChanged("EndTime");
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnSessionFinished()
        {
            if (SessionFinished != null)
            {
                EventArgs e = new EventArgs();
                SessionFinished(this, this);

            }
        }

        private void minuteElapsed(object sender, ElapsedEventArgs e)
        {
            if (MinutesLeft == 0)
            {
                Active = false;
                currentTimer.Stop();
                OnSessionFinished();
            }
            else MinutesLeft--;
        }
    }
}
