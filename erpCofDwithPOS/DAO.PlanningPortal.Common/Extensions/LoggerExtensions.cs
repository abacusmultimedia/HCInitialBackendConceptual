using Microsoft.Extensions.Logging;

namespace DAO.PlanningPortal.Common.Extensions;

public static class LoggerExtensions
{
    public static bool IsTraceEnabled(this ILogger logger)
    {
        return logger.IsEnabled(LogLevel.Trace);
    }

    public static bool IsDebugEnabled(this ILogger logger)
    {
        return logger.IsEnabled(LogLevel.Debug);
    }

    public static bool IsInformationEnabled(this ILogger logger)
    {
        return logger.IsEnabled(LogLevel.Information);
    }

    public static bool IsWarningEnabled(this ILogger logger)
    {
        return logger.IsEnabled(LogLevel.Warning);
    }

    public static bool IsErrorEnabled(this ILogger logger)
    {
        return logger.IsEnabled(LogLevel.Error);
    }

    public static bool IsCriticalEnabled(this ILogger logger)
    {
        return logger.IsEnabled(LogLevel.Critical);
    }
}