
using Microsoft.AspNetCore.Identity;

namespace API.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public ICollection<Article> Articles { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}