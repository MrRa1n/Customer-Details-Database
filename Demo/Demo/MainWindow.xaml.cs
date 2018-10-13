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
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ClearAllFields()
        {
            txtCustomerID.Clear();
            txtFirstName.Clear();
            txtSurname.Clear();
            txtEmailAddress.Clear();
            txtSkypeID.Clear();
            txtTelephone.Clear();
        }

        // Method for setting the customer's preferred method of contact
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
                
                
                Console.WriteLine();

                int customerID;

                if (store.Matrics.Count < 1)
                {
                    customerID = 10001;
                    Console.WriteLine(customerID);
                }
                else
                {
                    customerID = 10001 + 1;
                    Console.WriteLine(customerID);
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
                    throw new Exception("Customer not found!");
                }

                // Display the found information in form fields
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

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Check to see if the customer exists in the store
                Customer customer = store.Find(int.Parse(txtCustomerID.Text));

                // Throw new exception if customer doesn't exist, otherwise delete the customer
                if (customer == null)
                {
                    throw new Exception("Customer not found!");
                }
                else
                {
                    store.Delete(customer.ID);
                    MessageBox.Show("Customer deleted successfully");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
