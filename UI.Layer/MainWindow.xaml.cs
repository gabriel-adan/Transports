using System;
using System.Windows;
using System.ComponentModel;
using Spring.Context;
using Spring.Context.Support;
using UI.Layer.View;
using UI.Layer.Registry;
using UI.Layer.ViewModel;
using Logic.Layer.Contracts;

namespace UI.Layer
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string AppName = "SISTEMA DE PROGRAMACION DE TRASLADOS";
        IApplicationContext context;

        public MainWindow()
        {
            InitializeComponent();
            try
            {
                bool IsRegistred = LicenseKeyManager.IsRegistred();
                if (!IsRegistred)
                    Title = AppName + " - Licencia Inválida";
                context = new XmlApplicationContext("application-ci.xml");
                var dataContext = context.GetObject("DataContext") as Infraestructure.Layer.DataContext;
                dataContext.OpenConnection();
            }
            catch (LicenseKeyException e)
            {
                Title = AppName + " - " + e.Message;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
            Title = AppName;
        }

        private void Customers_Click(object sender, RoutedEventArgs e)
        {
            ICustomerLogic customerLogic = context.GetObject("CustomerLogic") as ICustomerLogic;
            IHourLogic hourLogic = context.GetObject("HourLogic") as IHourLogic;
            MainContainer.Content = new Customers(new CustomerViewsModel(customerLogic, hourLogic));
            ItemCustomer.IsEnabled = false;
            ItemHourAssignment.IsEnabled = true;
            ItemDriver.IsEnabled = true;
        }

        private void HourAssignment_Click(object sender, RoutedEventArgs e)
        {
            IHourLogic hourLogic = context.GetObject("HourLogic") as IHourLogic;
            IDriverLogic driverLogic = context.GetObject("DriverLogic") as IDriverLogic;
            MainContainer.Content = new HourAssignment(new HourAssignmentViewModel(hourLogic, driverLogic), hourLogic, driverLogic);
            ItemHourAssignment.IsEnabled = false;
            ItemDriver.IsEnabled = true;
            ItemCustomer.IsEnabled = true;
        }

        private void Drivers_Click(object sender, RoutedEventArgs e)
        {
            IDriverLogic driverLogic = context.GetObject("DriverLogic") as IDriverLogic;
            Drivers drivers = new Drivers(new DriverViewModel(driverLogic));
            drivers.Width = 350;
            MainContainer.Content = drivers;
            ItemDriver.IsEnabled = false;
            ItemCustomer.IsEnabled = true;
            ItemHourAssignment.IsEnabled = true;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            try
            {
                var dataContext = context.GetObject("DataContext") as Infraestructure.Layer.DataContext;
                dataContext.CloseConnection();
            }
            catch (Exception ex)
            {

            }
            base.OnClosing(e);
        }
    }
}