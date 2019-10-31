using System;
using System.Windows;
using System.ComponentModel;
using Spring.Context;
using Spring.Context.Support;
using UI.Layer.View;
using UI.Layer.Registry;
using UI.Layer.ViewModel;
using Logic.Layer.Contracts;
using UI.Layer.Properties;
using Microsoft.Win32;
using System.IO;

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
                ValidateLicense();
            }
            catch (LicenseKeyException e)
            {
                Title = AppName + " - " + e.Message;
                ItemCustomer.IsEnabled = false;
                ItemHourAssignment.IsEnabled = false;
                ItemDriver.IsEnabled = false;
                ItemLicense.IsEnabled = true;
            }
            catch (Exception e)
            {
                ItemCustomer.IsEnabled = false;
                ItemHourAssignment.IsEnabled = false;
                ItemDriver.IsEnabled = false;
                ItemLicense.IsEnabled = true;
                MessageBox.Show("Error: " + e.Message);
            }
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

        private void Registry_License_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.CheckFileExists = true;
                openFileDialog.CheckPathExists = true;
                openFileDialog.Filter = "Archivo de Licencia (*.key)|*.key";
                if (openFileDialog.ShowDialog() == true)
                {
                    string filePath = openFileDialog.FileName;
                    File.Copy(filePath, Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + openFileDialog.SafeFileName);
                    Settings.Default.KeyFilePath = filePath;
                    ValidateLicense();
                    Settings.Default.Save();
                    MessageBox.Show("Licencia registrada!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (UnauthorizedAccessException uae)
            {
                MessageBox.Show(uae.Message + " - Ejecute el programa en modo Administrador", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        void ValidateLicense()
        {
            bool IsRegistred = LicenseKeyManager.IsRegistred();
            if (!IsRegistred)
            {
                Title = AppName + " - Licencia Inválida";
                ItemCustomer.IsEnabled = false;
                ItemHourAssignment.IsEnabled = false;
                ItemDriver.IsEnabled = false;
                ItemLicense.IsEnabled = true;
            }
            else
            {
                Title = AppName;
                context = new XmlApplicationContext("application-ci.xml");
                var dataContext = context.GetObject("DataContext") as Infraestructure.Layer.DataContext;
                dataContext.OpenConnection();
                ItemCustomer.IsEnabled = true;
                ItemHourAssignment.IsEnabled = true;
                ItemDriver.IsEnabled = true;
                ItemLicense.IsEnabled = false;
            }
        }
    }
}