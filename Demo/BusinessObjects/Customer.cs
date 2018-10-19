using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects
{
    public enum ContactType
    {
        EMAIL,
        SKYPE,
        TEL
    }
    public class Customer
    {
        public StringBuilder validationErrors { get; set; }

        [Required(ErrorMessage = "ID is a required field.")]
        public int ID { get; set; }

        [Required(ErrorMessage = "First name is a required field.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Surname is a required field.")]
        public string LastName { get; set; }
        
        public string EmailAddress { get; set; }

        public string SkypeID { get; set; }

        [Required(ErrorMessage = "Telephone is a required field.")]
        public string TelephoneNo { get; set; }

        [Required(ErrorMessage = "Preferred contact is a required field.")]
        public ContactType PreferredContact { get; set; }

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

        public bool Validate()
        {
            List<ValidationResult> validationResults = new List<ValidationResult>();
            // Attempt to validate object, storing each failed validations in validationResults
            bool isValidated = Validator.TryValidateObject(this, new ValidationContext(this, null, null), validationResults, false);

            if (isValidated == false)
            {
                // If field is empty, append each error to string and return the string
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
