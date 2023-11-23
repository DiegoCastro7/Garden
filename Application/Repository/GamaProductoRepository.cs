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
    public class GamaProductoRepository : GenericStringRepository<GamaProducto> , IGamaProducto 
    { 
        public GardenContext _context { get; set; } 
        public GamaProductoRepository(GardenContext context) : base(context) 
        { 
            _context = context; 
        } 
    } 
} 
