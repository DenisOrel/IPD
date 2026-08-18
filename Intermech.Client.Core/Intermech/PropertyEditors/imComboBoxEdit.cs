
// Type: Intermech.PropertyEditors.imComboBoxEdit
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.XtraEditors;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>
/// перекрытие comboBox'а для устранения
/// WM_MOUSEWHEEL
/// </summary>
internal class imComboBoxEdit : ComboBoxEdit
{
  private Control _control;

  public void AttachControl(Control control) => this._control = control;

  public void DetachControl() => this._control = (Control) null;

  protected override void WndProc(ref Message m)
  {
    if (m.Msg.Equals(522))
    {
      if (this._control == null)
        return;
      NatWindow.SendMessage(new HandleRef((object) this._control, this._control.Handle), m.Msg, m.WParam, m.LParam);
    }
    else
      base.WndProc(ref m);
  }
}
