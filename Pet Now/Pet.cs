using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace Pet_Now
{
    public class Pet : INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }
        private string type;
        public string Type
        {
            get { return type; }
            set
            {
                if (type != value)
                {
                    type = value;
                    NotifyPropertyChanged("Type");
                }
            }
        }

        private DateTime birthday;
        public DateTime Birthday
        {
            get
            {
                return birthday;
            }
            set
            {
                if (birthday != value)
                {
                    birthday = value;
                    NotifyPropertyChanged("Birthday");
                }
            }
        }
        public string Age
        {
            get
            {
                TimeSpan age = DateTime.Now - birthday;
                return string.Format("{0}天", age.Days);
            }
        }

        private string weight;
        public string Weight
        {
            get { return weight; }
            set
            {
                if (weight != value)
                {
                    weight = value;
                    NotifyPropertyChanged("Weight");
                }
            }
        }

        private string hobby;
        public string Hobby
        {
            get { return hobby; }
            set
            {
                if (hobby != value)
                {
                    hobby = value;
                    NotifyPropertyChanged("Hobby");
                }
            }
        }

        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
