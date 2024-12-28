namespace EC2ComplianceChecker
{
    public class AWSClientManager
    {
        // In the future, components like S3, Lambda, etc., can be added.
        private readonly ILoggerService _logger;

        public AWSClientManager(ILoggerService logger)
        {
            _logger = logger;
        }

        public TClient GetClient<TClient>(IClientFactory<TClient> factory)
        {
            _logger.LogInfo($"AWSClientManager-> Fetching client of type {typeof(TClient).Name}");
            return factory.CreateClient();
        }
    }

}
