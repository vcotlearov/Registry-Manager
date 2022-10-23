using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Registry_Manager.UI.Models
{
    public class RMKey : INotifyPropertyChanged
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
        private string _templateName;
        public string TemplateName
        {
            get
            {
                return this._templateName;
            }
            set
            {
                if (value != this._templateName)
                {
                    this._templateName = value;
                    NotifyPropertyChanged("TemplateName");
                }
            }
        }
        public List<RMValue> Parameters { get; set; }

        public RMKey()
        {
            Parameters = new List<RMValue>();
        }

        protected void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}