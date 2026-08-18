
// Type: Intermech.Client.Core.FormDesigner.Controls.AttrsControlButtonRenderer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// 
/// </summary>
public class AttrsControlButtonRenderer
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="g"></param>
  /// <param name="bounds"></param>
  /// <param name="buttons"></param>
  public void Draw(Graphics g, Point location, List<ControlButton> buttons)
  {
    if (buttons.Count <= 0)
      return;
    int x1 = location.X;
    foreach (ControlButton button in buttons)
    {
      Rectangle bounds = new Rectangle(new Point(x1, location.Y), FormDesignerUtils.ButtonSize);
      Image image = this.GetImage(button.Name);
      Size size;
      if (image != null)
      {
        int num1 = x1;
        size = FormDesignerUtils.ButtonSize;
        int num2 = size.Width / 2;
        int num3 = num1 + num2;
        size = image.Size;
        int num4 = size.Width / 2;
        int x2 = num3 - num4;
        size = FormDesignerUtils.ButtonSize;
        int num5 = size.Height / 2;
        size = image.Size;
        int num6 = size.Height / 2;
        int y = num5 - num6;
        Rectangle imageBounds = new Rectangle(new Point(x2, y), image.Size);
        ButtonRenderer.DrawButton(g, bounds, image, imageBounds, false, button.State);
      }
      else
        ButtonRenderer.DrawButton(g, bounds, button.State);
      int num = x1;
      size = FormDesignerUtils.ButtonSize;
      int width = size.Width;
      x1 = num + width;
    }
  }

  /// <summary>Сслыка на ImageList родителя (хранится у формы).</summary>
  private Image GetImage(string name)
  {
    return !FormDesignerUtils.ButtonImages.ContainsKey(name) ? (Image) null : FormDesignerUtils.ButtonImages[name];
  }
}
