using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Text;

namespace ImdbServerCore
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
			services.Configure<ImdbDatabaseSettings>(Configuration.GetSection(nameof(ImdbDatabaseSettings)));
			services.AddSingleton<IImdbDatabaseSettings>(sp => sp.GetRequiredService<IOptions<ImdbDatabaseSettings>>().Value);
			//services.AddSingleton<MongoUsersManager>();
			//services.AddControllers().AddNewtonsoftJson(options => options.UseMemberCasing());

			services.AddCors(options =>
			{
				options.AddDefaultPolicy(
				builder =>
				{
					builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
				});
			});
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);
			services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = true);

			services.AddDbContext<ImdbFavoritesEntities>(item => item.UseSqlServer(Configuration.GetConnectionString("ImdbFavorites")));


			// configure strongly typed settings objects
			var appSettingsSection = Configuration.GetSection("AppSettings");
			services.Configure<AppSettings>(appSettingsSection);

			// configure jwt authentication
			var appSettings = appSettingsSection.Get<AppSettings>();
			var key = Encoding.ASCII.GetBytes(appSettings.Secret);
			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(x =>
			{
				x.RequireHttpsMetadata = false;
				x.SaveToken = true;
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false
				};
			});

			// configure DI for application services
			services.AddScoped<IImdbRepository, ImdbManager>();

			if (GlobalVariable.logicType == 0)
			{
				services.AddScoped<IUsersRepository, EntityUsersManager>();
				services.AddScoped<IMoviesExtendRepository, EntityMoviesExtendManager>();
				services.AddScoped<IMoviesRepository, EntityMoviesManager>();
			}
			else if (GlobalVariable.logicType == 1)
			{
				services.AddScoped<IUsersRepository, SqlUsersManager>();
				services.AddScoped<IMoviesExtendRepository, SqlMoviesExtendManager>();
				services.AddScoped<IMoviesRepository, SqlMoviesManager>();
			} else if (GlobalVariable.logicType == 2)
			{
				services.AddScoped<IUsersRepository, MySqlUsersManager>();
				services.AddScoped<IMoviesExtendRepository, MySqlMoviesExtendManager>();
				services.AddScoped<IMoviesRepository, MySqlMoviesManager>();
			}
			else if (GlobalVariable.logicType == 3)
			{
				services.AddScoped<IUsersRepository, MongoUsersManager>();
				services.AddScoped<IMoviesExtendRepository, MongoMoviesExtendManager>();
				services.AddScoped<IMoviesRepository, MongoMoviesManager>();
			}
			services.AddRazorPages();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseCors();
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseDefaultFiles(new DefaultFilesOptions { DefaultFileNames = new List<string> { "index.html" } });
			app.UseStaticFiles();

			//Add our new middleware to the pipeline
			app.UseRequestResponseLogging();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
