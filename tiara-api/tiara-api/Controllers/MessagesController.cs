using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tiara_api.DTOs;
using tiara_api.Models;
using tiara_api.Repository.IRepository;

namespace tiara_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IGenCRUDRepository<Message> messageRepo;
        private readonly IMapper mapper;

        public MessagesController(
           IGenCRUDRepository<Message> _messageRepo,
           IMapper _mapper
            )

        {
            messageRepo = _messageRepo;
            mapper = _mapper;

        }

        [HttpGet]
        [Route("getall")]
        public IActionResult GetMessages()
        {
            var result = messageRepo.GetWhereWithInclude(x => x.Active == true, "CreatedBy").OrderByDescending(x => x.CreatedDate);
            return Ok(mapper.Map(result, new List<MessageDto>()));
        }


        [HttpPost]
        [Route("createmessage")]
        public async Task<IActionResult> CreateMessage([FromBody] MessageDto dto)
        {
            dto.CreatedDate = DateTime.Now;
            dto.CreatedBy = null;//just for now as to not create new users every time
            dto.CreatedById = 1; //also placeholder for now
            dto.Active = true;
            var result = await messageRepo.AddAsync(mapper.Map(dto, new Message()));
            dto.Id = result;
            return Ok(dto);
        }
    }
}
