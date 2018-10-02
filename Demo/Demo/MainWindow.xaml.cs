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
        private FormValidation formValidation = new FormValidation();
        public MainWindow()
        {
            InitializeComponent();
        }


        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            formValidation.Validate(txtCustomerID.Text, null);
        }
    }

    public class FormValidation : ValidationRule
    {
        public FormValidation()
        {

        }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null)
            {
                return new ValidationResult(false, new Exception("Field cannot be empty!"));
            }
            else
            {
                try
                {
                    Int32.Parse(value.ToString());
                }
                catch
                {
                    return new ValidationResult(false, new Exception("Field must be numeric!"));
                }
            }
            return ValidationResult.ValidResult;
        }
    }
}
