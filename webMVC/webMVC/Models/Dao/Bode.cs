using System;
using System.Collections.Generic;
using System.Linq;
using webMVC.Models;
namespace webMVC.Modell.Dao
{
    public class Bode
    {

       webMVC.Models.TracnghiemonlineDB db = new TracnghiemonlineDB();


        public List<Kho_CauHoi> listkhocauhoi(long mabd)
        {

            var cauhoi = db.CauHois.Where(x => x.Ma_BoDe == mabd).ToList();
            List<Kho_CauHoi> khoch = new List<Kho_CauHoi>();
            var khocauhoi = db.Kho_CauHoi.Select(x => x).ToList();
            var dapan = db.Dap_AN.Select(x => x).ToList();

            
            foreach (var a in cauhoi)
            {
                Kho_CauHoi ch = new Kho_CauHoi();
                foreach (var b in khocauhoi)
                {
                    
                    
                    foreach(var c in dapan)
                    {
                        if(a.Ma_CauHoi==b.Ma_CauHoi && b.Ma_CauHoi == c.Ma_CauHoi)
                        {
                            Dap_AN da = new Dap_AN();
                            ch.NoiDung = b.NoiDung;
                            ch.HinhAnh = b.HinhAnh;
                            ch.MucDo = b.MucDo;
                            ch.Ma_Chuong = b.Ma_Chuong;
                            ch.TrangThai = b.TrangThai;
                            da.NoiDung = c.NoiDung;
                            da.TrangThai = c.TrangThai;
                            da.Ma_CauHoi = c.Ma_CauHoi;
                            da.MA_DAN = c.MA_DAN;
                            da.HinhAnh = c.HinhAnh;
                            ch.Dap_AN.Add(da);

                        }
                    }
                    
                }
                
                khoch.Add(ch);
            }
                

            return khoch;
        }  

    }
}