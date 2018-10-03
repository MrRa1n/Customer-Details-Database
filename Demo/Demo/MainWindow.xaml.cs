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

        private ContactType GetContactType()
        {
            if (listPreferredContact.SelectedItem.Equals("Email"))
            {
                return ContactType.EMAIL;
            }
            else if (listPreferredContact.SelectedItem.Equals("Skype"))
            {
                return ContactType.SKYPE;
            }
            else
            {
                return ContactType.TEL;
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
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtCustomerID.Text, out int parsedID))
            {
                MessageBox.Show("Enter a numerical value");
            }
            else if (parsedID < 10001 || parsedID > 50000)
            {
                MessageBox.Show("Please enter a number between 10001 and 50000");
            }
            else
            {
                Customer customer = new Customer
                {
                    ID = parsedID,
                    FirstName = txtFirstName.Text,
                    LastName = txtSurname.Text,
                    EmailAddress = txtEmailAddress.Text,
                    SkypeID = txtSkypeID.Text,
                    TelephoneNo = txtTelephone.Text,
                    PreferredContact = GetContactType()
                };

                customer.Validate();

                store.Add(customer);

                ClearAllFields();
            }
            

        }
    }
}
