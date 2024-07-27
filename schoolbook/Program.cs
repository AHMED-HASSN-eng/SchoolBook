using Microsoft.AspNetCore.Authentication.Cookies;
using schoolbook.Clients;

namespace schoolbook
{
    public class Program
    {
        public static void Main(string[] args)
        {
           
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession();
            builder.Services.AddAuthentication(
                CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(option => {
                    option.LoginPath = "/signin/signin";
                    option.ExpireTimeSpan = TimeSpan.FromMinutes(60);

                });

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(60);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });


            builder.Services.AddSingleton(x =>
                 new PaypalClient(
                     builder.Configuration["PayPalOptions:ClientId"],
                     builder.Configuration["PayPalOptions:ClientSecret"],
                     builder.Configuration["PayPalOptions:Mode"]
                 )
             );
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseSession();
          ;
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Homepage}/{action=index}/{id?}");

            app.Run();
        }
    }
}