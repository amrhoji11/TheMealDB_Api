
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheMealDB_Api.Error;
using TheMealDb_Core.Interface;
using TheMealDb_Core.Model;
using TheMealDb_Infrastructure.Data;
using TheMealDb_Infrastructure.Repository;

namespace TheMealDB_Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 1;
            })
               .AddRoles<IdentityRole<int>>()
               .AddDefaultTokenProviders()
               .AddEntityFrameworkStores<AppDbContext>();
            builder.Services.AddScoped<IAuthRepository, AuthRepository>();
            builder.Services.AddScoped<IMealRepository, MealRepository>();
            builder.Services.AddScoped<IAreaRepository, AreaRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IUnitRepository, UnitRepository>();
            builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
            builder.Services.AddScoped<IOther_IngredientRepository, Other_IngredientRepository>();
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();



            builder.Services.AddCors(option =>
            {
                option.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
                });
            });







            var app = builder.Build();

           

           

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseCors("AllowAll");

            app.UseAuthorization();


            app.MapControllers();
            app.UseExceptionHandler(op => { });

            app.Run();
        }
    }
}
