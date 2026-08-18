
// Type: Intermech.Controls.OleContainer.MSG
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;


namespace Intermech.Controls.OleContainer;

[Serializable]
public struct MSG
{
  public IntPtr hwnd;
  public int message;
  public IntPtr wParam;
  public IntPtr lParam;
  public int time;
  public int pt_x;
  public int pt_y;
}
