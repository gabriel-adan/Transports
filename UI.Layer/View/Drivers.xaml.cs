using System.Windows.Controls;
using UI.Layer.ViewModel;

namespace UI.Layer.View
{
    /// <summary>
    /// Lógica de interacción para Drivers.xaml
    /// </summary>
    public partial class Drivers : Page
    {
        DriverViewModel Model;

        public Drivers(DriverViewModel driverViewModel)
        {
            InitializeComponent();
            Model = driverViewModel;
            DataContext = driverViewModel;
        }
    }
}
