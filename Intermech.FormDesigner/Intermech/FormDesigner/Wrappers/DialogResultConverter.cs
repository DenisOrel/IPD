// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.DialogResultConverter
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
public class DialogResultConverter : TypeConverter
{
  internal BidirectHashtable hash = new BidirectHashtable();

  /// <summary>
  /// 
  /// </summary>
  public DialogResultConverter()
  {
    this.hash.Add((object) DialogResult.None, (object) LocalizationHolder.rm.GetString("FormDesigner.DialogResultConverter.None"));
    this.hash.Add((object) DialogResult.OK, (object) LocalizationHolder.rm.GetString("FormDesigner.DialogResultConverter.OK"));
    this.hash.Add((object) DialogResult.Cancel, (object) LocalizationHolder.rm.GetString("FormDesigner.DialogResultConverter.Cancel"));
    this.hash.Add((object) DialogResult.Abort, (object) LocalizationHolder.rm.GetString("FormDesigner.DialogResultConverter.Abort"));
    this.hash.Add((object) DialogResult.Retry, (object) LocalizationHolder.rm.GetString("FormDesigner.DialogResultConverter.Retry"));
    this.hash.Add((object) DialogResult.Ignore, (object) LocalizationHolder.rm.GetString("FormDesigner.DialogResultConverter.Ignore"));
    this.hash.Add((object) DialogResult.Yes, (object) LocalizationHolder.rm.GetString("FormDesigner.DialogResultConverter.Yes"));
    this.hash.Add((object) DialogResult.No, (object) LocalizationHolder.rm.GetString("FormDesigner.DialogResultConverter.No"));
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
    return destinationType == typeof (string) ? this.hash[value] : base.ConvertTo(context, culture, value, destinationType);
  }
}
