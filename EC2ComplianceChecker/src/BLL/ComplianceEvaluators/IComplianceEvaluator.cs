namespace EC2ComplianceChecker
{
    using Amazon.SimpleSystemsManagement.Model;

    public interface IComplianceEvaluator
    {
        bool IsInstanceCompliant(List<ComplianceItem> complianceItems);
    }
}