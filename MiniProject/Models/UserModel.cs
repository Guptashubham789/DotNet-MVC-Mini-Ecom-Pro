using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniProject.Models
{
    public class UserModel
    {
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
    public class UserSignUpModel
    {
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string number { get; set; }
        public string gender { get; set; }
        public string address { get; set; }
        public HttpPostedFileBase upicture { get; set; }
        public string upimage { get; set; }
    }
    
}