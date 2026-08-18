// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.HelpPathToFileConverter
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>
/// HelpPathToFileConverter конвертер для русификации.
/// Нужен, для отображения наименования файла, а не всего пути к файлу.
/// </summary>
public class HelpPathToFileConverter : TypeConverter
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <param name="sourceType"></param>
  /// <returns></returns>
  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => true;

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
    if (value == null)
      return value;
    string path = value.ToString();
    return !string.IsNullOrEmpty(path) && !Path.HasExtension(path) ? (object) $"{path}.chm" : value;
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
    Type destinationType)
  {
    return value == null || value.ToString() == string.Empty ? value : (object) Path.GetFileName(value.ToString());
  }
}
