namespace EC2ComplianceChecker
{
    using Amazon.SimpleSystemsManagement;

    public class DataCollector : IDataCollector
    {
        private readonly IAWSResourceService _resourceService;
        private readonly IComplianceChecker _complianceChecker;
        private readonly ILoggerService _logger;

        public DataCollector(IAWSResourceService resourceService, IComplianceChecker evaluator, ILoggerService logger)
        {
            _resourceService = resourceService;
            _complianceChecker = evaluator;
            _logger = logger;
        }

        public async Task<List<InstanceInfo>> GetUnhealthyInstancesAsync(List<ResourceType> resourcesType)
        {
            var unhealthyInstances = new List<InstanceInfo>();

            try
            {
                var instances = await _resourceService.GetManagedInstancesAsync(resourcesType);

                if (instances.Count == 0)
                {
                    _logger.LogInfo($"DataCollector-> No managed instances found");
                    return unhealthyInstances;
                }

                var tasks = instances.Select(async instance =>
                {
                    try
                    {
                        var complianceItems = await _resourceService.GetComplianceItemsAsync(instance.InstanceId);

                        if (!_complianceChecker.CheckCompliance(complianceItems))
                        {
                            lock (unhealthyInstances)
                            {
                                unhealthyInstances.Add(new InstanceInfo
                                {
                                    InstanceId = instance.InstanceId,
                                    InstanceType = instance.ResourceType
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"DataCollector-> Error checking compliance for instance {instance.InstanceId}, Type: {instance.ResourceType}: {ex.Message}");
                    }
                }).ToList();

                await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError($"DataCollector-> Error during compliance check process: {ex.Message}");
            }

            return unhealthyInstances;
        }
    }
}
