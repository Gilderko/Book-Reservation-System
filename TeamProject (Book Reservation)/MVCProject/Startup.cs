using Autofac;
using BL.Config;
using BL.Facades;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVCProject.StateManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProject
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
            services.AddControllersWithViews();

            services.AddTransient<AuthorFacade>(services => StateKeeper.Instance.GetNewScope().Resolve<AuthorFacade>());
            services.AddTransient<BookCollectionFacade>(services => StateKeeper.Instance.GetNewScope().Resolve<BookCollectionFacade>());
            services.AddTransient<BookFacade>(services => StateKeeper.Instance.GetNewScope().Resolve<BookFacade>());
            services.AddTransient<BookInstanceFacade>(services => StateKeeper.Instance.GetNewScope().Resolve<BookInstanceFacade>());
            services.AddTransient<EBookFacade>(services => StateKeeper.Instance.GetNewScope().Resolve<EBookFacade>());
            services.AddTransient<EBookPreviewFacade>(services => StateKeeper.Instance.GetNewScope().Resolve<EBookPreviewFacade>());
            services.AddTransient<EReaderFacade>(services => StateKeeper.Instance.GetNewScope().Resolve<EReaderFacade>());
            services.AddTransient<EReaderFacade>(services => StateKeeper.Instance.GetNewScope().Resolve<EReaderFacade>());
            services.AddTransient<EReaderInstanceFacade>(services => StateKeeper.Instance.GetNewScope().Resolve<EReaderInstanceFacade>());
            services.AddTransient<EReaderInstancePreviewFacade>(services => StateKeeper.Instance.GetNewScope().Resolve<EReaderInstancePreviewFacade>());
            services.AddTransient<ReservationFacade>(services => StateKeeper.Instance.GetNewScope().Resolve<ReservationFacade>());
            services.AddTransient<ReviewFacade>(services => StateKeeper.Instance.GetNewScope().Resolve<ReviewFacade>());
            services.AddTransient<UserFacade>(services => StateKeeper.Instance.GetNewScope().Resolve<UserFacade>());

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(5);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
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

            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();
            app.UseCookiePolicy();
            app.UseAuthentication();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
