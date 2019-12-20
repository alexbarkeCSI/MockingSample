using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using Moq;
using Autofac.Extras.Moq;
using ClassLib.DataAccess;
using ClassLib.Logic;
using ClassLib.Models;
using Assert = Xunit.Assert;

namespace MockingSample.Tests
{
    [TestClass]
    public class EmployeeTests
    {
        [Fact]
        public void Get_Employees_ValidCall()
        {
            using (AutoMock mock = AutoMock.GetLoose())
            {
                // setup mocked environment
                mock.Mock<IEmployeeDataAccess>()
                    .Setup(x => x.LoadEmployees("SELECT * FROM Employee"))
                    .Returns(GetSampleEmployees());

                // create the actual class to run the employee get method
                EmployeeProcessor cls = mock.Create<EmployeeProcessor>();

                // get sample expected data
                var expected = GetSampleEmployees();

                // run the get employees method which will instead return dummy data
                var actual = cls.GetEmployees();

                // do asserts
                Assert.True(actual != null);
                Assert.Equal(expected.Count, actual.Count);

                for (int i = 0; i < expected.Count; i++)
                {
                    Assert.Equal(expected[i].Id, actual[i].Id);
                    Assert.Equal(expected[i].FirstName, actual[i].FirstName);
                    Assert.Equal(expected[i].LastName, actual[i].LastName);
                    Assert.Equal(expected[i].Occupation, actual[i].Occupation);
                }
            }
        }

        [Fact]
        public void Save_Employee_ValidCall()
        {
            using (AutoMock mock = AutoMock.GetLoose())
            {
                Employee employee = GetSampleEmployees().First();
                const string sql = "INSERT INTO Employee VALUES (@Id, @FirstName, @LastName, @Occupation)";
                // setup mocked environment
                mock.Mock<IEmployeeDataAccess>()
                    .Setup(x => x.SaveEmployee(employee, sql));

                // create the actual class to run the employee get method
                EmployeeProcessor cls = mock.Create<EmployeeProcessor>();

                // run save employee
                cls.SaveEmployee(employee);

                // verify was run once
                mock.Mock<IEmployeeDataAccess>()
                    .Verify(x => x.SaveEmployee(employee, sql), Times.Exactly(1));
            }
        }

        [Fact]
        public void Update_Employee_ValidCall()
        {
            using (AutoMock mock = AutoMock.GetLoose())
            {
                Employee employee = GetSampleEmployees().First();
                const string sql = "UPDATE Employee Set FirstName = @FirstName, LastName = @LastName, Occupation = @Occupation WHERE Id = @Id";
                // setup mocked environment
                mock.Mock<IEmployeeDataAccess>()
                    .Setup(x => x.UpdateEmployee(employee, sql));

                // create the actual class to run the employee get method
                EmployeeProcessor cls = mock.Create<EmployeeProcessor>();

                // run update command
                cls.UpdateEmployee(employee);

                // verify was run once
                mock.Mock<IEmployeeDataAccess>()
                    .Verify(x => x.UpdateEmployee(employee, sql), Times.Exactly(1));
            }
        }

        private static List<Employee> GetSampleEmployees()
        {
            return new List<Employee>
            {
                new Employee
                {
                    Id = Guid.Parse("9d032840-f8c6-4d19-969b-b8b00b548957"),
                    FirstName = "Alex",
                    LastName = "Barke",
                    Occupation = "Testing Idiot"
                },
                new Employee
                {
                    Id = Guid.Parse("fd4657fa-5506-400d-b383-3aa5c801ecc5"),
                    FirstName = "Darren",
                    LastName = "Chan",
                    Occupation = "Testing Denier"
                },
            };
        }
    }
}
