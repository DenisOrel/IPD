
// Type: Intermech.Controls.FormEx
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Controls;

public class FormEx : Form
{
  protected override void CreateHandle()
  {
    this.KeyPreview = true;
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterParent;
    if (!this.DesignMode && this.FormBorderStyle == FormBorderStyle.Sizable)
      this.MinimumSize = new Size(250, 250);
    base.CreateHandle();
  }

  protected override void OnKeyDown(KeyEventArgs e)
  {
    base.OnKeyDown(e);
    if (!this.Modal || e.KeyCode != Keys.Escape)
      return;
    this.DialogResult = DialogResult.Cancel;
  }
}
