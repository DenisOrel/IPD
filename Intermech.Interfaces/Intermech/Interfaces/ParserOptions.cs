
// Type: Intermech.Interfaces.ParserOptions
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Опции парсера</summary>
    [Flags]
    public enum ParserOptions
    {
      /// <summary>Допускать пробелы перед числом</summary>
      LEADINGWHITE = 1,
      /// <summary>Допускать пробелы после числа</summary>
      TRAILINGWHITE = 2,
      /// <summary>Перед числом может быть знак</summary>
      LEADINGSIGN = 4,
      /// <summary>После числа может быть знак</summary>
      TRAILINGSIGN = 8,
      /// <summary>Допускать скобки</summary>
      PARENS = 16, // 0x00000010
      /// <summary>Десятичный формат числа</summary>
      DECIMAL = 32, // 0x00000020
      /// <summary>Допускать разделители тысяч</summary>
      THOUSANDS = 64, // 0x00000040
      /// <summary>Научный формат числа</summary>
      SCIENTIFIC = 128, // 0x00000080
      /// <summary>Валютный формат числа</summary>
      CURRENCY = 256, // 0x00000100
      /// <summary>Шестнадцатеричный формат числа</summary>
      HEX = 512, // 0x00000200
      /// <summary>Допустим знак процентов</summary>
      PERCENT = 1024, // 0x00000400
      /// <summary>Игнорировать текст после числа</summary>
      IgnoreTrailingText = 4096, // 0x00001000
      /// <summary>Пропускать текст перед числом</summary>
      SkipLeadingText = 8192, // 0x00002000
      /// <summary>Использовать и русский (,) и английский (.) десятичный разделитель</summary>
      UseRusAndEnDecimalSeparators = 16384, // 0x00004000
    }
}
