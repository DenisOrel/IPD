
// Type: Intermech.Expressions.ColumnExpressionItem
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;


namespace Intermech.Expressions
{
    /// <summary>
    /// Класс, описывающий вычисляемую колонку в таблице DataTable
    /// </summary>
    public class ColumnExpressionItem
    {
      /// <summary>Откомпилированная формула</summary>
      public ExpressionTree Expression;
      /// <summary>Индекс колонки формулы в таблице</summary>
      public int ColumnIndex;
      /// <summary>
      /// Индексы колонок-значений, которые используются для вычисления формулы. Если в таблице колонки нет, то хранится -1.
      /// </summary>
      public int[] ValueIndexes;
      /// <summary>Идентификаторы атрибутов, используемых в формуле.</summary>
      public int[] ValueAttributeIDs;
      /// <summary>
      /// Идентификатор атрибута, для которого рассчитывается формула
      /// </summary>
      public int FormulaAttributeID;

      public ColumnExpressionItem(
        string formula,
        int columnIndex,
        IDBAttributeType[] columnsList,
        IUserSession session)
      {
        this.ColumnIndex = columnIndex;
        ExpressionVariablesCollection variables;
        using (Parser parser = new Parser())
        {
          parser.AutoDetectVariables = true;
          parser.Validate = false;
          this.Expression = parser.Parse(formula);
          variables = this.Expression.Variables;
        }
        this.ValueIndexes = new int[variables.Count];
        this.ValueAttributeIDs = new int[variables.Count];
        this.FormulaAttributeID = columnsList[columnIndex].AttributeID;
        for (int index1 = 0; index1 < variables.Count; ++index1)
        {
          this.ValueIndexes[index1] = -1;
          for (int index2 = 0; index2 < columnsList.Length; ++index2)
          {
            if (variables[index1].Name.ToUpper() == columnsList[index2].Name.ToUpper())
            {
              this.ValueIndexes[index1] = index2;
              break;
            }
          }
          this.ValueAttributeIDs[index1] = session.GetAttributeType(variables[index1].Name, true).AttributeID;
        }
      }

      /// <summary>
      /// Сортирует массив формул для того, чтобы сперва вычислялись формулы, от которых зависят другие формулы массива
      /// </summary>
      public static ColumnExpressionItem[] Sort(ColumnExpressionItem[] list)
      {
        int index1 = 0;
        while (index1 < list.Length - 1)
        {
          int[] valueAttributeIds = list[index1].ValueAttributeIDs;
          bool flag = false;
          for (int index2 = index1 + 1; index2 < list.Length; ++index2)
          {
            for (int index3 = 0; index3 < valueAttributeIds.Length; ++index3)
            {
              if (valueAttributeIds[index3] == list[index2].FormulaAttributeID)
              {
                ColumnExpressionItem columnExpressionItem = list[index2];
                list[index2] = list[index1];
                list[index1] = columnExpressionItem;
                flag = true;
                break;
              }
            }
          }
          if (!flag)
            ++index1;
        }
        return list;
      }
    }
}
