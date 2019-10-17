using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventApp.Models;
using EventApp.Models.Mappings;
using EventApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventApp.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;

        public EventsController(IEventService eventService, IMapper mapper){
            _eventService = eventService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all Event.
        /// </summary>
        /// <response code="200">Successful query</response>
        /// <response code="500">Server error</response>
        // GET api/events/getall
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EventReadDto>), 200)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Administrator, User")]
        public ActionResult<IEnumerable<EventReadDto>> GetAll()
        {
            var events = _eventService.GetEvents();
            var eventDtos = events.Select(e => _mapper.Map<EventReadDto>(e));
            return Ok(eventDtos);
        }

        /// <summary>
        /// Gets a specific Event.
        /// </summary>
        /// <param name="evtId">The unique ID of the Event</param>
        /// <response code="200">Successful query</response>
        /// <response code="500">Server error</response>
        // GET api/events/get/1
        [HttpGet("{evtId}")]
        [ProducesResponseType(typeof(EventReadDto), 200)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Administrator, User")]
        public async Task<ActionResult<EventReadDto>> Get(int evtId)
        {
            var evt = await _eventService.GetEventAsync(evtId);
            var evtDto = _mapper.Map<EventReadDto>(evt);
            return Ok(evtDto);
        }

        /// <summary>
        /// Creates a new Event.
        /// </summary>
        /// <param name="evtDto">New Event data</param>
        /// <response code="201">Successful create</response>
        /// <response code="500">Server error</response>
        // POST api/events/create
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([FromBody] EventCreateDto evtDto)
        {
            var evt = _mapper.Map<Event>(evtDto);
            var e = await _eventService.CreateEventAsync(evt);
            return Created($"/api/events/get/{e.Id}", e);
        }

        /// <summary>
        /// Updates a specific Event.
        /// </summary>
        /// <param name="evtId">The unique ID of the Event</param>
        /// <param name="evtDto">Updated Event data</param>
        /// <response code="200">Successful update</response>
        /// <response code="500">Server error</response>
        // PUT api/events/update/1
        [HttpPut("{evtId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Update(int evtId, [FromBody] EventUpdateDto evtDto)
        {
            var evt = _mapper.Map<Event>(evtDto);
            await _eventService.UpdateEventAsync(evt);
            return Ok();
        }

        /// <summary>
        /// Deletes a specific Event.
        /// </summary>
        /// <param name="evtId">The unique ID of the Event</param>
        /// <response code="200">Successful delete</response>
        /// <response code="500">Server error</response>
        // DELETE api/events/delete/1
        [HttpDelete("{evtId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int evtId)
        {
            await _eventService.DeleteEventAsync(evtId);
            return Ok();
        }

        /// <summary>
        /// Get all events and places.
        /// </summary>
        /// <response code="200">Successful query</response>
        /// <response code="500">Server error</response>
        // GET api/events/geteventsandplaces
        [HttpGet]
        [ProducesResponseType(typeof(EventReadDto), 200)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Administrator, User")]
        public ActionResult<object> GetEventsAndPlaces()
        {
            var obj = _eventService.GetEventsAndPlaces();
            return Ok(obj);
        }

        /// <summary>
        /// Create event and place
        /// </summary>
        /// <response code="201">Created</response>
        /// <response code="500">Server error</response>
        // POST api/events/createeventwithplace
        [HttpPost]
        [ProducesResponseType(typeof(EventReadDto), 200)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateEventWithPlace([FromBody] Event evt)
        {
            var obj = await _eventService.CreateEventWithPlace(evt, evt.Place);
            return Created($"/api/events/createeventwithplace", obj);
        }
    }
}
