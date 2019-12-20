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
    }
}