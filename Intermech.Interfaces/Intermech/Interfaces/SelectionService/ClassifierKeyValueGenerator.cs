
// Type: Intermech.Interfaces.SelectionService.ClassifierKeyValueGenerator
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.SelectionService
{
    public static class ClassifierKeyValueGenerator
    {
      /// <summary>
      /// Функция генерации нового значения для ключа классификатора на основе текущего
      /// максимального значения. Значение ключа двухсимвольное. Код каждого символа находится в
      /// диапазоне от 40 до 126 включительно (за исключением символов '_' и '%' для организации
      /// SQL-запросов выбора из таблицы БД с использованием оператора LIKE).
      /// </summary>
      /// <param name="Value">текущее максимальное значение ключа</param>
      /// <returns>Новое значение ключа</returns>
      public static string GetNextKeyValue(string Value)
      {
        if (Value.Length < 2)
          return "((";
        if (Value.Length > 2)
          Value = Value.Substring(Value.Length - 2, 2);
        char ch = Convert.ToChar(Convert.ToByte(Value[1]) < (byte) 126 ? (int) Convert.ToByte(Value[1]) + 1 : (int) Convert.ToByte(Value[0]) + 1);
        while (ch == '_' || ch == '%' || ch == '[' || ch == ']')
          ch = Convert.ToChar((int) Convert.ToByte(ch) + 1);
        return Convert.ToByte(Value[1]) >= (byte) 126 ? Convert.ToString(ch) + "(" : Value[0].ToString() + Convert.ToString(ch);
      }
    }
}
