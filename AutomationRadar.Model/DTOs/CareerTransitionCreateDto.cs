using System.ComponentModel.DataAnnotations;

namespace AutomationRadar.Model.DTOs
{
    public class CareerTransitionCreateDto
    {
        [Required(ErrorMessage = "A ocupação de origem é obrigatória.")]
        public int FromOccupationId { get; set; }

        [Required(ErrorMessage = "A ocupação de destino é obrigatória.")]
        public int ToOccupationId { get; set; }

        [Required(ErrorMessage = "As ações recomendadas são obrigatórias.")]
        [StringLength(2000, ErrorMessage = "As ações recomendadas podem ter no máximo 2000 caracteres.")]
        public string RecommendedActions { get; set; } = string.Empty;

        [Range(1, 10, ErrorMessage = "A prioridade deve ser um número entre 1 e 10.")]
        public int Priority { get; set; } = 1;
    }
}
