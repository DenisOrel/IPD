// Decompiled with JetBrains decompiler
// Type: Intermech.UI.GlobalMouseHandler
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using System.Windows.Forms;

#nullable disable
namespace Intermech.UI;

public class GlobalMouseHandler : IMessageFilter
{
  private const int WM_MOUSEMOVE = 512 /*0x0200*/;

  public event MouseMovedEvent TheMouseMoved;

  public bool PreFilterMessage(ref Message m)
  {
    if (m.Msg == 512 /*0x0200*/)
    {
      MouseMovedEvent theMouseMoved = this.TheMouseMoved;
      if (theMouseMoved != null)
        theMouseMoved();
    }
    return false;
  }
}
