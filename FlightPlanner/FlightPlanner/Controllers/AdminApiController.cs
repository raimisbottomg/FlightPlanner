using FlightPlanner.Modules;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FlightPlanner.FlightValidations;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private static readonly object _lock = new object();
        private readonly FlightPlannerDbContext _context;
        public AdminApiController(FlightPlannerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            lock( _lock) 
            { 

                var flight = _context.Flights.
                    Include(flight => flight.To).
                    Include(flight => flight.From).
                    SingleOrDefault(flight => flight.Id == id);

                _context.SaveChanges();

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

        [HttpPut]
        [Route("flights")]
        public IActionResult AddFlight(Flight flight)
        { 
            lock( _lock) {

                if (FlightValidation.IsValidFlight(flight))
                {
                    var existingFlight = _context.Flights.Any(f =>
                        f.DepartureTime.Equals(flight.DepartureTime) &&
                        f.ArrivalTime.Equals(flight.ArrivalTime) &&
                        f.Carrier.Equals(flight.Carrier) &&
                        f.From.AirportCode.Equals(flight.From.AirportCode) &&
                        f.From.Country.Equals(flight.From.Country) &&
                        f.From.City.Equals(flight.From.City) &&
                        f.To.AirportCode.Equals(flight.To.AirportCode) &&
                        f.To.Country.Equals(flight.To.Country) &&
                        f.To.City.Equals(flight.To.City));
                    if (existingFlight == true)
                    {
                        return Conflict(); 
                    }

                    _context.Flights.Add(flight);
                    _context.SaveChanges();
                    
                    var airportTo = new Airport
                    {
                        City = flight.To.City,
                        Country = flight.To.Country,        
                        AirportCode = flight.To.AirportCode 
                    };

                    _context.Airports.Add(airportTo);

                    var airportFrom = new Airport
                    {
                        City = flight.From.City,
                        Country = flight.From.Country,         
                        AirportCode = flight.From.AirportCode 
                    };

                    _context.Airports.Add(airportFrom);

                    _context.SaveChanges();

                    return Created("", flight);
                }

                return BadRequest();
            }   
        } 

        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            lock(_lock) 
            { 

                var flightToDelete = _context.Flights.FirstOrDefault(flight => flight.Id == id);
                _context.SaveChanges();

                if (flightToDelete == null)
                {
                    return Ok(); 
                }

                _context.Flights.Remove(flightToDelete);
                _context.SaveChanges(); 

                return Ok(); 
            }
        }
    }
}