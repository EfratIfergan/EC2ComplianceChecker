namespace EC2ComplianceChecker
{
    using Amazon.SimpleSystemsManagement.Model;
    using System.Collections.Generic;

    public class ComplianceChecker : IComplianceChecker
    {
        private readonly List<IComplianceEvaluator> _evaluators;

        public ComplianceChecker()
        {
            _evaluators = new List<IComplianceEvaluator>
            {
                new SSM3ComplianceEvaluator(),
                // In the future, we can add additional checks here e.g. SSM4, etc.
            };
        }

        public bool CheckCompliance(List<ComplianceItem> complianceItems)
        {
            foreach (var evaluator in _evaluators)
            {
                if (!evaluator.IsInstanceCompliant(complianceItems))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
