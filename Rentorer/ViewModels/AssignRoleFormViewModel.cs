using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Rentor.Models;

namespace Rentorer.ViewModels
{
    public class AssignRoleFormViewModel
    {
        public IEnumerable<ApplicationUser> Users { get; set; }

        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}