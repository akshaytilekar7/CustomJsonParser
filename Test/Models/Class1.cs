using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class Class1
    {
        public class Employee
        {
            public int Id { get; set; }
            public string Name { get; set; }
            //public List<Address> lstAddress { get; set; }
            //public List<Department> lstDepartment { get; set; }
            public List<string> lstHobbies { get; set; }
        }

        public class Address
        {
            public string State { get; set; }
            public string City { get; set; }
            public int ZipCode { get; set; }
            public List<Country> lstCoutry { get; set; }
        }

        public class Country
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class Department
        {
            public int Id { get; set; }
            public string DeptName { get; set; }
            public Address DeptAddress { get; set; }
        }
    }
}