using EcommerceWebsite.Models;

namespace EcommerceWebsite.Repository
{
    public interface ILoaiSpRepository
    {
        TLoaiSp GetLoaiSp(String maLoaiSp);
        IEnumerable<TLoaiSp> GetAllLoaiSp();  

    }
}
