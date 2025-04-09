using LinkDev.IKEA.BLL;
using LinkDev.IKEA.DAL;
using LinkDev.IKEA.DAL.Contracts;
using LinkDev.IKEA.DAL.Persistence.Data;
using LinkDev.IKEA.DAL.Persistence.Data.DbInitializer;
using LinkDev.IKEA.PL.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.IKEA.PL
{
    public class Program
    {
        // Entry Point For Applecation
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services
            // Add services to the container.
            builder.Services.AddControllersWithViews(options=>new AutoValidateAntiforgeryTokenAttribute());
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddApplicationServices();

            #endregion

            var app = builder.Build();

            #region DataBase Initialization
            app.InitializeDataBase(); 
            #endregion

            #region  Configure  HTTP Request Pipeline
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            else
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            #endregion

            app.Run();
        }
    }
}
