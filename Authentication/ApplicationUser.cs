using Microsoft.AspNetCore.Identity;

namespace JWTAuthentication.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        // Add any additional properties needed for your application here
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
