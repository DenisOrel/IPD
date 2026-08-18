// Decompiled with JetBrains decompiler
// Type: Interop.Cadmech.HandleMode
// Assembly: Interop.IMText, Version=1.0.0.0, Culture=neutral, PublicKeyToken=8d2bc20ab69e4bb4
// MVID: 429E38D4-3785-4B44-8CD1-02E4A9CDD7BF
// Assembly location: D:\IPS\Client\Interop.IMText.dll

#nullable disable
namespace Interop.Cadmech;

public enum HandleMode
{
  hmNone = -1, // 0xFFFFFFFF
  hmEditAndPlace = 0,
  hmEdit = 1,
  hmRegen = 2,
  hmPlace = 3,
  hmAutoPlace = 4,
  hmRegenOnlyGeometry = 5,
}
