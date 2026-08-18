// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.ECO.ECOGoal
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

#nullable disable
namespace Intermech.Interfaces.ECO;

public enum ECOGoal
{
  NoGoal = -1, // 0xFFFFFFFF
  Change = 0,
  Annul = 1,
  Litera = 2,
  Replace = 3,
  Creation = 4,
  VersionCreate = 100, // 0x00000064
  Stamp = 101, // 0x00000065
}
