﻿using System;
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
            Logger.Log("Exception: " + exception.Message);
            foreach(string line in exception.StackTrace.Split("\n"))
            {
                Logger.Log(line);
            }
        }
#endif
    }
}