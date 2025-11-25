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
using System.Reflection;

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
        [HttpPost]
        [ActionName("UserRegistration")]
        public async Task<IActionResult> UserRegistration([FromForm] User Model)
        {
            try
            {
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Document", "AccountImages");

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                if (Model.file != null && Model.file.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(Model.file.FileName);
                    string filePath = Path.Combine(folderPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Model.file.CopyToAsync(stream);
                    }

                    Model.ProfileImage = "/AccountImages/" + fileName;  // save path to DB
                }

                    var nextRedirect = r.UserRegistration(Model);
                objreturn.Data = nextRedirect;
                objreturn.Message = "Success!";
                objreturn.Status = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                objreturn.Message = ex.Message;
                objreturn.Status = HttpStatusCode.NotFound;
            }
            return new JsonResult(objreturn);
        }
    }
}
