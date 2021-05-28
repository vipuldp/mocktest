using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using testapi.Common;
using testapi.Service;

namespace testapi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddTransient<IProductService, ProductService>();
			services.AddControllers().AddNewtonsoftJson();

			var cnnStr = Configuration.GetConnectionString("dbConnectionStr");
			services.AddDbContext<InventoryContext>(builder => builder.UseSqlServer(cnnStr));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			try
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
			}
			catch (Exception ex)
			{
				ErrorMessage.WriteExceptionLog(ex);
			}
		}
	}
}
