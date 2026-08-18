// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Xmp.XmpUtils
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Globalization;
using System.Xml;

#nullable disable
namespace Syncfusion.Pdf.Xmp;

internal class XmpUtils
{
  private const string c_dateFormat = "yyyy-MM-dd'T'HH:mm:ss.ffzzz";
  private const string c_False = "False";
  private const string c_realPattern = "^[+-]?[\\d]+([.]?[\\d])*$";
  private const string c_True = "True";

  private XmpUtils() => throw new NotImplementedException();

  private static void ClearChildren(XmlNode node)
  {
    XmlNodeList xmlNodeList = node != null ? node.ChildNodes : throw new ArgumentNullException(nameof (node));
    int num = 0;
    for (int count = xmlNodeList.Count; num < count; ++num)
    {
      XmlNode oldChild = xmlNodeList[0];
      node.RemoveChild(oldChild);
    }
  }

  public static bool GetBoolValue(string value)
  {
    return value != null ? value.Equals("True") : throw new ArgumentNullException(nameof (value));
  }

  public static DateTime GetDateTimeValue(string value)
  {
    if (value == null)
      throw new ArgumentNullException(nameof (value));
    DateTime result = DateTime.Now;
    if (value != string.Empty)
    {
      string format = "yyyyMMddHHmmss";
      DateTime.TryParseExact(value, format, (IFormatProvider) DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowLeadingWhite, out result);
    }
    return result;
  }

  public static int GetIntValue(string value)
  {
    if (value == null)
      throw new ArgumentNullException(nameof (value));
    double result = 0.0;
    double.TryParse(value, NumberStyles.Integer, (IFormatProvider) CultureInfo.InvariantCulture, out result);
    return (int) result;
  }

  public static float GetRealValue(string value)
  {
    if (value == null)
      throw new ArgumentNullException(nameof (value));
    double result = 0.0;
    double.TryParse(value, NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result);
    return (float) result;
  }

  public static Uri GetUriValue(string value)
  {
    return value != null ? new Uri(value) : throw new ArgumentNullException(nameof (value));
  }

  public static void SetBoolValue(XmlElement parent, bool value)
  {
    if (parent == null)
      throw new ArgumentNullException(nameof (parent));
    string str = value ? "True" : "False";
    XmpUtils.SetTextValue(parent, str);
  }

  public static void SetDateTimeValue(XmlElement parent, DateTime value)
  {
    if (parent == null)
      throw new ArgumentNullException(nameof (parent));
    string str = value.ToString("yyyy-MM-dd'T'HH:mm:ss.ffzzz");
    XmpUtils.SetTextValue(parent, str);
  }

  public static void SetIntValue(XmlElement parent, int value)
  {
    if (parent == null)
      throw new ArgumentNullException(nameof (parent));
    string str = value.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    XmpUtils.SetTextValue(parent, str);
  }

  public static void SetRealValue(XmlElement parent, float value)
  {
    if (parent == null)
      throw new ArgumentNullException(nameof (parent));
    string str = value.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    XmpUtils.SetTextValue(parent, str);
  }

  public static void SetTextValue(XmlElement parent, string value)
  {
    if (parent == null)
      throw new ArgumentNullException(nameof (parent));
    if (value == null)
      throw new ArgumentNullException(nameof (value));
    XmpUtils.ClearChildren((XmlNode) parent);
    XmlText textNode = parent.OwnerDocument.CreateTextNode(value);
    parent.AppendChild((XmlNode) textNode);
  }

  public static void SetUriValue(XmlElement parent, Uri value)
  {
    if (parent == null)
      throw new ArgumentNullException(nameof (parent));
    string str = value.ToString();
    XmpUtils.SetTextValue(parent, str);
  }

  public static void SetXmlValue(XmlElement parent, XmlElement child)
  {
    if (parent == null)
      throw new ArgumentNullException(nameof (parent));
    if (child == null)
      throw new ArgumentNullException(nameof (child));
    XmpUtils.ClearChildren((XmlNode) parent);
    parent.AppendChild((XmlNode) child);
  }
}
