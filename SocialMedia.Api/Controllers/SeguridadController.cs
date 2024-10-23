using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.Response;
using SocialMedia.Core.DTO;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Enumerations;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infraestructure.Interfaces;

namespace SocialMedia.Api.Controllers
{
    
    [Authorize(Roles =nameof(RolType.Administrator))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SeguridadController : Controller
    {
        private readonly ISeguridadService _seguridadService;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;
        public SeguridadController(ISeguridadService seguridadService,IMapper mapper, IPasswordService passwordService) { 


            _seguridadService = seguridadService;
            _mapper = mapper;
            _passwordService = passwordService;
        
        
            
        
        }

        [HttpPost]
        public async Task<IActionResult> Seguridad(SeguridadDTO seguridadDTO) {

            var seguridad = _mapper.Map<Seguridad>(seguridadDTO);

            seguridad.Password= _passwordService.Hash(seguridadDTO.Password);
            var seguridaddto = _mapper.Map<SeguridadDTO>(seguridad);
            await _seguridadService.RegisterUser(seguridad);
            var response = new ApiResponse<SeguridadDTO>(seguridaddto);


            return Ok(response);
        }
    }
}
