// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Versions.DBVersionRuleObject
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System.Data;


namespace Intermech.Kernel.Versions;

public class DBVersionRuleObject(UserSession uSession, DataTable objectParams) : DBObject(uSession, objectParams)
{
  protected override void DoCommitCreation()
  {
    base.DoCommitCreation();
    this.AfterAddVersionRule();
  }

  protected override void DoDelete()
  {
    IVersionRulesCacheService rulesCacheService;
    try
    {
      rulesCacheService = ServerServices.GetService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService;
    }
    catch
    {
      rulesCacheService = (IVersionRulesCacheService) null;
    }
    if (rulesCacheService == null)
      return;
    VersionsRule versionsRule = rulesCacheService[this.ObjectID];
    if (versionsRule != null && versionsRule.CurrentRuleType != VersionsRuleType.vrtStandardRule)
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_486"), (object) this.ObjectName));
    rulesCacheService.Delete(this.ObjectID);
  }

  protected override void DoPurge(long DeleteMode) => base.DoPurge(DeleteMode);

  private void AfterAddVersionRule()
  {
    if (!(ServerServices.GetService(typeof (IVersionRulesCacheService)) is IVersionRulesCacheService service))
      return;
    service.LoadRule((object) this.UserSession, this.ObjectID);
  }

  protected override void DoAfterSetAdditionalAttributeValue(IDBAttribute attribute)
  {
    if (attribute.AttributeID == MetaDataHelper.GetAttributeTypeID("cad00820-306c-11d8-b4e9-00304f19f545"))
      this.ObjectGUID.ToString();
    base.DoAfterSetAdditionalAttributeValue(attribute);
  }
}
