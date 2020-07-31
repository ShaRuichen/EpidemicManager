using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace EpidemicManager
{
    public class Program
    {
        public const string ConnString = "server=localhost;database=infectious_disease;uid=root;pwd=123456";

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
