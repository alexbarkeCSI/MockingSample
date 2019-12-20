using System;
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

        public Employee CreateEmployee(Guid id, string firstName, string lastName, string occupation)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Employee Id cannot be empty", nameof(id));
            }

            if (string.IsNullOrEmpty(firstName))
            {
                throw new ArgumentException("Employee FirstName cannot be empty or null", nameof(firstName));
            }

            if (string.IsNullOrEmpty(lastName))
            {
                throw new ArgumentException("Employee LastName cannot be empty or null", nameof(lastName));
            }

            if (string.IsNullOrEmpty(occupation))
            {
                throw new ArgumentException("Employee Occupation cannot be empty or null", nameof(occupation));
            }

            return new Employee(id, firstName, lastName, occupation);
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