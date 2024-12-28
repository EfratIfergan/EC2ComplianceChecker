namespace EC2ComplianceChecker
{
    using Amazon.SimpleSystemsManagement;
    using Amazon.SimpleSystemsManagement.Model;

    public interface IAWSResourceService
    {
        Task<List<InstanceInformation>> GetManagedInstancesAsync(List<ResourceType> resourcesType);
        Task<List<ComplianceItem>> GetComplianceItemsAsync(string instanceId);
    }
}
