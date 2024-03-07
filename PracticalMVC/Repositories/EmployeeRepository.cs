using PracticalMVC.Models;
using System.Data.SqlClient;

namespace PracticalMVC.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private string connectionString = "data source=HP-SIDDIKUR;initial catalog=Siddik_Practical;trusted_connection=true";


        public List<Employees_View> GetAllEmployees()
        {
            List<Employees_View> employeeList = new();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand query = new SqlCommand("SELECT * FROM Employees_View", conn))
                {
                    using (SqlDataReader reader = query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Employees_View employee = new()
                            {
                                UniqueId = (int)reader["UniqueId"],
                                Name = (string)reader["Name"],
                                Age = (int)reader["Age"],
                                JoinDate = (DateTime)reader["JoinDate"],
                                Designation = reader["Designation"] != DBNull.Value ? (string)reader["Designation"] : null
                        };
                            employeeList.Add(employee);
                        }
                    }
                }
            }
            return employeeList;
        }


        public Employee GetEmployeeById(int id)
        {
            Employee employee = null;
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand query = new SqlCommand("SELECT * FROM Employee WHERE UniqueId = @UniqueId", conn))
                {
                    query.Parameters.AddWithValue("@UniqueId", id);
                    {
                        using(SqlDataReader reader = query.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                employee = new Employee()
                                {
                                    UniqueId = (int)reader["UniqueId"],
                                    FirstName = (string)reader["FirstName"],
                                    LastName = (string)reader["LastName"],
                                    BirthDate = (DateTime)reader["BirthDate"],
                                    JoinDate = (DateTime)reader["JoinDate"],
                                    Designation = reader["Designation"] as string
                                };
                            }
                        }
                    }
                }
            }
            return employee;
        }


        public void InsertOrUpdateEmployee(Employee employee)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand query = new SqlCommand("Insert_Employee", conn))
                {
                    query.CommandType = System.Data.CommandType.StoredProcedure;

                    query.Parameters.AddWithValue("@UniqueId", employee.UniqueId);
                    query.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    query.Parameters.AddWithValue("@LastName", employee.LastName);
                    query.Parameters.AddWithValue("@BirthDate", employee.BirthDate);
                    query.Parameters.AddWithValue("@JoinDate", employee.JoinDate);
                    query.Parameters.AddWithValue("@Designation", employee.Designation);

                    query.ExecuteNonQuery(); 
                }

            }
        }


        public void DeleteEmployee(int id)
        {
            using (SqlConnection conn =new SqlConnection(connectionString))
            {
                conn.Open();
                using(SqlCommand query = new SqlCommand("DELETE FROM Employee WHERE UniqueId = @UniqueId", conn))
                {
                    query.Parameters.AddWithValue("@Uniqueid", id);
                    query.ExecuteNonQuery();
                }
            }
        }
    }
}
