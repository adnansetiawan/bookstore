using BookStore.Entities.Indentity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL
{
    public class ApplicationEntities : IdentityDbContext<ApplicationUser>
    {
        public ApplicationEntities()
            : base("AuthConnection")
        {
        }
        public static ApplicationEntities Create()
        {
            return new ApplicationEntities();
        }
    }
}
