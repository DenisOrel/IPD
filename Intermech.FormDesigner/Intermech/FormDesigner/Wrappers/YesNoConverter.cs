// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.YesNoConverter
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using System.Collections;
using System.ComponentModel;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>Boolean конвертер для русификации.</summary>
public class YesNoConverter : BaseTypeConverter
{
  /// <summary>Конструктор.</summary>
  public YesNoConverter()
  {
    this._hash.Add((object) true, (object) Consts.YesValue);
    this._hash.Add((object) false, (object) Consts.NoValue);
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
    this._values = new TypeConverter.StandardValuesCollection((ICollection) new object[2]
    {
      (object) true,
      (object) false
    });
    return this._values;
  }
}
