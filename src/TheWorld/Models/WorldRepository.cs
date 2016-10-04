using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace TheWorld.Models
{
    public class WorldRepository :IWorldRepository
    {
        private readonly WorldContext _context;
        private ILogger<WorldRepository> _logger;

        public WorldRepository(WorldContext context, ILogger<WorldRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            _logger.LogInformation("Getting All trips from the database");
            return _context.Trips.ToList();
        }

        public void AddTrip(Trip trip)
        {
            _context.Add(trip);
        }

        public async Task<bool> SaveChangeAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
