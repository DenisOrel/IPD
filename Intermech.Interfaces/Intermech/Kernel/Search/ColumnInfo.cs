
// Type: Intermech.Kernel.Search.ColumnInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Kernel.Search
{
    /// <summary>
    /// Структура с информацией об атрибуте, которую нужно передать обрабатывающей программе.
    /// </summary>
    [Serializable]
    public struct ColumnInfo(object attributeID, AttributeSourceTypes attributeSource, object addInfo)
    {
      /// <summary>Идентификатор атрибута (число, guid, алиас или имя)</summary>
      public object AttributeID = attributeID;
      /// <summary>
      /// Чему принадлежит данный атрибут (объекту, связи, т.д.)
      /// </summary>
      public AttributeSourceTypes AttributeSource = attributeSource;
      /// <summary>
      /// Дополнительная информация по атрибуту (определяется конкретной программой)
      /// </summary>
      public object AddInfo = addInfo;
    }
}
