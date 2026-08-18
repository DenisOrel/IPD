
// Type: Intermech.PropertyEditors.NatWindow
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>дополнительная обработка WM_MOUSEWHEEL</summary>
internal class NatWindow : NativeWindow
{
  public const int WM_MOUSEWHEEL = 522;
  private Control _control;

  public NatWindow(Control control) => this._control = control;

  protected override void WndProc(ref Message m)
  {
    if (m.Msg.Equals(522))
      NatWindow.SendMessage(new HandleRef((object) this._control, this._control.Handle), m.Msg, m.WParam, m.LParam);
    base.WndProc(ref m);
  }

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  internal static extern IntPtr SendMessage(HandleRef hWnd, int msg, IntPtr wParam, IntPtr lParam);
}
