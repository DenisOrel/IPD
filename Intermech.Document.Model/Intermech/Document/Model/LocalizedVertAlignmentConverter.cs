// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.LocalizedVertAlignmentConverter
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Localization;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

#nullable disable
namespace Intermech.Document.Model;

/// <summary>Конвертер типа VertAlignment</summary>
public class LocalizedVertAlignmentConverter : LocalizedEnumConverter
{
  /// <summary>Конструктор</summary>
  public LocalizedVertAlignmentConverter()
  {
    this.EnumType = typeof (StringAlignment);
    this.StringValues = new string[3]
    {
      LocalizationHolder.rm.GetString("Document.Model_498"),
      LocalizationHolder.rm.GetString("Document.Model_499"),
      LocalizationHolder.rm.GetString("Document.Model_500")
    };
  }

  /// <summary>Преобразует данный объект в тип этого конвертера,
  /// используя заданную контекстную информацию и информацию о культурной среде</summary>
  /// <param name="context">ITypeDescriptorContext, предоставляющий контекстную информацию о формате</param>
  /// <param name="culture">Объект CultureInfo, который нужно использовать в качестве текущей культурной среды</param>
  /// <param name="value">Объект Object, который нужно преобразовать</param>
  /// <returns>Объект Object, представляющий преобразованное значение</returns>
  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    return (object) (StringAlignment) base.ConvertFrom(context, culture, value);
  }
}
