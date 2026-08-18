// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Server.AutoSelectionServerPlugin
// Assembly: Intermech.AutoSelection.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 89DFCE1C-C473-4D66-BEC0-EFA8A5FDFD64
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.AutoSelection.Server.dll

using Intermech.AutoSelection.Server.AutoSelectionCache;
using Intermech.AutoSelection.Server.AutoSelectionCreator;
using Intermech.Interfaces;
using Intermech.Interfaces.AutoSelection;
using Intermech.Interfaces.AutoSelection.AutoSelectionCache;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.Plugins;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.AutoSelection.Server;

public class AutoSelectionServerPlugin : IPackage, IConfigurable
{
  private IPluginManager _manager;
  private AutoSelectionRuleCacheService _autoSelectionRuleCacheService;

  public string Name => Intermech.Localization.LocalizationHolder.rm.GetString("AutoSelection.Server_1");

  public void Load(IServiceProvider serviceProvider)
  {
    AutoSelectionServerCache.ServiceProvider = serviceProvider;
    AutoSelectionServerCache.DBTimedEvents = serviceProvider.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents;
    AutoSelectionServerCache.EventLogHelper = serviceProvider.GetService(typeof (IEventLogHelper)) as IEventLogHelper;
    this._manager = serviceProvider.GetService(typeof (IPluginManager)) as IPluginManager;
    if (this._manager != null)
      this._manager.LoadComplete += new EventHandler(this._manager_LoadComplete);
    if (AutoSelectionServerCache.EventLogHelper == null)
      return;
    AutoSelectionServerCache.EventLogHelper.BeforeDeleteAttributeTypeEvent += new DeleteAttributeTypeHandler(AutoSelectionServerPlugin._BeforeDeleteAttributeType_Handler);
    AutoSelectionServerCache.EventLogHelper.BeforeDeleteObjectTypeEvent += new DeleteObjectTypeHandler(AutoSelectionServerPlugin._BeforeDeleteObjectType_Handler);
    AutoSelectionServerCache.EventLogHelper.AfterDeleteObjectTypeEvent += new DeleteObjectTypeHandler(this._AfterDeleteObjectType_Handler);
  }

  public void Unload()
  {
    if (AutoSelectionServerCache.EventLogHelper != null)
    {
      AutoSelectionServerCache.EventLogHelper.BeforeDeleteAttributeTypeEvent -= new DeleteAttributeTypeHandler(AutoSelectionServerPlugin._BeforeDeleteAttributeType_Handler);
      AutoSelectionServerCache.EventLogHelper.BeforeDeleteObjectTypeEvent -= new DeleteObjectTypeHandler(AutoSelectionServerPlugin._BeforeDeleteObjectType_Handler);
      AutoSelectionServerCache.EventLogHelper.AfterDeleteObjectTypeEvent -= new DeleteObjectTypeHandler(this._AfterDeleteObjectType_Handler);
    }
    if (this._manager != null)
      this._manager.LoadComplete -= new EventHandler(this._manager_LoadComplete);
    this._manager_Unload();
  }

  public void LoadConfiguration(IConfigurationManager configurationManager)
  {
    configurationManager.Create("AutoSelectionServer");
  }

  public void SaveConfiguration(IConfigurationManager configurationManager)
  {
    configurationManager.Open("AutoSelectionServer");
  }

  private static ConditionStructure GetEqualityConditionOnGuidAttributeValue(
    string attributeTypeGuidStr,
    Guid value)
  {
    return new ConditionStructure(MetaDataHelper.GetAttributeID((object) new Guid(attributeTypeGuidStr)), RelationalOperators.Equal, (object) value.ToString(), (object) 0, LogicalOperators.OR, 0, false, AttributeSourceTypes.Object, ColumnContents.Text);
  }

  private static bool CheckExistsRuleWithAttributeValue(
    IUserSession session,
    Guid value,
    IEnumerable<string> inAttributeTypeGuids)
  {
    ConditionStructure[] array = inAttributeTypeGuids.Select<string, ConditionStructure>((Func<string, ConditionStructure>) (s => AutoSelectionServerPlugin.GetEqualityConditionOnGuidAttributeValue(s, value))).ToArray<ConditionStructure>();
    return session.GetObjectCollection(AutoSelectionConsts.objTypeRuleID).RecordsExists(array);
  }

  private static bool CheckInternalAttributeTypeUsage(
    IDBAttributeType attrType,
    IUserSession session)
  {
    return AutoSelectionServerPlugin.CheckExistsRuleWithAttributeValue(session, attrType.GUID, (IEnumerable<string>) new string[2]
    {
      "cad001d0-306c-11d8-b4e9-00304f19f545",
      "cadd9c03-306c-11d8-b4e9-00304f19f545"
    });
  }

  private static bool CheckInternalObjectTypeUsage(IDBObjectType objType, IUserSession session)
  {
    return AutoSelectionServerPlugin.CheckExistsRuleWithAttributeValue(session, objType.PropertiesStructure.ObjectTypeGuid, (IEnumerable<string>) new string[2]
    {
      "cad001a0-306c-11d8-b4e9-00304f19f545",
      "cad00149-306c-11d8-b4e9-00304f19f545"
    });
  }

  private bool RemoveObjectTypeFromRuleCache(int objectTypeId, IUserSession session)
  {
    if (this._autoSelectionRuleCacheService == null)
      return false;
    List<int> objectTypes = this._autoSelectionRuleCacheService.GetObjectTypes(session.SessionGUID);
    if (0 >= objectTypes.RemoveAll((Predicate<int>) (typeId => typeId == objectTypeId)))
      return false;
    this._autoSelectionRuleCacheService.SetObjectTypes(objectTypes, session.SessionGUID);
    return true;
  }

  private static void _BeforeDeleteAttributeType_Handler(
    IDBAttributeType sender,
    IUserSession session)
  {
    if (AutoSelectionServerPlugin.CheckInternalAttributeTypeUsage(sender, session))
      throw new KernelException(string.Format(Intermech.Localization.LocalizationHolder.rm.GetString("AutoSelection.Server_3_CanNotDeleteAttributeType"), (object) sender.GUID));
  }

  private static void _BeforeDeleteObjectType_Handler(IDBObjectType sender, IUserSession session)
  {
    if (AutoSelectionServerPlugin.CheckInternalObjectTypeUsage(sender, session))
      throw new KernelException(string.Format(Intermech.Localization.LocalizationHolder.rm.GetString("AutoSelection.Server_4_CanNotDeleteObjectType"), (object) sender.PropertiesStructure.ObjectTypeGuid));
  }

  private void _AfterDeleteObjectType_Handler(IDBObjectType sender, IUserSession session)
  {
    this.RemoveObjectTypeFromRuleCache(sender.ObjectType, session);
  }

  private void _manager_LoadComplete(object sender, EventArgs e)
  {
    ICustomServices service1 = ServiceUtils.GetService<ICustomServices>((object) AutoSelectionServerCache.ServiceProvider, false);
    this._autoSelectionRuleCacheService = new AutoSelectionRuleCacheService();
    service1?.AddService(typeof (IAutoSelectionRuleCacheService), (object) this._autoSelectionRuleCacheService);
    ServerServices.AddService(typeof (IAutoSelectionRuleCacheService), (object) this._autoSelectionRuleCacheService);
    if (!(ServiceUtils.GetService<IDBObjectService>((object) ServerServices.ServiceContainer, false) is ICreatorContainer service2))
      return;
    service2.AddCreator((object) AutoSelectionConsts.objTypeRuleGuid, (object) new AutoSelectionDBObjectCreator());
  }

  private void _manager_Unload()
  {
    ServiceUtils.GetService<ICustomServices>((object) AutoSelectionServerCache.ServiceProvider, false)?.RemoveService(typeof (IAutoSelectionRuleCacheService));
    ServerServices.RemoveService(typeof (IAutoSelectionRuleCacheService));
    if (!(ServiceUtils.GetService<IDBObjectService>((object) ServerServices.ServiceContainer, false) is ICreatorContainer service))
      return;
    service.RemoveCreator((object) AutoSelectionConsts.objTypeRuleGuid);
  }
}
