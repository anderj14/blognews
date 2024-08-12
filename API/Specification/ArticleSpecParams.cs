
namespace API.Specification
{
    public class ArticleSpecParams
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;

        private int _pageSize = 10;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize || value <= 0) ? MaxPageSize : value;
        }

        public int? StatusId { get; set; }
        public int? CategoryId { get; set; }

        private string _search;
        public string? Search
        {
            get => _search;
            set => _search = value?.ToLower();
        }
    }
}