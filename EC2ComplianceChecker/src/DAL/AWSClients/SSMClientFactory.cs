namespace EC2ComplianceChecker
{
    using Amazon.Runtime;
    using Amazon.SimpleSystemsManagement;

    public class SSMClientFactory : IClientFactory<AmazonSimpleSystemsManagementClient>
    {
        private static AmazonSimpleSystemsManagementClient _ssmClient;
        private readonly ILoggerService _logger;

        public SSMClientFactory(ILoggerService logger)
        {
            _logger = logger;
        }

        public AmazonSimpleSystemsManagementClient CreateClient()
        {
            try
            {
                if (_ssmClient == null)
                {
                    _logger.LogInfo("Initializing AWS SSM client.");
                    // Replace with valid AWS account credentials
                    var credentials = new BasicAWSCredentials("ACCESS_KEY", "SECRET_KEY");
                    _ssmClient = new AmazonSimpleSystemsManagementClient();
                    _logger.LogInfo("AWS SSM client initialized successfully.");
                }
                return _ssmClient;
            }
            catch (Exception ex)
            {
                _logger.LogError($"SSMClientFactory-> Error initializing AWS SSM client: {ex.Message}");
                throw;
            }
        }
    }
}
