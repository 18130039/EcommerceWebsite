namespace EcommerceWebsite.Models.Cart
{
    public class CartLine
    {
        public int CartLineID { get; set; }
        public TDanhMucSp Product { get; set; } = new();
        public int Quantity { get; set; }
       
    }
}
