
using API.Entities;

namespace API.Specification
{
    public class ArticleWithAllSpecification : BaseSpecification<Article>
    {
        public ArticleWithAllSpecification(ArticleSpecParams articleSpecParams)
        : base(x =>
            (string.IsNullOrEmpty(articleSpecParams.Search) || x.Title.ToLower().Contains(articleSpecParams.Search.ToLower())) &&
            (!articleSpecParams.CategoryId.HasValue || x.CategoryId == articleSpecParams.CategoryId) &&
            (!articleSpecParams.StatusId.HasValue || x.StatusId == articleSpecParams.StatusId)
        )
        {
            AddOrderBy(a => a.Title);
            // AddInclude(a => a.Comments);
            AddInclude(a => a.Status);
            AddInclude(a => a.Category);
            AddInclude(a => a.AppUser);

            ApplyPaging(articleSpecParams.PageSize * (articleSpecParams.PageIndex - 1),
            articleSpecParams.PageSize);
        }


        public ArticleWithAllSpecification(int id)
        : base(a => a.Id == id)
        {
            AddInclude(a => a.Comments);
            AddInclude(a => a.Status);
            AddInclude(a => a.Category);
            AddInclude(a => a.AppUser);
        }
    }
}