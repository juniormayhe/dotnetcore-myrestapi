using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DriverAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DriverAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class TaxiRequestController : Controller
    {


        // GET api/driver
        [HttpGet]
        public IEnumerable<TaxiRequest> GetTaxiRequestsHistory()
        {
            return new List<TaxiRequest> {
                new TaxiRequest { CustomerName = "Fernanda", PickupAddress ="POBLADO", DestinationAddress = "GIRARDOTA", Price = 140000, EstimatedTravelTimeInMinutes = TimeSpan.FromHours(2).TotalMinutes , EstimatedDriverArrivalTimeInMinutes = TimeSpan.FromMinutes(10).TotalMinutes, ServiceStatus = ServiceStatuses.TRAVEL_FINISHED },
                new TaxiRequest { CustomerName = "Claudia", PickupAddress ="AGUACATALA", DestinationAddress = "ENVIGADO", Price = 12000, EstimatedTravelTimeInMinutes = TimeSpan.FromMinutes(30).TotalMinutes , EstimatedDriverArrivalTimeInMinutes = TimeSpan.FromMinutes(5).TotalMinutes, ServiceStatus = ServiceStatuses.REQUEST_CANCELLED_BY_CUSTOMER  }
            };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
