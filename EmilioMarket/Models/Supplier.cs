using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmilioMarket.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }

        [Display(Name = "Supplier Name")]
        [Required(ErrorMessage = "You must enter {0})")]
        [StringLength(30, ErrorMessage = "The field {0} must between and {2} characters", MinimumLength = 3)]
        public string Name { get; set; }

        [Display(Name = "Contac First Name")]
        [Required(ErrorMessage = "You must enter {0})")]
        [StringLength(30, ErrorMessage = "The field {0} must between and {2} characters", MinimumLength = 3)]
        public string ContactFirstName { get; set; }

        [Display(Name = "Contact last Name")]
        [Required(ErrorMessage = "You must enter {0})")]
        [StringLength(30, ErrorMessage = "The field {0} must between and {2} characters", MinimumLength = 3)]
        public string ContactLastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "You must enter {0})")]
        [StringLength(30, ErrorMessage = "The field {0} must between and {2} characters", MinimumLength = 3)]
        public string Phone { get; set; }

      
        [Required(ErrorMessage = "You must enter {0})")]
        [StringLength(30, ErrorMessage = "The field {0} must between and {2} characters", MinimumLength = 3)]
        public string Address { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        //Lado Uno de la Relación:
        public virtual ICollection<SupplierProduct> SupplierProducts { get; set; }


    }
}