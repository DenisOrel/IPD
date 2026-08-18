
// Type: Intermech.Interfaces.WebPortal.AttributeInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>Структура с главной инфой по атрибуту объекта</summary>
    [Serializable]
    public class AttributeInfo
    {
      /// <summary>Глобальный идентификатор атрибута</summary>
      public string Guid;
      /// <summary>Имя атрибута</summary>
      public string Name;
      /// <summary>Краткое имя</summary>
      public string ShortName;
      /// <summary>Алиас атрибута</summary>
      public string Alias;
      /// <summary>Тип данных</summary>
      public FieldTypes FieldType;

      public AttributeInfo()
      {
        this.Guid = string.Empty;
        this.Name = string.Empty;
        this.ShortName = string.Empty;
        this.Alias = string.Empty;
        this.FieldType = FieldTypes.ftUnknown;
      }

      public AttributeInfo(string name)
      {
        this.Guid = string.Empty;
        this.Name = name;
        this.ShortName = string.Empty;
        this.Alias = string.Empty;
        this.FieldType = FieldTypes.ftUnknown;
      }

      public AttributeInfo(string guid, string name, string shortName, string alias, FieldTypes type)
      {
        this.Guid = guid;
        this.Name = name;
        this.ShortName = shortName;
        this.Alias = alias;
        this.FieldType = type;
      }
    }
}
