using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DriverAPI.Models
{
    public class TaxiRequest
    {
        public string CustomerName { get; set; }
        public string PickupAddress { get; set; }
        public decimal Price { get; set; }
        public string DestinationAddress { get; internal set; }
        public double EstimatedTravelTimeInMinutes { get; internal set; }
        public double EstimatedDriverArrivalTimeInMinutes { get; internal set; }
        public ServiceStatuses ServiceStatus { get; internal set; }
    }
}
