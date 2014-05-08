using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiberControlCore
{
    class Client : INotifyPropertyChanged
    {
        Guid _uid = new Guid();
        bool _active;
        Session _currentSession;
        ControlCore core = CiberControlCore.ControlCore.getInstance();
        System.Net.IPAddress _ip;
        String _hostName="";

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
