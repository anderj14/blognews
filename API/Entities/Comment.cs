
using API.Entities.Identity;

namespace API.Entities
{
    public class Comment: BaseEntity
    {
        public string Content { get; set; }
        public DateTime CommentDate { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public int ArticleId { get; set; }
        public Article Article { get; set; }
    }
}