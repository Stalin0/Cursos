using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back.Models
{
    public class Producto
    {
        [Key]
        public int ProductoId { get; set; }

        [Required]
        [StringLength(200)]
        public string Nombre { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }

        [Required]
        public int Stock { get; set; }


        [Required]
        public int CategoriaId { get; set; }  

        
        [StringLength(100)]
        public string? CategoriaNombre { get; set; }  

        
        [StringLength(500)] 
        public string? Descripcion { get; set; } 
    }
}
