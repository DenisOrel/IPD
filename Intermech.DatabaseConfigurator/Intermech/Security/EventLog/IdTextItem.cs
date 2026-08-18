// Decompiled with JetBrains decompiler
// Type: Intermech.Security.EventLog.IdTextItem
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

#nullable disable
namespace Intermech.Security.EventLog;

internal class IdTextItem
{
  private int id;
  private string text;

  public IdTextItem(int id, string text)
  {
    this.id = id;
    this.text = text;
  }

  public int Id => this.id;

  public string Text => this.text;

  public override string ToString() => this.text;
}
