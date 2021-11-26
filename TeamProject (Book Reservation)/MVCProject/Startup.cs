using Autofac;
using BL.Config;
using BL.Facades;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

            services.AddSingleton<IContainer>(services => AutofacBLConfig.Configure());

            services.AddTransient<AuthorFacade>(services => services.GetService<IContainer>().Resolve<AuthorFacade>());
            services.AddTransient<BookCollectionFacade>(services => services.GetService<IContainer>().Resolve<BookCollectionFacade>());
            services.AddTransient<BookCollectionPreviewsFacade>(services => services.GetService<IContainer>().Resolve<BookCollectionPreviewsFacade>());
            services.AddTransient<BookFacade>(services => services.GetService<IContainer>().Resolve<BookFacade>());
            services.AddTransient<BookInstanceFacade>(services => services.GetService<IContainer>().Resolve<BookInstanceFacade>());
            services.AddTransient<EBookFacade>(services => services.GetService<IContainer>().Resolve<EBookFacade>());
            services.AddTransient<EBookPreviewFacade>(services => services.GetService<IContainer>().Resolve<EBookPreviewFacade>());
            services.AddTransient<EReaderFacade>(services => services.GetService<IContainer>().Resolve<EReaderFacade>());
            services.AddTransient<EReaderFacade>(services => services.GetService<IContainer>().Resolve<EReaderFacade>());
            services.AddTransient<EReaderInstanceFacade>(services => services.GetService<IContainer>().Resolve<EReaderInstanceFacade>());
            services.AddTransient<EReaderInstancePreviewFacade>(services => services.GetService<IContainer>().Resolve<EReaderInstancePreviewFacade>());
            services.AddTransient<ReservationFacade>(services => services.GetService<IContainer>().Resolve<ReservationFacade>());
            services.AddTransient<ReviewFacade>(services => services.GetService<IContainer>().Resolve<ReviewFacade>());
            services.AddTransient<UserFacade>(services => services.GetService<IContainer>().Resolve<UserFacade>());

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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
