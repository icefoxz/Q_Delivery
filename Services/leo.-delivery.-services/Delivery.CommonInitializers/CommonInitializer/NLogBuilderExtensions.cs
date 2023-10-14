
namespace Delivery.CommonInitializers.CommonInitializer
{
    public static class NLogBuilderExtensions
    {
        public static void AddNLogServices(this IServiceCollection services)
        {
            //注入Nlog
            services.AddLogging(logging =>
            {
                logging.AddNLog();
                //logging.SetMinimumLevel(LogLevel.Debug);
            });
        }
    }
}
