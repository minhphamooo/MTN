using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MTN.Models;
namespace MTN.Controllers.admin
{
    public class DonhangController : Controller
    {
        dbQLdienthoaiDataContext data = new dbQLdienthoaiDataContext();
        // GET: Donhang
        public ActionResult Index()
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("LoginAdmin", "Admin");
            else
                return View(data.Donhangs.ToList());
        }
        public ActionResult Details(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("LoginAdmin", "Admin");
            else
            {
                var DH = data.Donhangs.Where(m => m.Madon == id).First();
                return View(DH);
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
        public ActionResult Create(FormCollection collection, Donhang dh)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("LoginAdmin", "Admin");
            else
            {
                var CR_Donhang = collection["Datthanhtoan"];
                if (string.IsNullOrEmpty(CR_Donhang))
                {
                    ViewData["Loi"] = "Tên đơn hàng không có";
                }
                else
                {
                    dh.Datthanhtoan = false;
                    data.Donhangs.InsertOnSubmit(dh);
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
                var Ed_ = data.Donhangs.First(m => m.Madon == id);
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
                var edit = data.Donhangs.First(m => m.Madon == id);
                var ED_Hangsanxuat = collection["Datthanhtoan"];
                edit.Madon = id;
                if (string.IsNullOrEmpty(ED_Hangsanxuat))
                {
                    ViewData["Loi"] = "Tên đơn hàng không có";
                }
                else
                {
                    edit.Datthanhtoan = false;
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
                var DL = data.Donhangs.First(m => m.Madon == id);
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
                var delete = data.Donhangs.Where(m => m.Madon == id).First();
                data.Donhangs.DeleteOnSubmit(delete);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
        }

    }
}
