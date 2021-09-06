using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Student.Models
{
    public class StudentBL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        public IPagedList<Student> GetAllStudent(int page, int pageSize)
        {
            List<Student> emp = new List<Student>();
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("ListStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Student std = new Student();
                    std.ID = Convert.ToInt32(rdr["ID"]);
                    std.Name = rdr["Name"].ToString();
                    std.Email = rdr["Email"].ToString();
                    std.Mobile = rdr["Mobile"].ToString();

                    emp.Add(std);

                }
                con.Close();

            }
            return emp.ToPagedList(page, pageSize);
        }
        private SqlConnection con;

        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            con = new SqlConnection(constr);

        }
        public List<Student> GetById()
        {
            List<Student> emp = new List<Student>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("ListStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Student std = new Student();
                    std.ID = Convert.ToInt32(rdr["ID"]);
                    std.Name = rdr["Name"].ToString();
                    std.Email = rdr["Email"].ToString();
                    std.Mobile = rdr["Mobile"].ToString();

                    emp.Add(std);
                }
                con.Close();
            }
            return emp;
        }
        public bool AddStudent(Student obj)
        {
            connection();
            SqlCommand com = new SqlCommand("AddStudent", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Name", obj.Name);
            com.Parameters.AddWithValue("@Email", obj.Email);
            com.Parameters.AddWithValue("@Mobile", obj.Mobile);

            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {

                return true;

            }
            else
            {

                return false;
            }

        }
        public bool UpdateStudent(Student obj)
        {

            connection();
            SqlCommand com = new SqlCommand("UpdateStudent", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@ID", obj.ID);
            com.Parameters.AddWithValue("@Name", obj.Name); 
            com.Parameters.AddWithValue("@Email", obj.Email);
            com.Parameters.AddWithValue("@Mobile", obj.Mobile);
            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >0)
            {

                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DeleteStudent(int Id)
        {

            connection();
            SqlCommand com = new SqlCommand("DeleteStudent", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@ID", Id);

            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {

                return false;
            }
        }
    }
 }
