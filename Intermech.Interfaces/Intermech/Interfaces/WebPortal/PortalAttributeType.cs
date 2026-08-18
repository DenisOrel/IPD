
// Type: Intermech.Interfaces.WebPortal.PortalAttributeType
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>
    /// Структура, описывающая тип атрибутов, используемый порталом
    /// </summary>
    [Serializable]
    public class PortalAttributeType
    {
      /// <summary>Идентификатор в базе портала</summary>
      public int ID;
      /// <summary>Наименование</summary>
      public string Name;
      /// <summary>Глобальный идентификатор атрибута</summary>
      public string GUID;
      /// <summary>Тип атрибутов</summary>
      public FieldTypes Type;

      /// <summary>Конструктор</summary>
      public PortalAttributeType()
      {
      }

      /// <summary>Конструктор</summary>
      public PortalAttributeType(int id, string name, string guid, FieldTypes type)
      {
        this.ID = id;
        this.Name = name;
        this.GUID = guid;
        this.Type = type;
      }

      public PortalAttributeType(string val)
      {
        string[] strArray = val.Split('|');
        this.ID = Convert.ToInt32(strArray[0]);
        this.Name = strArray[1];
        this.GUID = strArray[2];
        this.Type = (FieldTypes) Convert.ToInt32(strArray[3]);
      }

      public override string ToString() => $"{this.ID}|{this.Name}|{this.GUID}|{(int) this.Type}";
    }
}
