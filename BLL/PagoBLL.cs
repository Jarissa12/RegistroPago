using Microsoft.EntityFrameworkCore;
using RPrestamos.Entidades;
using RPrestamos.DAL;
using System.Linq.Expressions;

namespace RPrestamos.BLL
{
    public class PagoBLL
    {
        private Contexto _contexto;
        public PagoBLL(Contexto contexto)
        {
            _contexto = contexto;
        }

        public  async Task <bool> Guardar4(Pagos pago)
        {
            if (! await Existe4(pago.PagoId))
                return await this.Insertar4(pago);
            else
                return await this.Modificar4(pago);
        }
        public  async Task <bool>Existe4(int PagoId)
        {
            return   _contexto.Pago.Any(p => p.PagoId == PagoId);

        }   

         public  async Task <bool> Existe(int pagosId)
        {
            return await _contexto.Pago.AnyAsync(o => o.PagoId == pagosId);
        }

        
        private   async Task <bool> Insertar4(Pagos pagos)
        {
             _contexto.Pago.Add(pagos);
             
            foreach (var item in pagos.Detalle)
            {
                var prestamo =  _contexto.Prestamos.Find(item.PrestamoId);
                prestamo!.Balance -= item.ValorPagado;

            }
            var persona =  _contexto.Personas.Find(pagos.PersonaId);
            persona!.Balance -= pagos.Monto;

            var insertados =  _contexto.SaveChanges();

            return insertados > 0;
        }
        
        

        
        public  async Task <bool> Eliminar4(Pagos pagos)
        {

            var persona = _contexto.Personas.Find(pagos.PersonaId);
            persona!.Balance += pagos.Monto;

            foreach (var item in pagos.Detalle)
            {
                var prestamo =  _contexto.Prestamos.Find(item.PrestamoId);
                prestamo!.Balance += item.ValorPagado;
            }

            _contexto.Entry(pagos).State = EntityState.Deleted;

            return _contexto.SaveChanges() > 0;
        }

        
        public async Task <Pagos?>Buscar4(int pagosId)
        {
            return _contexto.Pago
            .Where(o => o.PagoId == pagosId)
            .Include(o => o.Detalle)
            .AsNoTracking()
            .SingleOrDefault();
        }
        
         public List<Pagos> Filtro2(DateTime fecha)
        {

            var fechas = _contexto.Pago
             .Where(f => f.Fecha.Date == fecha.Date)
             .AsNoTracking().ToList();
            return fechas;
        }


        
        private   async Task <bool> Modificar4(Pagos pagoActual)
        {
            var pagoAnterior = _contexto.Pago
                 .Where(p => p.PagoId == pagoActual.PagoId)
                 .AsNoTracking()
                 .SingleOrDefault();

            var Persona =  _contexto.Personas.Find(pagoAnterior!.PersonaId);
            Persona!.Balance += pagoAnterior.Monto;

            foreach (var item in pagoAnterior.Detalle)
            {
                var prestamos =  _contexto.Prestamos.Find(item.PrestamoId);
                prestamos!.Balance += item.ValorPagado;

            }
             _contexto.Database.ExecuteSqlRawAsync($"Delete FROM PagosDetalle Where PagoId = {pagoActual.PagoId}");

            foreach (var item in pagoActual.Detalle)
            {
                _contexto.Entry(item).State = EntityState.Added;

                var prestamo = _contexto.Prestamos.Find(item.PrestamoId);
                prestamo!.Balance -= item.ValorPagado;

            }

            Persona.Balance -= pagoActual.Monto;

            _contexto.Entry(pagoActual).State = EntityState.Modified;

            _contexto.Entry(pagoActual).State = EntityState.Detached;

            return _contexto.SaveChanges() > 0;

        }
        
       

        public  async Task <List<Prestamos>> GetPrestamos(Expression<Func<Prestamos, bool>> Criterio)
        {
            return _contexto.Prestamos
                .AsNoTracking()
                .Where(Criterio)
                .ToList();
        }


        public  async Task <List<Pagos>> GetPagos(Expression<Func<Pagos, bool>> Criterio)
        {
            return _contexto.Pago
                .AsNoTracking()
                .Where(Criterio)
                .ToList();
        }

        

        public async Task <List<Personas>> GetPersonas(Expression<Func<Personas, bool>> Criterio)
        {
            return _contexto.Personas
                .AsNoTracking()
                .Where(Criterio)
                .ToList();
        }

    }
}








