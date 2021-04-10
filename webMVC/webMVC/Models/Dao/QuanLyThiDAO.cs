
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

using webMVC.Models;

namespace TracNghiemOnline.Modell.Dao
{
    public class QuanLyThiDAO
    {
       webMVC.Models.TracnghiemonlineDB  db = new webMVC.Models.TracnghiemonlineDB();
       

        public KetQuaThi Mark(De_Thi exam)
        {
            exam.Da_SVLuaChon = db.Da_SVLuaChon.Where(x => x.MaDeThi == exam.MaDeThi).ToList();
            int socauDung = 0;
            var Bode =  new webMVC.Modell.Dao.BoDeDao().ChapterStudy(long.Parse(exam.Ma_BoDe.ToString()));

            List<Kho_CauHoi> kho_CauHois = new List<Kho_CauHoi>();
           
          
            foreach (var item in exam.CauHoiDeThis)
            {
                item.Kho_CauHoi = new webMVC.Modell.Dao.BoDeDao().Question(item.Kho_CauHoi.Ma_CauHoi);
                foreach (var item1 in item.Kho_CauHoi.Dap_AN)
                {
                    if (exam.Da_SVLuaChon.ToList().Exists(x => x.Ma_DAN == item1.MA_DAN && item1.TrangThai == true))
                    {
                        socauDung++;         

                    }

                }

            }
            //  Lay Ra So Cau sv da lam dung cua moi chuong
          
            double Hediem = (double)((double)10 / (double)(exam.CauHoiDeThis.Count));
            KetQuaThi ketQuaThi = new KetQuaThi();
            ketQuaThi.Ma_DeThi = exam.MaDeThi;
            ketQuaThi.NgayThi = DateTime.Now;
            ketQuaThi.SoCauDung = socauDung;
            ketQuaThi.SoCauSai = exam.Da_SVLuaChon.Count - socauDung;
            ketQuaThi.DiemSo = Math.Round((double)((double)(socauDung) * (double)(Hediem)), 3);
            exam.Bo_De = Bode;
            //    exam.Bo_De.
            ketQuaThi.De_Thi = exam;   
           
            return ketQuaThi;

        }

        //Lay bài thi cua 1 phong
      
        public De_Thi SearDethi(long? maDeThi)
        {
            De_Thi de_Thi = new De_Thi();

            de_Thi = db.De_Thi.Find(maDeThi);

            if (de_Thi != null)
            {
                de_Thi.CauHoiDeThis = db.CauHoiDeThis.Where(x => x.MaDeThi == de_Thi.MaDeThi).ToList();
                de_Thi.Da_SVLuaChon = db.Da_SVLuaChon.Where(x => x.MaDeThi == de_Thi.MaDeThi).ToList();

                foreach (var item1 in de_Thi.CauHoiDeThis)
                {
                    item1.Kho_CauHoi = new webMVC.Modell.Dao.BoDeDao().Question(long.Parse(item1.MaCauHoi.ToString()));
                    foreach (var item2 in item1.Kho_CauHoi.Dap_AN)
                    {
                        if (de_Thi.Da_SVLuaChon.ToList().Exists(x => x.Ma_DAN == item2.MA_DAN))
                        {
                            item2.TrangThai = true;
                        }
                        else
                        {
                            item2.TrangThai = false;
                        }
                    }



                }

            }
            return de_Thi;
        }
    }
}