﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MTN.Models;
namespace MTN.Controllers
{
    public class DienThoaiController : Controller
    {
        dbQLdienthoaiDataContext data = new dbQLdienthoaiDataContext();
        // GET: DienThoai
        public ActionResult Phone()
        {
            return View();
        }
    }
}