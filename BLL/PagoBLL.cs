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

        public bool Guardar4(Pago pago)
        {
            if (!Existe4(pago.PagoId))
                return this.Insertar4(pago);
            else
                return this.Modificar4(pago);
        }
        public bool Existe4(int PagoId)
        {
            return _contexto.Pago.Any(p => p.PagoId == PagoId);
        }
        
        

        public bool Eliminar4(Pago pago)
        {


            _contexto.Entry(pago).State = EntityState.Deleted;
            return _contexto.SaveChanges() > 0;


        }

        
        public Pago? Buscar4(int pagosId)
        {
            return _contexto.Pago
            .Where(o => o.PagoId == pagosId)
            .Include(o => o.Detalle)
            .AsNoTracking()
            .SingleOrDefault();
        }
        
         public List<Pago>Getlist (DateTime fecha)
        {

            var fechas = _contexto.Pago
             .Where(f => f.Fecha.Date == fecha.Date)
             .AsNoTracking().ToList();
            return fechas;
        }


        private bool Insertar4(Pago pagos)
        {
            _contexto.Pago.Add(pagos);

            foreach (var item in pagos.Detalle)
            {
                var pago = _contexto.Pago.Find(item.PagoId);

                pago.Monto += item.ValorPagado;

            }

            var insertados = _contexto.SaveChanges();

            return insertados > 0;
        }
        private bool Modificar4(Pago pago)
        {

            _contexto.Database.ExecuteSqlRaw($"Delete FROM PagosDetalle Where PagosId = {pago.PagoId}");

            foreach (var item in pago.Detalle)
            {
                _contexto.Entry(item).State = EntityState.Added;
            }
            _contexto.Entry(pago).State = EntityState.Modified;

            return _contexto.SaveChanges() > 0;

        }
        public List<Pago> GetPagos(Expression<Func<Pago, bool>> Criterio)
        {
            return _contexto.Pago
                .AsNoTracking()
                .Where(Criterio)
                .ToList();
        }

        public List<Prestamos> GetPrestamos(Expression<Func<Prestamos, bool>> Criterio)
        {
            return _contexto.Prestamos
                .AsNoTracking()
                .Where(Criterio)
                .ToList();
        }

        public List<Personas> GetPersonas(Expression<Func<Personas, bool>> Criterio)
        {
            return _contexto.Personas
                .AsNoTracking()
                .Where(Criterio)
                .ToList();
        }

    }
}








