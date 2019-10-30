using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using UI.Layer.ViewModel;

namespace UI.Layer.View
{
    /// <summary>
    /// Lógica de interacción para Transports.xaml
    /// </summary>
    public partial class Transports : Window
    {
        public Transports(TransportViewModel transportViewModel)
        {
            InitializeComponent();
            DataContext = transportViewModel;
        }

        private void DataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {
            if (!e.Column.SortMemberPath.Equals("EntryTime"))
                return;
            e.Handled = true;
        }

        private void DataGrid_Initialized(object sender, EventArgs e)
        {
            try
            {
                DataGridColumn column = ((DataGrid)sender).Columns[2];
                DataGrid_Sorting(sender, new DataGridSortingEventArgs(column));
                ListCollectionView view = (ListCollectionView)CollectionViewSource.GetDefaultView(((DataGrid)sender).ItemsSource);
                view.CustomSort = new HourSortingByEntryTime();
                column.SortDirection = System.ComponentModel.ListSortDirection.Ascending;
            }
            catch (Exception ex)
            {

            }
        }

        private void Copy_To_Clipboard_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                FrameworkElement grid = button.Parent as FrameworkElement;
                StackPanel stack = grid.Parent as StackPanel;
                DataGrid dataGrid = stack.Children[1] as DataGrid;
                Border border = stack.Parent as Border;
                RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)border.ActualWidth, (int)border.ActualHeight, 96, 96, PixelFormats.Default);
                renderTargetBitmap.Render(dataGrid);
                Clipboard.SetImage(renderTargetBitmap);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
