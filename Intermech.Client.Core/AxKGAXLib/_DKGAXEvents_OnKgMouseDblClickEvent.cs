
// Type: AxKGAXLib._DKGAXEvents_OnKgMouseDblClickEvent
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace AxKGAXLib;

public class _DKGAXEvents_OnKgMouseDblClickEvent
{
  public short nButton;
  public short nShiftState;
  public int x;
  public int y;
  public bool proceed;

  public _DKGAXEvents_OnKgMouseDblClickEvent(short nButton, short nShiftState, int x, int y)
  {
    this.nButton = nButton;
    this.nShiftState = nShiftState;
    this.x = x;
    this.y = y;
  }
}
