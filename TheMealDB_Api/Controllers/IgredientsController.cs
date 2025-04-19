using Landing.PL.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheMealDB_Api.Dtos;
using TheMealDb_Core.Dtos.Area;
using TheMealDb_Core.Dtos.Ingredient;
using TheMealDb_Core.Interface;
using TheMealDb_Infrastructure.Data;
using TheMealDb_Infrastructure.Repository;

namespace TheMealDB_Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class IgredientsController : ControllerBase
    {
        private readonly IIngredientRepository ingredientRepository;
        private readonly AppDbContext appDbContext;

        public IgredientsController(IIngredientRepository ingredientRepository, AppDbContext appDbContext)
        {
            this.ingredientRepository = ingredientRepository;
            this.appDbContext = appDbContext;
        }

        [HttpPost("AddIngredient")]
        public async Task<IActionResult> AddIngredient(AddFromIgredient dto)
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
            var dtoingredient = new AddIngredient
            {
                Name = dto.Name,
                ImageName = ImageName
            };
            var result = await ingredientRepository.AddIngredient(dtoingredient);
            if (result != "Successfuly")
            {
                return BadRequest("you have wrong!");
            }
            return Ok(result);

        }

        [HttpPut("UpdateIngredient")]
        public async Task<IActionResult> UpdateIngredient(AddFromIgredient dto)
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

            var ingredient = await appDbContext.Ingredients.FindAsync(dto.Id);


            if (dto.Image != null && ingredient.ImageName != null)
            {
                FileSetting.DeleteFile(ingredient.ImageName, "Images");
            }
            var imageName = FileSetting.UploadFile(dto.Image, "Images");

            var IngredientDto = new AddIngredient
            {
                Name = dto.Name,
                ImageName = imageName
            };

            var result = await ingredientRepository.UpdateIngredient(dto.Id, IngredientDto);

            if (result != "The Update was completed successfully")
            {
                return NotFound("Ingredient Not found");
            }

            return Ok(result);
        }

        [HttpDelete("DeleteIngredient")]
        public async Task<IActionResult> DeleteIngredient(int id)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("User is Unauthorized");

            }

            var result = await ingredientRepository.DeleteIngredient(id);
            return Ok(result);

        }
    }
}
