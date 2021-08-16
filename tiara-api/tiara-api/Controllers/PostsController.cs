using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tiara_api.DTOs;
using tiara_api.Models;
using tiara_api.Repository.IRepository;

namespace tiara_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IGenCRUDRepository<Post> postRepo;
        private readonly IMapper mapper;

        public PostsController(
           IGenCRUDRepository<Post> _postRepo,
           IMapper _mapper
            )

        {
            postRepo = _postRepo;
            mapper = _mapper;

        }

        [HttpGet]
        [Route("getfeed")]
        public IActionResult GetPostsFeed()
        {
            var result = postRepo.GetWhereWithInclude(x => x.Active == true, "CreatedBy").OrderByDescending(x => x.CreatedDate);
            return Ok(mapper.Map(result, new List<PostDto>()));
        }

        [HttpPost]
        [Route("createpost/{caption}/{location}/{person}")]
        public async Task<IActionResult> CreatePost(string caption, string location, string person)
        {
            try
            {
                PostDto dto = new PostDto();
                dto.Caption = caption;
                dto.Location = location;

                if(person == "Boy")
                {
                    dto.CreatedById = 1;
                }
                else
                {
                    dto.CreatedById = 2;
                }
                
                dto.Active = true;
                dto.Deleted = false;
                dto.CreatedDate = DateTime.Now;

                var photo = Request.Form.Files[0];
                byte[] fileBytes;
                string base64;
                if (photo.Length > 0)
                {
                    // convert to base 64 for db
                    using (var binaryReader = new BinaryReader(photo.OpenReadStream()))
                    {
                        fileBytes = binaryReader.ReadBytes((int)photo.Length);
                        base64 = Convert.ToBase64String(fileBytes);
                        dto.Photo = base64;
                        await postRepo.AddAsync(mapper.Map(dto, new Post()));
                    }

                }

                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }

    }
}
