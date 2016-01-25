using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmilioMarket.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        public int CustomerId { get; set; }
        public OrderStatus OrderStatus { get; set; }

        //Lado varios de la relación: 
        [JsonIgnore] 
        public virtual Customer Customer { get; set; }
        [JsonIgnore]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

    }
}