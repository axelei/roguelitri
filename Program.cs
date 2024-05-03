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
            INativeFmodLibrary nativeLibrary = new DesktopNativeFmodLibrary();
            using var game = new Game1(nativeLibrary);
            game.Run();
        }
#if DEBUG
        catch (Exception exception)
        {
            Console.WriteLine($"Exception caught: {exception.Message} -- see the logs.");
            Logger.Log("Exception: " + exception.Message);
            Logger.Log(exception.StackTrace);
            Logger.Dispose();
        }
#endif
    }
}