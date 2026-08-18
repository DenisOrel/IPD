// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.TlbComboBox
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class TlbComboBox : ComboBox
{
  internal bool locked;

  internal event TlbComboBox.DgtEnterPressed EnterPressed;

  internal TlbComboBox() => this.locked = true;

  private bool OurPrintf(params object[] msg) => CMisc.StcPrintf(msg);

  protected override bool ProcessCmdKey(ref Message msg, Keys keys)
  {
    if (keys != Keys.Return)
      return base.ProcessCmdKey(ref msg, keys);
    TlbComboBox.DgtEnterPressed enterPressed = this.EnterPressed;
    if (enterPressed != null)
      enterPressed((Control) this);
    return true;
  }

  internal delegate void DgtEnterPressed(Control Sender);
}
