using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineHotelBill;
using OnlineHotelModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onlineHotelutility
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private ApplicationDbContext _context;
        //private readonly OtherDependency _otherDependency;

        public DbInitializer(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        void IDbInitializer.Initialize()
        {
            Initialize();
            throw new NotImplementedException();

        }

        // Rest of your DbInitializer code...
        public void Initialize()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate();
                }
            }
            catch (Exception)
            {
                throw;
            }

            if (!_roleManager.RoleExistsAsync(WebSiteRoles.Hotel_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(WebSiteRoles.Hotel_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(WebSiteRoles.Hotel_Customer)).GetAwaiter().GetResult();

                var adminUser = new ApplicationUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    Name = "Admin",
                };

                // Create the user
                var result = _userManager.CreateAsync(adminUser, "Admin@123").GetAwaiter().GetResult();

                // Check if the user was created successfully
                if (result.Succeeded)
                {
                    // Retrieve the created user
                    ApplicationUser user = _context.AppicationUsers.FirstOrDefault(x => x.Email == "admin@gmail.com");

                    // Add the user to the "Hotel_Admin" role
                    _userManager.AddToRoleAsync(user, WebSiteRoles.Hotel_Admin).GetAwaiter().GetResult();
                }
            }
        }
    }


  

}
