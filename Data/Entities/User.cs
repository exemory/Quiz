using Microsoft.AspNetCore.Identity;

namespace Data.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;

        public IEnumerable<Test> AllowedTests { get; set; } = new List<Test>();
    }
}