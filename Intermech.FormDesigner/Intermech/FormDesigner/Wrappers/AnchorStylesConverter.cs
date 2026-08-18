// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.AnchorStylesConverter
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Localization;
using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>AnchorStyles конвертер для русификации.</summary>
public class AnchorStylesConverter : BaseTypeConverter
{
  /// <summary>Конструктор.</summary>
  public AnchorStylesConverter()
  {
    this._hash.Add((object) AnchorStyles.Bottom, (object) LocalizationHolder.rm.GetString("FormDesigner_64"));
    this._hash.Add((object) AnchorStyles.Left, (object) LocalizationHolder.rm.GetString("FormDesigner_66"));
    this._hash.Add((object) AnchorStyles.None, (object) LocalizationHolder.rm.GetString("FormDesigner_2"));
    this._hash.Add((object) AnchorStyles.Right, (object) LocalizationHolder.rm.GetString("FormDesigner_67"));
    this._hash.Add((object) AnchorStyles.Top, (object) LocalizationHolder.rm.GetString("FormDesigner_68"));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <param name="culture"></param>
  /// <param name="value"></param>
  /// <returns></returns>
  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    if (!(value.GetType() == typeof (string)))
      return base.ConvertFrom(context, culture, value);
    string str = (string) value;
    if (culture == null)
      culture = CultureInfo.CurrentCulture;
    char[] chArray = new char[1]
    {
      culture.TextInfo.ListSeparator[0]
    };
    string[] strArray = str.Split(chArray);
    AnchorStyles anchorStyles = AnchorStyles.None;
    foreach (string key in strArray)
      anchorStyles |= (AnchorStyles) this._hash[(object) key];
    return (object) anchorStyles;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <param name="culture"></param>
  /// <param name="value"></param>
  /// <param name="destinationType"></param>
  /// <returns></returns>
  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    System.Type destinationType)
  {
    if (!(destinationType == typeof (string)))
      return base.ConvertTo(context, culture, value, destinationType);
    if (culture == null)
      culture = CultureInfo.CurrentCulture;
    int num = (int) value;
    ArrayList arrayList = new ArrayList();
    if ((num & 1) > 0)
      arrayList.Add(this._hash[(object) AnchorStyles.Top]);
    if ((num & 2) > 0)
      arrayList.Add(this._hash[(object) AnchorStyles.Bottom]);
    if ((num & 4) > 0)
      arrayList.Add(this._hash[(object) AnchorStyles.Left]);
    if ((num & 8) > 0)
      arrayList.Add(this._hash[(object) AnchorStyles.Right]);
    if (arrayList.Count == 0)
      arrayList.Add(this._hash[(object) AnchorStyles.None]);
    string separator = culture.TextInfo.ListSeparator[0].ToString() + " ";
    string[] strArray1 = new string[arrayList.Count];
    arrayList.CopyTo((Array) strArray1);
    string[] strArray2 = strArray1;
    return (object) string.Join(separator, strArray2);
  }
}
