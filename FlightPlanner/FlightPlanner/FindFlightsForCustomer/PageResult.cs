using FlightPlanner.Modules;

namespace FlightPlanner.FindFlights
{
    public class FlightPageResult
    {
        public int Page { get; set; }
        public int TotalItems { get; set; }
        public List<Flight> Items { get; set; }
    }
}