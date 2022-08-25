using DevFramework.Core.CrossCuttingConcerns.Security.Web;
using DevFramework.Northwind.Business.Abstract;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DevFramework.Northwind.MvcWebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: Account
        public string Login(string userName, string password)
        {
            // Login ekranından girilen kullanıcı adı, şifre bilgisi Users tablosundan sorgulanacak ve başarılı bir authentication işleminin ardından
            // diğer kullanıcı bilgileri ile birlikte bir cookie yaratılacaktır.
            User user = _userService.GetUserByNameAndPassword(userName, password);

            if (user != null)
            {
                AuthenticationHelper.CreateAuthCookie(new Guid(),
                user.UserName,
                user.Email,
                user.FirstName,
                user.LastName,
                false,
                DateTime.Now.AddDays(7),
                _userService.GetUserRoles(user).Select(r => r.RoleName.ToLower()).ToArray()
                );

                return "User is authenticated!";
            }

            return "User is not authenticated";
        }
    }
}