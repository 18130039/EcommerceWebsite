using EcommerceWebsite.Models.Cart;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
namespace EcommerceWebsite.Models.Checkout
{
    public class CheckoutViewModel
    {
        [Display(Name = "Ho va ten")]
        public string Name { get; set; }

        [Display(Name = "Dia chi chi tiet")]
        public string Address { get; set; }

        [Display(Name = "Quan/huyen")]
        public string District { get; set; }

        [Display(Name = "Tinh/Thanh pho")]
        public string Province { get; set; }

        [Display(Name = "Dien thoai")]
        public string Phone { get; set; }
/*        public List<CartLine> listCartItems { get; set; }*/
    }
}
