
// Type: Intermech.DBNullException
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.Runtime.Serialization;


namespace Intermech
{
    [Serializable]
    public class DBNullException : KernelException
    {
      private string _attrname;

      public DBNullException(string attrname) => this._attrname = attrname;

      protected DBNullException(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this._attrname = info.GetString(nameof (_attrname));
      }

      public override void GetObjectData(SerializationInfo info, StreamingContext context)
      {
        base.GetObjectData(info, context);
        info.AddValue("_attrname", (object) this._attrname);
      }

      public override string Message
      {
        get
        {
          return string.Format(LocalizationHolder.rm.GetString("Interfaces_218"), (object) this._attrname);
        }
      }
    }
}
