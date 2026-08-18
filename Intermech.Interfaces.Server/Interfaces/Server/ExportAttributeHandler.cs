// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.ExportAttributeHandler
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

#nullable disable
namespace Intermech.Interfaces.Server;

public abstract class ExportAttributeHandler
{
  public object Value;

  public abstract void Handle(IUserSession session, IDBAttribute attr);
}
