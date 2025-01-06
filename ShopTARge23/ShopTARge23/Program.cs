using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using ShopTARge23.ApplicationServices.Services;
using ShopTARge23.Core.ServiceInterface;
using ShopTARge23.Data;
using ShopTARge23.Core.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using ShopTARge23.Hubs;


namespace ShopTARge23
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddSignalR();

            builder.Services.AddScoped<ISpaceshipsServices, SpaceshipsServices>();
            builder.Services.AddScoped<IFileServices, FileServices>();
            builder.Services.AddScoped<IRealEstateServices, RealEstateServices>();
            builder.Services.AddScoped<IKindergartenServices, KindergartenServices>();
            builder.Services.AddScoped<IChuckNorrisServices, ChuckNorrisServices>();
            builder.Services.AddScoped<IFreeToGamesServices, FreeToGamesServices>();
            builder.Services.AddScoped<IWeatherForecastServices, WeatherForecastServices>();
            builder.Services.AddScoped<IEmailServices, EmailServices>();
            builder.Services.AddScoped<IOpenWeatherMapServices, OpenWeatherMapServices>();

            var services = builder.Services;
            var configuration = builder.Configuration;

            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = configuration["Authentication:Facebook:AppSecret"];
            });

            builder.Services.AddDbContext<ShopTARge23Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequiredLength = 3;
                options.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";
                options.Lockout.MaxFailedAccessAttempts = 2;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            })
            .AddEntityFrameworkStores<ShopTARge23Context>()
            .AddDefaultTokenProviders()
            .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>("CustomEmailConfirmation")
            .AddDefaultUI();

            builder.Services.AddAuthentication()
                //.AddFacebook(options =>
                //{
                //    options.AppId = "";
                //    options.AppSecret = "";
                //})
                .AddGoogle(options =>
                {
                    options.ClientId = "494818291692-roe5lj5l4m3rnmkq6ltgn29ojs5ub96l.apps.googleusercontent.com";
                    options.ClientSecret = "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";
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider
                (Path.Combine(builder.Environment.ContentRootPath, "multipleFileUpload")),
                RequestPath = "/multipleFileUpload"
            });

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapHub<ChatHub>("/chatHub");

            app.Run();
        }
    }
}
