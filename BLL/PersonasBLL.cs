using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RPrestamos.DAL;
using RPrestamos.Entidades;

namespace RPrestamos.BLL
{
     public class PersonasBLL
     {
          private Contexto contextos;

          public PersonasBLL(Contexto contexto)
          {
               contextos = contexto;
          }

          public bool Existe1(int PersonaId)
          {
               return contextos.Personas.Any(o => o.PersonaId == PersonaId);
          }

          private bool Insertar1(Personas persona)
          {
               contextos.Personas.Add(persona);
               return contextos.SaveChanges() > 0;
          }

          private bool Modificar1(Personas persona)
          {
               contextos.Entry(persona).State = EntityState.Modified;
               return contextos.SaveChanges() > 0;
          }

          public bool Guardar1(Personas persona)
          {
               if (!Existe1(persona.PersonaId))
                    return this.Insertar1(persona);
               else
                    return this.Modificar1(persona);
          }

          public bool Eliminar1(Personas persona)
          {
               contextos.Entry(persona).State = EntityState.Deleted;
               return contextos.SaveChanges() > 0;
          }

          public Personas? Buscar1(int personaId)
          {
               return contextos.Personas
                       .Where(o => o.PersonaId == personaId)
                       .AsNoTracking()
                       .SingleOrDefault();

          }

          public bool Editar(Personas personas)
          {
               if (!Existe1(personas.PersonaId))
                    return this.Insertar1(personas);
               else
                    return this.Modificar1(personas);
          }
          public List<Personas> GetPersonas(Expression<Func<Personas, bool>> Criterio)
          {
               return contextos.Personas
                   .AsNoTracking()
                   .Where(Criterio)
                   .ToList();
          }
          public List<Ocupaciones> GetOcupaciones(Expression<Func<Ocupaciones, bool>> Criterio){
            return contextos.Ocupaciones
                .AsNoTracking()
                .Where(Criterio)
                .ToList();
          }

     }
}