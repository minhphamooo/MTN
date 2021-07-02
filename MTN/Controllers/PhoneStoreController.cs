using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MTN.Models;
namespace MTN.Controllers
{
    public class PhoneStoreController : Controller
    {
        // GET: PhoneStore
        dbQLdienthoaiDataContext data = new dbQLdienthoaiDataContext();
        // Ham lay n quyen sach moi
        private List<Sanpham> sanphammoi(int count)
        {
            //Sắp xếp sách theo ngày cập nhật, sau đó lấy top @count 
            return data.Sanphams.OrderByDescending(a => a.Sanphammoi).Take(count).ToList();
        }
        public ActionResult Index(int? page)
        {
            ////Tao bien quy dinh so san pham tren moi trang
            //int pageSize = 5;
            ////Tao bien so trang
            //int pageNum = (page ?? 1);
            return View();

            ////Lấy top 5 Album bán chạy nhất
            
        }
        //        public ActionResult Chude()
        //        {
        //            var chude = from cd in data.c select cd;
        //            return PartialView(chude);
        //        }

        //        public ActionResult Nhaxuatban()
        //        {
        //            var nhaxuatban = from nxb in data.NHAXUATBANs select nxb;
        //            return PartialView(nhaxuatban);
        //        }

        //        public ActionResult SPTheochude(int id)
        //        {
        //            var sach = from s in data.SACHes where s.MaCD == id select s;
        //            return View(sach);
        //        }
        //        public ActionResult SPTheoNXB(int id)
        //        {
        //            var sach = from s in data.SACHes where s.MaNXB == id select s;
        //            return View(sach);
        //        }

        //        public ActionResult Details(int id)
        //        {
        //            var sach = from s in data.SACHes
        //                       where s.Masach == id
        //                       select s;
        //            return View(sach.Single());
        //        }
        //    }
    }
}