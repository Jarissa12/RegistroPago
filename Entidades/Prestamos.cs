using System.ComponentModel.DataAnnotations;
using RPrestamos.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace RPrestamos.Entidades{


    public class Prestamos{

        [Key]
         
          [Required(ErrorMessage = "La Persona es requerido")]
          public  int PrestamosId {get; set;}
          [Required(ErrorMessage = "La fecha es requerida")]
          public DateTime Fecha {get; set;} = DateTime.Now ;
           public  int Vence {get; set;}

          
          [Required(ErrorMessage = "La el concepto es requerido")]
          public String? Concepto {get; set;}

          [Range(minimum: 100, maximum: 2000000, ErrorMessage = "El Monto no esta dentro del rango requerido ( entre 100 y 2,000,000)")]

          public  float Monto  {get; set;}
         // public float valorPagado{ get; set;}
       

          public float Balance {get; set;}
        
         
          [Range(1,int.MaxValue,ErrorMessage ="El selecionar una persona")]
          public int PersonaId { get; set;}
          
    }

} 