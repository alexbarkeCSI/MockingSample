using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using ClassLib.Models;
using Dapper;

namespace ClassLib.DataAccess
{
    /// <summary>
    /// Please note that MyConnection is not necessary here since we are just explaining how to
    /// do mocks.  In a real app, I would have a connection string
    /// </summary>
    public class EmployeeDataAccess : IEmployeeDataAccess
    {
        public List<Employee> LoadEmployees(string sql)
        {
            using (IDbConnection conn = new SqlConnection("MyConnection"))
            {
                var output = conn.Query<Employee>(sql, new DynamicParameters());
                return output.ToList();
            }
        }

        public void SaveEmployee(Employee employee, string sql)
        {
            using (IDbConnection conn = new SqlConnection("MyConnection"))
            {
                conn.Execute(sql, new { employee.Id, employee.FirstName, employee.LastName, employee.Occupation });
            }
        }

        public void UpdateEmployee(Employee employee, string sql)
        {
            using (IDbConnection conn = new SqlConnection("MyConnection"))
            {
                conn.Execute(sql, new { employee.Id, employee.FirstName, employee.LastName, employee.Occupation });
            }
        }

        public void DeleteEmployee(Employee employee, string sql)
        {
            using (IDbConnection conn = new SqlConnection("MyConnection"))
            {
                conn.Execute(sql, new { employee.Id });
            }
        }
    }
}