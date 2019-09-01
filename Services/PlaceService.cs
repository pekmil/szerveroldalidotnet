using System.Linq;
using System.Threading.Tasks;
using EventApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Services {
    public class PlaceService : IPlaceService
    {
        private readonly EventAppDbContext _context;

        public PlaceService(EventAppDbContext context){
            _context = context;
        }

        public async Task<Place> CreatePlaceAsync(Place place)
        {
            await _context.AddAsync(place);
            await _context.SaveChangesAsync();
            return place;
        }

        public async Task DeletePlaceAsync(int placeId)
        {
            var place = await _context.FindAsync<Place>(placeId);
            _context.Remove(place);
            await _context.SaveChangesAsync();
        }

        public async Task<Place> GetPlaceAsync(int placeId)
        {
            var place = await _context.FindAsync<Place>(placeId);
            return place;
        }

        public IQueryable<Place> GetPlaces()
        {
            return _context.Places.AsNoTracking();
        }

        public async Task UpdatePlaceAsync(Place place)
        {
            _context.Update(place);
            await _context.SaveChangesAsync();
        }
    }
}