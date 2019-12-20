using System.Collections.Generic;
using ClassLib.DataAccess;
using ClassLib.Models;

namespace ClassLib.Logic
{
    public class EmployeeProcessor : IEmployeeProcessor
    {
        private readonly IEmployeeDataAccess _dataAccess;

        public EmployeeProcessor(IEmployeeDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public List<Employee> GetEmployees()
        {
            const string sql = "SELECT * FROM Employee";

            return _dataAccess.LoadEmployees(sql);
        }

        public void SaveEmployee(Employee employee)
        {
            const string sql = "INSERT INTO Employee VALUES (@Id, @FirstName, @LastName, @Occupation)";

            _dataAccess.SaveEmployee(employee, sql);
        }

        public void UpdateEmployee(Employee employee)
        {
            const string sql = "UPDATE Employee Set FirstName = @FirstName, LastName = @LastName, Occupation = @Occupation WHERE Id = @Id";

            _dataAccess.UpdateEmployee(employee, sql);
        }

        public void DeleteEmployee(Employee employee)
        {
            const string sql = "DELETE FROM Employee WHERE Id = @Id";

            _dataAccess.DeleteEmployee(employee, sql);
        }
    }
}