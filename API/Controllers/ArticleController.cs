
using API.Dtos;
using API.Entities;
using API.Entities.Identity;
using API.Helper;
using API.Interfaces;
using API.Specification;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ArticleController : BaseApiController
    {
        private readonly IGenericRepository<Article> _articleRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public ArticleController(IGenericRepository<Article> articleRepo, IMapper mapper, UserManager<AppUser> userManager)
        {
            _articleRepo = articleRepo;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        // [Authorize]
        public async Task<ActionResult<Pagination<ArticleDto>>> GetArticles(
            [FromQuery] ArticleSpecParams articleSpecParams
            )
        {
            var spec = new ArticleWithAllSpecification(articleSpecParams);
            var countSpec = new ArticleWithFilterAndCountSpecification(articleSpecParams);

            var totalItems = await _articleRepo.CountAsync(countSpec);

            var patients = await _articleRepo.ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<ArticleDto>>(patients);

            return Ok(new Pagination<ArticleDto>(articleSpecParams.PageIndex, articleSpecParams.PageSize, totalItems, data));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleDto>> GetArticle(int id)
        {
            var spec = new ArticleWithAllSpecification(id);

            var article = await _articleRepo.GetEntityWithSpec(spec);
            var data = _mapper.Map<ArticleDto>(article);

            return Ok(data);
        }
    }
}