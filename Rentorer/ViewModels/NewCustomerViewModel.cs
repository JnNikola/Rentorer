using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rentor.Models;
using Rentorer.Models;

namespace Rentorer.ViewModels
{
    public class NewCustomerViewModel
    {
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
        public Customer Customer { get; set; }

        public bool? IsEditing { get; set; }
    }
}