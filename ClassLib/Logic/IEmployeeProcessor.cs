using System;
using System.Collections.Generic;
using ClassLib.Models;

namespace ClassLib.Logic
{
    public interface IEmployeeProcessor
    {
        List<Employee> GetEmployees();
        Employee CreateEmployee(Guid id, string firstName, string lastName, string occupation);
        void SaveEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
    }
}