using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
//using System.Web.Mvc;

namespace Web_Api_Test.Controllers
{
    [RoutePrefix("api/Employee")]
    //[Authorize(Roles = "Admin")]
    [
EnableCors(origins:
"*"
, headers:
"*"
, methods:
"*"
)
]
    public class EmployeeController : ApiController
    {
        Demo1Entities db = new Demo1Entities();


        [Route("GetEmployees")]
        public List<Employee> GetEmployees()
        {
            return db.Employees.ToList();
        }

        [Route("GetEmployeesByFirstName")]
        public List<Employee> GetEmployeesByFirstName(string FirstName)
        {
            return db.Employees.Where(x => x.FirstName.Equals(FirstName)).ToList();
        }



        [HttpPost]
        [Route("CreateEmployee")]

        public string CreateEmployee(Employee employee)
        {
            db.Employees.Add(employee);
            db.SaveChanges();
            return "Add successfully";

        }

        [HttpDelete]
        [Route("DeleteEmployee")]

        public string DeleteEmployee(int? id)
        {
            var getUser = db.Employees.Find(id);
            db.Employees.Remove(getUser);
            db.SaveChanges();
            return "Delete data successfully";


        }
    }
}