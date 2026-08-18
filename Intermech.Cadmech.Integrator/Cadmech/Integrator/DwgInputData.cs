// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.DwgInputData
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class DwgInputData
{
  private FileContent fieldLayoutContent;
  private FileContent structFileContent;
  private string passportData;

  public FileContent FieldLayoutContent
  {
    get => this.fieldLayoutContent;
    set => this.fieldLayoutContent = value;
  }

  public FileContent StructFileContent
  {
    get => this.structFileContent;
    set => this.structFileContent = value;
  }

  public string PassportData
  {
    get => this.passportData;
    set => this.passportData = value;
  }
}
