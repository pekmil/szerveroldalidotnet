using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventApp.Models;
using EventApp.Models.Mappings;
using EventApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventApp.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        private readonly IPlaceService _placeService;
        private readonly IMapper _mapper;

        public PlacesController(IPlaceService placeService, IMapper mapper){
            _placeService = placeService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all Place.
        /// </summary>
        /// <response code="200">Successful query</response>
        /// <response code="500">Server error</response>
        // GET api/places/getall
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PlaceReadDto>), 200)]
        [ProducesResponseType(500)]
        public ActionResult<IEnumerable<PlaceReadDto>> GetAll()
        {
            var places = _placeService.GetPlaces();
            var placeDtos = places.Select(p => _mapper.Map<PlaceReadDto>(p));
            return Ok(placeDtos);
        }

        /// <summary>
        /// Gets a specific Place.
        /// </summary>
        /// <param name="placeId">The unique ID of the Place</param>
        /// <response code="200">Successful query</response>
        /// <response code="500">Server error</response>
        // GET api/places/get/1
        [HttpGet("{placeId}")]
        [ProducesResponseType(typeof(PlaceReadDto), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<PlaceReadDto>> Get(int placeId)
        {
            var place = await _placeService.GetPlaceAsync(placeId);
            var placeDto = _mapper.Map<PlaceReadDto>(place);
            return Ok(placeDto);
        }

        /// <summary>
        /// Creates a new Place.
        /// </summary>
        /// <param name="placeDto">New Place data</param>
        /// <response code="201">Successful create</response>
        /// <response code="500">Server error</response>
        // POST api/places/create
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Create([FromBody] PlaceCreateDto placeDto)
        {
            var place = _mapper.Map<Place>(placeDto);
            var p = await _placeService.CreatePlaceAsync(place);
            return Created($"/api/places/get/{p.Id}", p);
        }

        /// <summary>
        /// Updates a specific Place.
        /// </summary>
        /// <param name="placeId">The unique ID of the Place</param>
        /// <param name="placeDto">Updated Place data</param>
        /// <response code="200">Successful update</response>
        /// <response code="500">Server error</response>
        // PUT api/places/update/1
        [HttpPut("{placeId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Update(int placeId, [FromBody] PlaceUpdateDto placeDto)
        {
            var place = _mapper.Map<Place>(placeDto);
            await _placeService.UpdatePlaceAsync(place);
            return Ok();
        }

        /// <summary>
        /// Deletes a specific Place.
        /// </summary>
        /// <param name="placeId">The unique ID of the Place</param>
        /// <response code="200">Successful delete</response>
        /// <response code="500">Server error</response>
        // DELETE api/places/delete/1
        [HttpDelete("{placeId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete(int placeId)
        {
            await _placeService.DeletePlaceAsync(placeId);
            return Ok();
        }
    }
}
