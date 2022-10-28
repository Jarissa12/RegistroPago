using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RPrestamos.DAL;
using RPrestamos.Entidades;

namespace RPrestamos.BLL
{

    public class PrestamosBLL
    {

        private Contexto contextos;

        public PrestamosBLL(Contexto contexto)
        {
            contextos = contexto;
        }

        public async Task <bool>  Existe2(int PrestamosId)
        {
            return await contextos.Prestamos.AnyAsync(o => o.PrestamosId == PrestamosId);
        }

        public async Task <bool>  Insertar2(Prestamos prestamo)
        {
            contextos.Prestamos.AddAsync(prestamo);
             prestamo.Balance = prestamo.Monto;

            var persona = contextos.Personas.Find(prestamo.PersonaId);
            persona.Balance += prestamo.Monto;

            int cantidad = contextos.SaveChanges();

            return cantidad > 0;
        }

        public async Task <bool>  Modificar2(Prestamos prestamoActual)
        {
            //descontar el monto anterior
            var prestamoAnterior = contextos.Prestamos
                .Where(p => p.PrestamosId == prestamoActual.PrestamosId)
                .AsNoTracking()
                .SingleOrDefault();

            var personaAnterior = contextos.Personas.Find(prestamoAnterior.PersonaId);
            personaAnterior.Balance -= prestamoAnterior.Monto;

            contextos.Entry(prestamoActual).State = EntityState.Modified;

            //descontar el monto nuevo
            var persona = contextos.Personas.Find(prestamoActual.PersonaId);
            persona.Balance += prestamoActual.Monto;

            return await contextos.SaveChangesAsync() > 0;
        }


        
        public async Task <bool>  Eliminar2(Prestamos prestamo)
        {
            var persona = contextos.Personas.Find(prestamo.PersonaId);
            persona.Balance -= prestamo.Monto;

            contextos.Entry(prestamo).State = EntityState.Deleted;
            return await contextos.SaveChangesAsync() > 0;
        }


        public async Task <bool> Guardar2(Prestamos prestamos)
        {
            if (!await Existe2(prestamos.PrestamosId))
                return await this.Insertar2(prestamos);
            else
                return await this.Modificar2(prestamos);

                
        }



        public async Task <Prestamos?> Buscar2(int prestamosId)
        {
            return contextos.Prestamos
                    .Where(o => o.PrestamosId == prestamosId)
                    .AsNoTracking()
                    .SingleOrDefault();

        }


        public async Task <bool>  Editar(Prestamos prestamos)
        {
            if (!await Existe2(prestamos.PrestamosId))
                return await this.Insertar2(prestamos);
            else
                return await this.Modificar2(prestamos);
        }


        public  async Task <List<Prestamos>> GetPrestamos(Expression<Func<Prestamos, bool>> Criterio)
        {
            return contextos.Prestamos
                .AsNoTracking()
                .Where(Criterio)
                .ToList();
        }

        public  async Task <List<Personas>> GetPersonas(Expression<Func<Personas, bool>> Criterio)
        {
            return contextos.Personas
                .AsNoTracking()
                .Where(Criterio)
                .ToList();
        }
    }
}

