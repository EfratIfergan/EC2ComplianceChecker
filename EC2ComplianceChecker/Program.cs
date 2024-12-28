using Amazon.SimpleSystemsManagement;
using EC2ComplianceChecker;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        DependencyInjectionConfig.ConfigureServices(services);
        var serviceProvider = services.BuildServiceProvider();

        var logger = serviceProvider.GetService<ILoggerService>();
        if (logger == null)
        {
            Console.WriteLine("Failed to initialize LoggerService.");
            return;
        }

        var complianceChecker = serviceProvider.GetService<IDataCollector>();
        if (complianceChecker == null)
        {
            logger.LogError("Failed to initialize ComplianceChecker.");
            return;
        }

        var resourcesType = new List<ResourceType> { ResourceType.ManagedInstance, ResourceType.EC2Instance };

        logger.LogInfo("Checking compliance...");

        try
        {
            var unhealthyInstances = await complianceChecker.GetUnhealthyInstancesAsync(resourcesType);

            if (unhealthyInstances.Count == 0)
            {
                logger.LogInfo("All instances are compliant!");
            }
            else
            {
                logger.LogInfo("Unhealthy Instances:");
                foreach (var instance in unhealthyInstances)
                {
                    logger.LogInfo($" - ID: {instance.InstanceId}, Type: {instance.InstanceType}");
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError($"An error occurred: {ex.Message}");
        }
    }
}
