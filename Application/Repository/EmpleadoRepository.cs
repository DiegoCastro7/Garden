using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
using Api.Repository; 
using Domain.Entities; 
using Domain.Interfaces; 
using Microsoft.EntityFrameworkCore; 
using Persistence.Data; 

namespace Application.Repository 
{ 
    public class EmpleadoRepository : GenericRepository<Empleado> , IEmpleado 
    { 
        public GardenContext _context { get; set; } 
        public EmpleadoRepository(GardenContext context) : base(context) 
        { 
            _context = context; 
        } 
        public async Task<IEnumerable<object>> NotClients() 
        {
            var result = from e in _context.Empleados
                            join c in _context.Clientes on e.Id equals c.CodigoEmpleadoRepVentas into Union
                            from clien in Union.DefaultIfEmpty()
                            join j in _context.Empleados on e.CodigoJefe equals j.Id
                            where clien == null
                            select new { Empleado = $"{e.Nombre} {e.Apellido1} {e.Apellido2}", e.Email, e.Puesto, Jefe = $"{j.Nombre} {j.Apellido1} {j.Apellido2}" };
            return await result.ToListAsync();
        }
    } 
} 
