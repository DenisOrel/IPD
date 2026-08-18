// Decompiled with JetBrains decompiler
// Type: Intermech.PropertyEditors.LCLink
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Map;

#nullable disable
namespace Intermech.PropertyEditors;

public class LCLink : MapLabeledLink
{
  private LCLinkObject lcLinkObject;

  public LCLinkObject LCLinkObject
  {
    get => this.lcLinkObject;
    set => this.lcLinkObject = value;
  }

  public LCLink()
  {
  }

  public LCLink(LCLinkObject lpd) => this.lcLinkObject = lpd;
}
