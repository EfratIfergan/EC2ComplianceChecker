namespace EC2ComplianceChecker
{
    using Amazon.SimpleSystemsManagement.Model;

    public interface IComplianceChecker
    {
        bool CheckCompliance(List<ComplianceItem> complianceItems);
    }
}