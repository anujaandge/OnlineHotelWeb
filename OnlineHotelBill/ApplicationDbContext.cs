using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineHotelModels;

namespace OnlineHotelBill
{
    public class ApplicationDbContext:IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>Options):base(Options)
        {
            
        }
        public  DbSet<ApplicationUser>AppicationUsers { get; set; }
    }
}
