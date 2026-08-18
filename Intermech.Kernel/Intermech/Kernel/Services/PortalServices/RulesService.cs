// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.RulesService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.WebPortal;


namespace Intermech.Kernel.Services.PortalServices;

public abstract class RulesService : LongLifeObject
{
  protected IUserSession session;
  protected readonly string moduleName;
  protected readonly string sectionName;

  public RulesService(IUserSession session, string sectionName)
  {
    this.session = session;
    this.moduleName = PortalConsts.PortalClientModuleName;
    this.sectionName = sectionName;
  }

  protected IDBConfigurations Config => this.session.Configurations;
}
