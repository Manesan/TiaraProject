using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tiara_api.DTOs;
using tiara_api.Models;
using tiara_api.Repository;
using tiara_api.Repository.IRepository;

namespace tiara_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThoughtController : ControllerBase
    {
        private IThoughtsRepository thoughtsRepo;
        private readonly IGenCRUDRepository<Thought> thoughtGenRepo;
        private readonly IUserRepository userRepo;
        private readonly IMapper mapper;

        public ThoughtController(
            IThoughtsRepository _thoughtsRepo,
            IGenCRUDRepository<Thought> _thoughtGenRepo,
            IUserRepository _userRepo,
            IMapper _mapper
            )
        {
            thoughtsRepo = _thoughtsRepo;
            thoughtGenRepo = _thoughtGenRepo;
            userRepo = _userRepo;
            mapper = _mapper;
        }

        [HttpGet]
        [Route("getgirlthoughts")]
        //[Authorize(Roles = "Admin")]
        public IActionResult GetGirlThoughts(string token)
        {
            var result = thoughtGenRepo.GetWhereWithInclude(x => x.CreatedBy.Role == "Girl", "CreatedBy").OrderByDescending(x => x.CreatedDate).ToList();
            return Ok(mapper.Map(result, new List<ThoughtDto>()));
        }

        [HttpGet]
        [Route("getboythoughts")]
        //[Authorize(Roles = "Boy")]
        public IActionResult GetBoyThoughts()
        {
            var result = thoughtGenRepo.GetWhereWithInclude(x => x.CreatedBy.Role == "Boy", "CreatedBy").OrderByDescending(x => x.CreatedDate).ToList();
            return Ok(mapper.Map(result, new List<ThoughtDto>()));
        }


        [HttpPost]
        [Route("createthought/{person}")]
        public async Task<IActionResult> CreateThought(string person, [FromBody] ThoughtDto dto)
        {
            if(person == "Boy")
            {
                dto.CreatedById = 1; //boy
            }
            else
            {
                dto.CreatedById = 2; //girl
            }
            dto.CreatedDate = DateTime.Now;
            var result = await thoughtGenRepo.AddAsync(mapper.Map(dto, new Thought()));
            dto.Id = result;
            return Ok(dto);
        }
    }
}
