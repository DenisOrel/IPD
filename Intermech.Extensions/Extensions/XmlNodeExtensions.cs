// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.XmlNodeExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using Intermech.Exceptions;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Xml;

#nullable disable
namespace Intermech.Extensions;

public static class XmlNodeExtensions
{
  [Pure]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int GetAttributeAsInt([NotNull] this XmlNode xmlNode, [NotNull, NotWhitespace] string attributeName)
  {
    if (xmlNode.Attributes == null)
      throw new XmlAttributeNotFoundException(attributeName, xmlNode.Name);
    string s = (xmlNode.Attributes[attributeName] ?? throw new XmlAttributeNotFoundException(attributeName, xmlNode.Name)).Value;
    int result;
    if (string.IsNullOrWhiteSpace(s) || !int.TryParse(s, out result))
      throw new XmlValueCantDecode(s, attributeName, xmlNode.Name, typeof (int));
    return result;
  }

  [Pure]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int GetAttributeAsIntDef(
    [NotNull] this XmlNode xmlNode,
    [NotNull, NotWhitespace] string attributeName,
    int defaultValue = 0)
  {
    if (xmlNode.Attributes == null)
      return defaultValue;
    XmlAttribute attribute = xmlNode.Attributes[attributeName];
    if (attribute == null)
      return defaultValue;
    string s = attribute.Value;
    int result;
    return string.IsNullOrWhiteSpace(s) || !int.TryParse(s, out result) ? defaultValue : result;
  }

  [Pure]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static long GetAttributeAsLong([NotNull] this XmlNode xmlNode, [NotNull, NotWhitespace] string attributeName)
  {
    if (xmlNode.Attributes == null)
      throw new XmlAttributeNotFoundException(attributeName, xmlNode.Name);
    string s = (xmlNode.Attributes[attributeName] ?? throw new XmlAttributeNotFoundException(attributeName, xmlNode.Name)).Value;
    long result;
    if (string.IsNullOrWhiteSpace(s) || !long.TryParse(s, out result))
      throw new XmlValueCantDecode(s, attributeName, xmlNode.Name, typeof (long));
    return result;
  }

  [Pure]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static long GetAttributeAsLongDef(
    [NotNull] this XmlNode xmlNode,
    [NotNull, NotWhitespace] string attributeName,
    long defaultValue = 0)
  {
    if (xmlNode.Attributes == null)
      return defaultValue;
    XmlAttribute attribute = xmlNode.Attributes[attributeName];
    if (attribute == null)
      return defaultValue;
    string s = attribute.Value;
    long result;
    return string.IsNullOrWhiteSpace(s) || !long.TryParse(s, out result) ? defaultValue : result;
  }

  [Pure]
  [NotNull]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetAttributeAsString([NotNull] this XmlNode xmlNode, [NotNull, NotWhitespace] string attributeName)
  {
    if (xmlNode.Attributes == null)
      throw new XmlAttributeNotFoundException(attributeName, xmlNode.Name);
    return (xmlNode.Attributes[attributeName] ?? throw new XmlAttributeNotFoundException(attributeName, xmlNode.Name)).Value ?? throw new XmlValueCantDecode((string) null, attributeName, xmlNode.Name, typeof (string));
  }

  [Pure]
  [NotNull]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetAttributeAsStringDef([NotNull] this XmlNode xmlNode, [NotNull, NotWhitespace] string attributeName)
  {
    return xmlNode.GetAttributeAsStringDef(attributeName, string.Empty);
  }

  [Pure]
  [NotNull]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string GetAttributeAsStringDef(
    [NotNull] this XmlNode xmlNode,
    [NotNull, NotWhitespace] string attributeName,
    [NotNull] string defaultValue)
  {
    if (xmlNode.Attributes == null)
      return defaultValue;
    XmlAttribute attribute = xmlNode.Attributes[attributeName];
    if (attribute == null)
      return defaultValue;
    string str = attribute.Value;
    return string.IsNullOrWhiteSpace(str) ? defaultValue : str;
  }

  [Pure]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static char GetAttributeAsChar([NotNull] this XmlNode xmlNode, [NotNull, NotWhitespace] string attributeName)
  {
    if (xmlNode.Attributes == null)
      throw new XmlAttributeNotFoundException(attributeName, xmlNode.Name);
    string str = (xmlNode.Attributes[attributeName] ?? throw new XmlAttributeNotFoundException(attributeName, xmlNode.Name)).Value;
    if (str == null)
      throw new XmlValueCantDecode((string) null, attributeName, xmlNode.Name, typeof (string));
    Intermech.Diagnostics.Check.Assert<XmlValueCantDecode>((str.Length == 1 ? 1 : 0) != 0, new object[3]
    {
      (object) attributeName,
      (object) xmlNode.Name,
      (object) typeof (char)
    });
    return str[0];
  }

  [Pure]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static char GetAttributeAsCharDef(
    [NotNull] this XmlNode xmlNode,
    [NotNull, NotWhitespace] string attributeName,
    char defaultValue = '\0')
  {
    if (xmlNode.Attributes == null)
      return defaultValue;
    XmlAttribute attribute = xmlNode.Attributes[attributeName];
    if (attribute == null)
      return defaultValue;
    string str = attribute.Value;
    return string.IsNullOrWhiteSpace(str) || str.Length != 1 ? defaultValue : str[0];
  }

  [Pure]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool GetAttributeAsBool([NotNull] this XmlNode xmlNode, [NotNull, NotWhitespace] string attributeName)
  {
    if (xmlNode.Attributes == null)
      throw new XmlAttributeNotFoundException(attributeName, xmlNode.Name);
    string a = (xmlNode.Attributes[attributeName] ?? throw new XmlAttributeNotFoundException(attributeName, xmlNode.Name)).Value;
    return !string.IsNullOrWhiteSpace(a) ? string.Equals(a, "True", StringComparison.InvariantCultureIgnoreCase) : throw new XmlValueCantDecode((string) null, attributeName, xmlNode.Name, typeof (string));
  }

  [Pure]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool GetAttributeAsBoolDef(
    [NotNull] this XmlNode xmlNode,
    [NotNull, NotWhitespace] string attributeName,
    bool defaultValue = false)
  {
    if (xmlNode.Attributes == null)
      return defaultValue;
    XmlAttribute attribute = xmlNode.Attributes[attributeName];
    if (attribute == null)
      return defaultValue;
    string a = attribute.Value;
    return string.IsNullOrWhiteSpace(a) ? defaultValue : string.Equals(a, "True", StringComparison.InvariantCultureIgnoreCase);
  }

  [NotNull]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static XmlElement AppendElement([NotNull] this XmlNode xmlNode, [NotNull, NotWhitespace] string childNodeName)
  {
    XmlElement element = xmlNode.OwnerDocument.CreateElement(childNodeName);
    xmlNode.AppendChild((XmlNode) element);
    return element;
  }

  [NotNull]
  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static XmlElement AppendElement(
    [NotNull] this XmlNode xmlNode,
    [NotNull, NotWhitespace] string qualifiedName,
    [NotNull, NotWhitespace] string namespaceURI)
  {
    XmlElement element = xmlNode.OwnerDocument.CreateElement(qualifiedName, namespaceURI);
    xmlNode.AppendChild((XmlNode) element);
    return element;
  }
}
