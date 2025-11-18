using AutomationRadar.Business.Interfaces;
using AutomationRadar.Model.DTOs;
using AutomationRadar.Model.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AutomationRadar.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OccupationsController : ControllerBase
    {
        private readonly IOccupationRepository _occupationRepository;
        private readonly IMapper _mapper;

        public OccupationsController(
            IOccupationRepository occupationRepository,
            IMapper mapper)
        {
            _occupationRepository = occupationRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Occupation>>> GetAll()
        {
            var occupations = await _occupationRepository.GetAllAsync();
            return Ok(occupations);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Occupation>> GetById(int id)
        {
            var occupation = await _occupationRepository.GetByIdAsync(id);

            if (occupation == null)
                return NotFound();

            return Ok(occupation);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Occupation>>> Search([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("O parâmetro 'name' é obrigatório.");

            var result = await _occupationRepository.SearchByNameAsync(name);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Occupation>> Create([FromBody] OccupationCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var occupation = _mapper.Map<Occupation>(dto);
            occupation.CreatedAt = DateTime.UtcNow;

            await _occupationRepository.AddAsync(occupation);

            return CreatedAtAction(nameof(GetById), new { id = occupation.Id }, occupation);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] OccupationUpdateDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID do corpo difere do ID da rota.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _occupationRepository.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            _mapper.Map(dto, existing);
            existing.UpdatedAt = DateTime.UtcNow;

            await _occupationRepository.UpdateAsync(existing);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _occupationRepository.ExistsAsync(id))
                return NotFound();

            await _occupationRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
