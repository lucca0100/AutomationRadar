using AutomationRadar.Business.Interfaces;
using AutomationRadar.Model.DTOs;
using AutomationRadar.Model.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AutomationRadar.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AutomationRiskController : ControllerBase
    {
        private readonly IAutomationRiskRepository _repository;
        private readonly IMapper _mapper;

        public AutomationRiskController(
            IAutomationRiskRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutomationRisk>>> GetAll()
        {
            var risks = await _repository.GetAllAsync();
            return Ok(risks);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AutomationRisk>> GetById(int id)
        {
            var risk = await _repository.GetByIdAsync(id);

            if (risk == null)
                return NotFound();

            return Ok(risk);
        }

        [HttpGet("occupation/{occupationId:int}")]
        public async Task<ActionResult<IEnumerable<AutomationRisk>>> GetByOccupation(int occupationId)
        {
            var risks = await _repository.GetByOccupationIdAsync(occupationId);
            return Ok(risks);
        }

        [HttpPost]
        public async Task<ActionResult<AutomationRisk>> Create([FromBody] AutomationRiskCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var risk = _mapper.Map<AutomationRisk>(dto);
            risk.CreatedAt = DateTime.UtcNow;

            await _repository.AddAsync(risk);

            return CreatedAtAction(nameof(GetById), new { id = risk.Id }, risk);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] AutomationRiskUpdateDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID da rota difere do ID do corpo.");

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
