using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class CleanUpApiController : ControllerBase
    {
        private readonly FlightPlannerDbContext _context;
        public CleanUpApiController(FlightPlannerDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("clear")]
        public IActionResult Clear()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.ChangeTracker.AutoDetectChangesEnabled = false;

                    _context.Database.ExecuteSqlRaw("DELETE FROM Flights");
                    _context.Database.ExecuteSqlRaw("DELETE FROM Airports");

                    transaction.Commit();

                    return Ok();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return StatusCode(500);
                }
                finally
                {
                    _context.ChangeTracker.AutoDetectChangesEnabled = true;
                }
            }
        }
    }
}