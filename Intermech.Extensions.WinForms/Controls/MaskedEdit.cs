// Decompiled with JetBrains decompiler
// Type: Intermech.Controls.MaskedEdit
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Controls;

[ToolboxBitmap(typeof (MaskedTextBox))]
public class MaskedEdit : MaskedTextBox
{
  private const int WM_KEYDOWN = 256 /*0x0100*/;
  private const int WM_KEYUP = 257;

  public event EventHandler EnterPressed;

  protected override void WndProc(ref Message m)
  {
    if (m.Msg == 257)
    {
      Keys wparam = (Keys) (int) m.WParam;
      if (wparam == Keys.Return || wparam == Keys.Return)
        this.FireEnterPressed();
    }
    base.WndProc(ref m);
  }

  protected virtual void FireEnterPressed()
  {
    EventHandler enterPressed = this.EnterPressed;
    if (enterPressed == null)
      return;
    enterPressed((object) this, new EventArgs());
  }
}
