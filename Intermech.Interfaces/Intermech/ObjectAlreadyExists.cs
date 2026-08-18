
// Type: Intermech.ObjectAlreadyExists
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.Runtime.Serialization;


namespace Intermech
{
    /// <summary>
    /// Исключение возникает при попытке присвоить атрибуту объекта неуникальное значение.
    /// </summary>
    [Serializable]
    public class ObjectAlreadyExists : KernelException
    {
      private long _ObjectID;
      private string _AttributeName;
      private string _ObjectCaption;
      private string _dopInfo;

      public ObjectAlreadyExists(
        long aObjectID,
        string attributeName,
        string objectCaption,
        string dopInfo)
      {
        this._ObjectID = aObjectID;
        this._AttributeName = attributeName;
        this._ObjectCaption = objectCaption;
        this._dopInfo = dopInfo;
        this.WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(aObjectID));
      }

      protected ObjectAlreadyExists(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this._ObjectID = info.GetInt64(nameof (_ObjectID));
        this._ObjectCaption = info.GetString(nameof (_ObjectCaption));
        this._AttributeName = info.GetString(nameof (_AttributeName));
        this._dopInfo = info.GetString(nameof (_dopInfo));
      }

      public override void GetObjectData(SerializationInfo info, StreamingContext context)
      {
        base.GetObjectData(info, context);
        info.AddValue("_ObjectID", this._ObjectID);
        info.AddValue("_ObjectCaption", (object) this._ObjectCaption);
        info.AddValue("_AttributeName", (object) this._AttributeName);
        info.AddValue("_dopInfo", (object) this._dopInfo);
      }

      /// <summary>
      /// Ид. версии объекта, у которой уже есть данное значение атрибута
      /// </summary>
      public long ObjectID => this._ObjectID;

      /// <summary>Имя атрибута, чья уникальность была нарушена</summary>
      public string AttributeName => this._AttributeName;

      public override string Message
      {
        get
        {
          return string.Format(LocalizationHolder.rm.GetString("Interfaces_213"), (object) this._AttributeName, (object) this._ObjectCaption, (object) this._ObjectID) + this._dopInfo;
        }
      }
    }
}
