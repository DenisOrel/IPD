
// Type: Intermech.RelationsFoundException
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Runtime.Serialization;


namespace Intermech
{
    /// <summary>
    /// Исключение, в котором кроме текста нужно показать список найденных связей, чтобы юзер предпринял какие-то с ними действия
    /// </summary>
    [Serializable]
    public class RelationsFoundException : AttributablesFoundException
    {
      /// <summary>Конструктор</summary>
      /// <param name="message">Сообщение об ошибке</param>
      /// <param name="objectsListName">Наименование списка найденных связей</param>
      /// <param name="objectsID">Список идентификаторов найденных связей</param>
      public RelationsFoundException(string message, string relationsListCaption, long[] relationsID)
        : base(message, relationsListCaption, relationsID)
      {
      }

      protected RelationsFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this._AttributablesListCaption = info.GetString(nameof (RelationsListCaption));
        this._AttributablesID = (long[]) info.GetValue(nameof (RelationsID), typeof (long[]));
      }

      public override void GetObjectData(SerializationInfo info, StreamingContext context)
      {
        base.GetObjectData(info, context);
        info.AddValue("RelationsID", (object) this._AttributablesID);
        info.AddValue("RelationsListCaption", (object) this._AttributablesListCaption);
      }

      /// <summary>Наименование списка найденных связей</summary>
      public string RelationsListCaption => this._AttributablesListCaption;

      /// <summary>Идентификаторы найденных связей</summary>
      public long[] RelationsID => this._AttributablesID;
    }
}
