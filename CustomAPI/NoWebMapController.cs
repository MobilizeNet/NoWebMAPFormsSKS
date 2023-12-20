using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKS.CustomAPI
{
    [Route("api/NoWebMapController")]
    [ApiController]
    public class NoWebMapController : ControllerBase
    {

        [Route("GetCustomerInfo")]
        [HttpGet]
        public IDictionary<string, object> GetCustomer()
        {

            var result = new Dictionary<string, object>();
            CustomConnection.ExecuteSql("Select * from Customers");
            var customers = CustomConnection.rs;
            result.Add("Customers", customers);

            return result;
        }


        [Route("UpdateCustomer")]
        [HttpPost]
        public void UpdateCustomer([FromQuery]int id, [FromQuery] string name, [FromQuery] string lastName)
        {

            CustomConnection.ExecuteSql("UPDATE Customers SET ContactFirstName=\"" + name + "\", ContactLastName=\"" + lastName + "\" WHERE CustomerID = " + id + ";");
        }
    }
}
