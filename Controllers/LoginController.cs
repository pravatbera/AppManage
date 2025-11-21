using AppManage.AppCode.BAL;
using AppManage.Controllers.System;
using AppManage.Model.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Net;
using static Azure.Core.HttpHeader;
using System.Security.Cryptography;
using AppManage.AppCode.DAL.Users;
using AppManage.AppCode.BAL.Users;

namespace AppManage.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class LoginController : APIBaseController
    {
        private readonly LoginBal r;
        public LoginController(IConfiguration configuration)
        {
            r = new LoginBal(configuration);
        }

        [HttpGet]
        [ActionName("Index")]
        public IActionResult Index()
        {
            objreturn.Data = new { Name = "Pravat", Type = "Calling" };
            objreturn.Message = "OK";
            return new JsonResult(objreturn);
        }
        [HttpPost]
        [ActionName("UserAuthentication")]
        public IActionResult UserAuthentication([FromBody] User Model)
        {
            try
            {
                var nextRedirect = r.UserAuthentication(Model);
                objreturn.Data = nextRedirect;
                objreturn.Message = "Logged in successfully";
                objreturn.Status = HttpStatusCode.OK;
            }
            catch (Exception ex )
            {
                objreturn.Message = ex.Message;
                objreturn.Status = HttpStatusCode.NotFound;
            }
            return new JsonResult(objreturn);
        }
    }
}
