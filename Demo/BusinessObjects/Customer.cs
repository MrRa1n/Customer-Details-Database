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
                    throw new Exception("Error: Invalid option!");
            }
        }

        public string Validate()
        {
            // Create new context to describe the tpye being validated (Customer object)
            ValidationContext validationContext = new ValidationContext(this, null, null);
            List<ValidationResult> validationResults = new List<ValidationResult>();

            // Attempt to validate object, storing each failed validations in validationResults
            bool isValid = Validator.TryValidateObject(this, validationContext, validationResults, false);

            if (isValid == false)
            {
                StringBuilder validationErrors = new StringBuilder();
                foreach (ValidationResult validationResult in validationResults)
                {
                    validationErrors.AppendLine(validationResult.ErrorMessage);
                }
                return validationErrors.ToString();
            }
            return "Data successfully stored";
        }
    }
}
