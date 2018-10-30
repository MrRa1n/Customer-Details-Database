using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text;

namespace BusinessObjects
{
    /// <summary>
    /// Enum values for contact type for the customer.
    /// </summary>
    public enum ContactType
    {
        EMAIL,
        SKYPE,
        TEL
    }
    /// <summary>
    /// Author: Toby Cook
    /// Description: Contains all the properties relating to the customer and validation for each property.
    /// Last modified: 30/10/2018
    /// </summary>
    public class Customer
    {
        /// <value>Store for the errors thrown in field validation</value>
        public StringBuilder validationErrors { get; private set; }

        /// <value>Customer ID in the range 10001 to 50000</value>
        [Required(ErrorMessage = "ID is a required field.")]
        public int ID { get; set; }

        /// <value>First name of customer - required field</value>
        [Required(ErrorMessage = "First name is a required field.")]
        public String FirstName { get; set; }

        /// <value>Last name of customer - required field</value>
        [Required(ErrorMessage = "Surname is a required field.")]
        public String LastName { get; set; }

        /// <value>Customer email address - must contain '@' symbol</value>
        public String EmailAddress { get; set; }

        /// <value>Customer Skype ID</value>
        public String SkypeID { get; set; }

        /// <value>Customer telephone number - required field</value>
        [Required(ErrorMessage = "Telephone is a required field.")]
        public String TelephoneNo { get; set; }

        /// <value>Customer's preferred means of contact - required field</value>
        [Required(ErrorMessage = "Preferred contact is a required field.")]
        public ContactType PreferredContact { get; set; }

        /// <summary>
        /// Gets the customer's preferred means of contact.
        /// </summary>
        /// <param name="contactType">Takes enum value <c>EMAIL</c>, <c>SKYPE</c> or <c>TEL</c></param>
        /// <returns>Formatted string with customer's contact type.</returns>
        public String GetPreferredContact(ContactType contactType)
        {
            switch (contactType)
            {
                case ContactType.EMAIL:
                    return "Email: " + EmailAddress;
                case ContactType.SKYPE:
                    return "Skype: " + SkypeID;
                case ContactType.TEL:
                    return "Tel: " + TelephoneNo;
                default:
                    throw new Exception("Please select a valid option");
            }
        }

        /// <summary>
        /// Validates any required fields using Validator. A validation context is created using the 
        /// customer object and checks if required fields are not null.
        /// 
        /// Any NullExceptions are stored in a List of ValidationResults and each message is appended to
        /// validationErrors string. 
        /// </summary>
        /// 
        /// <returns>
        /// <c>true</c> - If form has been successfully validated
        /// <c>false</c> - If required fields are null or email is incorrect format
        /// </returns>
        public bool Validate()
        {
            List<ValidationResult> validationResults = new List<ValidationResult>();
            System.Net.Mail.MailAddress email = new System.Net.Mail.MailAddress(EmailAddress);
            // Attempt to validate object, storing each failed validations in validationResults
            bool isValidated = Validator.TryValidateObject(this, new ValidationContext(this, null, null), validationResults, false);
            
            if (isValidated == false && !email.Host.Contains("@"))
            {
                // If field is empty or email does not contain '@', append each error to string and return false
                validationErrors = new StringBuilder();
                foreach (var validationResult in validationResults)
                {
                    validationErrors.AppendLine(validationResult.ErrorMessage);
                }
                return false;
            }
            return true;
        }

        
    }
}
