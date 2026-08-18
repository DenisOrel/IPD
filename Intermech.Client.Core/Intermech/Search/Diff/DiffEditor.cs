
// Type: Intermech.Search.Diff.DiffEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Search.UI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;


namespace Intermech.Search.Diff;

public sealed class DiffEditor : UITypeEditor, IStyleProvider
{
  public override bool GetPaintValueSupported(ITypeDescriptorContext context) => true;

  public override void PaintValue(PaintValueEventArgs e)
  {
    if (e == null)
      throw new ArgumentNullException(nameof (e));
    if (!(e.Context.PropertyDescriptor is IDiffPropertyDescriptor))
      throw new ArgumentException();
    Brush brush = this.GetBrush(((IDiffPropertyDescriptor) e.Context.PropertyDescriptor).GetDiff(e.Context.Instance).GetResult());
    e.Graphics.FillRectangle(brush, e.Bounds);
  }

  public Color GetBackColor(System.ComponentModel.PropertyDescriptor propertyDescriptor, object instance)
  {
    if (!(propertyDescriptor is IDiffPropertyDescriptor))
      throw new ArgumentException();
    return instance != null ? this.GetColor(((IDiffPropertyDescriptor) propertyDescriptor).GetDiff(instance).GetResult()) : throw new ArgumentNullException(nameof (instance));
  }

  private Brush GetBrush(DiffResult diffResult)
  {
    switch (diffResult)
    {
      case DiffResult.ValuesNotEquals:
        return Brushes.Yellow;
      case DiffResult.NotExist:
        return Brushes.Red;
      case DiffResult.NotExistOnOther:
        return Brushes.LightBlue;
      default:
        return Brushes.White;
    }
  }

  private Color GetColor(DiffResult diffResult)
  {
    switch (diffResult)
    {
      case DiffResult.ValuesNotEquals:
        return Color.Yellow;
      case DiffResult.NotExist:
        return Color.Red;
      case DiffResult.NotExistOnOther:
        return Color.LightBlue;
      default:
        return Color.White;
    }
  }
}
