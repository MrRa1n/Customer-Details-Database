using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
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

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtCustomerID.Text, out int n))
            {
                MessageBox.Show("Enter a number you dick!");
                return;
            }
            Customer customer = new Customer
            {
                ID = int.Parse(txtCustomerID.Text),
                FirstName = txtFirstName.Text,
                LastName = txtSurname.Text,
                EmailAddress = txtEmailAddress.Text,
                SkypeID = txtSkypeID.Text,
                TelephoneNo = txtTelephone.Text,
                PreferredContact = GetContactType()
            };

        }
    }
}
