
using System.ComponentModel.DataAnnotations;

namespace API.CreateDtos
{
    public class ArticleCreateDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime PublicationDate { get; set; }
        [Required]
        public int StatusId { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}