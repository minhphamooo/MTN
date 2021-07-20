using MTN.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MTN.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        dbQLdienthoaiDataContext db = new dbQLdienthoaiDataContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult LoginAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginAdmin(FormCollection collection)
        {

            var tendn = collection["UserAdmin"];
            var matkhau = collection["PassAdmin"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải Đăng Nhập :";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải Nhập Mật Khẩu";
            }
            else
            {
                Admin ad = db.Admins.SingleOrDefault(n => n.UserName == tendn && n.PassAdmin == matkhau);
                if (ad != null)
                {
                    Session["Taikhoanadmin"] = ad;
                    return RedirectToAction("Index", "Admin");

                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";

            }
            return RedirectToAction("Login", "Admin");
        }
        public ActionResult DienThoai(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 7;

            return View(db.Sanphams.ToList().OrderBy(n => n.Masp).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("LoginAdmin", "Admin");
            else
                return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, Sanpham sp)
        {

            var CB_Sanpham = collection["Tensp"];
            if (string.IsNullOrEmpty(CB_Sanpham))
            {
                ViewData["Loi"] = "Tên sản phẩm không có";
            }
            else
            {
                sp.Tensp = CB_Sanpham;
                db.Sanphams.InsertOnSubmit(sp);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();

        
    }
        public ActionResult Details(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("LoginAdmin", "Admin");
            else
            {
                var Details_sp = db.Sanphams.Where(m => m.Masp == id).First();
                return View(Details_sp);
            }

        }
        public ActionResult Edit(int id)
        {
            var Ed_ = db.Sanphams.First(m => m.Masp == id);
            return View(Ed_);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {

            var edit = db.Sanphams.First(m => m.Masp == id);
            var ED_Sanpham = collection["Tensp"];
            edit.Masp = id;
            if (string.IsNullOrEmpty(ED_Sanpham))
            {
                ViewData["Loi"] = "Tên hãng không có";
            }
            else
            {
                edit.Tensp = ED_Sanpham;
                UpdateModel(edit);
                db.SubmitChanges();
                return RedirectToAction("DienThoai");
            }
            return this.Edit(id);
        }
        public ActionResult Delete(int id)
        {

            var DL = db.Sanphams.First(m => m.Masp == id);
            return View(DL);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {

            var delete = db.Sanphams.Where(m => m.Masp == id).First();
            db.Sanphams.DeleteOnSubmit(delete);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }

    
    //Chinh sửa sản phẩm
    [HttpGet]
        public ActionResult Suasp(int id)
        {
            //Lay ra doi tuong sach theo ma
            Sanpham sp = db.Sanphams.SingleOrDefault(n => n.Masp == id);
            ViewBag.Masp = sp.Masp;
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Dua du lieu vao dropdownList
            //Lay ds tu tabke chu de, sắp xep tang dan trheo ten chu de, chon lay gia tri Ma CD, hien thi thi Tenchude
            ViewBag.MaHang = new SelectList(db.Hangsanxuats.ToList().OrderBy(n => n.Tenhang), "Mahang", "Tenhang");
            ViewBag.Mahdh = new SelectList(db.Hedieuhanhs.ToList().OrderBy(n => n.Mahdh), "Mahdh", "Tenhdh");
            return View(sp);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Suasp(Sanpham sp, HttpPostedFileBase fileUpload)
        {
            //Dua du lieu vao dropdownload
            ViewBag.MaHang = new SelectList(db.Hangsanxuats.ToList().OrderBy(n => n.Tenhang), "Mahang", "Tenhang");
            ViewBag.Mahdh = new SelectList(db.Hedieuhanhs.ToList().OrderBy(n => n.Mahdh), "Mahdh", "Tenhdh");
            //Kiem tra duong dan file
            if (fileUpload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();
            }
            //Them vao CSDL
            else
            {
                if (ModelState.IsValid)
                {
                    //Luu ten fie, luu y bo sung thu vien using System.IO;
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    //Luu duong dan cua file
                    var path = Path.Combine(Server.MapPath("~/Hinhsanpham"), fileName);
                    //Kiem tra hình anh ton tai chua?
                    if (System.IO.File.Exists(path))
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    else
                    {
                        //Luu hinh anh vao duong dan
                        fileUpload.SaveAs(path);
                    }
                    sp.Anhbia = fileName;
                    //Luu vao CSDL   
                    UpdateModel(sp);
                    db.SubmitChanges();

                }
                return RedirectToAction("Sach");
            }
        }

      
    }
} 
  