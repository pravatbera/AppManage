using AppManage.Controllers.System;
using Microsoft.AspNetCore.Mvc;

namespace AppManage.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class LoginController : APIBaseController
    {
        [HttpGet]
        [ActionName("Index")]
        public IActionResult Index()
        {
            objreturn.Data = new { Name = "Pravat", Type = "Calling" };
            objreturn.Message = "OK";
            return new JsonResult(objreturn);
        }
    }
}
