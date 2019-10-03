using System.Linq;
using System.Threading.Tasks;
using EventApp.Models;
using EventApp.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Services {
    public class PlaceService : IPlaceService
    {
        // private readonly EventAppDbContext _context;

        private readonly IUnitOfWork _unitOfWork;

        // public PlaceService(EventAppDbContext context){
        //     _context = context;
        // }

        public PlaceService(IUnitOfWork unitOfWork){
            _unitOfWork = unitOfWork;
        }

        public async Task<Place> CreatePlaceAsync(Place place)
        {
            // await _context.AddAsync(place);
            // await _context.SaveChangesAsync();
            // return place;

            await _unitOfWork.GetRepository<Place>().Create(place);
            await _unitOfWork.SaveChangesAsync();
            return place;
        }

        public async Task DeletePlaceAsync(int placeId)
        {
            // var place = await _context.FindAsync<Place>(placeId);
            // _context.Remove(place);
            // await _context.SaveChangesAsync();

            await _unitOfWork.GetRepository<Place>().Delete(placeId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Place> GetPlaceAsync(int placeId)
        {
            // var place = await _context.FindAsync<Place>(placeId);
            // return place;

            return await _unitOfWork.GetRepository<Place>().GetById(placeId);
        }

        public IQueryable<Place> GetPlaces()
        {
            // return _context.Places.AsNoTracking();

            return _unitOfWork.GetRepository<Place>().GetAll();
        }

        public async Task UpdatePlaceAsync(Place place)
        {
            // _context.Update(place);
            // await _context.SaveChangesAsync();

            await _unitOfWork.GetRepository<Place>().Update(place.Id, place);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}