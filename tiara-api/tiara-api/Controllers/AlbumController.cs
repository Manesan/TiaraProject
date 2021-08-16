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
    public class AlbumController : ControllerBase
    {
        private readonly IGenCRUDRepository<Album> albumRepo;
        private readonly IGenCRUDRepository<Photo> photoRepo;
        private readonly IMapper mapper;

        public AlbumController(
           IGenCRUDRepository<Album> _albumRepo,
           IGenCRUDRepository<Photo> _photoRepo,
           IMapper _mapper
            )

        {
            albumRepo = _albumRepo;
            photoRepo = _photoRepo;
            mapper = _mapper;

        }

        [HttpGet]
        [Route("getall")]
        public IActionResult GetAlbums()
        {
            var result = albumRepo.GetAll().OrderByDescending(x => x.CreatedDate);
            return Ok(mapper.Map(result, new List<AlbumDto>()));

            //return Ok();
        }

        [HttpPost]
        [Route("playalbum")]
        public async Task<IActionResult> PlayAlbumOnBooth([FromBody] AlbumDto dto)
        {
            List<Album> recordsToUpdate = new List<Album>();
            try
            {
                List<Album> albums = albumRepo.GetAll().ToList();
                foreach (var album in albums)
                {
                    album.PlayOnBooth = false;
                    recordsToUpdate.Add(album);
                }

                await albumRepo.BulkUpdateAsync(albums);
                var obj = albumRepo.GetWhere(x => x.Id == dto.Id).FirstOrDefault();
                obj.PlayOnBooth = true;
                await albumRepo.UpdateAsync(obj);

                return Ok();
            }
            catch
            {
                return BadRequest("An error occured. Please contact IT support");
            }

        }


        [HttpGet]
        [Route("getboothalbum")]
        public IActionResult GetBoothAlbum()
        {
            try
            {
                var album = albumRepo.GetWhere(x => x.PlayOnBooth == true).FirstOrDefault();
                return Ok(mapper.Map(album, new AlbumDto()));
            }
            catch
            {
                return BadRequest("Unable to fetch booth photo album");
            }
        }


        [HttpPost]
        [Route("create/{title}")]
        public async Task<IActionResult> CreateAlbum(string title)
        {
            try
            {
                //create new album
                AlbumDto newAlbum = new AlbumDto();
                newAlbum.Title = title;
                newAlbum.PlayOnBooth = false;
                newAlbum.CreatedById = 1;
                newAlbum.Active = true;
                newAlbum.Deleted = false;
                newAlbum.CreatedDate = DateTime.Now;
                var albumId = await albumRepo.AddAsync(mapper.Map(newAlbum, new Album()));

                //add photos to album
                var photos = Request.Form.Files;
                foreach(var photo in photos)
                {
                    byte[] fileBytes;
                    string base64;

                    PhotoDto dto = new PhotoDto();
                    if (photo.Length > 0)
                    {
                        // convert to base 64 for db
                        using (var binaryReader = new BinaryReader(photo.OpenReadStream()))
                        {
                            fileBytes = binaryReader.ReadBytes((int)photo.Length);
                            base64 = Convert.ToBase64String(fileBytes);
                            dto.Image = base64;
                            dto.PhotoAlbumId = albumId;
                            dto.CreatedById = 1;
                            dto.Active = true;
                            dto.Deleted = false;
                            dto.CreatedDate = DateTime.Now;
                            await photoRepo.AddAsync(mapper.Map(dto, new Photo()));
                        }

                    }
                }

                return Ok();

            }
            catch
            {
                return BadRequest("Unable to create photo album");
            }
        }
    }
}
