using AppManage.AppCode.DAL.System;
using AppManage.Models.Master;
using AppManage.Models.Products;

namespace AppManage.AppCode.DAL.Products
{
    public class ProductDal : DalBase
    {
        public ProductDal(IConfiguration configuration) : base(configuration)
        {
        }
        internal List<Product_Md> get_products()
        {
            var cmd = NewCommand("get_products");
            return GetResult(cmd).Convert<Product_Md>(); ;
        }
    }
}
