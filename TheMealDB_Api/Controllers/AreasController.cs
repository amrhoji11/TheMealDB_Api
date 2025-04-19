using Landing.PL.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TheMealDB_Api.Dtos;
using TheMealDb_Core.Dtos.Area;
using TheMealDb_Core.Interface;
using TheMealDb_Infrastructure.Data;

namespace TheMealDB_Api.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AreasController : ControllerBase
    {
        private readonly IAreaRepository areaRepository;
        private readonly AppDbContext appDbContext;

        public AreasController(IAreaRepository areaRepository, AppDbContext appDbContext)
        {
            this.areaRepository = areaRepository;
            this.appDbContext = appDbContext;
        }

        [HttpPost("AddArea")]
        public async Task<IActionResult> AddArea(AreaFromDto dto)
        {

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token)) 
            {
                return Unauthorized("User is Unauthorized");

            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ImageName = FileSetting.UploadFile(dto.Image,"Images");
            var dtoArea = new AreaDto
            {
                Name = dto.Name,
                ImageName = ImageName
            };
          var result=  await areaRepository.AddArea(dtoArea);
            if (result!= "Successfuly")
            {
                return BadRequest("you have wrong!");
            }
            return Ok(result);
           
        }

        [HttpPut("UpdateArea")]
        public async Task<IActionResult> UpdateArea(UpdateAreaDto dto)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("User is Unauthorized");

            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            var area = await appDbContext.Areas.FindAsync(dto.Id);
           
           
            if (dto.Image !=null && area.ImageName!= null)
            {
                FileSetting.DeleteFile(area.ImageName,"Images");
            }
            var imageName = FileSetting.UploadFile(dto.Image,"Images");

            var AreaDto = new AreaDto
            {
                Name = dto.Name,
                ImageName = imageName
            };

            var result = await areaRepository.UpdateArea(dto.Id,AreaDto);

            if (result != "The Update was completed successfully")
            {
                return NotFound("Area Not found");
            }

            return Ok(result);
        }

        [HttpDelete("DeleteArea")]
        public async Task<IActionResult> DeleteArea(int id)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("User is Unauthorized");

            }
            var result = await areaRepository.DeleteArea(id);
            return Ok(result);

        }
    }
}
