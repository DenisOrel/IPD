
// Type: Intermech.Controls.LineThicknessMenuItem
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Controls;

public class LineThicknessMenuItem : 
  LineMenuItem,
  IComponent,
  IDisposable,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IPopupControlHost,
  IPopupMenuItem,
  IArrowKeysNavigationSupported
{
  public LineThicknessMenuItem()
  {
    this.HiddenTextAlign = ContentAlignment.MiddleRight;
    this._stringFormat.Alignment = StringAlignment.Far;
    this.UpdateText();
  }

  /// <summary>Толщина линии</summary>
  public override int LineThickness
  {
    get => base.LineThickness;
    set
    {
      if (base.LineThickness == value)
        return;
      base.LineThickness = value;
      this.UpdateText();
      this.Invalidate();
    }
  }

  protected virtual void UpdateText()
  {
    this.HiddenText = $"{((float) this.LineThickness / 100f).ToString("0.##")} пт";
  }

  protected override bool DrawText() => true;

  protected override Rectangle GetLineRectangle(ref int maxTextRight)
  {
    Rectangle clientRectangle = this.ClientRectangle;
    clientRectangle.X += 55;
    clientRectangle.Width -= 65;
    maxTextRight = 50;
    return clientRectangle;
  }
}
