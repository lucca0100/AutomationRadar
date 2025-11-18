using System.ComponentModel.DataAnnotations;
using AutomationRadar.Model.Enums;

namespace AutomationRadar.Model.DTOs
{
    public class AutomationRiskUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "É necessário informar o Id da ocupação.")]
        public int OccupationId { get; set; }

        [Required(ErrorMessage = "O nível de risco é obrigatório.")]
        [Range(1, 3, ErrorMessage = "O nível de risco deve ser 1 (Low), 2 (Medium) ou 3 (High).")]
        public RiskLevel RiskLevel { get; set; }

        [Range(1, 50, ErrorMessage = "O horizonte de anos deve estar entre 1 e 50.")]
        public int? HorizonYears { get; set; }

        [StringLength(2000, ErrorMessage = "A justificativa deve ter no máximo 2000 caracteres.")]
        public string? Justification { get; set; }

        [StringLength(500, ErrorMessage = "A fonte deve ter no máximo 500 caracteres.")]
        public string? Source { get; set; }
    }
}
