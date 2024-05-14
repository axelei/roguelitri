using System;
using System.IO;

namespace roguelitri.Util;

public static class Logger
    {
    private static StreamWriter _writer;

    public static void Initialize()
    {
#if DEBUG
        _writer = new StreamWriter($"{Misc.AppName}_{GameUtils.EpochMillis()}.log");
        Log($"{Misc.AppName} {Misc.AppVersion} started");
#endif
    }

    public static void Log(string logMessage)
    {
#if DEBUG
        _writer?.WriteLine("{0:O} - {1}", DateTime.Now, logMessage);
#endif
    }

    public static void Dispose()
    {
#if DEBUG
        _writer?.Close();
#endif
    }
}