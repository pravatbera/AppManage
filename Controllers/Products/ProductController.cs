using AppManage.AppCode.BAL.Master;
using AppManage.AppCode.BAL.Products;
using AppManage.AppCode.BAL.Users;
using AppManage.Controllers.System;
using AppManage.Model.Users;
using AppManage.Models.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AppManage.Controllers.Products
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ProductController: APIBaseController
    {
        private readonly ProductBal r;
        private readonly MasterBal m;
        public ProductController(IConfiguration configuration)
        {
            r = new ProductBal(configuration);
            m = new MasterBal(configuration);
        }
        [HttpPost]
        [ActionName("ProductUnit")]
        public IActionResult ProductUnit()
        {
            try
            {
                var nextRedirect = m.get_unit();
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
        [HttpPost]
        [ActionName("Products")]
        public IActionResult Products()
        {
            try
            {
                var nextRedirect = r.get_products();
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
        [HttpPost]
        [ActionName("AddEditProduct")]
        public async Task<IActionResult> AddEditProduct([FromBody] Product_Md Model)
        {
            try
            {
                var nextRedirect = r.insert_Mst_Product(Model);
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
