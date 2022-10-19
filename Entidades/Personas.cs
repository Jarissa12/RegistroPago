
using System.ComponentModel.DataAnnotations;

namespace RPrestamos.Entidades
{
    public class Personas
    {
        [Key]

        public int PersonaId { get; set; }

        [Required(ErrorMessage = "La nombre de la persona es requerido")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El numero de telefono es requerido")]
        public double Telefono { get; set; }

        [Required(ErrorMessage = "El numero de celular es requerido")]
        public double Celular { get; set; }

        [Required(ErrorMessage = "El Email es requerido")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "La direccion es requerida")]
        public string? Direccion { get; set; }


        [Required(ErrorMessage = "La fecha es requerida")]
        public DateTime FechaNacimiento { get; set; } = DateTime.Now;

        [Range(1, int.MaxValue, ErrorMessage = "El selecionar una ocupacion")]

        public int OcupacionId { get; set; }

        public float Balance { get; set; }

    }
}