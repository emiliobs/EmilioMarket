using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmilioMarket.ViewModels
{
    public class UserView
    {
        //es string por que es un toquen:
        public string UserId { get; set; }
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        //para pintar los titulos en la vista:
        public RoleView Role { get; set; }
        public List<RoleView> Roles { get; set; }


    }
}