using MiniProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiniProject.Controllers
{

    public class HomeController : Controller
    {
        DBManager db = new DBManager();
        
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(UserSignUpModel user)
        {
            SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@action",1),
                    new SqlParameter("@name",user.name),
                    new SqlParameter("@email",user.email),
                    new SqlParameter("@password",user.password),
                    new SqlParameter("@mobile",user.number),
                    new SqlParameter("@gender",user.gender),
                    new SqlParameter("@address",user.address),
                    new SqlParameter("@userpic",user.upicture.FileName)
                };
            int res = db.Insert_Update_Delete("sp_userregister", parameters);
            if (res > 0)
            {
                user.upicture.SaveAs(Server.MapPath("/Content/UserProfile/" + user.upicture.FileName));
                return RedirectToAction("/login");
            }
            else
            {
                return Content("<scrip>alert('Account not create!!')</script>");
            }
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserSignUpModel login)
        {
            SqlParameter[] parameters = new SqlParameter[]
            { 
                new SqlParameter("@action",2),
                new SqlParameter("@email",login.email),
                new SqlParameter("@password",login.password)
            };
            DataTable dt = db.SelectRecord("sp_userregister", parameters);
            if (dt.Rows.Count > 0)
            {
                Session["userid"] = dt.Rows[0]["id"];
                Session["useremail"] = dt.Rows[0]["email"];
                Session["username"] = dt.Rows[0]["name"];
                Session["userpass"] = dt.Rows[0]["password"];
                Session["usermobile"] = dt.Rows[0]["mobile"];
                Session["usergender"] = dt.Rows[0]["gender"];
                Session["useraddress"] = dt.Rows[0]["address"];
                Session["userpics"] = dt.Rows[0]["userpic"];
                return RedirectToAction("Userdash");
            }
            else
            {
                return Content("<script>alert('Password is wrong...');Window.location.href='/home/Login/';</script>");
            }
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            // Session.Remove("username");
            return RedirectToAction("login");
        }
        [HttpGet]
        public ActionResult UserDash()
        {
            //if (Session["useremail"] != null)
            //{
            //    ViewBag.Message = "Your application description page.";
            //    return View();
            //}
            //else
            //{
            //    return Content("<script>alert('Login First');location.href='/home/login'</script>");
            //}
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@action",2)
            };
            DataTable dt = db.SelectRecord("sp_addproduct", parameters);
            

            return View(dt);

        }
        [HttpPost]
        public ActionResult UserDash(int? id)
        {
            return RedirectToAction("RedirectPage");
        }


        public ActionResult RedirectPage(int? id)
        {
            SqlParameter[] sqlParameters = new SqlParameter[] { 
                new SqlParameter("id",id),
                new SqlParameter("action",4)
            };
            DataTable dt = db.SelectRecord("sp_addproduct", sqlParameters);
            List<AddProduct> list = new List<AddProduct>();
            foreach (DataRow row in dt.Rows)
            {
                AddProduct product = new AddProduct();
                product.id = Convert.ToInt32(row["id"]);
                product.title = Convert.ToString(row["Title"]);
                product.brandname = Convert.ToString(row["BrandName"]);
                product.pcolor = Convert.ToString(row["Color"]);
                product.pdescription = Convert.ToString(row["Description"]);
                product.price = Convert.ToInt32(row["Price"]);
                product.saleprice = Convert.ToInt32(row["SalePrice"]);
                product.pimage = Convert.ToString(row["Picture"]);
                list.Add(product);
            }
            ViewBag.data = list;
            return View();
        }
       
        
        public ActionResult Profile()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Order()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Order(int id)
        {
            return View();
        }
        public ActionResult Edit(int id,Int64 userid)
        {
            return Json(JsonRequestBehavior.AllowGet);
        }
    }
} 