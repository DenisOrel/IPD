// Decompiled with JetBrains decompiler
// Type: Intermech.PropertyEditors.LCStepPropertyClass
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

#nullable disable
namespace Intermech.PropertyEditors;

public class LCStepPropertyClass
{
  private int lcStep;
  private string name = string.Empty;

  public int LCStep => this.lcStep;

  public LCStepPropertyClass(int aLCStep, string aName)
  {
    this.lcStep = aLCStep;
    this.name = aName;
  }

  public override string ToString() => this.name;
}
