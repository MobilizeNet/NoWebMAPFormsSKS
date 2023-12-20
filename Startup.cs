// <copyright file="Startup.cs" company="Mobilize.Net">
//        Copyright (C) Mobilize.Net info@mobilize.net - All Rights Reserved
//
//        This file is part of the Mobilize Frameworks, which is
//        proprietary and confidential.
//
//        NOTICE:  All information contained herein is, and remains
//        the property of Mobilize.Net Corporation.
//        The intellectual and technical concepts contained herein are
//        proprietary to Mobilize.Net Corporation and may be covered
//        by U.S. Patents, and are protected by trade secret or copyright law.
//        Dissemination of this information or reproduction of this material
//        is strictly forbidden unless prior written permission is obtained
//        from Mobilize.Net Corporation.
// </copyright>

namespace WebSite
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.Extensions.DependencyInjection;
    using Mobilize.Web;
    using Mobilize.WebMap.Common.Core;
    using Mobilize.WebMap.Common.DCP;
    using Mobilize.WebMap.Host;
    using Mobilize.WebMap.Server;
    using Mobilize.WebMap.Server.ObservableBinder;
    using Newtonsoft.Json.Serialization;
    using SKS;

    /// <summary>
    /// Startup
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        /// </summary>
        /// <param name="services">the collection of services</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(p => config);
            services.AddWebMap(false);
            services.RegisterModelMappers();
            services.RegisterWrappers();
            AddDesktopCompatibilityPlatform(services, Startup.Start);
            services.AddHttpContextAccessor();
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddAntiforgery(options => options.HeaderName = WebMapHeaders.AntiforgeryToken);
            services.AddMvc(options =>
            {
                options.ModelBinderProviders.Insert(0, new ObservableModelBinderProvider());
                options.ModelMetadataDetailsProviders.Insert(0, new SuppressChildValidationMetadataProvider(typeof(IObservable)));
            }).AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver())
;
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSignalR();
            // Sorting commands service.
            services.AddSingleton<Mobilize.WebMap.Common.Server.ISortingActionService>(new Mobilize.Web.Services.SortingCommandService());
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        /// </summary>
        /// <param name="app">the application builder</param>
        /// <param name="env">the hosting environment</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseSession();
            app.UseAntiforgeryToken();
            app.UseWebMap();
            app.UseMvc(routes =>
            {
                routes.MapRoute("DefaultApi", "api/{controller}/{id}");
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSignalR(routes =>
            {
                routes.MapHub<SignalHub>("/bgw");
            });
        }

        private static void AddDesktopCompatibilityPlatform(IServiceCollection services, EntryPoint entryPoint)
        {
            services.AddScoped<ICommandFactory, CommandFactory>();
            services.AddScoped<IApplication>((provider) => new ExtApplication(provider) { EntryPoint = entryPoint, AppName = "SKS" });
            services.AddTransient<IBackgroundWorkerManager, BackgroundWorkerManager>();
        }
    }
}

