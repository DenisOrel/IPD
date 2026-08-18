// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Structure.SignConfigs
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Structure;

public static class SignConfigs
{
  public static bool GetConfig(ConfigId Id)
  {
    switch (Id)
    {
      case ConfigId.cidShowLastPodpis:
        return false;
      case ConfigId.cidNoCheckActualPodpis:
        return false;
      case ConfigId.cidShowParentOpers:
        return false;
      default:
        return false;
    }
  }
}
