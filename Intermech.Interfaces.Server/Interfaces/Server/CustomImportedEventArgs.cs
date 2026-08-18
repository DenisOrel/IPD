// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.CustomImportedEventArgs
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

#nullable disable
namespace Intermech.Interfaces.Server;

public class CustomImportedEventArgs
{
  public IUserSession Session;
  public int CategoryID;
  public object DBSessionable;

  public CustomImportedEventArgs(IUserSession session, int categoryID, object dbSessionable)
  {
    this.Session = session;
    this.CategoryID = categoryID;
    this.DBSessionable = dbSessionable;
  }
}
