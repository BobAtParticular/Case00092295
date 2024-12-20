using NServiceBus.Logging;
using NServiceBus.Transport;
using NServiceBus;

public static class CustomRecoverabilityPolicy
{
    static readonly ILog log = LogManager.GetLogger("CustomRecoverabilityPolicy");

    public static RecoverabilityAction Invoke(RecoverabilityConfig config, ErrorContext context)
    {
        log.Warn($"Custom recoverability policy invoked");
        return DefaultRecoverabilityPolicy.Invoke(config, context);        
    }
}