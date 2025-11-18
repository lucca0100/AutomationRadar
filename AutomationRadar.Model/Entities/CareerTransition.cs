namespace AutomationRadar.Model.Entities
{
    public class CareerTransition
    {
        public int Id { get; set; }

        public int FromOccupationId { get; set; }      
        public int ToOccupationId { get; set; }       

        public string? RecommendedActions { get; set; } 
        public int Priority { get; set; } = 1;          

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public Occupation? FromOccupation { get; set; }
        public Occupation? ToOccupation { get; set; }
    }
}
