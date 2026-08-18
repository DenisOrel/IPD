
// Type: Intermech.Exceptions.XmlAttributeNotFoundException
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
    public class XmlAttributeNotFoundException : XmlParseException, ISerializable
    {
      [NotNull]
      [NotWhitespace]
      public string NodeName { get; }

      [NotNull]
      [NotWhitespace]
      public string AttributeName { get; }

      protected XmlAttributeNotFoundException()
      {
      }

      public XmlAttributeNotFoundException([NotNull, NotWhitespace] string nodeName, [NotNull, NotWhitespace] string attributeName)
        : base($"Attribute {attributeName} not found in {nodeName} xml node")
      {
        this.NodeName = nodeName;
        this.AttributeName = attributeName;
      }

      protected XmlAttributeNotFoundException([NotNull] SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this.NodeName = info.GetString(nameof (NodeName));
        this.AttributeName = info.GetString(nameof (AttributeName));
      }

      public override void GetObjectData([NotNull] SerializationInfo info, StreamingContext context)
      {
        base.GetObjectData(info, context);
        info.AddValue("NodeName", (object) this.NodeName);
        info.AddValue("AttributeName", (object) this.AttributeName);
      }
    }
}
