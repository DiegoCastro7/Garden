using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class ProductoController: BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Producto>>> Get()
        {
            var entidades = await _unitOfWork.Productos.GetAllAsync();
            return _mapper.Map<List<Producto>>(entidades);
        }

        [HttpGet("Consulting")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Cliente>>> Get(int Consulta)
        {   
            switch (Consulta)
            {
                case 1:
                    var Qproduct = await _unitOfWork.Productos.MoreSell();
                    return Ok(Qproduct);
                case 3:
                    var Qproduct3 = await _unitOfWork.Productos.FirstMoreSell();
                    return Ok(Qproduct3);
                default:
                    return BadRequest("Consulta no válida");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Producto>> Post(ProductoDto ProductoDto)
        {
            var entidad = _mapper.Map<Producto>(ProductoDto);
            this._unitOfWork.Productos.Add(entidad);
            await _unitOfWork.SaveAsync();
            if(entidad == null)
            {
                return BadRequest();
            }
            ProductoDto.Id = entidad.Id;
            return CreatedAtAction(nameof(Post), new {id = ProductoDto.Id}, ProductoDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductoDto>> Put(int id, [FromBody] ProductoDto ProductoDto)
        {
            if(ProductoDto == null)
            {
                return NotFound();
            }
            var entidades = _mapper.Map<Producto>(ProductoDto);
            _unitOfWork.Productos.Update(entidades);
            await _unitOfWork.SaveAsync();
            return ProductoDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            var entidad = await _unitOfWork.Productos.GetByIdAsync(id);
            if(entidad == null)
            {
                return NotFound();
            }
            _unitOfWork.Productos.Delete(entidad);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
