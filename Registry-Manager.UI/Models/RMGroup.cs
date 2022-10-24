using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private string _keyPath;
        public string KeyPath
        {
            get
            {
                return this._keyPath;
            }
            set
            {
                if (value != this._keyPath)
                {
                    this._keyPath = value;
                    NotifyPropertyChanged("KeyPath");
                }
            }
        }

        public ObservableCollection<RMKey> Records { get; set; }

        public RMGroup()
        {
            Records = new ObservableCollection<RMKey>();
        }

        protected void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
