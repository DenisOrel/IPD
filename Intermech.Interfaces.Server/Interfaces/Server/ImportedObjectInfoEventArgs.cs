// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.ImportedObjectInfoEventArgs
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

#nullable disable
namespace Intermech.Interfaces.Server;

public class ImportedObjectInfoEventArgs
{
  public IDBObject New;
  public ImportingObject Old;
  public BriefcaseInfoEventArgs BriefcaseInfo;

  public ImportedObjectInfoEventArgs(
    IDBObject newObject,
    ImportingObject oldObject,
    BriefcaseInfoEventArgs briefcaseInfo)
  {
    this.New = newObject;
    this.Old = oldObject;
    this.BriefcaseInfo = briefcaseInfo;
  }
}
