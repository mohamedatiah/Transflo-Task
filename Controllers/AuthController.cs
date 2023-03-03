using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security;
using System.Text.Json;
using TransfloDriver.Models;
using TransfloDriver.Models.Dto;
using TransfloDriver.Repository;
using TransfloDriver.Repository.IRepostiory;
using TransfloManager.Interface;
using TransfloManager.Services;

namespace TransfloDriver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : Controller
    {

        protected APIResponse _response;
        private readonly IDriverRepository _context;
        private IDriverService _driverService;
        private readonly IMapper _mapper;
        public AuthController(IDriverRepository context, IMapper mapper, IDriverService driverService)
        {
            _context = context;
            _mapper = mapper;
            _driverService = driverService;
            _response = new();

        }

        // POST: api/Auth/login
        [HttpPost("login")]
        public async Task<ActionResult<APIResponse>> login(Login driverDto)
        {
            try
            {
                Driver driver= await _context.
                     GetAsync(a => a.Email == driverDto.Email && a.Password == driverDto.Password);
                if(driver == null)
                {
                    _response.ErrorMessages = new List<string> { "Invalid Email or Password" };

                    _response.StatusCode = HttpStatusCode.BadRequest;

                    return Ok(_response);
                }

                var claims = AuthRespository.generateClaim(driverDto.Email,driverDto.Password);
                 string jwtSecurityToken = AuthRespository.GeneratejwtSecurityToken(claims);
                _response.Result = jwtSecurityToken;
                _response.StatusCode = HttpStatusCode.OK;
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
        // POST: api/Auth/register
        [HttpPost("register")]
        public async Task<ActionResult<APIResponse>> Register(DriverDTO driverDto)
        {
            try
            {
              var res= await _context.GetAsync(a => a.Email == driverDto.Email);
                if (res != null)
                {
                    _response.ErrorMessages = new List<string> { "This Email Registerd Before" };

                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return Ok(_response);
                }
                driverDto.IsAdmin = true;
                await _driverService.AddDriver(driverDto);
                _response.Result = driverDto;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode= HttpStatusCode.NotFound;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPost("refreshToken")]
        public async Task<ActionResult<APIResponse>> refreshToken(Login driverDto)
        {
            //todo
            return null;
        }
        [HttpPost("forgetPassword")]
        public async Task<ActionResult<APIResponse>> forgetPassword(Login driverDto)
        {
            //todo
            return null;
        }
    }
}
