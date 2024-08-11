
namespace API.Entities
{
    public class Status: BaseEntity
    {
        public string StatusName { get; set; }

        public ICollection<Article> Articles{ get; set; }
    }
}