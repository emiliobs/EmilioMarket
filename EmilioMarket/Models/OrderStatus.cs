using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmilioMarket.Models
{
    public enum OrderStatus
    {
        Created,
        InProgress,
        Shipped,
        Delivered  
    }
}