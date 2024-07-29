using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopAppWithCrud
{
    public class DataAccessLayer
    {
        const string conString = "";
        SqlConnection conn = null;
        SqlCommand cmd = null;
        SqlDataReader reader = null;

        public DataAccessLayer()
        {
            conn = new SqlConnection(conString);
            cmd = new SqlCommand();
            cmd.Connection = conn;
        }
          
        public List<Student> GetAllStudents()
        {
            List<Student> list = new List<Student>();
            try
            {
                conn.Open();
                cmd.CommandText = "Select * from Student";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Student s = new Student();
                    s.Id = Convert.ToInt32(reader["Id"]);
                    s.Name = Convert.ToString(reader["Name"]);
                    s.Address = Convert.ToString(reader["Address"]);
                    s.Phone = Convert.ToString(reader["Phone"]);
                    s.DoB = Convert.ToString(reader["DoB"]);
                    list.Add(s);
                }
            }
            catch (Exception ex) { 
                 MessageBox.Show("Something went wrong");
            }
          
            return list;
        }
        public bool Register(Student s)
        {
            cmd.CommandText = "insert into Student(Id,Name,Address,Phone,DoB) values (Select (case When max(Id) Is Null Then 1 else max(Id)+1 End) From Student,@Name,@Address,@Phone.@DoB)";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Name", s.Name);
            cmd.Parameters.AddWithValue("@Address", s.Address);
            cmd.Parameters.AddWithValue("@Phone", s.Phone);
            cmd.Parameters.AddWithValue("@DoB", s.DoB);
            conn.Open();
            int count = cmd.ExecuteNonQuery();
return false;
        }
        public bool Edit(Student s) { 
         return false;
        }

        public bool Remove(Student s) { 
        return false ;
        }
    }
}
