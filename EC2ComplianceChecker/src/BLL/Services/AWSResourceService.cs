namespace EC2ComplianceChecker
{
    using Amazon.SimpleSystemsManagement;
    using Amazon.SimpleSystemsManagement.Model;

    public class AWSResourceService : IAWSResourceService
    {
        private readonly IClientFactory<AmazonSimpleSystemsManagementClient> _ssmClientFactory;
        private AmazonSimpleSystemsManagementClient _ssmClient;
        private readonly ILoggerService _logger;

        public AWSResourceService(ILoggerService logger, IClientFactory<AmazonSimpleSystemsManagementClient> ssmClientFactory)
        {
            _ssmClientFactory = ssmClientFactory;
            _ssmClient = _ssmClientFactory.CreateClient();
            _logger = logger;
        }

        public async Task<List<InstanceInformation>> GetManagedInstancesAsync(List<ResourceType> resourcesType)
        {
            try
            {
                _logger.LogInfo("Starting to retrieve managed instances.");

                var request = new DescribeInstanceInformationRequest
                {
                    InstanceInformationFilterList = new List<InstanceInformationFilter>
                {
                    new InstanceInformationFilter
                    {
                        Key = "ResourceType",
                        ValueSet = resourcesType.Select(r => r.Value).ToList()
                    }
                },
                };

                var response = await _ssmClient.DescribeInstanceInformationAsync(request);

                _logger.LogInfo($"Retrieved {response.InstanceInformationList.Count} instances.");

                return response.InstanceInformationList;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AWSResourceService-> Error retrieving managed instances: {ex.Message}");
                throw;
            }
        }

        public async Task<List<ComplianceItem>> GetComplianceItemsAsync(string instanceId)
        {
            try
            {
                _logger.LogInfo($"Retrieving compliance items for instance: {instanceId}");

                var request = new ListComplianceItemsRequest
                {
                    ResourceIds = new List<string> { instanceId }
                };
                var response = await _ssmClient.ListComplianceItemsAsync(request);

                _logger.LogInfo($"Retrieved {response.ComplianceItems.Count} compliance items for instance {instanceId}.");

                return response.ComplianceItems;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AWSResourceService-> Error retrieving compliance items for instance {instanceId}: {ex.Message}");
                throw;
            }
        }
    }
}
