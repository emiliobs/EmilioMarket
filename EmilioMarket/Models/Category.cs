using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmilioMarket.Models
{
  public  class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [StringLength(30, ErrorMessage = "The field {0} must contain between 3 and 30 character")]
        [Required(ErrorMessage = "You must enter the field {0}")]
        [Display(Name = "Category Description")]
        public string  Description { get; set; }
    }
}
