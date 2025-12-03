namespace AppManage.Models.Products
{
    public class Product_Md
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int UnitID { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public long UserID { get; set; }
    }
}
