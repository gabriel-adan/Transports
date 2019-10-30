using System;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Business.Layer;
using Business.Layer.Model;
using Logic.Layer.Contracts;
using UI.Layer.Commands;

namespace UI.Layer.ViewModel
{
    public class HourAssignmentViewModel : NotifyPropertyChanged
    {
        private readonly IHourLogic hourLogic;
        private readonly IDriverLogic driverLogic;
        public ICommand CancelTransportCommand { get; }
        public ICommand DeleteTransportCommand { get; }

        public HourAssignmentViewModel(IHourLogic hourLogic, IDriverLogic driverLogic) : base ()
        {
            this.hourLogic = hourLogic;
            this.driverLogic = driverLogic;
            Drivers = new ObservableCollection<Driver>();
            HourList = new ObservableCollection<Hour>();
            DayOfWeek = (EDayOfWeek)DateTime.Now.DayOfWeek;

            CancelTransportCommand = new RelayCommand(c => {
                Hour hour = c as Hour;
                hour.IsCanceled = !hour.IsCanceled;
                Driver entryDriver = hour.EntryDriver;
                Driver exitDriver = hour.ExitDriver;
                if (hour.IsCanceled)
                {
                    hour.EntryDriver = null;
                    hour.ExitDriver = null;
                }
                if (!hourLogic.Modify(hour))
                {
                    hour.IsCanceled = !hour.IsCanceled;
                    hour.EntryDriver = entryDriver;
                    hour.ExitDriver = exitDriver;
                }
                StateCount();
            });
        }

        public ObservableCollection<Driver> Drivers { get; set; }
        public ObservableCollection<Hour> HourList { get; set; }

        Hour _hourSelected;
        public Hour HourSelected
        {
            get { return _hourSelected; }
            set
            {
                _hourSelected = value;
                OnNotifyPropertyChanged("HourSelected");
            }
        }

        private EDayOfWeek _dayOfWeek;
        public EDayOfWeek DayOfWeek
        {
            get { return _dayOfWeek; }
            set
            {
                _dayOfWeek = value;
                OnNotifyPropertyChanged("DayOfWeek");
                BackgroundWorker workerHours = new BackgroundWorker();
                workerHours.WorkerReportsProgress = true;
                workerHours.DoWork += delegate (object sender, DoWorkEventArgs e)
                {
                    HourList = new ObservableCollection<Hour>(hourLogic.ListByDayOfWeek((int)value));
                    OnNotifyPropertyChanged("HourList");
                    Drivers = new ObservableCollection<Driver>(driverLogic.ToList((int)value));
                    OnNotifyPropertyChanged("Drivers");
                    StateCount();
                };
                workerHours.RunWorkerCompleted += new RunWorkerCompletedEventHandler(delegate (object sender, RunWorkerCompletedEventArgs e)
                {
                    workerHours.Dispose();
                });
                workerHours.RunWorkerAsync();
            }
        }

        int assignedCount;
        public int AssignedCount
        {
            get { return assignedCount; }
            set
            {
                assignedCount = value;
                OnNotifyPropertyChanged("AssignedCount");
            }
        }

        int incompletedCount;
        public int IncompletedCount
        {
            get { return incompletedCount; }
            set
            {
                incompletedCount = value;
                OnNotifyPropertyChanged("IncompletedCount");
            }
        }

        int canceledCount;
        public int CanceledCount
        {
            get { return canceledCount; }
            set
            {
                canceledCount = value;
                OnNotifyPropertyChanged("CanceledCount");
            }
        }

        int notAssignedCount;
        public int NotAssignedCount
        {
            get { return notAssignedCount; }
            set
            {
                notAssignedCount = value;
                OnNotifyPropertyChanged("NotAssignedCount");
            }
        }

        public void AddEntryDriver(Driver entryDriver)
        {
            if (HourSelected != null)
            {
                if (entryDriver != null)
                {
                    HourSelected.EntryDriver = entryDriver;
                    if (!entryDriver.Equals(HourSelected.ExitDriver))
                    {
                        entryDriver.Hours.Add(HourSelected);
                    }
                    hourLogic.Modify(HourSelected);
                    StateCount();
                }
            }
        }

        public void AddExitDriver(Driver exitDriver)
        {
            if (HourSelected != null)
            {
                if (exitDriver != null)
                {
                    HourSelected.ExitDriver = exitDriver;
                    if (!exitDriver.Equals(HourSelected.EntryDriver))
                    {
                        exitDriver.Hours.Add(HourSelected);
                    }
                    hourLogic.Modify(HourSelected);
                    StateCount();
                }
            }
        }

        void StateCount()
        {
            AssignedCount = 0;
            IncompletedCount = 0;
            CanceledCount = 0;
            NotAssignedCount = 0;
            foreach (Hour hour in HourList)
            {
                if (hour.IsCompleted())
                    AssignedCount++;
                if (hour.IsInCompleted())
                    IncompletedCount++;
                if (hour.IsCanceled)
                    CanceledCount++;
                if (hour.IsNotAssigned() && !hour.IsCanceled)
                    NotAssignedCount++;
            }
        }
    }
}