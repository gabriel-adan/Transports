using Business.Layer;
using Business.Layer.Model;
using Logic.Layer.Contracts;
using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using UI.Layer.Commands;

namespace UI.Layer.ViewModel
{
    public class CustomerViewsModel : NotifyPropertyChanged
    {
        public ICommand CustomerUpdateCommand { get; }
        public ICommand CustomerDeleteCommand { get; }
        public ICommand CustomerCancelCommand { get; }
        public ICommand AddCustomerCommand { get; }
        public ICommand UpdateHourCommand { get; }
        public ICommand AddHourCommand { get; }
        public ICommand DeleteHourCommand { get; }

        private readonly ICustomerLogic customerLogic;
        private readonly IHourLogic hourLogic;

        public CustomerViewsModel(ICustomerLogic customerLogic, IHourLogic hourLogic) : base ()
        {
            this.customerLogic = customerLogic;
            this.hourLogic = hourLogic;
            Hour = hourLogic.Create();
            NewHour = hourLogic.Create();
            Customers = new ObservableCollection<Customer>(customerLogic.ToList());
            AddCustomerCommand = new RelayCommand(c => AddCustomer(), c => !string.IsNullOrEmpty(CustomerName));
            CustomerUpdateCommand = new RelayCommand(c => UpdateCustomer(), c => CustomerSelected != null && !string.IsNullOrEmpty(CustomerSelected.Name));
            CustomerDeleteCommand = new RelayCommand(c => DeleteCustomer(), c => CustomerSelected != null);
            CustomerCancelCommand = new RelayCommand(c => {
                CustomerSelected = null;
                CustomerName = string.Empty;
                CustomerAddress = string.Empty;
            });
            UpdateHourCommand = new RelayCommand(c => UpdateHour(), c => HourSelected != null);
            AddHourCommand = new RelayCommand(c => AddHour(), c => NewHour.DayOfWeek > -1 && NewHour.DayOfWeek < 7 && CustomerSelected != null);
            DeleteHourCommand = new RelayCommand(c => DeleteHour(), c => HourSelected != null);
        }

        public ObservableCollection<Customer> Customers { get; set; }

        private Customer _customerSelected;
        public Customer CustomerSelected
        {
            get { return _customerSelected; }
            set
            {
                _customerSelected = value;
                OnNotifyPropertyChanged("CustomerSelected");
            }
        }

        private Hour _newHour;
        public Hour NewHour
        {
            get { return _newHour; }
            set
            {
                _newHour = value;
                OnNotifyPropertyChanged("NewHour");
            }
        }

        private Hour _hour;
        public Hour Hour
        {
            get { return _hour; }
            set
            {
                _hour = value;
                OnNotifyPropertyChanged("Hour");
            }
        }

        private Hour _hourSelected;
        public Hour HourSelected
        {
            get { return _hourSelected; }
            set
            {
                _hourSelected = value;
                OnNotifyPropertyChanged("HourSelected");
                if (value != null)
                    Hour = hourLogic.Create(value.Place, value.EntryTime, value.ExitTime, value.DayOfWeek, value.Customer, value.EntryDriver, value.ExitDriver);
            }
        }

        private string _customerName;
        public string CustomerName
        {
            get { return _customerName; }
            set
            {
                _customerName = value;
                OnNotifyPropertyChanged("CustomerName");
            }
        }

        private string _customerAddress;
        public string CustomerAddress
        {
            get { return _customerAddress; }
            set
            {
                _customerAddress = value;
                OnNotifyPropertyChanged("CustomerAddress");
            }
        }

        void AddCustomer()
        {
            try
            {
                Customer customer = customerLogic.Create(CustomerName, CustomerAddress);
                if (customerLogic.Save(customer))
                {
                    CustomerName = string.Empty;
                    CustomerAddress = string.Empty;
                    Customers.Add(customer);
                    MessageBox.Show("Cliente Agregado", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("No se pudo registrar el Cliente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        void UpdateCustomer()
        {
            try
            {
                if (customerLogic.Modify(CustomerSelected))
                {
                    CustomerSelected = null;
                    MessageBox.Show("Datos modificados exitosamente.", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("No se pudo modificar el Cliente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        void DeleteCustomer()
        {
            MessageBoxResult result = MessageBox.Show("¿Está seguro que desea eliminar el cliente?", "Atención", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (customerLogic.Remove(CustomerSelected))
                    {
                        Customers.Remove(CustomerSelected);
                        CustomerSelected = null;
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el Cliente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        void UpdateHour()
        {
            try
            {
                if (hourLogic.Modify(Hour, HourSelected))
                {
                    MessageBox.Show("Horario Modificado", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("No se pudo modificar el Horario", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        void AddHour()
        {
            try
            {
                Hour hour = hourLogic.Create(NewHour.Place, NewHour.EntryTime, NewHour.ExitTime, NewHour.DayOfWeek, CustomerSelected, NewHour.EntryDriver, NewHour.ExitDriver);
                if (hourLogic.Save(hour))
                {
                    CustomerSelected.Hours.Add(hour);
                    NewHour = hourLogic.Create();
                    MessageBox.Show("Horario Agregado", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("No se pudo registrar el Horario", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        void DeleteHour()
        {
            MessageBoxResult result = MessageBox.Show("¿Está seguro que desea eliminar el horario?", "Atención", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (hourLogic.Remove(HourSelected))
                    {
                        CustomerSelected.Hours.Remove(HourSelected);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el Horario", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
