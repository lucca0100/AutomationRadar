using System.Text.Json.Serialization;
namespace AutomationRadar.Model.Entities
{
    public class Occupation
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;          
        public string? Description { get; set; }                  
        public string? Sector { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
       
        [JsonIgnore]
        public ICollection<AutomationRisk>? AutomationRisks { get; set; }

        [JsonIgnore]
        public ICollection<CareerTransition>? SourceTransitions { get; set; }  
        [JsonIgnore]
        public ICollection<CareerTransition>? TargetTransitions { get; set; }  

    }
}
