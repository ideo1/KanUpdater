namespace KanUpdater
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var hostbuilder = Host.CreateDefaultBuilder(args)
                    .ConfigureUmbracoDefaults()
                    .ConfigureAppConfiguration((config) =>
                    {
                        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    })
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                    });

            return hostbuilder;
        }
    }
}
