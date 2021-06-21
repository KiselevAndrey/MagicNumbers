using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Режим нажатия клавиатуры для функций классов помощи
/// </summary>
public enum PressMode { Pressed, Down, Up};

/// <summary>
/// Кнопки взаимодействия сразу для клавиатуры и keypad
/// </summary>
public enum NameSymbal { Enter, Space }; // пока не сделаны...   , Minus, Plus, Multiply, Slash, Period, Equals};

public enum TypeKeyCode { Char, Digit, SpecialSymbal};


/// <summary>
/// Общий класс для ввода с клавиатуры
/// </summary>
public static class HelpInput
{
    /// <summary>
    /// Проверка на ввод символа нужного класса. keyCode = default если KeyCode не символ не найден
    /// </summary>
    /// <param name="pressMode"></param>
    /// <param name="keyCode">default если KeyCode не символ этого класса</param>
    /// <returns></returns>
    public static bool InputKey(TypeKeyCode typeKeyCode, PressMode pressMode, out KeyCode keyCode)
    {
        if (typeKeyCode == TypeKeyCode.Char) return HelpInputChar.InputKey(pressMode, out keyCode);
        else if (typeKeyCode == TypeKeyCode.Digit) return HelpInputDigit.InputKey(pressMode, out keyCode);
        else if (typeKeyCode == TypeKeyCode.SpecialSymbal) return HelpInputSpecialSymbal.InputKey(pressMode, out keyCode);

        keyCode = default;
        return false;
    }
}


/// <summary>
/// Класс для удобства ввода букв с клавиатуры
/// </summary>
static class HelpInputChar
{
    static KeyCode[] _keyCodes = { KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E, KeyCode.F, KeyCode.G, KeyCode.H,
        KeyCode.I, KeyCode.J, KeyCode.K, KeyCode.L, KeyCode.M, KeyCode.N, KeyCode.O, KeyCode.P, KeyCode.Q, KeyCode.R,
        KeyCode.S, KeyCode.T, KeyCode.U, KeyCode.V, KeyCode.W, KeyCode.X, KeyCode.Y, KeyCode.Z };

    static Dictionary<char, KeyCode> _charToKeyCode = new Dictionary<char, KeyCode>()
    {
         {'A', KeyCode.A}, {'a', KeyCode.A},
         {'B', KeyCode.B}, {'b', KeyCode.B},
         {'C', KeyCode.C}, {'c', KeyCode.C},
         {'D', KeyCode.D}, {'d', KeyCode.D},
         {'E', KeyCode.E}, {'e', KeyCode.E},
         {'F', KeyCode.F}, {'f', KeyCode.F},
         {'G', KeyCode.G}, {'g', KeyCode.G},
         {'H', KeyCode.H}, {'h', KeyCode.H},
         {'I', KeyCode.I}, {'i', KeyCode.I},
         {'J', KeyCode.J}, {'j', KeyCode.J},
         {'K', KeyCode.K}, {'k', KeyCode.K},
         {'L', KeyCode.L}, {'l', KeyCode.L},
         {'M', KeyCode.M}, {'m', KeyCode.M},
         {'N', KeyCode.N}, {'n', KeyCode.N},
         {'O', KeyCode.O}, {'o', KeyCode.O},
         {'P', KeyCode.P}, {'p', KeyCode.P},
         {'Q', KeyCode.Q}, {'q', KeyCode.Q},
         {'R', KeyCode.R}, {'r', KeyCode.R},
         {'S', KeyCode.S}, {'s', KeyCode.S},
         {'T', KeyCode.T}, {'t', KeyCode.T},
         {'U', KeyCode.U}, {'u', KeyCode.U},
         {'V', KeyCode.V}, {'v', KeyCode.V},
         {'W', KeyCode.W}, {'w', KeyCode.W},
         {'X', KeyCode.X}, {'x', KeyCode.X},
         {'Y', KeyCode.Y}, {'y', KeyCode.Y},
         {'Z', KeyCode.Z}, {'z', KeyCode.Z},
    };

    public static KeyCode CharToKeyCode(char value) => _charToKeyCode[value];

    /// <summary>
    ///  Проверка на ввод символа этого класса. keyCode = default если KeyCode не символ этого класса
    /// </summary>
    /// <param name="pressMode"></param>
    /// <param name="keyCode"></param>
    /// <returns></returns>
    public static bool InputKey(PressMode pressMode, out KeyCode keyCode)
    {
        foreach (KeyCode key in _keyCodes)
        {
            if ((pressMode == PressMode.Down && Input.GetKeyDown(key)) ||
                (pressMode == PressMode.Pressed && Input.GetKey(key)) ||
                (pressMode == PressMode.Up && Input.GetKeyUp(key)))
            {
                keyCode = key;
                return true;
            }
        }
        keyCode = default;
        return false;
    }
}


/// <summary>
/// Класс для удобства ввода цифр с клавиатуры
/// </summary>
static class HelpInputDigit
{

    static KeyCode[] _keyCodes = {KeyCode.Alpha0, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9,
    KeyCode.Keypad0, KeyCode.Keypad1, KeyCode.Keypad2, KeyCode.Keypad3, KeyCode.Keypad4, KeyCode.Keypad5, KeyCode.Keypad6, KeyCode.Keypad7, KeyCode.Keypad8, KeyCode.Keypad9 };

    /// <summary>
    ///  Проверка на ввод символа этого класса. keyCode = default если KeyCode не символ этого класса
    /// </summary>
    /// <param name="pressMode"></param>
    /// <param name="keyCode"></param>
    /// <returns></returns>
    static public bool InputKey(PressMode pressMode, out KeyCode keyCode)
    {
        foreach (KeyCode key in _keyCodes)
        {
            if ((pressMode == PressMode.Down && Input.GetKeyDown(key)) ||
                (pressMode == PressMode.Pressed && Input.GetKey(key)) ||
                (pressMode == PressMode.Up && Input.GetKeyUp(key)))
            {
                keyCode = key;
                return true;
            }
        }
        keyCode = default;
        return false;
    }

    /// <summary>
    ///  Проверка на ввод символа этого класса. digit = default если KeyCode не символ этого класса
    /// </summary>
    /// <param name="pressMode"></param>
    /// <param name="digit"></param>
    /// <returns></returns>
    static public bool InputKey(PressMode pressMode, out string digit)
    {
        foreach (KeyCode key in _keyCodes)
        {
            if ((pressMode == PressMode.Down && Input.GetKeyDown(key)) ||
                (pressMode == PressMode.Pressed && Input.GetKey(key)) ||
                (pressMode == PressMode.Up && Input.GetKeyUp(key)))
            {
                digit = key.ToString();
                digit = digit[digit.Length - 1].ToString();
                return true;
            }
        }
        digit = default;
        return false;
    }
}


/// <summary>
/// Класс для удобства ввода символов
/// </summary>
static class HelpInputSpecialSymbal
{
    static KeyCode[] _keyCodes = { KeyCode.Return, KeyCode.KeypadEnter };
    //    KeyCode.Plus, KeyCode.KeypadPlus, KeyCode.Minus, KeyCode.KeypadMinus, KeyCode.Asterisk, KeyCode.KeypadMultiply, KeyCode.Slash, KeyCode.KeypadDivide,
    //KeyCode.Period, KeyCode.KeypadPeriod, KeyCode.Equals, 

    static Dictionary<NameSymbal, KeyCode> _symbalToCode = new Dictionary<NameSymbal, KeyCode>()
    {
        {NameSymbal.Enter, KeyCode.Return },
        {NameSymbal.Space, KeyCode.Space }
    };

    /// <summary>
    /// Проверка на ввод символа этого класса. keyCode = default если KeyCode не символ этого класса
    /// </summary>
    /// <param name="pressMode"></param>
    /// <param name="keyCode">default если KeyCode не символ этого класса</param>
    /// <returns></returns>
    public static bool InputKey(PressMode pressMode, out KeyCode keyCode)
    {
        foreach (KeyCode key in _keyCodes)
        {
            if ((pressMode == PressMode.Down && Input.GetKeyDown(key)) ||
                (pressMode == PressMode.Pressed && Input.GetKey(key)) ||
                (pressMode == PressMode.Up && Input.GetKeyUp(key)))
            {
                keyCode = key;
                return true;
            }
        }
        keyCode = default;
        return false;
    }

    /// <summary>
    /// Проверка на ввод особенного символа этого класса.
    /// </summary>
    /// <param name="pressMode"></param>
    /// <param name="keyCode">default если KeyCode не символ этого класса</param>
    /// <returns></returns>
    public static bool InputKey(PressMode pressMode, NameSymbal nameSymbal)
    {
        return InputKey(pressMode, nameSymbal, out KeyCode keyCode);
    }

    /// <summary>
    /// Проверка на ввод особенного символа этого класса. keyCode = default если KeyCode не символ этого класса
    /// </summary>
    /// <param name="pressMode"></param>
    /// <param name="keyCode">default если KeyCode не символ этого класса</param>
    /// <returns></returns>
    public static bool InputKey(PressMode pressMode, NameSymbal nameSymbal, out KeyCode keyCode)
    {
        ///// Enter /////
        if (nameSymbal == NameSymbal.Enter && 
            ((pressMode == PressMode.Down && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))) ||
            (pressMode == PressMode.Pressed && (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter))) ||
            (pressMode == PressMode.Up && (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.KeypadEnter)))))
        {
            keyCode = _symbalToCode[NameSymbal.Enter];
            return true;
        }

        ///// Space /////
        if (nameSymbal == NameSymbal.Space && ((pressMode == PressMode.Down && Input.GetKeyDown(KeyCode.Space)) ||
            (pressMode == PressMode.Pressed && Input.GetKey(KeyCode.Space)) || (pressMode == PressMode.Up && Input.GetKeyUp(KeyCode.Space))))
        {
            keyCode = _symbalToCode[NameSymbal.Space];
            return true;
        }

        keyCode = default;
        return false;
    }

    public static KeyCode SymbalToKeyCode(NameSymbal nameSymbal) => _symbalToCode[nameSymbal];
}