
// Type: Intermech.Interfaces.fncnConsts
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс с вариантами значений для отрицания функции сравнения
    /// </summary>
    public abstract class fncnConsts
    {
      public const string fncnDefault = "";
      /// <summary>НЕ</summary>
      public static readonly string fncnNegation = LocalizationHolder.rm.GetString("Interfaces_520");

      /// <summary>Метод возвращает массив со всеми константами класса</summary>
      /// <returns>Массив со всеми константами класса</returns>
      public static object[] GetMembers()
      {
        return new object[2]
        {
          (object) "",
          (object) fncnConsts.fncnNegation
        };
      }

      /// <summary>
      /// Проверить, является ли указанное значение одной из констант класса
      /// </summary>
      /// <param name="value">Искомое значение</param>
      /// <returns>true, если значение является константой класса</returns>
      public static bool IsMember(string value)
      {
        foreach (object member in fncnConsts.GetMembers())
        {
          if (member.ToString() == value)
            return true;
        }
        return false;
      }

      /// <summary>В зависимости от value вернуть соответствующую строку</summary>
      /// <param name="value">false = "", true = "НЕ"</param>
      /// <returns>"" или "НЕ"</returns>
      public static string GetNegationValue(bool value) => value ? fncnConsts.fncnNegation : "";
    }
}
