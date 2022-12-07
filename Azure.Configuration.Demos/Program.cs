using Azure.Identity;

namespace Azure.Configuration.Demos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureAppConfiguration((webHostBuilderContext, configurationBuilder) =>
                    {
                        // First we build the IConfigurationBuilder to be able to access settings in the
                        // sources already added (appsettings.json).
                        var settings = configurationBuilder.Build();
                        configurationBuilder.AddAzureAppConfiguration(options =>
                        {
                            options.Connect(settings["AppConfigurationConnectionString"])
                                   //.ConfigureRefresh(refresh =>
                                   //{
                                   //     refresh.Register("sentinel", refreshAll: true)
                                   //             .SetCacheExpiration(new TimeSpan(0, 2, 30));
                                   //})
                                   .ConfigureKeyVault(kv =>
                                   {
                                       kv.SetCredential(new DefaultAzureCredential());
                                   });
                            // Azure App Configuration supports giving settings Labels. This way a setting can have
                            // multiple values, all for different labels. Such a label acts like a tag for settings. 
                            // If you don't specify a label to filter on, you only get values with a null label. As 
                            // soon as you specify a label, you only get values with that label.
                            // The good news: you can stack filters! The last filter 'wins' (stacks over the rest).
                            // The below configuration roughly translates into:
                            // - If there's a value for a setting with the label 'Prod', give me that one.
                            // - If there is no value labeled 'Prod', but there's a value labeled 'Acc', give me that one.
                            // - If there is no value labeled 'Acc', but there's a value labeled 'Test', give me that one.
                            // - If there is no value labeled 'Test', give me the one without a label.
                            //options.Connect(settings["AppConfigurationConnectionString"])
                            //       .Select(KeyFilter.Any, null)
                            //       .Select(KeyFilter.Any, "Test")
                            //       .Select(KeyFilter.Any, "Acc")
                            //       .Select(KeyFilter.Any, "Prod");
                        });
                    });
                });
    }
}