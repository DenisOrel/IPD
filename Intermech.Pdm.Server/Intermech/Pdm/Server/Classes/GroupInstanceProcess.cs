// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.Classes.GroupInstanceProcess
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces;
using Intermech.Kernel;

#nullable disable
namespace Intermech.Pdm.Server.Classes;

internal abstract class GroupInstanceProcess
{
  public void Run(IUserSession session, IDBObject dbObject, IDBObject parentObject)
  {
    UserSession userSession = (UserSession) session;
    userSession.StartTransaction();
    try
    {
      this.OnRun(session, dbObject, parentObject);
      userSession.Commit();
    }
    catch
    {
      userSession.Rollback();
      throw;
    }
  }

  protected abstract void OnRun(IUserSession session, IDBObject dbObject, IDBObject parentObject);
}
