// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.PDMSystemService
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Pdm;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using System;

#nullable disable
namespace Intermech.Pdm.Server;

internal class PDMSystemService : LongLifeObject, IFileNameGenerator
{
  private IUserSession convertToUserSession(object usObject)
  {
    switch (usObject)
    {
      case IUserSession _:
        return usObject as IUserSession;
      case Guid sessionGUID:
        return UserSession.GetSessionByID(sessionGUID);
      default:
        return (IUserSession) null;
    }
  }

  public string GenerateFileName(object session, string Prefix, string Extention)
  {
    IDbManager dataManager = (this.convertToUserSession(session) as UserSession).DataManager;
    long num = dataManager.DataProvider.NextGeneratorValue("IMS_FILE_ID_GEN", dataManager);
    return $"{Prefix}{num.ToString()}.{Extention}";
  }
}
