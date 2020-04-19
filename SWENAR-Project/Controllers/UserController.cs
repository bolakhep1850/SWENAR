using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWENAR.Data;
using SWENAR.Identity;
using SWENAR.Models;
using SWENAR.ViewModels;
using SWENAR.Validation;

namespace SWENAR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SWENARDBContext _db;
        private readonly UserManager<User> _userManager;

        public UserController(SWENARDBContext db, UserManager<User> userManager)
        {
            this._db = db;
            this._userManager = userManager;
        }

        /// <summary>
        /// Create a user for a person
        /// </summary>
        /// <param name="vm">User Create View Model</param>
        /// <returns>New user</returns>
        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<bool>> Create(UserCreateVm vm)
        {

            var person = await _db.People
                .Include(p => p.Customer)
                .SingleOrDefaultAsync(p => p.Id == vm.PersonId);

            if (person.UserId == null)
            {
                var user = new User()
                {
                    UserName = vm.UserName
                };

                var userResult = await _userManager.CreateAsync(user, "Password@1");

                //if (userResult.Succeeded)
                //{
                //    var newUser = await _db.Users.SingleOrDefaultAsync(u => u.UserName == user.UserName);
                //    var roleResult = await _userManager.AddToRoleAsync(user, Roles.Admin);

                //    if (roleResult.Succeeded)
                //    {
                //        person.UserId = user.Id;
                //        user.Claims.Add(new IdentityUserClaim<int> { ClaimType = Claims.UserId, ClaimValue = user.Id.ToString() });

                //        if (person.CustomerId != null)
                //        {
                //            user.Claims.Add(new IdentityUserClaim<int>
                //            {
                //                ClaimType = Claims.CustomerId,
                //                ClaimValue = person.CustomerId.ToString()
                //            });
                //        }
                //        var result = await _db.SaveChangesAsync();

                //        if (result > 0)
                //        {
                //            return true;
                //        }

                //    }
                //}
                if (userResult.Succeeded)
                {
                   return true;
                }
            }

            return false;
        }
    }
}