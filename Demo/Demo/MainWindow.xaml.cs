using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel.DataAnnotations;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BusinessObjects;

namespace Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MailingList store = new MailingList();
        private int customerCount = 0;
        private int customerID = 10001;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DisplayCustomerDetails(Customer customer)
        {
            try
            {
                txtCustomerID.Text = customer.ID.ToString();
                txtFirstName.Text = customer.FirstName;
                txtSurname.Text = customer.LastName;
                txtEmailAddress.Text = customer.EmailAddress;
                txtSkypeID.Text = customer.SkypeID;
                txtTelephone.Text = customer.TelephoneNo;
                listPreferredContact.SelectedIndex = (int)customer.PreferredContact;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearAllFields()
        {
            txtCustomerID.Clear();
            txtFirstName.Clear();
            txtSurname.Clear();
            txtEmailAddress.Clear();
            txtSkypeID.Clear();
            txtTelephone.Clear();
            listPreferredContact.SelectedIndex = 0;
        }

        // Method to get the currently selected contact method from the 
        // ListBox and return its enum value.
        private ContactType SetContactType()
        {
            if (listPreferredContact.SelectedIndex == 0)
            {
                return ContactType.EMAIL;
            }
            else if (listPreferredContact.SelectedIndex == 1)
            {
                return ContactType.SKYPE;
            }
            else
            {
                return ContactType.TEL;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // If a customer already exists, increment the ID by 1
                if (customerCount > 0)
                {
                    customerID++;
                }

                Customer customer = new Customer
                {
                    ID = customerID,
                    FirstName = txtFirstName.Text,
                    LastName = txtSurname.Text,
                    EmailAddress = txtEmailAddress.Text,
                    SkypeID = txtSkypeID.Text,
                    TelephoneNo = txtTelephone.Text,
                    PreferredContact = SetContactType()
                };

                // Check perform validation on required fields
                if (customer.Validate() == false)
                {
                    throw new Exception(customer.validationErrors.ToString());
                }
                
                store.Add(customer);

                // Add the customer's ID and name to the ListBox
                listCustomerNames.Items.Add(customer.ID + " " + customer.FirstName + " " + customer.LastName);

                ClearAllFields();

                customerCount++;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Find the customer by the supplied customer ID
                Customer customer = store.Find(int.Parse(txtCustomerID.Text));

                if (customer == null)
                {
                    throw new Exception("Customer not found.");
                }

                DisplayCustomerDetails(customer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(txtCustomerID.Text))
                {
                    throw new Exception("No customer ID provided.");
                }

                // Check to see if the customer exists in the store
                Customer customer = store.Find(int.Parse(txtCustomerID.Text));

                // Throw new exception if customer doesn't exist, otherwise delete the customer
                if (customer == null)
                {
                    throw new Exception("Customer not found.");
                }
                else
                {
                    store.Delete(customer.ID);
                    ClearAllFields();
                    listCustomerNames.Items.Remove(listCustomerNames.SelectedItem);
                    MessageBox.Show("Customer deleted successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listCustomerNames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Get the value of the selected item in list
                String selectedCustomer = listCustomerNames.SelectedItem.ToString();
                // Split the item where space occurs
                String[] split = selectedCustomer.Split(' ');
                // Store the parsed ID
                int selectedCustomerID = int.Parse(split[0]);
                // Search for the customer in the database
                Customer customer = store.Find(selectedCustomerID);

                DisplayCustomerDetails(customer);
            }
            catch (Exception ex)
            {

            }
            
        }

        private void btnListAll_Click(object sender, RoutedEventArgs e)
        {
            CustomerDetails customerDetails = new CustomerDetails(store);
            customerDetails.ShowDialog();
            
        }

        private void SplitCustomers()
        {
            // Get the value of the selected item in list
            String selectedCustomer = listCustomerNames.SelectedItem.ToString();
            // Split the item where space occurs
            String[] split = selectedCustomer.Split(' ');
            // Store the parsed ID
            int selectedCustomerID = int.Parse(split[0]);
            // Search for the customer in the database
            Customer customer = store.Find(selectedCustomerID);
        }
    }
}
