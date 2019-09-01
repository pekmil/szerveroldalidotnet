using System.Linq;
using System.Threading.Tasks;
using EventApp.Models;

namespace EventApp.Services {
    public interface IPlaceService
    {
        Task<Place> CreatePlaceAsync(Place place);

        Task UpdatePlaceAsync(Place place);

        Task DeletePlaceAsync(int placeId);

        IQueryable<Place> GetPlaces();

        Task<Place> GetPlaceAsync(int placeId);
    }
}