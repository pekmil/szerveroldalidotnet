using Microsoft.AspNetCore.Authorization;

namespace EventApp.Auth {

    public class AdultsOnlyRequirement : IAuthorizationRequirement 
    {
        public int RequiredMinimumAge { get; }

        public AdultsOnlyRequirement(int requiredMinimumAge){
            RequiredMinimumAge = requiredMinimumAge;
        }
    }

}