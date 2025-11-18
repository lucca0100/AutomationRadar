namespace AutomationRadar.Model.DTOs
{
    public class TransitionSuggestionDto
    {
        public int ToOccupationId { get; set; }
        public string ToOccupationName { get; set; } = string.Empty;
        public string? RecommendedActions { get; set; }
        public int Priority { get; set; }
    }

    public class AutomationRiskSummaryDto
    {
        public int RiskLevel { get; set; }          
        public string RiskLevelName { get; set; } = string.Empty;
        public int? HorizonYears { get; set; }
        public string? Justification { get; set; }
        public string? Source { get; set; }
    }

    public class OccupationAutomationAdvisorDto
    {
        public int OccupationId { get; set; }
        public string OccupationName { get; set; } = string.Empty;
        public string? Sector { get; set; }

        public AutomationRiskSummaryDto? MainRisk { get; set; }

        public List<TransitionSuggestionDto> Transitions { get; set; } = new();
    }
}
