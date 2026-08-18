
// Type: Intermech.InvalidAreaIDException
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.Runtime.Serialization;


namespace Intermech
{
    [Serializable]
    public class InvalidAreaIDException : KernelException
    {
      private string _AreaID = "";

      public InvalidAreaIDException(string anAreaID) => this._AreaID = anAreaID;

      protected InvalidAreaIDException(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this._AreaID = info.GetString(nameof (_AreaID));
      }

      public override void GetObjectData(SerializationInfo info, StreamingContext context)
      {
        base.GetObjectData(info, context);
        info.AddValue("_AreaID", (object) this._AreaID);
      }

      public override string Message
      {
        get => string.Format(LocalizationHolder.rm.GetString("Interfaces_215"), (object) this._AreaID);
      }
    }
}
