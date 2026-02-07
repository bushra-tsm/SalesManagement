using Microsoft.AspNetCore.Identity;
using SalesManagement.Constants;


namespace SalesManagement.Data
{
    public static class DbSeeder
    {

        public static async Task SeedRolesAndAdminAsync(IServiceProvider service)
        {
            //seed roles
            var userManager = service.GetService<UserManager<ApplicationUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Manager.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.SalesRep.ToString()));



            // creating admin
            //var user = new ApplicationUser
            //{

            //    FirstName = "Bushra",
            //    LastName = "Tabassum",
            //    Email = "bushra@gmail.com",
            //    EmailConfirmed = true,
            //    PhoneNumberConfirmed = true,
            //};
            //var userInDb = await userManager.FindByEmailAsync(user.Email);
            //if (userInDb == null)
            //{
            //    await userManager.CreateAsync(user, "Bushra@123");
            //    await userManager.AddToRoleAsync(user, Roles.Admin.ToString());
            //}
        }
    }
}
