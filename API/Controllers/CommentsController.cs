
using API.Dtos;
using API.Entities;
using API.Interfaces;
using API.Specification;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CommentsController : BaseApiController
    {
        private readonly IGenericRepository<Comment> _commentRepo;
        private readonly IMapper _mapper;

        public CommentsController(IGenericRepository<Comment> commentRepo, IMapper mapper)
        {
            _mapper = mapper;
            _commentRepo = commentRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CommentDto>>> GetAppointments()
        {
            var spec = new CommentWithSpecification();
            var comments = await _commentRepo.ListAsync(spec);
            var commentsDto = _mapper.Map<IReadOnlyList<CommentDto>>(comments);

            return Ok(commentsDto);
        }
    }
}