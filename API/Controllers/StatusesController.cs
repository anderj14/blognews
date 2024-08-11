
using API.Dtos;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class StatusesController : BaseApiController
    {
        private readonly IGenericRepository<Status> _statusRepo;
        private readonly IMapper _mapper;

        public StatusesController(IGenericRepository<Status> statusRepo, IMapper mapper)
        {
            _statusRepo = statusRepo;
            _mapper = mapper;
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
    }
}