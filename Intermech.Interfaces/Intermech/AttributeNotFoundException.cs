
// Type: Intermech.AttributeNotFoundException
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.Runtime.Serialization;


namespace Intermech
{
    [Serializable]
    public class AttributeNotFoundException : KernelException
    {
      private string _AttributeName = "";
      private string _GUID = "";
      private int _AttributeID;
      private long _ObjectID;

      public AttributeNotFoundException(string AttributeName, string aGUID, long anObjectID)
      {
        this._AttributeName = AttributeName;
        this._GUID = aGUID;
        this._ObjectID = anObjectID;
        this.WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(anObjectID));
      }

      public AttributeNotFoundException(int AttributeID, long anObjectID)
      {
        this._AttributeID = AttributeID;
        this._ObjectID = anObjectID;
        this.WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(anObjectID));
      }

      protected AttributeNotFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this._AttributeName = info.GetString(nameof (_AttributeName));
        this._GUID = info.GetString(nameof (_GUID));
        this._AttributeID = info.GetInt32(nameof (_AttributeID));
        this._ObjectID = info.GetInt64(nameof (_ObjectID));
      }

      public override void GetObjectData(SerializationInfo info, StreamingContext context)
      {
        base.GetObjectData(info, context);
        info.AddValue("_AttributeName", (object) this._AttributeName);
        info.AddValue("_GUID", (object) this._GUID);
        info.AddValue("_AttributeID", this._AttributeID);
        info.AddValue("_ObjectID", this._ObjectID);
      }

      public override string Message
      {
        get
        {
          string message = LocalizationHolder.rm.GetString("Interfaces_203");
          if (this._AttributeName != "")
            message = string.Format(LocalizationHolder.rm.GetString("Interfaces_204"), (object) this._AttributeName, (object) this._ObjectID);
          else if (this._GUID != "")
            message = string.Format(LocalizationHolder.rm.GetString("Interfaces_205"), (object) this._GUID, (object) this._ObjectID);
          else if (this._AttributeID != 0)
            message = string.Format(LocalizationHolder.rm.GetString("Interfaces_206"), (object) this._AttributeID, (object) this._ObjectID);
          return message;
        }
      }
    }
}
