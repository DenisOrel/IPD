// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Server.DBResolution
// Assembly: Intermech.Office.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 414402D9-801C-4C77-86BA-4C6FCAC834BE
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Office.Server.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Localization;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Office.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.Office.Server;

internal class DBResolution([NotNull] UserSession uSession, [NotNull] DataTable objectParams) : 
  DBObject(Intermech.Diagnostics.Check.ArgumentNotNull<UserSession>(uSession, nameof (uSession)), Intermech.Diagnostics.Check.ArgumentNotNull<DataTable>(objectParams, nameof (objectParams))),
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData,
  IDBGuid,
  IDeletable,
  IDBLifecycleLevel,
  IDBSecurityCollection,
  IDBSecurity,
  IDBLocalizable,
  IDBResolution
{
  public override AttributeValues[] SetAttributesValues(
    [NotNull] AttributeValues[] valuesList,
    bool deleteNotExistingAttributes,
    bool dontDeleteBlobs,
    bool returnDelta,
    GetAttributeValuesModes modes,
    Dictionary<string, Exception> exceptionsList)
  {
    foreach (AttributeValues values in valuesList)
    {
      if (values.AttributeID.Equals(OfficeConsts.AttrIsControlResolutionID))
      {
        if ((bool) values.Values[0])
        {
          bool flag = false;
          using (IEnumerator<AttributeValues> enumerator = ((IEnumerable<AttributeValues>) valuesList).Where<AttributeValues>((System.Func<AttributeValues, bool>) (attributeValue => attributeValue.AttributeID.Equals(OfficeConsts.AttrControllerID))).GetEnumerator())
          {
            if (enumerator.MoveNext())
            {
              if (enumerator.Current.Values[0] == DBNull.Value || values.Values[0] == null)
                throw new Exception(Intermech.Office.Server.Localization.GetString("Office.Server_13"));
              flag = true;
            }
          }
          if (!flag && this.ControllerID == 0L)
            throw new Exception(Intermech.Office.Server.Localization.GetString("Office.Server_13"));
          break;
        }
        break;
      }
      if (values.AttributeID.Equals(OfficeConsts.AttrControllerID))
      {
        if (values.Values[0] == DBNull.Value || values.Values[0] == null)
        {
          bool flag = false;
          using (IEnumerator<AttributeValues> enumerator = ((IEnumerable<AttributeValues>) valuesList).Where<AttributeValues>((System.Func<AttributeValues, bool>) (attributeValue => attributeValue.AttributeID.Equals(OfficeConsts.AttrIsControlResolutionID))).GetEnumerator())
          {
            if (enumerator.MoveNext())
            {
              if ((bool) enumerator.Current.Values[0])
                throw new Exception(Intermech.Office.Server.Localization.GetString("Office.Server_13"));
              flag = true;
            }
          }
          if (!flag && this.IsControlResolution)
            throw new Exception(Intermech.Office.Server.Localization.GetString("Office.Server_13"));
          break;
        }
        break;
      }
    }
    return base.SetAttributesValues(valuesList, deleteNotExistingAttributes, dontDeleteBlobs, returnDelta, modes, exceptionsList);
  }

  protected override void DoCommitCreation()
  {
    bool result;
    int num = this.TryGetAttrBoolValue(OfficeConsts.AttrTempDelayedRunID, out result) ? 1 : 0;
    if (num == 0 || !result)
      this.Run();
    if (num == 0)
      return;
    this.DeleteAttribute(OfficeConsts.AttrTempDelayedRunID);
  }

  internal void UpdateFiltrationTable(
    [NotNull] IUserSession session,
    bool checkCurrentAndUpdate,
    [CanBeNull] IFiltrationTableService filtrationService = null,
    [CanBeNull] IDbManager dbManager = null)
  {
    IFiltrationTableService filtrationTableService = filtrationService ?? ApplicationServices.Container.GetService<IFiltrationTableService>();
    IDbManager db = dbManager ?? ((UserSession) session).DataManager;
    long objectID = Math.Abs(this.ObjectID);
    long[] currentSeeingUserIDs = checkCurrentAndUpdate ? filtrationTableService.GetFilterIDs(db, objectID) : (long[]) null;
    List<long> actualSeeingUserIDs = this.GetActualSeeingUserIDsEnum();
    if (currentSeeingUserIDs != null && currentSeeingUserIDs.Length != 0)
    {
      foreach (long filterID in ((IEnumerable<long>) currentSeeingUserIDs).Where<long>((System.Func<long, bool>) (userID => !actualSeeingUserIDs.Contains(userID))))
        filtrationTableService.DeleteValue(db, objectID, filterID);
    }
    foreach (long filterID in currentSeeingUserIDs == null || currentSeeingUserIDs.Length == 0 ? (IEnumerable<long>) actualSeeingUserIDs : (actualSeeingUserIDs.Count > 0 ? actualSeeingUserIDs.Where<long>((System.Func<long, bool>) (userID => Array.IndexOf<long>(currentSeeingUserIDs, userID) == -1)) : (IEnumerable<long>) currentSeeingUserIDs))
      filtrationTableService.AddValue(db, objectID, filterID, (string) null);
  }

  [NotNull]
  private List<long> GetActualSeeingUserIDsEnum()
  {
    return this.GetActualSeeingUserIDsEnum(this.ExecutorIDs) ?? new List<long>(0);
  }

  [CanBeNull]
  private List<long> GetActualSeeingUserIDsEnum([CanBeNull] long[] executorIDs)
  {
    List<long> collection = (List<long>) null;
    if (executorIDs != null)
    {
      foreach (long executorId in executorIDs)
        (collection ?? (collection = new List<long>(executorIDs.Length + 3))).Add(executorId);
    }
    long creatorId = this.CreatorID;
    if (creatorId != 0L)
    {
      if (collection == null)
      {
        // ISSUE: explicit non-virtual call
        __nonvirtual ((collection = new List<long>(3)).Add(creatorId));
      }
      else
        collection.SafeAdd<long>(creatorId);
    }
    long authorId = this.AuthorID;
    if (authorId != 0L)
    {
      if (collection == null)
      {
        // ISSUE: explicit non-virtual call
        __nonvirtual ((collection = new List<long>(2)).Add(authorId));
      }
      else
        collection.SafeAdd<long>(authorId);
    }
    if (this.IsControlResolution)
    {
      long controllerId = this.ControllerID;
      if (controllerId != 0L)
      {
        if (collection == null)
        {
          // ISSUE: explicit non-virtual call
          __nonvirtual ((collection = new List<long>(1)).Add(controllerId));
        }
        else
          collection.SafeAdd<long>(controllerId);
      }
    }
    return collection;
  }

  protected override void DoAfterSetAdditionalAttributeValue([NotNull] IDBAttribute attribute)
  {
    if (attribute.AttributeID == OfficeConsts.AttrActualDateID)
    {
      IDBAttribute attributeById1 = this.GetAttributeByID(OfficeConsts.AttrExecutorsID);
      IDBAttribute attributeById2 = this.GetAttributeByID(OfficeConsts.AttrReportDatesID);
      if (attributeById1 == null || attributeById2 == null || attributeById1.ValuesCount <= 0 || attributeById1.ValuesCount != attributeById2.ValuesCount)
        return;
      this.UserSession.StartTransaction();
      try
      {
        DateTime asDateTime = attribute.AsDateTime;
        IDBRelationCollection relationCollection = this.UserSession.GetRelationCollection(OfficeConsts.ReltypeOfficeCompositionID);
        long int64_1 = Convert.ToInt64(relationCollection.EntersIn(new DBRecordSetParams((ConditionStructure[]) null, new object[1]
        {
          (object) -2
        }), this.ID).Rows[0][0]);
        List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(OfficeConsts.ObjtypeResolutionsID);
        relationCollection.ChildObjectTypes = (IList<int>) MetaDataHelper.OptimizeChildObjectTypes((IEnumerable<int>) childrenIdRecursive);
        DataTable dataTable = relationCollection.ConsistFrom(new DBRecordSetParams((ConditionStructure[]) null, new object[1]
        {
          (object) -2
        }), int64_1);
        bool flag = true;
        for (int index = 0; index < dataTable.Rows.Count; ++index)
        {
          long int64_2 = Convert.ToInt64(dataTable.Rows[index][0]);
          if (int64_2 != this.ObjectID)
          {
            IDBAttribute attributeById3 = this.UserSession.GetObject(int64_2).GetAttributeByID(OfficeConsts.AttrActualDateID);
            if ((attributeById3 != null ? (attributeById3.IsNull ? 1 : 0) : 1) != 0)
            {
              flag = false;
              break;
            }
            if (attributeById3.AsDateTime > asDateTime)
              asDateTime = attributeById3.AsDateTime;
          }
        }
        if (flag)
          this.UserSession.GetObject(int64_1).SetAttrDateTimeValue(OfficeConsts.AttrActualDateID, asDateTime);
        this.UserSession.Commit();
      }
      catch
      {
        this.UserSession.Rollback();
        throw;
      }
    }
    else
    {
      if (attribute.AttributeID != OfficeConsts.AttrExecutorsID && attribute.AttributeID != OfficeConsts.AttrControllerID && attribute.AttributeID != OfficeConsts.AttrAuthorID && attribute.AttributeID != OfficeConsts.AttrIsControlResolutionID || this.IsCreationMode)
        return;
      this.UserSession.GetCustomService<IResolutionAccessService>().SetAccess(this.ObjectID);
      this.UpdateFiltrationTable((IUserSession) this.UserSession, true);
    }
  }

  private bool CheckExecutionOrders([NotNull] IList<long> executors)
  {
    if (this.ResolutionExecutionType != ResolutionExecution.Combined)
      return true;
    IDBAttribute attributeById = this.GetAttributeByID(OfficeConsts.AttrExecutionOrderID);
    return (attributeById != null && attributeById.ValuesCount != 0 || executors.Count <= 0) && attributeById.ValuesCount == executors.Count;
  }

  public string Name
  {
    get => this.GetAttrSureStrValue(OfficeConsts.AttrNameID);
    set => this.SetAttrStrValue(OfficeConsts.AttrNameID, value);
  }

  public long AuthorID
  {
    get => this.GetAttrSureObjLinkValue(OfficeConsts.AttrAuthorID);
    set => this.SetAttrObjLinkValue(OfficeConsts.AttrAuthorID, value);
  }

  public bool IsControlResolution
  {
    get => this.GetAttrSureBoolValue(OfficeConsts.AttrIsControlResolutionID);
    set => this.SetAttrBoolValue(OfficeConsts.AttrIsControlResolutionID, value);
  }

  public long ControllerID
  {
    get => this.GetAttrSureObjLinkValue(OfficeConsts.AttrControllerID);
    set => this.SetAttrObjLinkValue(OfficeConsts.AttrControllerID, value);
  }

  public long[] ExecutorIDs
  {
    get => this.GetAttrObjLinkValuesArray(OfficeConsts.AttrExecutorsID);
    set => this.SetAttrMultiObjLinkValues(OfficeConsts.AttrExecutorsID, (IEnumerable<long>) value);
  }

  public long OfficeDocumentObjVerID
  {
    get => OfficeHelper.FindOfficeDocument((IUserSession) this.UserSession, this.ObjectID);
  }

  public bool IsPrivate => this.IsTypeOrSubtype(OfficeConsts.ObjtypeConfidentialResolutionsID);

  [NotNull]
  public ResolutionProgressReportRecord[] ProgressReportRecords
  {
    get
    {
      IReadOnlyList<long> attrObjLinkValues = this.GetAttrObjLinkValues(OfficeConsts.AttrReportAuthorsID);
      IReadOnlyList<DateTime> attrDateTimeValues = this.GetAttrDateTimeValues(OfficeConsts.AttrReportDatesID);
      return attrObjLinkValues.Count > 0 && attrDateTimeValues.Count > 0 ? attrObjLinkValues.Zip<long, DateTime, ResolutionProgressReportRecord>((IEnumerable<DateTime>) attrDateTimeValues, (Func<long, DateTime, ResolutionProgressReportRecord>) ((authorsID, reportDate) => new ResolutionProgressReportRecord(authorsID, reportDate))).ToArray<ResolutionProgressReportRecord>(Math.Min(attrObjLinkValues.Count, attrDateTimeValues.Count)) : Array.Empty<ResolutionProgressReportRecord>();
    }
  }

  public DateTime RegistrationDate
  {
    get => this.GetAttrSureDateTimeValue(OfficeConsts.AttrRegistrationDateID);
    set => this.SetAttrDateTimeValue(OfficeConsts.AttrRegistrationDateID, value);
  }

  public ResolutionExecution ResolutionExecutionType
  {
    get => (ResolutionExecution) this.GetAttrSureIntValue(OfficeConsts.AttrResolutionExecuteTypeID);
  }

  public long ResponseUserID
  {
    get => this.GetAttrSureObjLinkValue(OfficeConsts.AttrResponseUserID);
    set
    {
      this.SetAttrObjLinkValue(OfficeConsts.AttrResponseUserID, value, autoDelAttrIfEmpty: true);
    }
  }

  public DateTime PlannedDate
  {
    get => this.GetAttrSureDateTimeValue(OfficeConsts.AttrPlannedDateID);
    set => this.SetAttrDateTimeValue(OfficeConsts.AttrPlannedDateID, value);
  }

  public string ResolutionText
  {
    get => this.GetAttrSureStrValue(OfficeConsts.AttrResolutionTextID);
    set => this.SetAttrStrValue(OfficeConsts.AttrResolutionTextID, value);
  }

  public DateTime ActualDate
  {
    get => this.GetAttrSureDateTimeValue(OfficeConsts.AttrActualDateID);
    set => this.SetAttrDateTimeValue(OfficeConsts.AttrActualDateID, value);
  }

  public DateTime ControlDate => this.GetAttrSureDateTimeValue(OfficeConsts.AttrControlDateID);

  public ResolutionContextInfo ContextInfo
  {
    get => OfficeHelper.GetResolutionContextInfo(this.Session, this.ObjectID, this.ID);
  }

  public bool IsUserAnyOfRoles(long userID, ResolutionUserRoles resolutionUserRoles)
  {
    if (this.UserSession.UserID == userID && (resolutionUserRoles & ResolutionUserRoles.Admin) == ResolutionUserRoles.Admin && this.UserSession.IsAdmin || (resolutionUserRoles & ResolutionUserRoles.Creator) == ResolutionUserRoles.Creator && this.CreatorID == userID || (resolutionUserRoles & ResolutionUserRoles.Author) == ResolutionUserRoles.Author && this.AuthorID == userID || (resolutionUserRoles & ResolutionUserRoles.Controller) == ResolutionUserRoles.Controller && this.IsControlResolution && this.ControllerID == userID)
      return true;
    return (resolutionUserRoles & ResolutionUserRoles.Executor) == ResolutionUserRoles.Executor && ((IEnumerable<long>) this.ExecutorIDs).Contains<long>((Predicate<long>) (executorID => executorID == userID));
  }

  public bool IsUserAnyOfRoles(ResolutionUserRoles resolutionUserRoles)
  {
    return this.IsUserAnyOfRoles(this.UserSession.UserID, resolutionUserRoles);
  }

  public void Run()
  {
    if (OfficeConsts.LсResolutionStepCreationID != -1 && this.LCStep != OfficeConsts.LсResolutionStepCreationID)
      throw new Exception($"Рассылка поручений возможна доступна для поручений, находящихся на этапе жизненного цикла \"{MetaDataHelper.GetLCStepName(OfficeConsts.LсResolutionStepCreationID)}\", поручение \"{this.Caption}\" (идентификатор версии == {this.ObjectID}) находится на этапе \"{MetaDataHelper.GetLCStepName(this.LCStep)}\"");
    bool controlResolution = this.IsControlResolution;
    ResolutionExecution resolutionExecutionType = this.ResolutionExecutionType;
    IDBObject officeDoc = OfficeHelper.GetOfficeDoc((IUserSession) this.UserSession, this.ObjectType, this.ObjectID, this.ID);
    OfficeGeneralSettings settings = Intermech.Diagnostics.Check.NotNull<IOfficeGeneralSettingsService>((IOfficeGeneralSettingsService) this.UserSession.GetCustomService(typeof (IOfficeGeneralSettingsService)), "settingsService").Settings;
    Intermech.Diagnostics.Check.NotNull<OfficeGeneralSettings>(settings, "officeGeneralSettings");
    IOfficeDocumentTypeService customService = this.UserSession.GetCustomService<IOfficeDocumentTypeService>();
    OfficeDocumentTypeSettings documentTypeSettings = (OfficeDocumentTypeSettings) null;
    if (officeDoc != null)
    {
      documentTypeSettings = customService.GetSettings(this.UserSession.SessionGUID, officeDoc.ObjectType);
      Intermech.Diagnostics.Check.NotNull<OfficeDocumentTypeSettings>(documentTypeSettings, "settings");
      Intermech.Diagnostics.Check.NotNull<OrderProcessTemplates>(documentTypeSettings.ProcessTemplates, "ProcessTemplates");
      if (resolutionExecutionType == ResolutionExecution.Parallel)
      {
        if (controlResolution && documentTypeSettings.ProcessTemplates.Control == Guid.Empty)
          throw new Exception(Intermech.Office.Server.Localization.GetString("Office.Server_2", (object) MetaDataHelper.GetObjectTypeName(officeDoc.ObjectType)));
        if (!controlResolution && documentTypeSettings.ProcessTemplates.NoControl == Guid.Empty)
          throw new Exception(Intermech.Office.Server.Localization.GetString("Office.Server_3", (object) MetaDataHelper.GetObjectTypeName(officeDoc.ObjectType)));
      }
      else
      {
        if (controlResolution && documentTypeSettings.ProcessTemplates.SuccessiveControl == Guid.Empty)
          throw new Exception(Intermech.Office.Server.Localization.GetString("Office.Server_15", (object) MetaDataHelper.GetObjectTypeName(officeDoc.ObjectType)));
        if (!controlResolution && documentTypeSettings.ProcessTemplates.SuccessiveNoControl == Guid.Empty)
          throw new Exception(Intermech.Office.Server.Localization.GetString("Office.Server_16", (object) MetaDataHelper.GetObjectTypeName(officeDoc.ObjectType)));
      }
    }
    else if (resolutionExecutionType == ResolutionExecution.Parallel)
    {
      QuickObjectInfo objectInfo;
      if (controlResolution)
      {
        if (settings.ParallelControlResolutionTemplateID != 0L)
        {
          objectInfo = this.Session.GetObjectInfo(settings.ParallelControlResolutionTemplateID);
          if (!objectInfo.Empty)
            goto label_17;
        }
        throw new Exception("В настройках канцелярии не выбран шаблон для контрольных поручений параллельного исполнения поручений без документов");
      }
label_17:
      if (!controlResolution)
      {
        if (settings.ParallelNonControlResolutionTemplateID != 0L)
        {
          objectInfo = this.Session.GetObjectInfo(settings.ParallelNonControlResolutionTemplateID);
          if (!objectInfo.Empty)
            goto label_29;
        }
        throw new Exception("В настройках канцелярии не выбран шаблон для неконтрольных поручений параллельного исполнения поручений без документов");
      }
    }
    else
    {
      QuickObjectInfo objectInfo;
      if (controlResolution)
      {
        if (settings.ConsistentControlResolutionTemplateID != 0L)
        {
          objectInfo = this.Session.GetObjectInfo(settings.ConsistentControlResolutionTemplateID);
          if (!objectInfo.Empty)
            goto label_25;
        }
        throw new Exception("В настройках канцелярии не выбран шаблон для контрольных поручений последовательного исполнения поручений без документов");
      }
label_25:
      if (!controlResolution)
      {
        if (settings.ConsistentNonControlResolutionTemplateID != 0L)
        {
          objectInfo = this.Session.GetObjectInfo(settings.ConsistentNonControlResolutionTemplateID);
          if (!objectInfo.Empty)
            goto label_29;
        }
        throw new Exception("В настройках канцелярии не выбран шаблон для неконтрольных поручений последовательного исполнения поручений без документов");
      }
    }
label_29:
    IList<long> executorIds = (IList<long>) this.ExecutorIDs;
    if (executorIds.Count == 0)
      throw new Exception(Intermech.Office.Server.Localization.GetString("Office.Server_4"));
    long controlUserID = 0;
    if (controlResolution)
    {
      controlUserID = this.ControllerID;
      if (controlUserID == 0L)
        throw new Exception(Intermech.Office.Server.Localization.GetString("Office.Server_5"));
    }
    IDBAttribute attributeById = this.GetAttributeByID(OfficeConsts.AttrPlannedDateID);
    if (controlResolution && (attributeById != null ? (attributeById.IsNull ? 1 : 0) : 1) != 0)
      throw new Exception(Intermech.Office.Server.Localization.GetString("Office.Server_12"));
    this.UpdateFiltrationTable((IUserSession) this.UserSession, false);
    IResolutionProcess resolutionProcess;
    if (officeDoc != null)
    {
      resolutionProcess = ResolutionProcess.GetProcess(resolutionExecutionType, documentTypeSettings.ProcessTemplates, string.Format(Intermech.Office.Server.Localization.GetString("Office.Server_6"), (object) officeDoc.NameInMessages), controlResolution);
    }
    else
    {
      long documentResolution = ResolutionProcess.GetTemplateIDofNonDocumentResolution(settings, resolutionExecutionType, controlResolution);
      QuickObjectInfo objectInfo = this.UserSession.GetObjectInfo(this.ObjectID);
      Guid versionGuid = this.Session.GetObjectInfo(documentResolution).VersionGuid;
      resolutionProcess = ResolutionProcess.GetNonDocumentProcess(resolutionExecutionType, versionGuid, objectInfo.Caption, controlResolution);
    }
    Intermech.Diagnostics.Check.NotNull<IResolutionProcess>(resolutionProcess, "resolutionProcess");
    resolutionProcess.Execute((IUserSession) this.UserSession, (IDBObject) this, new ResolutionProcessExecuteArgs(officeDoc != null ? officeDoc.ObjectID : 0L, executorIds, attributeById == null || attributeById.IsNull ? DateTime.MinValue : attributeById.AsDateTime, controlUserID));
  }
}
