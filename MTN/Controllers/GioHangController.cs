using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MTN.Models;
namespace MTN.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        dbQLdienthoaiDataContext db = new dbQLdienthoaiDataContext();
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                //Nếu giỏ hàng chưa tồn tại thì mình tiến hành khởi tao list giỏ hàng (sessionGioHang)
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        //Thêm giỏ hàng
        public ActionResult ThemGioHang(int iMasp, string strURL)
        {
            //Lấy ra session giỏ hàng
            List<GioHang> lstGioHang = LayGioHang();
            //Kiểm tra sp này đã tồn tại trong session[giohang] chưa
            GioHang sanpham = lstGioHang.Find(n => n.iMasp == iMasp);
            if (sanpham == null)
            {
                sanpham = new GioHang(iMasp);
                //Add sản phẩm mới thêm vào list
                lstGioHang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoluong++;
                return Redirect(strURL);
            }
        }
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.iSoluong);
            }
            return iTongSoLuong;
        }
        private double TongTien()
        {
            double dTongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                dTongTien = lstGioHang.Sum(n => n.dThanhtien);
            }
            return dTongTien;
        }
        public ActionResult GioHang()
        {
            List<GioHang> lstGiohang = LayGioHang();
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "PhoneStore");
            }
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstGiohang);
        }
        public ActionResult GiohangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return PartialView();
        }
        //Cập nhật giỏ hàng 
        public ActionResult CapNhatGioHang(int iMaSP, FormCollection f)
        {
            //Lấy giỏ hàng ra từ session
            List<GioHang> lstGioHang = LayGioHang();
            //Kiểm tra sp có tồn tại trong session["GioHang"]
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMasp == iMaSP);
            //Nếu mà tồn tại thì chúng ta cho sửa số lượng
            if (sanpham != null)
            {
                sanpham.iSoluong = int.Parse(f["txtSoLuong"].ToString());

            }
            return RedirectToAction("GioHang");
        }
        //Xóa giỏ hàng
        public ActionResult XoaGioHang(int iMaSP)
        {
            
            //Lấy giỏ hàng ra từ session
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMasp == iMaSP);
            //Nếu mà tồn tại thì chúng ta cho sửa số lượng
            if (sanpham != null)
            {
                lstGioHang.RemoveAll(n => n.iMasp == iMaSP);
                return RedirectToAction("GioHang");
            }
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "PhoneStore");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaTatcaGiohang()
        {
            //Lay gio hang tu Session
            List<GioHang> lstGiohang = LayGioHang();
            lstGiohang.Clear();
            return RedirectToAction("Index", "PhoneStore");
        }
        //Xây dựng 1 view cho người dùng chỉnh sửa giỏ hàng
        public ActionResult SuaGioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "PhoneStore");
            }
            List<GioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);

        }

        [HttpGet]
        public ActionResult DatHang()
        {
            //Kiểm tra đăng đăng nhập
            if (Session["use"] == null || Session["use"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "Nguoidung");
            }
            //Kiểm tra giỏ hàng
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "PhoneStore");
            }
            //Lay gio hang tu Session
            List<GioHang> lstGiohang = LayGioHang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();

            return View(lstGiohang);
        }
        //Xay dung chuc nang Dathang
        [HttpPost]
        public ActionResult DatHang(FormCollection collection)
        {
            //Thêm đơn hàng
            Donhang ddh = new Donhang();
            Nguoidung kh = (Nguoidung)Session["use"];
            List<GioHang> gh = LayGioHang();
            ddh.MaNguoidung = kh.MaNguoiDung;
            ddh.Ngaydat = DateTime.Now;
            db.Donhangs.InsertOnSubmit(ddh);
           
            //Thêm chi tiết đơn hàng
            foreach (var item in gh)
            {
                Chitietdonhang ctDH = new Chitietdonhang();
                ctDH.Madon = ddh.Madon;
                ctDH.Masp = item.iMasp;
                ctDH.Soluong = item.iSoluong;
                ctDH.Dongia = (decimal)item.dDonGia;
                db.Chitietdonhangs.InsertOnSubmit(ctDH);
            }
            db.SubmitChanges();
            Session["Giohang"] = null;
            return RedirectToAction("Xacnhandonhang", "Giohang");
        }
        public ActionResult Xacnhandonhang()
        {
            return View();
        }

    }
}