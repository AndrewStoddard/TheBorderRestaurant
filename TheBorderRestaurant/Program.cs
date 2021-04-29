using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace TheBorderRestaurant
{
    public class Program
    {
        #region Methods

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                       .ConfigureWebHostDefaults(webBuilder =>
                       {
                           webBuilder.UseStartup<Startup>()
                                     .UseDefaultServiceProvider(options => options.ValidateScopes = false);
                       });
        }

        #endregion
    }
}