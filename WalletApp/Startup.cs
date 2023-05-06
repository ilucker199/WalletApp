using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;
using WalletApp.Services;

namespace WalletApp
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers()
				.AddJsonOptions(options =>
				{
					options.JsonSerializerOptions.PropertyNamingPolicy = null;
					options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
				});

			// Configure PostgreSQL connection
			var connectionString = Configuration.GetConnectionString("DefaultConnection");
			services.AddDbContext<WalletAppContext>(options => options.UseNpgsql(connectionString));

			services.AddScoped<IWalletService, WalletService>();

			// Register Swagger generator
			services.AddSwaggerGen();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			// Use Swagger middleware
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "WalletApp V1");
			});

			// Apply database migrations
			using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
			{
				var context = serviceScope.ServiceProvider.GetRequiredService<WalletAppContext>();
				context.Database.Migrate();
			}
		}
	}
}
