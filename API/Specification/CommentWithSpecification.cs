using API.Entities;

namespace API.Specification
{
    public class CommentWithSpecification : BaseSpecification<Comment>
    {
        public CommentWithSpecification()
        {
            AddInclude(a => a.AppUser);
        }

        public CommentWithSpecification(int id)
        : base(a => a.Id == id)
        {
            AddInclude(a => a.AppUser);
        }
    }
}