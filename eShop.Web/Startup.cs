using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using eShop.Web.Data;
using eShop.UseCases.SearchProductScreen;
using eShop.UseCases.PluginInterfaces.DataStore;
using eShop.ShoppingCart.LocalStorage;
// using eShop.DataStore.HardCoded;
using eShop.DataStore.SQL.Dapper;
using eShop.UseCases.ViewProductScreen;
using eShop.UseCases.PluginInterfaces.UI;
using eShop.UseCases.ShoppingCartScreen;
using eShop.UseCases.PluginInterfaces.StateStore;
using eShop.StateStore.LocalStorage;
using eShop.CoreBusiness.Services;
using eShop.UseCases.OrderConfirmationScreen;
using eShop.UseCases.OutstandingOrderScreen;
using eShop.UseCases.ProcessedOrderScreen;
using eShop.UseCases.ProcessOrderScreen;
using eShop.Web.Common.JsInterOp;

namespace eShop.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Configuration.GetConnectionString

            services.AddControllers();
            services.AddAuthentication("eShop.CookieAuth")
                .AddCookie("eShop.CookieAuth", config =>
                {
                    config.Cookie.Name = "eShop.CookieAuth";
                    config.LoginPath = "/authenticate";
                });

            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddTransient<JsNavigator>();
            
            //services.AddSingleton<IProductRepository, ProductRepository>();
            //services.AddSingleton<IOrderRepository, OrderRepository>();
            services.AddScoped<IShoppingCartStateStore, ShoppingCartStateStore>();
            
            services.AddTransient<IOrderService, OrderService>();

            services.AddTransient<IDataAccess>(sp => new DataAccess(Configuration.GetConnectionString("Default")));
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();

            services.AddTransient<IShoppingCart, eShop.ShoppingCart.LocalStorage.ShoppingCart>();            
            services.AddTransient<ISearchProductUseCase, SearchProductUseCase>();
            services.AddTransient<IViewProductUseCase, ViewProductUseCase>();
            services.AddTransient<IAddProductToCartUseCase, AddProductToCartUseCase>();
            services.AddTransient<IDeleteProductUseCase, DeleteProductUseCase>();
            services.AddTransient<IUpdateQuantityUseCase, UpdateQuantityUseCase>();
            services.AddTransient<IViewShoppingCartUseCase, ViewShoppingCartUseCase>();
            services.AddTransient<IPlaceOrderUseCase, PlaceOrderUseCase>();
            services.AddTransient<IViewOrderConfirmationUseCase, ViewOrderConfirmationUseCase>();

            services.AddTransient<IViewOutstandingOrderUseCase, ViewOutstandingOrderUseCase>();
            services.AddTransient<IViewProcessedOrdersUseCase, ViewProcessedOrdersUseCase>();
            services.AddTransient<IViewOrderDetailUseCase, ViewOrderDetailUseCase>();
            services.AddTransient<IProcessOrderUseCase, ProcessOrderUseCase>();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
