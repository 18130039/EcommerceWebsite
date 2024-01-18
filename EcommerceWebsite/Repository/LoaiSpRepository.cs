using EcommerceWebsite.Models;

namespace EcommerceWebsite.Repository
{
    public class LoaiSpRepository : ILoaiSpRepository
    {
        private readonly QlbanVaLiContext _context;
        public LoaiSpRepository(QlbanVaLiContext context)
        {
            _context = context;
        }
        public IEnumerable<TLoaiSp> GetAllLoaiSp()
        {
            return _context.TLoaiSps;
        }

        public TLoaiSp GetLoaiSp(string maLoaiSp)
        {
            return _context.TLoaiSps.Find(maLoaiSp);
        }
    }
}
