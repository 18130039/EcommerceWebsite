namespace EcommerceWebsite.Models
{
    public class CartItemModel
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Image {  get; set; }
        public decimal PriceTotal { get { return Quantity * Price; } }
        public CartItemModel() { }
        public CartItemModel(TDanhMucSp product)
        {
            ProductId = product.MaSp;
            ProductName = product.TenSp;
            Price = (decimal)product.GiaNhoNhat;
            Quantity = 1;
            Image = product.AnhDaiDien;
        }
    }
}
