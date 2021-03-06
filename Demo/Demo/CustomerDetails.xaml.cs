﻿using System.Windows;
using System.Data;
using BusinessObjects;

namespace Demo
{
    /// <summary>
    /// Author: Toby Cook
    /// Description: Interaction logic for Customer Details form where information 
    /// is displayed to the user in an easy to view manner.
    /// Last Modified: 30/10/2018
    /// </summary>
    public partial class CustomerDetails : Window
    {
        private MailingList _store;

        public CustomerDetails(MailingList store)
        {
            InitializeComponent();
            _store = store;
        }

        /// <summary>
        /// Upon the form loading, the DataTable is populated with database information.
        /// The information is then displayed using a DataGrid control.
        /// </summary>
        private void frmCustomerDetails_Loaded(object sender, RoutedEventArgs e)
        {
            
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

            // Create new variable to store current customer details
            Customer currentCustomer;

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
