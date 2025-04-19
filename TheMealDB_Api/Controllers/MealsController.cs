using Landing.PL.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheMealDB_Api.Dtos;
using TheMealDb_Core.Dtos.Meal;
using TheMealDb_Core.Interface;
using TheMealDb_Infrastructure.Data;
using TheMealDb_Infrastructure.Repository;

namespace TheMealDB_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealsController : ControllerBase
    {
        private readonly IMealRepository mealRepository;
        private readonly AppDbContext appDbContext;

        public MealsController(IMealRepository mealRepository,AppDbContext appDbContext)
        {
            this.mealRepository = mealRepository;
            this.appDbContext = appDbContext;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("Add_Meals")]
        public async Task<IActionResult> AddMeals(MealFromDto dto)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("User is Unauthorized");

            }

            if (! ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var imageName = FileSetting.UploadFile(dto.Image,"Images");

            var Dto = new MealDto
            {
                Name = dto.Name,
                Description = dto.Description,
                ImageName = imageName,
                Preparation_Method = dto.Preparation_Method,
                Category_Id = dto.Category_Id,
                Area_Id = dto.Area_Id
                

            };
            

            var result = await mealRepository.AddMeals(Dto);
            if (result != "The Meal is Added")
            {
                return BadRequest("you have a mistake!!");
            }
            return Ok(result);

        }
        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateMeal")]
        public async Task<IActionResult> UpdateMeal(MealFromDto dto)
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

            var meal = await appDbContext.Meals.FindAsync(dto.Id);


            if (dto.Image != null && meal.ImageName != null)
            {
                FileSetting.DeleteFile(meal.ImageName, "Images");
            }
            var imageName = FileSetting.UploadFile(dto.Image, "Images");

            var MealDto = new MealDto
            {
                Name = dto.Name,
                Description= dto.Description,
                Preparation_Method= dto.Preparation_Method,
                Category_Id = dto.Category_Id,
                Area_Id = dto.Area_Id,
                ImageName = imageName
            };

            var result = await mealRepository.UpdateMeal(dto.Id, MealDto);

            if (result != "The Update was completed successfully")
            {
                return NotFound("Meal Not found");
            }

            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteMeal")]
        public async Task<IActionResult> DeleteMeal(int id)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("User is Unauthorized");

            }

            var result = await mealRepository.DeleteMeal(id);
            return Ok(result);

        }

        [HttpGet("AllMealByLatter")]
        public async Task<IActionResult> AllMealByLatter(string a)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            var result = await mealRepository.AllMealByLatters(a);
            return Ok(result);

        }

        [HttpGet("AllMealRandom")]
        public async Task<IActionResult> AllMealRandom(int a)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            if (a<=0)
            {
                return BadRequest("must number be more than 0 ");
            }

            var result = await mealRepository.RandomMeals(a);
            return Ok(result);

        }

        [HttpGet("AllMealsByIngredient")]

        public async Task<IActionResult> AllMealsByIngredient(string a)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            var result = await mealRepository.MealsByIngredient(a);
            return Ok(result);

        }

        [HttpGet("AllMealsByCategory")]

        public async Task<IActionResult> AllMealsByCategory(string a)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            var result = await mealRepository.MealsByCategory(a);
            return Ok(result);

        }

        [HttpGet("MealsByAllpossibilities")]

        public async Task<IActionResult> MealsByAllpossibilities(int pageIndex , int pageSize)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            var result = await mealRepository.MealsByAllpossibilities(pageIndex , pageSize);
            return Ok(result);

        }
    }
}
