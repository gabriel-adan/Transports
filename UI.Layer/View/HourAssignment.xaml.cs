using System;
using System.Windows.Controls;
using Business.Layer.Model;
using Logic.Layer.Contracts;
using UI.Layer.ViewModel;

namespace UI.Layer.View
{
    /// <summary>
    /// Lógica de interacción para HourAssignment.xaml
    /// </summary>
    public partial class HourAssignment : Page
    {
        HourAssignmentViewModel Model;
        private readonly IHourLogic hourLogic;
        private readonly IDriverLogic driverLogic;

        public HourAssignment(HourAssignmentViewModel hourAssignmentViewModel, IHourLogic hourLogic, IDriverLogic driverLogic)
        {
            InitializeComponent();
            this.hourLogic = hourLogic;
            this.driverLogic = driverLogic;
            Model = hourAssignmentViewModel;
            DataContext = hourAssignmentViewModel;
        }

        private void ComboBox_Entry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (((ComboBox)sender).IsDropDownOpen)
                {
                    Driver driver = ((ComboBox)sender).SelectedItem as Driver;
                    Model.AddEntryDriver(driver);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void ComboBox_Exit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (((ComboBox)sender).IsDropDownOpen)
                {
                    Driver driver = ((ComboBox)sender).SelectedItem as Driver;
                    Model.AddExitDriver(driver);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void Transports_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            new Transports(new TransportViewModel(hourLogic, driverLogic)).ShowDialog();
        }
    }
}
