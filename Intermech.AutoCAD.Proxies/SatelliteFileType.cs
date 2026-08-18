// Decompiled with JetBrains decompiler
// Type: Intermech.AutoCAD.Proxies.SatelliteFileType
// Assembly: Intermech.AutoCAD.Proxies, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 36C988AC-B8D8-43EC-AA22-521DF7E8A085
// Assembly location: D:\IPS\Client\Intermech.AutoCAD.Proxies.dll
// XML documentation location: D:\IPS\Client\Intermech.AutoCAD.Proxies.xml

using System;

#nullable disable
namespace Intermech.AutoCAD.Proxies;

[Flags]
public enum SatelliteFileType
{
  None = 0,
  Dwg = 1,
  RasterImage = 2,
  Underlay = 4,
  All = Underlay | RasterImage | Dwg, // 0x00000007
}
