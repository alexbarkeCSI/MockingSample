﻿using System.Collections.Generic;
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
    }
}