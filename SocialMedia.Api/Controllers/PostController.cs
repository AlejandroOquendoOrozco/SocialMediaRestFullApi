
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SocialMedia.Api.Response;
using SocialMedia.Core.CustomEntitys;
using SocialMedia.Core.DTO;
using SocialMedia.Core.Entities;
using SocialMedia.Core.QueryFilters;
using SocialMedia.Core.Services;
using SocialMedia.Infraestructure.Interfaces;
using System.Net;
namespace SocialMedia.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {

        private readonly IPublicacionService _publicacionService;
        private readonly IMapper _mapper;
        private readonly IUriServices _uriServices;
        public PostController(IPublicacionService postRepsotory, IMapper mapper,IUriServices uriServices)
        {
            _mapper = mapper;
            _publicacionService = postRepsotory;
            _uriServices = uriServices;
        }


        /// <summary>
        /// Retrieve  All Post
        /// </summary>
        /// <param name="filters">Filters To App</param>
        /// <returns></returns>
        [HttpGet(Name =nameof(GetPosts))]
        [ProducesResponseType((int)HttpStatusCode.OK,Type =typeof(ApiResponse<IEnumerable<PublicacionDTO>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetPosts([FromQuery] PublicacionQueryFilter filters)
        {

            var post = _publicacionService.GetPostsAsync(filters);
            var postDTO = _mapper.Map<IEnumerable<PublicacionDTO>>(post);
            string ActionUrl = Url.RouteUrl(nameof(GetPosts))??"";

            var metadata = new MetaData
            {

                TotalCount = post.TotalCount,
                PageSize = post.PageSize,
                CurrentPage = post.CurrentPage,
                TotalPages = post.TotalPages,
                HasNextPage = post.HasNextPage,
                HasPreviusPage = post.HasPreviusPage,
                NextPageUrl = _uriServices.GetUri(filters, ActionUrl).ToString(),
                PreviusPageUrl = _uriServices.GetUri(filters, ActionUrl).ToString()
            };
            
            var response = new ApiResponse<IEnumerable<PublicacionDTO>>(postDTO){
                Meta=metadata,
            };
            Response.Headers.Add("X-Paginations", JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {

            var post = await _publicacionService.GetPublicacionAsync(id);
            var postDTO = _mapper.Map<PublicacionDTO>(post);
            var response = new ApiResponse<PublicacionDTO>(postDTO);
            return Ok(response);
        }



        [HttpPost]
        public async Task<IActionResult> InsertarPublicacion(PublicacionDTO postDTO)
        {

            var posts = _mapper.Map<Publicacion>(postDTO);
            var post = _mapper.Map<PublicacionDTO>(posts);
            await _publicacionService.AddPublicacion(posts);
            var response = new ApiResponse<PublicacionDTO>(post);


            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> ActualizarPublicacion(int id, PublicacionDTO postDTO)
        {

            var post = _mapper.Map<Publicacion>(postDTO);
            post.Id = id;
            var result = await _publicacionService.UpdatePost(post);
            var response = new ApiResponse<bool>(result);
            return Ok(response);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarPublicacionn(int id)
        {

            var result = await _publicacionService.DeletePost(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);

        }
    }
}