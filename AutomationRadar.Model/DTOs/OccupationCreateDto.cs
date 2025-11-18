using System.ComponentModel.DataAnnotations;

namespace AutomationRadar.Model.DTOs
{
    public class OccupationCreateDto
    {
        [Required(ErrorMessage = "O nome da ocupação é obrigatório.")]
        [StringLength(200, ErrorMessage = "O nome da ocupação deve ter no máximo 200 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "A descrição deve ter no máximo 1000 caracteres.")]
        public string? Description { get; set; }

        [StringLength(200, ErrorMessage = "O setor deve ter no máximo 200 caracteres.")]
        public string? Sector { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
