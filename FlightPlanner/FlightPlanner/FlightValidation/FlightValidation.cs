using FlightPlanner.Modules;
using System.Globalization;

namespace FlightPlanner.FlightValidations
{
    public static class FlightValidation
    {
        public static bool IsValidFlight(Flight flight)
        {

            if (flight == null ||
                string.IsNullOrWhiteSpace(flight.Carrier) ||
                string.IsNullOrWhiteSpace(flight.DepartureTime) ||
                string.IsNullOrWhiteSpace(flight.ArrivalTime) ||
                flight.From == null ||
                flight.To == null ||
                string.IsNullOrWhiteSpace(flight.From.Country)||
                string.IsNullOrWhiteSpace(flight.From.City) ||
                string.IsNullOrWhiteSpace(flight.From.AirportCode) ||
                string.IsNullOrWhiteSpace(flight.To.Country) ||
                string.IsNullOrWhiteSpace(flight.To.City) ||
                string.IsNullOrWhiteSpace(flight.To.AirportCode))

            {
                return false;
            }

            if (flight.From.AirportCode.ToLower().Trim().Equals(flight.To.AirportCode.ToLower().Trim())&&
                flight.From.Country.ToLower().Trim().Equals(flight.To.Country.ToLower().Trim())&&
                flight.From.City.ToLower().Trim().Equals(flight.To.City.ToLower().Trim()))
            {
                return false;
            }

            if (!DateTime.TryParseExact(flight.DepartureTime, "yyyy-MM-dd HH:mm", 
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime departureDateTime) ||
                !DateTime.TryParseExact(flight.ArrivalTime, "yyyy-MM-dd HH:mm", 
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime arrivalDateTime))
            {
                return false;
            }

            if (departureDateTime >= arrivalDateTime)
            {
                return false;
            }

            return true;
        }
    }
}