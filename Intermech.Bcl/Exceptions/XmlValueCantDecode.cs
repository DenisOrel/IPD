
// Type: Intermech.Exceptions.XmlValueCantDecode
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
    public class XmlValueCantDecode : XmlParseException, ISerializable
    {
      [CanBeNull]
      [CanBeEmpty]
      public string Value { get; }

      [NotNull]
      [NotWhitespace]
      public string AttributeName { get; }

      [NotNull]
      [NotWhitespace]
      public string NodeName { get; }

      [NotNull]
      [NotWhitespace]
      public string TargetTypeName { get; }

      protected XmlValueCantDecode()
      {
      }

      public XmlValueCantDecode([CanBeNull, CanBeEmpty] string value, [NotNull, NotWhitespace] string attributeName, [NotNull, NotWhitespace] string nodeName, [NotNull] Type type)
        : base($"Value {XmlValueCantDecode.GetValueStr(value)} of attribute {Intermech.Diagnostics.Check.ArgumentNotNull(attributeName, nameof (attributeName))} xml node {Intermech.Diagnostics.Check.ArgumentNotNull(nodeName, nameof (nodeName))} can not be casted to {Intermech.Diagnostics.Check.ArgumentNotNull(type, nameof (type)).Name}")
      {
        this.Value = value;
        this.AttributeName = attributeName;
        this.NodeName = nodeName;
        this.TargetTypeName = type.Name;
      }

      [NotNull]
      private static string GetValueStr([CanBeNull, CanBeEmpty] string value) => value ?? "null";

      protected XmlValueCantDecode([NotNull] SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this.Value = info.GetString(nameof (Value));
        this.AttributeName = info.GetString(nameof (AttributeName));
        this.NodeName = info.GetString(nameof (NodeName));
        this.TargetTypeName = info.GetString(nameof (TargetTypeName));
      }

      public override void GetObjectData([NotNull] SerializationInfo info, StreamingContext context)
      {
        base.GetObjectData(info, context);
        info.AddValue("Value", (object) this.Value);
        info.AddValue("AttributeName", (object) this.AttributeName);
        info.AddValue("NodeName", (object) this.NodeName);
        info.AddValue("TargetTypeName", (object) this.TargetTypeName);
      }
    }
}
