
using API.CreateDtos;
using API.Dtos;
using API.Entities;
using API.Entities.Identity;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class StatusesController : BaseApiController
    {
        private readonly IGenericRepository<Status> _statusRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public StatusesController(
            IGenericRepository<Status> statusRepo,
            IMapper mapper,
            UserManager<AppUser> userManager

            )
        {
            _statusRepo = statusRepo;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<StatusDto>>> GetStatuses()
        {
            var statuses = await _statusRepo.ListAllAsync();
            var statusesDto = _mapper.Map<List<StatusDto>>(statuses);

            return Ok(statusesDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StatusDto>> GetStatus(int id)
        {
            var status = await _statusRepo.GetByIdAsync(id);
            var statusDto = _mapper.Map<StatusDto>(status);

            return Ok(statusDto);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<StatusDto>> CreateCategory([FromBody] StatusCreateDto statusCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");

            try
            {
                var newStatus = _mapper.Map<Status>(statusCreateDto);

                await _statusRepo.AddAsync(newStatus);

                var category = _mapper.Map<StatusDto>(newStatus);

                return CreatedAtAction(nameof(GetStatus), new { id = newStatus.Id }, category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateCategory(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");

            try
            {
                var status = await _statusRepo.GetByIdAsync(id);

                if (status == null)
                    return NotFound("Status not found");

                await _statusRepo.DeleteAsync(status);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}