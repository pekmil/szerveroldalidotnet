using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventApp.Models;
using EventApp.Models.Mappings;
using EventApp.Repository;
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
    public class PeopleController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;        

        public PeopleController(IPersonService personService, IMapper mapper){
            _personService = personService;
            _mapper = mapper;            
        }

        /// <summary>
        /// Gets all People.
        /// </summary>
        /// <response code="200">Successful query</response>
        /// <response code="500">Server error</response>
        // GET api/people/getall
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PersonReadDto>), 200)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Administrator, User")]
        public ActionResult<IEnumerable<PersonReadDto>> GetAll()
        {
            var people = _personService.GetPeople();
            var peopleDtos = people.Select(p => _mapper.Map<PersonReadDto>(p));
            return Ok(peopleDtos);
        }

        /// <summary>
        /// Gets a specific Person.
        /// </summary>
        /// <param name="personId">The unique ID of the Person</param>
        /// <response code="200">Successful query</response>
        /// <response code="500">Server error</response>
        // GET api/people/get/1
        [HttpGet("{personId}")]
        [ProducesResponseType(typeof(PersonReadDto), 200)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Administrator, User")]
        public async Task<ActionResult<PersonReadDto>> Get(int personId)
        {
            var person = await _personService.GetPersonAsync(personId);
            var personDto = _mapper.Map<PersonReadDto>(person);
            return Ok(personDto);
        }

        /// <summary>
        /// Creates a new Person.
        /// </summary>
        /// <param name="personDto">New Person data</param>
        /// <response code="201">Successful create</response>
        /// <response code="500">Server error</response>
        // POST api/people/create
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([FromBody] PersonCreateDto personDto)
        {
            var person = _mapper.Map<Person>(personDto);
            var p = await _personService.CreatePersonAsync(person);
            return Created($"/api/people/get/{p.Id}", p);
        }

        /// <summary>
        /// Updates a specific Person.
        /// </summary>
        /// <param name="personId">The unique ID of the Person</param>
        /// <param name="personDto">Updated Person data</param>
        /// <response code="200">Successful update</response>
        /// <response code="500">Server error</response>
        // PUT api/people/update/1
        [HttpPut("{peopleId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Update(int personId, [FromBody] PersonUpdateDto personDto)
        {
            var person = _mapper.Map<Person>(personDto);
            await _personService.UpdatePersonAsync(person);
            return Ok();
        }

        /// <summary>
        /// Deletes a specific Person.
        /// </summary>
        /// <param name="personId">The unique ID of the Person</param>
        /// <response code="200">Successful delete</response>
        /// <response code="500">Server error</response>
        // DELETE api/people/delete/1
        [HttpDelete("{personId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int personId)
        {
            await _personService.DeletePersonAsync(personId);
            return Ok();
        }

        /// <summary>
        /// Gets all People who birth after 1990.
        /// </summary>
        /// <response code="200">Successful query</response>
        /// <response code="500">Server error</response>
        // GET api/people/getafter1990
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PersonReadDto>), 200)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Administrator, User")]
        public ActionResult<IEnumerable<PersonReadDto>> GetAfter1990()
        {
            var people = _personService.GetAfter1990();
            var peopleDtos = people.Select(p => _mapper.Map<PersonReadDto>(p));
            return Ok(peopleDtos);
        }
    }
}
