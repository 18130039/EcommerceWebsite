namespace EcommerceWebsite.Models.Cart
{
    public class Cart
    {
        public List<CartLine> Lines { get; set; } = new List<CartLine>();
        public void AddItem(TDanhMucSp product, int quantity)
        {
            CartLine? line = Lines
            .Where(p => p.Product.MaSp == product.MaSp)
            .FirstOrDefault();
            if (line == null)
            {
                Lines.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public void RemoveLine(TDanhMucSp product) =>
        Lines.RemoveAll(l => l.Product.MaSp == product.MaSp);
        public decimal ComputeTotalValue() =>
        (decimal)Lines.Sum(e => e.Product.GiaLonNhat * e.Quantity);
        public void Clear() => Lines.Clear();
    }
}

