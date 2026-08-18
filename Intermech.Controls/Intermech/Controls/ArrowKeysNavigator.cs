
// Type: Intermech.Controls.ArrowKeysNavigator
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using Intermech.Collections;
using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Controls;

[ProvideProperty("UpControl", typeof (Control))]
public class ArrowKeysNavigator : 
  Component,
  IComponent,
  IDisposable,
  IMessageFilter,
  IExtenderProvider
{
  private BiDirectMultiDictionary<Control, Control> _upControls = new BiDirectMultiDictionary<Control, Control>();

  protected override void Dispose(bool disposing)
  {
    if (disposing)
      this._upControls = (BiDirectMultiDictionary<Control, Control>) null;
    base.Dispose(disposing);
  }

  public bool CanExtend(object extendee) => extendee != null && extendee is Control;

  public bool PreFilterMessage(ref Message m)
  {
    if (m.HWnd != IntPtr.Zero)
      Control.FromHandle(m.HWnd);
    return false;
  }

  [DefaultValue(null)]
  public Control GetUpControl(Control control)
  {
    if (this._upControls == null)
      return (Control) null;
    Control upControl;
    this._upControls.TryGetValue(control, out upControl);
    return upControl;
  }

  public void SetUpControl(Control control, Control upControl)
  {
    if (this._upControls == null || control == null)
      return;
    if (upControl != null && !this._upControls.ContainsValue(upControl))
      upControl.Disposed += new EventHandler(this.upControl_Disposed);
    if (upControl == null)
    {
      Control control1 = (Control) null;
      this._upControls.TryGetValue(control, out control1);
      this._upControls.Remove(control);
      if (control1 == null || this._upControls.ContainsValue(control1))
        return;
      control1.Disposed -= new EventHandler(this.upControl_Disposed);
    }
    else
      this._upControls[control] = upControl;
  }

  private void upControl_Disposed(object sender, EventArgs e)
  {
    if (this._upControls == null || sender == null || !(sender is Control))
      return;
    Control control = (Control) sender;
    control.Disposed -= new EventHandler(this.upControl_Disposed);
    this._upControls.RemoveValues(control);
  }
}
