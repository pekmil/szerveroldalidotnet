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
    public class InvitationsController : ControllerBase
    {
        private readonly IInvitationService _invitationService;
        private readonly IMapper _mapper;

        public InvitationsController(IInvitationService invitationService, IMapper mapper){
            _invitationService = invitationService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all Invitation.
        /// </summary>
        /// <response code="200">Successful query</response>
        /// <response code="500">Server error</response>
        // GET api/invitations/getall
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<InvitationReadDto>), 200)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Administrator, User")]
        public ActionResult<IEnumerable<InvitationReadDto>> GetAll()
        {
            var invitations = _invitationService.GetInvitations();
            var invitationDtos = invitations.Select(i => _mapper.Map<InvitationReadDto>(i));
            return Ok(invitationDtos);
        }

        /// <summary>
        /// Creates a new Invitation.
        /// </summary>
        /// <param name="invitationDto">New Invitation data</param>
        /// <response code="201">Successful create</response>
        /// <response code="500">Server error</response>
        // POST api/invitations/create
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([FromBody] InvitationCreateDto invitationDto)
        {
            var invitation = _mapper.Map<Invitation>(invitationDto);
            var i = await _invitationService.CreateInvitationAsync(invitation);
            return Created($"/api/invitations/getall", i);
        }

        /// <summary>
        /// Accepts a specific Invitation.
        /// </summary>
        /// <param name="personId">The unique ID of the Person</param>
        /// <param name="eventId">The unique ID of the Event</param>
        /// <response code="200">Successful update</response>
        /// <response code="500">Server error</response>
        // PUT api/invitations/accept/1/1
        [HttpPut("{personId}/{eventId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Administrator, User")]
        [Authorize(Policy = "AdultsOnly")]
        public async Task<IActionResult> Accept(int personId, int eventId)
        {
            await _invitationService.UpdateInvitationAsync(personId, eventId, InvitationStatus.Accepted);
            return Ok();
        }

        /// <summary>
        /// Declines a specific Invitation.
        /// </summary>
        /// <param name="personId">The unique ID of the Person</param>
        /// <param name="eventId">The unique ID of the Event</param>
        /// <response code="200">Successful update</response>
        /// <response code="500">Server error</response>
        // PUT api/invitations/decline/1/1
        [HttpPut("{personId}/{eventId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Administrator, User")]
        [Authorize(Policy = "AdultsOnly")]
        public async Task<IActionResult> Decline(int personId, int eventId)
        {
            await _invitationService.UpdateInvitationAsync(personId, eventId, InvitationStatus.Declined);
            return Ok();
        }
    }
}
