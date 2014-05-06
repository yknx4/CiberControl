using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notifications;

namespace CiberControlCore
{
    public class ControlCore
    {
        private NotificationsCore _notify = new NotificationsCore();
        private ControlCore() { }

        private static ControlCore _singleton = null;
        public static ControlCore getInstance(){
            if (_singleton == null) _singleton = new ControlCore();
            return _singleton;
        }
    }
}
