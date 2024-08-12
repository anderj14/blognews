
using System.ComponentModel.DataAnnotations;

namespace API.CreateDtos
{
    public class CommentCreateDto
    {
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime CommentDate { get; set; }
        [Required]
        public int ArticleId { get; set; }
    }
}