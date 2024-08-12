
namespace API.Dtos
{
    public class ArticleDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublicationDate { get; set; }
        public string StatusName { get; set; }
        public string CategoryName { get; set; }
        public string AuthorUserName { get; set; }
    }
}