
// Type: Intermech.SysGUIDNotFoundException
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.Runtime.Serialization;


namespace Intermech
{
    [Serializable]
    public class SysGUIDNotFoundException : KernelException
    {
      private string _GuidString;

      public SysGUIDNotFoundException(string aGuidString) => this._GuidString = aGuidString;

      protected SysGUIDNotFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this._GuidString = info.GetString(nameof (_GuidString));
      }

      public override void GetObjectData(SerializationInfo info, StreamingContext context)
      {
        base.GetObjectData(info, context);
        info.AddValue("_GuidString", (object) this._GuidString);
      }

      public override string Message
      {
        get
        {
          return string.Format(LocalizationHolder.rm.GetString("Interfaces_209"), (object) this._GuidString);
        }
      }
    }
}
