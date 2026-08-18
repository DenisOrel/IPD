
// Type: Intermech.Interfaces.cvConsts
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Варианты для поля, в котором вводятся искомые значения для сравнения
    /// </summary>
    public abstract class cvConsts
    {
      /// <summary>[Укажите значение для сравнения]</summary>
      public static readonly string cvConst = LocalizationHolder.rm.GetString("Interfaces_522");
      /// <summary>[Укажите начальное значение диапазона]</summary>
      public static readonly string cvMinValue = LocalizationHolder.rm.GetString("Interfaces_523");
      /// <summary>[Укажите конечное значение диапазона]</summary>
      public static readonly string cvMaxValue = LocalizationHolder.rm.GetString("Interfaces_524");
      /// <summary>[Значение для сравнения укажет пользователь]</summary>
      public static readonly string cvVariable = LocalizationHolder.rm.GetString("Interfaces_525");
      /// <summary>[Выберите подходящий атрибут для сравнения]</summary>
      public static readonly string cvAttribute = LocalizationHolder.rm.GetString("Interfaces_526");
    }
}
