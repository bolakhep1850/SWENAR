using Microsoft.AspNetCore.Identity;
using SWENAR.Data;
using SWENAR.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace SWENAR.Models
{
    /// <summary>
    /// Seeding initial user
    /// </summary>
    public static class UserSeed
    {

        /// <summary>
        /// Seed default user for the application
        /// </summary>
        /// <param name="db">SbContext database context</param>
        /// <param name="userManager">User Manager for application</param>
        /// <returns>Nothing</returns>
        public static async Task SeedDefaultUserAsync(this SWENARDBContext db, UserManager<User> userManager)
        {

            //Add default user
            var dbUser = await userManager.FindByNameAsync("testuser");
            if (dbUser is null)
            {
                if (await userManager.FindByNameAsync("testuser") == null)
                {
                    var user = new User()
                    {
                        UserName = "testuser",
                        Email = "bolakhep1850@uhcl.edu"
                    };

                    var result  = await userManager.CreateAsync(user, "Password@1");
                    user = await userManager.FindByNameAsync("testuser");
                    await userManager.AddToRoleAsync(user, Roles.Admin);
                }

                if (db.People.SingleOrDefault(p => p.Email == "bolakhep1850@uhcl.edu") == null)
                {
                    var user = await userManager.FindByNameAsync("testuser");
                    var person = new Person()
                    {
                        LName = "Operator",
                        FName = "System",
                        Email = "bolakhep1850@uhcl.edu",
                        UserId = user.Id
                    };
                    db.People.Add(person);
                }

                await db.SaveChangesAsync();

            }
        }
    }
}
