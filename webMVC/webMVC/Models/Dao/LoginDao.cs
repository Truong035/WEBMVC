using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webMVC.Models;

namespace webMVC.Modell.Dao
{

    public class LoginDao
    {

        TracnghiemonlineDB db = new TracnghiemonlineDB();
        internal TaiKhoan Login(TaiKhoan taiKhoan)
        {
            return db.TaiKhoans.SingleOrDefault(x => x.TaiKhoan1.Trim().Equals(taiKhoan.TaiKhoan1.Trim()) && x.MatKhau.Equals(taiKhoan.MatKhau));
        }
    }
}