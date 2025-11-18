using AutomationRadar.Business.Interfaces;
using AutomationRadar.Model.DTOs;
using AutomationRadar.Model.Entities;
using AutomationRadar.Model.Enums;
using Microsoft.AspNetCore.Mvc;

namespace AutomationRadar.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AutomationAdvisorController : ControllerBase
    {
        private readonly IOccupationRepository _occupationRepository;
        private readonly IAutomationRiskRepository _automationRiskRepository;
        private readonly ICareerTransitionRepository _careerTransitionRepository;

        public AutomationAdvisorController(
            IOccupationRepository occupationRepository,
            IAutomationRiskRepository automationRiskRepository,
            ICareerTransitionRepository careerTransitionRepository)
        {
            _occupationRepository = occupationRepository;
            _automationRiskRepository = automationRiskRepository;
            _careerTransitionRepository = careerTransitionRepository;
        }

        [HttpGet("occupation/{occupationId:int}")]
        public async Task<ActionResult<OccupationAutomationAdvisorDto>> GetRecommendationByOccupation(int occupationId)
        {
            var occupation = await _occupationRepository.GetByIdAsync(occupationId);
            if (occupation == null)
                return NotFound($"Ocupação com Id {occupationId} não encontrada.");

            var risks = (await _automationRiskRepository.GetByOccupationIdAsync(occupationId)).ToList();

            AutomationRiskSummaryDto? mainRiskDto = null;

            if (risks.Any())
            {
                var mainRisk = risks
                    .OrderByDescending(r => r.RiskLevel)
                    .First();

                mainRiskDto = new AutomationRiskSummaryDto
                {
                    RiskLevel = (int)mainRisk.RiskLevel,
                    RiskLevelName = mainRisk.RiskLevel.ToString(), 
                    HorizonYears = mainRisk.HorizonYears,
                    Justification = mainRisk.Justification,
                    Source = mainRisk.Source
                };
            }

            var transitions = (await _careerTransitionRepository.GetByOriginAsync(occupationId)).ToList();

            var transitionDtos = transitions
                .Select(t => new TransitionSuggestionDto
                {
                    ToOccupationId = t.ToOccupationId,
                    ToOccupationName = t.ToOccupation?.Name ?? string.Empty,
                    RecommendedActions = t.RecommendedActions,
                    Priority = t.Priority
                })
                .OrderBy(t => t.Priority)
                .ToList();

            var response = new OccupationAutomationAdvisorDto
            {
                OccupationId = occupation.Id,
                OccupationName = occupation.Name,
                Sector = occupation.Sector,
                MainRisk = mainRiskDto,
                Transitions = transitionDtos
            };

            return Ok(response);
        }
    }
}
