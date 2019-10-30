using System;
using System.Windows.Data;
using System.Windows.Controls;
using Business.Layer.Model;
using UI.Layer.ViewModel;

namespace UI.Layer.View
{
    /// <summary>
    /// Lógica de interacción para Customers.xaml
    /// </summary>
    public partial class Customers : Page
    {
        CustomerViewsModel Model;

        public Customers(CustomerViewsModel customerViewsModel)
        {
            InitializeComponent();
            Model = customerViewsModel;
            DataContext = Model;
            CustomerList.ItemsSource = Model.Customers;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(CustomerList.ItemsSource);
            view.Filter = CustomerFilter;
        }

        private bool CustomerFilter(object item)
        {
            if (string.IsNullOrEmpty(TxtSearch.Text))
                return true;
            else
                return ((item as Customer).Name.IndexOf(TxtSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(CustomerList.ItemsSource).Refresh();
        }
    }
}
