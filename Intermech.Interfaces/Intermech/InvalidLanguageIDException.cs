
// Type: Intermech.InvalidLanguageIDException
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.Runtime.Serialization;


namespace Intermech
{
    [Serializable]
    public class InvalidLanguageIDException : KernelException
    {
      private string _LanguageID = "";

      public InvalidLanguageIDException(string aLanguageID) => this._LanguageID = aLanguageID;

      protected InvalidLanguageIDException(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this._LanguageID = info.GetString(nameof (_LanguageID));
      }

      public override void GetObjectData(SerializationInfo info, StreamingContext context)
      {
        base.GetObjectData(info, context);
        info.AddValue("_LanguageID", (object) this._LanguageID);
      }

      public override string Message
      {
        get
        {
          return string.Format(LocalizationHolder.rm.GetString("Interfaces_216"), (object) this._LanguageID);
        }
      }
    }
}
