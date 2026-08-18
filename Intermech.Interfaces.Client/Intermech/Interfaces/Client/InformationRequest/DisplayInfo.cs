// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.InformationRequest.DisplayInfo
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.WindowsDll;

#nullable disable
namespace Intermech.Interfaces.Client.InformationRequest;

public class DisplayInfo
{
  public string Availability { get; set; }

  public string ScreenHeight { get; set; }

  public string ScreenWidth { get; set; }

  public Interop.RECT MonitorArea { get; set; }

  public Interop.RECT WorkArea { get; set; }
}
