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

          public async Task <bool> Existe1(int PersonaId)
          {
               return await contextos.Personas.AnyAsync(o => o.PersonaId == PersonaId);
          }

          private async Task <bool> Insertar1(Personas persona)
          {
               await contextos.Personas.AddAsync(persona);
               return contextos.SaveChanges() > 0;
          }

          private async Task <bool> Modificar1(Personas persona)
          {
               contextos.Entry(persona).State = EntityState.Modified;
               return await contextos.SaveChangesAsync() > 0;
          }

          public async Task <bool> Guardar1(Personas persona)
          {
               if (!await Existe1(persona.PersonaId))
                    return await this.Insertar1(persona);
               else
                    return await this.Modificar1(persona);
          }

          public async Task <bool> Eliminar1(Personas persona)
          {
               contextos.Entry(persona).State = EntityState.Deleted;
               return await contextos.SaveChangesAsync() > 0;
          }

          public  async Task<Personas?> Buscar1(int personaId)
          {
               return  contextos.Personas
                       .Where(o => o.PersonaId == personaId)
                       .AsNoTracking()
                       .SingleOrDefault();

          }

          public async Task <bool> Editar(Personas personas)
          {
               if (!await Existe1(personas.PersonaId))
                    return await  this.Insertar1(personas);
               else
                    return await this.Modificar1(personas);
          }
          public async Task <List<Personas>>  GetPersonas(Expression<Func<Personas, bool>> Criterio)
          {
               return contextos.Personas
                   .AsNoTracking()
                   .Where(Criterio)
                   .ToList();
          }
          public async Task <List<Ocupaciones>>  GetOcupaciones(Expression<Func<Ocupaciones, bool>> Criterio){
            return contextos.Ocupaciones
                .AsNoTracking()
                .Where(Criterio)
                .ToList();
          }

     }
}