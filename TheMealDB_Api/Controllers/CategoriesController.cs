using Landing.PL.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheMealDB_Api.Dtos;
using TheMealDb_Core.Dtos.Category;
using TheMealDb_Core.Interface;
using TheMealDb_Infrastructure.Data;
using TheMealDb_Infrastructure.Repository;

namespace TheMealDB_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly AppDbContext appDbContext;

        public CategoriesController(ICategoryRepository categoryRepository ,AppDbContext appDbContext)
        {
            this.categoryRepository = categoryRepository;
            this.appDbContext = appDbContext;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory(AddFromCategory dto)
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
            var imageName = FileSetting.UploadFile(dto.Image,"Images");

            var category = new AddCategory
            {
                Name = dto.Name,
                Description = dto.Description,
                ImageName = imageName

            };
            var result = await categoryRepository.AddCategory(category);
            return Ok(result);

        }
        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(UpdateFromCategory dto)
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

            var category = await appDbContext.Categories.FindAsync(dto.Id);


            if (dto.Image != null && category.ImageName != null)
            {
                FileSetting.DeleteFile(category.ImageName, "Images");
            }
            var imageName = FileSetting.UploadFile(dto.Image, "Images");

            var categoryDto = new AddCategory 
            {
                Name = dto.Name,
                Description = dto.Description,
                ImageName = imageName
            };

            var result = await categoryRepository.UpdateCategory(dto.Id, categoryDto);

            if (result != "The Update was completed successfully")
            {
                return NotFound("Area Not found");
            }

            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("User is Unauthorized");

            }

            var result = await categoryRepository.DeleteCategory(id);
            return Ok(result);

        }

        [HttpGet("AllCategory")]
        public async Task<IActionResult> AllCategory()
        {
            var result = await categoryRepository.AllCategory();
            return Ok(result);
        }
    }
}
