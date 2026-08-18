
// Type: Intermech.Client.Core.TrackBarEx
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core;

public class TrackBarEx : TrackBar
{
  protected override void WndProc(ref Message m)
  {
    if (m.Msg == 522)
    {
      int int32 = m.WParam.ToInt32();
      short num1 = this.HiWord(int32);
      int num2 = (int) this.LoWord(int32);
      this.Value = Math.Max(this.Minimum, Math.Min(this.Value - Math.Sign(num1) * this.SmallChange, this.Maximum));
    }
    else
      base.WndProc(ref m);
  }

  private short LoWord(int value) => (short) new Point(value).X;

  private short HiWord(int value) => (short) new Point(value).Y;

  [Flags]
  private enum VirtualKeyState
  {
    None = 0,
    ControlKey = 8,
    LeftButton = 1,
    MiddleButton = 16, // 0x00000010
    RightButton = 2,
    ShiftKey = 4,
    XButton1 = 32, // 0x00000020
    XButton2 = 64, // 0x00000040
  }
}
