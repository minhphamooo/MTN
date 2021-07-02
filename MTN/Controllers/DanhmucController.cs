using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MTN.Models;
namespace MTN.Controllers
{
    public class DanhmucController : Controller
    {
        // GET: Danhmuc
        dbQLdienthoaiDataContext db = new dbQLdienthoaiDataContext();
        // GET: Danhmuc
        public ActionResult DanhmucPartial()
        {
            var danhmuc = db.Hangsanxuats.ToList();
            return PartialView(danhmuc);
        }
    }
}