// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.XmlReaderExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using Intermech.Exceptions;
using System;
using System.Runtime.CompilerServices;
using System.Xml;

#nullable disable
namespace Intermech.Extensions;

public static class XmlReaderExtensions
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static bool ReadObjectInternal(
    [NotNull] this XmlReader reader,
    [CanBeNull] string nodeNameToRead,
    [NotNull] XmlReaderExtensions.LoadObjectPropertiesMethod loadObjectPropertiesMethod,
    bool throwExceptionIfNotFound = true,
    XmlReaderExtensions.ParseItem parseItems = XmlReaderExtensions.ParseItem.All)
  {
    XmlNodeType nodeType = reader.NodeType;
    string localName1 = reader.LocalName;
    while (true)
    {
      if (nodeType == XmlNodeType.Element)
      {
        if (nodeNameToRead != null)
        {
          if (!string.Equals(localName1, nodeNameToRead, StringComparison.InvariantCulture))
          {
            reader.Skip();
            if (reader.EOF)
              goto label_6;
          }
          else
            goto label_12;
        }
        else
          break;
      }
      else if (!reader.Read())
        goto label_8;
      nodeType = reader.NodeType;
      localName1 = reader.LocalName;
    }
    nodeNameToRead = localName1;
    goto label_12;
label_6:
    throw new XmlParseException($"Xml element \"{nodeNameToRead}\" not found!");
label_8:
    if (throwExceptionIfNotFound)
      throw new XmlParseException($"Xml element \"{nodeNameToRead}\" not found!");
    return false;
label_12:
    if (parseItems.HasFlag((Enum) XmlReaderExtensions.ParseItem.Attributes) && reader.HasAttributes && reader.MoveToFirstAttribute())
    {
      do
      {
        int num = loadObjectPropertiesMethod(reader.LocalName, reader.Value) ? 1 : 0;
      }
      while (reader.MoveToNextAttribute());
      reader.MoveToElement();
    }
    if (reader.IsEmptyElement)
    {
      reader.Read();
      return true;
    }
    XmlNodeType xmlNodeType = reader.Read() ? reader.NodeType : throw new XmlParseException($"End of xml element \"{nodeNameToRead}\" not found!");
    string localName2 = reader.LocalName;
    while (true)
    {
      switch (xmlNodeType)
      {
        case XmlNodeType.Element:
          if (parseItems.HasFlag((Enum) XmlReaderExtensions.ParseItem.SubItems) && loadObjectPropertiesMethod(reader.LocalName, (string) null))
          {
            if (!reader.EOF)
            {
              if (reader.NodeType == XmlNodeType.EndElement && string.Equals(reader.LocalName, localName2, StringComparison.InvariantCulture) && !reader.Read())
                goto label_29;
            }
            else
              goto label_27;
          }
          else
          {
            reader.Skip();
            if (reader.EOF)
              goto label_31;
          }
          xmlNodeType = reader.NodeType;
          localName2 = reader.LocalName;
          continue;
        case XmlNodeType.Text:
          if (parseItems.HasFlag((Enum) XmlReaderExtensions.ParseItem.Text))
          {
            int num = loadObjectPropertiesMethod("Text", reader.Value) ? 1 : 0;
            break;
          }
          break;
        case XmlNodeType.CDATA:
          if (parseItems.HasFlag((Enum) XmlReaderExtensions.ParseItem.Cdata))
          {
            int num = loadObjectPropertiesMethod("CDATA", reader.Value) ? 1 : 0;
            break;
          }
          break;
        case XmlNodeType.EndElement:
          if (!string.Equals(localName2, nodeNameToRead, StringComparison.InvariantCulture))
            break;
          goto label_34;
      }
      if (reader.Read())
      {
        xmlNodeType = reader.NodeType;
        localName2 = reader.LocalName;
      }
      else
        goto label_36;
    }
label_27:
    throw new XmlParseException($"End of xml element \"{nodeNameToRead}\" not found!");
label_29:
    throw new XmlParseException($"End of xml element \"{nodeNameToRead}\" not found!");
label_31:
    throw new XmlParseException($"End of xml element \"{nodeNameToRead}\" not found!");
label_34:
    reader.ReadEndElement();
    return true;
label_36:
    throw new XmlParseException($"End of xml element \"{nodeNameToRead}\" not found!");
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool ReadObject(
    [NotNull] this XmlReader reader,
    [NotNull, NotWhitespace] string nodeNameToRead,
    [NotNull] XmlReaderExtensions.LoadObjectPropertiesMethod loadObjectPropertiesMethod,
    bool throwExceptionIfNotFound = true)
  {
    return reader.ReadObjectInternal(nodeNameToRead, loadObjectPropertiesMethod, throwExceptionIfNotFound);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool ReadObject(
    [NotNull] this XmlReader reader,
    [NotNull] XmlReaderExtensions.LoadObjectPropertiesMethod loadObjectPropertiesMethod,
    bool throwExceptionIfNotFound = true)
  {
    return reader.ReadObjectInternal((string) null, loadObjectPropertiesMethod, throwExceptionIfNotFound);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool ReadObject(
    [NotNull] this XmlReader reader,
    [NotNull] XmlReaderExtensions.LoadObjectPropertiesFromReaderMethod loadObjectPropertiesMethod,
    bool throwExceptionIfNotFound = true)
  {
    return reader.ReadObjectInternal((string) null, (XmlReaderExtensions.LoadObjectPropertiesMethod) ((name, value) => loadObjectPropertiesMethod(reader, name, value)), throwExceptionIfNotFound);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool ReadObject(
    [NotNull] this XmlReader reader,
    [NotNull, NotWhitespace] string nodeNameToRead,
    [NotNull] XmlReaderExtensions.LoadObjectPropertiesFromReaderMethod loadObjectPropertiesMethod,
    bool throwExceptionIfNotFound = true)
  {
    return reader.ReadObjectInternal(nodeNameToRead, (XmlReaderExtensions.LoadObjectPropertiesMethod) ((name, value) => loadObjectPropertiesMethod(reader, name, value)), throwExceptionIfNotFound);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool ReadEnumeration(
    [NotNull] this XmlReader reader,
    [NotNull] XmlReaderExtensions.LoadEnumerationItemMethod loadEnumerationItemMethod,
    bool throwExceptionIfNotFound = true)
  {
    return reader.ReadObjectInternal((string) null, (XmlReaderExtensions.LoadObjectPropertiesMethod) ((name, value) =>
    {
      loadEnumerationItemMethod();
      return true;
    }), throwExceptionIfNotFound, XmlReaderExtensions.ParseItem.SubItems);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool ReadEnumeration(
    [NotNull] this XmlReader reader,
    [NotNull, NotWhitespace] string itemNodeName,
    [NotNull] XmlReaderExtensions.LoadEnumerationItemMethod loadEnumerationItemMethod,
    bool throwExceptionIfNotFound = true)
  {
    return reader.ReadObjectInternal((string) null, (XmlReaderExtensions.LoadObjectPropertiesMethod) ((name, value) =>
    {
      loadEnumerationItemMethod();
      return true;
    }), throwExceptionIfNotFound, XmlReaderExtensions.ParseItem.SubItems);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool ReadEnumeration(
    [NotNull] this XmlReader reader,
    [NotNull, NotWhitespace] string nodeNameToRead,
    [CanBeNull, NotWhitespace] string itemNodeName,
    [NotNull] XmlReaderExtensions.LoadEnumerationItemMethod loadEnumerationItemMethod,
    bool throwExceptionIfNotFound = true)
  {
    return reader.ReadObjectInternal(nodeNameToRead, (XmlReaderExtensions.LoadObjectPropertiesMethod) ((name, value) =>
    {
      loadEnumerationItemMethod();
      return true;
    }), throwExceptionIfNotFound, XmlReaderExtensions.ParseItem.SubItems);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool ReadObjectsEnumeration(
    [NotNull] this XmlReader reader,
    [NotNull, NotWhitespace] string itemNodeName,
    [NotNull] XmlReaderExtensions.LoadNamedEnumerationItemMethod loadNamedEnumerationItemMethod,
    bool throwExceptionIfNotFound = true)
  {
    return reader.ReadObjectInternal((string) null, (XmlReaderExtensions.LoadObjectPropertiesMethod) ((name, value) => loadNamedEnumerationItemMethod(reader, name)), throwExceptionIfNotFound, XmlReaderExtensions.ParseItem.SubItems);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool ReadObjectsEnumeration(
    [NotNull] this XmlReader reader,
    [NotNull, NotWhitespace] string nodeNameToRead,
    [NotNull, NotWhitespace] string itemNodeName,
    [NotNull] XmlReaderExtensions.LoadNamedEnumerationItemMethod loadNamedEnumerationItemMethod,
    bool throwExceptionIfNotFound = true)
  {
    return reader.ReadObjectInternal(nodeNameToRead, (XmlReaderExtensions.LoadObjectPropertiesMethod) ((name, value) => loadNamedEnumerationItemMethod(reader, name)), throwExceptionIfNotFound, XmlReaderExtensions.ParseItem.SubItems);
  }

  [Flags]
  public enum ParseItem
  {
    None = 0,
    Attributes = 1,
    SubItems = 2,
    Text = 4,
    Cdata = 8,
    All = Cdata | Text | SubItems | Attributes, // 0x0000000F
  }

  public delegate bool LoadObjectPropertiesMethod([NotNull, NotWhitespace] string name, [CanBeNull] string value);

  public delegate bool LoadObjectPropertiesFromReaderMethod(
    [NotNull] XmlReader reader,
    [NotNull, NotWhitespace] string name,
    [CanBeNull] string value);

  public delegate void LoadEnumerationItemMethod();

  public delegate bool LoadNamedEnumerationItemMethod([NotNull] XmlReader reader, [NotNull, NotWhitespace] string name);
}
