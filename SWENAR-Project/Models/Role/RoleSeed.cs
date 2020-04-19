using Microsoft.AspNetCore.Identity;
using SWENAR.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWENAR.Models
{
    /// <summary>
    /// Application role seeding
    /// </summary>
    public static class RoleSeed
    {
        private static readonly List<Role> DefaultRoles = new List<Role>()
        {
            new Role(){Name = Roles.Customer},
            new Role(){Name = Roles.Admin}
        };

        /// <summary>
        /// Seeding roles needed for the application starts here. Please add, remove, or update the default roles above as needed.
        /// </summary>
        /// <param name="roleManager">Application role manager</param>
        /// <returns>Nothing</returns>
        public static async Task SeedDefaultRolesAsync(RoleManager<Role> roleManager)
        {
            //Add roles and role claims
            foreach (var identityRole in DefaultRoles)
            {
                var result = await roleManager.CreateAsync(identityRole);
            }
        }
    }
}
