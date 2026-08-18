
// Type: Intermech.Kernel.Search.ConditionGroupIDReplacer
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.ComponentModel;


namespace Intermech.Kernel.Search
{
    /// <summary>
    /// Объект, указывающий ядру на необходимость преобразования ид. группы/орг. единицы в идентификаторы входящих в них пользователей
    /// </summary>
    [TypeConverter(typeof (ToBase64StringTypeConverter<ConditionGroupIDReplacer>))]
    [Serializable]
    public class ConditionGroupIDReplacer
    {
      /// <summary>Ид. группы/орг. единицы</summary>
      public long GroupID { get; private set; }

      /// <summary>
      /// Нужно ли включать в список идентификаторов ид. групп/орг. единиц
      /// </summary>
      public bool IncludeGroupsID { get; private set; }

      public ConditionGroupIDReplacer(long groupID, bool incGroupsID)
      {
        this.GroupID = groupID;
        this.IncludeGroupsID = incGroupsID;
      }
    }
}
