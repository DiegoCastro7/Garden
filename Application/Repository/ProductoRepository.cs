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
    public class ProductoRepository : GenericStringRepository<Producto> , IProducto 
    { 
        public GardenContext _context { get; set; } 
        public ProductoRepository(GardenContext context) : base(context) 
        { 
            _context = context; 
        } 
        public async Task<IEnumerable<object>> MoreSell()
        {
            var result = (from p in _context.Pedidos
                        join d in _context.DetallePedidos on p.Id equals d.Id into UPed
                        from detalle in UPed.DefaultIfEmpty()
                        join pr in _context.Productos on detalle.CodigoProducto equals pr.Id into UProd
                        from producto in UProd.DefaultIfEmpty()
                        group detalle by new { detalle.CodigoProducto, producto.Nombre } into g
                        orderby g.Sum(dp => dp.Cantidad) descending
                        select new
                        {
                            Producto = g.Key.Nombre,
                            Cantidad = g.Sum(x => x.Cantidad)
                        }).Take(20);
            return await result.ToListAsync();
        }
        public async Task<IEnumerable<object>> FirstMoreSell()
        {
            var result = (from p in _context.Pedidos
                        join d in _context.DetallePedidos on p.Id equals d.Id into UPed
                        from detalle in UPed.DefaultIfEmpty()
                        join pr in _context.Productos on detalle.CodigoProducto equals pr.Id into UProd
                        from producto in UProd.DefaultIfEmpty()
                        group detalle by new { detalle.CodigoProducto, producto.Nombre } into g
                        orderby g.Sum(dp => dp.Cantidad) descending
                        select new
                        {
                            Producto = g.Key.Nombre,
                            Cantidad = g.Sum(x => x.Cantidad)
                        }).FirstOrDefault();
            return await Task.FromResult(new List<object> { result });
        }
    } 
} 


