namespace EC2ComplianceChecker
{
    using Amazon.SimpleSystemsManagement;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IAWSResourceService, AWSResourceService>();
            services.AddSingleton<IComplianceEvaluator, SSM3ComplianceEvaluator>();
            services.AddSingleton<IDataCollector, DataCollector>();
            services.AddSingleton<ILoggerService, ConsoleLoggerService>();
            services.AddSingleton<IComplianceChecker, ComplianceChecker>();
            services.AddSingleton<IClientFactory<AmazonSimpleSystemsManagementClient>, SSMClientFactory>();
            services.AddSingleton<AWSClientManager>();

            return services;
        }
    }
}