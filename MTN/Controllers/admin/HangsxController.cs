using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MTN.Models;
namespace MTN.Controllers
{
    public class HangsxController : Controller
    {
        // GET: Hangsx
        dbQLdienthoaiDataContext db = new dbQLdienthoaiDataContext();
        public ActionResult Index()
        {

            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("LoginAdmin", "Admin");
            else
                return View(db.Hangsanxuats.ToList());
        }
        public ActionResult Details(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("LoginAdmin", "Admin");
            else {
                var Details_sx = db.Hangsanxuats.Where(m => m.Mahang == id).First();
                return View(Details_sx);
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
        public ActionResult Create(FormCollection collection, Hangsanxuat hsx)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("LoginAdmin", "Admin");
            else
            {
                var CB_Hangsanxuat = collection["Tenhang"];
                if (string.IsNullOrEmpty(CB_Hangsanxuat))
                {
                    ViewData["Loi"] = "Tên hãng không có";
                }
                else
                {
                    hsx.Tenhang = CB_Hangsanxuat;
                    db.Hangsanxuats.InsertOnSubmit(hsx);
                    db.SubmitChanges();
                    return RedirectToAction("Index");
                }
                return this.Create();
            }
        }
        public ActionResult Edit(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("LoginAdmin", "Admin");
            else
            {
                var Ed_ = db.Hangsanxuats.First(m => m.Mahang == id);
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
                var edit = db.Hangsanxuats.First(m => m.Mahang == id);
                var ED_Hangsanxuat = collection["Tenhang"];
                edit.Mahang = id;
                if (string.IsNullOrEmpty(ED_Hangsanxuat))
                {
                    ViewData["Loi"] = "Tên hãng không có";
                }
                else
                {
                    edit.Tenhang = ED_Hangsanxuat;
                    UpdateModel(edit);
                    db.SubmitChanges();
                    return RedirectToAction("Index");
                }
                return this.Edit(id);
            }
        }
        public ActionResult Delete(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("LoginAdmin", "Admin");
            else
            {
                var DL = db.Hangsanxuats.First(m => m.Mahang == id);
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
                var delete = db.Hangsanxuats.Where(m => m.Mahang == id).First();
                db.Hangsanxuats.DeleteOnSubmit(delete);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
        }
         
        }
}