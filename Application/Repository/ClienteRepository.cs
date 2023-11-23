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
    } 
} 
