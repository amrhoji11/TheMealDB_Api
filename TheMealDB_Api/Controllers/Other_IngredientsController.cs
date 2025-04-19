using Landing.PL.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheMealDB_Api.Dtos;
using TheMealDb_Core.Dtos.Area;
using TheMealDb_Core.Dtos.Other_Ingredient;
using TheMealDb_Core.Interface;
using TheMealDb_Infrastructure.Data;
using TheMealDb_Infrastructure.Repository;

namespace TheMealDB_Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class Other_IngredientsController : ControllerBase
    {
        private readonly IOther_IngredientRepository other_IngredientRepository;
        private readonly AppDbContext appDbContext;

        public Other_IngredientsController(IOther_IngredientRepository other_IngredientRepository, AppDbContext appDbContext)
        {
            this.other_IngredientRepository = other_IngredientRepository;
            this.appDbContext = appDbContext;
        }
        [HttpPost("AddOther_Ingredient")]
        public async Task<IActionResult> AddOther_Ingredient(AddFromOtherIngredient dto)
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
            var ImageName = FileSetting.UploadFile(dto.Image, "Images");
            var dtoOther_Ingred = new AddOtherIngredient
            {
                Name = dto.Name,
                ImageName = ImageName,
                Unit_Id = dto.Unit_Id
            };
            var result = await other_IngredientRepository.AddOther_Ingredient(dtoOther_Ingred);
            if (result != "Successfuly")
            {
                return BadRequest("you have wrong!");
            }
            return Ok(result);


        }

        [HttpPut("UpdateOther_Ingredient")]
        public async Task<IActionResult> UpdateOther_Ingredient(Update_Other_Ingredient dto)
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

            var other = await appDbContext.Other_Ingredients.FindAsync(dto.Id);


            if (dto.Image != null && other.ImageName != null)
            {
                FileSetting.DeleteFile(other.ImageName, "Images");
            }
            var imageName = FileSetting.UploadFile(dto.Image, "Images");

            var OtherDto = new AddOtherIngredient
            {
                Name = dto.Name,
                ImageName = imageName,
                Unit_Id = dto.Unit_Id
            };

            var result = await other_IngredientRepository.UpdateOther_Ingredient(dto.Id, OtherDto);

            if (result != "The Update was completed successfully")
            {
                return NotFound("Other_Ingredient Not found");
            }

            return Ok(result);
        }

        [HttpDelete("DeleteOther_Ingredient")]
        public async Task<IActionResult> DeleteOther_Ingredient(int id)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("User is Unauthorized");

            }

            var result = await other_IngredientRepository.DeleteOther_Ingredient(id);
            return Ok(result);

        }



    }
}
