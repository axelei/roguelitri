using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace roguelitri.util;

public static class Input
{

    private static Keys[] _previousPressedQuery;
    public static bool KeyPressed(params Microsoft.Xna.Framework.Input.Keys[] keys)
    {
        return keys.All(key => Keyboard.GetState().IsKeyDown(key));
    }

    public static bool HasBeenPressed(params Keys[] keys)
    {
        if (keys.Any(key => Keyboard.GetState().IsKeyUp(key)))
        {
            _previousPressedQuery = null;
        }
        if (_previousPressedQuery != null && keys.SequenceEqual(_previousPressedQuery))
        {
            return false;
        }
        if (keys.All(key => Keyboard.GetState().IsKeyDown(key)))
        {
            _previousPressedQuery = keys;
            return true;
        }
        return false;
    }

    public static bool ButtonPressed(PlayerIndex playerIndex, params Buttons[] buttons)
    {
        foreach (var button in buttons)
        {
            ButtonState element = button switch
            {
                Buttons.DPadUp => GamePad.GetState(playerIndex).DPad.Up,
                Buttons.DPadDown => GamePad.GetState(playerIndex).DPad.Down,
                Buttons.DPadLeft => GamePad.GetState(playerIndex).DPad.Left,
                Buttons.DPadRight => GamePad.GetState(playerIndex).DPad.Right,
                Buttons.Start => GamePad.GetState(playerIndex).Buttons.Start,
                Buttons.Back => GamePad.GetState(playerIndex).Buttons.Back,
                Buttons.LeftStick => GamePad.GetState(playerIndex).Buttons.LeftStick,
                Buttons.RightStick => GamePad.GetState(playerIndex).Buttons.RightStick,
                Buttons.LeftShoulder => GamePad.GetState(playerIndex).Buttons.LeftShoulder,
                Buttons.RightShoulder => GamePad.GetState(playerIndex).Buttons.RightShoulder,
                Buttons.BigButton => GamePad.GetState(playerIndex).Buttons.BigButton,
                Buttons.A => GamePad.GetState(playerIndex).Buttons.A,
                Buttons.B => GamePad.GetState(playerIndex).Buttons.B,
                Buttons.X => GamePad.GetState(playerIndex).Buttons.X,
                Buttons.Y => GamePad.GetState(playerIndex).Buttons.Y,
                _ => throw new ArgumentOutOfRangeException(nameof(button), button,
                    "Specified input does not seem to exist or supported for this controller.")
            };
            if (element == ButtonState.Released)
            {
                return false;
            }
        }
        return true;
    }
}