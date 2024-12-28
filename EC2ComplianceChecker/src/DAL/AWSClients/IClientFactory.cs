namespace EC2ComplianceChecker
{
    public interface IClientFactory<TClient>
    {
        TClient CreateClient();
    }
}