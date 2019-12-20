using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using Moq;
using Autofac.Extras.Moq;
using Castle.Components.DictionaryAdapter.Xml;
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

        [Fact]
        public void Delete_Employee_ValidCall()
        {
            using (AutoMock mock = AutoMock.GetLoose())
            {
                Employee employee = GetSampleEmployees().First();
                const string sql = "DELETE FROM Employee WHERE Id = @Id";
                // setup mocked environment
                mock.Mock<IEmployeeDataAccess>()
                    .Setup(x => x.DeleteEmployee(employee, sql));

                // create the actual class to run the employee get method
                EmployeeProcessor cls = mock.Create<EmployeeProcessor>();

                // run update command
                cls.DeleteEmployee(employee);

                // verify was run once
                mock.Mock<IEmployeeDataAccess>()
                    .Verify(x => x.DeleteEmployee(employee, sql), Times.Exactly(1));
            }
        }

        [Theory]
        [InlineData("d1503281-dc30-4bf1-89af-13f5b5ac8cbc", "", "", "")]
        [InlineData("cefadf1c-1da4-4342-8710-41c375a83507", "", "", null)]
        [InlineData("00000000-0000-0000-0000-000000000000", "", null, "")]
        [InlineData("00000000-0000-0000-0000-000000000000", null, "", "")]
        [InlineData("c5320cae-906f-430d-9071-17cd4a78e153", null, "", "")]
        public void Create_Employee_Fails_With_Bad_Inputs(string guidAsString, string firstName, string lastName, string occupation)
        {
            EmployeeProcessor employeeProcessor = new EmployeeProcessor(null);

            Assert.Throws<ArgumentException>(() =>
                employeeProcessor.CreateEmployee(Guid.Parse(guidAsString), firstName, lastName, occupation));
        }

        [Theory]
        [InlineData("d1503281-dc30-4bf1-89af-13f5b5ac8cbc", "Alex", "Barke", "Testing Idiot")]
        [InlineData("d1503281-dc30-4bf1-89af-13f5b5ac8cbc", "Darren", "Chan", "Testing Denial Expert Coder")]
        public void Create_Employee_Passes_With_Good_Inputs(string guidAsString, string firstName, string lastName, string occupation)
        {
            EmployeeProcessor employeeProcessor = new EmployeeProcessor(null);

            Employee expected = new Employee(Guid.Parse(guidAsString), firstName, lastName, occupation);

            Employee actual = employeeProcessor.CreateEmployee(Guid.Parse(guidAsString), firstName, lastName, occupation);

            Assert.NotNull(actual);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.FirstName, actual.FirstName);
            Assert.Equal(expected.LastName, actual.LastName);
            Assert.Equal(expected.Occupation, actual.Occupation);
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
