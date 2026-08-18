// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.DocumentStatusesBatch
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

public abstract class DocumentStatusesBatch : DocumentFilesBatch
{
  protected IUserNamesCache userNamesCache;
  protected long currentUserId;
  protected string currentUserName;

  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.userNamesCache = (IUserNamesCache) CacheManager.Cache("UserNamesCache");
    ICurrentUserAndRole service = ServiceUtils.GetService<ICurrentUserAndRole>((object) ServicesManager.ServiceContainer, true);
    this.currentUserId = service.UserID;
    this.currentUserName = service.UserName;
  }
}
