
// Type: Intermech.Kernel.Search.RelationalOperatorsHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Kernel.Search
{
    /// <summary>
    /// Вспомогательный класс для работы с операторами отношений
    /// </summary>
    public class RelationalOperatorsHelper
    {
      /// <summary>Получить заголовок для указанного оператора отношений</summary>
      /// <param name="mode">Оператор отношений</param>
      /// <returns>Заголовок</returns>
      public static string GetCaption(RelationalOperators mode)
      {
        return EnumTypeHelper.GetCaption((Enum) mode);
      }

      /// <summary>
      /// Получить оператор SQL для указанного оператора отношений
      /// </summary>
      /// <param name="mode">Оператор отношений</param>
      /// <returns>Оператор SQL</returns>
      public static string SQLOperator(RelationalOperators mode)
      {
        string str = "";
        switch (mode)
        {
          case RelationalOperators.Empty:
            str = " IS NULL";
            break;
          case RelationalOperators.NotEmpty:
            str = " IS NOT NULL";
            break;
          case RelationalOperators.Equal:
            str = " = {0}";
            break;
          case RelationalOperators.NotEqual:
            str = " <> {0}";
            break;
          case RelationalOperators.Greater:
            str = " > {0}";
            break;
          case RelationalOperators.GreaterOrEqual:
            str = " >= {0}";
            break;
          case RelationalOperators.Less:
            str = " < {0}";
            break;
          case RelationalOperators.LessOrEqual:
            str = " <= {0}";
            break;
          case RelationalOperators.Substring:
            str = " LIKE {0}";
            break;
          case RelationalOperators.StartString:
            str = " LIKE {0}";
            break;
          case RelationalOperators.EndString:
            str = " LIKE {0}";
            break;
          case RelationalOperators.Between:
            str = " BETWEEN {0} AND {1}";
            break;
          case RelationalOperators.NotSubstring:
            str = " NOT LIKE {0}";
            break;
          case RelationalOperators.NotStartString:
            str = " NOT LIKE {0}";
            break;
          case RelationalOperators.NotEndString:
            str = " NOT LIKE {0}";
            break;
          case RelationalOperators.NotBetween:
            str = " NOT BETWEEN {0} AND {1}";
            break;
          case RelationalOperators.In:
            str = " IN ({0})";
            break;
          case RelationalOperators.NotIn:
            str = " NOT IN ({0})";
            break;
          case RelationalOperators.NotExistsOrEmpty:
            str = " IS NULL";
            break;
          case RelationalOperators.StringTemplate:
            str = " LIKE {0}";
            break;
        }
        return str;
      }
    }
}
