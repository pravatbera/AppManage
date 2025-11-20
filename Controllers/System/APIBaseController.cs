using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AppManage.Controllers.System
{
    public class APIBaseController : Controller
    {
        public ClintJsonResult objreturn = new ClintJsonResult()
        {
            Status = HttpStatusCode.OK
        };
    }
}
