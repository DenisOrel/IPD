
// Type: Intermech.ObjectAlreadyCommited
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.Runtime.Serialization;


namespace Intermech
{
    [Serializable]
    public class ObjectAlreadyCommited : KernelException
    {
      private long _ObjectID;

      public ObjectAlreadyCommited(long aObjectID)
      {
        this._ObjectID = aObjectID;
        this.WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(aObjectID));
      }

      protected ObjectAlreadyCommited(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this._ObjectID = info.GetInt64(nameof (_ObjectID));
      }

      public override void GetObjectData(SerializationInfo info, StreamingContext context)
      {
        base.GetObjectData(info, context);
        info.AddValue("_ObjectID", this._ObjectID);
      }

      public override string Message
      {
        get
        {
          return string.Format(LocalizationHolder.rm.GetString("Interfaces_212"), (object) this._ObjectID);
        }
      }
    }
}
