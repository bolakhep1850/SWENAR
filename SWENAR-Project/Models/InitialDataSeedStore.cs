using Microsoft.AspNetCore.Identity;
using SWENAR.Data;
using SWENAR.Models;
using System.Threading.Tasks;

namespace SWENAR.SeedCommon
{
    /// <summary>
    /// This class is used to seed initial data.
    /// </summary>
    public class InitialDataSeedStore
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly SWENARDBContext _db;

        public InitialDataSeedStore(
            RoleManager<Role> roleManager,
            UserManager<User> userManager,
            SWENARDBContext db)
        {
            this._roleManager = roleManager;
            this._userManager = userManager;
            this._db = db;
        }

        /// <summary>
        /// All seed methods are called from here
        /// </summary>
        /// <returns>Nothing</returns>
        public async Task EnsureSeedData()
        {
            await RoleSeed.SeedDefaultRolesAsync(_roleManager);
            await _db.SeedDefaultUserAsync(_userManager);
        }
    }
}
