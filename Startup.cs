using supra.Context;
using Microsoft.EntityFrameworkCore;
using supra.Repositories;
using supra.Repositories.Interface;
using Microsoft.AspNetCore.Builder;

namespace supra;
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

        services.AddDbContext<AppDbContext>( options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection")
            )
        );

        services.AddTransient<ICategoriaRepository, CategoriaRepository>();

        services.AddTransient<IFornecedorRepository, FornecedorRepository>();

        services.AddTransient<IFuncionarioRepository, FuncionarioRepository>();

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddControllersWithViews();
        services.AddMemoryCache();
        services.AddSession();
    

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
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
        app.UseStaticFiles();

        app.UseRouting();

        app.UseSession();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {

            endpoints.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Supervisor}/{action=Index}/{id?}"
            );

            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}