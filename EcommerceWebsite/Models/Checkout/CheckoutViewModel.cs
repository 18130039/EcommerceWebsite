using EcommerceWebsite.Models.Cart;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EcommerceWebsite.Models.Checkout
{
    public class CheckoutViewModel
    {
        [NotMapped]
        [Display(Name = "Ho va ten")]
        public string Name { get; set; }

        /*        [Display(Name = "Dia chi chi tiet")]
                public string Address { get; set; }

                [Display(Name = "Quan/huyen")]
                public string District { get; set; }

                [Display(Name = "Tinh/Thanh pho")]
                public string Province { get; set; }*/
        [NotMapped]
        [Display(Name = "Dien thoai")]
        public string Phone { get; set; }
        /*public List<CartLine> listCartItems { get; set; }*/
    }
}
