


namespace WebSite
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    
    [Mobilize.WebMap.Common.Attributes.ExcludeObservableWrapperTracking]
    public partial class Startup
    {
        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        private static IConfiguration config { get; set; }

        /// <summary>
        /// Entry point of windows form application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Start(string[] args)
        {
            Mobilize.Web.Application.CurrentApplication.ControlsComponentized.Add("Mobilize.Web.TabPage");
            SKS.modMain.Main();
        }

        /// <summary>
        /// Entry Point of the web Application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        /// <summary>
        /// Returns a new WebHost
        /// </summary>
        /// <param name="args">run arguments</param>
        /// <returns>a new WebHost</returns>
        public static IWebHost BuildWebHost(string[] args){
            config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                .Build();

            return new WebHostBuilder()
                .UseContentRoot(System.IO.Directory.GetCurrentDirectory())
                .UseKestrel()
                //.UseIISIntegration()
                .UseConfiguration(config)
                .UseStartup<Startup>()
                .ConfigureKestrel((context, options) =>
                {
                    // Set properties and call methods on options
                })
                .Build();
        }
    }
}

