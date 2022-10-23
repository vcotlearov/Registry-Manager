using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Registry_Manager.UI.Models
{
    public class RMGroup : INotifyPropertyChanged
    {
        private string _name;
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                if (value != this._name)
                {
                    this._name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        public List<RMRecord> Records { get; set; }

        public RMGroup()
        {
            Records = new List<RMRecord>();
        }

        protected void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
