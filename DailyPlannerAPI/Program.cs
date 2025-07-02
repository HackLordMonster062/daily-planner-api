
using DailyPlannerAPI.Services;

namespace DailyPlannerAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
			var builder = WebApplication.CreateBuilder(args);

			// Add services
			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();  
			builder.Services.AddSwaggerGen();

			builder.Services.AddSingleton<ProjectManager>();
			builder.Services.AddSingleton<IStorageService, JsonFileStorage>();

			var corsPolicyName = "_allowCORS";

			builder.Services.AddCors(options =>
			{
				options.AddPolicy(name: corsPolicyName,
					policy => {
						policy
							.WithOrigins("http://localhost:5173") // React dev server
							.AllowAnyHeader()
							.AllowAnyMethod();
					});
			});

			var app = builder.Build();

			if (app.Environment.IsDevelopment()) {
				app.UseSwagger();                         
				app.UseSwaggerUI();                       
			}

			app.UseHttpsRedirection();
			app.UseAuthorization();
			app.MapControllers();

			app.UseCors(corsPolicyName);

			app.Run();
		}
	}
}
