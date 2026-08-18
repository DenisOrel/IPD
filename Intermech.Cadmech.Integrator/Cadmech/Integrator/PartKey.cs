// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.PartKey
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal class PartKey
{
  public static string Calculate(
    char sectionCode,
    TaggingModes tagMode,
    string tag,
    string okpCode,
    string name)
  {
    if (tagMode != TaggingModes.FakeDesignation || sectionCode != 'S' && sectionCode != 'P' && sectionCode != 'M')
      return tag;
    return !(okpCode != string.Empty) ? name : okpCode;
  }
}
