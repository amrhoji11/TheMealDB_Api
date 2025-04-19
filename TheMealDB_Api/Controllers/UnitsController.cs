using Landing.PL.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheMealDB_Api.Dtos;
using TheMealDb_Core.Dtos.Area;
using TheMealDb_Core.Dtos.Unit;
using TheMealDb_Core.Interface;
using TheMealDb_Infrastructure.Data;
using TheMealDb_Infrastructure.Repository;

namespace TheMealDB_Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UnitsController : ControllerBase
    {
        private readonly IUnitRepository unitRepository;

        public UnitsController(IUnitRepository unitRepository )
        {
            this.unitRepository = unitRepository;
        }

        [HttpPost("AddUnit")]
        public async Task<IActionResult> AddUnit(AddUnitdto dto)
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
           
            
            var result = await unitRepository.AddUnit(dto);
            if (result != "Successfuly")
            {
                return BadRequest("you have wrong!");
            }
            return Ok(result);

        }

        [HttpPut("UpdateUnit")]
        public async Task<IActionResult> UpdateUnit(UpdateUnitDto dto)
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

    
            var result = await unitRepository.UpdateUnit(dto);

            if (result != "The Update was completed successfully")
            {
                return NotFound("Area Not found");
            }

            return Ok(result);
        }

        [HttpDelete("DeleteUnit")]
        public async Task<IActionResult> Deleteunit(int id)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("User is Unauthorized");

            }

            var result = await unitRepository.DeleteUnit(id);
            return Ok(result);

        }
    }
}
