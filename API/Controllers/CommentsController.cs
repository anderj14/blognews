
using API.CreateDtos;
using API.Dtos;
using API.Entities;
using API.Entities.Identity;
using API.Extensions;
using API.Interfaces;
using API.Specification;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CommentsController : BaseApiController
    {
        private readonly IGenericRepository<Comment> _commentRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public CommentsController(
            IGenericRepository<Comment> commentRepo,
            IMapper mapper,
            UserManager<AppUser> userManager
            )
        {
            _mapper = mapper;
            _commentRepo = commentRepo;
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
        public async Task<ActionResult<IReadOnlyList<CommentDto>>> GetComments()
        {
            var spec = new CommentWithSpecification();
            var comments = await _commentRepo.ListAsync(spec);
            var commentsDto = _mapper.Map<IReadOnlyList<CommentDto>>(comments);

            return Ok(commentsDto);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IReadOnlyList<CommentDto>>> GetComment(int id)
        {
            var spec = new CommentWithSpecification(id);
            var comment = await _commentRepo.GetEntityWithSpec(spec);
            var commentDto = _mapper.Map<CommentDto>(comment);

            return Ok(commentDto);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CommentDto>> CreateComment([FromBody] CommentCreateDto commentCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");

            try
            {
                var user = await GetAuthenticateUserAsync();

                if (user == null)
                    return Unauthorized("User not authenticated or not found");

                var newComment = _mapper.Map<Comment>(commentCreateDto);
                newComment.AppUserId = user.Id;
                newComment.CommentDate = DateTime.Now;

                await _commentRepo.Add(newComment);

                var comment = _mapper.Map<CommentDto>(newComment);

                return CreatedAtAction(nameof(GetComment), new { id = newComment.Id }, comment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}