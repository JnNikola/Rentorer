using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Rentorer.CustomValidations;
using Rentorer.Models;

namespace Rentor.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public DateTime? Birthday { get; set; }

        [Required] //not nullable
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSubscribedToNewsLetter { get; set; }
        public MembershipType MembershipTypeType { get; set; }
        [Display(Name = "Membership Type")]

        //as a foreign key
        [Min18IfMember]
        public byte MembershipTypeId { get; set; }
    }
}