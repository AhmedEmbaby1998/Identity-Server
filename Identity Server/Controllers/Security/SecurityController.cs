using Identity_Server.Services.Security;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity_Server.Controllers.Security
{
    [ApiController]
    [Route("api/[Controller]")]
    public class SecurityController : Controller
    {
        private readonly ISecurityService _service;
        public SecurityController(ISecurityService securityService) => _service = securityService;


        [HttpPost("register")]
        public async Task<IActionResult> Regisetr([FromBody] DTOs.RegisterDto dto)
        {
            var result = await _service.RegisterAsync(dto);
            if (result.Ok)
                return Ok(result.Data);
            return BadRequest(result.Error);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromHeader] string email, [FromHeader] string password)
        {
            var result = await _service.Login(email, password);
            if (result.Ok)
                return Ok(result.Data);
            return BadRequest(result.Error);
        }
    }
}
