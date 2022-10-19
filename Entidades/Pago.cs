using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPrestamos.Entidades
{
    public class Pago
    {
        [Key]

        //Campos: Pagos(PagoId, Fecha, PersonaId, Concepto, Monto),   
        //Crear la Consulta de Pagos.  

        public int PagoId { get; set; }

        public float Monto { get; set; }

        public String? Concepto { get; set; }


        [Required(ErrorMessage = "La fecha es requerida")]
        public DateTime Fecha { get; set; }

        public int PersonaId { get; set; }

        [ForeignKey("PagoId")]

        public virtual List<PagosDetalle> Detalle { get; set; }

    }


    //PagosDetalle(Id, PagoId, PrestamoId, ValorPagado)


    public class PagosDetalle
    {


        public int Id { get; set; }
        public int PagoId { get; set; }

        public int PrestamoId { get; set; }

        public float ValorPagado { get; set; }

    }


}