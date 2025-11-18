using AutomationRadar.Business.Interfaces;
using AutomationRadar.Model.DTOs;
using AutomationRadar.Model.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AutomationRadar.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CareerTransitionController : ControllerBase
    {
        private readonly ICareerTransitionRepository _repository;
        private readonly IMapper _mapper;

        public CareerTransitionController(
            ICareerTransitionRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CareerTransition>>> GetAll()
        {
            var transitions = await _repository.GetAllAsync();
            return Ok(transitions);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CareerTransition>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("from/{originId:int}")]
        public async Task<ActionResult<IEnumerable<CareerTransition>>> GetByOrigin(int originId)
        {
            var result = await _repository.GetByOriginAsync(originId);
            return Ok(result);
        }

        [HttpGet("to/{destId:int}")]
        public async Task<ActionResult<IEnumerable<CareerTransition>>> GetByDestination(int destId)
        {
            var result = await _repository.GetByDestinationAsync(destId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CareerTransition>> Create([FromBody] CareerTransitionCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var transition = _mapper.Map<CareerTransition>(dto);
            transition.CreatedAt = DateTime.UtcNow;

            await _repository.AddAsync(transition);

            return CreatedAtAction(nameof(GetById), new { id = transition.Id }, transition);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CareerTransitionUpdateDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID da rota difere do ID enviado no corpo.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            _mapper.Map(dto, existing);
            existing.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(existing);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _repository.ExistsAsync(id))
                return NotFound();

            await _repository.DeleteAsync(id);

            return NoContent();
        }
    }
}
