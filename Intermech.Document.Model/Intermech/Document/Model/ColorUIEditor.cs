// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ColorUIEditor
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

#nullable disable
namespace Intermech.Document.Model;

public class ColorUIEditor : ColorEditor
{
  /// <summary>Поддерживает ли отрисовка значения</summary>
  /// <param name="context">Контекст</param>
  /// <returns>Поддерживает ли отрисовка значения</returns>
  public override bool GetPaintValueSupported(ITypeDescriptorContext context)
  {
    object obj = context.PropertyDescriptor.GetValue(context.Instance);
    return (obj == null || !(obj is Color? nullable) || nullable.HasValue) && obj != null;
  }
}
