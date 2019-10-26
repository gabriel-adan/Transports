using System;
using System.Windows;
using UI.Layer.Registry;

namespace UI.Layer
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string AppName = "SISTEMA";

        public MainWindow()
        {
            InitializeComponent();
            try
            {
                bool IsRegistred = LicenseKeyManager.IsRegistred();
                if (!IsRegistred)
                    Title = AppName + " - Licencia Inválida";
            }
            catch (LicenseKeyException e)
            {
                Title = AppName + " - " + e.Message;
            }
            catch (Exception e)
            {

            }
            Title = AppName;
        }
    }
}
