// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.FontUnitsConverter
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Localization;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>FontUnits конвертер для русификации.</summary>
public class FontUnitsConverter : BaseTypeConverter
{
  /// <summary>Конструктор.</summary>
  public FontUnitsConverter()
  {
    this._hash.Add((object) GraphicsUnit.World, (object) LocalizationHolder.rm.GetString("FormDesigner.FontUnitsConverter.World"));
    this._hash.Add((object) GraphicsUnit.Pixel, (object) LocalizationHolder.rm.GetString("FormDesigner.FontUnitsConverter.Pixel"));
    this._hash.Add((object) GraphicsUnit.Point, (object) LocalizationHolder.rm.GetString("FormDesigner.FontUnitsConverter.Point"));
    this._hash.Add((object) GraphicsUnit.Inch, (object) LocalizationHolder.rm.GetString("FormDesigner.FontUnitsConverter.Inch"));
    this._hash.Add((object) GraphicsUnit.Document, (object) LocalizationHolder.rm.GetString("FormDesigner.FontUnitsConverter.Document"));
    this._hash.Add((object) GraphicsUnit.Millimeter, (object) LocalizationHolder.rm.GetString("FormDesigner.FontUnitsConverter.Millimeter"));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public override TypeConverter.StandardValuesCollection GetStandardValues(
    ITypeDescriptorContext context)
  {
    if (this._values != null)
      return this._values;
    ArrayList values = new ArrayList((ICollection) Enum.GetValues(typeof (GraphicsUnit)));
    values.Remove((object) GraphicsUnit.Display);
    this._values = new TypeConverter.StandardValuesCollection((ICollection) values);
    return this._values;
  }
}
