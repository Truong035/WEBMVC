using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using TracNghiemOnline.Modell.Dao;
using webMVC.Modell.Dao;
using webMVC.Models;
using EXCELL = Microsoft.Office.Interop.Excel;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using System.Drawing;
using System.IO;
using System.Net;
using Spire.Pdf.Graphics;
using Spire.Pdf;
using System.Net.Mail;

namespace webMVC.Controllers
{
    public class HomeController :BaseController
    {
        public ActionResult Index(string id)
        {         
            var bode = new Models.TracnghiemonlineDB().Bo_De.Where(x => x.Ma_Mon == 1).ToList();
            try
            {
                if (id.Length > 0)
                {
                     bode = new Models.TracnghiemonlineDB().Bo_De.Where(x => x.Ma_Mon.ToString().Equals(id.Trim()) && x.Xoa==true).ToList();
                    var mon = new TracnghiemonlineDB().MonHocs.Find(long.Parse(id));
                    ViewBag.danhmuc = mon;
                }
            }
            catch {
                 new Models.TracnghiemonlineDB().Bo_De.Where(x => x.Ma_Mon == 1 && x.Xoa==true).ToList();
                MonHoc mon = new MonHoc();
                mon.MaMon = 1;
                mon.TenMon = "Công nghệ thông tin";
                ViewBag.danhmuc = mon;
            }
            var TK = (TaiKhoan)Session[webMVC.ComMonStants.UserLogin];
            ViewBag.TenDangNhap = TK.TenDangNhap;


            return View(bode);
        }
        public void save_file_from_url(string file_name, string url)
        {
            byte[] content;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();


            Stream stream = response.GetResponseStream();

            using (BinaryReader br = new BinaryReader(stream))
            {
                content = br.ReadBytes(500000);
                br.Close();
            }
            response.Close();

            FileStream fs = new FileStream(file_name, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            try
            {
                bw.Write(content);
            }
            finally
            {
                fs.Close();
                bw.Close();
            }
        }
       
        public string copyanh(string path)
        {

            DateTime aDateTime = DateTime.Now;
            int sas = Convert.ToInt32(aDateTime.Year * 12 * 30 * 24 * 60 * 60 + aDateTime.Month * 30 * 24 * 60 * 60 + aDateTime.Day * 24 * 60 * 60 + aDateTime.Hour * 60 * 60 + aDateTime.Minute * 60 + aDateTime.Second);
            string Filename = "Anh" + sas + ".jpg";
            string saveLocation = "~/Content/Img/" + Filename;
            string file_name = Server.MapPath(saveLocation);
            if (path.Contains("https"))
            {
                save_file_from_url(file_name, path);
            }
            else
            {
                // path = Server.MapPath(path);
                string path1 = System.IO.Path.GetFullPath(path);
                Image png = Image.FromFile(path1);
                png.Save(file_name, System.Drawing.Imaging.ImageFormat.Jpeg);
                png.Dispose();

            }

            return "/Content/Img/" + Filename;
        }

        public ActionResult TaoDe()
        {
            TracnghiemonlineDB db = new TracnghiemonlineDB();
          //  var bode = db.BoDeOnTaps.Where(x => x.MaLopHP.Equals(id)).ToList();

            List<Kho_CauHoi> cauHois = (List<Kho_CauHoi>)Session[webMVC.ComMonStants.Cauhoi];
            try
            {
                if (cauHois.Count == 0)
                {
                    cauHois = new List<Kho_CauHoi>();
                }
            }
            catch { cauHois = new List<Kho_CauHoi>(); }
            List<MonHoc> lstMon = db.MonHocs.Select(x => x).ToList();

            ViewBag.lstMon = new SelectList(lstMon, "MaMon", "TenMon");
            ViewBag.DeOn = cauHois;
            var TK = (TaiKhoan)Session[webMVC.ComMonStants.UserLogin];
            ViewBag.TenDangNhap = TK.TenDangNhap;
            return View();

        }

        public ActionResult LoadDe(string id)
        {
            var TK = (TaiKhoan)Session[webMVC.ComMonStants.UserLogin];
            ViewBag.TenDangNhap = TK.TenDangNhap;
            var list = new webMVC.Modell.Dao.BoDeDao().ChapterStudy(long.Parse(id));                    
            return View(list);
        }
        public string xuatpdf(long id, string tenmon)
        {
            PdfDocument pdf = new PdfDocument();
            PdfPageBase page = pdf.Pages.Add();
            PdfTrueTypeFont font1 = new PdfTrueTypeFont(new Font("Arial Unicode MS", 24f, FontStyle.Bold), true);
            PdfStringFormat centerAlignment = new PdfStringFormat(PdfTextAlignment.Center);
            PdfTrueTypeFont font2 = new PdfTrueTypeFont(new Font("Arial Unicode MS", 13f, FontStyle.Regular), true);

            PdfPen pen1 = new PdfPen(Color.Black, 1f);
            page.Canvas.DrawRectangle(pen1, new Rectangle(new Point(390, 150), new Size(120, 40)));
            PdfPen pen2 = new PdfPen(Color.Black, 1f);


            page.Canvas.DrawRectangle(pen1, new Rectangle(new Point(0, 0), new Size((int)page.GetClientSize().Width, (int)page.GetClientSize().Height)));
            page.Canvas.DrawString("TRƯỜNG ĐẠI HỌC GIAO THÔNG VẬN TẢI PHÂN HIỆU TẠI TP HỒ CHÍ MINH", font1, new PdfSolidBrush(Color.Black), new RectangleF(10, 50, page.GetClientSize().Width - 10, page.GetClientSize().Height), centerAlignment);
            page.Canvas.DrawString("Đề thi môn:" + tenmon, new PdfTrueTypeFont(new Font("Arial Unicode MS", 15f, FontStyle.Bold), true), new PdfSolidBrush(Color.Black), new RectangleF(40, 160, page.GetClientSize().Width / 2 - 40 - 10, page.GetClientSize().Height), new PdfStringFormat() { LineLimit = true });
            page.Canvas.DrawString("Mã đề:" + id, new PdfTrueTypeFont(new Font("Arial Unicode MS", 15f, FontStyle.Bold), true), new PdfSolidBrush(Color.Black), new RectangleF(400, 160, page.GetClientSize().Width / 2 - 400 - 10, page.GetClientSize().Height), new PdfStringFormat() { LineLimit = true });
            var khoch = new Bode().listkhocauhoi(id);
            int vitridong = 220;
            int slcau = 0;
            int x = 50;
            int y = (int)(page.GetClientSize().Width) / 2 + 50;
            string[] cau = { "A", "B", "C", "D" };

            foreach (var item in khoch)
            {

                slcau++;
                vitridong += 13;
                page.Canvas.DrawString("Câu " + slcau + ":", font2, new PdfSolidBrush(Color.Blue), new RectangleF(20, vitridong, page.GetClientSize().Width - 90, page.GetClientSize().Height), new PdfStringFormat() { LineLimit = true });
                page.Canvas.DrawString(item.NoiDung, font2, new PdfSolidBrush(Color.Black), new RectangleF(20 + 55, vitridong, page.GetClientSize().Width - 90, page.GetClientSize().Height), new PdfStringFormat() { LineLimit = true });
                vitridong += (int)((("Câu " + slcau + ":" + item.NoiDung).Length * 13) / (page.GetClientSize().Width - 40)) * 13;
                int slda = 0;
                int z = 0;
                int k = 0;
                int[] vtdongdapan = new int[4];
                if (item.HinhAnh != null)
                {
                    try
                    {
                        Image image = Image.FromFile(Server.MapPath(item.HinhAnh));
                        int width = image.Width;
                        int height = image.Height;
                        float schale = 1.5f;
                        Size size = new Size((int)(width * schale), (int)(height * schale));
                        Bitmap schaleImage = new Bitmap(image, size);
                        PdfImage pdfImage = PdfImage.FromImage(schaleImage);
                        page.Canvas.DrawImage(pdfImage, new RectangleF((page.GetClientSize().Width - 150) / 2, vitridong, 150, 150));

                        vitridong += 150;
                    }
                    catch { }

                }
                vitridong += 13;
                foreach (var da in item.Dap_AN)
                {
                    if (slda == 0 || slda == 2)
                    {
                        z = x;
                    }
                    else z = y;
                    vtdongdapan[slda] = (int)(((cau[slda] + ":" + da.NoiDung).Length * 13) / (page.GetClientSize().Width / 2)) * 13;
                    if (vtdongdapan[slda] <= 1) vtdongdapan[slda] = 13;

                    if (da.HinhAnh != null)
                    {
                        if (13 + vtdongdapan[slda] + vitridong + 150 > page.GetClientSize().Height - 20)
                        {
                            vitridong = 20;
                            page = pdf.Pages.Add();
                            page.Canvas.DrawRectangle(pen1, new Rectangle(new Point(0, 0), new Size((int)page.GetClientSize().Width, (int)page.GetClientSize().Height)));
                        }
                    }
                    if (da.TrangThai == false) { page.Canvas.DrawString(cau[slda] + ":", font2, new PdfSolidBrush(Color.Blue), new RectangleF(z - 10, vitridong, page.GetClientSize().Width / 2 - 20, page.GetClientSize().Height - 80), new PdfStringFormat() { LineLimit = true }); }
                    else { page.Canvas.DrawString(cau[slda] + ":", font2, new PdfSolidBrush(Color.Red), new RectangleF(z - 10, vitridong, page.GetClientSize().Width / 2 - 20, page.GetClientSize().Height - 80), new PdfStringFormat() { LineLimit = true }); }

                    page.Canvas.DrawString(da.NoiDung, font2, new PdfSolidBrush(Color.Black), new RectangleF(z + 5, vitridong, page.GetClientSize().Width / 2 - 20, page.GetClientSize().Height - 80), new PdfStringFormat() { LineLimit = true });


                    if (da.HinhAnh != null)
                    {
                        if (13 + vtdongdapan[slda] + vitridong + 150 > page.GetClientSize().Height - 20)
                        {
                            vitridong = 20;
                            page = pdf.Pages.Add();
                            page.Canvas.DrawRectangle(pen1, new Rectangle(new Point(0, 0), new Size((int)page.GetClientSize().Width, (int)page.GetClientSize().Height)));
                        }
                        try
                        {
                            Image image = Image.FromFile(Server.MapPath(item.HinhAnh));
                            int width = image.Width;
                            int height = image.Height;
                            float schale = 1.5f;
                            Size size = new Size((int)(width * schale), (int)(height * schale));
                            Bitmap schaleImage = new Bitmap(image, size);
                            PdfImage pdfImage = PdfImage.FromImage(schaleImage);
                            page.Canvas.DrawImage(pdfImage, new RectangleF(z + 10, 30 + vtdongdapan[slda] + vitridong, 150, 150));
                            k = 163;
                        }
                        catch
                        {

                        }

                    }
                    if (slda == 1)
                    {
                        if ((int)(((cau[slda] + ":" + da.NoiDung).Length * 13) / page.GetClientSize().Width) * 13 < 1) vitridong += 13;
                        if (vtdongdapan[0] > vtdongdapan[1])
                            vitridong += vtdongdapan[0];
                        else vitridong += vtdongdapan[1];
                        vitridong += k;
                        k = 0;
                    }
                    else if (slda == 3)
                    {
                        if ((int)(((cau[slda] + ":" + da.NoiDung).Length * 13) / page.GetClientSize().Width) * 13 < 1) vitridong += 13;
                        if (vtdongdapan[2] > vtdongdapan[3])
                            vitridong += vtdongdapan[2];
                        else vitridong += vtdongdapan[3];
                        vitridong += k;
                    }



                    if (vitridong > page.GetClientSize().Height - 20)
                    {
                        vitridong = 20;
                        page = pdf.Pages.Add();
                        page.Canvas.DrawRectangle(pen1, new Rectangle(new Point(0, 0), new Size((int)page.GetClientSize().Width, (int)page.GetClientSize().Height)));
                    }
                    slda++;
                }

            }
            page.Canvas.SetTransparency(0.2f);
            page.Canvas.DrawString("Hết", font1, new PdfSolidBrush(Color.Black), new RectangleF(20, page.GetClientSize().Height - 30, page.GetClientSize().Width, page.GetClientSize().Height), centerAlignment);
            page.Canvas.SetTransparency(1f);
            pdf.SaveToFile(Server.MapPath("~/Content/" + "De_" + id + ".pdf"));
            pdf.Close();

            return "/Content/" + "De_" + id + ".pdf";



        }

        public ActionResult DSDeOn()
        {
            var TK = (TaiKhoan)Session[webMVC.ComMonStants.UserLogin];
            ViewBag.TenDangNhap = TK.TenDangNhap;
            var session = (TaiKhoan)Session[webMVC.ComMonStants.UserLogin];
            var bode = new TracnghiemonlineDB().Bo_De.Where(x => x.Ma_NguoiTao.Equals(session.TaiKhoan1) && x.Xoa==true ).ToList();
            return View(bode);
        }
        public JsonResult UpdateDethi(long maDe, string nd, string tg, bool xoa)
        {


            TracnghiemonlineDB db = new TracnghiemonlineDB();
            var Bode = db.Bo_De.Find(maDe);
            Bode.ThoiGianThi = tg;
            Bode.NoiDung = nd;
            Bode.Xoa = xoa;
            db.SaveChanges();
            var session = (TaiKhoan)Session[webMVC.ComMonStants.UserLogin];
            List<Bo_De> bo_Des = new BoDeDao().ListALLChapterStudy();
            //  if (session.ChưcVu.Equals("Cán Bộ"))
            //  {
            bo_Des = new TracnghiemonlineDB().Bo_De.Where(x => x.Ma_NguoiTao == session.TaiKhoan1 && x.Xoa == true).ToList();
            //  }

            var bode1 = (from n in bo_Des
                         select new
                         {
                             Ten = n.NoiDung,
                             MaDe = n.Ma_BoDe,
                             SoCau = n.CauHois.Count,
                             ThoiGian = n.ThoiGianThi,
                             TenMon = n.MonHoc.TenMon,
                             Giaovien = n.NgayTao,
                            
                         }).ToList();
            return Json(new
            {
                Bode = bode1

            }, JsonRequestBehavior.AllowGet); ;

        }

        public void ThemDeOn(string nd, string tg, long MA)
        {
            TracnghiemonlineDB db = new TracnghiemonlineDB();
            List<Kho_CauHoi> cauHois = (List<Kho_CauHoi>)Session[webMVC.ComMonStants.Cauhoi];
            foreach (var item in cauHois)
            {
                item.Ma_CauHoi = new webMVC.Modell.Dao.CauHoiDao().CreatrQuestion(item);
            }
            var session = (TaiKhoan)Session[webMVC.ComMonStants.UserLogin];
            Bo_De bode = new Bo_De();
            bode.NoiDung = nd;
            bode.Xoa = true;
            bode.ThoiGianThi = tg;
            bode.Ma_Mon = MA;
            bode.LuotTai = 0;
            bode.LuotThich = 0;
            bode.Luuothi = 0;
           bode.Ma_NguoiTao = session.TaiKhoan1.Trim();
            bode.NgayTao = DateTime.Now;
         
            db.Bo_De.Add(bode);
            db.SaveChanges();
            db = new TracnghiemonlineDB();
            var Bode = db.Bo_De.Where(x=>x.Ma_NguoiTao.Equals(session.TaiKhoan1)).ToList().Last();
            Bode.LinkTai = xuatpdf(Bode.Ma_BoDe, Bode.NoiDung);
            db.SaveChanges();
            foreach (var item in cauHois)
            {
                CauHoi cauHoi = new CauHoi();
                cauHoi.Ma_BoDe = Bode.Ma_BoDe;
                cauHoi.Ma_CauHoi = item.Ma_CauHoi;
                db.CauHois.Add(cauHoi);
                db.SaveChanges();
            }

            cauHois = new List<Kho_CauHoi>();
            Session[webMVC.ComMonStants.Cauhoi] = cauHois;
            //  db.BoDeOnTaps.Add(deontap);
        }
        public JsonResult XuLyFile(HttpPostedFileBase file)
        {
            List<Kho_CauHoi> cauHois = new List<Kho_CauHoi>();
            string strExtexsion = Path.GetFileName(file.FileName).Trim();

            if (strExtexsion.Contains(".xls"))
            {
                try
                {
                    string path = Server.MapPath("~/Content/" + file.FileName);
                    try
                    {
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }
                        file.SaveAs(path);
                    }
                    catch { }


                    EXCELL.Application application = new EXCELL.Application();
                    EXCELL.Workbook workbook = application.Workbooks.Open(path);
                    EXCELL.Worksheet worksheet = workbook.ActiveSheet;


                    EXCELL.Range range = worksheet.UsedRange;

                    cauHois = new List<Kho_CauHoi>();

                    for (int i = 2; i <= range.Rows.Count; i++)
                    {
                        try
                        {

                            Kho_CauHoi cauHoi = new Kho_CauHoi();

                            cauHoi.NoiDung = ((EXCELL.Range)range.Cells[i, 1]).Text;

                            cauHoi.HinhAnh = ((EXCELL.Range)range.Cells[i, 8]).Text;


                            if (((EXCELL.Range)range.Cells[i, 7]).Text.Equals("1"))
                            {
                                cauHoi.MucDo = "Nhận Biết";
                            }
                            else if (((EXCELL.Range)range.Cells[i, 7]).Text.Equals("2"))
                            {
                                cauHoi.MucDo = "Thông Hiểu";
                            }
                            else if (((EXCELL.Range)range.Cells[i, 7]).Text.Equals("3"))
                            {
                                cauHoi.MucDo = "Vận Dụng";
                            }
                            else
                            {
                                cauHoi.MucDo = "Vận Dụng Cao";
                            }




                            cauHoi.Dap_AN = new List<Dap_AN>();
                            Dap_AN dapAn = new Dap_AN();
                            dapAn.NoiDung = "A) " + ((EXCELL.Range)range.Cells[i, 2]).Text;
                            dapAn.HinhAnh = ((EXCELL.Range)range.Cells[i, 9]).Text;

                            if (((EXCELL.Range)range.Cells[i, 6]).Text.Equals("A"))
                            {
                                dapAn.TrangThai = true;
                            }


                            else { dapAn.TrangThai = false; }
                            cauHoi.Dap_AN.Add(dapAn);
                            dapAn = new Dap_AN();
                            dapAn.NoiDung = "B) " + ((EXCELL.Range)range.Cells[i, 3]).Text;
                            dapAn.HinhAnh = ((EXCELL.Range)range.Cells[i, 10]).Text;

                            if (((EXCELL.Range)range.Cells[i, 6]).Text.Equals("B"))
                            {
                                dapAn.TrangThai = true;
                            }
                            else { dapAn.TrangThai = false; }
                            cauHoi.Dap_AN.Add(dapAn);
                            dapAn = new Dap_AN();
                            dapAn.NoiDung = "C) " + ((EXCELL.Range)range.Cells[i, 4]).Text;
                            dapAn.HinhAnh = ((EXCELL.Range)range.Cells[i, 11]).Text;

                            if (((EXCELL.Range)range.Cells[i, 6]).Text.Equals("C"))
                            {
                                dapAn.TrangThai = true;
                            }
                            else { dapAn.TrangThai = false; }
                            cauHoi.Dap_AN.Add(dapAn);

                            dapAn = new Dap_AN();
                            dapAn.NoiDung = "D) " + ((EXCELL.Range)range.Cells[i, 5]).Text;
                            dapAn.HinhAnh = ((EXCELL.Range)range.Cells[i, 12]).Text;

                            if (((EXCELL.Range)range.Cells[i, 6]).Text.Equals("D"))
                            {
                                dapAn.TrangThai = true;
                            }
                            else { dapAn.TrangThai = false; }
                            cauHoi.Dap_AN.Add(dapAn);

                            cauHois.Add(cauHoi);

                        }
                        catch
                        {

                        }
                    }


                    application.Workbooks.Close();
                    try
                    {
                        System.IO.File.Delete(path);
                    }
                    catch { }

                    foreach (var item in cauHois)
                    {


                        try
                        {
                            if (item.HinhAnh.Length > 0)
                            {
                                item.HinhAnh = copyanh(item.HinhAnh.Trim());

                            }
                            foreach (var item1 in item.Dap_AN)
                            {

                                if (item1.HinhAnh.Length > 0)
                                {
                                    item1.HinhAnh = copyanh(item1.HinhAnh.Trim());

                                }
                            }
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }

                    }
                    Session[webMVC.ComMonStants.Cauhoi] = cauHois;
                    return Json(new
                    {
                        status = true
                    });


                }
                catch
                {
                    return Json(new
                    {
                        status = false
                    });
                }
            }
            else
            {
                DateTime aDateTime = DateTime.Now;
                object path = Server.MapPath("~/Content/" + file.FileName);
                if (System.IO.File.Exists(path.ToString()))
                {
                    System.IO.File.Delete(path.ToString());
                }

                file.SaveAs(path.ToString());

                List<String> anh = new List<string>();
                string totalText = "";
                Document document = new Document(path.ToString());


                //Get Each Section of Document  
                foreach (Section section in document.Sections)
                {
                    //Get Each Paragraph of Section  
                    foreach (Paragraph paragraph in section.Paragraphs)
                    {
                        //Get Each Document Object of Paragraph Items  
                        foreach (DocumentObject docObject in paragraph.ChildObjects)
                        {
                            //If Type of Document Object is Picture, Extract.  
                            if (docObject.DocumentObjectType == DocumentObjectType.Picture)
                            {
                                int sas = Convert.ToInt32(aDateTime.Year * 12 * 30 * 24 * 60 * 60 + aDateTime.Month * 30 * 24 * 60 * 60 + aDateTime.Day * 24 * 60 * 60 + aDateTime.Hour * 60 * 60 + aDateTime.Minute * 60 + aDateTime.Second);
                                DocPicture pic = docObject as DocPicture;
                                String imgName = Server.MapPath("~/Content/Img/Anh" + sas + String.Format(".png"));
                                anh.Add("/Content/Img/Anh" + sas + String.Format(".png"));
                                //Save Image  
                                pic.Image.Save(imgName, System.Drawing.Imaging.ImageFormat.Png);

                            }

                        }
                        totalText += paragraph.Text.ToString();

                    }

                }
                int slanh = 0;
                List<Dap_AN> dapan2 = new List<Dap_AN>();
                cauHois = new List<Kho_CauHoi>();
                for (int i = 0; i < totalText.Length; i++)
                {
                    if (totalText[i] == '$' && totalText[i + 1] == 'c' && totalText[i + 2] == '$')
                    {
                        int slcau = 0;
                        Kho_CauHoi ch = new Kho_CauHoi();
                        int sldapa = 0;
                        int slda = 0;
                        List<Dap_AN> dapan = new List<Dap_AN>();

                        ch.Dap_AN = new List<Dap_AN>();
                        for (int j = i; j < totalText.Length; j++)
                        {

                            if ((totalText[j] == '$' && totalText[j + 1] == '*' && totalText[j + 2] == '$') || (totalText[j] == '$' && totalText[j + 1] == '$'))
                            {
                                slcau++;
                                Dap_AN da = new Dap_AN();
                                if (slcau == 1)
                                {

                                    ch.NoiDung = totalText.Substring(i + 3, j - i - 3);
                                    if (ch.NoiDung[0] == '1')
                                    {
                                        ch.MucDo = "Nhận Biết";
                                    }
                                    else if (ch.NoiDung[0] == '1')
                                    {
                                        ch.MucDo = "Thông Hiểu";
                                    }
                                    else if (ch.NoiDung[0] == '1')
                                    {
                                        ch.MucDo = "Vận Dụng";
                                    }
                                    else if (ch.NoiDung[0] == '1')
                                    {
                                        ch.MucDo = "Vận Dụng Cao";
                                    }
                                    else ch.MucDo = "Chua có mức độ";
                                    ch.NoiDung = ch.NoiDung.Substring(1, ch.NoiDung.Length - 1);
                                    ch.HinhAnh = "";
                                    for (int z = 0; z < ch.NoiDung.Length - 2; z++)
                                    {
                                        if (ch.NoiDung[z] == '#' && ch.NoiDung[z + 1] == 'h' && ch.NoiDung[z + 2] == '#')
                                        {

                                            ch.HinhAnh = anh[slanh];
                                            slanh++;
                                            ch.NoiDung = ch.NoiDung.Substring(0, z);
                                            break;
                                        }

                                    }

                                }


                                if (ch.MucDo == "Chua có mức độ") break;
                                for (int k = j + 2; k < totalText.Length; k++)
                                {


                                    if (totalText[j] == '$' && totalText[j + 1] == '*' && totalText[j + 2] == '$')
                                    {

                                        if (totalText[k] == '$' && totalText[k + 1] == '$')
                                        {
                                            da.HinhAnh = "";
                                            da.NoiDung = totalText.Substring(j + 3, k - j - 3);
                                            da.TrangThai = true;
                                            for (int z = 0; z < da.NoiDung.Length - 2; z++)
                                            {
                                                if (da.NoiDung[z] == '#' && da.NoiDung[z + 1] == 'h' && da.NoiDung[z + 2] == '#')
                                                {
                                                    da.HinhAnh = anh[slanh];
                                                    slanh++;
                                                    da.NoiDung = da.NoiDung.Substring(0, z);
                                                }

                                            }
                                            j = k - 1;
                                            ch.Dap_AN.Add(da);


                                        }
                                        else if (totalText[k] == '$' && totalText[k + 1] == 'c' && totalText[k + 2] == '$')
                                        {
                                            da.HinhAnh = "";
                                            da.NoiDung = totalText.Substring(j + 3, k - 3 - j);
                                            da.TrangThai = true;
                                            for (int z = 0; z < da.NoiDung.Length - 2; z++)
                                            {
                                                if (da.NoiDung[z] == '#' && da.NoiDung[z + 1] == 'h' && da.NoiDung[z + 2] == '#')
                                                {
                                                    da.HinhAnh = anh[slanh];
                                                    slanh++;
                                                    da.NoiDung = da.NoiDung.Substring(0, z);
                                                }

                                            }
                                            sldapa++;
                                            j = k - 1;
                                            ch.Dap_AN.Add(da);
                                            break;
                                        }
                                        else if (k == totalText.Length - 1)
                                        {
                                            da.HinhAnh = "";
                                            da.NoiDung = totalText.Substring(j + 3, totalText.Length - j - 3);
                                            da.TrangThai = true;
                                            for (int z = 0; z < da.NoiDung.Length - 2; z++)
                                            {
                                                if (da.NoiDung[z] == '#' && da.NoiDung[z + 1] == 'h' && da.NoiDung[z + 2] == '#')
                                                {
                                                    da.HinhAnh = anh[slanh];
                                                    slanh++;
                                                    da.NoiDung = da.NoiDung.Substring(0, z);
                                                }

                                            }
                                            sldapa++;
                                            j = totalText.Length - 1;
                                            ch.Dap_AN.Add(da);
                                            break;
                                        }
                                    }

                                    else if (totalText[j] == '$' && totalText[j + 1] == '$')
                                    {

                                        if (totalText[k] == '$' && totalText[k + 1] == '$')
                                        {
                                            da.HinhAnh = "";
                                            da.NoiDung = totalText.Substring(j + 2, k - j - 2);
                                            da.TrangThai = false;
                                            for (int z = 0; z < da.NoiDung.Length - 2; z++)
                                            {
                                                if (da.NoiDung[z] == '#' && da.NoiDung[z + 1] == 'h' && da.NoiDung[z + 2] == '#')
                                                {
                                                    da.HinhAnh = anh[slanh];
                                                    slanh++;
                                                    da.NoiDung = da.NoiDung.Substring(0, z);
                                                }

                                            }
                                            j = k - 1;
                                            ch.Dap_AN.Add(da);


                                        }
                                        else if (totalText[k] == '$' && totalText[k + 1] == '*' && totalText[k + 2] == '$')
                                        {
                                            da.HinhAnh = "";
                                            da.NoiDung = totalText.Substring(j + 2, k - j - 2);
                                            da.TrangThai = false;
                                            for (int z = 0; z < da.NoiDung.Length - 2; z++)
                                            {
                                                if (da.NoiDung[z] == '#' && da.NoiDung[z + 1] == 'h' && da.NoiDung[z + 2] == '#')
                                                {
                                                    da.HinhAnh = anh[slanh];
                                                    slanh++;
                                                    da.NoiDung = da.NoiDung.Substring(0, z);
                                                }

                                            }
                                            j = k - 1;
                                            ch.Dap_AN.Add(da);

                                        }
                                        else if (totalText[k] == '$' && totalText[k + 1] == 'c' && totalText[k + 2] == '$')
                                        {
                                            da.HinhAnh = "";
                                            da.NoiDung = totalText.Substring(j + 2, k - j - 2);
                                            da.TrangThai = false;
                                            for (int z = 0; z < da.NoiDung.Length - 2; z++)
                                            {
                                                if (da.NoiDung[z] == '#' && da.NoiDung[z + 1] == 'h' && da.NoiDung[z + 2] == '#')
                                                {
                                                    da.HinhAnh = anh[slanh];
                                                    slanh++;
                                                    da.NoiDung = da.NoiDung.Substring(0, z);
                                                }

                                            }
                                            sldapa++;
                                            j = k - 1;
                                            ch.Dap_AN.Add(da);
                                            break;
                                        }
                                        else if (k == totalText.Length - 1)
                                        {
                                            da.HinhAnh = "";
                                            da.NoiDung = totalText.Substring(j + 2, totalText.Length - j - 2);
                                            da.TrangThai = false;
                                            for (int z = 0; z < da.NoiDung.Length - 2; z++)
                                            {
                                                if (da.NoiDung[z] == '#' && da.NoiDung[z + 1] == 'h' && da.NoiDung[z + 2] == '#')
                                                {
                                                    da.HinhAnh = anh[slanh];
                                                    slanh++;
                                                    da.NoiDung = da.NoiDung.Substring(0, z);
                                                }

                                            }
                                            sldapa++;
                                            ch.Dap_AN.Add(da);
                                            j = totalText.Length - 1;
                                            break;
                                        }
                                    }

                                }



                            }



                            if (sldapa != 0)
                            {
                                cauHois.Add(ch);
                                break;
                            }


                        }
                    }


                }
                Session[webMVC.ComMonStants.Cauhoi] = cauHois;
            }

            return Json(new
            {
                status = true
            });

        }
        public JsonResult Timde(DateTime NBD,DateTime NKT,long mamon)
        {
            var dethi = from c in new TracnghiemonlineDB().Bo_De.Where(x => x.NgayTao >= NBD && x.NgayTao <= NKT && x.Ma_Mon==mamon && x.Xoa==true).ToList()
                        select new {
                         c.NoiDung,c.Ma_BoDe,c.MonHoc.TenMon,
                         c.ThoiGianThi,
                         c.LuotTai,
                         c.Luuothi,
                         c.TaiKhoan.TenDangNhap,
                            NgayTao = c.NgayTao.ToString(),
                            c.LinkTai,

                        }
                        ;
            return Json(new
            { 
                dethi,
                status = true
            },JsonRequestBehavior.AllowGet);
        }
        public JsonResult TimKiem(string ND,long mamon)
        {
            var dethi = from c in new TracnghiemonlineDB().Bo_De.Where(x => x.Ma_Mon == mamon && x.NoiDung.Contains(ND) && x.Xoa == true).ToList()
                        select new
                        {
                            c.NoiDung,
                            c.Ma_BoDe,
                            c.MonHoc.TenMon,
                           c.ThoiGianThi,
                            c.LuotTai,
                            c.Luuothi,
                            c.TaiKhoan.TenDangNhap,
                            NgayTao = c.NgayTao.ToString(),
                            c.LinkTai,

                        }
                        ;
            return Json(new
            {
                dethi,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Thinhhanh(long mamon)
        {
            var dethi = from c in new TracnghiemonlineDB().Bo_De.Where(x=> x.Ma_Mon == mamon && x.Xoa == true).OrderByDescending(x=>x.Luuothi).Take(10).ToList()
                        select new
                        {
                            c.NoiDung,
                            c.Ma_BoDe,
                            c.MonHoc.TenMon,
                            c.ThoiGianThi,
                            c.LuotTai,
                            c.Luuothi,
                            c.TaiKhoan.TenDangNhap,
                          NgayTao=c.NgayTao.ToString(),
                               c.LinkTai,                 

                        }
                        ;
            return Json(new
            {
                dethi,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Hot(long mamon)
        {
            var dethi = from c in new TracnghiemonlineDB().Bo_De.Where(x => x.Ma_Mon==mamon && x.Xoa == true).OrderByDescending(x => x.NgayTao).Take(10).ToList()
                        select new
                        {
                            c.NoiDung,
                            c.Ma_BoDe,
                            c.MonHoc.TenMon,
                            c.ThoiGianThi,
                            c.LuotTai,
                            c.Luuothi,
                            c.TaiKhoan.TenDangNhap,
                            NgayTao = c.NgayTao.ToString(),
                            c.LinkTai,

                        }
                        ;
            return Json(new
            {
                dethi,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        [ChildActionOnly]
        public ActionResult Menull() {

            var Mon = new Models.TracnghiemonlineDB().MonHocs.Select(x => x).ToList();
                return PartialView(Mon);
            }
        public ActionResult DiemSo(long id)
        {
            var TK = (TaiKhoan)Session[webMVC.ComMonStants.UserLogin];
            ViewBag.TenDangNhap = TK.TenDangNhap;
            TracnghiemonlineDB db = new TracnghiemonlineDB();
            var de = db.De_Thi.Find(id);
            de.TrangThai = true;
            db.SaveChanges();
            var exam = new QuanLyThiDAO().SearDethi(id);
            var mark = new QuanLyThiDAO().Mark(exam);
            return View(mark);
        }

        public ActionResult LuaChon(string madethi, string id)
        {
            var TK = (TaiKhoan)Session[webMVC.ComMonStants.UserLogin];
            ViewBag.TenDangNhap = TK.TenDangNhap;
            var examQuestion = (De_Thi)Session[webMVC.ComMonStants.ExamQuesTion];
            foreach (var item in examQuestion.CauHoiDeThis)
            {
                foreach (var item1 in item.Kho_CauHoi.Dap_AN)
                {
                    if (item1.MA_DAN == long.Parse(id))
                    {
                        foreach (var item2 in item.Kho_CauHoi.Dap_AN)
                        {
                            if (item2.MA_DAN == long.Parse(id))
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

            }

            new BoDeDao().OptionStudent(examQuestion);
            Session[webMVC.ComMonStants.ExamQuesTion] = examQuestion;
            return Content("");

        }
        public ActionResult Loald(long id)
        {
            var TK = (TaiKhoan)Session[webMVC.ComMonStants.UserLogin];
            ViewBag.TenDangNhap = TK.TenDangNhap;
            TracnghiemonlineDB db = new TracnghiemonlineDB();
          var Bode=  db.Bo_De.Find(id);
            Bode.Luuothi++;
            db.SaveChanges();
            var session = (TaiKhoan)Session[webMVC.ComMonStants.UserLogin];
            var list = new webMVC.Modell.Dao.BoDeDao().ChapterStudy(id);
         
            var Exem = new webMVC.Modell.Dao.BoDeDao().MixExemQuestion(list, session.TaiKhoan1);
            Session[webMVC.ComMonStants.ExamQuesTion] = Exem;
            DateTime data = DateTime.Now.AddMinutes(double.Parse(list.ThoiGianThi));
            ViewBag.GioThi = data.ToString("yyyy/MM/dd HH:mm:ss");
            ViewBag.DeThi = (De_Thi)Exem;
            return View();
        }
     
    }
}