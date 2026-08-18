// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.ServerMRPSettings
// Assembly: Intermech.MRP.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 90CF20BA-CEDA-4320-95C8-661A6AE661C2
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.MRP.Server.dll

using Intermech.Kernel;
using Intermech.Localization;
using System;

#nullable disable
namespace Intermech.Interfaces.MRP;

public class ServerMRPSettings : MRPSettings
{
  public ServerMRPSettings()
  {
  }

  public ServerMRPSettings(IUserSession session) => this.LoadSettings(session);

  private IUserSession GetUserSession(object usrSession)
  {
    switch (usrSession)
    {
      case IUserSession _:
        return usrSession as IUserSession;
      case Guid sessionGUID:
        return UserSession.GetSessionByID(sessionGUID);
      case string _:
        return UserSession.GetSessionByID(new Guid((string) usrSession));
      default:
        return (IUserSession) null;
    }
  }

  public override bool LoadSettings(Guid sessionGuid)
  {
    return this.LoadSettings(this.GetUserSession((object) sessionGuid) ?? throw new Exception(LocalizationHolder.rm.GetString("MRP.Server_1")));
  }

  public override bool SaveSettings(Guid sessionGuid)
  {
    return this.SaveSettings(this.GetUserSession((object) sessionGuid) ?? throw new Exception(LocalizationHolder.rm.GetString("MRP.Server_2")));
  }
}
