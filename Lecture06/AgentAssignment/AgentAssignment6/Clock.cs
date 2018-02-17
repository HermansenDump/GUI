using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AgentAssignment6.Annotations;

namespace AgentAssignment6
{
    public class Clock : INotifyPropertyChanged
    {
        private string date;
        private string time;

        public Clock()
        {
            Update();
        }

        public void Update()
        {
            Date = DateTime.Now.ToLongDateString();
            Time = DateTime.Now.ToLongTimeString();
        }

        public string Date
        {
            get
            {
                return date;

            }
            private set {
                if (date != value)
                {
                    date = value;
                    Notify("Date");
                }
            }
        }

        public string Time
        {
            get
            {
                return time;

            }
            private set
            {
                if (time != value)
                {
                    time = value;
                    Notify("Time");
                }
            }
        }

        private void Notify(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
