using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace testapi.Common
{
	public class InventoryContext : ConfigureContext
	{
		public InventoryContext(DbContextOptions<InventoryContext> options) : base(options)
		{

		}

		public InventoryContext()
		{

		}
		public DbSet<Product> Products { get; set; }
	}

	public class ConfigureContext : DbContext
	{
		public ConfigureContext(DbContextOptions<ConfigureContext> options)
			   : base(options)
		{ }
		public ConfigureContext(DbContextOptions<InventoryContext> options)
			   : base(options)
		{ }

		public ConfigureContext() : base()
		{
			Database.SetCommandTimeout(600);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			//directory path
			var directoryPath = AppDomain.CurrentDomain.BaseDirectory.Split(new String[] { @"bin\" }, StringSplitOptions.None)[0];

			//find appsettings.json file
			IConfigurationRoot configuration = new ConfigurationBuilder()
			.SetBasePath(directoryPath)
			.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
			.Build();

			//get db connection value from appsettings file
			optionsBuilder.UseSqlServer(configuration.GetConnectionString("dbConnectionStr"));
		}
	}
}
