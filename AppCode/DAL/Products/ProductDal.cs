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
        internal DBMessage insert_Mst_Product(Product_Md model)
        {
            var cmd = NewCommand("insert_Mst_Product");
            cmd.Parameters.AddWithValue("@ProductID", model.ProductID);
            cmd.Parameters.AddWithValue("@ProductName", model.ProductName);
            cmd.Parameters.AddWithValue("@UnitID", model.UnitID);
            cmd.Parameters.AddWithValue("@Price", model.Price);
            cmd.Parameters.AddWithValue("@IsActive", model.IsActive);
            cmd.Parameters.AddWithValue("@InsertedBy", model.UserID);
            return GetResult(cmd).Convert<DBMessage>().FirstOrDefault();
        }

    }
}
