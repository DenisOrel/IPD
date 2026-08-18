// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.ToolbarControl
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class ToolbarControl : Control
{
  internal event ToolbarControl.DgtTlbTimer TlbTimer;

  protected override void OnMouseMove(MouseEventArgs ev)
  {
    this.Cursor = Cursors.Arrow;
    base.OnMouseMove(ev);
  }

  protected override void OnPaintBackground(PaintEventArgs ev)
  {
  }

  private bool OurPrintf(params object[] msg) => CMisc.StcPrintf(msg);

  protected override bool ProcessCmdKey(ref Message msg, Keys keys)
  {
    return (keys != Keys.I && keys != Keys.K && keys != Keys.M && keys != Keys.O && keys != Keys.Y && keys != Keys.D9 || this.Focused || !this.ContainsFocus) && base.ProcessCmdKey(ref msg, keys);
  }

  protected override void WndProc(ref Message msg)
  {
    if (msg.Msg == 275)
    {
      ToolbarControl.DgtTlbTimer tlbTimer = this.TlbTimer;
      if (tlbTimer == null)
        return;
      tlbTimer((int) msg.WParam);
    }
    else
      base.WndProc(ref msg);
  }

  internal delegate void DgtTlbTimer(int TimerId);
}
