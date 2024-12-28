namespace EC2ComplianceChecker
{
    //In the future, may include implementations like file writing, external services, etc.
    public interface ILoggerService
    {
        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(string message);
    }
}
