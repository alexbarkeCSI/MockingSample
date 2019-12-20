using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using ClassLib.Models;
using Dapper;

namespace ClassLib.DataAccess
{
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
    }
}