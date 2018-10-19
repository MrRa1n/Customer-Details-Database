using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Data;
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

        private void frmCustomerDetails_Loaded(object sender, RoutedEventArgs e)
        {
            // Create new variable to store current customer details
            Customer currentCustomer;

            // Creates new instance of DataTable to display customer details
            DataTable customerTable = new DataTable();

            // Set up header names for data table
            customerTable.Columns.Add("ID");
            customerTable.Columns.Add("First Name");
            customerTable.Columns.Add("Last Name");
            customerTable.Columns.Add("Email");
            customerTable.Columns.Add("Skype");
            customerTable.Columns.Add("Telephone");
            customerTable.Columns.Add("Preferred Contact");

            
            foreach (int customerId in _store.Matrics)
            {
                // Return customer based on the customerId
                currentCustomer = _store.Find(customerId);
                // Build our rows of customer details and add to our data table
                customerTable.Rows.Add(
                    currentCustomer.ID, 
                    currentCustomer.FirstName, 
                    currentCustomer.LastName, 
                    currentCustomer.EmailAddress,
                    currentCustomer.SkypeID,
                    currentCustomer.TelephoneNo,
                    currentCustomer.GetPreferredContact(currentCustomer.PreferredContact)
                    );
            }

            // Bind DataGrid to our data table to display customer details in our form
            gridCustomerDetails.DataContext = customerTable.DefaultView;
        }
    }
}
