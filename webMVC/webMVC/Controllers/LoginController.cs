using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using webMVC.Models;

namespace webMVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            Session[webMVC.ComMonStants.UserLogin] = null;
            return View();
        } 
        public ActionResult TaoTK()
        {
            return View();
        }

        public void SendEmail(string address, string subject, string message)
        {
            string email = "phamtruong0305@gmail.com";
            string password = "0353573467";
            var loginInfo = new NetworkCredential(email, password);
            var msg = new MailMessage();
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);

            msg.From = new MailAddress(email);
            msg.To.Add(new MailAddress(address));
            msg.Subject = subject;
            msg.Body = message;
            msg.IsBodyHtml = true;
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(msg);
        }

        public JsonResult Create(string USE ,string Adress,string Pass)
        {

            if (!new TracnghiemonlineDB().TaiKhoans.Select(x=>x).ToList().Exists(x => x.TaiKhoan1.Trim().Equals(Adress)))
            {
                try
                {
                    TaiKhoan taiKhoan = new TaiKhoan();
                    taiKhoan.MatKhau = Pass;
                    taiKhoan.TaiKhoan1 = Adress;
                    taiKhoan.TenDangNhap = USE;
                    Random random = new Random();
                    String Ma = "" + random.Next(100000, 999999);
                    SendEmail(Adress, "Tạo Tài Khoản", "Mã Xác Nhận Của Bạn Là " + Ma);
                    Session["Ma"] = Ma;
                    Session[webMVC.ComMonStants.UserLogin] = taiKhoan;
                    return Json(new
                    {
                        status = true
                    },JsonRequestBehavior.AllowGet );
                }
                catch (Exception)
                {
                    return Json(new
                    {
                        status = "Vui lòng nhập đúng định dạng gmail"
                    }, JsonRequestBehavior.AllowGet);

                }
            }


            return Json(new
            {
                status = "Gmail Bạn Nhập Đã Tồn Tại"
            }, JsonRequestBehavior.AllowGet) ;

            
        }

        public JsonResult LayMK(string Adress)
        {

            if (new TracnghiemonlineDB().TaiKhoans.Select(x => x).ToList().Exists(x => x.TaiKhoan1.Trim().Equals(Adress)))
            {
                try
                {
                    TaiKhoan taiKhoan = new TracnghiemonlineDB().TaiKhoans.Find(Adress);
                 
                    Random random = new Random();
                    String Ma = "" + random.Next(100000, 999999);
                    SendEmail(Adress, "Lấy Lại Mật Khẩu", "Mật Khẩu Mới Của Bạn Là " + Ma);
                    Session["Ma"] = Ma;
                    Session[webMVC.ComMonStants.UserLogin] = taiKhoan;
                    return Json(new
                    {
                        status = true
                    });
                }
                catch (Exception)
                {
                    return Json(new
                    {
                        status = "Vui lòng nhập đúng định dạng gmail"
                    }, JsonRequestBehavior.AllowGet);

                }
            }
            return Json(new
            {
                status = "Gmail Bạn Nhập Không Tồn Tại "
            }, JsonRequestBehavior.AllowGet);


        }


        public JsonResult KiemTra(string Adress)
        {
            TaiKhoan tk = (TaiKhoan)Session[webMVC.ComMonStants.UserLogin];
            string ma = (string)Session["Ma"];
             if(ma.Equals(Adress))
            {
                if (!new TracnghiemonlineDB().TaiKhoans.Select(x=>x).ToList().Exists(x => x.TaiKhoan1.Trim().Equals(tk.TaiKhoan1.Trim())))
                {
 
                    TracnghiemonlineDB db = new TracnghiemonlineDB();
                    db.TaiKhoans.Add(tk);
                    db.SaveChanges();
                    Session["Ma"] = "";
                    return Json(new
                    {
                        status = true
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    try
                    {
                        TracnghiemonlineDB db = new TracnghiemonlineDB();
                        TaiKhoan taiKhoan = db.TaiKhoans.Find(tk.TaiKhoan1);
                        taiKhoan.MatKhau = Adress;
                        db.SaveChanges();
  
                        Session["Ma"] = "";
                        Session[webMVC.ComMonStants.UserLogin] = taiKhoan;
                        return Json(new
                        {
                            status = true
                        }, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception)
                    {
                        return Json(new
                        {
                            status = "Vui lòng nhập đúng định dạng gmail"
                        }, JsonRequestBehavior.AllowGet);

                    }

                }
            } 
                return Json(new
                {
                    status = false
                }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SendMa(string mess,string mess1)
        {
            Random random = new Random();
            TaiKhoan tk =(TaiKhoan)Session[webMVC.ComMonStants.UserLogin];
            String Ma = "" + random.Next(100000, 999999);
            SendEmail(tk.TaiKhoan1, mess, mess1 + Ma);
            Session["Ma"] = Ma;
          
            return Json(new
            {
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult QuenTk()
        {
            return View();
        }
        public ActionResult TaiKhoan()
        {
         var tl=(TaiKhoan)Session[webMVC.ComMonStants.UserLogin];
            return View(tl);
        }
        public JsonResult Doimk(string mk)
        {
            try
            {
                var tl = (TaiKhoan)Session[webMVC.ComMonStants.UserLogin];
                TracnghiemonlineDB db = new TracnghiemonlineDB();
                tl = db.TaiKhoans.Find(tl.TaiKhoan1);
                tl.MatKhau = mk;
                db.SaveChanges();
            }
            catch
            {
                return Json(new
                {
                    status = false

                },JsonRequestBehavior.AllowGet);
            }
           


            return Json(new
            {
                status = true

            },JsonRequestBehavior.AllowGet) ;
        }
        public ActionResult DangNhap(TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                var TK = new Modell.Dao.LoginDao().Login(taiKhoan);
                if (TK != null)
                {
                    Session.Add(webMVC.ComMonStants.UserLogin, TK);
                 
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng Nhập Không Đúng ");
                }

            }
            return View("Login");
        }
    }
}
