namespace EC2ComplianceChecker
{
    using Amazon.SimpleSystemsManagement;
    using Amazon.SimpleSystemsManagement.Model;

    public class SSM3ComplianceEvaluator : IComplianceEvaluator
    {
        public bool IsInstanceCompliant(List<ComplianceItem> complianceItems)
        {
            foreach (var item in complianceItems)
            {
                if (item.Status != ComplianceStatus.COMPLIANT)
                    return false;
            }
            return true;
        }
    }
}
