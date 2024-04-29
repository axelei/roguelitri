using System;
using System.IO;

namespace roguelitri.Util;

public static class Logger
{
    private static string _logFile;
    private static StreamWriter _writer;
    
    public static void Initialize()
    {
#if DEBUG
        _logFile = Misc.AppName + "_" + GameUtils.EpochMillis() + ".log";
        _writer = new StreamWriter(_logFile);
        Log(Misc.AppName + " " + Misc.AppVersion + " started");
#endif
    }

    public static void Log(string logMessage)
    {
#if DEBUG
        if (_writer == null)
        {
            throw new InvalidOperationException("Log class not initialized");
        }
        _writer.WriteLine("{0:O} - {1}", DateTime.Now, logMessage);
#endif
    }

    public static void Dispose()
    {
#if DEBUG
        _writer.Close();
#endif
    }
}