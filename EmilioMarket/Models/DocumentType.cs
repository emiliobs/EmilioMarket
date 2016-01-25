using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmilioMarket.Models
{
    public class DocumentType
    {
        [Key]
        [Display(Name = "Document Type")]
        public int DocumentTypeId { get; set; }

        [Display(Name = "Document")]
        public string Description { get; set; }

        //Relacón Uno a varios de la Relación:
        public ICollection<Employee> Employees { get; set; }
        public ICollection<Customer> Customer { get; set; }
    }
}