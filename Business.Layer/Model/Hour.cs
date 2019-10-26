using System;

namespace Business.Layer.Model
{
    public class Hour : NotifyPropertyChanged
    {
        TimeSpan _entryTime;
        TimeSpan _exitTime;
        bool _isCanceled;

        Driver _entryDriver;
        Driver _exitDriver;

        public string Place { get; set; }

        public TimeSpan EntryTime
        {
            get { return _entryTime; }
            set
            {
                _entryTime = value;
                OnNotifyPropertyChanged("EntryTime");
            }
        }

        public TimeSpan ExitTime
        {
            get { return _exitTime; }
            set
            {
                _exitTime = value;
                OnNotifyPropertyChanged("ExitTime");
            }
        }

        public bool IsCanceled
        {
            get { return _isCanceled; }
            set
            {
                _isCanceled = value;
                OnNotifyPropertyChanged("IsCanceled");
            }
        }

        public int DayOfWeek { get; set; }

        public Customer Customer { get; set; }

        public Driver EntryDriver
        {
            get { return _entryDriver; }
            set
            {
                _entryDriver = value;
                OnNotifyPropertyChanged("EntryDriver");
            }
        }

        public Driver ExitDriver
        {
            get { return _exitDriver; }
            set
            {
                _exitDriver = value;
                OnNotifyPropertyChanged("ExitDriver");
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Hour))
                return false;

            Hour hour = (Hour)obj;
            if (hour.Customer == null || this.Customer == null)
                return false;
            return hour.Customer.Id == this.Customer.Id && hour.DayOfWeek == this.DayOfWeek && hour.EntryTime == this.EntryTime && hour.ExitTime == this.ExitTime;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool IsInCompleted()
        {
            return (_entryDriver != null && _exitDriver == null) || (_entryDriver == null && _exitDriver != null);
        }

        public bool IsCompleted()
        {
            return _entryDriver != null && _exitDriver != null;
        }

        public bool IsNotAssigned()
        {
            return _entryDriver == null && _exitDriver == null;
        }
    }
}
