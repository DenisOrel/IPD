
// Type: Intermech.TypeConversionException
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.Runtime.Serialization;


namespace Intermech
{
    [Serializable]
    public class TypeConversionException : KernelException
    {
      private string _TargetName;
      private string _SourceName;

      public TypeConversionException(string targetName, string sourceName)
      {
        this._TargetName = targetName;
        this._SourceName = sourceName;
      }

      protected TypeConversionException(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this._TargetName = info.GetString(nameof (_TargetName));
        this._SourceName = info.GetString(nameof (_SourceName));
      }

      public override void GetObjectData(SerializationInfo info, StreamingContext context)
      {
        base.GetObjectData(info, context);
        info.AddValue("_TargetName", (object) this._TargetName);
        info.AddValue("_SourceName", (object) this._SourceName);
      }

      public override string Message
      {
        get
        {
          return string.Format(LocalizationHolder.rm.GetString("Interfaces_210"), (object) this._TargetName, (object) this._SourceName);
        }
      }
    }
}
