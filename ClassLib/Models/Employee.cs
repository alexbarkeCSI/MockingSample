﻿using System;

namespace ClassLib.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Occupation { get; set; }

        public Employee()
        {
            
        }

        public Employee(Guid id, string firstName, string lastName, string occupation)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Occupation = occupation;
        }
    }
}