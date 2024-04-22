using System;

namespace roguelitri.Util;

public static class Misc
{
    public static readonly Version AppVersion = typeof(Program).Assembly.GetName().Version;
    public static readonly string AppName = typeof(Program).Assembly.GetName().Name;
}