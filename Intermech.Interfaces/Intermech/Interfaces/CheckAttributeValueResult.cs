
// Type: Intermech.Interfaces.CheckAttributeValueResult
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс для хранения результатов проверки значений атрибутов объекта/связи
    /// </summary>
    [Serializable]
    public class CheckAttributeValueResult
    {
      /// <summary>Ид. версии объекта или связи</summary>
      public long ObjectID { get; private set; }

      /// <summary>Ид. атрибута</summary>
      public int AttributeID { get; private set; }

      /// <summary>Сообщение об ошибке в значении атрибута</summary>
      public string ErrorMessage { get; private set; }

      /// <summary>
      /// Поле для хранения дополнительной информации (например, ид. объекта с таким же значением атрибута при проверке уникальности)
      /// </summary>
      public object Tag { get; private set; }

      public CheckAttributeValueResult(long objID, int attrID, string errMessage, object tag)
      {
        this.AttributeID = attrID;
        this.ObjectID = objID;
        this.ErrorMessage = errMessage;
        this.Tag = tag;
      }

      public CheckAttributeValueResult(long objID, int attrID, string errMessage)
      {
        this.AttributeID = attrID;
        this.ObjectID = objID;
        this.ErrorMessage = errMessage;
        this.Tag = (object) null;
      }
    }
}
