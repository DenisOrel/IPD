
// Type: Intermech.Controls.OleContainer.SYSTEM_POWER_STATUS
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml


namespace Intermech.Controls.OleContainer;

public struct SYSTEM_POWER_STATUS
{
  public byte ACLineStatus;
  public byte BatteryFlag;
  public byte BatteryLifePercent;
  public byte Reserved1;
  public int BatteryLifeTime;
  public int BatteryFullLifeTime;
}
