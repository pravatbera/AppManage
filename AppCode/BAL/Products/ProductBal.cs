using AppManage.AppCode.DAL.Products;
using AppManage.AppCode.DAL.Users;
using AppManage.Model.Users;
using AppManage.Models.Products;

namespace AppManage.AppCode.BAL.Products
{
    public class ProductBal
    {
        private readonly ProductDal r;
        private readonly IConfiguration _config;

        public ProductBal(IConfiguration configuration)
        {
            _config = configuration;
            r = new ProductDal(configuration);
        }
        internal List<Product_Md> get_products()
        {
            return r.get_products();
        }
        internal DBMessage insert_Mst_Product(Product_Md model)
        {
            return r.insert_Mst_Product(model);
        }
    }
}
