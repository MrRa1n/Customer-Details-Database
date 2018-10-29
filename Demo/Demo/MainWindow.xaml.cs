using System;
using System.Windows;
using System.Windows.Controls;
using BusinessObjects;

namespace Demo
{
    /// <summary>
    /// Author: Toby Cook
    /// Description: Interaction logic for MainWindow.xaml
    /// Last Modified: 29/10/2018
    /// </summary>
    public partial class MainWindow : Window
    {
        private MailingList store = new MailingList();
        private int customerCount = 0;
        private int customerID = 10001;
        private int prevCustomerID = 10001;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Displays the information about the customer in the form fields.
        /// </summary>
        /// <param name="customer">Customer object</param>
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

        /// <summary>
        /// Clears any data that has been typed into the form fields.
        /// </summary>
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

        /// <summary>
        /// Method to get the currently selected contact method from the ListBox and return its enum value.
        /// </summary>
        /// <returns>ContactType value <c>EMAIL</c>, <c>SKYPE</c> or <c>TEL</c> for customer</returns>
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

        /// <summary>
        /// Event handler for adding a new customer to the database. Performs validation 
        /// to check required fields are not blank.
        /// </summary>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Only increment Customer ID if
                // - a customer exists
                // - the current ID matches the previous ID
                // - the current customer ID is less than 50000
                if (customerCount > 0 && prevCustomerID == customerID && customerID < 50000)
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
                // Set the previous customer ID to the one that has been added to store
                prevCustomerID = customerID;
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

        /// <summary>
        /// Event handler for searching for customer details in database by Customer ID.
        /// Displays customer details in form fields once found
        /// </summary>
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

        /// <summary>
        /// Deletes customer from database using the supplied customer ID.
        /// If the customer exists, they are deleted from the database. If the customer
        /// doesn't exist an exception is thrown.
        /// </summary>
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
                    listCustomerNames.Items.Remove(listCustomerNames.SelectedItem);
                    store.Delete(customer.ID);
                    ClearAllFields();
                    
                    MessageBox.Show("Customer deleted successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// When a customer is selected in the ListBox, their details are displayed
        /// in the form fields.
        /// </summary>
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
            catch (NullReferenceException)
            {
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Creates new instance of CustomerDetails form and passes it the customer database.
        /// </summary>
        private void btnListAll_Click(object sender, RoutedEventArgs e)
        {
            CustomerDetails customerDetails = new CustomerDetails(store);
            customerDetails.ShowDialog();
        }
    }
}
