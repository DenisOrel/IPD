
// Type: Intermech.Mvp.MvpException
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Runtime.Serialization;


namespace Intermech.Mvp
{
    [Serializable]
    public class MvpException : Exception
    {
      public MvpException(string message)
        : base(message)
      {
      }

      protected MvpException(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
      }
    }
}
