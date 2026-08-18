
// Type: Intermech.AccessDeniedException
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Runtime.Serialization;


namespace Intermech
{
    [Serializable]
    public class AccessDeniedException : KernelException
    {
      public string[] LogList;

      public override string Message => LocalizationHolder.rm.GetString("Interfaces_208");

      public AccessDeniedException(IUserSession session)
      {
        this.LogList = session.GetCheckAccessLog(GetAccessModes.LastNRecs);
      }

      protected AccessDeniedException(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this.LogList = (string[]) info.GetValue(nameof (LogList), typeof (string[]));
      }

      public override void GetObjectData(SerializationInfo info, StreamingContext context)
      {
        base.GetObjectData(info, context);
        info.AddValue("LogList", (object) this.LogList);
      }
    }
}
