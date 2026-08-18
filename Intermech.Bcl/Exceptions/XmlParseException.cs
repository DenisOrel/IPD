
// Type: Intermech.Exceptions.XmlParseException
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Diagnostics;
using System;
using System.Runtime.Serialization;


namespace Intermech.Exceptions
{
    [Serializable]
    public class XmlParseException : XmlException, ISerializable
    {
      public XmlParseException()
      {
      }

      public XmlParseException([CanBeNull, CanBeEmpty] string message)
        : base(message)
      {
      }

      public XmlParseException([CanBeNull, CanBeEmpty] string message, [CanBeNull] Exception innerException)
        : base(message, innerException)
      {
      }

      protected XmlParseException([NotNull] SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
      }
    }
}
