using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Business.Layer;
using Business.Layer.Model;
using Logic.Layer.Contracts;
using UI.Layer.Commands;
using System.Windows;

namespace UI.Layer.ViewModel
{
    public class TransportViewModel : NotifyPropertyChanged
    {
        private readonly IHourLogic hourLogic;
        private readonly IDriverLogic driverLogic;
        public ICommand DeleteCommand { get; }

        public TransportViewModel(IHourLogic hourLogic, IDriverLogic driverLogic) : base ()
        {
            this.hourLogic = hourLogic;
            this.driverLogic = driverLogic;
            DayOfWeek = (EDayOfWeek)DateTime.Now.DayOfWeek;
            Drivers = new ObservableCollection<Driver>(driverLogic.ToList((int)DayOfWeek));
            DeleteCommand = new RelayCommand(c => {
                Driver driver = c as Driver;
                DeleteTransport(driver);
            }, c => HourSelected != null);
        }

        public ObservableCollection<Driver> Drivers { get; set; }

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
                Drivers = new ObservableCollection<Driver>(driverLogic.ToList((int)value));
                OnNotifyPropertyChanged("Drivers");
            }
        }

        void DeleteTransport(Driver driver)
        {
            MessageBoxResult result = MessageBox.Show("¿Está seguro que desea eliminar la asignación?", "Atención", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (driver.Equals(HourSelected.EntryDriver))
                        HourSelected.EntryDriver = null;
                    if (driver.Equals(HourSelected.ExitDriver))
                        HourSelected.ExitDriver = null;

                    if (hourLogic.Modify(HourSelected))
                    {
                        driver.Hours.Remove(HourSelected);
                        HourSelected = null;
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar la asignación", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}