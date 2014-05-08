using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace CiberControlCore
{
   public class Session : INotifyPropertyChanged
    {
        bool _active = false;
        int _minutes = 0;
        int _minutesLeft = 0;
        Timer currentTimer = new Timer();
        Guid uid = new Guid();
        public Session(int minutes)
        {
            _minutes = _minutesLeft = minutes;
            currentTimer.Interval = 60000;
#if DEBUG
            currentTimer.Interval /=10;
#endif

            currentTimer.AutoReset = true;
            currentTimer.Elapsed += minuteElapsed;
            currentTimer.Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<Session> SessionFinished;

        public delegate void SessionFinishedHandler(object sender, EventArgs e);

        public bool Active
        {
            get { return _active; }
            set { _active = value;
            OnPropertyChanged("Active");
            }
        }
        public int Minutes
        {
            get { return _minutes; }
            private set { _minutes = value;
            OnPropertyChanged("Minutes");
            }
        }
        public int MinutesLeft
        {
            get { return _minutesLeft; }
            private set { _minutesLeft = value;
            OnPropertyChanged("MinutesLeft");
            }
        }
        public void addMinutes(int howMany)
        {
            Minutes += howMany;
            MinutesLeft += howMany;
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
                SessionFinished(this,this);
                
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
