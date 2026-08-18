// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Requirement.RequirementsSettings
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System.ComponentModel;

#nullable disable
namespace Intermech.Tools.Client.Requirement;

public class RequirementsSettings
{
  private bool _enableRequirement;
  private bool _enableRequirementForCurrentUser;

  [DisplayName("Включить режим создания ТТ для всех пользователей")]
  [DefaultValue(false)]
  public bool EnableRequirement
  {
    get => this._enableRequirement;
    set => this._enableRequirement = value;
  }

  [DisplayName("Включить режим создания ТТ")]
  [DefaultValue(false)]
  public bool EnableRequirementForCurrentUser
  {
    get => this._enableRequirementForCurrentUser;
    set => this._enableRequirementForCurrentUser = value;
  }

  public void Save()
  {
    ICurrentUserAndRole service1 = ApplicationServices.Container.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    IDBConfigurations service2 = ApplicationServices.Container.GetService(typeof (IDBConfigurations)) as IDBConfigurations;
    if (service1 != null && service2 != null)
    {
      if (service1.IsAdmin)
        service2.WriteBool("Requirements", "Global", "EnableRequirement", this._enableRequirement, 0L);
      service2.WriteBool("Requirements", "UserOnly", "EnableRequirementForCurrentUser", this._enableRequirementForCurrentUser, service1.UserID);
    }
    else
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBConfigurations configurations = sessionKeeper.Session.Configurations;
        if (sessionKeeper.Session.IsAdmin)
          configurations.WriteBool("Requirements", "Global", "EnableRequirement", this._enableRequirement, 0L);
        configurations.WriteBool("Requirements", "UserOnly", "EnableRequirementForCurrentUser", this._enableRequirementForCurrentUser, sessionKeeper.Session.UserID);
      }
    }
  }

  public void Load()
  {
    IDBConfigurations service = ApplicationServices.Container.GetService(typeof (IDBConfigurations)) as IDBConfigurations;
    this.EnableRequirement = service.ReadBool("Requirements", "Global", "EnableRequirement", false, DBConfigMode.GlobalOnly);
    this.EnableRequirementForCurrentUser = service.ReadBool("Requirements", "UserOnly", "EnableRequirementForCurrentUser", this.EnableRequirement, DBConfigMode.UserOnly);
  }
}
