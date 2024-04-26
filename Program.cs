using System;
using FmodForFoxes;
using roguelitri.Util;

namespace roguelitri;

internal static class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
#if DEBUG
        Console.WriteLine("Args: " + string.Join(", ", args));
        try
#endif
        {
            INativeFmodLibrary _nativeLibrary = new DesktopNativeFmodLibrary();
            using var game = new Game1(_nativeLibrary);
            game.Run();
        }
#if DEBUG
        catch (Exception exception)
        {
            Logger.Log("Exception: " + exception.Message);
            foreach(string line in exception.StackTrace.Split("\n"))
            {
                Logger.Log(line);
            }

            throw;
        }
#endif
    }
}