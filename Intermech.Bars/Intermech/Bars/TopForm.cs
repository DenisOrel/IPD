
// Type: Intermech.Bars.TopForm
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using Intermech.Util;
using System;
using System.Windows.Forms;


namespace Intermech.Bars
{
    internal class TopForm : Form
    {
      protected override void WndProc(ref Message msg)
      {
        if (msg.Msg == 33)
          msg.Result = new IntPtr(3);
        else
          base.WndProc(ref msg);
      }

      internal virtual void MakeVisible() => Win32.ShowWindow(this.Handle, 4);
    }
}
