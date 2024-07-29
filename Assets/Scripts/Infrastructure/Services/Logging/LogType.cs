using System;

namespace Logging
{
    [Flags]
    public enum LogType
    {
        Log = 0,
        LogError,
        LogWarning,
    }
}