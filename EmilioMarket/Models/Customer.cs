using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmilioMarket.Models
{

    public class Customer
    {
        private string _FullName;

        [Key]
        public int CustomerId { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "You must enter {0})")]
        [StringLength(30, ErrorMessage = "The field {0} must between and {2} characters", MinimumLength = 3)]
        public string FirstName { get; set; }

        [Display(Name = "last Name")]
        [Required(ErrorMessage = "You must enter {0})")]
        [StringLength(30, ErrorMessage = "The field {0} must between and {2} characters", MinimumLength = 3)]
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "You must enter {0})")]
        [StringLength(30, ErrorMessage = "The field {0} must between and {2} characters", MinimumLength = 3)]
        public string Phone { get; set; }


        [Required(ErrorMessage = "You must enter {0})")]
        [StringLength(30, ErrorMessage = "The field {0} must between and {2} characters", MinimumLength = 3)]
        public string Address { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

       
        [Required(ErrorMessage = "You must enter {0})")]
        [StringLength(30, ErrorMessage = "The field {0} must between and {2} characters", MinimumLength = 5)]
        public string Document { get; set; }

        //Lado Uno de la relación:
        [Display(Name = "Document Type")]
        public int DocumentTypeId { get; set; }

        [Display(Name = "Document Type")]
        [JsonIgnore]
        public virtual DocumentType DocumentType { get; set; }

        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}",FirstName,LastName);
            }

            set
            {
                _FullName = value;
            }
        }
    }
}