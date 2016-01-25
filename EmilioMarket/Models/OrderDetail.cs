using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmilioMarket.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }

        [Display(Name = "Product Description")]
        [Required(ErrorMessage = "You must enter {0})")]
        [StringLength(30, ErrorMessage = "The field {0} must between and {2} characters", MinimumLength = 3)]     
        public string Descripcion { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "YOu must the field {0}")]
        public decimal Price { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "You must the field {0}")]
        public decimal Quantity { get; set; }

        public int OrderId { get; set; }
        public int ProductId { get; set; }

        //Lado uno de la relación:
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }


    }
}