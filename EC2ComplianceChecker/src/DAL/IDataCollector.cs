using Amazon.SimpleSystemsManagement;

namespace EC2ComplianceChecker
{
    public interface IDataCollector
    {
        Task<List<InstanceInfo>> GetUnhealthyInstancesAsync(List<ResourceType> resourcesType);
    }
}
