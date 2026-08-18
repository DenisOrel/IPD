// Decompiled with JetBrains decompiler
// Type: Intermech.UI.CenterWinDialogHelper
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using Intermech.WindowsDll;
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.UI;

public class CenterWinDialogHelper : IDisposable
{
  private int _mTries;
  [NotNull]
  private readonly Form _mOwner;

  public CenterWinDialogHelper([NotNull] Form owner)
  {
    this._mOwner = owner;
    owner.BeginInvoke((Delegate) new MethodInvoker(this.FindDialog));
  }

  public void Dispose() => this._mTries = -1;

  private void FindDialog()
  {
    if (this._mTries < 0)
      return;
    User32.EnumThreadWndProc callback = new User32.EnumThreadWndProc(this.CheckWindow);
    if (!User32.EnumThreadWindows(Kernel32.GetCurrentThreadId(), callback, IntPtr.Zero) || ++this._mTries >= 10)
      return;
    this._mOwner.BeginInvoke((Delegate) new MethodInvoker(this.FindDialog));
  }

  private bool CheckWindow(IntPtr hWnd, IntPtr lp)
  {
    StringBuilder buffer = new StringBuilder(260);
    User32.GetClassName(hWnd, buffer, buffer.Capacity);
    if (buffer.ToString() != "#32770")
      return true;
    Rectangle rectangle = new Rectangle(this._mOwner.Location, this._mOwner.Size);
    Interop.RECT rc;
    User32.GetWindowRect(hWnd, out rc);
    User32.MoveWindow(hWnd, rectangle.Left + (rectangle.Width - rc.Right + rc.Left) / 2, rectangle.Top + (rectangle.Height - rc.Bottom + rc.Top) / 2, rc.Right - rc.Left, rc.Bottom - rc.Top, true);
    return false;
  }
}
