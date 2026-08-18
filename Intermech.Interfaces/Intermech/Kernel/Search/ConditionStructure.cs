
// Type: Intermech.Kernel.Search.ConditionStructure
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Diagnostics;


namespace Intermech.Kernel.Search
{
    /// <summary>Структура, описывающая условие поиска объектов в базе</summary>
    [Serializable]
    public struct ConditionStructure
    {
      public object Attribute;
      /// <summary>Оператор отношений</summary>
      public RelationalOperators RelationalOperator;
      /// <summary>Искомое значение</summary>
      public object Value;
      public object Value2;
      /// <summary>
      /// Логический оператор, которым это условие объединяется со следующим по списку условий
      /// </summary>
      public LogicalOperators LogicalOperator;
      /// <summary>
      /// Управляет группировкой условий.
      /// (если GroupID больше 0, то перед условием открываются GroupID скобок,
      ///  если GroupID меньше 0, то за условием закрываются GroupID скобок)
      /// </summary>
      public int GroupID;
      /// <summary>
      /// SQL-оператор (для продвинутых юзеров). Если не пустой, то Attribute игнорируется
      /// </summary>
      public string SQL;
      /// <summary>Указывает на чувствительность поиска к регистру букв</summary>
      public bool CaseSensitive;
      /// <summary>
      /// Используется для идентификации типов объектов и связей в различных условиях типа EntersIn
      /// </summary>
      public object TypeID;
      /// <summary>
      /// Указывает на источник атрибута (объект, связь или определять автоматом)
      /// </summary>
      public AttributeSourceTypes AttributeSource;
      public ColumnContents Content;
      /// <summary>Вложенные условия поиска</summary>
      public ConditionStructure[] NestedConditions;
      /// <summary>Пустая структура</summary>
      private static ConditionStructure _empty = new ConditionStructure();
      private static readonly ConditionStructure[] _emptyArray = new ConditionStructure[0];

      /// <summary>Пустая структура</summary>
      public static ConditionStructure Empty
      {
        [DebuggerStepThrough] get => ConditionStructure._empty;
      }

      public ConditionStructure(
        int attributeID,
        RelationalOperators relationalOperator,
        object conditionValue,
        LogicalOperators logicalOperator,
        int groupID,
        bool caseSensitive)
      {
        this.Attribute = (object) attributeID;
        this.RelationalOperator = relationalOperator;
        this.LogicalOperator = logicalOperator;
        this.Value = conditionValue;
        this.GroupID = groupID;
        this.SQL = "";
        this.Value2 = (object) null;
        this.CaseSensitive = caseSensitive;
        this.TypeID = (object) null;
        this.AttributeSource = AttributeSourceTypes.Auto;
        this.Content = ColumnContents.Text;
        this.NestedConditions = (ConditionStructure[]) null;
      }

      public ConditionStructure(
        int attributeID,
        RelationalOperators relationalOperator,
        object conditionValue,
        object conditionValue2,
        LogicalOperators logicalOperator,
        int groupID,
        bool caseSensitive)
      {
        this.Attribute = (object) attributeID;
        this.RelationalOperator = relationalOperator;
        this.LogicalOperator = logicalOperator;
        this.Value = conditionValue;
        this.GroupID = groupID;
        this.SQL = "";
        this.Value2 = conditionValue2;
        this.CaseSensitive = caseSensitive;
        this.TypeID = (object) null;
        this.AttributeSource = AttributeSourceTypes.Auto;
        this.Content = ColumnContents.Text;
        this.NestedConditions = (ConditionStructure[]) null;
      }

      public ConditionStructure(
        int attributeID,
        RelationalOperators relationalOperator,
        object conditionValue,
        object conditionValue2,
        LogicalOperators logicalOperator,
        int groupID,
        bool caseSensitive,
        AttributeSourceTypes attributeSource)
      {
        this.Attribute = (object) attributeID;
        this.RelationalOperator = relationalOperator;
        this.LogicalOperator = logicalOperator;
        this.Value = conditionValue;
        this.GroupID = groupID;
        this.SQL = "";
        this.Value2 = conditionValue2;
        this.CaseSensitive = caseSensitive;
        this.TypeID = (object) null;
        this.AttributeSource = attributeSource;
        this.Content = ColumnContents.Text;
        this.NestedConditions = (ConditionStructure[]) null;
      }

      public ConditionStructure(
        int attributeID,
        RelationalOperators relationalOperator,
        object conditionValue,
        object conditionValue2,
        LogicalOperators logicalOperator,
        int groupID,
        bool caseSensitive,
        AttributeSourceTypes attributeSource,
        ColumnContents content)
      {
        this.Attribute = (object) attributeID;
        this.RelationalOperator = relationalOperator;
        this.LogicalOperator = logicalOperator;
        this.Value = conditionValue;
        this.GroupID = groupID;
        this.SQL = "";
        this.Value2 = conditionValue2;
        this.CaseSensitive = caseSensitive;
        this.TypeID = (object) null;
        this.AttributeSource = attributeSource;
        this.Content = content;
        this.NestedConditions = (ConditionStructure[]) null;
      }

      public ConditionStructure(
        string attributeName,
        RelationalOperators relationalOperator,
        object conditionValue,
        LogicalOperators logicalOperator,
        int groupID,
        bool caseSensitive)
      {
        this.Attribute = (object) attributeName;
        this.RelationalOperator = relationalOperator;
        this.LogicalOperator = logicalOperator;
        this.Value = conditionValue;
        this.GroupID = groupID;
        this.SQL = "";
        this.Value2 = (object) null;
        this.CaseSensitive = caseSensitive;
        this.TypeID = (object) null;
        this.AttributeSource = AttributeSourceTypes.Auto;
        this.Content = ColumnContents.Text;
        this.NestedConditions = (ConditionStructure[]) null;
      }

      public ConditionStructure(
        string attributeName,
        RelationalOperators relationalOperator,
        object conditionValue,
        object conditionValue2,
        LogicalOperators logicalOperator,
        int groupID,
        bool caseSensitive)
      {
        this.Attribute = (object) attributeName;
        this.RelationalOperator = relationalOperator;
        this.LogicalOperator = logicalOperator;
        this.Value = conditionValue;
        this.GroupID = groupID;
        this.SQL = "";
        this.Value2 = conditionValue2;
        this.CaseSensitive = caseSensitive;
        this.TypeID = (object) null;
        this.AttributeSource = AttributeSourceTypes.Auto;
        this.Content = ColumnContents.Text;
        this.NestedConditions = (ConditionStructure[]) null;
      }

      public ConditionStructure(
        Guid attributeGuid,
        RelationalOperators relationalOperator,
        object conditionValue,
        LogicalOperators logicalOperator,
        int groupID)
      {
        this.RelationalOperator = relationalOperator;
        this.LogicalOperator = logicalOperator;
        this.Value = conditionValue;
        this.GroupID = groupID;
        this.SQL = "";
        this.Attribute = (object) attributeGuid;
        this.Value2 = (object) null;
        this.CaseSensitive = true;
        this.TypeID = (object) null;
        this.AttributeSource = AttributeSourceTypes.Auto;
        this.Content = ColumnContents.Text;
        this.NestedConditions = (ConditionStructure[]) null;
      }

      public ConditionStructure(
        string sql,
        LogicalOperators logicalOperator,
        int groupID,
        object conditionValue)
      {
        this.Attribute = (object) 0;
        this.RelationalOperator = RelationalOperators.None;
        this.LogicalOperator = logicalOperator;
        this.Value = conditionValue;
        this.GroupID = groupID;
        this.SQL = sql;
        this.Value2 = (object) null;
        this.CaseSensitive = true;
        this.TypeID = (object) null;
        this.AttributeSource = AttributeSourceTypes.Auto;
        this.Content = ColumnContents.Text;
        this.NestedConditions = (ConditionStructure[]) null;
      }

      public ConditionStructure(string sql, params DBDataParam[] values)
      {
        this.Attribute = (object) 0;
        this.RelationalOperator = RelationalOperators.Equal;
        this.LogicalOperator = LogicalOperators.NONE;
        this.Value = (object) new ConditionFormula(sql, values);
        this.GroupID = 0;
        this.SQL = string.Empty;
        this.Value2 = (object) null;
        this.CaseSensitive = true;
        this.TypeID = (object) null;
        this.AttributeSource = AttributeSourceTypes.Auto;
        this.Content = ColumnContents.Text;
        this.NestedConditions = (ConditionStructure[]) null;
      }

      /// <summary>
      /// Объединяет указанное новое условие с массивом существующих условий.
      /// Этот метод может использоваться в производных классах для сцепления
      /// добавляемых ими условий с условиями, предоставляемыми базовым классом.
      /// </summary>
      /// <param name="joinedCondition">Новое условие</param>
      /// <param name="existingConditions">Массив существующих условий</param>
      /// <returns>Объединённые условия</returns>
      public static ConditionStructure[] Join(
        ConditionStructure joinedCondition,
        ConditionStructure[] existingConditions)
      {
        if (existingConditions == null)
          return new ConditionStructure[1]{ joinedCondition };
        ConditionStructure[] conditionStructureArray = new ConditionStructure[existingConditions.Length + 1];
        conditionStructureArray[0] = joinedCondition;
        conditionStructureArray[0].LogicalOperator = LogicalOperators.AND;
        existingConditions.CopyTo((Array) conditionStructureArray, 1);
        return conditionStructureArray;
      }

      /// <summary>
      /// Объединяет указанный массив новых условий с массивом существующих
      /// условий. Этот метод может использоваться в производных классах для
      /// сцепления добавляемых ими условий с условиями, предоставляемыми базовым
      /// классом.
      /// </summary>
      /// <param name="joinedConditions">Массив новых условий</param>
      /// <param name="existingConditions">Массив существующих условий</param>
      /// <returns>Объединённые условия</returns>
      public static ConditionStructure[] Join(
        ConditionStructure[] joinedConditions,
        ConditionStructure[] existingConditions)
      {
        if (existingConditions == null)
          return joinedConditions;
        if (joinedConditions == null)
          return existingConditions;
        ConditionStructure[] conditionStructureArray = new ConditionStructure[joinedConditions.Length + existingConditions.Length];
        joinedConditions.CopyTo((Array) conditionStructureArray, 0);
        if (joinedConditions.Length != 0)
          conditionStructureArray[joinedConditions.Length - 1].LogicalOperator = LogicalOperators.AND;
        existingConditions.CopyTo((Array) conditionStructureArray, joinedConditions.Length);
        return conditionStructureArray;
      }

      public bool EqualsWithValues(ConditionStructure obj)
      {
        if (!this.EqualsWithoutValues(obj, true) || (this.Value != null ? (obj.Value != null ? (this.Value.Equals(obj.Value) ? 1 : 0) : 0) : (obj.Value == null ? 1 : 0)) == 0)
          return false;
        if (this.Value2 == null)
          return obj.Value2 == null;
        return obj.Value2 != null && this.Value2.Equals(obj.Value2);
      }

      public bool EqualsWithoutValues(ConditionStructure obj, bool nestedConditionsWithValues = false)
      {
        return (this.Attribute == null || obj.Attribute != null) && (this.Attribute != null || obj.Attribute == null) && (this.Attribute != null && this.Attribute.Equals(obj.Attribute) || this.Attribute == null) && this.AttributeSource == obj.AttributeSource && this.CaseSensitive == obj.CaseSensitive && this.Content == obj.Content && this.GroupID == obj.GroupID && this.LogicalOperator == obj.LogicalOperator && this.RelationalOperator == obj.RelationalOperator && this.SQL == obj.SQL && this.TypeID == obj.TypeID && ConditionStructure.Equals(this.NestedConditions, obj.NestedConditions, nestedConditionsWithValues);
      }

      public static bool Equals(ConditionStructure[] cs1, ConditionStructure[] cs2, bool withValues = false)
      {
        if (cs1 == null && cs2 == null)
          return true;
        if (cs1 == null && cs2 != null || cs1 != null && cs2 == null || cs1.Length != cs2.Length)
          return false;
        for (int index = 0; index < cs1.Length; ++index)
        {
          if (withValues)
          {
            if (!cs1[index].EqualsWithValues(cs2[index]))
              return false;
          }
          else if (!cs1[index].EqualsWithoutValues(cs2[index]))
            return false;
        }
        return true;
      }

      public ConditionStructure Clone()
      {
        ConditionStructure conditionStructure = new ConditionStructure()
        {
          Attribute = this.Attribute,
          AttributeSource = this.AttributeSource,
          CaseSensitive = this.CaseSensitive,
          Content = this.Content,
          GroupID = this.GroupID,
          LogicalOperator = this.LogicalOperator,
          RelationalOperator = this.RelationalOperator,
          SQL = this.SQL,
          TypeID = this.TypeID,
          Value = this.Value == null || !(this.Value is ICloneable) ? this.Value : ((ICloneable) this.Value).Clone(),
          Value2 = this.Value2 == null || !(this.Value2 is ICloneable) ? this.Value2 : ((ICloneable) this.Value2).Clone()
        };
        if (this.NestedConditions != null && this.NestedConditions.Length != 0)
        {
          conditionStructure.NestedConditions = new ConditionStructure[this.NestedConditions.Length];
          for (int index = 0; index < this.NestedConditions.Length; ++index)
            conditionStructure.NestedConditions[index] = this.NestedConditions[index].Clone();
        }
        return conditionStructure;
      }

      /// <summary>В целях упрощения синтасиса создания DBRecordSetParams с единственным условием или вообще без условний (ConditionStructure.Empty)</summary>
      public static implicit operator ConditionStructure[](ConditionStructure condition)
      {
        if (condition.Attribute == null)
          return ConditionStructure._emptyArray;
        return new ConditionStructure[1]{ condition };
      }

      /// <summary>Метода обнуляет поля структуры</summary>
      public void Clear()
      {
        this.Attribute = (object) null;
        this.RelationalOperator = RelationalOperators.None;
        this.LogicalOperator = LogicalOperators.NONE;
        this.Value = (object) null;
        this.GroupID = 0;
        this.SQL = string.Empty;
        this.Value2 = (object) null;
        this.CaseSensitive = false;
        this.TypeID = (object) null;
        this.AttributeSource = AttributeSourceTypes.Auto;
        this.Content = ColumnContents.Text;
        this.NestedConditions = (ConditionStructure[]) null;
      }
    }
}
