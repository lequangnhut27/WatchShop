using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchShop.Data.Infrastructure;
using WatchShop.Data.Models;
using WatchShop.Data.Repositories;

namespace WatchShop.Application.Services
{
    public interface IDongHoService
    {
        List<DongHo> GetDongHoGiamGia();
        List<DongHo> GetDongHoThoiTrang();
        List<DongHo> GetDongHoThongMinh();
        
    }    
    public class DongHoService : IDongHoService
    {
        private readonly IDongHoRepository _dongHoRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DongHoService(IDongHoRepository dongHoRepository, IUnitOfWork unitOfWork)
        {
            _dongHoRepository = dongHoRepository;
            _unitOfWork = unitOfWork;
        }

        public List<DongHo> GetDongHoGiamGia()
        {
            return _dongHoRepository.GetMany(x => x.MaGG != null).ToList();
        }
        public List<DongHo> GetDongHoThoiTrang()
        {
            return _dongHoRepository.GetMany(x => x.MaDH == x.DongHoThoiTrang.MaDH).ToList();
        }
        public List<DongHo> GetDongHoThongMinh()
        {
            return _dongHoRepository.GetMany(x => x.MaDH == x.DongHoThongMinh.MaDH).ToList();
        }
    }
}