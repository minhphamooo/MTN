using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MTN.Models;
namespace MTN.Controllers
{
    // GET: Sanpham
    public class SanphamController : Controller
    {
        dbQLdienthoaiDataContext db = new dbQLdienthoaiDataContext();

        // GET: Sanpham
        public ActionResult dtiphonepartial()
        {
            var ip = db.Sanphams.Where(n => n.Mahang == 2).Take(4).ToList();
            return PartialView(ip);
        }
        public ActionResult dtsamsungpartial()
        {
            var ss = db.Sanphams.Where(n => n.Mahang == 1).Take(4).ToList();
            return PartialView(ss);
        }
        public ActionResult dtxiaomipartial()
        {
            var mi = db.Sanphams.Where(n => n.Mahang == 3).Take(4).ToList();
            return PartialView(mi);
        }
         public ActionResult smarphone()
        {
            var mi = db.Sanphams.Where(n => n.Mahang == 19).Take(4).ToList();
            return PartialView(mi);
        }
        public ActionResult Anker()
        {
            var mi = db.Sanphams.Where(n => n.Mahang == 4).Take(4).ToList();
            return PartialView(mi);
        }
        public ActionResult JBL()
        {
            var mi = db.Sanphams.Where(n => n.Mahang == 5).Take(4).ToList();
            return PartialView(mi);
        }
        public ActionResult PIN ()
        {
            var mi = db.Sanphams.Where(n => n.Mahang == 20).Take(4).ToList();
            return PartialView(mi);
        }
        public ActionResult Sony()
        {
            var mi = db.Sanphams.Where(n => n.Mahang == 6).Take(4).ToList();
            return PartialView(mi);
        }
        public ActionResult HP()
        {
            var mi = db.Sanphams.Where(n => n.Mahang == 16).Take(4).ToList();
            return PartialView(mi);
        }
        public ActionResult Acer()
        {
            var mi = db.Sanphams.Where(n => n.Mahang == 17).Take(4).ToList();
            return PartialView(mi);
        }
        public ActionResult Lenovo()
        {
            var mi = db.Sanphams.Where(n => n.Mahang == 18).Take(4).ToList();
            return PartialView(mi);
        }

        //public ActionResult dttheohang()
        //{
        //    var mi = db.Sanphams.Where(n => n.Mahang == 5).Take(4).ToList();
        //    return PartialView(mi);
        //}
        public ActionResult xemchitiet(int Masp = 0)
        {
            var chitiet = db.Sanphams.SingleOrDefault(n => n.Masp == Masp);
            if (chitiet == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(chitiet);
        }


    }
}