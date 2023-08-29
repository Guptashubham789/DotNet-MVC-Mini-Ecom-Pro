using MiniProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.Mvc;

namespace MiniProject.Controllers
{
    
    public class AdminController : Controller
    {
        
        // GET: Admin
        DBManager db = new DBManager();
      
       
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Index(AdminLoginModel admin)
        {
           SqlParameter[] sqlParameters = new SqlParameter[]
            {
               new SqlParameter("@action",1),
               new SqlParameter("@email",admin.email),
               new SqlParameter("@password",admin.password)
            };
            DataTable dt = db.SelectRecord("sp_adminlogin", sqlParameters);
            if (dt.Rows.Count > 0)
            {              
                Session["useremail"] = dt.Rows[0]["email"];               
                return RedirectToAction("Dashboard");
            }
            else
            {
                return Content("<script>alert('User & Password wrong!!')</script>");
            }
        }
        public ActionResult  Dashboard()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            
            return View();
        }

        
            [HttpPost]
        public ActionResult AddProduct(AddProduct product)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@action",1),
                new SqlParameter("@Title",product.title),
                new SqlParameter("@BrandName",product.brandname),
                new SqlParameter("@Color",product.pcolor),
                new SqlParameter("@Description",product.pdescription),
                new SqlParameter("@Price",product.price),
                new SqlParameter("@SalePrice",product.saleprice),
                new SqlParameter("@Picture",product.picture.FileName)
            };
            int res = db.Insert_Update_Delete("sp_addproduct", parameters);
            if(res>0)
            {
                product.picture.SaveAs(Server.MapPath("/Content/ProductPicture/" + product.picture.FileName));
                return  RedirectToAction("/addproduct");
            }
            else
            {
                return Content("<scrip>alert('Not insert data')</script>");
            }
            
        }

        public ActionResult ManageProduct()
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@action",2)
            };
            DataTable dt = db.SelectRecord("sp_addproduct", parameters);
            
            return View(dt);
        }
        public ActionResult DeleteRecord(int? id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id",id),
                new SqlParameter("@action",3)
            };
            int res = db.Insert_Update_Delete("sp_addproduct", parameters);
            return Json("Do you want to sure delete the record ?", JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditProduct(int id)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id",id),
                new SqlParameter("@action",4)
            };
            DataTable dt = db.SelectRecord("sp_addproduct", parameters);
            List<AddProduct> list = new List<AddProduct>();
            foreach(DataRow row in dt.Rows)
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
            return Json(list,JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddSlider()
        {
            return View();
        }
    }
}