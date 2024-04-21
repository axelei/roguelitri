using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace roguelitri.util;

public static class Input
{

    private static Keys[] _previousKeyPressedQuery;
    private static Tuple<PlayerIndex, Buttons[]> _previousButtonPressedQuery;
    public static bool KeyPressed(params Microsoft.Xna.Framework.Input.Keys[] keys)
    {
        return keys.All(key => Keyboard.GetState().IsKeyDown(key));
    }

    public static bool HasBeenPressed(params Keys[] keys)
    {
        var keyUp = keys.Any(key => Keyboard.GetState().IsKeyUp(key));
        var keyDown = keys.All(key => Keyboard.GetState().IsKeyDown(key));

        if (keyUp || _previousKeyPressedQuery != null && keys.SequenceEqual(_previousKeyPressedQuery))
        {
            _previousKeyPressedQuery = null;
            return false;
        }

        if (keyDown)
        {
            _previousKeyPressedQuery = keys;
            return true;
        }
        return false;

    }

    public static bool HasBeenPressed(PlayerIndex playerIndex, params Buttons[] buttons)
    {
        var buttonNotPressed = buttons.Any(button => !ButtonPressed(GetByButton(playerIndex, button)));
        var buttonPressed = buttons.Any(button => ButtonPressed(GetByButton(playerIndex, button)));
        var previousQueryMatch = _previousButtonPressedQuery != null 
                                 && _previousButtonPressedQuery.Item1 == playerIndex 
                                 && _previousButtonPressedQuery.Item2.SequenceEqual(buttons);

        if (buttonNotPressed)
        {
            _previousButtonPressedQuery = null;
        }

        if (previousQueryMatch)
        {
            return false;
        }

        if (buttonPressed)
        {
            _previousButtonPressedQuery = new Tuple<PlayerIndex, Buttons[]>(playerIndex, buttons);
        }

        return false;
    }


    public static bool ButtonPressed(PlayerIndex playerIndex, params Buttons[] buttons)
    {
        return buttons.Select(button => GetByButton(playerIndex, button)).All(element => element != ButtonState.Released);
    }

    public static bool AnyKeyPressed()
    {
        return Enum.GetValues(typeof(Keys)).Cast<Keys>().Any(keys => HasBeenPressed(keys)) 
                             || Enumerable.Range(0, GamePad.MaximumGamePadCount)
                                 .Any(i => Enum.GetValues(typeof(Buttons)).Cast<Buttons>()
                                     .Any(button => ButtonPressed(GetByButton((PlayerIndex)i, button))));
    }

    public static Keys[] KeysPressed()
    {
        return Enum.GetValues(typeof(Keys)).Cast<Keys>().Where(key => HasBeenPressed(key)).ToArray();
    }

    private static ButtonState GetByButton(PlayerIndex playerIndex, Buttons button)
    {
        return GamePad.GetState(playerIndex).IsButtonDown(button) ? ButtonState.Pressed : ButtonState.Released;
    }

    private static bool ButtonPressed(ButtonState buttonState)
    {
        return buttonState == ButtonState.Pressed;
    }
}