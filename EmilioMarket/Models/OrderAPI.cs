using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmilioMarket.Models
{
   public class OrderAPI
    {
        //composicion, cuando relacione varios modelo de clase:
        public int OrderID { get; set; }
        public DateTime DateOrder { get; set; }
        public Customer Customer { get; set; }
        public OrderStatus OrderStatus { get; set; }   
        public ICollection<OrderDetail> Details { get; set; }
    }
}
