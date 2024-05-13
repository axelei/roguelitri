using System;
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
            using var game = new Game1();
            game.Run();
        }
#if DEBUG
        catch (Exception exception)
        {
            Console.WriteLine($"Exception caught: {exception.Message} -- see the logs.");
            Console.WriteLine(exception.StackTrace);
            Logger.Log("Exception: " + exception.Message);
            Logger.Log(exception.StackTrace);
            Logger.Dispose();
        }
#endif
    }
}