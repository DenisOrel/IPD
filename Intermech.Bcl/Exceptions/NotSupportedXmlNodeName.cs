
// Type: Intermech.Exceptions.NotSupportedXmlNodeName
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
    public class NotSupportedXmlNodeName : XmlParseException, ISerializable
    {
      protected NotSupportedXmlNodeName()
      {
      }

      [NotNull]
      [NotEmpty]
      private static string GetMessage([NotNull, NotWhitespace] string nodeName, [CanBeNull] string ownerNodeName = null)
      {
        return !string.IsNullOrWhiteSpace(ownerNodeName) ? $"Node {nodeName} not supported inside node {ownerNodeName}" : $"Node {nodeName} not supported here";
      }

      public NotSupportedXmlNodeName([NotNull, NotWhitespace] string nodeName, [CanBeNull] string ownerNodeName = null)
        : base(NotSupportedXmlNodeName.GetMessage(Intermech.Diagnostics.Check.NotNullOrWhitespace(nodeName, nameof (nodeName)), ownerNodeName))
      {
      }

      protected NotSupportedXmlNodeName([NotNull] SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
      }
    }
}
