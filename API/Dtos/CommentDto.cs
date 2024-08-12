
namespace API.Dtos
{
    public class CommentDto
    {
        public string Content { get; set; }
        public DateTime CommentDate { get; set; }
        public int ArticleId { get; set; }
        public string UserName { get; set; }
    }
}