using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data
{
    public class GardenContext : DbContext
    {
        public GardenContext(DbContextOptions<GardenContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<DetallePedido> DetallePedidos { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<GamaProducto> GamaProductos { get; set; }
        public DbSet<Oficina> Oficinas { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Producto> Productos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
