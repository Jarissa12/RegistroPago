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

        public async Task <bool> Existe(int ocupacionId){
            return await _contexto.Ocupaciones.AnyAsync(o => o.OcupacionId == ocupacionId);
        }

        private async Task <bool> Insertar(Ocupaciones ocupacion){
            await _contexto.Ocupaciones.AddAsync(ocupacion);
            return _contexto.SaveChanges()> 0;
        }

        public async Task <bool> Modificar(Ocupaciones ocupacion){
             _contexto.Entry(ocupacion).State = EntityState.Modified;
            return  await _contexto.SaveChangesAsync()> 0;
        }

        public async Task <bool> Guardar(Ocupaciones ocupacion){
            if (!await Existe(ocupacion.OcupacionId))
                return await this.Insertar(ocupacion);
            else
                return await this.Modificar(ocupacion);
        }

        public async Task <bool> Editar(Ocupaciones ocupacion){
            if (!await Existe(ocupacion.OcupacionId))
                return await this.Insertar(ocupacion);
            else
                return await this.Modificar(ocupacion);
        }

        public async Task <bool> Eliminar(Ocupaciones ocupacion){
            _contexto.Entry(ocupacion).State = EntityState.Deleted;
            return  await _contexto.SaveChangesAsync() > 0;
        }

        public async Task <Ocupaciones?> Buscar(int ocupacionId){
            return await _contexto.Ocupaciones
                    .Where(o=> o.OcupacionId== ocupacionId)
                    .AsNoTracking()
                    .SingleOrDefaultAsync();
                    
        }
        

        public async Task <List<Ocupaciones>> GetOcupaciones(Expression<Func<Ocupaciones, bool>> Criterio)
        {
            return await _contexto.Ocupaciones
                .AsNoTracking()
                .Where(Criterio)
                .ToListAsync();
        } 

        

    }
}