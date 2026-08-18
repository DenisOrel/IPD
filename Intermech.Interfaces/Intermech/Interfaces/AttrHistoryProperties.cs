
// Type: Intermech.Interfaces.AttrHistoryProperties
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс для хранения информации по истории значений атрибутов
    /// </summary>
    public class AttrHistoryProperties
    {
      /// <summary>Ид. атрибута</summary>
      public int AttributeID;
      /// <summary>Ид. типа объектов (если атрибут объектов, иначе -1)</summary>
      public int ObjectType;
      /// <summary>Ид. типа связей (если атрибут связи, иначе -1)</summary>
      public int RelationType;
      /// <summary>Ид. юзера, который присвоил значение</summary>
      public long UserID;
      /// <summary>Дата и время присвоения значения в UTC</summary>
      public DateTime SetDate;
      /// <summary>Ид. объекта/связи, которому меняют значение</summary>
      public long ID;
      /// <summary>Целочисленное значение</summary>
      public object IntValue;
      /// <summary>Строковая часть значения</summary>
      public object StrValue;
      /// <summary>Значение Дата и время</summary>
      public object DateValue;
      /// <summary>Вещественная честь значения</summary>
      public object DoubleValue;

      public AttrHistoryProperties(
        int attributeID,
        int objectType,
        int relationType,
        long userID,
        DateTime setDate,
        long id,
        object intValue,
        object strValue,
        object dateValue,
        object doubleValue)
      {
        this.AttributeID = attributeID;
        this.ObjectType = objectType;
        this.RelationType = relationType;
        this.UserID = userID;
        this.SetDate = setDate;
        this.ID = id;
        this.IntValue = intValue;
        this.StrValue = strValue;
        this.DateValue = dateValue;
        this.DoubleValue = doubleValue;
      }
    }
}
