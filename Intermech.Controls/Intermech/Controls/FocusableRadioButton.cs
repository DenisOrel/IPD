
// Type: Intermech.Controls.FocusableRadioButton
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Windows.Forms;


namespace Intermech.Controls;

internal class FocusableRadioButton : RadioButton
{
  public FocusableRadioButton() => this.AutoCheck = false;

  protected override bool ShowFocusCues => true;

  protected override void OnKeyDown(KeyEventArgs kevent)
  {
    if (kevent.KeyCode == Keys.Return || kevent.KeyCode == Keys.Space)
    {
      if (this.Checked)
        return;
      this.Checked = true;
    }
    else
      base.OnKeyDown(kevent);
  }

  protected override void OnMouseDown(MouseEventArgs mevent)
  {
    if (!this.Checked)
      this.Checked = true;
    base.OnMouseDown(mevent);
  }
}
