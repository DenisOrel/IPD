
// Type: Intermech.ObjectsFoundException
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Runtime.Serialization;


namespace Intermech
{
    /// <summary>
    /// Исключение, в котором кроме текста нужно показать список найденных объектов, чтобы юзер предпринял какие-то с ними действия
    /// </summary>
    [Serializable]
    public class ObjectsFoundException : AttributablesFoundException
    {
      /// <summary>Конструктор</summary>
      /// <param name="message">Сообщение об ошибке</param>
      /// <param name="objectsListName">Наименование списка найденных объектов</param>
      /// <param name="objectsID">Список идентификаторов версий найденных объектов</param>
      public ObjectsFoundException(string message, string objectsListCaption, long[] objectsID)
        : base(message, objectsListCaption, objectsID)
      {
      }

      protected ObjectsFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this._AttributablesListCaption = info.GetString(nameof (ObjectsListCaption));
        this._AttributablesID = (long[]) info.GetValue(nameof (ObjectsID), typeof (long[]));
      }

      public override void GetObjectData(SerializationInfo info, StreamingContext context)
      {
        base.GetObjectData(info, context);
        info.AddValue("ObjectsID", (object) this._AttributablesID);
        info.AddValue("ObjectsListCaption", (object) this._AttributablesListCaption);
      }

      /// <summary>Наименование списка найденных объектов</summary>
      public string ObjectsListCaption => this._AttributablesListCaption;

      /// <summary>Идентификаторы версий найденных объектов</summary>
      public long[] ObjectsID => this._AttributablesID;
    }
}
