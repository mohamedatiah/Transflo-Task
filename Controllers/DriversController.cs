using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TransfloDriver.Models.Dto;
using TransfloDriver.Models;
using TransfloDriver.Repository.IRepostiory;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net.Http;
using TransfloManager.Interface;

namespace Transflo_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class DriversController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IDriverService _driverService;
        private IHttpContextAccessor _httpContext;
        public DriversController(IDriverService driverService,IHttpContextAccessor httpContext)
        {
            _driverService = driverService;
             _response = new();
            _httpContext = httpContext;

        }

        // GET: api/Drivers
        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetDrivers()
        {
            try
            {

                var res = _httpContext?.HttpContext?.User.Claims.Single(x => x.Type == "username").Value;
                var drivers=await _driverService.GetAll(res??"");
                _response.Result = drivers;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        // GET: api/Drivers/1
        [HttpGet("{id:int}")]
        public async Task<ActionResult<APIResponse>> GetDriver(int id)
        {
            try
            {
                if (id == 0)
                {
                    return Ok(_response.InvalidPropertyResponse("Id"));
                }
               var driver=await _driverService.GetDriver(id);
                if (driver == null)
                {
                    return Ok(_response.InvalidPropertyResponse("Id"));
                }
                _response.Result = driver;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        // PUT: api/Drivers/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<APIResponse>> UpdateDriver(int id, [FromBody] DriverDTO updateDTO)
        {
            try
            {
                if (updateDTO == null ||id==0)
                {
                   
                    return Ok(_response.InvalidData());
                }
               var driver=await _driverService.GetDriverByEmail(updateDTO.Email);
                if (driver!=null&&driver.Id!=id)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "email of driver already Exists!" };
                    _response.StatusCode = HttpStatusCode.BadRequest;

                    return _response;
                }


                await  _driverService.UpdateDriver(updateDTO);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;

                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }



        [HttpPost]
        public async Task<ActionResult<APIResponse>> CreateDriver([FromBody] DriverDTO createDTO)
        {
            try
            {
                if (createDTO == null)
                {
                    return Ok( _response.InvalidData());
                }
                var res =await _driverService.IsEmailExist(createDTO);
                if (res)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages = new List<string> { "email of driver already Exists!" };
                    return Ok( _response);
                }

                var username = _httpContext?.HttpContext?.User.Claims.Single(x => x.Type == "username").Value;
                createDTO.Createdby = username;

                await _driverService.AddDriver(createDTO);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = createDTO;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;

                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }







        // DELETE: api/Drivers/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<APIResponse>> DeleteDriver(int id)
        {
            try
            {
                if (id == 0||_driverService.GetDriver(id).Result==null)
                {
                    return Ok(_response.InvalidPropertyResponse("Id"));
                }
               await _driverService.DeleteDriver(id);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }


    }
}
