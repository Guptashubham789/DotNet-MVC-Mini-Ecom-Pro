using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MiniProject.Models
{
    public class DBManager
    {
        public int Insert_Update_Delete(string procedure, SqlParameter[] parameters)
        {
            SqlConnection con = new SqlConnection("Data Source=SSG\\SHUBHAM;Initial Catalog=MiniProject;Integrated Security=True");
            SqlCommand cmd = new SqlCommand(procedure, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            foreach (SqlParameter parameter in parameters)
            {
                cmd.Parameters.Add(parameter);
            }
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }
         
        public DataTable SelectRecord(string procedure, SqlParameter[] parameters)
        {
            SqlConnection con = new SqlConnection("Data Source=SSG\\SHUBHAM;Initial Catalog=MiniProject;Integrated Security=True");
            SqlCommand cmd = new SqlCommand(procedure, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            foreach (SqlParameter parameter in parameters)
            {
                cmd.Parameters.Add(parameter);
            }
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }
    }
}
//< div class= "col-md-6" >
//                     Category:< select class= "form-control" name = "brandname" >
   
//                       < option > Select Brand Name</option>
//                          @{ 
//                        foreach(System.Data.DataRow data in ViewBag.BrandCargory.Rows)
//{
//                                                     < option value = "@data["id"]" > @data["BrandName"] </ option >
//                  }
//                    }
//                    </ select >
//                </ div >