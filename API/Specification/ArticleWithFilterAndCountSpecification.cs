
using API.Entities;

namespace API.Specification
{
    public class ArticleWithFilterAndCountSpecification : BaseSpecification<Article>
    {
        public ArticleWithFilterAndCountSpecification(ArticleSpecParams articleSpecParams)
        : base(x =>
            (string.IsNullOrEmpty(articleSpecParams.Search) || x.Title.ToLower().Contains(articleSpecParams.Search.ToLower())) &&
            (!articleSpecParams.CategoryId.HasValue || x.CategoryId == articleSpecParams.CategoryId) &&
            (!articleSpecParams.StatusId.HasValue || x.CategoryId == articleSpecParams.StatusId)

        )
        {

        }
    }
}