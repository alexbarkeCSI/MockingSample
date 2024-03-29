﻿using System.Collections.Generic;
using ClassLib.Models;

namespace ClassLib.DataAccess
{
    public interface IEmployeeDataAccess
    {
        List<Employee> LoadEmployees(string sql);
        void SaveEmployee(Employee employee, string sql);
        void UpdateEmployee(Employee employee, string sql);
        void DeleteEmployee(Employee employee, string sql);
    }
}