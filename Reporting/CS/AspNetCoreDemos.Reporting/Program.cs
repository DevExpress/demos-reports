using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace AspNetCoreDemos.Reporting {
    public class Program {
        public static void Main(string[] args) {
            var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .Build();

            var host = CreateWebHostBuilder(args)
                .UseConfiguration(config)
                .Build();

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
