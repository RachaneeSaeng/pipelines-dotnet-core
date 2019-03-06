using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.IO;
using Microsoft.EntityFrameworkCore;
using dotnetcoresample.Customers.Queries.GetCustomerDetail;
using MediatR;
using StackExchange.Redis;
using EventServiceBus;
using Microsoft.Azure.ServiceBus;
using EventServiceBus.Abstractions;
using dotnetcoresample.IntegrationEvents.EventHandling;
using Autofac;
using dotnetcoresample.IntegrationEvents.Events;

namespace dotnetcoresample
{
    public class Startup
    {
        //public Startup(IHostingEnvironment env)
        //{
        //    string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        //    string appSettingsFilePath = Path.Combine(assemblyFolder,"appsettings.json");

        //    var builder = new ConfigurationBuilder()
        //        .SetBasePath(env.ContentRootPath)
        //        .AddJsonFile(appSettingsFilePath, optional: false, reloadOnChange: true)
        //        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
        //        .AddEnvironmentVariables();
        //    Configuration = builder.Build();
        //}

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var x = typeof(GetCustomerDetailQueryHandler).GetTypeInfo().Assembly;
            services.AddMediatR(typeof(GetCustomerDetailQueryHandler).GetTypeInfo().Assembly);

            // Add DbContext using SQL Server Provider
            services.AddDbContext<DotNetSampleDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MyFirstAzureDatabase")));

            // Add framework services.
            services.AddMvc();

            //By connecting here we are making sure that our service
            //cannot start until redis is ready. This might slow down startup,
            //but given that there is a delay on resolving the ip address
            //and then creating the connection it seems reasonable to move
            //that cost to startup instead of having the first request pay the
            //penalty.
            services.AddSingleton<ConnectionMultiplexer>(sp =>
            {
                var configuration = ConfigurationOptions.Parse(Configuration.GetConnectionString("RedisDb"), true);
                configuration.ResolveDns = true;

                return ConnectionMultiplexer.Connect(configuration);
            });

            services.AddSingleton<IServiceBusPersisterConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultServiceBusPersisterConnection>>();

                var serviceBusConnectionString = Configuration["EventBusConnection"];
                var serviceBusConnection = new ServiceBusConnectionStringBuilder(serviceBusConnectionString);

                return new DefaultServiceBusPersisterConnection(serviceBusConnection, logger);
            });

            RegisterEventBus(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseSwaggerUi3(settings =>
            {
                settings.Path = "/api";
                settings.DocumentPath = "/api/specification.json";
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            ConfigureEventBus(app);
        }

        private void RegisterEventBus(IServiceCollection services)
        {
            var subscriptionClientName = Configuration["SubscriptionClientName"];

            services.AddSingleton<IEventBus, EventBusServiceBus>(sp =>
            {
                var serviceBusPersisterConnection = sp.GetRequiredService<IServiceBusPersisterConnection>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var logger = sp.GetRequiredService<ILogger<EventBusServiceBus>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                return new EventBusServiceBus(serviceBusPersisterConnection, logger,
                    eventBusSubcriptionsManager, subscriptionClientName, iLifetimeScope);
            });

        services.AddSingleton<IEventBusSubscriptionsManager, EventBusSubscriptionsManager>();

        services.AddTransient<CacheValueChangedIntegrationEventHandler>();
        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
            //var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            //eventBus.Subscribe<CacheValueChangedIntegrationEvent, CacheValueChangedIntegrationEventHandler>();
        }
    }
}
