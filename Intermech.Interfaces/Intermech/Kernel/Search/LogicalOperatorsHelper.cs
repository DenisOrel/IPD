
// Type: Intermech.Kernel.Search.LogicalOperatorsHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Kernel.Search
{
    /// <summary>Вспомогательный класс по логическим операторам SQL</summary>
    public class LogicalOperatorsHelper
    {
      /// <summary>Получить заголовок логического оператора</summary>
      /// <param name="mode">Логический оператор</param>
      /// <returns>Заголовок</returns>
      public static string GetCaption(LogicalOperators mode) => EnumTypeHelper.GetCaption((Enum) mode);

      /// <summary>Получить логический оператор по заголовку</summary>
      /// <param name="mode">Логический оператор</param>
      /// <returns>Логический оператор в виде строки</returns>
      public static string SQLOperator(LogicalOperators mode)
      {
        string str = "";
        if (mode != LogicalOperators.NONE)
          str = mode.ToString();
        return str;
      }
    }
}
