
// Type: Intermech.Controls.OleContainer.PAINTSTRUCT
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;


namespace Intermech.Controls.OleContainer;

public struct PAINTSTRUCT
{
  public IntPtr hdc;
  public bool fErase;
  public int rcPaint_left;
  public int rcPaint_top;
  public int rcPaint_right;
  public int rcPaint_bottom;
  public bool fRestore;
  public bool fIncUpdate;
  public int reserved1;
  public int reserved2;
  public int reserved3;
  public int reserved4;
  public int reserved5;
  public int reserved6;
  public int reserved7;
  public int reserved8;
}
