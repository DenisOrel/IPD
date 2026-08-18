// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.StructFileConfigException
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal class StructFileConfigException : StructFileException
{
  public StructFileConfigException(string fileName, string message)
    : base(string.Format(Intermech.Localization.Localization.rm.GetString("Cadmech.Integrator_13"), (object) fileName, (object) message))
  {
  }

  public StructFileConfigException(string message)
    : base(message)
  {
  }
}
