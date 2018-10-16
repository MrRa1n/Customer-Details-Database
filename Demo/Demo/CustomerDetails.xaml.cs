using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BusinessObjects;
namespace Demo
{
    /// <summary>
    /// Interaction logic for CustomerDetails.xaml
    /// </summary>
    public partial class CustomerDetails : Window
    {
        private MailingList _store;

        public CustomerDetails(MailingList store)
        {
            InitializeComponent();
            _store = store;
        }

        private void gridCustomerDetails_Loaded(object sender, RoutedEventArgs e)
        {
            
            DataGrid dataGrid = new DataGrid();
            dataGrid.ItemsSource = _store.Matrics;
            

            foreach (Customer customer in _store)
            {

            }

        }
    }
}
