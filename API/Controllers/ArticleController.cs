
using API.CreateDtos;
using API.Dtos;
using API.Entities;
using API.Entities.Identity;
using API.Extensions;
using API.Helper;
using API.Interfaces;
using API.Specification;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ArticleController : BaseApiController
    {
        private readonly IGenericRepository<Article> _articleRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public ArticleController(
            IGenericRepository<Article> articleRepo,
            IMapper mapper,
            UserManager<AppUser> userManager
            )
        {
            _articleRepo = articleRepo;
            _mapper = mapper;
            _userManager = userManager;
        }

        protected async Task<AppUser> GetAuthenticateUserAsync()
        {
            var email = User.GetEmail();

            if (string.IsNullOrEmpty(email))
                return null;

            var user = await _userManager.FindByEmailAsync(email);
            return user;
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

        [HttpPost]
        [Authorize(Roles = "Author, Admin")]
        public async Task<ActionResult<ArticleDto>> CreateArticle([FromBody] ArticleCreateDto articleCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");

            try
            {
                var user = await GetAuthenticateUserAsync();

                if (user == null)
                    return Unauthorized();

                var newArticle = _mapper.Map<Article>(articleCreateDto);
                newArticle.AppUserId = user.Id;
                newArticle.PublicationDate = DateTime.Now;

                await _articleRepo.AddAsync(newArticle);

                var article = _mapper.Map<ArticleDto>(newArticle);

                return CreatedAtAction(nameof(GetArticle), new { id = newArticle.Id }, article);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Author, Admin")]
        public async Task<ActionResult<ArticleDto>> UpdateArticle(int id, ArticleCreateDto articleUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }

            try
            {
                var user = await GetAuthenticateUserAsync();

                if (user == null)
                    return Unauthorized();

                var spec = new ArticleWithAllSpecification(id);

                var article = await _articleRepo.GetByIdAsync(id);

                if (article == null)
                {
                    return NotFound("Article not found");
                }

                if (article.AppUserId != user.Id)
                {
                    return NotFound("User not authorized");
                }

                _mapper.Map(articleUpdateDto, article);
                await _articleRepo.UpdateAsync(article);

                var articleUpdated = _mapper.Map<ArticleDto>(article);

                return Ok(articleUpdated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}