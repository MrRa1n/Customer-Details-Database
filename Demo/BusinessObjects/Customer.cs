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
        [Required(ErrorMessage = "First Name is a required field.")]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string SkypeID { get; set; }

        public string TelephoneNo { get; set; }

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

        public void Validate()
        {
            ValidationContext context = new ValidationContext(this, serviceProvider: null, items: null);
            List<ValidationResult> results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(this, context, results, false);

            if (isValid == false)
            {
                StringBuilder sbrErrors = new StringBuilder();
                foreach (ValidationResult validationResult in results)
                {
                    sbrErrors.AppendLine(validationResult.ErrorMessage);
                }
                throw new ValidationException(sbrErrors.ToString());
            }
        }
    }
}
