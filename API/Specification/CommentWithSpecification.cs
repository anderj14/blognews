using API.Entities;

namespace API.Specification
{
    public class CommentWithSpecification : BaseSpecification<Comment>
    {
        public CommentWithSpecification()
        {
            AddInclude(a => a.AppUser);
        }
    }
}