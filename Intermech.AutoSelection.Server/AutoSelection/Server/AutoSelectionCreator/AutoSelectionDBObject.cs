// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Server.AutoSelectionCreator.AutoSelectionDBObject
// Assembly: Intermech.AutoSelection.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 89DFCE1C-C473-4D66-BEC0-EFA8A5FDFD64
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.AutoSelection.Server.dll

using Intermech.AutoSelection.Server.AutoSelectionCache;
using Intermech.Interfaces;
using Intermech.Interfaces.AutoSelection.AutoSelectionCache;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using System.Data;

#nullable disable
namespace Intermech.AutoSelection.Server.AutoSelectionCreator;

public class AutoSelectionDBObject(UserSession session, DataTable objectParams) : DBObject(session, objectParams)
{
  private AutoSelectionRuleCacheService GetAutoSelectionRuleCacheService()
  {
    IAutoSelectionRuleCacheService ruleCacheService;
    try
    {
      ruleCacheService = ServiceUtils.GetService<IAutoSelectionRuleCacheService>((object) ServerServices.ServiceContainer, false);
    }
    catch
    {
      ruleCacheService = (IAutoSelectionRuleCacheService) null;
    }
    return ruleCacheService as AutoSelectionRuleCacheService;
  }

  protected override void DoCommitCreation()
  {
    base.DoCommitCreation();
    this.GetAutoSelectionRuleCacheService()?.RuleCacheAdd(this.ObjectID, (IUserSession) this.UserSession);
  }

  protected override void DoDelete()
  {
    this.GetAutoSelectionRuleCacheService()?.RuleCacheDelete(this.ObjectID, (IUserSession) this.UserSession);
    base.DoDelete();
  }

  protected override void DoPurge(long deleteMode) => base.DoPurge(deleteMode);

  protected override void DoAfterSetAdditionalAttributeValue(IDBAttribute attribute)
  {
    base.DoAfterSetAdditionalAttributeValue(attribute);
    this.GetAutoSelectionRuleCacheService()?.RuleCacheUpdate(this.ObjectID, attribute, (IUserSession) this.UserSession);
  }

  protected override void DoAfterDeleteAdditionalAttributeValue(
    IDBAttribute attribute,
    AttributeDataTableValue deletedValue)
  {
    base.DoAfterDeleteAdditionalAttributeValue(attribute, deletedValue);
    this.GetAutoSelectionRuleCacheService()?.RuleCacheUpdate(this.ObjectID, attribute, (IUserSession) this.UserSession);
  }
}
