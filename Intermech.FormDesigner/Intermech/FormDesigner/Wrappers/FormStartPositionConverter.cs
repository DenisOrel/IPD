// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.FormStartPositionConverter
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Localization;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>
/// 
/// </summary>
public class FormStartPositionConverter : TypeConverter
{
  internal BidirectHashtable hash = new BidirectHashtable();

  /// <summary>
  /// 
  /// </summary>
  public FormStartPositionConverter()
  {
    this.hash.Add((object) FormStartPosition.CenterParent, (object) LocalizationHolder.rm.GetString("FormDesigner_69"));
    this.hash.Add((object) FormStartPosition.CenterScreen, (object) LocalizationHolder.rm.GetString("FormDesigner_70"));
    this.hash.Add((object) FormStartPosition.Manual, (object) LocalizationHolder.rm.GetString("FormDesigner_71"));
    this.hash.Add((object) FormStartPosition.WindowsDefaultBounds, (object) LocalizationHolder.rm.GetString("FormDesigner_72"));
    this.hash.Add((object) FormStartPosition.WindowsDefaultLocation, (object) LocalizationHolder.rm.GetString("FormDesigner_73"));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <param name="sourceType"></param>
  /// <returns></returns>
  public override bool CanConvertFrom(ITypeDescriptorContext context, System.Type sourceType)
  {
    return sourceType == typeof (string) || base.CanConvertFrom(context, sourceType);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <param name="destinationType"></param>
  /// <returns></returns>
  public override bool CanConvertTo(ITypeDescriptorContext context, System.Type destinationType)
  {
    return destinationType == typeof (string) || base.CanConvertTo(context, destinationType);
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
    return value.GetType() == typeof (string) ? this.hash[value] : base.ConvertFrom(context, culture, value);
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
    return destinationType == typeof (string) && value is FormStartPosition ? this.hash[value] : base.ConvertTo(context, culture, value, destinationType);
  }
}
