// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.AutoScaleModeConverter
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
public class AutoScaleModeConverter : TypeConverter
{
  internal BidirectHashtable hash = new BidirectHashtable();

  /// <summary>
  /// 
  /// </summary>
  public AutoScaleModeConverter()
  {
    this.hash.Add((object) AutoScaleMode.Dpi, (object) "Dpi");
    this.hash.Add((object) AutoScaleMode.Font, (object) LocalizationHolder.rm.GetString("FormDesigner_101"));
    this.hash.Add((object) AutoScaleMode.Inherit, (object) LocalizationHolder.rm.GetString("FormDesigner_102"));
    this.hash.Add((object) AutoScaleMode.None, (object) LocalizationHolder.rm.GetString("FormDesigner_2"));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <param name="sourceType"></param>
  /// <returns></returns>
  public override bool CanConvertFrom(ITypeDescriptorContext context, System.Type sourceType)
  {
    return sourceType.Equals(typeof (string)) || base.CanConvertFrom(context, sourceType);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <param name="destinationType"></param>
  /// <returns></returns>
  public override bool CanConvertTo(ITypeDescriptorContext context, System.Type destinationType)
  {
    return destinationType.Equals(typeof (string)) || base.CanConvertTo(context, destinationType);
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
    return value.GetType().Equals(typeof (string)) ? this.hash[value] : base.ConvertFrom(context, culture, value);
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
    return destinationType.Equals(typeof (string)) ? this.hash[value] : base.ConvertTo(context, culture, value, destinationType);
  }
}
