using Microsoft.AspNetCore.Mvc;
using FlightPlanner.FindFlights;
using Microsoft.EntityFrameworkCore;
using FlightPlanner.Modules;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerFlightApi : ControllerBase
    {
        private readonly FlightPlannerDbContext _context;
        private static readonly object _lock = new object();
        public CustomerFlightApi(FlightPlannerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("airports")]
        public IActionResult SearchAirports(string search)
        {
            lock (_lock) 
            { 
                if(search == null)
                {
                    return BadRequest();
                }

                search = search.Trim();

                Airport airport = _context.Airports.FirstOrDefault(airport =>
                        airport.City.ToUpper().Contains(search.ToUpper()) ||
                        airport.Country.ToUpper().Contains(search.ToUpper()) ||
                        airport.AirportCode.ToUpper().Contains(search.ToUpper()));
    
                if (airport != null)
                {
                    return Ok(new[] { airport });
                }

                else
                {
                    return BadRequest();
                }
            }

        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlights(SearchFlightRequest req)
        {
            lock(_lock) 
            { 
                if (req == null || req.From == null || req.To == null || req.DepartureDate == null)
                {
                    return BadRequest();
                }

                if (req.From.Equals(req.To))
                {
                    return BadRequest();
                }

                var flights = _context.Flights.Where(flight =>
                    flight.From.AirportCode.StartsWith(req.From) ||
                    flight.To.AirportCode.StartsWith(req.To) ||
                    flight.DepartureTime == req.DepartureDate
                    ).ToList();

                var pageResult = new FlightPageResult
                {
                Page = 0,
                TotalItems = flights.Count,
                Items = flights
                };

            return Ok(pageResult);
            }
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            lock (_lock)
            {
                var flight = _context.Flights.
                    Include(flight => flight.To).
                    Include(flight => flight.From).
                    SingleOrDefault(flight => flight.Id == id);

                if (flight != null)
                {
                    return Ok(flight);
                }
                else
                {
                    return NotFound();
                }
            }
        }
    }
}