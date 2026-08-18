
// Type: Intermech.Interfaces.State
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Состояние парсера</summary>
    [Flags]
    public enum State
    {
      /// <summary>Знак</summary>
      SIGN = 1,
      /// <summary>Скобки</summary>
      PARENS = 2,
      /// <summary>Обрабатываются числа</summary>
      DIGITS = 4,
      /// <summary>Число не ноль</summary>
      NONZERO = 8,
      /// <summary>Число десятичное</summary>
      DECIMAL = 16, // 0x00000010
      /// <summary>Число - валюта</summary>
      CURRENCY = 32, // 0x00000020
    }
}
