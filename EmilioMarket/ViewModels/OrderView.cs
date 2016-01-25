using EmilioMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmilioMarket.ViewModels
{
    public class OrderView
    {
        public Customer Customer { get; set; }
        //para mostrar los titulos de la tabla:
        public ProductOrder Product { get; set; }
        public List<ProductOrder> ProductOrders  { get; set; }
    }
}