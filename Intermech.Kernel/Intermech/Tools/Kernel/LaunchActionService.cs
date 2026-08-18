// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Kernel.LaunchActionService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Security;
using Intermech.Tools.LaunchActions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Xml;


namespace Intermech.Tools.Kernel;

internal sealed class LaunchActionService : LongLifeObject, ILaunchActionServer
{
  private IUserSession systemSession;
  private ToolSecurityService toolSecurity;

  public LaunchActionService(IUserSession systemSession, ToolSecurityService toolSecurity)
  {
    this.systemSession = (systemSession as IServerSession).Clone(true, nameof (LaunchActionService));
    this.toolSecurity = toolSecurity;
  }

  [MethodImpl(MethodImplOptions.Synchronized)]
  public LaunchActionInfo CreateAction(
    Guid objectType,
    ITarget target,
    LaunchType launchType,
    Guid handlerId,
    string xmlData)
  {
    if (objectType == Guid.Empty)
      throw new ArgumentException();
    if (target == null)
      throw new ArgumentNullException();
    if (handlerId == Guid.Empty)
      throw new ArgumentException();
    if (string.IsNullOrEmpty(xmlData))
      throw new ArgumentException();
    RBSServer.AuthenticateCaller();
    this.toolSecurity.CheckWriteAccess(target);
    XmlDocument data = new XmlDocument();
    data.LoadXml(xmlData);
    this.CheckDataFormat(data);
    IDBObject dbObj = this.systemSession.GetObjectCollection(Consts.LaunchActionObjectType).Create();
    DBUtils.WriteAttribute((IDBAttributable) dbObj, Consts.ObjectTypeRefAttr, (object) objectType);
    DBUtils.WriteAttribute((IDBAttributable) dbObj, Consts.TargetAttr, (object) this.toolSecurity.EncodeTarget(target));
    DBUtils.WriteAttribute((IDBAttributable) dbObj, Consts.LaunchTypeAttr, (object) (int) launchType);
    DBUtils.WriteAttribute((IDBAttributable) dbObj, Consts.HandlerIdAttr, (object) handlerId);
    this.SetActionDataInternal(dbObj, data);
    dbObj.CommitCreation(true);
    return new LaunchActionInfo(dbObj.ObjectGUID, handlerId, DBAttributeReader.GetDisplayName(dbObj));
  }

  [MethodImpl(MethodImplOptions.Synchronized)]
  public void RemoveAction(Guid actionId)
  {
    if (actionId == Guid.Empty)
      throw new ArgumentException();
    RBSServer.AuthenticateCaller();
    IDBObject dbObj = this.systemSession.GetObject(actionId, false);
    if (dbObj == null)
      return;
    this.toolSecurity.CheckWriteAccess(DBAttributeReader.GetTarget(dbObj, this.toolSecurity));
    this.RemoveReferencedDefaults(dbObj.ObjectID);
    dbObj.Delete(0L);
  }

  private void RemoveReferencedDefaults(long actionId)
  {
    ConditionStructure conditionStructure = new ConditionStructure(Consts.LaunchActionRefAttr, RelationalOperators.Equal, (object) actionId, LogicalOperators.NONE, 0);
    DBRecordSetParams paramSet = new DBRecordSetParams();
    paramSet.RecordCount = -1;
    paramSet.Columns = new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    };
    paramSet.Conditions = new ConditionStructure[1]
    {
      conditionStructure
    };
    IDBObjectCollection objectCollection = this.systemSession.GetObjectCollection(Consts.DefaultActionObjectType);
    DataTable dataTable = objectCollection.Select(paramSet);
    List<long> longList = new List<long>(dataTable.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      longList.Add(Convert.ToInt64(row[0]));
    objectCollection.Delete(longList.ToArray(), true, 0L);
  }

  [MethodImpl(MethodImplOptions.Synchronized)]
  public void SetActionData(Guid actionId, string xmlData)
  {
    if (actionId == Guid.Empty)
      throw new ArgumentException();
    if (string.IsNullOrEmpty(xmlData))
      throw new ArgumentException();
    RBSServer.AuthenticateCaller();
    XmlDocument data = new XmlDocument();
    data.LoadXml(xmlData);
    this.CheckDataFormat(data);
    IDBObject dbObj = this.systemSession.GetObject(actionId, true);
    this.toolSecurity.CheckWriteAccess(DBAttributeReader.GetTarget(dbObj, this.toolSecurity));
    this.SetActionDataInternal(dbObj, data);
  }

  private void SetActionDataInternal(IDBObject dbObj, XmlDocument data)
  {
    DBUtils.WriteAttribute((IDBAttributable) dbObj, Consts.NameAttr, (object) data.SelectSingleNode("//LookupData/@displayName").Value);
    DBUtils.WriteAttribute((IDBAttributable) dbObj, Consts.XmlDataAttr, (object) data.OuterXml);
  }

  [MethodImpl(MethodImplOptions.Synchronized)]
  public string GetActionData(Guid actionId)
  {
    if (actionId == Guid.Empty)
      throw new ArgumentException();
    RBSServer.AuthenticateCaller();
    return DBAttributeReader.GetXmlData(this.systemSession.GetObject(actionId, true));
  }

  [MethodImpl(MethodImplOptions.Synchronized)]
  public List<LaunchActionInfo> GetActionList(
    Guid objectType,
    ITarget target,
    LaunchType launchType)
  {
    if (objectType == Guid.Empty)
      throw new ArgumentException();
    if (target == null)
      throw new ArgumentNullException();
    RBSServer.AuthenticateCaller();
    ConditionStructure conditionStructure1 = new ConditionStructure(Consts.ObjectTypeRefAttr, RelationalOperators.Equal, (object) objectType, LogicalOperators.AND, 0);
    ConditionStructure conditionStructure2 = new ConditionStructure(Consts.TargetAttr, RelationalOperators.Equal, (object) this.toolSecurity.EncodeTarget(target), LogicalOperators.AND, 0);
    conditionStructure2.CaseSensitive = false;
    ConditionStructure conditionStructure3 = new ConditionStructure(Consts.LaunchTypeAttr, RelationalOperators.Equal, (object) (int) launchType, LogicalOperators.NONE, 0);
    DataTable dataTable = this.systemSession.GetObjectCollection(Consts.LaunchActionObjectType).Select(new DBRecordSetParams()
    {
      RecordCount = -1,
      Columns = new object[3]
      {
        (object) ObligatoryObjectAttributes.F_GUID,
        (object) Consts.HandlerIdAttr,
        (object) Consts.NameAttr
      },
      Conditions = new ConditionStructure[3]
      {
        conditionStructure1,
        conditionStructure2,
        conditionStructure3
      }
    });
    List<LaunchActionInfo> actionList = new List<LaunchActionInfo>(dataTable.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      Guid guid = new Guid(Convert.ToString(row[0]));
      Guid handlerId = new Guid(Convert.ToString(row[1]));
      string displayName = DBAttributeReader.GetDisplayName(row, 2, (object) guid);
      actionList.Add(new LaunchActionInfo(guid, handlerId, displayName));
    }
    return actionList;
  }

  [MethodImpl(MethodImplOptions.Synchronized)]
  public LaunchActionInfo GetActionInfo(Guid actionId)
  {
    if (actionId == Guid.Empty)
      throw new ArgumentException();
    RBSServer.AuthenticateCaller();
    IDBObject dbObj = this.systemSession.GetObject(actionId, true);
    Guid handlerId = new Guid(DBUtils.ReadAttribute<string>((IDBAttributable) dbObj, Consts.HandlerIdAttr));
    string displayName = DBAttributeReader.GetDisplayName(dbObj);
    return new LaunchActionInfo(actionId, handlerId, displayName);
  }

  [MethodImpl(MethodImplOptions.Synchronized)]
  public List<LaunchActionInfo> LookupActionList(
    Guid objectType,
    ITarget target,
    LaunchType launchType)
  {
    if (objectType == Guid.Empty)
      throw new ArgumentException();
    if (target == null)
      throw new ArgumentNullException();
    RBSServer.AuthenticateCaller();
    List<Guid> guidList = new List<Guid>(16 /*0x10*/);
    guidList.Add(objectType);
    guidList.AddRange((IEnumerable<Guid>) DBUtils.GetParentsInverted(objectType, this.systemSession));
    List<ITarget> targetList = new List<ITarget>(8);
    targetList.Add(target);
    targetList.AddRange((IEnumerable<ITarget>) this.toolSecurity.GetParentTargets(target));
    List<LaunchActionInfo> infos = new List<LaunchActionInfo>(32 /*0x20*/);
    foreach (Guid objectType1 in guidList)
    {
      foreach (ITarget target1 in targetList)
      {
        List<LaunchActionInfo> all = this.GetActionList(objectType1, target1, launchType).FindAll((Predicate<LaunchActionInfo>) (internalInfo => !infos.Exists((Predicate<LaunchActionInfo>) (info => info.ActionId == internalInfo.ActionId))));
        infos.AddRange((IEnumerable<LaunchActionInfo>) all);
      }
    }
    return infos;
  }

  private void CheckDataFormat(XmlDocument data)
  {
    XmlNode xmlNode = data.SelectSingleNode("//LookupData") != null ? data.SelectSingleNode("//LookupData/@displayName") : throw new KernelException("В xml-конфигурации команды отсутствует обязательный элемент LookupData.");
    if (xmlNode == null || string.IsNullOrEmpty(xmlNode.Value))
      throw new KernelException(LocalizationHolder.rm.GetString("Kernel_1130"));
  }

  [MethodImpl(MethodImplOptions.Synchronized)]
  public void SetDefaultAction(Guid objectType, ITarget target, Guid actionId)
  {
    if (objectType == Guid.Empty)
      throw new ArgumentNullException();
    if (target == null)
      throw new ArgumentNullException();
    if (actionId == Guid.Empty)
      throw new ArgumentException();
    RBSServer.AuthenticateCaller();
    this.toolSecurity.CheckWriteAccess(target);
    IDBObject dbObj1 = this.systemSession.GetObject(actionId, true);
    ITarget target1 = DBAttributeReader.GetTarget(dbObj1, this.toolSecurity);
    if (!this.toolSecurity.IsSubset(target1, target))
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_1131"), (object) this.toolSecurity.EncodeTarget(target), (object) this.toolSecurity.EncodeTarget(target1)));
    LaunchType launchType = DBAttributeReader.GetLaunchType(dbObj1);
    LaunchActionService.FoundDefaultAction defaultAction = this.FindDefaultAction(objectType, target, launchType);
    IDBObject dbObj2;
    if (defaultAction != null)
    {
      dbObj2 = this.systemSession.GetObject(defaultAction.Id, true);
    }
    else
    {
      dbObj2 = this.systemSession.GetObjectCollection(Consts.DefaultActionObjectType).Create();
      DBUtils.WriteAttribute((IDBAttributable) dbObj2, Consts.ObjectTypeRefAttr, (object) objectType);
      DBUtils.WriteAttribute((IDBAttributable) dbObj2, Consts.TargetAttr, (object) this.toolSecurity.EncodeTarget(target));
    }
    DBUtils.WriteAttribute((IDBAttributable) dbObj2, Consts.LaunchActionRefAttr, (object) dbObj1.ObjectID);
    if (!dbObj2.IsCreationMode)
      return;
    dbObj2.CommitCreation(true);
  }

  [MethodImpl(MethodImplOptions.Synchronized)]
  public void ResetDefaultAction(Guid objectType, ITarget target, Guid actionId)
  {
    if (objectType == Guid.Empty)
      throw new ArgumentNullException();
    if (target == null)
      throw new ArgumentNullException();
    if (actionId == Guid.Empty)
      throw new ArgumentException();
    RBSServer.AuthenticateCaller();
    this.toolSecurity.CheckWriteAccess(target);
    IDBObject dbObj = this.systemSession.GetObject(actionId, true);
    LaunchType launchType = DBAttributeReader.GetLaunchType(dbObj);
    LaunchActionService.FoundDefaultAction defaultAction = this.FindDefaultAction(objectType, target, launchType);
    if (defaultAction == null)
      return;
    if (defaultAction.ActionRefId != dbObj.ObjectID)
      throw new Exception();
    this.systemSession.GetObject(defaultAction.Id, true).Delete(0L);
  }

  [MethodImpl(MethodImplOptions.Synchronized)]
  public LaunchActionInfo GetDefaultAction(Guid objectType, ITarget target, LaunchType launchType)
  {
    if (objectType == Guid.Empty)
      throw new ArgumentNullException();
    if (target == null)
      throw new ArgumentNullException();
    RBSServer.AuthenticateCaller();
    LaunchActionService.FoundDefaultAction defaultAction = this.FindDefaultAction(objectType, target, launchType);
    if (defaultAction == null)
      return (LaunchActionInfo) null;
    IDBObject dbObj = this.systemSession.GetObject(defaultAction.ActionRefId, true);
    Guid handlerId = new Guid(DBUtils.ReadAttribute<string>((IDBAttributable) dbObj, Consts.HandlerIdAttr));
    string displayName = DBAttributeReader.GetDisplayName(dbObj);
    return new LaunchActionInfo(dbObj.ObjectGUID, handlerId, displayName);
  }

  [MethodImpl(MethodImplOptions.Synchronized)]
  public LaunchActionInfo LookupDefaultAction(
    Guid objectType,
    ITarget target,
    LaunchType launchType)
  {
    if (objectType == Guid.Empty)
      throw new ArgumentNullException();
    if (target == null)
      throw new ArgumentNullException();
    RBSServer.AuthenticateCaller();
    List<Guid> guidList = new List<Guid>(16 /*0x10*/);
    guidList.Add(objectType);
    guidList.AddRange((IEnumerable<Guid>) DBUtils.GetParentsInverted(objectType, this.systemSession));
    List<ITarget> targetList = new List<ITarget>(8);
    targetList.Add(target);
    targetList.AddRange((IEnumerable<ITarget>) this.toolSecurity.GetParentTargets(target));
    foreach (Guid objectType1 in guidList)
    {
      foreach (ITarget target1 in targetList)
      {
        LaunchActionInfo defaultAction = this.GetDefaultAction(objectType1, target1, launchType);
        if (defaultAction != null)
          return defaultAction;
      }
    }
    return (LaunchActionInfo) null;
  }

  private LaunchActionService.FoundDefaultAction FindDefaultAction(
    Guid objectType,
    ITarget target,
    LaunchType launchType)
  {
    ConditionStructure conditionStructure1 = new ConditionStructure(Consts.ObjectTypeRefAttr, RelationalOperators.Equal, (object) objectType, LogicalOperators.AND, 0);
    ConditionStructure conditionStructure2 = new ConditionStructure(Consts.TargetAttr, RelationalOperators.Equal, (object) this.toolSecurity.EncodeTarget(target), LogicalOperators.AND, 0);
    conditionStructure2.CaseSensitive = false;
    ConditionStructure conditionStructure3 = new ConditionStructure(Consts.LaunchActionRefAttr, RelationalOperators.NotEmpty, (object) null, LogicalOperators.NONE, 0);
    foreach (DataRow row in (InternalDataCollectionBase) this.systemSession.GetObjectCollection(Consts.DefaultActionObjectType).Select(new DBRecordSetParams()
    {
      RecordCount = -1,
      Columns = new object[2]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID,
        (object) Consts.LaunchActionRefAttr
      },
      Contents = new ColumnContents[2]
      {
        ColumnContents.ID,
        ColumnContents.ID
      },
      Conditions = new ConditionStructure[3]
      {
        conditionStructure1,
        conditionStructure2,
        conditionStructure3
      }
    }).Rows)
    {
      long int64_1 = Convert.ToInt64(row[0]);
      long int64_2 = Convert.ToInt64(row[1]);
      if (DBAttributeReader.GetLaunchType(this.systemSession.GetObject(int64_2, true)) == launchType)
        return new LaunchActionService.FoundDefaultAction(int64_1, int64_2);
    }
    return (LaunchActionService.FoundDefaultAction) null;
  }

  private class FoundDefaultAction
  {
    private long defaultActionId;
    private long actionRefId;

    public FoundDefaultAction(long defaultActionId, long actionRefId)
    {
      this.defaultActionId = defaultActionId;
      this.actionRefId = actionRefId;
    }

    public long Id => this.defaultActionId;

    public long ActionRefId => this.actionRefId;
  }
}
