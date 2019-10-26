using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Business.Layer.Model
{
    public class Customer : NotifyPropertyChanged
    {
        public long Id { get; set; }
        string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnNotifyPropertyChanged("Name");
            }
        }

        string address;
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnNotifyPropertyChanged("Address");
            }
        }

        public IList<Hour> Hours { get; set; }

        public Customer() : base()
        {
            Hours = new ObservableCollection<Hour>();
        }
    }
}
