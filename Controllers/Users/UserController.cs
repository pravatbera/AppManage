using AppManage.AppCode.BAL.Users;
using AppManage.Controllers.System;
using AppManage.Model.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AppManage.Controllers.Users
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : APIBaseController
    {
        private readonly UserBal r;
        public UserController(IConfiguration configuration)
        {
            r = new UserBal(configuration);
        }
        [HttpPost]
        [ActionName("UsersDeatiles")]
        public IActionResult UsersDeatiles()
        {
            try
            {
                var nextRedirect = r.UsersDeatiles();
                objreturn.Data = nextRedirect;
                objreturn.Message = "Success";
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
