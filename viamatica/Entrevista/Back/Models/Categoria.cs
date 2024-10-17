using System.ComponentModel.DataAnnotations;

namespace Back.Models
{
    public class Categoria
    {
        [Key] 
        public int CategoriaId { get; set; } 

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
    }
}
