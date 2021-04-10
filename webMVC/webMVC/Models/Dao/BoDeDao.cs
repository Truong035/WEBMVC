using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

using webMVC.Models;

namespace webMVC.Modell.Dao
{
    public class BoDeDao
    {
        webMVC.Models.TracnghiemonlineDB db = new TracnghiemonlineDB();

      
        public Bo_De ChapterStudy(long id)
        {
            var bode = db.Bo_De.Find(id);
            bode.MonHoc = db.MonHocs.Find(long.Parse(bode.Ma_Mon.ToString()));
            ListChapterQuestion(bode);
            return bode;
        }
      
        public void CreateChapterStudy(Bo_De bo_De1)
        {
            Bo_De bo_De = new Bo_De();
            bo_De.NoiDung = bo_De1.NoiDung;
            bo_De.ThoiGianThi = bo_De1.ThoiGianThi;
            bo_De.Ma_Mon = bo_De1.Ma_Mon;
            bo_De.Ma_NguoiTao = bo_De1.Ma_NguoiTao;
       
            bo_De.Xoa = true;
            foreach (var item in bo_De1.CauHois)
            {
                item.Kho_CauHoi = null;
                bo_De.CauHois.Add(item);  
            }
            bo_De.SoCau = bo_De.CauHois.Count;
            try
            {
                db = new TracnghiemonlineDB();
                db.Bo_De.Add(bo_De);
                db.SaveChanges();
             
            }catch(Exception e)
            {
                //MessageBox.Show(e.Message);
            }
      
        }


        public De_Thi MixExemQuestion(Bo_De bo_De ,string SV)
        {
            Random random = new Random();
            De_Thi de_Thi = new De_Thi();
            de_Thi.Ma_BoDe = bo_De.Ma_BoDe;
           de_Thi.Ma_SV = SV;
            de_Thi.TrangThai = true;
            var lisQuestion = (List<CauHoi>)bo_De.CauHois;
            int Lenght = lisQuestion.Count;

            for (int i = 0; i < Lenght; i++)
            {
                int vt = random.Next(lisQuestion.Count);

                CauHoiDeThi cauHoi = new CauHoiDeThi();
                cauHoi.MaCauHoi = lisQuestion[vt].Ma_CauHoi;

                de_Thi.CauHoiDeThis.Add(cauHoi);
                lisQuestion.RemoveAt(vt);
            }
            de_Thi.TrangThai = true;
            db.De_Thi.Add(de_Thi);
            db.SaveChanges();
            de_Thi = db.De_Thi.Where(x =>x.Ma_SV.Equals(SV)).ToList().Last();
            de_Thi.CauHoiDeThis = db.CauHoiDeThis.Where(x => x.MaDeThi == de_Thi.MaDeThi).ToList();
           de_Thi.Bo_De= ChapterStudy(long.Parse(de_Thi.Ma_BoDe.ToString()));
            de_Thi.TaiKhoan = db.TaiKhoans.Find(de_Thi.Ma_SV);
            foreach (var item in de_Thi.CauHoiDeThis)
            {
                item.Kho_CauHoi =Question(long.Parse(item.MaCauHoi.ToString()));
                foreach (var item1 in item.Kho_CauHoi.Dap_AN)
                {
                    item1.TrangThai = false;
                }
            }
            return de_Thi;
        }
        public Kho_CauHoi Question(long id)
        {
            var Question = db.Kho_CauHoi.Find(id);
            Question.Dap_AN = lisALLAnsWer(Question.Ma_CauHoi);
            return Question;
        }
        private ICollection<Dap_AN> lisALLAnsWer(long ma_CauHoi)
        {
            return db.Dap_AN.Where(x => x.Ma_CauHoi == ma_CauHoi).ToList();
        }

        internal void OptionStudent(De_Thi examQuestion)
        {
            var ListOptionStudent = db.Da_SVLuaChon.Where(x => x.MaDeThi == examQuestion.MaDeThi).ToList();
            if (ListOptionStudent.Count > 0)
            {
                foreach (var item in ListOptionStudent)
                {
                    db.Da_SVLuaChon.Remove(item);
                    db.SaveChanges();
                }
            }
            foreach (var item in examQuestion.CauHoiDeThis)
            {
                foreach (var item1 in item.Kho_CauHoi.Dap_AN)
                {
                    if (item1.TrangThai == true)
                    {
                        Da_SVLuaChon da_SVLuaChon = new Da_SVLuaChon();
                        da_SVLuaChon.Ma_DAN = item1.MA_DAN;
                        da_SVLuaChon.MaDeThi = examQuestion.MaDeThi;
                        db.Da_SVLuaChon.Add(da_SVLuaChon);
                        db.SaveChanges();
                    }

                }

            }

        }

   
       
        public List<Bo_De> ListALLChapterStudy()
        {
            var bode = db.Bo_De.Where(x=>x.Xoa==true).ToList();
         
            return bode;
        }

        public void ListChapterQuestion(Bo_De bo_De)
        {
           bo_De.CauHois = db.CauHois.Where(x => x.Ma_BoDe==bo_De.Ma_BoDe).ToList();
         
        }




    }
}