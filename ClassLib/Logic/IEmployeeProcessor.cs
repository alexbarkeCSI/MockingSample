using System.Collections.Generic;
using ClassLib.Models;

namespace ClassLib.Logic
{
    public interface IEmployeeProcessor
    {
        List<Employee> GetEmployees();
        void SaveEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
    }
}