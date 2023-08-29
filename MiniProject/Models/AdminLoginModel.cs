using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniProject.Models
{
    public class AdminLoginModel
    {
        public string email { get; set; }
        public string password { get; set; }
    }

    public class AddProduct
    {
        public int id { get; set; }
        public string title { get; set; }
        public string brandname { get; set; }
        public string pcolor { get; set; }
        public string pdescription { get; set; }
        public long price { get; set; }
        public long saleprice { get; set; }
        public HttpPostedFileBase picture { get; set; }
        public string pimage { get; set; }
    }
}