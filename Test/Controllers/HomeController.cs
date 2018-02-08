
using CustomJsonParser;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Test.Models.Class1;

namespace Test.Controllers
{
    public class HomeController : Controller
    {
        public ContentResult Index()
        {
            var employee = GetEmployee();

            //THEM
            var Inbuilt = JsonConvert.SerializeObject(employee);

            //MINE
            JsonParser jsonParser = new JsonParser();
            var Custom = jsonParser.ConvertToJsonString(employee);

            return Content("Custom :<br>" + Custom + " <br/><br/> Inbuilt : <br> " + Inbuilt);
        }

        /// <summary>
        /// Employee class with all data
        /// </summary>
        /// <returns>Employee Object</returns>
        public Employee GetEmployee()
        {
            //If you want to see a bit complex example comment out below and related code
            return new Employee()
            {
                Id = 1,
                Name = "Akshay Tilekar",
                lstHobbies = new List<string>() { "Coding", "Problem Solving", "UI Desigining" },
                //lstAddress = new List<Address>() {
                //    new Address() {
                //        City = "PUNE",
                //        State = "Mh",
                //        ZipCode =411037,
                //        lstCoutry = new List<Country>(){
                //            new Country() {
                //                Id = 1,
                //                Name = "INDIA"
                //            },
                //            new Country(){
                //                Id=2,
                //                Name = "USA"
                //            }
                //        },
                //    }
                //},
                //lstDepartment = new List<Department>() {
                //    new Department(){
                //        Id = 1,
                //        DeptName = "Comp Sci",
                //        DeptAddress = new Address(){
                //            City = "NAGPUR",
                //            State ="UK",
                //            ZipCode = 5443,
                //            lstCoutry = new List<Country>(){
                //                new Country(){
                //                    Id =5,
                //                    Name = "Dubai"
                //                },
                //                new Country(){
                //                    Id = 10,`
                //                    Name = "Austrailia"
                //                }
                //            }
                //        }
                //    }
                //}
            };
        }
    }
}