// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.UI.RulerButton
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Model.UI;

internal class RulerButton : Control
{
  protected override void OnPaint(PaintEventArgs e)
  {
    base.OnPaint(e);
    using (SolidBrush solidBrush = new SolidBrush(SystemColors.InactiveCaptionText))
      e.Graphics.FillRectangle((Brush) solidBrush, 0, 0, this.Width, this.Height);
  }
}
