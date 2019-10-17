using System;
using System.Security.Claims;
using System.Threading.Tasks;
using EventApp.Models;
using EventApp.Services;
using Microsoft.AspNetCore.Authorization;

namespace EventApp.Auth {

    public class AdultsOnlyHandler : AuthorizationHandler<AdultsOnlyRequirement> {

        private readonly IEventService _eventService;

        public AdultsOnlyHandler(IEventService eventService){
            _eventService = eventService;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                   AdultsOnlyRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth &&
                                            c.Issuer == "https://mik.uni-pannon.hu"))
            {
                return Task.FromResult(0);
            }

            if (!context.TryGetParamValue<int>("eventId", out var eventId))
            {
                return Task.FromResult(0);
            }

            Event evt = _eventService.GetEventAsync(eventId).Result;
            var adultsOnlyEvent = evt.AdultsOnly;

            var dateOfBirth = Convert.ToDateTime(
                context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth && 
                                            c.Issuer == "https://mik.uni-pannon.hu").Value);

            int calculatedAge = DateTime.Today.Year - dateOfBirth.Year;
            if (dateOfBirth > DateTime.Today.AddYears(-calculatedAge))
            {
                calculatedAge--;
            }

            if (!adultsOnlyEvent || calculatedAge >= requirement.RequiredMinimumAge)
            {
                context.Succeed(requirement);
            }
            return Task.FromResult(0);
        }
    }

}