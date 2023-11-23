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
public class ClienteController: BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClienteController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Cliente>>> Get()
        {
            var entidades = await _unitOfWork.Clientes.GetAllAsync();
            return _mapper.Map<List<Cliente>>(entidades);
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
                    var Qcliente = await _unitOfWork.Clientes.NoPay();
                    return Ok(Qcliente);
                case 2:
                    var Qcliente2 = await _unitOfWork.Clientes.PedidosClient();
                    return Ok(Qcliente2);
                case 3:
                    var Qcliente3 = await _unitOfWork.Clientes.InfoClient();
                    return Ok(Qcliente3);
                default:
                    return BadRequest("Consulta no válida");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Cliente>> Post(ClienteDto ClienteDto)
        {
            var entidad = _mapper.Map<Cliente>(ClienteDto);
            this._unitOfWork.Clientes.Add(entidad);
            await _unitOfWork.SaveAsync();
            if(entidad == null)
            {
                return BadRequest();
            }
            ClienteDto.Id = entidad.Id;
            return CreatedAtAction(nameof(Post), new {id = ClienteDto.Id}, ClienteDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClienteDto>> Put(int id, [FromBody] ClienteDto ClienteDto)
        {
            if(ClienteDto == null)
            {
                return NotFound();
            }
            var entidades = _mapper.Map<Cliente>(ClienteDto);
            _unitOfWork.Clientes.Update(entidades);
            await _unitOfWork.SaveAsync();
            return ClienteDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var entidad = await _unitOfWork.Clientes.GetByIdAsync(id);
            if(entidad == null)
            {
                return NotFound();
            }
            _unitOfWork.Clientes.Delete(entidad);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
