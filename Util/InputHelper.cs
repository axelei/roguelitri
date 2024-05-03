using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace roguelitri.Util;

public static class InputHelper
{

    public static bool AllKeyPressed(params Keys[] keys)
    {
        return keys.All(key => Keyboard.GetState().IsKeyDown(key));
    }
    
    public static bool KeysPressed(params Keys[] keys)
    {
        if (keys.Length == 0)
        {
            return Keyboard.GetState().GetPressedKeys().Length > 0;
        }
        return keys.Any(key => Keyboard.GetState().IsKeyDown(key));
    }
    
    public static bool AnyKeyPressed()
    {
        return Keyboard.GetState().GetPressedKeys().Length > 0;
    }
    
}