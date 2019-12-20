using System;
using System.Collections.Generic;
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
        public void TestMethod1()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IEmployeeDataAccess>()
                    .Setup(x => x.LoadEmployees("SELECT * FROM Employee"))
                    .Returns(GetSampleEmployees());

                var cls = mock.Create<EmployeeProcessor>();
                var expected = GetSampleEmployees();

                var actual = cls.GetEmployees();

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
