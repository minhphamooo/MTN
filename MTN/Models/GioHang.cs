using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MTN.Models
{
    public class GioHang
    {
        dbQLdienthoaiDataContext data = new dbQLdienthoaiDataContext();
        public int iMasp { get; set; }
        public string sTensp { get; set; }
        public string sAnhBia { get; set; }
        public double dDonGia { get; set; }
        public int iSoluong { get; set; }
        public double dThanhtien
        {
            get { return iSoluong * dDonGia; }
        }
        //Hàm tạo cho giỏ hàng
        public GioHang(int Masp)
        {
            iMasp = Masp;
            Sanpham sp = data.Sanphams.Single(n => n.Masp == iMasp);
            sTensp = sp.Tensp;
            sAnhBia = sp.Anhbia;
            dDonGia = double.Parse(sp.Giatien.ToString());
            iSoluong = 1;
        }

    }

}