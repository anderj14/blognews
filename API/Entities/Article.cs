
using API.Entities.Identity;

namespace API.Entities
{
    public class Article : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublicationDate { get; set; }

        public int StatusId { get; set; }
        public Status Status { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}