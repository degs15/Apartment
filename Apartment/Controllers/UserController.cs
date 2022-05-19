using Apartment.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Apartment.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        ApplicationDbContext context;
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        public UserController()
        {
            context = new ApplicationDbContext();
        }

        // GET: User
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ViewBag.Name = user.Name;
                
                ViewBag.displayMenu = "No";

                if (isAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }
                return View();
            }
            else
            {
                ViewBag.Name = "Not Logged IN";
            }
            return View();

        }

        public ActionResult getAllUser()
        {

            var listOfUsers = context.Users.Select(x => new UserModel { Id = x.Id, Name = x.UserName, Email = x.Email }).ToList();



            return View(listOfUsers);

        }

        // GET: /Manage/ChangePassword
        public ActionResult changePassword(string id)
        {
            ChangePWD chModel = new ChangePWD();

            chModel.Id = id;

            return View(chModel);
        }


        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> changePassword(ChangePWD model)
        {


            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var token = await UserManager.GeneratePasswordResetTokenAsync(model.Id);

            var result = await UserManager.ResetPasswordAsync(model.Id, token, model.NewPassword);


            if (result.Succeeded)
            {

                return RedirectToAction("getAllUser", new { Message = "Change Password Successfully!" });
            }
            else
            {
                string errorString = "";

                foreach (var err in result.Errors)
                {
                    errorString += err;
                }
                ModelState.AddModelError("", errorString);

            }

            return RedirectToAction("getAllUser");
        }

        public async Task<ActionResult> deleteUser(string id)
        {
            var user = await UserManager.FindByIdAsync(id);

            var result = await UserManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("getAllUser", new { Message = "User Successfully Deleted!" });
            }

            return RedirectToAction("getAllUser", new { Message = "Unable to Delete User!" });

        }
    

        [NonAction]
        public Boolean isAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Admin")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}