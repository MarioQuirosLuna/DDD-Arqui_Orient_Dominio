﻿using Application.DTOs;
using AutoMapper;
using Entities.Models;
using Services.Services.Implements;
using SISCOA_API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace DDD.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class UsuarioController : ApiController
    {
        private IMapper _mapper;
        private readonly PersonService service = new PersonService();

        /// <summary>
        /// Constructor
        /// </summary>
        public UsuarioController()
        {
            this._mapper = WebApiApplication.MapperConfiguration.CreateMapper();
        }
        /// <summary>
        /// Obtiene todos los registros
        /// </summary>
        /// <returns>Lista de todos los registros</returns>
        /// <response code="200">OK. Devuelve la lista de los registros</response>
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Person_DTO>))]
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                var entities = await service.GetAll();
                var DTO = entities.Select(x => _mapper.Map<Person_DTO>(x));

                return Ok(DTO);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        /// <summary>
        /// Obtiene un registro por su id
        /// </summary>
        /// <remark>
        /// </remark>
        /// <param name="id">Id del registro</param>
        /// <returns>Registro</returns>
        /// <response code="200">OK. Devuelve la lista de los registros</response>
        /// <response code="404">NotFound. No se encontro el registro</response>
        [HttpGet]
        [ResponseType(typeof(Person_DTO))]
        public async Task<IHttpActionResult> GetById(int id)
        {
            try
            {
                var entities = await service.GetById(id);
                if (entities == null)
                    return NotFound();

                var DTO = _mapper.Map<Person_DTO>(entities);

                return Ok(DTO);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        /// <summary>
        /// Obtiene la persona de mayor edad
        /// </summary>
        /// <remark>
        /// </remark>
        /// <returns>Registro</returns>
        /// <response code="200">OK. Devuelve el registro</response>
        /// <response code="404">NotFound. No se encontro el registro</response>
        [Route("api/person/GetOldestPerson")]
        [HttpGet]
        [ResponseType(typeof(Person_DTO))]
        public async Task<IHttpActionResult> GetOldestPerson()
        {
            try
            {
                var oldestPerson = await service.GetOldestPerson();
                if (oldestPerson == null)
                    return NotFound();

                var DTO = _mapper.Map<Person_DTO>(oldestPerson);

                return Ok(DTO);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Crea un registro
        /// </summary>
        /// <param name="DTO">El objeto JSON del registro</param>
        /// <returns>Registro insertado</returns>
        /// <response code="200">OK. Devuelve la lista de los registros</response>
        /// <response code="400">BadRequest. Consulta erronea</response>
        /// <response code="500">InternalServerError. Error con el servidor</response>
        [HttpPost]
        public async Task<IHttpActionResult> Post(Person_DTO DTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var userSaved = _mapper.Map<Person>(DTO);
                userSaved = await service.Insert(userSaved);
                if (userSaved != null)
                {
                    return Ok(DTO);
                }
                else
                {
                    return Content(HttpStatusCode.BadRequest, "Consideraciones: El usuario ya existe, Contrasena/Identificacion menor a 8 caracteres");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        /// <summary>
        /// Actualiza un registro
        /// </summary>
        /// <param name="DTO">El objeto JSON del registro</param>
        /// <param name="id">Id del registro que quiere modificar</param>
        /// <returns>Registro modificado</returns>
        /// <response code="200">OK. Devuelve el registro modificado</response>
        /// <response code="400">BadRequest. Consulta erronea</response>
        /// <response code="404">NotFound. No se encontro el registro</response>
        /// <response code="500">InternalServerError. Error con el servidor</response>
        [HttpPut]
        [ResponseType(typeof(Person_DTO))]
        public async Task<IHttpActionResult> Put(Person_DTO DTO, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (DTO.IdPerson != id)
                return BadRequest("Object id does not match route id");

            var flag = await service.GetById(id);
            if (flag == null)
                return NotFound();

            try
            {
                var entities = _mapper.Map<Person>(DTO);
                entities = await service.Update(entities);
                return Ok(DTO);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        /// <summary>
        /// Elimina un registro
        /// </summary>
        /// <param name="id">Id del registro que quiere eliminar</param>
        /// <returns>OK</returns>
        /// <response code="200">OK. El registro fue eliminado</response>
        /// <response code="404">NotFound. No se encontro el registro</response>
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var flag = await service.GetById(id);
            if (flag == null)
                return NotFound();

            try
            {
                await service.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
