using PhanCongGiangDay.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhanCongGiangDay.Api.Models
{
    public interface INhomLopRepository
    {
        Task<IEnumerable<NhomLop>> GetAllNhomLop();
        Task<IEnumerable<NhomLop>> GetNhomLopChuaPhanCong();
        Task<NhomLop> GetNhomLopByIdNhomLop(int nhomLopId);
        Task<NhomLop> CreateNhomLop(NhomLop nhomLop);
        Task<NhomLop> DeleteNhomLop(int nhomLopId);
        Task<NhomLop> UpdateNhomLop(NhomLop nhomLop);

        Task<IEnumerable<NhomLop>> CreateListNhomLop(List<NhomLop> listNhomLop);
    }
}
