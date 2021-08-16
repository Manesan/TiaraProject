using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using tiara_api.DTOs;
using tiara_api.Models;
using tiara_api.Repository.IRepository;

namespace tiara_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MilestoneController : ControllerBase
    {
        private readonly IGenCRUDRepository<Milestone> milestoneRepo;
        private readonly IMapper mapper;

        public MilestoneController(
           IGenCRUDRepository<Milestone> _milestoneRepo,
           IMapper _mapper
            )

        {
            milestoneRepo = _milestoneRepo;
            mapper = _mapper;

        }


        [HttpGet]
        [Route("getall")]
        public IActionResult GetMilestones()
        {
            var result = milestoneRepo.GetAll();
            return Ok(mapper.Map(result, new List<MileStoneDto>()));
        }

        [HttpGet]
        [Route("createmilestone/{description}")]
        public async Task<IActionResult> CreateMilestone(string description)
        {
            //fix this in automapper so dont have to always specify
            MileStoneDto dto = new MileStoneDto();
            dto.Description = description;
            dto.Achieved = false;
            dto.CreatedById = 1;
            dto.Active = true;
            dto.Deleted = false;
            dto.CreatedDate = DateTime.Now;

            await milestoneRepo.AddAsync(mapper.Map(dto, new Milestone()));
            return Ok();
        }

        [HttpPost]
        [Route("updatemilestonecontainers")]
        public async Task<IActionResult> UpdateMilestoneStatus([FromBody] MilestoneContainerDto container)
        {            
            foreach(var item in container.todo)
            {
                await milestoneRepo.UpdateAsync(mapper.Map(item, new Milestone()));
            }

            foreach (var item in container.done)
            {
                await milestoneRepo.UpdateAsync(mapper.Map(item, new Milestone()));
            }

            return Ok();
        }
    }
}
