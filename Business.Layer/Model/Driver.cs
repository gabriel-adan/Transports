using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Business.Layer.Model
{
    public class Driver : NotifyPropertyChanged
    {
        public long Id { get; set; }
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnNotifyPropertyChanged("Name");
            }
        }

        public Driver() : base()
        {
            Hours = new ObservableCollection<Hour>();
        }

        public IList<Hour> Hours { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Driver))
                return false;

            return ((Driver)obj).Id == this.Id;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}