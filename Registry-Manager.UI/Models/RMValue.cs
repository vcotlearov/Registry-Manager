using System.ComponentModel;

namespace Registry_Manager.UI.Models
{
    public class RMValue : INotifyPropertyChanged
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

        private string _type;
        public string Type
        {
            get
            {
                return this._type;
            }
            set
            {
                if (value != this._type)
                {
                    this._type = value;
                    NotifyPropertyChanged("Type");
                }
            }
        }

        private string _data;
        public string Data
        {
            get
            {
                return this._data;
            }
            set
            {
                if (value != this._data)
                {
                    this._data = value;
                    NotifyPropertyChanged("Data");
                }
            }
        }

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
