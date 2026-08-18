
// Type: Intermech.Interfaces.ParsedNumberData
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Вспомогательный класс для хранения результатов парсера
    /// и последующего преобразования их в число</summary>
    [Serializable]
    public class ParsedNumberData
    {
      /// <summary>Точность числа (к-во знаков)</summary>
      public int precision;
      /// <summary>Множитель</summary>
      public int scale;
      /// <summary>Знак: 0 - "+", 1 - "-"</summary>
      public int sign;
      /// <summary>Цифры</summary>
      public char[] digits = new char[NumberParserAdvanced.NUMBER_MAXDIGITS + 1];

      /// <summary>Конструктор</summary>
      public ParsedNumberData()
      {
      }

      /// <summary>Конструктор</summary>
      /// <param name="precision">Точность числа (к-во цифр)</param>
      /// <param name="scale">Множитель</param>
      /// <param name="sign">Знак: 0 - "+", 1 - "-"</param>
      public ParsedNumberData(int precision, int scale, int sign)
      {
        this.precision = precision;
        this.scale = scale;
        this.sign = sign;
      }
    }
}
