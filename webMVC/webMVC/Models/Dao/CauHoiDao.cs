using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Windows.Forms;
using webMVC.Models;

namespace webMVC.Modell.Dao
{
    public class CauHoiDao
    {
        TracnghiemonlineDB tracNghiem = new TracnghiemonlineDB();
        public List<Kho_CauHoi> ListQuestion(long id)
        {
            return tracNghiem.Kho_CauHoi.Where(x => x.Ma_Chuong == id).ToList();
        }

     

        public Kho_CauHoi Question(long id)
        {
            var Question = tracNghiem.Kho_CauHoi.Find(id);
            Question.Dap_AN = lisALLAnsWer(Question.Ma_CauHoi);
            return Question ;
        }

        public long CreatrQuestion(Kho_CauHoi QuesTion)
        {
            try
            {
                
                tracNghiem.Kho_CauHoi.Add(QuesTion);
               
                tracNghiem.SaveChanges();
            }
            catch (Exception e){
                MessageBox.Show(""+e.Message);
                return 0 ; }


            return tracNghiem.Kho_CauHoi.Select(x=>x).ToList().Last().Ma_CauHoi;
        }

        public  void UpdateQuestion(Kho_CauHoi QuesTion)
        {
            var cmd = Question(QuesTion.Ma_CauHoi);
            cmd.MucDo = QuesTion.MucDo;
            cmd.NoiDung = QuesTion.NoiDung;
            cmd.HinhAnh = QuesTion.MucDo;
            tracNghiem.SaveChanges();

        }

        public bool DeleteQuestion(Kho_CauHoi QuesTion)
        {
            try
            {
                tracNghiem.Kho_CauHoi.Remove(QuesTion);
                tracNghiem.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
            
        }

        internal List<Kho_CauHoi> Nuberofquestion(long ma_Chuong, string v)
        {
            List<Kho_CauHoi> kho_CauHois = tracNghiem.Kho_CauHoi.Where(x => x.Ma_Chuong == ma_Chuong && x.MucDo.Equals(v)).ToList();
            foreach (var item in kho_CauHois)
            {
                item.Dap_AN = lisALLAnsWer(item.Ma_CauHoi);
            }
            return kho_CauHois;
        }

        private ICollection<Dap_AN> lisALLAnsWer(long ma_CauHoi)
        {
            return tracNghiem.Dap_AN.Where(x => x.Ma_CauHoi == ma_CauHoi).ToList();
        }       
    }
}