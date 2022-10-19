using Microsoft.EntityFrameworkCore;
using RPrestamos.Entidades;
using RPrestamos.DAL;
using System.Linq.Expressions;

namespace RPrestamos.BLL
{
    public class OcupacionesBLL
    {
        private Contexto _contexto;
       public OcupacionesBLL(Contexto contexto){
            _contexto = contexto;
        }

        public bool Existe(int ocupacionId){
            return _contexto.Ocupaciones.Any(o => o.OcupacionId == ocupacionId);
        }

        private bool Insertar(Ocupaciones ocupacion){
            _contexto.Ocupaciones.Add(ocupacion);
            return _contexto.SaveChanges()> 0;
        }

        public bool Modificar(Ocupaciones ocupacion){
            _contexto.Entry(ocupacion).State = EntityState.Modified;
            return _contexto.SaveChanges()> 0;
        }

        public bool Guardar(Ocupaciones ocupacion){
            if (!Existe(ocupacion.OcupacionId))
                return this.Insertar(ocupacion);
            else
                return this.Modificar(ocupacion);
        }

        public bool Editar(Ocupaciones ocupacion){
            if (!Existe(ocupacion.OcupacionId))
                return this.Insertar(ocupacion);
            else
                return this.Modificar(ocupacion);
        }

        public bool Eliminar(Ocupaciones ocupacion){
            _contexto.Entry(ocupacion).State = EntityState.Deleted;
            return _contexto.SaveChanges() > 0;
        }

        public Ocupaciones? Buscar(int ocupacionId){
            return _contexto.Ocupaciones
                    .Where(o=> o.OcupacionId== ocupacionId)
                    .AsNoTracking()
                    .SingleOrDefault();
                    
        }
        

        public List<Ocupaciones> GetOcupaciones(Expression<Func<Ocupaciones, bool>> Criterio)
        {
            return _contexto.Ocupaciones
                .AsNoTracking()
                .Where(Criterio)
                .ToList();
        } 

        

    }
}