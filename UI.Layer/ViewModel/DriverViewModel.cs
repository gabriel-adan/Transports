using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Business.Layer;
using Business.Layer.Model;
using Logic.Layer.Contracts;
using UI.Layer.Commands;

namespace UI.Layer.ViewModel
{
    public class DriverViewModel : NotifyPropertyChanged
    {
        public ICommand AddCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        private readonly IDriverLogic driverLogic;

        public DriverViewModel(IDriverLogic driverLogic) : base()
        {
            this.driverLogic = driverLogic;
            Drivers = new ObservableCollection<Driver>(driverLogic.ToList());
            AddCommand = new RelayCommand(c => AddDriver(), c => !string.IsNullOrEmpty(DriverName) && DriverSelected == null);
            UpdateCommand = new RelayCommand(c => UpdateDriver(), c => DriverSelected != null && !string.IsNullOrEmpty(DriverName));
            CancelCommand = new RelayCommand(c => {
                DriverSelected = null;
                DriverName = string.Empty;
            });
            DeleteCommand = new RelayCommand(c => DeleteDriver(), c => DriverSelected != null);
        }

        public ObservableCollection<Driver> Drivers { get; set; }

        private string _driverName;
        public string DriverName
        {
            get { return _driverName; }
            set
            {
                _driverName = value;
                OnNotifyPropertyChanged("DriverName");
            }
        }

        private Driver _driverSelected;
        public Driver DriverSelected
        {
            get { return _driverSelected; }
            set
            {
                _driverSelected = value;
                OnNotifyPropertyChanged("DriverSelected");
                if (value != null)
                    DriverName = value.Name;
                else
                    DriverName = string.Empty;
            }
        }

        void AddDriver()
        {
            Driver driver = driverLogic.Create(DriverName);
            if (driverLogic.Save(driver))
            {
                DriverName = string.Empty;
                Drivers.Add(driver);
                MessageBox.Show("Chofer Agregado exitosamente", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("No se pudo registrar el Chofer", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        void UpdateDriver()
        {
            try
            {
                DriverSelected.Name = DriverName;
                if (driverLogic.Modify(DriverSelected))
                {
                    DriverSelected = null;
                    DriverName = string.Empty;
                    MessageBox.Show("Chofer modificado exitosamente", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("No se pudo modificar el Chofer", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        void DeleteDriver()
        {
            MessageBoxResult result = MessageBox.Show("¿Está seguro que desea eliminar el chofer?", "Atención", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (driverLogic.Remove(DriverSelected))
                    {
                        Drivers.Remove(DriverSelected);
                        DriverSelected = null;
                        DriverName = string.Empty;
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el Chofer", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}