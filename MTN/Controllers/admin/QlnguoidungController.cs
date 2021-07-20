using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MTN.Models;
namespace MTN.Controllers.admin
{
    public class QlnguoidungController : Controller
    {
        dbQLdienthoaiDataContext data = new dbQLdienthoaiDataContext();
        // GET: Qlnguoidung
        public ActionResult Index()
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("LoginAdmin", "Admin");
            else
                return View(data.Nguoidungs.ToList());
        }
        public ActionResult Details(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("LoginAdmin", "Admin");
            else
            {
                var ND = data.Nguoidungs.Where(m => m.MaNguoiDung == id).First();
                return View(ND);
            }

        }
        public ActionResult Create()
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("LoginAdmin", "Admin");
            else
                return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, Nguoidung nd)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("LoginAdmin", "Admin");
            else
            {
                var CR_Nguoidung = collection["Hoten"];
                if (string.IsNullOrEmpty(CR_Nguoidung))
                {
                    ViewData["Loi"] = "Tên đơn hàng không có";
                }
                else
                {
                    nd.Hoten = CR_Nguoidung;
                    data.Nguoidungs.InsertOnSubmit(nd);
                    data.SubmitChanges();
                    return RedirectToAction("Index");
                }
            }
            return this.Create();
        }
        public ActionResult Edit(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("LoginAdmin", "Admin");
            else
            {
                var Ed_ = data.Nguoidungs.First(m => m.MaNguoiDung == id);
                return View(Ed_);
            }
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("LoginAdmin", "Admin");
            else
            {
                var edit = data.Nguoidungs.First(m => m.MaNguoiDung == id);
                var ED_Hangsanxuat = collection["Hoten"];
                edit.MaNguoiDung = id;
                if (string.IsNullOrEmpty(ED_Hangsanxuat))
                {
                    ViewData["Loi"] = "Tên đơn hàng không có";
                }
                else
                {
                    edit.Hoten = ED_Hangsanxuat;
                    UpdateModel(edit);
                    data.SubmitChanges();
                    return RedirectToAction("Index");
                }
            }
            return this.Edit(id);
        }
        public ActionResult Delete(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("LoginAdmin", "Admin");
            else
            {
                var DL = data.Nguoidungs.First(m => m.MaNguoiDung == id);
                return View(DL);
            }
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("LoginAdmin", "Admin");
            else
            {
                var delete = data.Nguoidungs.Where(m => m.MaNguoiDung == id).First();
                data.Nguoidungs.DeleteOnSubmit(delete);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
        }

    }
}
