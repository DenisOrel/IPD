
// Type: Intermech.Controls.DocumentFormulaTextBox.DocumentFormulaTextBox
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Controls.DocumentFormulaTextBox;

public class DocumentFormulaTextBox : TextBox
{
  public DocumentFormulaTextBox()
  {
    if (this.DesignMode)
      return;
    this.ContextMenuStrip = (ContextMenuStrip) new DocumentFormulaToolStrip((TextBoxBase) this);
  }

  protected override void OnReadOnlyChanged(EventArgs e)
  {
    base.OnReadOnlyChanged(e);
    if (this.ReadOnly)
      this.BackColor = SystemColors.Control;
    else
      this.BackColor = SystemColors.Window;
  }

  public override bool PreProcessMessage(ref Message msg)
  {
    if (msg.Msg == 256 /*0x0100*/ || msg.Msg == 260)
    {
      switch ((Keys) (int) msg.WParam | Control.ModifierKeys)
      {
        case Keys.C | Keys.Control:
          this.Copy();
          return true;
        case Keys.V | Keys.Control:
          this.Paste();
          return true;
        case Keys.X | Keys.Control:
          this.Cut();
          return true;
      }
    }
    return base.PreProcessMessage(ref msg);
  }
}
