// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.XmlWriterExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

#nullable disable
namespace Intermech.Extensions;

public static class XmlWriterExtensions
{
  public static void WriteObject(
    [NotNull] this XmlWriter writer,
    [NotNull, NotWhitespace] string nodeName,
    [NotNull, ItemNotEmpty] params (string name, string value)[] attributes)
  {
    writer.WriteObject(nodeName, attributes, ((string, IXmlWriterSupport)[]) null, (string) null);
  }

  public static void WriteObject(
    [NotNull] this XmlWriter writer,
    [NotNull, NotWhitespace] string nodeName,
    [NotNull, ItemNotEmpty] params (string name, IXmlWriterSupport nestedObject)[] objects)
  {
    writer.WriteObject(nodeName, ((string, string)[]) null, objects, (string) null);
  }

  public static void WriteObject(
    [NotNull] this XmlWriter writer,
    [NotNull, NotWhitespace] string nodeName,
    [NotNull, ItemNotEmpty] params IXmlWriterSupport[] objects)
  {
    writer.WriteObject(nodeName, ((string, string)[]) null, objects.Length != 0 ? ((IEnumerable<IXmlWriterSupport>) objects).Select<IXmlWriterSupport, (string, IXmlWriterSupport)>((Func<IXmlWriterSupport, (string, IXmlWriterSupport)>) (obj => ((string) null, obj))).ToArray<(string, IXmlWriterSupport)>() : Array.Empty<(string, IXmlWriterSupport)>(), (string) null);
  }

  public static void WriteObject(
    [NotNull] this XmlWriter writer,
    [NotNull, NotWhitespace] string nodeName,
    [CanBeNull, ItemNotEmpty] (string name, string value)[] attributes,
    [NotNull, ItemNotEmpty] params (string name, IXmlWriterSupport nestedObject)[] objects)
  {
    writer.WriteObject(nodeName, attributes, objects, (string) null);
  }

  public static void WriteObject(
    [NotNull] this XmlWriter writer,
    [NotNull, NotWhitespace] string nodeName,
    [CanBeNull, ItemNotEmpty] (string name, string value)[] attributes,
    [NotNull, ItemNotEmpty] params IXmlWriterSupport[] objects)
  {
    writer.WriteObject(nodeName, attributes, objects.Length != 0 ? ((IEnumerable<IXmlWriterSupport>) objects).Select<IXmlWriterSupport, (string, IXmlWriterSupport)>((Func<IXmlWriterSupport, (string, IXmlWriterSupport)>) (obj => ((string) null, obj))).ToArray<(string, IXmlWriterSupport)>() : Array.Empty<(string, IXmlWriterSupport)>(), (string) null);
  }

  private static void WriteObject(
    [NotNull] this XmlWriter writer,
    [NotNull, NotWhitespace] string nodeName,
    [CanBeNull, ItemNotEmpty] (string name, string value)[] attributes,
    [CanBeNull, ItemNotEmpty] (string name, IXmlWriterSupport nestedObject)[] objects,
    [CanBeNull] string CDATA)
  {
    writer.WriteStartElement(nodeName);
    if (attributes != null)
    {
      foreach ((string str, string value) in attributes)
        writer.WriteAttributeString(str, value);
    }
    if (objects != null)
    {
      foreach ((string name, IXmlWriterSupport nestedObject) tuple in objects)
      {
        string name = tuple.name;
        tuple.nestedObject.WriteToXml(writer, name);
      }
    }
    if (!string.IsNullOrWhiteSpace(CDATA))
      writer.WriteCData(CDATA);
    writer.WriteEndElement();
  }
}
