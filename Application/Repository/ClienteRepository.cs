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
    public class ClienteRepository : GenericRepository<Cliente> , ICliente 
    { 
        public GardenContext _context { get; set; } 
        public ClienteRepository(GardenContext context) : base(context) 
        { 
            _context = context; 
        } 
        public async Task<IEnumerable<object>> NoPay() 
        {
            var result = from c in _context.Clientes
                        join e in _context.Empleados on c.CodigoEmpleadoRepVentas equals e.Id into UEmp
                        from representante in UEmp.DefaultIfEmpty()
                        join oficina in _context.Oficinas on representante.CodigoOficina equals oficina.Id into UOff
                        from ciudadOficina in UOff.DefaultIfEmpty()
                        where !_context.Pagos.Any(p => p.Id == c.Id)
                        select new
                        {
                            Cliente = c.NombreCliente,
                            Representante = representante != null ? $"{representante.Nombre} {representante.Apellido1}" : "No asignado",
                            Ciudad = ciudadOficina != null ? ciudadOficina.Ciudad : "No asignado"
                        };
            return await result.ToListAsync();
        }
        public async Task<IEnumerable<object>> PedidosClient() 
        {
            var result = from c in _context.Clientes
                        join e in _context.Empleados on c.CodigoEmpleadoRepVentas equals e.Id into UEmp
                        from representante in UEmp.DefaultIfEmpty()
                        join p in _context.Pedidos on c.Id equals p.CodigoCliente into UPed
                        from pedidos in UPed.DefaultIfEmpty()
                        select new
                        {
                            Cliente = c.NombreCliente,
                            Pedidos = pedidos != null ? pedidos.Id.ToString() : "No ha hecho pedidos"
                        };
            return await result.ToListAsync();
        }
        public async Task<IEnumerable<object>> InfoClient() 
        {
            var result = from c in _context.Clientes
                        join e in _context.Empleados on c.CodigoEmpleadoRepVentas equals e.Id into UEmp
                        from representante in UEmp.DefaultIfEmpty()
                        join oficina in _context.Oficinas on representante.CodigoOficina equals oficina.Id into UOff
                        from ciudadOficina in UOff.DefaultIfEmpty()
                        select new
                        {
                            Cliente = c.NombreCliente,
                            Representante = representante != null ? $"{representante.Nombre} {representante.Apellido1}" : "No asignado",
                            Ciudad = ciudadOficina != null ? ciudadOficina.Ciudad : "No asignado"
                        };
            return await result.ToListAsync();
        }
    } 
} 
