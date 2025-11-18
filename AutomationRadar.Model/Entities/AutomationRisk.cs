using AutomationRadar.Model.Enums;

namespace AutomationRadar.Model.Entities
{
    public class AutomationRisk
    {
        public int Id { get; set; }

        public int OccupationId { get; set; }              
        public RiskLevel RiskLevel { get; set; }           

        public int? HorizonYears { get; set; }             
        public string? Justification { get; set; }         
        public string? Source { get; set; }               

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public Occupation? Occupation { get; set; }
    }
}
