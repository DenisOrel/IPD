// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.KernelUpdate
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.MetadataUpdates;
using Intermech.Interfaces.Objects;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using Intermech.IO;
using Intermech.Kernel.Projects;
using Intermech.Kernel.Search;
using Intermech.Kernel.Services;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Text;
using System.Text.RegularExpressions;


namespace Intermech.Kernel;

public class KernelUpdate : IUpdatable, IDBVersionUpdater
{
  private long _eventID;
  private IEventLogHelper _eventLogHelper;
  private static readonly PathNormalizer _pathNormalizer = new PathNormalizer(AppDomain.CurrentDomain.BaseDirectory);

  public KernelUpdate(IEventLogHelper eventLogHelper)
  {
    this._eventLogHelper = eventLogHelper != null ? eventLogHelper : throw new ArgumentNullException(nameof (eventLogHelper));
  }

  public static PathNormalizer PathNormalizer
  {
    [DebuggerStepThrough] get => KernelUpdate._pathNormalizer;
  }

  public string[] GetUpdateScripts()
  {
    return new string[144 /*0x90*/]
    {
      "1.1 Intermech.Kernel.LCLevels.xml",
      "Intermech.Kernel.Attributes.xml",
      "Users.Schema.xml",
      "1.2 Intermech.Kernel.Selections.AttributeTypes.xml",
      "1.2 Intermech.Kernel.Selections.RelationTypes.xml",
      "1.2 Intermech.Kernel.Selections.ObjectTypes.xml",
      "1.3 Intermech.Kernel.VersionRules.AttributeTypes.xml",
      "1.3 Intermech.Kernel.VersionRules.RelationTypes.xml",
      "1.3 Intermech.Kernel.VersionRules.ObjectTypes.xml",
      "1.3 Intermech.Kernel.VersionRules.Objects.xml",
      "1.4 Intermech.Kernel.EditingContexts.AttributeTypes.xml",
      "1.4 Intermech.Kernel.EditingContexts.ObjectTypes.xml",
      "1.4 Intermech.Kernel.EditingContexts.RelationTypes.xml",
      "1.5 Intermech.Kernel.Roles.AttributeTypes.xml",
      "1.5 Intermech.Kernel.Roles.ObjectTypes.xml",
      "1.5 Intermech.Kernel.Roles.Objects.xml",
      "1.5 Intermech.Kernel.Roles.RelationTypes.xml",
      "1.5 Intermech.Kernel.Roles.Constraints.xml",
      "1.6 Intermech.Kernel.Projects.AttributeTypes.xml",
      "1.6 Intermech.Kernel.Projects.RelationTypes.xml",
      "1.6 Intermech.Kernel.Projects.ObjectTypes.xml",
      "1.6 Intermech.Kernel.Projects.Objects.xml",
      "1.61 Intermech.City.Attributes.xml",
      "1.62 Intermech.Organizations.Attributes.xml",
      "1.62 Intermech.Organizations.ObjectTypes.xml",
      "1.7 Intermech.Kernel.Users.AttributeTypes.xml",
      "1.7 Intermech.Kernel.Users.ObjectTypes.xml",
      "1.7 Intermech.Kernel.Users.Objects.xml",
      "1.7 Intermech.Scripting.Client.xml",
      "1.8 Intermech.Kernel.Plugins.AttributeTypes.xml",
      "1.8 Intermech.Kernel.Plugins.Objects.xml",
      "1.9 Intermech.Kernel.MeasureValues.AttributeTypes.xml",
      "1.9 Intermech.Kernel.MeasureValues.ObjectTypes.xml",
      "1.9 Intermech.Kernel.MeasureValues.Objects.xml",
      "1.9.1 SpecificHeatCapacity.xml",
      "1.9.2 SpecificThermalConductivity.xml",
      "1.9.3 InchMeasures.xml",
      "1.9.4 FootMeasures.xml",
      "1.10 Intermech.Kernel.AttrContainers.AttributeTypes.xml",
      "1.10 Intermech.Kernel.AttrContainers.ObjectTypes.xml",
      "1.11 Intermech.Kernel.Documents.AttributeTypes.xml",
      "1.11 Intermech.Kernel.Documents.RelationTypes.xml",
      "1.11 Intermech.Kernel.Documents.ObjectTypes.xml",
      "1.11 Intermech.Kernel.Documents.Objects.xml",
      "1.12 Intermech.Kernel.Articles.AttributeTypes.xml",
      "1.12 Intermech.Kernel.Articles.ObjectTypes.xml",
      "1.13 Intermech.Kernel.FileCabinets.AttributeTypes.xml",
      "1.13 Intermech.Kernel.FileCabinets.ObjectTypes.xml",
      "1.14 Intermech.Kernel.Workspaces.ObjectTypes.xml",
      "1.15 Intermech.Kernel.TableReports.AttributeTypes.xml",
      "1.15 Intermech.Kernel.TableReports.ObjectTypes.xml",
      "1.16 Intermech.Kernel.FormulaLibs.ObjectTypes.xml",
      "1.17 Intermech.Kernel.DocTemplates.ObjectTypes.xml",
      "1.18 Intermech.Kernel.Storages.AttributeTypes.xml",
      "1.19 Intermech.Kernel.Storages.ObjectType.xml",
      "1.20 Intermech.Kernel.Objects.xml",
      "Intermech.Kernel.MaterialReplaces.xml",
      "1.21 Global Editing Contexts.xml",
      "1.23 Intermech.Kernel.BricsCAD.Documents.xml",
      "1.23 Intermech.Kernel.NanoCAD.Documents.xml",
      "1.24 Intermech.Kernel.RevisionInstantiationModeAttribute.xml",
      "2.1 Intermech.Imbase.Attributes.xml",
      "2.2 Intermech.Kernel.EcoImportService.xml",
      "3.1 Document Relation Type - CheckOut Files.xml",
      "4.2.Intermech.City.ObjectTypes.xml",
      "4.3 Intermech.Kernel.Email.xml",
      "5.1 Intermech.Kernel.Web.AttribyteTypes.xml",
      "5.2 Intermech.Kernel.Web.RelationTypes.xml",
      "5.3 Intermech.Kernel.Web.LCSchema.xml",
      "5.4 Intermech.Kernel.Web.ObjectTypes.xml",
      "5.5 Intermech.Kernel.Web.Objects.xml",
      "Intermech.Tools.HandlerId.xml",
      "Intermech.Tools.LaunchActionType.xml",
      "Intermech.Tools.Target.xml",
      "Intermech.Tools.XmlConfig.xml",
      "Intermech.Tools.ObjectTypeRef.xml",
      "Intermech.Tools.ToolSecurityGroupAttr.xml",
      "Intermech.Tools.UserRefAttr.xml",
      "Intermech.Tools.BasedOnCADModelAttr.xml",
      "Intermech.Tools.ToolServiceObject.xml",
      "Intermech.Tools.IntegratorObject.xml",
      "Intermech.Tools.LaunchActionObject.xml",
      "Intermech.Tools.LaunchActionRef.xml",
      "Intermech.Tools.DefaultActionObject.xml",
      "Intermech.Tools.UserSecurityObject.xml",
      "Intermech.Tools.CADConfigurationFile.xml",
      "Intermech.Tools.CADConfigurationName.xml",
      "Intermech.Tools.InstanceKey.xml",
      "Intermech.Tools.OccurenceKey.xml",
      "Intermech.Tools.DocumentTree.xml",
      "Intermech.Tools.ArticleTree.xml",
      "Intermech.Tools.ArticleToDocumentTree.xml",
      "Intermech.Tools.ToolClient.xml",
      "Intermech.Tools.StandardModels.xml",
      "Intermech.Tools.StandardModelsArticleLink.xml",
      "Intermech.Tools.PrivateFiles.xml",
      "Intermech.Tools.BugAttributes.xml",
      "Intermech.Tools.DocumentBugs.xml",
      "Intermech.Tools.OwnedByIntegrator.xml",
      "Intermech.Tools.CADLinkType.xml",
      "Intermech.Tools.DocumentRepresentations.xml",
      "Intermech.Tools.ThereAreJTRepresentations.xml",
      "Intermech.Tools.DraftDocuments.xml",
      "Intermech.Tools.IntegrationStatus.xml",
      "Intermech.Tools.IntegrationErrors.xml",
      "Intermech.Tools.ExternalMaterialID.xml",
      "6.0 Intermech.Compas2D.Integrator.xml",
      "6.0 Intermech.Compas3D.Integrator.xml",
      "6.05 Intermech.ECAD.Integrator.xml",
      "6.1 Intermech.AltiumDesigner.Integrator.xml",
      "6.3 Intermech.MG.Integrator.xml",
      "Intermech.Kernel.Material.xml",
      "Intermech.Calendars.xml",
      "Intermech.Organizer.AttributesTypes.xml",
      "Intermech.Organizer.RelationsTypes.xml",
      "Intermech.Organizer.ObjectsTypes.xml",
      "Intermech.Kernel.Ldap.xml",
      "1.22 Intermech.Client.Core.ParamsStorageService.ObjectTypes.xml",
      "Intermech.Kernel.LCStepScript.ObjectTypes.xml",
      "IOSettings.xml",
      "PrevVersionID.xml",
      "6.2 Intermech.Kernel.Web.ImportedObjects.xml",
      "Intermech.SearchAPI.plugin.xml",
      "Intermech.Kernel.Users.Internal.xml",
      "Intermech.Kernel.AutoSnapshots.xml",
      "Intermech.Kernel.Selections.Attributes.1.xml",
      "NotifySamples.xml",
      "Intermech.Client.DocumentPreview.xml",
      "KernelDiagnostics.xml",
      "Intermech.Search.CompositionByObjectTypesFilters.Updates.xml",
      "Intermech.Search.ContextMenus.Updates.xml",
      "Intermech.Search.AutoConcretization.Updates.xml",
      "main_lc_schemas.xml",
      "Manual_Samples_VersionsMode.xml",
      "Intermech.Kernel.HtmlReports.xml",
      "user_locked.xml",
      "Intermech.Kernel.ComponentSelection.xml",
      "Intermech.Kernel.ObjectContentStatus.xml",
      "Intermech.Kernel.IMViewerObjects.xml",
      "Intermech.Kernel.TextData.xml",
      "Intermech.Kernel.PdfPrintCenter.xml",
      "Intermech.Search.VersionSelectionRules.AddingToDropdownList.xml",
      "Intermech.SpellCheck.xml",
      "Intermech.Search.CompositionSelectionContexts.xml"
    };
  }

  public void BeforeExecScript(IUserSession session, string scriptName)
  {
    if (!(scriptName == "1.23 Intermech.Kernel.BricsCAD.Documents.xml"))
      return;
    this.ChangeDBObjectTypeGuid(session, new Guid("97F64F25-62F4-4F6A-9170-33C1913A91F9"), new Guid("cadd9a0a-306c-11d8-b4e9-00304f19f545"));
    this.ChangeDBObjectTypeGuid(session, new Guid("C3FC00FE-2238-49F8-A78F-F9CBF6EDDF13"), new Guid("cadd9a0b-306c-11d8-b4e9-00304f19f545"));
  }

  public void AfterExecScript(IUserSession session, string scriptName)
  {
  }

  public void AfterExecAllScripts(IUserSession session)
  {
    this.ClearInvalidFileValue(session, new Guid("cad00727-306c-11d8-b4e9-00304f19f545"), "Intermech.Navigator.dll");
    this.ClearInvalidFileValue(session, new Guid("cad00720-306c-11d8-b4e9-00304f19f545"), "Intermech.Navigator.dll");
  }

  private void ChangeDBObjectTypeGuid(IUserSession session, Guid oldGuid, Guid newGuid)
  {
    IDBObjectType objectType = session.GetObjectType(oldGuid, false);
    if (objectType == null)
      return;
    try
    {
      ObjectTypeProperties propertiesStructure = objectType.PropertiesStructure with
      {
        ObjectTypeGuid = newGuid
      };
      objectType.PropertiesStructure = propertiesStructure;
    }
    catch (Exception ex)
    {
      this._eventLogHelper.AddToTrace(ExceptionServices.GetExtendedExceptionText(ex, $"Ошибка изменения глобального идентификатора метаданных с '{oldGuid}' на '{newGuid}'."), Intermech.Consts.traceError, string.Empty);
    }
  }

  private void ClearInvalidFileValue(IUserSession session, Guid objectGuid, string invalidFileName)
  {
    IDBObject dbObject = session.GetObject(objectGuid, false);
    if (dbObject == null)
      return;
    IDBAttribute attributeById = dbObject.GetAttributeByID(session.IdentHelper.FileAttributeID);
    if (attributeById == null || string.Compare(attributeById.AsString, invalidFileName, true) != 0)
      return;
    attributeById.Clear();
  }

  public static string GetUpdateFolderPath(IConfigurationManager configManager)
  {
    IConfiguration configuration = (configManager.Open("Updates") ?? throw new Exception("В конфигурации сервера не найдена секция с настройками автообновления ")).Open("UpdatesFolder");
    DirectoryInfo directoryInfo = configuration != null && configuration.HasProperty("Location") ? new DirectoryInfo(KernelUpdate.PathNormalizer.Normalize(configuration.GetProperty("Location"))) : throw new Exception("В конфигурации не указана папка со скриптами автообновления ");
    return directoryInfo.Exists ? directoryInfo.FullName : throw new Exception("Папки со скриптами автообновления, указанной в конфигурации, не существует ");
  }

  private bool NeedUpdate(IDbManager dbManager, IEventLogHelper eventLogHelper, int version)
  {
    object obj = dbManager.ExecuteScalar("SELECT F_VERSION_ID FROM IMS_DBVERSION WHERE F_MODULE_NAME = 'KERNEL'");
    if (obj != null)
    {
      if (obj != DBNull.Value)
      {
        try
        {
          int int32 = Convert.ToInt32(obj);
          if (int32 < version)
          {
            this._eventID = eventLogHelper.AddEvent(0L, 0L, 14, 0L, LocalizationHolder.rm.GetString("Kernel_798"), string.Format(LocalizationHolder.rm.GetString("DBVersionUpdating"), (object) version), ActionType.Execute, EventlogRecordType.Information, 0L, EnvironmentConsts.MachineName, (IUserSession) null);
            if (AdminUtilsService.ServerRunMode == ServerRunModes.Console)
              Console.WriteLine(LocalizationHolder.rm.GetString("DBVersionUpdating"), (object) version);
            eventLogHelper?.AddToTrace(string.Format(LocalizationHolder.rm.GetString("DBVersionUpdating"), (object) version), Intermech.Consts.traceAlways, string.Empty);
          }
          return int32 < version;
        }
        catch (Exception ex)
        {
          eventLogHelper?.AddToTrace(LocalizationHolder.rm.GetString("Kernel_910") + ex.Message, Intermech.Consts.traceAlways, string.Empty);
        }
      }
    }
    return false;
  }

  private bool UpdateVersion(IDbManager dbManager, IEventLogHelper eventLogHelper, int version)
  {
    try
    {
      dbManager.ExecuteNonQuery($"UPDATE IMS_DBVERSION SET F_VERSION_ID = {version} WHERE F_MODULE_NAME = 'KERNEL'");
      if (this._eventID > 0L)
      {
        eventLogHelper.CloseEvent(this._eventID, EventlogRecordType.Information, string.Format(LocalizationHolder.rm.GetString("DBVersionUpdating"), (object) version), (IUserSession) null);
        this._eventID = 0L;
      }
      return true;
    }
    catch (Exception ex)
    {
      eventLogHelper?.AddToTrace(LocalizationHolder.rm.GetString("Kernel_911") + ex.Message, Intermech.Consts.traceAlways, string.Empty);
    }
    return false;
  }

  public bool IsNeedUpdateModule(
    IDbManager dbManager,
    IEventLogHelper eventLogHelper,
    string moduleName,
    string moduleCaption,
    int version)
  {
    object obj = dbManager.ExecuteScalar("SELECT F_VERSION_ID FROM IMS_DBVERSION WHERE F_MODULE_NAME = :moduleName", dbManager.Parameter(nameof (moduleName), (object) moduleName));
    if (obj == null || obj == DBNull.Value)
    {
      dbManager.ExecuteNonQuery("INSERT INTO IMS_DBVERSION (F_MODULE_NAME, F_VERSION_ID) VALUES (:moduleName, :verID)", dbManager.Parameter(nameof (moduleName), (object) moduleName), dbManager.Parameter("verID", (object) (version - 1)));
      obj = (object) (version - 1);
    }
    try
    {
      int int32 = Convert.ToInt32(obj);
      if (int32 < version)
      {
        string str = $"Обновляется база данных модуля {moduleCaption} до версии {version}...";
        this._eventID = eventLogHelper.AddEvent(0L, 0L, 14, 0L, LocalizationHolder.rm.GetString("Kernel_798"), str, ActionType.Execute, EventlogRecordType.Information, 0L, EnvironmentConsts.MachineName, (IUserSession) null);
        if (AdminUtilsService.ServerRunMode == ServerRunModes.Console)
          Console.WriteLine(str);
        eventLogHelper?.AddToTrace(str, Intermech.Consts.traceAlways, string.Empty);
      }
      return int32 < version;
    }
    catch (Exception ex)
    {
      eventLogHelper?.AddToTrace($"Ошибка получения текущей версии модуля {moduleCaption}: {ex.Message}", Intermech.Consts.traceAlways, string.Empty);
      return false;
    }
  }

  public void UpdateModuleVersion(
    IDbManager dbManager,
    IEventLogHelper eventLogHelper,
    string moduleName,
    string moduleCaption,
    int version)
  {
    try
    {
      dbManager.ExecuteNonQuery("UPDATE IMS_DBVERSION SET F_VERSION_ID = :verID WHERE F_MODULE_NAME = :moduleName", dbManager.Parameter("verID", (object) version), dbManager.Parameter(nameof (moduleName), (object) moduleName));
      if (this._eventID <= 0L)
        return;
      eventLogHelper.CloseEvent(this._eventID, EventlogRecordType.Information, $"Обновляется база данных модуля {moduleCaption} до версии {version}...", (IUserSession) null);
      this._eventID = 0L;
    }
    catch (Exception ex)
    {
      eventLogHelper?.AddToTrace($"Ошибка обновления версии модуля {moduleCaption}: {ex.Message}", Intermech.Consts.traceAlways, string.Empty);
    }
  }

  private bool CheckColumns(
    DataTable table,
    List<KernelUpdate.IMSColumn> columns,
    bool onlyThisColumns)
  {
    if (table == null || columns == null || columns.Count == 0 || onlyThisColumns && table.Columns.Count != columns.Count)
      return false;
    List<KernelUpdate.IMSColumn> imsColumnList = new List<KernelUpdate.IMSColumn>((IEnumerable<KernelUpdate.IMSColumn>) columns);
    for (int index = 0; index < table.Columns.Count; ++index)
    {
      DataColumn column = table.Columns[index];
      KernelUpdate.IMSColumn imsColumn = new KernelUpdate.IMSColumn(column.ColumnName, column.DataType);
      if (imsColumnList.IndexOf(imsColumn) < 0 & onlyThisColumns)
        return false;
      imsColumnList.Remove(imsColumn);
    }
    return imsColumnList.Count == 0;
  }

  public void PatchKernelMetadata(UserSession session)
  {
    int version1 = 31 /*0x1F*/;
    if (this.NeedUpdate(session.DataManager, session.EventLogHelper, version1))
    {
      int nameId = session.IdentHelper.NameID;
      int attributeId = session.IdentHelper.GetAttributeID("cad008d8-306c-11d8-b4e9-00304f19f545");
      foreach (DataRow row in (InternalDataCollectionBase) session.DataManager.ExecuteDataTable("SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = " + session.IdentHelper.GetObjectTypeID("cad00861-306c-11d8-b4e9-00304f19f545").ToString()).Rows)
      {
        IDBObject dbObject = session.GetObject(Convert.ToInt64(row[0]), false);
        if (dbObject != null)
        {
          DBAttribute byId1 = dbObject.Attributes.FindByID(nameId) as DBAttribute;
          IDBAttribute byId2 = dbObject.Attributes.FindByID(attributeId);
          if (byId1 != null)
          {
            if (byId2 != null)
            {
              try
              {
                byId1.SetCalculatedValue(byId2.Value, true);
              }
              catch (Exception ex)
              {
                session.EventLogHelper.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_1145"), row[0], (object) ex.Message), Intermech.Consts.traceAlways, string.Empty);
              }
            }
          }
        }
      }
      this.UpdateVersion(session.DataManager, session.EventLogHelper, version1);
    }
    int version2 = 55;
    if (this.NeedUpdate(session.DataManager, session.EventLogHelper, version2))
    {
      IDBAttributeType attributeType = session.GetAttributeType(new Guid("cadd94ce-306c-11d8-b4e9-00304f19f545"));
      int objectTypeId1 = session.IdentHelper.GetObjectTypeID("cad00133-306c-11d8-b4e9-00304f19f545");
      int objectTypeId2 = session.IdentHelper.GetObjectTypeID("cad00132-306c-11d8-b4e9-00304f19f545");
      foreach (DataRow dataRow in session.DBCache.GetTable("IMS_OBJECT_TYPES").Select())
      {
        int int32 = Convert.ToInt32(dataRow["F_ATTRIBUTE_ID"]);
        if ((Convert.ToInt32(dataRow["F_OPTIONS"]) & 64 /*0x40*/) == 64 /*0x40*/ || int32 == objectTypeId1 || int32 == objectTypeId2)
        {
          IDBObjectType objectType = session.GetObjectType(Convert.ToInt32(dataRow["F_OBJECT_TYPE"]));
          if (objectType.Attributes.GetAttributeByID(attributeType.AttributeID) == null)
          {
            Attribute4ObjectTypeProperties attrProperties = new Attribute4ObjectTypeProperties(attributeType.AttributeID, objectType.ObjectType, InheritModes.Private, RequiredModes.Manual, string.Empty, attributeType.Computed, attributeType.Formula, UniqueValueModes.NotUnique, attributeType.LevelID, attributeType.DefaultValue, OptimizationModes.Write, attributeType.IsContent, attributeType.Options, attributeType.Mask, 0, 0);
            (objectType.Attributes as IDBAttribute4ObjectTypeCollection).Create(attrProperties);
          }
        }
      }
      this.UpdateVersion(session.DataManager, session.EventLogHelper, version2);
    }
    int version3 = 56;
    if (this.NeedUpdate(session.DataManager, session.EventLogHelper, version3))
    {
      IDBObjectCollection objectCollection1 = session.GetObjectCollection(session.IdentHelper.UsersTypeID);
      IDBObjectCollection objectCollection2 = session.GetObjectCollection(new Guid("cadd94e2-306c-11d8-b4e9-00304f19f545"));
      DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(new Guid("cad015c9-306c-11d8-b4e9-00304f19f545"), RelationalOperators.AttributeExists, (object) null, LogicalOperators.NONE, 0)
      }, new object[1]{ (object) -2 });
      DataTable dataTable = objectCollection1.Select(paramSet);
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        IDBAttribute attributeByGuid = session.GetObject(Convert.ToInt64(dataTable.Rows[index][0])).GetAttributeByGuid(new Guid("cad015c9-306c-11d8-b4e9-00304f19f545"));
        if (attributeByGuid != null)
        {
          object[] values = attributeByGuid.Values;
          IDBObject dbObject = objectCollection2.Create();
          dbObject.Attributes.AddAttribute(attributeByGuid.AttributeID, false, values);
          dbObject.GetAttributeByGuid(new Guid("cadd91f5-306c-11d8-b4e9-00304f19f545")).AsInteger = Convert.ToInt64(dataTable.Rows[index][0]);
          dbObject.CommitCreation(false);
          attributeByGuid.Delete((long) Intermech.Consts.PurgeMode);
        }
      }
      int attributeId = session.GetAttributeType(new Guid("cad015c9-306c-11d8-b4e9-00304f19f545")).AttributeID;
      try
      {
        session.GetObjectType(session.IdentHelper.UsersTypeID).Attributes.GetAttributeByID(attributeId).Delete(0L);
      }
      catch
      {
      }
      this.UpdateVersion(session.DataManager, session.EventLogHelper, version3);
    }
    int version4 = 401;
    if (this.IsNeedUpdateModule(session.DataManager, session.EventLogHelper, "KERNEL.SESSION", "KERNEL.SESSION", version4))
    {
      DataTable dataTable = session.GetObjectCollection(session.IdentHelper.ProjectsTypeID).Select(new DBRecordSetParams((ConditionStructure[]) null, new object[3]
      {
        (object) -2,
        (object) -80,
        (object) session.IdentHelper.SecurityLevelID
      }));
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        int int32 = Convert.ToInt32(dataTable.Rows[index][2]);
        if (Convert.ToInt32(dataTable.Rows[index][1]) != int32 && session.GetObject(Convert.ToInt64(dataTable.Rows[index][0])) is DBProjectObject dbProjectObject)
        {
          dbProjectObject.DoSetAccessLevel(int32);
          dbProjectObject.SetProjectAccessLevel(int32);
        }
      }
      this.UpdateModuleVersion(session.DataManager, session.EventLogHelper, "KERNEL.SESSION", "KERNEL.SESSION", version4);
    }
    int version5 = 501;
    if (this.IsNeedUpdateModule(session.DataManager, session.EventLogHelper, "KERNEL.SESSION", "KERNEL.SESSION", version5))
    {
      IDBObjectCollection objectCollection = session.GetObjectCollection(session.IdentHelper.WorkspaceTypeID);
      bool showPersonalObjects = session.ShowPersonalObjects;
      session.ShowPersonalObjects = true;
      try
      {
        DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
        {
          new ConditionStructure(-7, RelationalOperators.Equal, (object) session.IdentHelper.WorkspaceTypeID, LogicalOperators.NONE, 0, true)
        }, new ColumnDescriptor[2]
        {
          new ColumnDescriptor((object) -2, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
          new ColumnDescriptor((object) -8, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0)
        });
        foreach (DataRow row in (InternalDataCollectionBase) objectCollection.Select(paramSet).Rows)
        {
          if (session.GetObject(Convert.ToInt64(row[0])) is IServerWorkspace serverWorkspace)
            serverWorkspace.CreateSamples();
        }
      }
      finally
      {
        session.ShowPersonalObjects = showPersonalObjects;
      }
      this.UpdateModuleVersion(session.DataManager, session.EventLogHelper, "KERNEL.SESSION", "KERNEL.SESSION", version5);
    }
    int version6 = 502;
    if (this.IsNeedUpdateModule(session.DataManager, session.EventLogHelper, "KERNEL.SESSION", "KERNEL.SESSION", version6))
    {
      this.ModifyAttrGroupNoSystemGuids503(session, session.DataManager);
      session.DBCache.ReloadTables((IUserSession) session, session.DataManager, "IMS_ATTR_GROUPS");
      this.UpdateModuleVersion(session.DataManager, session.EventLogHelper, "KERNEL.SESSION", "KERNEL.SESSION", version6);
    }
    int version7 = 503;
    if (this.IsNeedUpdateModule(session.DataManager, session.EventLogHelper, "KERNEL.SESSION", "KERNEL.SESSION", version7))
    {
      DataTable dataTable = session.GetObjectCollection(new Guid("cad002ac-306c-11d8-b4e9-00304f19f545")).Select(new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(new Guid("cad002cd-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) 8, LogicalOperators.NONE, 0)
      }, new object[1]{ (object) -2 }));
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        IDBObject dbObject = session.GetObject(Convert.ToInt64(dataTable.Rows[index][0]), false);
        if (dbObject != null && dbObject.GetAttributeByGuid(new Guid("cad002cd-306c-11d8-b4e9-00304f19f545")) is DBAdditionalAttribute attributeByGuid)
        {
          attributeByGuid.ValidatingOn = false;
          attributeByGuid.AsInteger = -1L;
        }
      }
      this.UpdateModuleVersion(session.DataManager, session.EventLogHelper, "KERNEL.SESSION", "KERNEL.SESSION", version7);
    }
    int version8 = 504;
    if (this.IsNeedUpdateModule(session.DataManager, session.EventLogHelper, "KERNEL.SESSION", "KERNEL.SESSION", version8))
    {
      this.PatchOfficeData(session);
      this.UpdateModuleVersion(session.DataManager, session.EventLogHelper, "KERNEL.SESSION", "KERNEL.SESSION", version8);
    }
    int version9 = 505;
    if (this.IsNeedUpdateModule(session.DataManager, session.EventLogHelper, "KERNEL.SESSION", "KERNEL.SESSION", version9))
    {
      this.PatchCopiesDocumentID(session);
      this.UpdateModuleVersion(session.DataManager, session.EventLogHelper, "KERNEL.SESSION", "KERNEL.SESSION", version9);
    }
    if (session.GetAttributeType(new Guid("cadd9b65-306c-11d8-b4e9-00304f19f545"), false) is DBAttributeType attributeType1)
      attributeType1.GUID = new Guid("424e4095-d402-44f1-b3c8-379ac6e60e8c");
    if (session.GetAttributeType(new Guid("cadd9b66-306c-11d8-b4e9-00304f19f545"), false) is DBAttributeType attributeType2)
      attributeType2.GUID = new Guid("cae0d224-f228-401f-bff4-8395e19c05a8");
    int version10 = 600;
    if (session.DataManager.DataProvider.Name == "PostgreSQL")
    {
      if (this.IsNeedUpdateModule(session.DataManager, session.EventLogHelper, "KERNEL.POSTGRE", "KERNEL.POSTGRE", version10))
      {
        IBlobStoragesPool service = ServerServices.GetService(typeof (IBlobStoragesPool)) as IBlobStoragesPool;
        DataTable dataTable = session.GetObjectCollection(new Guid("cad00014-306c-11d8-b4e9-00304f19f545")).Select(new DBRecordSetParams((ConditionStructure[]) null, new object[2]
        {
          (object) -2,
          (object) -50
        }));
        for (int index = 0; index < dataTable.Rows.Count; ++index)
        {
          IBlobStorage storage = service.GetStorage(Convert.ToInt64(dataTable.Rows[index][0]), (IUserSession) session);
          try
          {
            if (storage.DataManager.DataProvider.Name == "PostgreSQL")
            {
              if (storage is DBBlobStorage)
              {
                if (storage.DataManager.ExecuteDataTable($"SELECT * FROM {storage.StorageName} WHERE F_FILE_ID = 0").Columns.IndexOf("f_oid") < 0)
                  storage.DataManager.ExecuteNonQuery($"ALTER TABLE {storage.StorageName} ADD F_OID INTEGER DEFAULT 0 NOT NULL");
              }
            }
          }
          finally
          {
            service.ReleaseStorage(storage);
          }
        }
      }
      this.UpdateModuleVersion(session.DataManager, session.EventLogHelper, "KERNEL.POSTGRE", "KERNEL.POSTGRE", version10);
    }
    int version11 = 601;
    if (this.IsNeedUpdateModule(session.DataManager, session.EventLogHelper, "KERNEL.SESSION", "KERNEL.SESSION", version11))
    {
      this.PatchScripts((IUserSession) session);
      this.UpdateModuleVersion(session.DataManager, session.EventLogHelper, "KERNEL.SESSION", "KERNEL.SESSION", version11);
    }
    if (this.IsNeedUpdateModule(session.DataManager, session.EventLogHelper, "KERNEL.PORTAL", "KERNEL.PORTAL", 200))
    {
      try
      {
        this.CorrectPortalMetadata(session);
      }
      catch (Exception ex)
      {
        session.EventLogHelper.AddToTrace("Ошибка корректировки метаданных, используемых порталом: " + ex.Message, Intermech.Consts.traceAlways, string.Empty);
      }
      this.UpdateModuleVersion(session.DataManager, session.EventLogHelper, "KERNEL.PORTAL", "KERNEL.PORTAL", 200);
    }
    int version12 = 700;
    if (this.IsNeedUpdateModule(session.DataManager, session.EventLogHelper, "KERNEL.SESSION", "KERNEL.SESSION", version12))
    {
      try
      {
        this.PatchSession700(session);
      }
      catch (Exception ex)
      {
        session.EventLogHelper.AddToTrace("Ошибка выключения флага синхронного завершения изменений для связей между типами объектов с атрибутом Файл: " + ex.Message, Intermech.Consts.traceAlways, string.Empty);
      }
      this.UpdateModuleVersion(session.DataManager, session.EventLogHelper, "KERNEL.SESSION", "KERNEL.SESSION", version12);
    }
    int version13 = 701;
    if (this.IsNeedUpdateModule(session.DataManager, session.EventLogHelper, "KERNEL.SESSION", "KERNEL.SESSION", version13))
    {
      try
      {
        this.ClearOldFilesCache(session);
      }
      catch (Exception ex)
      {
        session.EventLogHelper.AddToTrace("Ошибка очистки кэша сервера приложений в IsolatedStorage: " + ex.Message, Intermech.Consts.traceAlways, string.Empty);
      }
      this.UpdateModuleVersion(session.DataManager, session.EventLogHelper, "KERNEL.SESSION", "KERNEL.SESSION", version13);
    }
    int version14 = 703;
    if (this.IsNeedUpdateModule(session.DataManager, session.EventLogHelper, "KERNEL.SESSION", "KERNEL.SESSION", version14))
    {
      IDBObjectType objectType1 = session.GetObjectType(new Guid("cad0034a-306c-11d8-b4e9-00304f19f545"), false);
      if (objectType1 != null && objectType1.ObjectTypeShortName != "ПИ")
      {
        IDBObjectType objectType2 = session.GetObjectType(new Guid("cad0034b-306c-11d8-b4e9-00304f19f545"), false);
        if (objectType1 != null)
        {
          objectType1.ObjectTypeShortName = "ПИ_ПР11";
          objectType2.ObjectTypeShortName = "ПР";
          objectType1.ObjectTypeShortName = "ПИ";
        }
      }
      this.UpdateModuleVersion(session.DataManager, session.EventLogHelper, "KERNEL.SESSION", "KERNEL.SESSION", version14);
    }
    if (this.IsNeedUpdateModule(session.DataManager, session.EventLogHelper, "KERNEL.PORTAL", "KERNEL.PORTAL", 201))
    {
      try
      {
        this.CorrectPublicationNecessaryAttribute(session);
      }
      catch (Exception ex)
      {
        session.EventLogHelper.AddToTrace("Ошибка корректировки атрибута  Необходима публикация на портал: " + ex.Message, Intermech.Consts.traceAlways, string.Empty);
      }
      this.UpdateModuleVersion(session.DataManager, session.EventLogHelper, "KERNEL.PORTAL", "KERNEL.PORTAL", 201);
    }
    int version15 = 704;
    if (this.IsNeedUpdateModule(session.DataManager, session.EventLogHelper, "KERNEL.SESSION", "KERNEL.SESSION", version15))
    {
      try
      {
        this.SetUserID4LoginEvent(session.DataManager);
      }
      catch (Exception ex)
      {
        session.EventLogHelper.AddToTrace("Ошибка обновления : " + ex.Message, Intermech.Consts.traceAlways, string.Empty);
      }
      this.UpdateModuleVersion(session.DataManager, session.EventLogHelper, "KERNEL.SESSION", "KERNEL.SESSION", version15);
    }
    if (this.IsNeedUpdateModule(session.DataManager, session.EventLogHelper, "KERNEL.UPDATER", "KERNEL.UPDATER", 600))
    {
      try
      {
        this.PatchIMV_A850(session);
      }
      catch (Exception ex)
      {
        session.EventLogHelper.AddToTrace("Ошибка корректировки атрибута F_STRING_VALUE в таблицах IMV_A: " + ex.Message, Intermech.Consts.traceAlways, string.Empty);
      }
      this.UpdateModuleVersion(session.DataManager, session.EventLogHelper, "KERNEL.UPDATER", "KERNEL.UPDATER", 600);
    }
    if (!this.IsNeedUpdateModule(session.DataManager, session.EventLogHelper, "KERNEL.UPDATER", "KERNEL.UPDATER", 701))
      return;
    try
    {
      this.PatchMemoBlob850(session);
    }
    catch (Exception ex)
    {
      session.EventLogHelper.AddToTrace("Ошибка корректировки максимальной длины строковой части двоичных и мемо-полей до 850 символов в представлениях данных: " + ex.Message, Intermech.Consts.traceAlways, string.Empty);
    }
    this.UpdateModuleVersion(session.DataManager, session.EventLogHelper, "KERNEL.UPDATER", "KERNEL.UPDATER", 701);
  }

  private void PatchMemoBlob850_IMVX(
    IDbManager dbManager,
    string attrsTableName,
    string viewSuffix,
    string typeIDfld)
  {
    string fldType = !(dbManager.DataProvider.Name == "Sql") ? (!(dbManager.DataProvider.Name == "Oracle") ? "varchar(850)" : "NVARCHAR2(850)") : "String850_DEF";
    DataTable dataTable = dbManager.ExecuteDataTable($"SELECT A1.{typeIDfld}, A1.F_INVIEW, A2.F_ATTRIBUTE_ID FROM {attrsTableName} A1, IMS_ATTRIBUTES A2 WHERE A2.F_ATTRIBUTE_ID = A1.F_ATTRIBUTE_ID AND A2.F_ATTRIBUTE_TYPE IN ({5}, {10}, {11})");
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      string tableName = $"IMV_{viewSuffix}{Convert.ToInt32(dataTable.Rows[index][0]).ToString()}";
      OptimizationModes int32 = (OptimizationModes) Convert.ToInt32(dataTable.Rows[index][1]);
      if ((int32 & OptimizationModes.Read) == OptimizationModes.Read || (int32 & OptimizationModes.Seek) == OptimizationModes.Seek)
        dbManager.ExecuteNonQuery(dbManager.DataProvider.GetModifyColumnSQL(tableName, "F" + dataTable.Rows[index][2].ToString(), fldType));
    }
  }

  private void PatchMemoBlob850(UserSession session)
  {
    this.PatchMemoBlob850_IMVX(session.DataManager, "IMS_ATTR4OBJ_TYPES", "O", "F_OBJECT_TYPE");
    this.PatchMemoBlob850_IMVX(session.DataManager, "IMS_ATTR4RELATION_TYPES", "R", "F_RELATION_TYPE");
  }

  private void PatchIMV_A850(UserSession session)
  {
    IDbManager dataManager = session.DataManager;
    string fldType;
    if (dataManager.DataProvider.Name == "Sql")
    {
      fldType = "String850_DEF";
    }
    else
    {
      if (dataManager.DataProvider.Name == "Oracle")
        return;
      fldType = "varchar(850)";
    }
    DataTable dataTable = dataManager.ExecuteDataTable("SELECT F_OBJECT_TYPE, F_OPTIONS FROM IMS_OBJECT_TYPES");
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      string str = "IMV_A" + Convert.ToInt32(dataTable.Rows[index][0]).ToString();
      ObjectTypeOptions int32 = (ObjectTypeOptions) Convert.ToInt32(dataTable.Rows[index][1]);
      if ((int32 & ObjectTypeOptions.LocalObjectType) == ObjectTypeOptions.LocalObjectType)
      {
        if ((int32 & ObjectTypeOptions.AttributesIndex) == ObjectTypeOptions.AttributesIndex && dataManager.DataProvider.Name == "Sql")
          dataManager.DataProvider.DropAttrValuesIndex(str, dataManager);
        dataManager.ExecuteNonQuery(dataManager.DataProvider.GetModifyColumnSQL(str, "F_STRING_VALUE", fldType));
        if ((int32 & ObjectTypeOptions.AttributesIndex) == ObjectTypeOptions.AttributesIndex && dataManager.DataProvider.Name == "Sql")
          dataManager.DataProvider.CreateAttrValuesIndex(str, dataManager);
      }
    }
  }

  private void SetUserID4LoginEvent(IDbManager dataManager)
  {
    dataManager.ExecuteNonQuery("update IMS_EVENTLOG SET F_USER_ID = F_OBJECT_ID WHERE F_USER_ID = 0 AND F_OBJECT_ID <> 0 AND F_EVENT_TYPE = 20");
    dataManager.ExecuteNonQuery("update IMS_EVENTLOG_ARC SET F_USER_ID = F_OBJECT_ID WHERE F_USER_ID = 0 AND F_OBJECT_ID <> 0 AND F_EVENT_TYPE = 20");
  }

  internal static void DeleteHistoryRelations(UserSession session, string logFileName)
  {
    DataTable dataTable = session.DataManager.ExecuteDataTable("select F_PRJLINK_ID from IMS_RELATIONS where F_DELETE_DATE is not null");
    if (dataTable.Rows.Count <= 0)
      return;
    session.EventLog.AddToTrace($"Found {dataTable.Rows.Count} relations with not null delete date...", Intermech.Consts.traceAlways, logFileName);
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      if (session.GetRelation(Convert.ToInt64(dataTable.Rows[index][0]), false) is DBRelation relation)
        relation.DeleteWithoutCheck((long) Intermech.Consts.PurgeMode);
    }
  }

  private void CorrectPublicationNecessaryAttribute(UserSession session)
  {
    IDBAttributeType attributeType = session.GetAttributeType(PortalConsts.attributePublicationNecessary);
    session.DataManager.ExecuteNonQuery($"UPDATE IMS_ATTR4OBJ_TYPES SET F_DEFAULT_VALUE = 0 where F_ATTRIBUTE_ID = {attributeType.AttributeID}");
    session.DBCache.ReloadTables((IUserSession) session, session.DataManager, "IMS_ATTR4OBJ_TYPES");
  }

  private void CorrectPortalMetadata(UserSession session)
  {
    IDBObjectType objectType = session.GetObjectType(PortalConsts.objtypeReceipt);
    objectType.Attributes.GetAttributeByGUID(new Guid("cadd95f0-306c-11d8-b4e9-00304f19f545"), false)?.Delete(0L);
    IDBAttributeType4 attributeByGuid1 = objectType.Attributes.GetAttributeByGUID(PortalConsts.attributeReceiptFile);
    if ((attributeByGuid1.Options & AttributeOptions.DisableNulls) == AttributeOptions.DisableNulls)
      attributeByGuid1.Options &= ~AttributeOptions.DisableNulls;
    IDBAttributeType4 attributeByGuid2 = session.GetObjectType(PortalConsts.objtypePacket).Attributes.GetAttributeByGUID(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545"), false);
    if (attributeByGuid2 == null)
      return;
    attributeByGuid2.UniqueMode = UniqueValueModes.NotUnique;
  }

  private void ClearOldFilesCache(UserSession session)
  {
    IsolatedStorageFile userStoreForDomain = IsolatedStorageFile.GetUserStoreForDomain();
    string[] fileNames = userStoreForDomain.GetFileNames("*");
    for (int index = 0; index < fileNames.Length; ++index)
    {
      try
      {
        userStoreForDomain.DeleteFile(fileNames[index]);
      }
      catch (Exception ex)
      {
        session.EventLogHelper.AddToTrace($"Ошибка удаления файла {fileNames[index]} из изолированного хранилища: {ex.Message}", Intermech.Consts.traceWarning, "");
      }
    }
  }

  private void PatchSession700(UserSession session)
  {
    foreach (DataRow row in (InternalDataCollectionBase) session.DataManager.ExecuteDataTable("SELECT * FROM IMS_TYPES_APPLICABILITY WHERE F_OPTIONS <> 0").Rows)
    {
      if ((Convert.ToInt32(row["F_OPTIONS"]) & 32 /*0x20*/) == 32 /*0x20*/)
      {
        IDBObjectType objectType1 = session.GetObjectType(Convert.ToInt32(row["F_INOBJECT_TYPE"]), false);
        IDBObjectType objectType2 = session.GetObjectType(Convert.ToInt32(row["F_OBJECT_TYPE"]), false);
        if (objectType1 != null && objectType2 != null && (objectType1.Attributes.GetAttributeByID(session.IdentHelper.FileAttributeID) != null || objectType2.Attributes.GetAttributeByID(session.IdentHelper.FileAttributeID) != null))
        {
          session.EventLogHelper.AddToTrace($"Найден включенный флаг синхронного завершения изменений на связи '{session.GetRelationType(Convert.ToInt32(row["F_RELATION_TYPE"])).Description}' между типами объектов '{objectType1.ObjectTypeName}' и '{objectType2.ObjectTypeName}' с атрибутом 'Файл'. Выключаем...", Intermech.Consts.traceAlways, string.Empty);
          session.GetRelationsApplicabilityCollection().GetApplicability(Convert.ToInt32(row["F_APPLICABILITY_ID"])).Options &= ~ApplicabilityOptions.SyncCheckin;
        }
      }
    }
  }

  private void PatchCopiesDocumentID(UserSession userSession)
  {
    Guid attributeGuid = new Guid("cadd9359-306c-11d8-b4e9-00304f19f545");
    DataTable dataTable = userSession.GetObjectCollection(new Guid("cadd9364-306c-11d8-b4e9-00304f19f545")).Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(attributeGuid, RelationalOperators.Less, (object) 0, LogicalOperators.NONE, 0)
    }, new object[1]{ (object) -2 }));
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      IDBObject dbObject = userSession.GetObject(Convert.ToInt64(dataTable.Rows[index][0]), false);
      if (dbObject != null && dbObject.GetAttributeByGuid(attributeGuid) is DBAdditionalAttribute attributeByGuid)
        attributeByGuid.DirectSetValue("F_INTEGER_VALUE", (object) Math.Abs(attributeByGuid.AsInteger));
    }
  }

  private void PatchScripts(IUserSession session)
  {
    DataTable dataTable = session.GetObjectCollection(new Guid("cad0036a-306c-11d8-b4e9-00304f19f545")).SelectWithLocalObjects(new DBRecordSetParams((ConditionStructure[]) null, new object[1]
    {
      (object) -2
    }));
    try
    {
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        IDBObject dbObject = session.GetObject(Convert.ToInt64(row[0]), false);
        if (dbObject != null)
        {
          string nameInMessages = dbObject.NameInMessages;
          if (dbObject.CheckoutBy != 0L)
          {
            session.EventLog.AddToTrace($"Ошибка патча текста {nameInMessages}. Объект взят на изменение пользователем.", Intermech.Consts.traceAlways, string.Empty);
          }
          else
          {
            IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad00366-306c-11d8-b4e9-00304f19f545"));
            string newScript;
            if (attributeByGuid != null && !attributeByGuid.IsNull && this.ReplaceOldScriptFormat(attributeByGuid.Value.ToString(), out newScript))
            {
              bool flag = false;
              if (dbObject.ObjectModifyMode == ObjectModifyModes.Checkout)
              {
                dbObject = dbObject.CheckOut(false);
                if (dbObject == null)
                {
                  session.EventLog.AddToTrace($"Ошибка патча текста {nameInMessages}. Объект невозможно взять на изменение.", Intermech.Consts.traceAlways, string.Empty);
                  continue;
                }
                attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad00366-306c-11d8-b4e9-00304f19f545"));
                flag = true;
              }
              attributeByGuid.Value = (object) newScript;
              if (flag)
                dbObject.CheckIn();
            }
          }
        }
      }
    }
    catch (Exception ex)
    {
      session.EventLog.AddToTrace($"Ошибка патча базы SESSION.KERNEL.601: {ex.Message}", Intermech.Consts.traceAlways, string.Empty);
    }
  }

  private bool ReplaceOldScriptFormat(string oldScript, out string newScript)
  {
    Match match = new Regex("public\\s+(static|void)\\s+(static|void)\\s+Execute\\s*\\(").Match(oldScript);
    if (match.Success)
    {
      newScript = oldScript.Replace(match.Value, "public ICSharpScriptContext ScriptContext {get; private set;}\r\n\r\n\tpublic void Execute(");
      return true;
    }
    newScript = (string) null;
    return false;
  }

  private void PatchOfficeData(UserSession session)
  {
    int objectTypeId1 = session.IdentHelper.GetObjectTypeID("cad00070-306c-11d8-b4e9-00304f19f545");
    session.StartTransaction();
    try
    {
      Guid guid = new Guid("cadd9282-306c-11d8-b4e9-00304f19f545");
      if (MetaDataHelper.ExistsAttributeType(guid))
      {
        int attributeId1 = MetaDataHelper.GetAttributeID((object) guid);
        int attributeId2 = MetaDataHelper.GetAttributeID((object) new Guid("cadd924a-306c-11d8-b4e9-00304f19f545"));
        if (MetaDataHelper.GetAttribute4ObjectType(objectTypeId1, attributeId1) != null)
        {
          DataTable dataTable = session.GetObjectCollection(objectTypeId1).Select(new DBRecordSetParams(new ConditionStructure[1]
          {
            new ConditionStructure(attributeId1, RelationalOperators.NotEmpty, (object) null, LogicalOperators.NONE, 0, true)
          }, new ColumnDescriptor[2]
          {
            new ColumnDescriptor((object) -2, SortOrders.NONE, 0),
            new ColumnDescriptor((object) attributeId1, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.NONE, 0)
          }));
          if (dataTable != null)
          {
            foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
            {
              if (session.GetObject(Convert.ToInt64(row[0]), false) is DBObject dbObject1)
              {
                dbObject1.ValidationRulesOn = false;
                (dbObject1.Attributes as DBAttributeCollection).ValidatingOn = false;
                IDBObject dbObject = session.GetObject(Convert.ToInt64(row[1]), false);
                if (dbObject != null)
                  (dbObject1.Attributes as DBAttributeCollection).AddAttribute(attributeId2, false, false, new object[1]
                  {
                    (object) dbObject.ObjectID
                  });
                dbObject1.Attributes.FindByID(attributeId1).Delete((long) Intermech.Consts.PurgeMode);
              }
            }
          }
        }
      }
      session.Commit();
    }
    catch
    {
      session.Rollback();
      throw;
    }
    session.StartTransaction();
    try
    {
      Guid guid = new Guid("cad014cb-306c-11d8-b4e9-00304f19f545");
      if (MetaDataHelper.ExistsAttributeType(guid))
      {
        int attributeId3 = MetaDataHelper.GetAttributeID((object) guid);
        int objectTypeId2 = MetaDataHelper.GetObjectTypeID(new Guid("cadd927d-306c-11d8-b4e9-00304f19f545"));
        int attributeId4 = MetaDataHelper.GetAttributeID((object) new Guid("cadd924b-306c-11d8-b4e9-00304f19f545"));
        IMSAttribute4ObjectType attribute4ObjectType = MetaDataHelper.GetAttribute4ObjectType(objectTypeId2, attributeId4);
        if (MetaDataHelper.GetAttribute4ObjectType(objectTypeId2, attributeId3) != null && attribute4ObjectType != null)
        {
          if (attribute4ObjectType.OptimizationMode == OptimizationModes.Write)
            session.GetObjectType(objectTypeId2).Attributes.GetAttributeByID(attributeId4).OptimizationMode = OptimizationModes.Seek;
          DataTable dataTable = session.GetObjectCollection(objectTypeId2).Select(new DBRecordSetParams(new ConditionStructure[2]
          {
            new ConditionStructure(attributeId4, RelationalOperators.NotExistsOrEmpty, (object) null, LogicalOperators.AND, 0, true),
            new ConditionStructure(attributeId3, RelationalOperators.NotEmpty, (object) null, LogicalOperators.NONE, 0, true)
          }, new ColumnDescriptor[2]
          {
            new ColumnDescriptor((object) -2, SortOrders.NONE, 0),
            new ColumnDescriptor((object) attributeId3, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 0)
          }));
          if (dataTable != null)
          {
            foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
            {
              if (session.GetObject(Convert.ToInt64(row[0]), false) is DBObject dbObject)
              {
                dbObject.ValidationRulesOn = false;
                (dbObject.Attributes as DBAttributeCollection).ValidatingOn = false;
                (dbObject.Attributes as DBAttributeCollection).AddAttribute(attributeId4, false, false, new object[1]
                {
                  row[1]
                });
              }
            }
          }
        }
      }
      session.Commit();
    }
    catch
    {
      session.Rollback();
      throw;
    }
  }

  private void ModifyAttrGroupNoSystemGuids503(UserSession session, IDbManager dbManager)
  {
    this.ModifyAttrGroupGuid(session, dbManager, "b2cc378c-0cd1-4290-823d-a4dd21ae490a", "cadd972e-306c-11d8-b4e9-00304f19f545", "Атрибуты для проведения изменений");
    this.ModifyAttrGroupGuid(session, dbManager, "5ff39386-95b7-41bf-a534-f7ec9893e85c", "cadd972f-306c-11d8-b4e9-00304f19f545", "Атрибуты ядра системы");
    this.ModifyAttrGroupGuid(session, dbManager, "347c69ae-5db9-438f-90f0-f83f77f5a799", "cadd9730-306c-11d8-b4e9-00304f19f545", "Атрибуты AVS");
    this.ModifyAttrGroupGuid(session, dbManager, "4e9ccf64-9129-49a5-92fb-5d2628538c0e", "cadd9731-306c-11d8-b4e9-00304f19f545", "Атрибуты экспертной системы");
  }

  private void ModifyAttrGroupGuid(
    UserSession session,
    IDbManager dbManager,
    string oldGuid,
    string newGuid,
    string groupName)
  {
    object obj1 = dbManager.ExecuteScalar("SELECT F_GROUP_ID FROM IMS_ATTR_GROUPS WHERE F_GUID = :guid_ID", dbManager.Parameter("guid_ID", (object) new Guid(oldGuid)));
    if (obj1 != null && obj1 != DBNull.Value)
    {
      dbManager.ExecuteNonQuery("UPDATE IMS_ATTR_GROUPS SET F_GUID = :guid_ID WHERE F_GROUP_ID = :grpID", dbManager.Parameter("guid_ID", (object) new Guid(newGuid)), dbManager.Parameter("grpID", (object) Convert.ToInt32(obj1)));
    }
    else
    {
      if (session == null)
        return;
      object obj2 = dbManager.ExecuteScalar("SELECT F_GROUP_ID FROM IMS_ATTR_GROUPS WHERE F_GUID = :guid_ID", dbManager.Parameter("guid_ID", (object) new Guid(newGuid)));
      if (obj2 != null && obj2 != DBNull.Value)
        return;
      session.GetAttributesGroupCollection().Create(groupName, string.Empty, string.Empty, string.Empty, new Guid(newGuid));
    }
  }

  public void DeleteAttributeFromType(
    UserSession session,
    IEventLogHelper eventLogHelper,
    Guid attributeGuid,
    Guid typeGuid,
    bool deleteInstances,
    bool throwException)
  {
    IDbManager dataManager = session.DataManager;
    if (!(session.GetObjectType(typeGuid, false) is DBAttributableType attributableType))
      attributableType = session.GetRelationType(typeGuid, false) as DBAttributableType;
    if (attributableType != null)
    {
      try
      {
        IDBAttributeType4 attributeByGuid = attributableType.Attributes.GetAttributeByGUID(attributeGuid);
        if (attributeByGuid == null)
          return;
        long DeleteMode;
        if (deleteInstances)
        {
          DeleteMode = (long) Intermech.Consts.DeleteInstances;
          attributeByGuid.IsContent = false;
        }
        else
          DeleteMode = 0L;
        attributeByGuid.Delete(DeleteMode);
      }
      catch (Exception ex)
      {
        eventLogHelper.AddToTrace($"Ошибка удаления атрибута {attributeGuid.ToString()} у типа {attributableType.ObjectName}: {ex.Message}", Intermech.Consts.traceAlways, string.Empty);
        if (!throwException)
          return;
        throw;
      }
    }
    else
    {
      string str = $"Тип объектов или связей с гуидом {typeGuid.ToString()} не найден.";
      eventLogHelper.AddToTrace(str, Intermech.Consts.traceAlways, string.Empty);
      if (throwException)
        throw new KernelException(str);
    }
  }

  public void PatchStoredProc(
    string procName,
    IDbManager dbManager,
    IEventLogHelper eventLogHelper)
  {
    string path = Path.Combine(KernelUpdate.GetUpdateFolderPath(ServerServices.GetService(typeof (IConfigurationManager)) as IConfigurationManager), procName);
    try
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (string readAllLine in File.ReadAllLines(path))
        stringBuilder.AppendLine(readAllLine);
      string commandText = stringBuilder.ToString();
      dbManager.ExecuteNonQuery(commandText);
    }
    catch (Exception ex)
    {
      eventLogHelper.AddToTrace($"Error executing file {path}: {ex.Message}", Intermech.Consts.traceAlways, string.Empty);
    }
  }

  public void PatchStoredProcs(
    string[] storedProcs,
    IDbManager dbManager,
    IEventLogHelper eventLogHelper,
    int version_id)
  {
    if (!this.NeedUpdate(dbManager, eventLogHelper, version_id))
      return;
    dbManager.DataProvider.NoLockMode = false;
    for (int index = 0; index < storedProcs.Length; ++index)
    {
      try
      {
        string path2 = !(dbManager.DataProvider.Name == "Sql") ? (!(dbManager.DataProvider.Name == "Oracle") ? (!(dbManager.DataProvider.Name == "Linter") ? storedProcs[index] + ".postgre.sql" : storedProcs[index] + ".ln.sql") : storedProcs[index] + ".ora.sql") : storedProcs[index] + ".ms.sql";
        string path = Path.Combine(KernelUpdate.GetUpdateFolderPath(ServerServices.GetService(typeof (IConfigurationManager)) as IConfigurationManager), path2);
        StringBuilder stringBuilder = new StringBuilder();
        foreach (string readAllLine in File.ReadAllLines(path))
          stringBuilder.AppendLine(readAllLine);
        string commandText = stringBuilder.ToString();
        dbManager.ExecuteNonQuery(commandText);
      }
      catch (Exception ex)
      {
        eventLogHelper.AddToTrace($"Error executing file {storedProcs[index]}: {ex.Message}", Intermech.Consts.traceAlways, string.Empty);
      }
    }
    dbManager.DataProvider.NoLockMode = true;
    this.UpdateVersion(dbManager, eventLogHelper, version_id);
  }

  public void PatchDatabase(IDbManager dbManager, IEventLogHelper eventLogHelper)
  {
    object obj1 = dbManager.ExecuteScalar("SELECT F_VERSION_ID FROM IMS_DBVERSION WHERE F_MODULE_NAME = 'KERNEL'");
    if (obj1 != null && obj1 != DBNull.Value)
    {
      int int32 = Convert.ToInt32(obj1);
      object obj2 = (object) System.Configuration.ConfigurationManager.AppSettings.Get("MinDBVersion");
      if (obj2 != null)
      {
        int num;
        try
        {
          num = Convert.ToInt32(obj2);
        }
        catch
        {
          num = 0;
        }
        if (int32 < num)
          throw new KernelException($"Сервер приложений не может работать с версией базы данных {int32}, т.к. в конфигурации сервера запрещена работа с базой данных ниже версии {num}.");
      }
      if (710 > int32)
      {
        object obj3 = dbManager.ExecuteScalar("SELECT F_VALUE FROM IMS_CONFIGS WHERE F_MODULE_NAME = :moduleName AND F_USER_ID = 0 AND F_SECTION_ID = :sectID AND F_PARAM_NAME = :parName", dbManager.Parameter("moduleName", (object) "KERNEL"), dbManager.Parameter("sectID", (object) "COMMON"), dbManager.Parameter("parName", (object) "DisableDBPatch"));
        if (obj3 != null && obj3 != DBNull.Value && obj3.ToString() == "1")
          throw new KernelException($"Сервер приложений не может работать с версией базы данных {int32}, т.к. в настройках IPS запрещена возможность обновления версии этой базы данных, а данному серверу приложений требуется версия базы данных {710}.");
      }
    }
    if (dbManager.DataProvider.Name == "Sql")
    {
      try
      {
        dbManager.ExecuteNonQuery($"ALTER DATABASE [{dbManager.DataProvider.DatabaseName}] SET AUTO_SHRINK OFF");
      }
      catch
      {
      }
    }
    int version1 = 12;
    if (this.NeedUpdate(dbManager, eventLogHelper, version1))
    {
      if (dbManager.DataProvider.Name == "Sql")
      {
        dbManager.ExecuteNonQuery("CREATE TABLE IMS_IMBASE_INDEX (F_ATTRIBUTE_ID INTEGER NOT NULL,F_TEXT         MaximumString_DEF NULL,F_HASHTEXT     MaximumString_DEF NULL,F_GROUP        ObjectName_DEF NULL,F_LINK_ID      INTEGER NOT NULL,F_TABLE_ID     INTEGER NOT NULL,F_TABKEY       INTEGER NOT NULL,F_CATALOG_ID   INTEGER NOT NULL,F_CLASSIVKEY   MaximumString_DEF NOT NULL)");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_IMBASE_INDEX ADD  FOREIGN KEY (F_ATTRIBUTE_ID) REFERENCES IMS_ATTRIBUTES ON DELETE CASCADE");
      }
      else if (dbManager.DataProvider.Name == "Oracle")
      {
        dbManager.ExecuteNonQuery("CREATE TABLE IMS_IMBASE_INDEX (F_ATTRIBUTE_ID INTEGER NOT NULL,F_TEXT         NVARCHAR2(450) NULL,F_HASHTEXT     NVARCHAR2(450) NULL,F_GROUP        NVARCHAR2(255) NULL,F_LINK_ID      INTEGER NOT NULL,F_TABLE_ID     INTEGER NOT NULL,F_TABKEY       INTEGER NOT NULL,F_CATALOG_ID   INTEGER NOT NULL,F_CLASSIVKEY   NVARCHAR2(450) NOT NULL)");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_IMBASE_INDEX ADD  ( FOREIGN KEY (F_ATTRIBUTE_ID) REFERENCES IMS_ATTRIBUTES ON DELETE CASCADE)");
      }
      dbManager.DataProvider.CreateIndex("IMS_IMBASE_INDEX", "F_TEXT", dbManager, SortOrders.ASC);
      dbManager.DataProvider.CreateIndex("IMS_IMBASE_INDEX", "F_HASHTEXT", dbManager, SortOrders.ASC);
      dbManager.DataProvider.CreateIndex("IMS_IMBASE_INDEX", "F_ATTRIBUTE_ID", dbManager, SortOrders.ASC);
      dbManager.DataProvider.CreateIndex("IMS_IMBASE_INDEX", "F_CATALOG_ID", dbManager, SortOrders.ASC);
      dbManager.DataProvider.CreateIndex("IMS_IMBASE_INDEX", "F_LINK_ID", dbManager, SortOrders.ASC);
      dbManager.DataProvider.CreateIndex("IMS_IMBASE_INDEX", "F_TABLE_ID", dbManager, SortOrders.ASC);
      dbManager.DataProvider.CreateIndex("IMS_IMBASE_INDEX", "F_CLASSIVKEY", dbManager, SortOrders.ASC);
      this.UpdateVersion(dbManager, eventLogHelper, version1);
    }
    int version2 = 13;
    if (this.NeedUpdate(dbManager, eventLogHelper, version2))
    {
      string commandText = "SELECT * FROM IMS_VERSIONS_CONTEXT WHERE F_CONTEXT_ID = 0";
      List<KernelUpdate.IMSColumn> columns = new List<KernelUpdate.IMSColumn>(4);
      columns.Add(new KernelUpdate.IMSColumn("F_CONTEXT_ID", typeof (long)));
      columns.Add(new KernelUpdate.IMSColumn("F_ID", typeof (long)));
      columns.Add(new KernelUpdate.IMSColumn("F_OBJECT_ID", typeof (long)));
      columns.Add(new KernelUpdate.IMSColumn("F_MODIFICATION_ID", typeof (long)));
      bool flag;
      try
      {
        flag = !this.CheckColumns(dbManager.ExecuteDataTable(commandText), columns, false);
      }
      catch
      {
        flag = true;
      }
      if (dbManager.DataProvider.Name == "Sql")
      {
        if (flag)
        {
          try
          {
            dbManager.ExecuteNonQuery(sc_12780.ssp_appserver_12781());
          }
          catch
          {
          }
          dbManager.ExecuteNonQuery(sc_12780.ssp_appserver_12782() + "F_CONTEXT_ID          BigNumber_DEF NOT NULL,F_ID                  BigNumber_DEF NOT NULL,F_OBJECT_ID           BigNumber_DEF NOT NULL,F_MODIFICATION_ID     BigNumber_DEF NOT NULL)");
          dbManager.ExecuteNonQuery("ALTER TABLE IMS_VERSIONS_CONTEXT ADD PRIMARY KEY CLUSTERED (F_CONTEXT_ID, F_MODIFICATION_ID, F_ID)");
        }
      }
      else if (dbManager.DataProvider.Name == "Oracle")
      {
        if (flag)
        {
          try
          {
            dbManager.ExecuteNonQuery(sc_12780.ssp_appserver_12783());
          }
          catch
          {
          }
          dbManager.ExecuteNonQuery(sc_12780.ssp_appserver_12784() + "F_CONTEXT_ID      NUMBER NOT NULL,F_ID              NUMBER NOT NULL,F_OBJECT_ID       NUMBER NOT NULL,F_MODIFICATION_ID NUMBER NOT NULL)");
          dbManager.ExecuteNonQuery(sc_12780.ssp_appserver_12785());
        }
      }
      if (flag)
      {
        dbManager.DataProvider.CreateIndex("IMS_VERSIONS_CONTEXT", "F_CONTEXT_ID", dbManager, SortOrders.ASC);
        dbManager.DataProvider.CreateIndex("IMS_VERSIONS_CONTEXT", "F_MODIFICATION_ID", dbManager, SortOrders.ASC);
      }
      this.UpdateVersion(dbManager, eventLogHelper, version2);
    }
    int version3 = 15;
    if (this.NeedUpdate(dbManager, eventLogHelper, version3))
    {
      try
      {
        dbManager.ExecuteNonQuery("DROP TABLE IMS_FILENAMES");
      }
      catch
      {
      }
      if (dbManager.DataProvider.Name == "Sql")
      {
        dbManager.ExecuteNonQuery("CREATE TABLE IMS_FILENAMES (F_FILENAME MaximumString_DEF NOT NULL,F_KEY   BigNumber_DEF NOT NULL,F_ID     BigNumber_DEF NOT NULL)");
        dbManager.ExecuteNonQuery("CREATE INDEX IMS_FILENAMES_KEY ON IMS_FILENAMES (F_KEY)");
        dbManager.ExecuteNonQuery("CREATE INDEX IMS_FILENAMES_FILENAME ON IMS_FILENAMES (F_FILENAME)");
        dbManager.ExecuteNonQuery("CREATE INDEX IMS_FILENAMES_ID ON IMS_FILENAMES (F_ID)");
      }
      else if (dbManager.DataProvider.Name == "Oracle")
      {
        dbManager.ExecuteNonQuery("CREATE TABLE IMS_FILENAMES (F_FILENAME NVARCHAR2(450) NOT NULL,F_KEY   INTEGER NOT NULL,F_ID     INTEGER NOT NULL)");
        dbManager.ExecuteNonQuery("CREATE INDEX IMS_FILENAMES_KEY ON IMS_FILENAMES (F_KEY)");
        dbManager.ExecuteNonQuery("CREATE INDEX IMS_FILENAMES_FILENAME ON IMS_FILENAMES (F_FILENAME)");
        dbManager.ExecuteNonQuery("CREATE INDEX IMS_FILENAMES_ID ON IMS_FILENAMES (F_ID)");
      }
      this.UpdateVersion(dbManager, eventLogHelper, version3);
    }
    int version4 = 16 /*0x10*/;
    if (this.NeedUpdate(dbManager, eventLogHelper, version4))
    {
      if (dbManager.DataProvider.Name == "Sql")
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_LC_STEPS ADD F_OPTIONS INT DEFAULT 0 NOT NULL");
      else if (dbManager.DataProvider.Name == "Oracle")
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_LC_STEPS ADD (F_OPTIONS INTEGER DEFAULT 0 NOT NULL)");
      this.UpdateVersion(dbManager, eventLogHelper, version4);
    }
    int version5 = 17;
    if (this.NeedUpdate(dbManager, eventLogHelper, version5))
    {
      this.PatchDB17(dbManager, "IMS_OBJECTS", eventLogHelper);
      this.PatchDB17(dbManager, "IMS_OBJECTS_VIEW", eventLogHelper);
      DataTable dataTable = dbManager.ExecuteDataTable(sc_12780.ssp_appserver_12786());
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        string tableName = "IMV_O" + dataTable.Rows[index][0].ToString();
        try
        {
          dbManager.ExecuteScalar(string.Format(sc_12780.ssp_appserver_12787(), (object) tableName));
          this.PatchDB17(dbManager, tableName, eventLogHelper);
        }
        catch
        {
        }
      }
      dbManager.ExecuteNonQuery(string.Format(sc_12780.ssp_appserver_12788(), (object) dbManager.DataProvider.Now, (object) "IMS_ATTRIBUTES"));
      this.UpdateVersion(dbManager, eventLogHelper, version5);
    }
    int version6 = 18;
    if (this.NeedUpdate(dbManager, eventLogHelper, version6))
    {
      if (dbManager.DataProvider.Name == "Sql")
      {
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_OBJECTS ADD F_SITE_ID VARCHAR(2) NULL");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_OBJECTS_VIEW ADD F_SITE_ID VARCHAR(2) NULL");
      }
      else if (dbManager.DataProvider.Name == "Oracle")
      {
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_OBJECTS ADD F_SITE_ID VARCHAR2(2) NULL");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_OBJECTS_VIEW ADD F_SITE_ID VARCHAR2(2) NULL");
      }
      DataTable dataTable = dbManager.ExecuteDataTable(sc_12780.ssp_appserver_12789());
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        string str = "IMV_O" + dataTable.Rows[index][0].ToString();
        try
        {
          dbManager.ExecuteScalar(string.Format(sc_12780.ssp_appserver_12790(), (object) str));
          if (dbManager.DataProvider.Name == "Sql")
            dbManager.ExecuteNonQuery($"ALTER TABLE {str} ADD F_SITE_ID VARCHAR(2) NULL");
          else if (dbManager.DataProvider.Name == "Oracle")
            dbManager.ExecuteNonQuery($"ALTER TABLE {str} ADD F_SITE_ID VARCHAR2(2) NULL");
        }
        catch
        {
        }
      }
      dbManager.ExecuteNonQuery(string.Format(sc_12780.ssp_appserver_12791(), (object) dbManager.DataProvider.Now, (object) "IMS_ATTRIBUTES"));
      this.UpdateVersion(dbManager, eventLogHelper, version6);
    }
    int version7 = 19;
    if (this.NeedUpdate(dbManager, eventLogHelper, version7))
    {
      try
      {
        dbManager.ExecuteNonQuery(sc_12780.ssp_appserver_12792());
      }
      catch
      {
      }
      if (dbManager.DataProvider.Name == "Sql")
      {
        dbManager.ExecuteNonQuery(sc_12780.ssp_appserver_12793() + "F_OBJECT_ID    BigNumber_DEF NOT NULL,F_ATTRIBUTE_ID INTEGER NOT NULL,F_INLIST_ID    INTEGER NOT NULL,F_TOOBJECT_ID  BigNumber_DEF NOT NULL)");
        dbManager.ExecuteNonQuery(sc_12780.ssp_appserver_12794() + "(F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID)");
      }
      else if (dbManager.DataProvider.Name == "Oracle")
      {
        dbManager.ExecuteNonQuery(sc_12780.ssp_appserver_12795() + "F_OBJECT_ID    INTEGER NOT NULL,F_ATTRIBUTE_ID INTEGER NOT NULL,F_INLIST_ID    INTEGER NOT NULL,F_TOOBJECT_ID  INTEGER NOT NULL)");
        dbManager.ExecuteNonQuery(sc_12780.ssp_appserver_12796());
      }
      dbManager.ExecuteNonQuery(sc_12780.ssp_appserver_12797());
      dbManager.ExecuteNonQuery(sc_12780.ssp_appserver_12798());
      dbManager.ExecuteNonQuery(sc_12780.ssp_appserver_12799());
      dbManager.ExecuteNonQuery(sc_12780.ssp_appserver_12800());
      dbManager.ExecuteNonQuery(string.Format($"{sc_12780.ssp_appserver_12801()}{sc_12780.ssp_appserver_12802()}{sc_12780.ssp_appserver_12803()} exists(select * from IMS_OBJECTS O where O.F_OBJECT_ID = AO.F_OBJECT_ID)", (object) 8));
      this.UpdateVersion(dbManager, eventLogHelper, version7);
    }
    int version8 = 20;
    if (this.NeedUpdate(dbManager, eventLogHelper, version8))
    {
      if (dbManager.DataProvider.Name == "Sql")
      {
        dbManager.ExecuteNonQuery(sc_12780.ssp_appserver_12804() + "F_ATTRIBUTE_ID            INTEGER NOT NULL,F_OBJECT_TYPE             INTEGER NOT NULL,F_RELATION_TYPE           INTEGER NOT NULL,F_PARAM_NAME          ObjectShortName_DEF NOT NULL,F_INLIST_ID           SmallNumber_DEF NOT NULL,F_CATEGORY_TYPE       SmallNumber_DEF NOT NULL,F_VALUE               MaximumString_DEF NULL)");
        dbManager.ExecuteNonQuery(sc_12780.ssp_appserver_12805());
      }
      else if (dbManager.DataProvider.Name == "Oracle")
      {
        dbManager.ExecuteNonQuery(sc_12780.ssp_appserver_12806() + "F_ATTRIBUTE_ID            INTEGER NOT NULL,F_OBJECT_TYPE             INTEGER NOT NULL,F_RELATION_TYPE           INTEGER NOT NULL,F_PARAM_NAME          NVARCHAR2(32) NOT NULL,F_INLIST_ID           INTEGER NOT NULL,F_CATEGORY_TYPE           INTEGER NOT NULL,F_VALUE               NVARCHAR2(450) NULL)");
        dbManager.ExecuteNonQuery(sc_12780.ssp_appserver_12807());
      }
      this.UpdateVersion(dbManager, eventLogHelper, version8);
    }
    int version9 = 21;
    if (this.NeedUpdate(dbManager, eventLogHelper, version9))
    {
      dbManager.ExecuteNonQuery("DROP TABLE IMS_TIMED_EVENTS");
      KernelUpdate.CreateTimedEventsTable(dbManager);
      this.UpdateVersion(dbManager, eventLogHelper, version9);
    }
    int version10 = 22;
    if (this.NeedUpdate(dbManager, eventLogHelper, version10))
    {
      KernelUpdate.RepairObjectLinksTable(dbManager, eventLogHelper);
      this.UpdateVersion(dbManager, eventLogHelper, version10);
    }
    int version11 = 23;
    if (this.NeedUpdate(dbManager, eventLogHelper, version11))
    {
      this.CreateSnapshotTables(dbManager, eventLogHelper);
      this.UpdateVersion(dbManager, eventLogHelper, version11);
    }
    int version12 = 24;
    if (this.NeedUpdate(dbManager, eventLogHelper, version12))
    {
      this.CreateMDExtensionsTriggers(dbManager, eventLogHelper);
      this.UpdateVersion(dbManager, eventLogHelper, version12);
    }
    int version13 = 25;
    if (this.NeedUpdate(dbManager, eventLogHelper, version13))
    {
      if (dbManager.DataProvider.Name == "Sql")
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_GUID ADD F_CHECKOUT_DATE datetime NULL");
      else if (dbManager.DataProvider.Name == "Oracle")
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_GUID ADD F_CHECKOUT_DATE DATE NULL");
      try
      {
        dbManager.ExecuteNonQuery("DROP TRIGGER IMS_OBJECT_ATTRS_DLT");
      }
      catch
      {
      }
      try
      {
        dbManager.ExecuteNonQuery("DROP TRIGGER IMS_OBJECT_ATTRS_INS");
      }
      catch
      {
      }
      try
      {
        dbManager.ExecuteNonQuery("DROP TRIGGER IMS_OBJECT_ATTRS_UPD");
      }
      catch
      {
      }
      this.UpdateVersion(dbManager, eventLogHelper, version13);
    }
    int version14 = 26;
    if (this.NeedUpdate(dbManager, eventLogHelper, version14))
    {
      if (!(dbManager.DataProvider.Name == "Sql") && dbManager.DataProvider.Name == "Oracle")
      {
        dbManager.ExecuteNonQuery("CREATE GLOBAL TEMPORARY TABLE IMS_TMP_INTEGER (F_KEY    INTEGER NOT NULL,F_VALUE  INTEGER NOT NULL) ON COMMIT DELETE ROWS");
        dbManager.ExecuteNonQuery("CREATE INDEX IMS_TMP_INT_KEY ON IMS_TMP_INTEGER (F_KEY)");
        dbManager.ExecuteNonQuery("CREATE INDEX IMS_TMP_INT_VAL ON IMS_TMP_INTEGER (F_VALUE)");
        dbManager.ExecuteNonQuery("CREATE GLOBAL TEMPORARY TABLE IMS_TMP_DOUBLE (F_KEY    INTEGER NOT NULL,F_VALUE  FLOAT NOT NULL) ON COMMIT DELETE ROWS");
        dbManager.ExecuteNonQuery("CREATE INDEX IMS_TMP_DBL_KEY ON IMS_TMP_DOUBLE (F_KEY)");
        dbManager.ExecuteNonQuery("CREATE INDEX IMS_TMP_DBL_VAL ON IMS_TMP_DOUBLE (F_VALUE)");
        dbManager.ExecuteNonQuery("CREATE GLOBAL TEMPORARY TABLE IMS_TMP_STRING (F_KEY    INTEGER NOT NULL,F_VALUE   NVARCHAR2(450) NULL) ON COMMIT DELETE ROWS");
        dbManager.ExecuteNonQuery("CREATE INDEX IMS_TMP_STR_KEY ON IMS_TMP_STRING (F_KEY)");
        dbManager.ExecuteNonQuery("CREATE INDEX IMS_TMP_STR_VAL ON IMS_TMP_STRING (F_VALUE)");
        dbManager.ExecuteNonQuery("CREATE GLOBAL TEMPORARY TABLE IMS_TMP_DATE (F_KEY    INTEGER NOT NULL,F_VALUE  DATE NOT NULL) ON COMMIT DELETE ROWS");
        dbManager.ExecuteNonQuery("CREATE INDEX IMS_TMP_DAT_KEY ON IMS_TMP_DATE (F_KEY)");
        dbManager.ExecuteNonQuery("CREATE INDEX IMS_TMP_DAT_VAL ON IMS_TMP_DATE (F_VALUE)");
      }
      this.UpdateVersion(dbManager, eventLogHelper, version14);
    }
    int version15 = 28;
    if (this.NeedUpdate(dbManager, eventLogHelper, version15))
    {
      this.PatchDB28(dbManager, eventLogHelper);
      this.UpdateVersion(dbManager, eventLogHelper, version15);
    }
    int version16 = 29;
    if (this.NeedUpdate(dbManager, eventLogHelper, version16))
    {
      dbManager.ExecuteNonQuery($"UPDATE IMS_METADATA SET F_MODIFY_DATE = {dbManager.DataProvider.Now} WHERE F_TABLE_NAME = '{"IMS_RELATION_TYPES"}'");
      this.UpdateVersion(dbManager, eventLogHelper, version16);
    }
    int version17 = 30;
    if (this.NeedUpdate(dbManager, eventLogHelper, version17))
    {
      this.PatchDB30(dbManager, eventLogHelper);
      this.UpdateVersion(dbManager, eventLogHelper, version17);
    }
    int version18 = 32 /*0x20*/;
    if (this.NeedUpdate(dbManager, eventLogHelper, version18))
    {
      if (dbManager.DataProvider.Name == "Sql")
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_ATTR_GROUPS ADD F_PARENT_ID INTEGER NOT NULL DEFAULT 0");
      else if (dbManager.DataProvider.Name == "Oracle")
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_ATTR_GROUPS ADD F_PARENT_ID INTEGER DEFAULT 0 NOT NULL");
      this.UpdateVersion(dbManager, eventLogHelper, version18);
    }
    int version19 = 33;
    if (this.NeedUpdate(dbManager, eventLogHelper, version19))
    {
      if (dbManager.DataProvider.Name == "Sql")
      {
        dbManager.ExecuteNonQuery("CREATE TABLE IMS_IMBASE_ATTRS (F_OBJECT_ID          BigNumber_DEF NOT NULL,F_ATTRIBUTE_ID       int NOT NULL)");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_IMBASE_ATTRS ADD PRIMARY KEY CLUSTERED (F_OBJECT_ID, F_ATTRIBUTE_ID)");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_IMBASE_ATTRS ADD FOREIGN KEY (F_ATTRIBUTE_ID) REFERENCES IMS_ATTRIBUTES");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_IMBASE_ATTRS ADD FOREIGN KEY (F_OBJECT_ID) REFERENCES IMS_OBJECTS ON DELETE CASCADE");
        dbManager.ExecuteNonQuery(sc_12780.ssp_appserver_12808() + "F_TABLE_ID\t\tBigNumber_DEF NOT NULL,F_OBJECT_ID\tBigNumber_DEF NOT NULL)");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_IMBASE_OBJ_LINKS ADD PRIMARY KEY CLUSTERED (F_TABLE_ID, F_OBJECT_ID)");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_IMBASE_OBJ_LINKS ADD FOREIGN KEY (F_TABLE_ID) REFERENCES IMS_OBJECTS (F_OBJECT_ID) ON DELETE CASCADE");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_IMBASE_OBJ_LINKS ADD FOREIGN KEY (F_OBJECT_ID) REFERENCES IMS_OBJECTS");
      }
      else if (dbManager.DataProvider.Name == "Oracle")
      {
        dbManager.ExecuteNonQuery("CREATE TABLE IMS_IMBASE_ATTRS (F_OBJECT_ID          INTEGER NOT NULL,F_ATTRIBUTE_ID       INTEGER NOT NULL)");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_IMBASE_ATTRS ADD (PRIMARY KEY (F_OBJECT_ID, F_ATTRIBUTE_ID))");
        dbManager.ExecuteNonQuery(sc_12780.ssp_appserver_12809());
        dbManager.ExecuteNonQuery(sc_12780.ssp_appserver_12810());
        dbManager.ExecuteNonQuery(sc_12780.ssp_appserver_12811() + "F_TABLE_ID    INTEGER NOT NULL,F_OBJECT_ID\tINTEGER NOT NULL)");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_IMBASE_OBJ_LINKS ADD (PRIMARY KEY (F_TABLE_ID, F_OBJECT_ID))");
        dbManager.ExecuteNonQuery(sc_12780.ssp_appserver_12812());
        dbManager.ExecuteNonQuery(sc_12780.ssp_appserver_12813());
      }
      this.UpdateVersion(dbManager, eventLogHelper, version19);
    }
    int version20 = 34;
    if (this.NeedUpdate(dbManager, eventLogHelper, version20))
    {
      this.CreateGlobalIndex(dbManager, eventLogHelper);
      this.UpdateVersion(dbManager, eventLogHelper, version20);
    }
    int version21 = 35;
    if (this.NeedUpdate(dbManager, eventLogHelper, version21))
    {
      if (dbManager.DataProvider.Name == "Sql")
      {
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_TIMED_EVENTS ADD F_EVENT_KIND INTEGER NOT NULL DEFAULT 0");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_TIMED_EVENTS ADD F_COMPUTER_NAME String40_DEF");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_TIMED_EVENTS ADD F_SCHEDULE MaximumString_DEF");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_TIMED_EVENTS ADD F_NAME MaximumString_DEF");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_TIMED_EVENTS ADD F_PREV_DATE datetime NULL");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_TIMED_EVENTS ADD F_ERROR_MSG MaximumString_DEF");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_TIMED_EVENTS ADD F_IMMEDIATE_RUN INTEGER NOT NULL DEFAULT 0");
      }
      else if (dbManager.DataProvider.Name == "Oracle")
      {
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_TIMED_EVENTS ADD F_EVENT_KIND INTEGER DEFAULT 0 NOT NULL");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_TIMED_EVENTS ADD F_COMPUTER_NAME VARCHAR2(40) NULL");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_TIMED_EVENTS ADD F_SCHEDULE NVARCHAR2(450) NULL");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_TIMED_EVENTS ADD F_NAME NVARCHAR2(450) NULL");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_TIMED_EVENTS ADD F_PREV_DATE DATE NULL");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_TIMED_EVENTS ADD F_ERROR_MSG NVARCHAR2(450) NULL");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_TIMED_EVENTS ADD F_IMMEDIATE_RUN INTEGER DEFAULT 0 NOT NULL");
      }
      dbManager.ExecuteNonQuery("CREATE INDEX IMS_TE_COMPUTER_NAME ON IMS_TIMED_EVENTS (F_COMPUTER_NAME)");
      dbManager.ExecuteNonQuery("CREATE INDEX IMS_TE_NAME ON IMS_TIMED_EVENTS (F_NAME)");
      this.UpdateVersion(dbManager, eventLogHelper, version21);
    }
    int version22 = 36;
    if (this.NeedUpdate(dbManager, eventLogHelper, version22))
    {
      if (dbManager.DataProvider.Name == "Sql")
      {
        dbManager.ExecuteNonQuery("CREATE TABLE IMS_IMH_INDEX (F_SOURCE_ID    INTEGER NOT NULL,F_CLASSIVKEY   MaximumString_DEF NOT NULL,F_ATTRIBUTE_ID INTEGER NOT NULL,F_LINK_ID      INTEGER NOT NULL,F_TABKEY       INTEGER NOT NULL,F_TEXT         MaximumString_DEF NULL,F_HASHTEXT     MaximumString_DEF NULL,F_INTEGER_VALUE INTEGER NULL,F_DOUBLE_VALUE  float NULL)");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_IMH_INDEX ADD  FOREIGN KEY (F_ATTRIBUTE_ID) REFERENCES IMS_ATTRIBUTES ON DELETE CASCADE");
      }
      else if (dbManager.DataProvider.Name == "Oracle")
      {
        dbManager.ExecuteNonQuery("CREATE TABLE IMS_IMH_INDEX (F_SOURCE_ID   INTEGER NOT NULL,F_CLASSIVKEY   NVARCHAR2(450) NOT NULL,F_ATTRIBUTE_ID INTEGER NOT NULL,F_LINK_ID      INTEGER NOT NULL,F_TABKEY       INTEGER NOT NULL,F_TEXT         NVARCHAR2(450) NULL,F_HASHTEXT     NVARCHAR2(450) NULL,F_INTEGER_VALUE INTEGER NULL,F_DOUBLE_VALUE  FLOAT NULL)");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_IMH_INDEX ADD  ( FOREIGN KEY (F_ATTRIBUTE_ID) REFERENCES IMS_ATTRIBUTES ON DELETE CASCADE)");
      }
      string format = "CREATE INDEX {0} ON {1} ({2})";
      string commandText1 = string.Format(format, (object) "IMS_IMH_INDEX_AI_IDX", (object) "IMS_IMH_INDEX", (object) $"F_ATTRIBUTE_ID {(object) SortOrders.ASC}, F_INTEGER_VALUE {(object) SortOrders.ASC}");
      dbManager.ExecuteNonQuery(commandText1);
      string commandText2 = string.Format(format, (object) "IMS_IMH_INDEX_AD_IDX", (object) "IMS_IMH_INDEX", (object) $"F_ATTRIBUTE_ID {(object) SortOrders.ASC}, F_DOUBLE_VALUE {(object) SortOrders.ASC}");
      dbManager.ExecuteNonQuery(commandText2);
      this.UpdateVersion(dbManager, eventLogHelper, version22);
    }
    int version23 = 37;
    if (this.NeedUpdate(dbManager, eventLogHelper, version23))
    {
      try
      {
        dbManager.ExecuteNonQuery("CREATE INDEX IMS_VCONTEXT_OBJECT_ID ON IMS_VERSIONS_CONTEXT (F_OBJECT_ID)");
      }
      catch
      {
      }
      this.UpdateVersion(dbManager, eventLogHelper, version23);
    }
    int version24 = 38;
    if (this.NeedUpdate(dbManager, eventLogHelper, version24))
    {
      if (dbManager.DataProvider.Name == "Sql")
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_IMH_INDEX ADD F_CLASS_NAME MaximumString_DEF NULL");
      else if (dbManager.DataProvider.Name == "Oracle")
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_IMH_INDEX ADD (F_CLASS_NAME NVARCHAR2(450) NULL)");
      this.UpdateVersion(dbManager, eventLogHelper, version24);
    }
    int version25 = 39;
    if (this.NeedUpdate(dbManager, eventLogHelper, version25))
    {
      if (dbManager.DataProvider.Name == "Sql")
      {
        dbManager.ExecuteNonQuery("CREATE TABLE IMS_ATTRFILTER_VALUE (F_OBJECT_ID    BigNumber_DEF NOT NULL,F_FILTER_ID    BigNumber_DEF NOT NULL,F_STRING_VALUE MaximumString_DEF NULL)");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_ATTRFILTER_VALUE ADD CONSTRAINT IMS_ATTRFILTER_PK PRIMARY KEY CLUSTERED (F_OBJECT_ID, F_FILTER_ID)");
      }
      else if (dbManager.DataProvider.Name == "Oracle")
      {
        dbManager.ExecuteNonQuery("CREATE TABLE IMS_ATTRFILTER_VALUE (F_OBJECT_ID    INTEGER NOT NULL,F_FILTER_ID    INTEGER NOT NULL,F_STRING_VALUE NVARCHAR2(450) NULL)");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_ATTRFILTER_VALUE ADD CONSTRAINT IMS_ATTRFILTER_PK PRIMARY KEY (F_OBJECT_ID, F_FILTER_ID)");
      }
      dbManager.ExecuteNonQuery("CREATE INDEX IMS_ATTRFILTER_VALUE_NDX ON IMS_ATTRFILTER_VALUE (F_STRING_VALUE)");
      this.UpdateVersion(dbManager, eventLogHelper, version25);
    }
    int version26 = 40;
    if (this.NeedUpdate(dbManager, eventLogHelper, version26))
    {
      dbManager.ExecuteNonQuery("CREATE INDEX IMS_SNAPSHOT_USER_NDX ON IMS_OBJ_SNAPSHOT (F_USER_ID)");
      this.UpdateVersion(dbManager, eventLogHelper, version26);
    }
    int version27 = 42;
    if (this.NeedUpdate(dbManager, eventLogHelper, version27))
    {
      if (dbManager.DataProvider.Name == "Sql")
      {
        dbManager.ExecuteNonQuery("CREATE TABLE IMS_TMP_INTEGER (F_KEY    INTEGER NOT NULL,F_VALUE  INTEGER NOT NULL)");
        dbManager.ExecuteNonQuery("CREATE INDEX IMS_TMP_INT_KEY ON IMS_TMP_INTEGER (F_KEY)");
        dbManager.ExecuteNonQuery("CREATE INDEX IMS_TMP_INT_VAL ON IMS_TMP_INTEGER (F_VALUE)");
        dbManager.ExecuteNonQuery("CREATE TABLE IMS_TMP_DOUBLE (F_KEY    INTEGER NOT NULL,F_VALUE  FLOAT NOT NULL)");
        dbManager.ExecuteNonQuery("CREATE INDEX IMS_TMP_DBL_KEY ON IMS_TMP_DOUBLE (F_KEY)");
        dbManager.ExecuteNonQuery("CREATE INDEX IMS_TMP_DBL_VAL ON IMS_TMP_DOUBLE (F_VALUE)");
        dbManager.ExecuteNonQuery("CREATE TABLE IMS_TMP_STRING (F_KEY    INTEGER NOT NULL,F_VALUE   NVARCHAR(450) NULL)");
        dbManager.ExecuteNonQuery("CREATE INDEX IMS_TMP_STR_KEY ON IMS_TMP_STRING (F_KEY)");
        dbManager.ExecuteNonQuery("CREATE INDEX IMS_TMP_STR_VAL ON IMS_TMP_STRING (F_VALUE)");
        dbManager.ExecuteNonQuery("CREATE TABLE IMS_TMP_DATE (F_KEY    INTEGER NOT NULL,F_VALUE  datetime NOT NULL)");
        dbManager.ExecuteNonQuery("CREATE INDEX IMS_TMP_DAT_KEY ON IMS_TMP_DATE (F_KEY)");
        dbManager.ExecuteNonQuery("CREATE INDEX IMS_TMP_DAT_VAL ON IMS_TMP_DATE (F_VALUE)");
      }
      this.UpdateVersion(dbManager, eventLogHelper, version27);
    }
    int version28 = 50;
    if (this.NeedUpdate(dbManager, eventLogHelper, version28))
      this.UpdateVersion(dbManager, eventLogHelper, version28);
    int version29 = 51;
    if (this.NeedUpdate(dbManager, eventLogHelper, version29))
    {
      try
      {
        dbManager.SetAdminCommandTimeout();
        if (dbManager.DataProvider.Name == "Sql")
          dbManager.ExecuteNonQuery("ALTER TABLE IMS_EVENTLOG ALTER COLUMN F_COMPUTER_NAME String40_DEF");
        else if (dbManager.DataProvider.Name == "Oracle")
          dbManager.ExecuteNonQuery("ALTER TABLE IMS_EVENTLOG MODIFY (F_COMPUTER_NAME NVARCHAR2(40) NULL)");
      }
      finally
      {
        dbManager.SetNormalCommandTimeout();
      }
      this.UpdateVersion(dbManager, eventLogHelper, version29);
    }
    int version30 = 52;
    if (this.NeedUpdate(dbManager, eventLogHelper, version30))
    {
      if (dbManager.DataProvider.Name == "Sql")
      {
        try
        {
          dbManager.ExecuteNonQuery("DROP INDEX IMS_TMP_INTEGER.IMS_TMP_INT_KEY");
        }
        catch
        {
        }
        try
        {
          dbManager.ExecuteNonQuery("DROP INDEX IMS_TMP_INTEGER.IMS_TMP_INT_VAL");
        }
        catch
        {
        }
        try
        {
          dbManager.ExecuteNonQuery("DROP INDEX IMS_TMP_DOUBLE.IMS_TMP_DBL_KEY");
        }
        catch
        {
        }
        try
        {
          dbManager.ExecuteNonQuery("DROP INDEX IMS_TMP_DOUBLE.IMS_TMP_DBL_VAL");
        }
        catch
        {
        }
        try
        {
          dbManager.ExecuteNonQuery("DROP INDEX IMS_TMP_STRING.IMS_TMP_STR_KEY");
        }
        catch
        {
        }
        try
        {
          dbManager.ExecuteNonQuery("DROP INDEX IMS_TMP_STRING.IMS_TMP_STR_VAL");
        }
        catch
        {
        }
        try
        {
          dbManager.ExecuteNonQuery("DROP INDEX IMS_TMP_DATE.IMS_TMP_DAT_KEY");
        }
        catch
        {
        }
        try
        {
          dbManager.ExecuteNonQuery("DROP INDEX IMS_TMP_DATE.IMS_TMP_DAT_VAL");
        }
        catch
        {
        }
      }
      else if (dbManager.DataProvider.Name == "Oracle")
      {
        try
        {
          dbManager.ExecuteNonQuery("DROP INDEX IMS_TMP_INT_KEY");
        }
        catch
        {
        }
        try
        {
          dbManager.ExecuteNonQuery("DROP INDEX IMS_TMP_INT_VAL");
        }
        catch
        {
        }
        try
        {
          dbManager.ExecuteNonQuery("DROP INDEX IMS_TMP_DBL_KEY");
        }
        catch
        {
        }
        try
        {
          dbManager.ExecuteNonQuery("DROP INDEX IMS_TMP_DBL_VAL");
        }
        catch
        {
        }
        try
        {
          dbManager.ExecuteNonQuery("DROP INDEX IMS_TMP_STR_KEY");
        }
        catch
        {
        }
        try
        {
          dbManager.ExecuteNonQuery("DROP INDEX IMS_TMP_STR_VAL");
        }
        catch
        {
        }
        try
        {
          dbManager.ExecuteNonQuery("DROP INDEX IMS_TMP_DAT_KEY");
        }
        catch
        {
        }
        try
        {
          dbManager.ExecuteNonQuery("DROP INDEX IMS_TMP_DAT_VAL");
        }
        catch
        {
        }
      }
      try
      {
        dbManager.ExecuteNonQuery("CREATE INDEX IMS_INDXRES_WORD_ID ON IMS_INDEX_RESULT (F_WORD_ID)");
      }
      catch
      {
      }
      try
      {
        dbManager.ExecuteNonQuery("CREATE INDEX IMS_TMP_INT_NDX ON IMS_TMP_INTEGER (F_KEY, F_VALUE)");
      }
      catch
      {
      }
      try
      {
        dbManager.ExecuteNonQuery("CREATE INDEX IMS_TMP_DBL_NDX ON IMS_TMP_DOUBLE (F_KEY, F_VALUE)");
      }
      catch
      {
      }
      try
      {
        dbManager.ExecuteNonQuery("CREATE INDEX IMS_TMP_STR_NDX ON IMS_TMP_STRING (F_KEY, F_VALUE)");
      }
      catch
      {
      }
      try
      {
        dbManager.ExecuteNonQuery("CREATE INDEX IMS_TMP_DAT_NDX ON IMS_TMP_DATE (F_KEY, F_VALUE)");
      }
      catch
      {
      }
      this.UpdateVersion(dbManager, eventLogHelper, version30);
    }
    int version31 = 53;
    if (this.NeedUpdate(dbManager, eventLogHelper, version31))
    {
      this.PatchDB53(dbManager, eventLogHelper);
      this.UpdateVersion(dbManager, eventLogHelper, version31);
    }
    int version32 = 54;
    if (this.NeedUpdate(dbManager, eventLogHelper, version32))
    {
      this.PatchDB54(dbManager, eventLogHelper);
      this.UpdateVersion(dbManager, eventLogHelper, version32);
    }
    int version33 = 57;
    if (this.NeedUpdate(dbManager, eventLogHelper, version33))
    {
      if (dbManager.DataProvider.Name == "Sql")
        dbManager.ExecuteNonQuery("CREATE TABLE IMS_TMP_GEN (F_WORD_ID                BigNumber_DEF NOT NULL IDENTITY(1,1), F_CREATED DATETIME DEFAULT GETUTCDATE())");
      else
        dbManager.ExecuteNonQuery("CREATE SEQUENCE IMS_TMP_GEN START WITH 1 INCREMENT BY 1 NOMAXVALUE MINVALUE 1 NOCYCLE CACHE 5 NOORDER");
      this.UpdateVersion(dbManager, eventLogHelper, version33);
    }
    int version34 = 58;
    if (this.NeedUpdate(dbManager, eventLogHelper, version34))
    {
      dbManager.BeginTransaction();
      try
      {
        if (dbManager.DataProvider.Name == "Sql")
        {
          dbManager.ExecuteNonQuery("CREATE TABLE IMS_IMBASE_INDEXES (F_CATALOG_ID   INTEGER NOT NULL,F_ATTRIBUTE_ID INTEGER NOT NULL,F_FLAG         INTEGER NOT NULL,F_TABLE_NAME   MaximumString_DEF NOT NULL,F_ATTRIBUTE_STATE  INTEGER NOT NULL)");
          dbManager.ExecuteNonQuery(sc_12780.ssp_appserver_12814());
        }
        else if (dbManager.DataProvider.Name == "Oracle")
        {
          dbManager.ExecuteNonQuery("CREATE TABLE IMS_IMBASE_INDEXES (F_CATALOG_ID   INTEGER NOT NULL,F_ATTRIBUTE_ID INTEGER NOT NULL,F_FLAG         INTEGER NOT NULL,F_TABLE_NAME   NVARCHAR2(30) NOT NULL,F_ATTRIBUTE_STATE  INTEGER NOT NULL)");
          dbManager.ExecuteNonQuery(sc_12780.ssp_appserver_12815());
        }
        dbManager.Commit();
      }
      catch (Exception ex)
      {
        dbManager.Rollback();
        throw ex;
      }
      this.UpdateVersion(dbManager, eventLogHelper, version34);
    }
    int version35 = 59;
    if (this.NeedUpdate(dbManager, eventLogHelper, version35))
    {
      if (dbManager.DataProvider.Name == "Sql")
      {
        dbManager.ExecuteNonQuery("IF (NOT EXISTS(SELECT st.* FROM sys.types st WHERE st.[name] = 'IMS_TMP_INTEGER_STRUCT'))BEGIN CREATE TYPE IMS_TMP_INTEGER_STRUCT AS TABLE (F_KEY INT, F_VALUE INT) END");
        dbManager.ExecuteNonQuery("CREATE PROCEDURE dbo.IMS_TMP_INTEGER_TVP (@ImportTable dbo.IMS_TMP_INTEGER_STRUCT READONLY) AS SET NOCOUNT ON INSERT INTO dbo.IMS_TMP_INTEGER (F_KEY, F_VALUE) SELECT\tF_KEY, F_VALUE FROM @ImportTable");
      }
      this.UpdateVersion(dbManager, eventLogHelper, version35);
    }
    int version36 = 400;
    if (this.NeedUpdate(dbManager, eventLogHelper, version36))
    {
      if (dbManager.DataProvider.Name == "Sql")
        dbManager.ExecuteNonQuery("CREATE PROCEDURE IMS_ADD_EVENTLOG_EX ( @inCATEGORY_TYPE SmallNumber_DEF, @inCATEGORY_ID BigNumber_DEF, @inOBJECT_ID BigNumber_DEF, @inRELATION_ID BigNumber_DEF, @inOBJECT_NAME ObjectName_DEF, @inUSER_ID BigNumber_DEF, @inCOMPUTER_NAME String40_DEF, @inNOTE MaximumString_DEF, @inEVENT_TYPE INT, @inAUDIT_TYPE INT, @inBEGIN_DATE DATETIME, @inEND_DATE DATETIME, @outEVENT_ID BigNumber_DEF OUTPUT )  AS set nocount on insert into IMS_EVENTLOG ( F_CATEGORY_TYPE, F_CATEGORY_ID, F_OBJECT_ID, F_RELATION_ID, F_OBJECT_NAME, F_USER_ID, F_COMPUTER_NAME, F_NOTE, F_EVENT_TYPE, F_AUDIT_TYPE, F_BEGIN_DATE, F_END_DATE ) values ( @inCATEGORY_TYPE, @inCATEGORY_ID, @inOBJECT_ID, @inRELATION_ID, @inOBJECT_NAME, @inUSER_ID, @inCOMPUTER_NAME, @inNOTE, @inEVENT_TYPE, @inAUDIT_TYPE, @inBEGIN_DATE, @inEND_DATE )  select @outEVENT_ID=@@IDENTITY");
      else if (dbManager.DataProvider.Name == "Oracle")
        dbManager.ExecuteNonQuery("CREATE OR REPLACE PROCEDURE IMS_ADD_EVENTLOG_EX ( inCATEGORY_TYPE IN INTEGER, inCATEGORY_ID IN INTEGER, inOBJECT_ID IN INTEGER, inRELATION_ID IN INTEGER, inOBJECT_NAME IN NVARCHAR2, inUSER_ID IN INTEGER, inCOMPUTER_NAME IN NVARCHAR2, inNOTE IN NVARCHAR2, inEVENT_TYPE IN INTEGER, inAUDIT_TYPE IN INTEGER, inBEGIN_DATE IN DATE, inEND_DATE IN DATE, outEVENT_ID OUT INTEGER )  AS BEGIN select IMS_EVENTLOG_GEN.NEXTVAL into outEVENT_ID FROM DUAL; insert into IMS_EVENTLOG ( F_EVENT_ID, F_CATEGORY_TYPE, F_CATEGORY_ID, F_OBJECT_ID, F_RELATION_ID, F_OBJECT_NAME, F_USER_ID, F_COMPUTER_NAME, F_NOTE, F_EVENT_TYPE, F_BEGIN_DATE, F_AUDIT_TYPE, F_END_DATE ) values ( outEVENT_ID, inCATEGORY_TYPE, inCATEGORY_ID, inOBJECT_ID, inRELATION_ID, inOBJECT_NAME, inUSER_ID, inCOMPUTER_NAME, inNOTE, inEVENT_TYPE, inBEGIN_DATE, inAUDIT_TYPE, inEND_DATE ); END;");
      this.UpdateVersion(dbManager, eventLogHelper, version36);
    }
    int version37 = 401;
    if (this.NeedUpdate(dbManager, eventLogHelper, version37))
    {
      if (dbManager.DataProvider.Name == "Sql")
      {
        try
        {
          dbManager.ExecuteNonQuery("DROP INDEX IMS_ATTR_HISTORY.IMS_HISTORY_INTEGER");
        }
        catch
        {
        }
        try
        {
          dbManager.ExecuteNonQuery("DROP INDEX IMS_ATTR_HISTORY.IMS_HISTORY_STRING");
        }
        catch
        {
        }
        try
        {
          dbManager.ExecuteNonQuery("DROP INDEX IMS_ATTR_HISTORY.IMS_HISTORY_DOUBLE");
        }
        catch
        {
        }
        try
        {
          dbManager.ExecuteNonQuery("DROP INDEX IMS_ATTR_HISTORY.IMS_HISTORY_DATE");
        }
        catch
        {
        }
      }
      this.UpdateVersion(dbManager, eventLogHelper, version37);
    }
    int version38 = 402;
    if (this.NeedUpdate(dbManager, eventLogHelper, version38))
    {
      this.PatchDB402(dbManager, eventLogHelper);
      this.UpdateVersion(dbManager, eventLogHelper, version38);
    }
    int version39 = 403;
    if (this.NeedUpdate(dbManager, eventLogHelper, version39))
    {
      dbManager.ExecuteNonQuery($"UPDATE IMS_METADATA SET F_MODIFY_DATE = {dbManager.DataProvider.Now} WHERE F_TABLE_NAME = '{"IMS_ATTRIBUTES"}'");
      this.UpdateVersion(dbManager, eventLogHelper, version39);
    }
    int version40 = 404;
    if (this.NeedUpdate(dbManager, eventLogHelper, version40))
    {
      this.Patch404(dbManager);
      this.UpdateVersion(dbManager, eventLogHelper, version40);
    }
    int version41 = 405;
    if (this.NeedUpdate(dbManager, eventLogHelper, version41))
    {
      this.PatchIndexes405(dbManager, eventLogHelper);
      this.UpdateVersion(dbManager, eventLogHelper, version41);
    }
    int version42 = 406;
    if (this.NeedUpdate(dbManager, eventLogHelper, version42))
    {
      if (dbManager.DataProvider.Name == "Sql")
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_LEVELS ADD F_STORAGE_ID BigNumber_DEF NOT NULL DEFAULT 0");
      else if (dbManager.DataProvider.Name == "Oracle")
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_LEVELS ADD F_STORAGE_ID INTEGER DEFAULT 0 NOT NULL");
      else
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_LEVELS ADD F_STORAGE_ID BIGINT DEFAULT 0 NOT NULL");
      this.UpdateVersion(dbManager, eventLogHelper, version42);
    }
    int version43 = 407;
    if (this.NeedUpdate(dbManager, eventLogHelper, version43))
    {
      dbManager.ExecuteNonQuery("UPDATE IMS_ATTR_GROUPS SET F_GUID = :guid1 WHERE F_GROUP_NAME = :gname1", dbManager.Parameter("guid1", (object) "cadd9596-306c-11d8-b4e9-00304f19f545"), dbManager.Parameter("gname1", (object) "Атрибуты генерации документов"));
      this.UpdateVersion(dbManager, eventLogHelper, version43);
    }
    int version44 = 411;
    if (this.NeedUpdate(dbManager, eventLogHelper, version44))
    {
      dbManager.ExecuteNonQuery("CREATE INDEX IMS_IMBASELNK_OBJECT_ID ON IMS_IMBASE_OBJ_LINKS (F_OBJECT_ID)");
      dbManager.ExecuteNonQuery("CREATE INDEX IMS_IMBASELNK_TABLE_ID ON IMS_IMBASE_OBJ_LINKS (F_TABLE_ID)");
      dbManager.ExecuteNonQuery("CREATE INDEX IMS_PRJTEAM_PROJECT_ID ON IMS_PROJECT_TEAM (F_PROJECT_ID)");
      dbManager.ExecuteNonQuery("CREATE INDEX IMS_PRJTEAM_USER_ID ON IMS_PROJECT_TEAM (F_USER_ID)");
      this.UpdateVersion(dbManager, eventLogHelper, version44);
    }
    int version45 = 412;
    if (this.NeedUpdate(dbManager, eventLogHelper, version45))
      this.UpdateVersion(dbManager, eventLogHelper, version45);
    int version46 = 413;
    if (this.NeedUpdate(dbManager, eventLogHelper, version46))
    {
      if (dbManager.DataProvider.Name == "Sql")
      {
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_OBJECTS ALTER COLUMN F_SITE_ID VARCHAR(10) NULL");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_OBJECTS_VIEW ALTER COLUMN F_SITE_ID VARCHAR(10) NULL");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_OBJ_SNAPSHOT ALTER COLUMN F_SITE_ID VARCHAR(10) NULL");
      }
      else
      {
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_OBJECTS MODIFY (F_SITE_ID VARCHAR2(10))");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_OBJECTS_VIEW MODIFY (F_SITE_ID VARCHAR2(10))");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_OBJ_SNAPSHOT MODIFY (F_SITE_ID VARCHAR2(10))");
      }
      DataTable dataTable = dbManager.ExecuteDataTable("SELECT F_OBJECT_TYPE FROM IMS_OBJECT_TYPES");
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        string str = "IMV_O" + dataTable.Rows[index][0].ToString();
        try
        {
          dbManager.ExecuteScalar($"SELECT F_OBJECT_ID FROM {str} WHERE F_OBJECT_ID = -1");
          if (dbManager.DataProvider.Name == "Sql")
            dbManager.ExecuteNonQuery($"ALTER TABLE {str} ALTER COLUMN F_SITE_ID VARCHAR(10) NULL");
          else
            dbManager.ExecuteNonQuery($"ALTER TABLE {str} MODIFY (F_SITE_ID VARCHAR2(10))");
        }
        catch
        {
        }
      }
      this.UpdateVersion(dbManager, eventLogHelper, version46);
    }
    int version47 = 500;
    if (this.NeedUpdate(dbManager, eventLogHelper, version47))
    {
      this.PatchDB500(dbManager, eventLogHelper);
      this.UpdateVersion(dbManager, eventLogHelper, version47);
    }
    int version48 = 501;
    if (this.NeedUpdate(dbManager, eventLogHelper, version48))
    {
      this.PatchDB501(dbManager, eventLogHelper);
      this.UpdateVersion(dbManager, eventLogHelper, version48);
    }
    this.PatchStoredProcs(new string[3]
    {
      "import_object",
      "add_object",
      "add_relation"
    }, dbManager, eventLogHelper, 502);
    int version49 = 503;
    if (this.NeedUpdate(dbManager, eventLogHelper, version49))
    {
      this.ModifyAttrGroupNoSystemGuids503((UserSession) null, dbManager);
      this.UpdateVersion(dbManager, eventLogHelper, version49);
    }
    int version50 = 504;
    if (this.NeedUpdate(dbManager, eventLogHelper, version50))
    {
      if (dbManager.DataProvider.Name == "Sql")
      {
        dbManager.ExecuteNonQuery("CREATE TABLE IMS_PUMP_WCFILES (F_OBJECT_ID          BigNumber_DEF NOT NULL,F_INLIST_ID       int NOT NULL,F_FILE_ID          BigNumber_DEF NOT NULL,F_USER_ID          BigNumber_DEF NOT NULL,F_STORAGE_ID          BigNumber_DEF NOT NULL)");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_PUMP_WCFILES ADD PRIMARY KEY CLUSTERED (F_OBJECT_ID, F_INLIST_ID)");
      }
      else if (dbManager.DataProvider.Name == "PostgreSQL")
      {
        dbManager.ExecuteNonQuery("CREATE TABLE IMS_PUMP_WCFILES (F_OBJECT_ID          bigint NOT NULL,F_INLIST_ID        INTEGER NOT NULL,F_FILE_ID          bigint NOT NULL,F_USER_ID          bigint NOT NULL,F_STORAGE_ID          bigint NOT NULL)");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_PUMP_WCFILES ADD PRIMARY KEY (F_OBJECT_ID, F_INLIST_ID)");
      }
      else
      {
        dbManager.ExecuteNonQuery("CREATE TABLE IMS_PUMP_WCFILES (F_OBJECT_ID          INTEGER NOT NULL,F_INLIST_ID        INTEGER NOT NULL,F_FILE_ID          INTEGER NOT NULL,F_USER_ID          INTEGER NOT NULL,F_STORAGE_ID          INTEGER NOT NULL)");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_PUMP_WCFILES ADD (PRIMARY KEY (F_OBJECT_ID, F_INLIST_ID))");
      }
      this.UpdateVersion(dbManager, eventLogHelper, version50);
    }
    int version51 = 505;
    if (this.NeedUpdate(dbManager, eventLogHelper, version51))
    {
      if (dbManager.DataProvider.Name == "Sql")
      {
        dbManager.ExecuteNonQuery("CREATE TABLE IMS_SERVERS (F_SERVER_NAME          ObjectName_DEF NOT NULL,F_DATE              datetime NOT NULL)");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_SERVERS ADD PRIMARY KEY CLUSTERED (F_SERVER_NAME)");
        dbManager.ExecuteNonQuery("CREATE TABLE IMS_ISB (F_KEY                 BigNumber_DEF NOT NULL IDENTITY(1,1),F_SERVER_SRC          ObjectName_DEF NOT NULL,F_SERVER_DST          ObjectName_DEF NOT NULL,F_GUID          GUID_DEF NOT NULL,F_STRING_INFO          MaximumString_DEF NULL,F_DATE          datetime NOT NULL,F_DELETE_ON_START          SmallNumber_DEF NOT NULL DEFAULT 0)");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_ISB ADD PRIMARY KEY CLUSTERED (F_KEY)");
      }
      else if (dbManager.DataProvider.Name == "PostgreSQL")
      {
        dbManager.ExecuteNonQuery("CREATE TABLE IMS_SERVERS (F_SERVER_NAME           varchar(255) NOT NULL,F_DATE              timestamp NOT NULL)");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_SERVERS ADD PRIMARY KEY (F_SERVER_NAME)");
        dbManager.ExecuteNonQuery("CREATE TABLE IMS_ISB (F_KEY                 bigint NOT NULL,F_SERVER_SRC          varchar(255) NOT NULL,F_SERVER_DST          varchar(255) NOT NULL,F_GUID          uuid NOT NULL,F_STRING_INFO          varchar(455) NULL,F_DATE          timestamp NOT NULL,F_DELETE_ON_START          smallint DEFAULT 0 NOT NULL)");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_ISB ADD PRIMARY KEY (F_KEY)");
        dbManager.ExecuteNonQuery("CREATE SEQUENCE IMS_ISB_GEN START WITH 1 INCREMENT BY 1 MINVALUE 1");
      }
      else
      {
        dbManager.ExecuteNonQuery("CREATE TABLE IMS_SERVERS (F_SERVER_NAME           NVARCHAR2(255) NOT NULL,F_DATE              DATE NOT NULL)");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_SERVERS ADD PRIMARY KEY (F_SERVER_NAME)");
        dbManager.ExecuteNonQuery("CREATE TABLE IMS_ISB (F_KEY                 INTEGER NOT NULL,F_SERVER_SRC          NVARCHAR2(255) NOT NULL,F_SERVER_DST          NVARCHAR2(255) NOT NULL,F_GUID          VARCHAR2(40) NOT NULL,F_STRING_INFO          NVARCHAR2(455) NULL,F_DATE          DATE NOT NULL,F_DELETE_ON_START          smallint DEFAULT 0 NOT NULL)");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_ISB ADD PRIMARY KEY (F_KEY)");
        dbManager.ExecuteNonQuery("CREATE SEQUENCE IMS_ISB_GEN START WITH 1 INCREMENT BY 1 NOMAXVALUE MINVALUE 1");
      }
      this.UpdateVersion(dbManager, eventLogHelper, version51);
    }
    int version52 = 506;
    if (this.NeedUpdate(dbManager, eventLogHelper, version52))
    {
      dbManager.ExecuteNonQuery("CREATE INDEX IMS_ISB_SERVER_DST ON IMS_ISB (F_SERVER_DST)");
      this.UpdateVersion(dbManager, eventLogHelper, version52);
    }
    int version53 = 507;
    if (this.NeedUpdate(dbManager, eventLogHelper, version53))
    {
      if (dbManager.DataProvider.Name == "Sql")
      {
        dbManager.ExecuteNonQuery("IF (NOT EXISTS (SELECT st.* FROM sys.types st WHERE st.[name] = 'IMS_IDATA_STRUCT'))BEGIN CREATE TYPE IMS_IDATA_STRUCT AS TABLE (F_LINK_ID  BigNumber_DEF NOT NULL, F_TABLE_ID BigNumber_DEF NOT NULL, F_TABKEY   INTEGER       NOT NULL, F_TEXT     MaximumString_DEF NULL, F_HASHTEXT MaximumString_DEF NULL) END");
        string str1 = "CREATE PROCEDURE IMS_IDATA_TVP (@importTableName nvarchar(45), @importTable IMS_IDATA_STRUCT READONLY) AS ";
        string str2 = "DECLARE @sqlString nvarchar(500); DECLARE @parmDefinition nvarchar(500); ";
        string str3 = "SET @sqlString = N'INSERT INTO ' + @importTableName + ' (F_LINK_ID, F_TABLE_ID, F_TABKEY, F_TEXT, F_HASHTEXT) SELECT F_LINK_ID, F_TABLE_ID, F_TABKEY, F_TEXT, F_HASHTEXT FROM @tableData WHERE 1=1'; ";
        string str4 = "SET @parmDefinition = N'@tableData IMS_IDATA_STRUCT READONLY'; ";
        string str5 = "EXECUTE sp_executesql @sqlString, @parmDefinition, @tableData = @importTable;";
        dbManager.DataProvider.NoLockMode = false;
        dbManager.ExecuteNonQuery($"{str1}{str2}{str3}{str4}{str5}");
        dbManager.DataProvider.NoLockMode = true;
      }
      this.UpdateVersion(dbManager, eventLogHelper, version53);
    }
    int version54 = 508;
    if (this.NeedUpdate(dbManager, eventLogHelper, version54))
    {
      dbManager.ExecuteNonQuery("CREATE INDEX IMS_IMBASE_ATTRS_ATT ON IMS_IMBASE_ATTRS (F_ATTRIBUTE_ID)");
      this.UpdateVersion(dbManager, eventLogHelper, version54);
    }
    int version55 = 600;
    if (this.NeedUpdate(dbManager, eventLogHelper, version55))
    {
      dbManager.ExecuteNonQuery("DELETE FROM IMS_FILENAMES WHERE NOT EXISTS(SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE IMS_OBJECTS.F_OBJECT_ID = IMS_FILENAMES.F_KEY)");
      dbManager.ExecuteNonQuery("ALTER TABLE IMS_FILENAMES ADD CONSTRAINT IMS_FILENAMES_FID FOREIGN KEY (F_KEY) REFERENCES IMS_OBJECTS");
      this.UpdateVersion(dbManager, eventLogHelper, version55);
    }
    int version56 = 601;
    if (this.NeedUpdate(dbManager, eventLogHelper, version56))
    {
      if (dbManager.DataProvider.Name == "Sql")
      {
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_ATTR_GROUPS ALTER COLUMN F_AREA_ID String20_DEF");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_ATTRIBUTES ALTER COLUMN F_AREA_ID String20_DEF");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_LEVELS ALTER COLUMN F_AREA_ID String20_DEF");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_OBJECT_TYPES ALTER COLUMN F_AREA_ID String20_DEF");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_RELATION_TYPES ALTER COLUMN F_AREA_ID String20_DEF");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_LC_SCHEMAS ALTER COLUMN F_AREA_ID String20_DEF");
      }
      else if (dbManager.DataProvider.Name == "Oracle")
      {
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_ATTR_GROUPS MODIFY (F_AREA_ID NVARCHAR2(20))");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_ATTRIBUTES MODIFY (F_AREA_ID NVARCHAR2(20))");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_LEVELS MODIFY (F_AREA_ID NVARCHAR2(20))");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_OBJECT_TYPES MODIFY (F_AREA_ID NVARCHAR2(20))");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_RELATION_TYPES MODIFY (F_AREA_ID NVARCHAR2(20))");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_LC_SCHEMAS MODIFY (F_AREA_ID NVARCHAR2(20))");
      }
      else
      {
        dbManager.ExecuteNonQuery("DROP VIEW IMS_ATTR4OBJTYPE_VIEW");
        dbManager.ExecuteNonQuery("DROP VIEW IMS_ATTR4RELTYPE_VIEW");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_ATTR_GROUPS ALTER COLUMN F_AREA_ID TYPE VARCHAR(20)");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_ATTRIBUTES ALTER COLUMN F_AREA_ID TYPE VARCHAR(20)");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_LEVELS ALTER COLUMN F_AREA_ID TYPE VARCHAR(20)");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_OBJECT_TYPES ALTER COLUMN F_AREA_ID TYPE VARCHAR(20)");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_RELATION_TYPES ALTER COLUMN F_AREA_ID TYPE VARCHAR(20)");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_LC_SCHEMAS ALTER COLUMN F_AREA_ID TYPE VARCHAR(20)");
        this.CreateAttr4Views(dbManager);
      }
      this.UpdateVersion(dbManager, eventLogHelper, version56);
    }
    int version57 = 602;
    if (this.NeedUpdate(dbManager, eventLogHelper, version57))
    {
      if (dbManager.DataProvider.Name == "Sql")
      {
        dbManager.ExecuteNonQuery("CREATE TABLE IMS_EVENTLOG_ARC (F_EVENT_ID           BigNumber_DEF NOT NULL, F_CATEGORY_TYPE      SmallNumber_DEF NOT NULL DEFAULT 0, F_CATEGORY_ID        BigNumber_DEF NOT NULL DEFAULT 0, F_OBJECT_ID          BigNumber_DEF NULL, F_RELATION_ID        BigNumber_DEF NULL, F_OBJECT_NAME        ObjectName_DEF, F_USER_ID            BigNumber_DEF NOT NULL, F_COMPUTER_NAME      String40_DEF NOT NULL, F_NOTE               MaximumString_DEF, F_EVENT_TYPE         SmallNumber_DEF NOT NULL, F_BEGIN_DATE         datetime NOT NULL, F_END_DATE           datetime NULL, F_AUDIT_TYPE         SmallNumber_DEF NOT NULL)");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_EVENTLOG_ARC ADD PRIMARY KEY CLUSTERED(F_EVENT_ID)");
      }
      else if (dbManager.DataProvider.Name == "Oracle")
      {
        dbManager.ExecuteNonQuery("CREATE TABLE IMS_EVENTLOG_ARC (F_EVENT_ID           INTEGER NOT NULL,F_CATEGORY_TYPE      SMALLINT DEFAULT 0 NOT NULL,F_CATEGORY_ID        INTEGER DEFAULT 0 NOT NULL,F_OBJECT_ID          INTEGER NULL,F_RELATION_ID        INTEGER NULL,F_OBJECT_NAME        NVARCHAR2(255) NULL,F_USER_ID            INTEGER NOT NULL,F_COMPUTER_NAME      NVARCHAR2(40) NOT NULL,F_NOTE               NVARCHAR2(450) NULL,F_EVENT_TYPE         SMALLINT NOT NULL,F_BEGIN_DATE         DATE NOT NULL,F_END_DATE           DATE NULL,F_AUDIT_TYPE         SMALLINT NOT NULL)");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_EVENTLOG_ARC ADD(PRIMARY KEY(F_EVENT_ID))");
      }
      else
      {
        dbManager.ExecuteNonQuery("CREATE TABLE IMS_EVENTLOG_ARC (F_EVENT_ID           bigint NOT NULL,F_CATEGORY_TYPE      SMALLINT DEFAULT 0 NOT NULL, F_CATEGORY_ID        bigint DEFAULT 0 NOT NULL, F_OBJECT_ID          bigint NULL, F_RELATION_ID        bigint NULL, F_OBJECT_NAME        varchar(255) NULL, F_USER_ID            bigint NOT NULL, F_COMPUTER_NAME      varchar(40) NOT NULL, F_NOTE               varchar(450) NULL, F_EVENT_TYPE         SMALLINT NOT NULL, F_BEGIN_DATE         timestamp NOT NULL, F_END_DATE           timestamp NULL, F_AUDIT_TYPE         SMALLINT NOT NULL)");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_EVENTLOG_ARC ADD   PRIMARY KEY(F_EVENT_ID)");
      }
      dbManager.ExecuteNonQuery("CREATE INDEX IMS_EVENTLOGARC_CAT_TYPE ON IMS_EVENTLOG_ARC (F_CATEGORY_TYPE)");
      dbManager.ExecuteNonQuery("CREATE INDEX IMS_EVENTLOGARC_CAT_ID ON IMS_EVENTLOG_ARC (F_CATEGORY_ID)");
      dbManager.ExecuteNonQuery("CREATE INDEX IMS_EVENTLOGARC_OBJ_ID ON IMS_EVENTLOG_ARC (F_OBJECT_ID)");
      dbManager.ExecuteNonQuery("CREATE INDEX IMS_EVENTLOGARC_REL_ID ON IMS_EVENTLOG_ARC (F_RELATION_ID)");
      dbManager.ExecuteNonQuery("CREATE INDEX IMS_EVENTLOGARC_USER_ID ON IMS_EVENTLOG_ARC (F_USER_ID)");
      dbManager.ExecuteNonQuery("CREATE INDEX IMS_EVENTLOGARC_COMP_NAME ON IMS_EVENTLOG_ARC (F_COMPUTER_NAME)");
      dbManager.ExecuteNonQuery("CREATE INDEX IMS_EVENTLOGARC_EVENT_TYPE ON IMS_EVENTLOG_ARC (F_EVENT_TYPE)");
      dbManager.ExecuteNonQuery("CREATE INDEX IMS_EVENTLOGARC_BEGIN_DATE ON IMS_EVENTLOG_ARC (F_BEGIN_DATE)");
      dbManager.ExecuteNonQuery("CREATE INDEX IMS_EVENTLOGARC_END_DATE ON IMS_EVENTLOG_ARC (F_END_DATE)");
      dbManager.ExecuteNonQuery("CREATE INDEX IMS_EVENTLOGARC_AUDIT_TYPE ON IMS_EVENTLOG_ARC (F_AUDIT_TYPE)");
      this.UpdateVersion(dbManager, eventLogHelper, version57);
    }
    int version58 = 603;
    if (this.NeedUpdate(dbManager, eventLogHelper, version58))
    {
      this.PatchMaxStringTo850(dbManager);
      this.UpdateVersion(dbManager, eventLogHelper, version58);
    }
    int version59 = 604;
    if (this.NeedUpdate(dbManager, eventLogHelper, version59))
    {
      if (dbManager.DataProvider.Name == "Sql")
      {
        dbManager.ExecuteNonQuery("DROP PROCEDURE IMS_IDATA_TVP");
        dbManager.ExecuteNonQuery("DROP TYPE IMS_IDATA_STRUCT");
        dbManager.ExecuteNonQuery("CREATE TYPE IMS_IDATA_STRUCT AS TABLE(F_LINK_ID BigNumber_DEF NOT NULL,F_TABLE_ID BigNumber_DEF NOT NULL,F_TABKEY INTEGER NOT NULL,F_TEXT MaximumString_DEF NULL,F_HASHTEXT MaximumString_DEF NULL,F_APPLICABILITY INTEGER NOT NULL)");
        dbManager.DataProvider.NoLockMode = false;
        dbManager.ExecuteNonQuery("CREATE PROCEDURE IMS_IDATA_TVP\r\n(@importTableName nvarchar(45), @importTable IMS_IDATA_STRUCT READONLY) \r\nAS \r\nDECLARE @sqlString nvarchar(500); \r\nDECLARE @parmDefinition nvarchar(500); \r\nSET @sqlString = N'INSERT INTO  ' + @importTableName + ' (F_LINK_ID, F_TABLE_ID, F_TABKEY, F_TEXT, F_HASHTEXT, F_APPLICABILITY) \r\nSELECT F_LINK_ID, F_TABLE_ID, F_TABKEY, F_TEXT, F_HASHTEXT, F_APPLICABILITY \r\nFROM @tableData WHERE 1 = 1'; \r\nSET @parmDefinition = N'@tableData IMS_IDATA_STRUCT READONLY'; \r\nEXECUTE sp_executesql @sqlString, \r\n@parmDefinition, \r\n@tableData = @importTable;");
        dbManager.DataProvider.NoLockMode = true;
      }
      this.UpdateVersion(dbManager, eventLogHelper, version59);
    }
    int version60 = 605;
    if (this.NeedUpdate(dbManager, eventLogHelper, version60))
    {
      if (dbManager.DataProvider.Name == "Sql")
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_CATEGORY_ACCESS ADD F_CONDITION_ID BigNumber_DEF NOT NULL DEFAULT 0");
      else if (dbManager.DataProvider.Name == "Oracle")
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_CATEGORY_ACCESS ADD F_CONDITION_ID INTEGER DEFAULT 0 NOT NULL");
      else
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_CATEGORY_ACCESS ADD F_CONDITION_ID BIGINT DEFAULT 0 NOT NULL");
      dbManager.ExecuteNonQuery("CREATE INDEX IMS_CATEGORY_ACCESS_COND ON IMS_CATEGORY_ACCESS (F_CONDITION_ID)");
      this.UpdateVersion(dbManager, eventLogHelper, version60);
    }
    int version61 = 606;
    if (this.NeedUpdate(dbManager, eventLogHelper, version61))
    {
      dbManager.DataProvider.DropIndexIfExists("CHECK_ACCESS_NDX", "IMS_CATEGORY_ACCESS", dbManager);
      dbManager.ExecuteNonQuery("CREATE INDEX CHECK_ACCESS_NDX ON IMS_CATEGORY_ACCESS (F_CATEGORY_ID, F_CATEGORY_TYPE, F_RIGHT_ID, F_USER_ID)");
      this.UpdateVersion(dbManager, eventLogHelper, version61);
    }
    int version62 = 607;
    if (this.NeedUpdate(dbManager, eventLogHelper, version62))
    {
      dbManager.DataProvider.DropIndexIfExists("IMS_RELATIONS_DELETE_DATE", "IMS_RELATIONS", dbManager);
      foreach (DataRow row in (InternalDataCollectionBase) dbManager.ExecuteDataTable("SELECT F_RELATION_TYPE FROM IMS_RELATION_TYPES").Rows)
      {
        try
        {
          string tableName = "IMV_R" + Convert.ToInt32(row[0]).ToString();
          dbManager.ExecuteNonQuery(dbManager.DataProvider.GetDropIndexSQL(tableName, "F_DELETE_DATE", SortOrders.ASC));
        }
        catch (Exception ex)
        {
          eventLogHelper.AddToTrace("Ошибка удаления индекса у поля F_DELETE_DATE: " + ex.Message);
        }
      }
      this.UpdateVersion(dbManager, eventLogHelper, version62);
    }
    this.PatchStoredProcs(new string[2]
    {
      "check_in_tree_up",
      "check_in_tree_down"
    }, dbManager, eventLogHelper, 608);
    this.PatchStoredProcs(new string[1]
    {
      "check_in_tree_up"
    }, dbManager, eventLogHelper, 609);
    this.PatchStoredProcs(new string[1]
    {
      "check_in_tree_down"
    }, dbManager, eventLogHelper, 610);
    this.PatchStoredProcs(new string[2]
    {
      "add_relation",
      "check_in_tree_up"
    }, dbManager, eventLogHelper, 700);
    int version63 = 701;
    if (this.NeedUpdate(dbManager, eventLogHelper, version63))
    {
      this.DropRelationDeleteDate(dbManager, eventLogHelper);
      this.UpdateVersion(dbManager, eventLogHelper, version63);
    }
    int version64 = 702;
    if (this.NeedUpdate(dbManager, eventLogHelper, version64))
    {
      if (dbManager.DataProvider.Name == "Sql")
      {
        dbManager.ExecuteNonQuery("CREATE TABLE IMS_LOCKER(F_METHOD_NAME         ObjectName_DEF NOT NULL,F_COMPUTER_NAME       ObjectName_DEF NOT NULL,F_USER_NAME           ObjectName_DEF NOT NULL,F_DATE                datetime NOT NULL)");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_LOCKER ADD PRIMARY KEY CLUSTERED (F_METHOD_NAME)");
      }
      else if (dbManager.DataProvider.Name == "Oracle")
      {
        dbManager.ExecuteNonQuery("CREATE TABLE IMS_LOCKER(F_METHOD_NAME         NVARCHAR2(255) NOT NULL,F_COMPUTER_NAME       NVARCHAR2(255) NOT NULL,F_USER_NAME           NVARCHAR2(255) NOT NULL,F_DATE                DATE NOT NULL)");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_LOCKER ADD PRIMARY KEY (F_METHOD_NAME)");
      }
      else
      {
        dbManager.ExecuteNonQuery("CREATE TABLE IMS_LOCKER(F_METHOD_NAME         varchar(255) NOT NULL,F_COMPUTER_NAME       varchar(255) NOT NULL,F_USER_NAME           varchar(255) NOT NULL,F_DATE                timestamp NOT NULL);");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_LOCKER ADD PRIMARY KEY (F_METHOD_NAME)");
      }
      this.UpdateVersion(dbManager, eventLogHelper, version64);
    }
    int version65 = 703;
    if (this.NeedUpdate(dbManager, eventLogHelper, version65))
      this.UpdateVersion(dbManager, eventLogHelper, version65);
    int version66 = 704;
    if (this.NeedUpdate(dbManager, eventLogHelper, version66))
    {
      try
      {
        dbManager.ExecuteNonQuery(sc_12780.ssp_appserver_12816());
      }
      catch
      {
      }
      if (dbManager.DataProvider.Name == "Sql")
      {
        dbManager.ExecuteNonQuery(sc_12780.ssp_appserver_12817() + "F_OBJECT_ID    BigNumber_DEF NOT NULL,F_ATTRIBUTE_ID INTEGER NOT NULL,F_INLIST_ID    INTEGER NOT NULL,F_TO_ID  BigNumber_DEF NOT NULL)");
        dbManager.ExecuteNonQuery(sc_12780.ssp_appserver_12818() + "(F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID)");
      }
      else if (dbManager.DataProvider.Name == "Oracle")
      {
        dbManager.ExecuteNonQuery(sc_12780.ssp_appserver_12819() + "F_OBJECT_ID    INTEGER NOT NULL,F_ATTRIBUTE_ID INTEGER NOT NULL,F_INLIST_ID    INTEGER NOT NULL,F_TO_ID  INTEGER NOT NULL)");
        dbManager.ExecuteNonQuery(sc_12780.ssp_appserver_12820());
      }
      else
      {
        dbManager.ExecuteNonQuery("CREATE TABLE IMS_ID_LINKS(F_OBJECT_ID    bigint NOT NULL,F_ATTRIBUTE_ID INTEGER NOT NULL,F_INLIST_ID    INTEGER NOT NULL,F_TO_ID  bigint NOT NULL)");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_ID_LINKS ADD CONSTRAINT IMS_ID_LINKS_PK PRIMARY KEY(F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID)");
      }
      dbManager.ExecuteNonQuery(sc_12780.ssp_appserver_12821());
      dbManager.ExecuteNonQuery(sc_12780.ssp_appserver_12822());
      dbManager.ExecuteNonQuery(sc_12780.ssp_appserver_12823());
      dbManager.ExecuteNonQuery(sc_12780.ssp_appserver_12824());
      this.UpdateVersion(dbManager, eventLogHelper, version66);
    }
    int version67 = 705;
    if (this.NeedUpdate(dbManager, eventLogHelper, version67))
    {
      if (dbManager.DataProvider.Name == "Sql")
      {
        dbManager.SetAdminCommandTimeout();
        try
        {
          dbManager.DataProvider.DropIndexIfExists("IMS_EVENTLOG_BEGIN_DATE", "IMS_EVENTLOG", dbManager);
          dbManager.DataProvider.DropIndexIfExists("IMS_EVENTLOG_END_DATE", "IMS_EVENTLOG", dbManager);
          dbManager.ExecuteNonQuery("CREATE INDEX IMS_EVENTLOG_BEGIN_DATE ON IMS_EVENTLOG (F_BEGIN_DATE DESC)");
          dbManager.ExecuteNonQuery("CREATE INDEX IMS_EVENTLOG_END_DATE ON IMS_EVENTLOG (F_END_DATE DESC)");
          dbManager.DataProvider.DropIndexIfExists("IMS_EVENTLOGARC_BEGIN_DATE", "IMS_EVENTLOG_ARC", dbManager);
          dbManager.DataProvider.DropIndexIfExists("IMS_EVENTLOGARC_END_DATE", "IMS_EVENTLOG_ARC", dbManager);
          dbManager.ExecuteNonQuery("CREATE INDEX IMS_EVENTLOGARC_BEGIN_DATE ON IMS_EVENTLOG_ARC (F_BEGIN_DATE DESC)");
          dbManager.ExecuteNonQuery("CREATE INDEX IMS_EVENTLOGARC_END_DATE ON IMS_EVENTLOG_ARC (F_END_DATE DESC)");
        }
        finally
        {
          dbManager.SetNormalCommandTimeout();
        }
      }
      this.UpdateVersion(dbManager, eventLogHelper, version67);
    }
    int version68 = 706;
    if (this.NeedUpdate(dbManager, eventLogHelper, version68))
    {
      if (dbManager.DataProvider.Name == "Oracle")
      {
        try
        {
          dbManager.ExecuteNonQuery("DROP PROCEDURE IMS_GET_DOWN_LINKS");
        }
        catch
        {
        }
        try
        {
          dbManager.ExecuteNonQuery("DROP PROCEDURE IMS_GET_UP_LINKS");
        }
        catch
        {
        }
      }
      this.UpdateVersion(dbManager, eventLogHelper, version68);
    }
    int version69 = 707;
    if (this.NeedUpdate(dbManager, eventLogHelper, version69))
    {
      dbManager.SetAdminCommandTimeout();
      try
      {
        try
        {
          dbManager.ExecuteNonQuery(dbManager.DataProvider.GetDropIndexSQL("IMS_VERSIONS_TREE", "F_OBJECT_ID", SortOrders.ASC));
        }
        catch
        {
        }
        dbManager.DataProvider.CreateIndex("IMS_VERSIONS_TREE", "F_OBJECT_ID", dbManager, SortOrders.ASC);
      }
      finally
      {
        dbManager.SetNormalCommandTimeout();
      }
      this.UpdateVersion(dbManager, eventLogHelper, version69);
    }
    this.PatchStoredProcs(new string[1]{ "delete_object" }, dbManager, eventLogHelper, 708);
    int version70 = 709;
    if (this.NeedUpdate(dbManager, eventLogHelper, version70))
    {
      dbManager.SetAdminCommandTimeout();
      try
      {
        dbManager.DataProvider.DropIndexIfExists("IMS_VERSIONS_CONTEXT_CONTEXT_ID", "IMS_VERSIONS_CONTEXT", dbManager);
        dbManager.DataProvider.DropIndexIfExists("IMS_VERSIONS_CONTEXT_ID", "IMS_VERSIONS_CONTEXT", dbManager);
        dbManager.ExecuteNonQuery("CREATE INDEX IMS_VERSIONS_CONTEXT_ID ON IMS_VERSIONS_CONTEXT (F_ID)");
      }
      finally
      {
        dbManager.SetNormalCommandTimeout();
      }
      this.UpdateVersion(dbManager, eventLogHelper, version70);
    }
    int version71 = 710;
    if (!this.NeedUpdate(dbManager, eventLogHelper, version71))
      return;
    this.PatchMaxStringIMVATo850(dbManager);
    this.UpdateVersion(dbManager, eventLogHelper, version71);
  }

  private void PatchMaxStringIMVATo850(IDbManager dbManager)
  {
    dbManager.SetAdminCommandTimeout();
    try
    {
      string fldType = !(dbManager.DataProvider.Name == "Sql") ? (!(dbManager.DataProvider.Name == "Oracle") ? "varchar(850)" : "NVARCHAR2(850)") : "String850_DEF";
      DataTable dataTable = dbManager.ExecuteDataTable("SELECT F_OBJECT_TYPE, F_OPTIONS FROM IMS_OBJECT_TYPES");
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        string str = "IMV_A" + Convert.ToInt32(dataTable.Rows[index][0]).ToString();
        ObjectTypeOptions int32 = (ObjectTypeOptions) Convert.ToInt32(dataTable.Rows[index][1]);
        if ((int32 & ObjectTypeOptions.LocalObjectType) == ObjectTypeOptions.LocalObjectType)
        {
          if ((int32 & ObjectTypeOptions.AttributesIndex) == ObjectTypeOptions.AttributesIndex && dbManager.DataProvider.Name == "Sql")
            dbManager.DataProvider.DropAttrValuesIndex(str, dbManager);
          dbManager.ExecuteNonQuery(dbManager.DataProvider.GetModifyColumnSQL(str, "F_STRING_VALUE", fldType));
          if ((int32 & ObjectTypeOptions.AttributesIndex) == ObjectTypeOptions.AttributesIndex && dbManager.DataProvider.Name == "Sql")
            dbManager.DataProvider.CreateAttrValuesIndex(str, dbManager);
        }
      }
    }
    finally
    {
      dbManager.SetNormalCommandTimeout();
    }
  }

  private void DropRelationDeleteDate(IDbManager dbManager, IEventLogHelper eventLogHelper)
  {
    List<string> stringList = new List<string>((IEnumerable<string>) new string[2]
    {
      "IMS_RELATIONS",
      "IMS_REL_SNAPSHOT"
    });
    foreach (DataRow row in (InternalDataCollectionBase) dbManager.ExecuteDataTable("SELECT F_RELATION_TYPE FROM IMS_RELATION_TYPES").Rows)
    {
      string tableName = "IMV_R" + Convert.ToInt32(row[0]).ToString();
      try
      {
        dbManager.DataProvider.CheckTableExists(tableName, "F_PRJLINK_ID", dbManager);
        stringList.Add(tableName);
      }
      catch
      {
      }
    }
    dbManager.DataProvider.NoLockMode = false;
    dbManager.ExecuteNonQuery("delete from IMS_RELATION_ATTRS WHERE EXISTS(SELECT * FROM IMS_RELATIONS WHERE(IMS_RELATIONS.F_DELETE_DATE IS NOT NULL) AND (IMS_RELATIONS.F_PRJLINK_ID = IMS_RELATION_ATTRS.F_PRJLINK_ID))");
    foreach (string tableName in stringList)
    {
      try
      {
        dbManager.ExecuteNonQuery($"DELETE FROM {tableName} WHERE F_DELETE_DATE IS NOT NULL");
        dbManager.ExecuteNonQuery(dbManager.DataProvider.GetDropColumnsSQL(tableName, "F_DELETE_DATE"));
      }
      catch (Exception ex)
      {
        eventLogHelper.AddToTrace($"Ошибка удаления поля F_DELETE_DATE из таблицы {tableName}: {ex.Message}", Intermech.Consts.traceAlways);
      }
    }
    dbManager.ExecuteNonQuery("DELETE FROM IMS_ATTRIBUTES WHERE F_ATTRIBUTE_ID = " + Convert.ToInt32((object) ObligatoryObjectAttributes.F_DELETE_DATE).ToString());
  }

  private void CreateAttr4Views(IDbManager dbManager)
  {
    dbManager.ExecuteNonQuery("CREATE VIEW IMS_ATTR4OBJTYPE_VIEW AS SELECT IMS_ATTR4OBJ_TYPES.*, IMS_ATTRIBUTES.F_NAME, IMS_ATTRIBUTES.F_SHORT_NAME, IMS_ATTRIBUTES.F_ALIAS, IMS_ATTRIBUTES.F_NOTE, IMS_ATTRIBUTES.F_ATTRIBUTE_TYPE, IMS_ATTRIBUTES.F_MULTIPLE_VALUED, IMS_ATTRIBUTES.F_SIZE_TYPE, IMS_ATTRIBUTES.F_LANGUAGE_ID, IMS_ATTRIBUTES.F_GUID, IMS_ATTRIBUTES.F_AREA_ID FROM IMS_ATTR4OBJ_TYPES, IMS_ATTRIBUTES WHERE IMS_ATTRIBUTES.F_ATTRIBUTE_ID = IMS_ATTR4OBJ_TYPES.F_ATTRIBUTE_ID ");
    dbManager.ExecuteNonQuery("CREATE VIEW IMS_ATTR4RELTYPE_VIEW AS SELECT IMS_ATTR4RELATION_TYPES.*, IMS_ATTRIBUTES.F_NAME, IMS_ATTRIBUTES.F_SHORT_NAME, IMS_ATTRIBUTES.F_ALIAS, IMS_ATTRIBUTES.F_NOTE, IMS_ATTRIBUTES.F_ATTRIBUTE_TYPE, IMS_ATTRIBUTES.F_MULTIPLE_VALUED, IMS_ATTRIBUTES.F_SIZE_TYPE, IMS_ATTRIBUTES.F_LANGUAGE_ID, IMS_ATTRIBUTES.F_GUID, IMS_ATTRIBUTES.F_AREA_ID FROM IMS_ATTR4RELATION_TYPES, IMS_ATTRIBUTES WHERE IMS_ATTRIBUTES.F_ATTRIBUTE_ID = IMS_ATTR4RELATION_TYPES.F_ATTRIBUTE_ID");
  }

  private void PatchMaxStringTo850(IDbManager dbManager)
  {
    string fldType1;
    if (dbManager.DataProvider.Name == "Sql")
    {
      dbManager.ExecuteNonQuery("CREATE TYPE String850_DEF FROM nvarchar(850) NULL");
      fldType1 = "String850_DEF";
    }
    else
      fldType1 = !(dbManager.DataProvider.Name == "Oracle") ? "varchar(850)" : "NVARCHAR2(850)";
    dbManager.ExecuteNonQuery(dbManager.DataProvider.GetModifyColumnSQL("IMS_ATTR_HISTORY", "F_STRING_VALUE", fldType1));
    if (dbManager.DataProvider.Name == "PostgreSQL")
    {
      dbManager.ExecuteNonQuery("DROP VIEW IMS_ATTR4OBJTYPE_VIEW");
      dbManager.ExecuteNonQuery("DROP VIEW IMS_ATTR4RELTYPE_VIEW");
    }
    dbManager.ExecuteNonQuery(dbManager.DataProvider.GetModifyColumnSQL("IMS_ATTR4OBJ_TYPES", "F_DEFAULT_VALUE", fldType1));
    dbManager.ExecuteNonQuery(dbManager.DataProvider.GetModifyColumnSQL("IMS_ATTR4RELATION_TYPES", "F_DEFAULT_VALUE", fldType1));
    if (dbManager.DataProvider.Name == "PostgreSQL")
      this.CreateAttr4Views(dbManager);
    dbManager.ExecuteNonQuery(dbManager.DataProvider.GetModifyColumnSQL("IMS_ATTRFILTER_VALUE", "F_STRING_VALUE", fldType1));
    dbManager.ExecuteNonQuery(dbManager.DataProvider.GetModifyColumnSQL("IMS_ATTRIBUTES", "F_DEFAULT_VALUE", fldType1));
    dbManager.ExecuteNonQuery(dbManager.DataProvider.GetModifyColumnSQL("IMS_CONFIGS", "F_VALUE", fldType1));
    dbManager.ExecuteNonQuery(dbManager.DataProvider.GetModifyColumnSQL("IMS_EVENTLOG", "F_NOTE", fldType1));
    dbManager.ExecuteNonQuery(dbManager.DataProvider.GetModifyColumnSQL("IMS_EVENTLOG_ARC", "F_NOTE", fldType1));
    dbManager.ExecuteNonQuery(dbManager.DataProvider.GetModifyColumnSQL("IMS_GUID", "CAPTION", fldType1));
    dbManager.ExecuteNonQuery(dbManager.DataProvider.GetModifyColumnSQL("IMS_GUID", "F_WORK_CAPTION", fldType1));
    dbManager.ExecuteNonQuery(dbManager.DataProvider.GetModifyColumnSQL("IMS_OBJ_SNAPATTRS", "F_STRING_VALUE", fldType1));
    dbManager.ExecuteNonQuery(dbManager.DataProvider.GetModifyColumnSQL("IMS_OBJ_SNAPSHOT", "CAPTION", fldType1));
    if (dbManager.DataProvider.Name == "Sql")
      dbManager.ExecuteNonQuery("drop index IMS_OBJECT_ATTRS_STRING_VALUE on IMS_OBJECT_ATTRS");
    dbManager.ExecuteNonQuery(dbManager.DataProvider.GetModifyColumnSQL("IMS_OBJECT_ATTRS", "F_STRING_VALUE", fldType1));
    if (dbManager.DataProvider.Name == "Sql")
      dbManager.ExecuteNonQuery("CREATE INDEX IMS_OBJECT_ATTRS_STRING_VALUE ON IMS_OBJECT_ATTRS (F_STRING_VALUE)  WHERE F_STRING_VALUE IS NOT NULL");
    dbManager.ExecuteNonQuery(dbManager.DataProvider.GetModifyColumnSQL("IMS_OBJECTS_VIEW", "CAPTION", fldType1));
    dbManager.ExecuteNonQuery(dbManager.DataProvider.GetModifyColumnSQL("IMS_POSSIBLE_VALUES", "F_STRING_VALUE", fldType1));
    dbManager.ExecuteNonQuery(dbManager.DataProvider.GetModifyColumnSQL("IMS_POSSIBLE_VALUES", "F_DESCRIPTION", fldType1));
    dbManager.ExecuteNonQuery(dbManager.DataProvider.GetModifyColumnSQL("IMS_REL_SNAPATTRS", "F_STRING_VALUE", fldType1));
    if (dbManager.DataProvider.Name == "Sql")
      dbManager.ExecuteNonQuery("drop index IMS_RELATION_A_STRING_VALUE on IMS_RELATION_ATTRS");
    dbManager.ExecuteNonQuery(dbManager.DataProvider.GetModifyColumnSQL("IMS_RELATION_ATTRS", "F_STRING_VALUE", fldType1));
    if (dbManager.DataProvider.Name == "Sql")
      dbManager.ExecuteNonQuery("CREATE INDEX IMS_RELATION_A_STRING_VALUE ON IMS_RELATION_ATTRS (F_STRING_VALUE)  WHERE F_STRING_VALUE IS NOT NULL");
    DataTable dataTable = dbManager.ExecuteDataTable("SELECT F_OBJECT_TYPE, F_OPTIONS FROM IMS_OBJECT_TYPES");
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      int int32_1 = Convert.ToInt32(dataTable.Rows[index][0]);
      string str = "IMV_A" + int32_1.ToString();
      ObjectTypeOptions int32_2 = (ObjectTypeOptions) Convert.ToInt32(dataTable.Rows[index][1]);
      if ((int32_2 & ObjectTypeOptions.LocalObjectType) == ObjectTypeOptions.LocalObjectType)
      {
        if ((int32_2 & ObjectTypeOptions.AttributesIndex) == ObjectTypeOptions.AttributesIndex && dbManager.DataProvider.Name == "Sql")
          dbManager.DataProvider.DropAttrValuesIndex(str, dbManager);
        dbManager.ExecuteNonQuery(dbManager.DataProvider.GetModifyColumnSQL(str, "F_STRING_VALUE", fldType1));
        if ((int32_2 & ObjectTypeOptions.AttributesIndex) == ObjectTypeOptions.AttributesIndex && dbManager.DataProvider.Name == "Sql")
          dbManager.DataProvider.CreateAttrValuesIndex(str, dbManager);
      }
      try
      {
        IDbManager dbManager1 = dbManager;
        IDbDataProvider dataProvider = dbManager.DataProvider;
        int32_1 = Convert.ToInt32(dataTable.Rows[index][0]);
        string tableName = "IMV_O" + int32_1.ToString();
        string fldType2 = fldType1;
        string modifyColumnSql = dataProvider.GetModifyColumnSQL(tableName, "CAPTION", fldType2);
        dbManager1.ExecuteNonQuery(modifyColumnSql);
      }
      catch
      {
      }
    }
  }

  private void RebuildIMVO_Indexes(IDbManager dbManager, string tableName)
  {
    try
    {
      dbManager.ExecuteNonQuery(dbManager.DataProvider.GetDropIndexSQL(tableName, "F_VERSION_ID", SortOrders.ASC));
    }
    catch
    {
    }
    dbManager.DataProvider.CreateIndex(tableName, "F_VERSION_ID", dbManager, SortOrders.ASC);
    try
    {
      dbManager.ExecuteNonQuery(dbManager.DataProvider.GetDropIndexSQL(tableName, "F_CHKOUT_BY", SortOrders.ASC));
    }
    catch
    {
    }
    dbManager.DataProvider.CreateIndex(tableName, "F_CHKOUT_BY", dbManager, SortOrders.ASC);
    try
    {
      dbManager.ExecuteNonQuery(dbManager.DataProvider.GetDropIndexSQL(tableName, "F_OBJECT_VER_TYPE", SortOrders.ASC));
    }
    catch
    {
    }
    dbManager.DataProvider.CreateIndex(tableName, "F_OBJECT_VER_TYPE", dbManager, SortOrders.ASC);
    try
    {
      dbManager.ExecuteNonQuery(dbManager.DataProvider.GetDropIndexSQL(tableName, "F_PROJECT_ID", SortOrders.ASC));
    }
    catch
    {
    }
    dbManager.DataProvider.CreateIndex(tableName, "F_PROJECT_ID", dbManager, SortOrders.ASC);
    try
    {
      dbManager.ExecuteNonQuery(dbManager.DataProvider.GetDropIndexSQL(tableName, "F_MODIFICATION_ID", SortOrders.ASC));
    }
    catch
    {
    }
    dbManager.DataProvider.CreateIndex(tableName, "F_MODIFICATION_ID", dbManager, SortOrders.ASC);
  }

  private void PatchIndexes405(IDbManager dbManager, IEventLogHelper eventLogHelper)
  {
    DataTable dataTable1 = dbManager.ExecuteDataTable("SELECT F_OBJECT_TYPE, F_OPTIONS FROM IMS_OBJECT_TYPES");
    for (int index = 0; index < dataTable1.Rows.Count; ++index)
    {
      if ((Convert.ToInt32(dataTable1.Rows[index][1]) & 16 /*0x10*/) == 16 /*0x10*/)
      {
        try
        {
          dbManager.DataProvider.DropAttrValuesIndex("IMV_A" + Convert.ToString(dataTable1.Rows[index][0]), dbManager);
        }
        catch
        {
        }
      }
      if (dbManager.DataProvider.Name == "Sql")
      {
        try
        {
          this.RebuildIMVO_Indexes(dbManager, "IMV_O" + Convert.ToString(dataTable1.Rows[index][0]));
        }
        catch
        {
        }
      }
    }
    if (!(dbManager.DataProvider.Name == "Sql"))
      return;
    this.RebuildIMVO_Indexes(dbManager, "IMS_OBJECTS_VIEW");
    try
    {
      dbManager.ExecuteNonQuery("DROP INDEX IMS_OBJECT_ATTRS.IMS_OBJECT_ATTRS_INTEGER_VALUE");
    }
    catch
    {
    }
    dbManager.ExecuteNonQuery("CREATE INDEX IMS_OBJECT_ATTRS_INTEGER_VALUE ON IMS_OBJECT_ATTRS (F_INTEGER_VALUE) WHERE F_INTEGER_VALUE IS NOT NULL");
    try
    {
      dbManager.ExecuteNonQuery("DROP INDEX IMS_OBJECT_ATTRS.IMS_OBJECT_ATTRS_STRING_VALUE");
    }
    catch
    {
    }
    dbManager.ExecuteNonQuery("CREATE INDEX IMS_OBJECT_ATTRS_STRING_VALUE ON IMS_OBJECT_ATTRS (F_STRING_VALUE)  WHERE F_STRING_VALUE IS NOT NULL");
    try
    {
      dbManager.ExecuteNonQuery("DROP INDEX IMS_OBJECT_ATTRS.IMS_OBJECT_ATTRS_DOUBLE_VALUE");
    }
    catch
    {
    }
    dbManager.ExecuteNonQuery("CREATE INDEX IMS_OBJECT_ATTRS_DOUBLE_VALUE ON IMS_OBJECT_ATTRS (F_DOUBLE_VALUE)  WHERE F_DOUBLE_VALUE IS NOT NULL");
    try
    {
      dbManager.ExecuteNonQuery("DROP INDEX IMS_OBJECT_ATTRS.IMS_OBJECT_ATTRS_DATE_VALUE");
    }
    catch
    {
    }
    dbManager.ExecuteNonQuery("CREATE INDEX IMS_OBJECT_ATTRS_DATE_VALUE ON IMS_OBJECT_ATTRS (F_DATE_VALUE)  WHERE F_DATE_VALUE IS NOT NULL");
    try
    {
      dbManager.ExecuteNonQuery("DROP INDEX IMS_RELATION_ATTRS.IMS_RELATION_A_INTEGER_VALUE");
    }
    catch
    {
    }
    dbManager.ExecuteNonQuery("CREATE INDEX IMS_RELATION_A_INTEGER_VALUE ON IMS_RELATION_ATTRS (F_INTEGER_VALUE) WHERE F_INTEGER_VALUE IS NOT NULL");
    try
    {
      dbManager.ExecuteNonQuery("DROP INDEX IMS_RELATION_ATTRS.IMS_RELATION_A_STRING_VALUE");
    }
    catch
    {
    }
    dbManager.ExecuteNonQuery("CREATE INDEX IMS_RELATION_A_STRING_VALUE ON IMS_RELATION_ATTRS (F_STRING_VALUE) WHERE F_STRING_VALUE IS NOT NULL");
    try
    {
      dbManager.ExecuteNonQuery("DROP INDEX IMS_RELATION_ATTRS.IMS_RELATION_A_DOUBLE_VALUE");
    }
    catch
    {
    }
    dbManager.ExecuteNonQuery("CREATE INDEX IMS_RELATION_A_DOUBLE_VALUE ON IMS_RELATION_ATTRS (F_DOUBLE_VALUE) WHERE F_DOUBLE_VALUE IS NOT NULL");
    try
    {
      dbManager.ExecuteNonQuery("DROP INDEX IMS_RELATION_ATTRS.IMS_RELATION_A_DATE_VALUE");
    }
    catch
    {
    }
    dbManager.ExecuteNonQuery("CREATE INDEX IMS_RELATION_A_DATE_VALUE ON IMS_RELATION_ATTRS (F_DATE_VALUE) WHERE F_DATE_VALUE IS NOT NULL");
    DataTable dataTable2 = dbManager.ExecuteDataTable("SELECT F_RELATION_TYPE FROM IMS_RELATION_TYPES");
    for (int index = 0; index < dataTable2.Rows.Count; ++index)
    {
      try
      {
        dbManager.ExecuteNonQuery(dbManager.DataProvider.GetDropIndexSQL("IMV_R" + Convert.ToString(dataTable2.Rows[index][0]), "F_DELETE_DATE", SortOrders.ASC));
      }
      catch
      {
      }
      try
      {
        dbManager.DataProvider.CreateIndex("IMV_R" + Convert.ToString(dataTable2.Rows[index][0]), "F_DELETE_DATE", dbManager, SortOrders.ASC);
      }
      catch
      {
      }
    }
  }

  private void Patch404(IDbManager Table1)
  {
    Table1.BeginTransaction();
    try
    {
      if (Table1.DataProvider.Name == "Sql")
        Table1.ExecuteNonQuery("SET IDENTITY_INSERT IMS_ATTRIBUTES ON");
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_CHKOUT_BY, LocalizationHolder.rm.GetString("Kernel_662"), "cad0002d-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_ID, LocalizationHolder.rm.GetString("Kernel_663"), "cad0002a-306c-11d8-b4e9-00304f19f545", UniqueValueModes.AllVerTypes);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_LC_STEP, LocalizationHolder.rm.GetString("Kernel_664"), "cad0002b-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_LEVEL_ID, LocalizationHolder.rm.GetString("Kernel_665"), "cad00030-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_MODIFY_DATE, LocalizationHolder.rm.GetString("Kernel_666"), "cad00031-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_OBJECT_ID, LocalizationHolder.rm.GetString("Kernel_667"), "cad00029-306c-11d8-b4e9-00304f19f545", UniqueValueModes.AllVerTypes);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_OBJECT_TYPE, LocalizationHolder.rm.GetString("Kernel_668"), "cad0002e-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_OWNER_ID, LocalizationHolder.rm.GetString("Kernel_669"), "cad0002f-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_VERSION_ID, LocalizationHolder.rm.GetString("Kernel_670"), "cad0002c-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_PRJLINK_ID, LocalizationHolder.rm.GetString("Kernel_671"), "cad00033-306c-11d8-b4e9-00304f19f545", UniqueValueModes.AllVerTypes);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_PROJ_ID, LocalizationHolder.rm.GetString("Kernel_672"), "cad00034-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_PART_ID, LocalizationHolder.rm.GetString("Kernel_673"), "cad00035-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_RELATION_TYPE, LocalizationHolder.rm.GetString("Kernel_674"), "cad00036-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_CREATE_DATE, LocalizationHolder.rm.GetString("Kernel_675"), "cad00037-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_DELETE_DATE, LocalizationHolder.rm.GetString("Kernel_676"), "cad00038-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_EVENT_ID, LocalizationHolder.rm.GetString("Kernel_677"), "cad00039-306c-11d8-b4e9-00304f19f545", UniqueValueModes.AllVerTypes);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_CATEGORY_TYPE, LocalizationHolder.rm.GetString("Kernel_678"), "cad0003a-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_CATEGORY_ID, LocalizationHolder.rm.GetString("Kernel_679"), "cad0003b-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_AUDIT_TYPE, LocalizationHolder.rm.GetString("Kernel_680"), "cad00044-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_NOTE, LocalizationHolder.rm.GetString("Kernel_681"), "cad00040-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_OBJECT_NAME, LocalizationHolder.rm.GetString("Kernel_682"), "cad0003d-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_BEGIN_DATE, LocalizationHolder.rm.GetString("Kernel_683"), "cad00042-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_END_DATE, LocalizationHolder.rm.GetString("Kernel_684"), "cad00043-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_COMPUTER_NAME, LocalizationHolder.rm.GetString("Kernel_685"), "cad0003f-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_USER_ID, LocalizationHolder.rm.GetString("Kernel_686"), "cad0003e-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_EVENT_TYPE, LocalizationHolder.rm.GetString("Kernel_687"), "cad00041-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_RELATION_ID, LocalizationHolder.rm.GetString("Kernel_688"), "cad0003c-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.CAPTION, LocalizationHolder.rm.GetString("Kernel_689"), "cad00047-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_GUID, LocalizationHolder.rm.GetString("Kernel_690"), "cad00130-306c-11d8-b4e9-00304f19f545", UniqueValueModes.AllVerTypes);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_OBJ_CREATE, LocalizationHolder.rm.GetString("Kernel_691"), "cad0013c-306c-11d8-b4e9-00304f19f545", UniqueValueModes.AllVerTypes);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_SET_DATE, LocalizationHolder.rm.GetString("Kernel_692"), "cad0015d-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_STATUS, LocalizationHolder.rm.GetString("Kernel_693"), "cad0015c-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_INTEGER_VALUE, string.Empty, "cad0015e-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_STRING_VALUE, string.Empty, "cad0015f-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_DOUBLE_VALUE, string.Empty, "cad00160-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_DATE_VALUE, string.Empty, "cad00161-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_KEY, LocalizationHolder.rm.GetString("Kernel_694"), "cad001aa-306c-11d8-b4e9-00304f19f545", UniqueValueModes.AllVerTypes);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_ATTRIBUTE_ID, LocalizationHolder.rm.GetString("Kernel_695"), "cad001ab-306c-11d8-b4e9-00304f19f545", UniqueValueModes.AllVerTypes);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_PRJ_GUID, string.Empty, "cad00344-306c-11d8-b4e9-00304f19f545", UniqueValueModes.AllVerTypes);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_FILE_ID, LocalizationHolder.rm.GetString("Kernel_696"), "cad001f2-306c-11d8-b4e9-00304f19f545", UniqueValueModes.AllVerTypes);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_FILENAME, LocalizationHolder.rm.GetString("Kernel_697"), "cad001f3-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_FILESIZE, LocalizationHolder.rm.GetString("Kernel_698"), "cad001f4-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_FILEDATE, LocalizationHolder.rm.GetString("Kernel_699"), "cad001f5-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_ZIPSIZE, LocalizationHolder.rm.GetString("Kernel_700"), "cad001f6-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_OBJECTLINK_ID, LocalizationHolder.rm.GetString("Kernel_701"), "cad001f7-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_ARC_METHOD, LocalizationHolder.rm.GetString("Kernel_702"), "cad001f8-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_PROJECT_ID, LocalizationHolder.rm.GetString("Kernel_703"), "cad00811-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_MODIFICATION_ID, LocalizationHolder.rm.GetString("ModificationIDNote"), "cad014d2-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_BASE_VERSION, LocalizationHolder.rm.GetString("BaseVersionNote"), "cad014d3-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_SITE_ID, LocalizationHolder.rm.GetString("SiteIDNote"), "cad01501-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_OBJ_GUID, LocalizationHolder.rm.GetString("ObjectGUIDNote"), "cad00800-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_OBJECT_VER_TYPE, LocalizationHolder.rm.GetString("ObjectVerTypeNote"), "cadd937c-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(Table1, ObligatoryObjectAttributes.F_ACCESS, LocalizationHolder.rm.GetString("AccessLevelNote"), "cadd959f-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      if (Table1.DataProvider.Name == "Sql")
        Table1.ExecuteNonQuery("SET IDENTITY_INSERT IMS_ATTRIBUTES OFF");
      Table1.Commit();
    }
    catch
    {
      Table1.Rollback();
      throw;
    }
  }

  private void AddSystemAttribute(
    IDbManager dbManager,
    ObligatoryObjectAttributes attribute,
    string note,
    string guidStr,
    UniqueValueModes unique)
  {
    dbManager.ExecuteNonQuery("INSERT INTO IMS_ATTRIBUTES (F_ATTRIBUTE_ID, F_NAME, F_SHORT_NAME, F_ALIAS, F_NOTE, F_ATTRIBUTE_TYPE, F_DEFAULT_VALUE, F_MULTIPLE_VALUED, F_COMPUTED, F_SIZE_TYPE, F_FORMULA,F_GUID, F_AREA_ID, F_UNIQUE, F_LANGUAGE_ID, F_LEVEL_ID, F_CONTENT, F_MASTER_ID, F_SOURCE_ID, F_OPTIONS, F_INVIEW) VALUES (:attrID, :fname, NULL, NULL, :fnote, :data_type, NULL, :mult_val, :comp_val, 0, NULL, :fguid, NULL, :funiq, NULL, 0, 0, 0, 0, :fopt, :fview)", dbManager.Parameter("attrID", (object) Convert.ToInt32((object) attribute)), dbManager.Parameter("fname", (object) ObligatoryObjectAttributesHelper.GetCaption(attribute)), dbManager.Parameter("fnote", (object) note), dbManager.Parameter("data_type", (object) Convert.ToInt32((object) FieldTypes.ftSystem)), dbManager.Parameter("mult_val", (object) Convert.ToInt32((object) MultiValueModes.SingleValue)), dbManager.Parameter("comp_val", (object) Convert.ToInt32((object) ComputeValueModes.StoredValue)), dbManager.Parameter("fguid", (object) guidStr), dbManager.Parameter("funiq", (object) Convert.ToInt32((object) unique)), dbManager.Parameter("fopt", (object) Convert.ToInt32((object) AttributeOptions.None)), dbManager.Parameter("fview", (object) Convert.ToInt32((object) OptimizationModes.Seek)));
  }

  private void PatchDB402(IDbManager dbManager, IEventLogHelper eventLogHelper)
  {
    if (dbManager.DataProvider.Name == "Sql")
    {
      dbManager.ExecuteNonQuery("ALTER TABLE IMS_OBJECTS ADD F_ACCESS SmallNumber_DEF NOT NULL DEFAULT 0");
      dbManager.ExecuteNonQuery("ALTER TABLE IMS_OBJECTS_VIEW ADD F_ACCESS SmallNumber_DEF NOT NULL DEFAULT 0");
      dbManager.ExecuteNonQuery("ALTER TABLE IMS_OBJ_SNAPSHOT ADD F_ACCESS SmallNumber_DEF NOT NULL DEFAULT 0");
    }
    else
    {
      dbManager.ExecuteNonQuery("ALTER TABLE IMS_OBJECTS ADD F_ACCESS SMALLINT DEFAULT 0 NOT NULL");
      dbManager.ExecuteNonQuery("ALTER TABLE IMS_OBJECTS_VIEW ADD F_ACCESS SMALLINT DEFAULT 0 NOT NULL");
      dbManager.ExecuteNonQuery("ALTER TABLE IMS_OBJ_SNAPSHOT ADD F_ACCESS SMALLINT DEFAULT 0 NOT NULL");
    }
    dbManager.ExecuteNonQuery("CREATE INDEX IMS_OBJECTS_F_ACCESS ON IMS_OBJECTS (F_ACCESS)");
    dbManager.ExecuteNonQuery("CREATE INDEX IMS_OBJECTS_VIEW_F_ACCESS ON IMS_OBJECTS_VIEW (F_ACCESS)");
    int int32_1 = Convert.ToInt32(dbManager.ExecuteScalar("SELECT F_OBJECT_TYPE FROM IMS_OBJECT_TYPES WHERE F_GUID = :typeGuid", dbManager.Parameter("typeGuid", (object) "cad00812-306c-11d8-b4e9-00304f19f545")));
    int int32_2 = Convert.ToInt32(dbManager.ExecuteScalar("SELECT F_ATTRIBUTE_ID FROM IMS_ATTRIBUTES WHERE F_GUID = :typeGuid", dbManager.Parameter("typeGuid", (object) "cad00816-306c-11d8-b4e9-00304f19f545")));
    string str = "IMV_O" + int32_1.ToString();
    try
    {
      dbManager.ExecuteNonQuery(string.Format("UPDATE IMS_OBJECTS SET F_ACCESS = (SELECT {0}.F{1} FROM {0} WHERE {0}.F_OBJECT_ID = IMS_OBJECTS.F_OBJECT_ID AND {0}.F{1} IS NOT NULL) WHERE IMS_OBJECTS.F_PROJECT_ID > 0 AND EXISTS(SELECT * FROM {0} WHERE {0}.F_OBJECT_ID = IMS_OBJECTS.F_OBJECT_ID AND {0}.F{1} IS NOT NULL)", (object) str, (object) int32_2));
    }
    catch
    {
    }
    dbManager.ExecuteNonQuery("DELETE FROM IMS_OBJECTS_VIEW WHERE NOT EXISTS(SELECT * FROM IMS_OBJECTS WHERE IMS_OBJECTS.F_OBJECT_ID = IMS_OBJECTS_VIEW.F_OBJECT_ID)");
    dbManager.ExecuteNonQuery("UPDATE IMS_OBJECTS_VIEW SET IMS_OBJECTS_VIEW.F_ACCESS = (SELECT IMS_OBJECTS.F_ACCESS FROM IMS_OBJECTS WHERE IMS_OBJECTS.F_OBJECT_ID = IMS_OBJECTS_VIEW.F_OBJECT_ID)");
    DataTable dataTable = dbManager.ExecuteDataTable("SELECT F_OBJECT_TYPE FROM IMS_OBJECT_TYPES");
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      try
      {
        string tableName = "IMV_O" + dataTable.Rows[index][0].ToString();
        dbManager.ExecuteNonQuery(dbManager.DataProvider.GetAddColumnsSQL(tableName, $"F_ACCESS {dbManager.DataProvider.SMALLINTType} DEFAULT 0"));
        dbManager.ExecuteNonQuery(string.Format("DELETE FROM {0} WHERE NOT EXISTS(SELECT * FROM IMS_OBJECTS WHERE IMS_OBJECTS.F_OBJECT_ID = {0}.F_OBJECT_ID)", (object) tableName));
        dbManager.ExecuteNonQuery(string.Format("UPDATE {0} SET F_ACCESS = (SELECT IMS_OBJECTS.F_ACCESS FROM IMS_OBJECTS WHERE IMS_OBJECTS.F_OBJECT_ID = {0}.F_OBJECT_ID)", (object) tableName));
        dbManager.DataProvider.CreateIndex(tableName, "F_ACCESS", dbManager, SortOrders.ASC);
      }
      catch
      {
      }
    }
  }

  private void PatchDB30(IDbManager dbManager, IEventLogHelper eventLogHelper)
  {
    int int32_1 = Convert.ToInt32(dbManager.ExecuteScalar("SELECT F_OBJECT_TYPE FROM IMS_OBJECT_TYPES WHERE F_GUID = :fguid", dbManager.Parameter("fguid", (object) "cad00014-306c-11d8-b4e9-00304f19f545")));
    int int32_2 = Convert.ToInt32(dbManager.ExecuteScalar("SELECT F_ATTRIBUTE_ID FROM IMS_ATTRIBUTES WHERE F_GUID = :fguid", dbManager.Parameter("fguid", (object) "cad00028-306c-11d8-b4e9-00304f19f545")));
    DataTable storages = dbManager.ExecuteDataTable($"SELECT IMS_OBJECTS.F_OBJECT_ID, IMS_GUID.CAPTION, IMS_GUID.CAPTION FROM IMS_OBJECTS, IMS_GUID WHERE IMS_OBJECTS.F_OBJECT_TYPE = {int32_1.ToString()} AND IMS_GUID.F_OBJECT_ID = IMS_OBJECTS.F_OBJECT_ID AND IMS_OBJECTS.F_OBJECT_VER_TYPE = 0");
    for (int index = 0; index < storages.Rows.Count; ++index)
    {
      long int64 = Convert.ToInt64(storages.Rows[index][0]);
      string str1 = storages.Rows[index][1].ToString();
      string str2 = "IMV_A" + int32_1.ToString();
      string str3;
      try
      {
        str3 = Convert.ToString(dbManager.ExecuteScalar($"SELECT F_STRING_VALUE FROM {str2} WHERE F_OBJECT_ID = {int64} AND F_ATTRIBUTE_ID = {int32_2} AND F_INLIST_ID = 0"));
      }
      catch
      {
        str3 = Convert.ToString(dbManager.ExecuteScalar($"SELECT F_STRING_VALUE FROM {"IMS_OBJECT_ATTRS"} WHERE F_OBJECT_ID = {int64} AND F_ATTRIBUTE_ID = {int32_2} AND F_INLIST_ID = 0"));
      }
      if (str3 == string.Empty)
        str3 = "IMS_STORAGE";
      try
      {
        if (dbManager.ExecuteDataTable($"SELECT * FROM {str3} WHERE F_FILE_ID = 0").Columns.IndexOf("F_ATTRIBUTE_ID") < 0)
        {
          if (dbManager.DataProvider.Name == "Sql")
            dbManager.ExecuteNonQuery($"ALTER TABLE {str3} ADD F_ATTRIBUTE_ID INTEGER DEFAULT 0 NOT NULL");
          else if (dbManager.DataProvider.Name == "Oracle")
            dbManager.ExecuteNonQuery($"ALTER TABLE {str3} ADD F_ATTRIBUTE_ID INTEGER DEFAULT 0 NOT NULL");
          dbManager.ExecuteNonQuery($"CREATE INDEX IMSTOR_ATTR_{int64} ON {str3} (F_ATTRIBUTE_ID)");
        }
        storages.Rows[index][2] = (object) str3;
      }
      catch (Exception ex)
      {
        eventLogHelper.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_1146"), (object) str1, (object) ex.Message), Intermech.Consts.traceAlways, string.Empty);
        if (AdminUtilsService.ServerRunMode == ServerRunModes.Console)
          Console.WriteLine(LocalizationHolder.rm.GetString("Kernel_1146"), (object) str1, (object) ex.Message);
        storages.Rows[index][0] = (object) 0;
      }
    }
    storages.AcceptChanges();
    KernelUpdate.UpdateAttributeIDinStorage(dbManager, storages);
  }

  internal static void UpdateAttributeIDinStorage(IDbManager dbManager, DataTable storages)
  {
    DataTable dataTable1 = dbManager.ExecuteDataTable(sc_12780.ssp_appserver_12825(), dbManager.Parameter("at1", (object) 6), dbManager.Parameter("at2", (object) 11));
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < dataTable1.Rows.Count; ++index)
      stringBuilder.Append(dataTable1.Rows[index][0].ToString() + ",");
    --stringBuilder.Length;
    List<string> stringList = new List<string>();
    stringList.Add("IMS_OBJECT_ATTRS");
    stringList.Add("IMS_RELATION_ATTRS");
    stringList.Add("IMS_OBJ_SNAPATTRS");
    stringList.Add("IMS_REL_SNAPATTRS");
    DataTable dataTable2 = dbManager.ExecuteDataTable("SELECT F_OBJECT_TYPE, F_OPTIONS FROM IMS_OBJECT_TYPES");
    for (int index = 0; index < dataTable2.Rows.Count; ++index)
    {
      if ((Convert.ToInt32(dataTable2.Rows[index][1]) & 16 /*0x10*/) == 16 /*0x10*/)
        stringList.Add("IMV_A" + Convert.ToString(dataTable2.Rows[index][0]));
    }
    for (int index1 = 0; index1 < stringList.Count; ++index1)
    {
      DataTable dataTable3 = dbManager.ExecuteDataTable($"SELECT F_DOUBLE_VALUE, F_INTEGER_VALUE, F_ATTRIBUTE_ID FROM {stringList[index1]} WHERE F_ATTRIBUTE_ID IN ({stringBuilder.ToString()})");
      for (int index2 = 0; index2 < dataTable3.Rows.Count; ++index2)
      {
        long int64_1 = Convert.ToInt64(dataTable3.Rows[index2][0]);
        long int64_2 = Convert.ToInt64(dataTable3.Rows[index2][1]);
        for (int index3 = 0; index3 < storages.Rows.Count; ++index3)
        {
          if (int64_1 > 0L && Convert.ToInt64(storages.Rows[index3][0]) == int64_1)
            dbManager.ExecuteNonQuery($"UPDATE {storages.Rows[index3][2].ToString().Trim()} SET F_ATTRIBUTE_ID = :attrID WHERE F_FILE_ID = :fileID", dbManager.Parameter("attrID", dataTable3.Rows[index2][2]), dbManager.Parameter("fileID", (object) int64_2));
        }
      }
    }
  }

  internal static void CreateTimedEventsTable(IDbManager dbManager)
  {
    if (dbManager.DataProvider.Name == "Sql")
    {
      dbManager.ExecuteNonQuery("CREATE TABLE IMS_TIMED_EVENTS (F_KEY                int NOT NULL IDENTITY(1000,1),F_GUID_TYPE          GUID_DEF,F_STRING_INFO        MaximumString_DEF,F_DATE               datetime NOT NULL,F_INT_INFO           SmallNumber_DEF,F_USER_ID            BigNumber_DEF NOT NULL,F_OBJECT_ID          BigNumber_DEF NULL,F_DEADLOCK_DATE      datetime NULL,F_TRY_COUNT          int NOT NULL DEFAULT 1)");
      dbManager.ExecuteNonQuery("ALTER TABLE IMS_TIMED_EVENTS ADD PRIMARY KEY CLUSTERED (F_KEY)");
    }
    else if (dbManager.DataProvider.Name == "Oracle")
    {
      dbManager.ExecuteNonQuery("CREATE TABLE IMS_TIMED_EVENTS (F_KEY                INTEGER NOT NULL,F_GUID_TYPE          VARCHAR2(40) NULL,F_STRING_INFO        NVARCHAR2(450) NULL,F_DATE               DATE NULL,F_INT_INFO           SMALLINT NULL,F_USER_ID            INTEGER NOT NULL,F_OBJECT_ID          INTEGER NULL,F_DEADLOCK_DATE      DATE NULL,F_TRY_COUNT          INTEGER DEFAULT 1 NOT NULL)");
      dbManager.ExecuteNonQuery("ALTER TABLE IMS_TIMED_EVENTS ADD  ( PRIMARY KEY (F_KEY) )");
    }
    dbManager.ExecuteNonQuery("CREATE INDEX IMS_TE_DATE ON IMS_TIMED_EVENTS (F_DATE)");
    dbManager.ExecuteNonQuery("CREATE INDEX IMS_TE_DEADLOCK_DATE ON IMS_TIMED_EVENTS (F_DEADLOCK_DATE)");
  }

  internal static void RepairObjectLinksTable(IDbManager dbManager, IEventLogHelper eventLogHelper)
  {
    DataTable dataTable = dbManager.ExecuteDataTable("SELECT F_OBJECT_TYPE, F_OPTIONS FROM IMS_OBJECT_TYPES");
    dbManager.BeginTransaction();
    try
    {
      dbManager.SetAdminCommandTimeout();
      dbManager.ExecuteNonQuery("DELETE FROM IMS_OBJECT_LINKS");
      dbManager.ExecuteNonQuery($"INSERT INTO IMS_OBJECT_LINKS (F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID, F_TOOBJECT_ID) SELECT AO.F_OBJECT_ID, AO.F_ATTRIBUTE_ID, AO.F_INLIST_ID, AO.F_INTEGER_VALUE FROM IMS_OBJECT_ATTRS AO, IMS_ATTRIBUTES A WHERE A.F_ATTRIBUTE_ID = AO.F_ATTRIBUTE_ID AND A.F_ATTRIBUTE_TYPE = {8} AND (AO.F_INTEGER_VALUE IS NOT NULL) AND AO.F_INTEGER_VALUE <> 0 AND exists(select * from IMS_OBJECTS O where O.F_OBJECT_ID = AO.F_OBJECT_ID)");
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        if ((Convert.ToInt32(dataTable.Rows[index][1]) & 16 /*0x10*/) == 16 /*0x10*/)
          dbManager.ExecuteNonQuery(string.Format("INSERT INTO IMS_OBJECT_LINKS (F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID, F_TOOBJECT_ID) SELECT AO.F_OBJECT_ID, AO.F_ATTRIBUTE_ID, AO.F_INLIST_ID, AO.F_INTEGER_VALUE FROM IMV_A{1} AO, IMS_ATTRIBUTES A WHERE A.F_ATTRIBUTE_ID = AO.F_ATTRIBUTE_ID AND A.F_ATTRIBUTE_TYPE = {0} AND (AO.F_INTEGER_VALUE IS NOT NULL) AND AO.F_INTEGER_VALUE <> 0 AND exists(select * from IMS_OBJECTS O where O.F_OBJECT_ID = AO.F_OBJECT_ID)", (object) 8, (object) dataTable.Rows[index][0].ToString()));
      }
      dbManager.Commit();
      dbManager.BeginTransaction();
      dbManager.ExecuteNonQuery("DELETE FROM IMS_ID_LINKS");
      dbManager.ExecuteNonQuery($"INSERT INTO IMS_ID_LINKS (F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID, F_TO_ID) SELECT AO.F_OBJECT_ID, AO.F_ATTRIBUTE_ID, AO.F_INLIST_ID, AO.F_INTEGER_VALUE FROM IMS_OBJECT_ATTRS AO, IMS_ATTRIBUTES A WHERE A.F_ATTRIBUTE_ID = AO.F_ATTRIBUTE_ID AND A.F_ATTRIBUTE_TYPE = {17} AND (AO.F_INTEGER_VALUE IS NOT NULL) AND AO.F_INTEGER_VALUE <> 0 AND exists(select * from IMS_OBJECTS O where O.F_OBJECT_ID = AO.F_OBJECT_ID)");
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        if ((Convert.ToInt32(dataTable.Rows[index][1]) & 16 /*0x10*/) == 16 /*0x10*/)
          dbManager.ExecuteNonQuery(string.Format("INSERT INTO IMS_ID_LINKS (F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID, F_TO_ID) SELECT AO.F_OBJECT_ID, AO.F_ATTRIBUTE_ID, AO.F_INLIST_ID, AO.F_INTEGER_VALUE FROM IMV_A{1} AO, IMS_ATTRIBUTES A WHERE A.F_ATTRIBUTE_ID = AO.F_ATTRIBUTE_ID AND A.F_ATTRIBUTE_TYPE = {0} AND (AO.F_INTEGER_VALUE IS NOT NULL) AND AO.F_INTEGER_VALUE <> 0 AND exists(select * from IMS_OBJECTS O where O.F_OBJECT_ID = AO.F_OBJECT_ID)", (object) 17, (object) dataTable.Rows[index][0].ToString()));
      }
      dbManager.Commit();
    }
    catch
    {
      dbManager.Rollback();
      throw;
    }
    finally
    {
      dbManager.SetNormalCommandTimeout();
    }
  }

  private void PatchDB17(IDbManager dbManager, string tableName, IEventLogHelper eventLogHelper)
  {
    try
    {
      if (dbManager.DataProvider.Name == "Sql")
      {
        dbManager.ExecuteNonQuery($"ALTER TABLE {tableName} ADD F_MODIFICATION_ID BigNumber_DEF DEFAULT 0 NOT NULL");
        dbManager.ExecuteNonQuery($"ALTER TABLE {tableName} ADD F_BASE_VERSION BigNumber_DEF DEFAULT 0 NOT NULL");
      }
      else if (dbManager.DataProvider.Name == "Oracle")
      {
        dbManager.ExecuteNonQuery($"ALTER TABLE {tableName} ADD F_MODIFICATION_ID INTEGER DEFAULT 0 NOT NULL");
        dbManager.ExecuteNonQuery($"ALTER TABLE {tableName} ADD F_BASE_VERSION INTEGER DEFAULT 0 NOT NULL");
      }
      dbManager.ExecuteNonQuery(string.Format("CREATE INDEX {0}_MODIFICATION_ID ON {0} (F_MODIFICATION_ID)", (object) tableName));
      dbManager.ExecuteNonQuery(string.Format("CREATE INDEX {0}_BASE_VERSION ON {0} (F_BASE_VERSION)", (object) tableName));
      dbManager.ExecuteNonQuery(string.Format("UPDATE {0} SET F_BASE_VERSION = 1 WHERE F_VERSION_ID = (SELECT MAX(A.F_VERSION_ID) FROM {0} A WHERE A.F_ID = {0}.F_ID)", (object) tableName));
    }
    catch (Exception ex)
    {
      eventLogHelper.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_1147"), (object) tableName, (object) ex.Message), Intermech.Consts.traceAlways, string.Empty);
    }
  }

  private void CreateSnapshotTables(IDbManager dbManager, IEventLogHelper eventLogHelper)
  {
    if (dbManager.DataProvider.Name == "Sql")
    {
      dbManager.ExecuteNonQuery("CREATE TABLE IMS_OBJ_SNAPSHOT (F_SNAPSHOT_ID        BigNumber_DEF NOT NULL,F_OBJECT_ID          BigNumber_DEF NOT NULL,F_ID                 BigNumber_DEF NOT NULL,F_LC_STEP            int NOT NULL,F_VERSION_ID         int NOT NULL,F_OBJECT_TYPE        int NOT NULL,F_OWNER_ID           BigNumber_DEF NOT NULL,F_MODIFY_DATE        datetime NOT NULL,F_LEVEL_ID           int NOT NULL,F_OBJ_CREATE         datetime NOT NULL,F_PROJECT_ID         BigNumber_DEF NOT NULL,F_MODIFICATION_ID    BigNumber_DEF NOT NULL,CAPTION              MaximumString_DEF NULL,F_SITE_ID            VARCHAR(2) NULL,F_NOTE               MaximumString_DEF NULL,F_USER_ID            BigNumber_DEF NOT NULL,F_SNAPSHOT_DATE      datetime NOT NULL)");
      dbManager.ExecuteNonQuery("ALTER TABLE IMS_OBJ_SNAPSHOT ADD CONSTRAINT IMS_OBJ_SNAPSHOT_PK PRIMARY KEY CLUSTERED (F_SNAPSHOT_ID, F_OBJECT_ID)");
      dbManager.ExecuteNonQuery("CREATE TABLE IMS_REL_SNAPSHOT (F_SNAPSHOT_ID        BigNumber_DEF NOT NULL,F_PRJLINK_ID         BigNumber_DEF NOT NULL,F_PROJ_ID            BigNumber_DEF NOT NULL,F_PART_ID            BigNumber_DEF NOT NULL,F_RELATION_TYPE      int NOT NULL,F_CREATE_DATE        datetime NOT NULL,F_DELETE_DATE        datetime NULL,F_PRJ_GUID           GUID_DEF)");
      dbManager.ExecuteNonQuery("ALTER TABLE IMS_REL_SNAPSHOT ADD CONSTRAINT IMS_REL_SNAPSHOT_PK PRIMARY KEY CLUSTERED (F_SNAPSHOT_ID, F_PRJLINK_ID)");
      dbManager.ExecuteNonQuery("CREATE TABLE IMS_BLOBS_SNAPSHOT (F_KEY                int NOT NULL IDENTITY(1,1),F_SNAPSHOT_ID        BigNumber_DEF NOT NULL,F_VALUE              image NULL,F_FILESIZE           BigNumber_DEF NOT NULL DEFAULT 0,F_FILEDATE           datetime NULL,F_ARC_METHOD         SmallNumber_DEF NOT NULL DEFAULT 0,F_ZIPSIZE            BigNumber_DEF NOT NULL DEFAULT 0)");
      dbManager.ExecuteNonQuery("ALTER TABLE IMS_BLOBS_SNAPSHOT ADD CONSTRAINT IMS_BLOBS_SNAPSHOT_PK PRIMARY KEY CLUSTERED (F_KEY)");
      dbManager.ExecuteNonQuery("CREATE TABLE IMS_MEMOS_SNAPSHOT (F_KEY                int NOT NULL IDENTITY(1,1),F_SNAPSHOT_ID        BigNumber_DEF NOT NULL,F_VALUE              Memo_DEF)");
      dbManager.ExecuteNonQuery("ALTER TABLE IMS_MEMOS_SNAPSHOT ADD CONSTRAINT IMS_MEMOS_SNAPSHOT_PK PRIMARY KEY CLUSTERED (F_KEY)");
      dbManager.ExecuteNonQuery("CREATE TABLE IMS_OBJ_SNAPATTRS (F_SNAPSHOT_ID        BigNumber_DEF NOT NULL,F_OBJECT_ID          BigNumber_DEF NOT NULL,F_ATTRIBUTE_ID       int NOT NULL,F_INLIST_ID          int NOT NULL,F_INTEGER_VALUE      BigNumber_DEF NULL,F_STRING_VALUE       MaximumString_DEF,F_DOUBLE_VALUE       float NULL,F_DATE_VALUE         datetime NULL)");
      dbManager.ExecuteNonQuery("ALTER TABLE IMS_OBJ_SNAPATTRS ADD CONSTRAINT IMS_OBJ_SNAPATTRS_PK PRIMARY KEY CLUSTERED (F_SNAPSHOT_ID, F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID)");
      dbManager.ExecuteNonQuery("CREATE TABLE IMS_REL_SNAPATTRS (F_SNAPSHOT_ID        BigNumber_DEF NOT NULL,F_PRJLINK_ID         BigNumber_DEF NOT NULL,F_ATTRIBUTE_ID       int NOT NULL,F_INLIST_ID          int NOT NULL,F_INTEGER_VALUE      BigNumber_DEF NULL,F_STRING_VALUE       MaximumString_DEF,F_DOUBLE_VALUE       float NULL,F_DATE_VALUE         datetime NULL)");
      dbManager.ExecuteNonQuery("ALTER TABLE IMS_REL_SNAPATTRS ADD CONSTRAINT IMS_REL_SNAPATTRS_PK PRIMARY KEY CLUSTERED (F_SNAPSHOT_ID, F_PRJLINK_ID, F_ATTRIBUTE_ID, F_INLIST_ID)");
    }
    else if (dbManager.DataProvider.Name == "Oracle")
    {
      dbManager.ExecuteNonQuery("CREATE TABLE IMS_OBJ_SNAPSHOT (F_SNAPSHOT_ID        INTEGER NOT NULL,F_OBJECT_ID          INTEGER NOT NULL,F_ID                 INTEGER NOT NULL,F_LC_STEP            INTEGER NOT NULL,F_VERSION_ID         INTEGER NOT NULL,F_OBJECT_TYPE        INTEGER NOT NULL,F_OWNER_ID           INTEGER NOT NULL,F_MODIFY_DATE        DATE NOT NULL,F_LEVEL_ID           INTEGER NOT NULL,F_OBJ_CREATE         DATE NOT NULL,F_PROJECT_ID         INTEGER NOT NULL,F_MODIFICATION_ID    INTEGER NOT NULL,CAPTION              NVARCHAR2(450) NULL,F_SITE_ID            VARCHAR2(2) NULL,F_NOTE               NVARCHAR2(450) NULL,F_USER_ID            INTEGER NOT NULL,F_SNAPSHOT_DATE      DATE NOT NULL)");
      dbManager.ExecuteNonQuery("ALTER TABLE IMS_OBJ_SNAPSHOT ADD CONSTRAINT IMS_OBJ_SNAPSHOT_PK PRIMARY KEY (F_SNAPSHOT_ID, F_OBJECT_ID)");
      dbManager.ExecuteNonQuery("CREATE TABLE IMS_REL_SNAPSHOT (F_SNAPSHOT_ID        INTEGER NOT NULL,F_PRJLINK_ID         INTEGER NOT NULL,F_PROJ_ID            INTEGER NOT NULL,F_PART_ID            INTEGER NOT NULL,F_RELATION_TYPE      INTEGER NOT NULL,F_CREATE_DATE        DATE NOT NULL,F_DELETE_DATE        DATE NULL,F_PRJ_GUID           VARCHAR2(40) NOT NULL)");
      dbManager.ExecuteNonQuery("ALTER TABLE IMS_REL_SNAPSHOT ADD CONSTRAINT IMS_REL_SNAPSHOT_PK PRIMARY KEY (F_SNAPSHOT_ID, F_PRJLINK_ID)");
      dbManager.ExecuteNonQuery("CREATE TABLE IMS_BLOBS_SNAPSHOT (F_KEY                INTEGER NOT NULL,F_SNAPSHOT_ID        INTEGER NOT NULL,F_VALUE              BLOB NULL,F_FILESIZE           INTEGER DEFAULT 0 NOT NULL,F_FILEDATE           DATE NULL,F_ARC_METHOD         SMALLINT DEFAULT 0 NOT NULL,F_ZIPSIZE            INTEGER DEFAULT 0 NOT NULL)");
      dbManager.ExecuteNonQuery("ALTER TABLE IMS_BLOBS_SNAPSHOT ADD CONSTRAINT IMS_BLOBS_SNAPSHOT_PK PRIMARY KEY (F_KEY)");
      dbManager.ExecuteNonQuery("CREATE TABLE IMS_MEMOS_SNAPSHOT (F_KEY                INTEGER NOT NULL,F_SNAPSHOT_ID        INTEGER NOT NULL,F_VALUE              NCLOB NULL)");
      dbManager.ExecuteNonQuery("ALTER TABLE IMS_MEMOS_SNAPSHOT ADD CONSTRAINT IMS_MEMOS_SNAPSHOT_PK PRIMARY KEY (F_KEY)");
      dbManager.ExecuteNonQuery("CREATE TABLE IMS_OBJ_SNAPATTRS (F_SNAPSHOT_ID        INTEGER NOT NULL,F_OBJECT_ID          INTEGER NOT NULL,F_ATTRIBUTE_ID       INTEGER NOT NULL,F_INLIST_ID          INTEGER NOT NULL,F_INTEGER_VALUE      INTEGER NULL,F_STRING_VALUE       NVARCHAR2(450) NULL,F_DOUBLE_VALUE       FLOAT NULL,F_DATE_VALUE         DATE NULL)");
      dbManager.ExecuteNonQuery("ALTER TABLE IMS_OBJ_SNAPATTRS ADD CONSTRAINT IMS_OBJ_SNAPATTRS_PK PRIMARY KEY (F_SNAPSHOT_ID, F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID)");
      dbManager.ExecuteNonQuery("CREATE TABLE IMS_REL_SNAPATTRS (F_SNAPSHOT_ID        INTEGER NOT NULL,F_PRJLINK_ID         INTEGER NOT NULL,F_ATTRIBUTE_ID       INTEGER NOT NULL,F_INLIST_ID          INTEGER NOT NULL,F_INTEGER_VALUE      INTEGER NULL,F_STRING_VALUE       NVARCHAR2(450) NULL,F_DOUBLE_VALUE       FLOAT NULL,F_DATE_VALUE         DATE NULL)");
      dbManager.ExecuteNonQuery("ALTER TABLE IMS_REL_SNAPATTRS ADD CONSTRAINT IMS_REL_SNAPATTRS_PK PRIMARY KEY (F_SNAPSHOT_ID, F_PRJLINK_ID, F_ATTRIBUTE_ID, F_INLIST_ID)");
      dbManager.ExecuteNonQuery("CREATE SEQUENCE IMS_BLOBS_SNAPSHOT_GEN START WITH 1 INCREMENT BY 1 NOMAXVALUE MINVALUE 1 NOCYCLE CACHE 5 NOORDER");
      dbManager.ExecuteNonQuery("CREATE SEQUENCE IMS_MEMOS_SNAPSHOT_GEN START WITH 1 INCREMENT BY 1 NOMAXVALUE MINVALUE 1 NOCYCLE CACHE 5 NOORDER");
    }
    dbManager.ExecuteNonQuery("CREATE INDEX IMS_OBJ_SNAP_ID ON IMS_OBJ_SNAPSHOT (F_ID)");
    dbManager.ExecuteNonQuery("CREATE INDEX IMS_OBJ_SNAP_TYPE ON IMS_OBJ_SNAPSHOT (F_OBJECT_TYPE)");
    dbManager.ExecuteNonQuery("CREATE INDEX IMS_BLOBS_SNAP_ID ON IMS_BLOBS_SNAPSHOT (F_SNAPSHOT_ID)");
    dbManager.ExecuteNonQuery("CREATE INDEX IMS_MEMOS_SNAP_ID ON IMS_MEMOS_SNAPSHOT (F_SNAPSHOT_ID)");
    dbManager.ExecuteNonQuery("CREATE INDEX IMS_OBJ_SNAPATTRS_ATTR ON IMS_OBJ_SNAPATTRS (F_ATTRIBUTE_ID)");
    dbManager.ExecuteNonQuery("CREATE INDEX IMS_OBJ_SNAPATTRS_INT ON IMS_OBJ_SNAPATTRS (F_INTEGER_VALUE)");
    dbManager.ExecuteNonQuery("CREATE INDEX IMS_OBJ_SNAPATTRS_DOUBLE ON IMS_OBJ_SNAPATTRS (F_DOUBLE_VALUE)");
    dbManager.ExecuteNonQuery("CREATE INDEX IMS_REL_SNAPATTRS_ATTR ON IMS_REL_SNAPATTRS (F_ATTRIBUTE_ID)");
    dbManager.ExecuteNonQuery("CREATE INDEX IMS_REL_SNAPATTRS_INT ON IMS_REL_SNAPATTRS (F_INTEGER_VALUE)");
    dbManager.ExecuteNonQuery("CREATE INDEX IMS_REL_SNAPATTRS_DOUBLE ON IMS_REL_SNAPATTRS (F_DOUBLE_VALUE)");
  }

  private void CreateMDExtensionsTriggers(IDbManager dbManager, IEventLogHelper eventLogHelper)
  {
    if (dbManager.DataProvider.Name == "Sql")
    {
      try
      {
        dbManager.ExecuteNonQuery("DROP TRIGGER IMS_MD_EXTENSIONS_INS_M");
      }
      catch
      {
      }
      dbManager.ExecuteNonQuery("CREATE TRIGGER IMS_MD_EXTENSIONS_INS_M ON IMS_MD_EXTENSIONS FOR INSERT AS set nocount on UPDATE IMS_METADATA SET F_MODIFY_DATE = GETUTCDATE() WHERE F_TABLE_NAME = 'IMS_MD_EXTENSIONS'");
      try
      {
        dbManager.ExecuteNonQuery("DROP TRIGGER IMS_MD_EXTENSIONS_UPD_M");
      }
      catch
      {
      }
      dbManager.ExecuteNonQuery("CREATE TRIGGER IMS_MD_EXTENSIONS_UPD_M ON IMS_MD_EXTENSIONS FOR UPDATE AS set nocount on UPDATE IMS_METADATA SET F_MODIFY_DATE = GETUTCDATE() WHERE F_TABLE_NAME = 'IMS_MD_EXTENSIONS'");
      try
      {
        dbManager.ExecuteNonQuery("DROP TRIGGER IMS_MD_EXTENSIONS_DLT_M");
      }
      catch
      {
      }
      dbManager.ExecuteNonQuery("CREATE TRIGGER IMS_MD_EXTENSIONS_DLT_M ON IMS_MD_EXTENSIONS FOR DELETE AS set nocount on UPDATE IMS_METADATA SET F_MODIFY_DATE = GETUTCDATE() WHERE F_TABLE_NAME = 'IMS_MD_EXTENSIONS'");
    }
    else
    {
      if (!(dbManager.DataProvider.Name == "Oracle"))
        return;
      dbManager.ExecuteNonQuery("CREATE OR REPLACE TRIGGER IMS_MD_EXTENSIONS_IUD_M AFTER INSERT OR DELETE OR UPDATE ON IMS_MD_EXTENSIONS BEGIN UPDATE IMS_METADATA SET F_MODIFY_DATE = SYS_EXTRACT_UTC(SYSTIMESTAMP) WHERE F_TABLE_NAME = 'IMS_MD_EXTENSIONS'; END;");
    }
  }

  private void PatchDB28(IDbManager dbManager, IEventLogHelper eventLogHelper)
  {
    try
    {
      if (dbManager.DataProvider.Name == "Sql")
      {
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_RELATION_TYPES ADD F_OPTIONS INT NOT NULL DEFAULT 0");
      }
      else
      {
        if (!(dbManager.DataProvider.Name == "Oracle"))
          return;
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_RELATION_TYPES ADD F_OPTIONS INTEGER DEFAULT 0 NOT NULL");
      }
    }
    catch (Exception ex)
    {
      eventLogHelper.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_1148"), (object) ex.Message), Intermech.Consts.traceAlways, string.Empty);
    }
  }

  private void CreateGlobalIndex(IDbManager dbManager, IEventLogHelper eventLogHelper)
  {
    if (dbManager.DataProvider.Name == "Sql")
    {
      dbManager.ExecuteNonQuery("CREATE TABLE IMS_INDEX_WORDS (F_WORD               MaximumString_DEF NOT NULL,F_WORD_ID            BigNumber_DEF NOT NULL,F_OBJECT_COUNT       int NOT NULL)");
      dbManager.ExecuteNonQuery("ALTER TABLE IMS_INDEX_WORDS ADD CONSTRAINT IMS_INDEX_WORDS_PK PRIMARY KEY CLUSTERED (F_WORD)");
      dbManager.ExecuteNonQuery("CREATE TABLE IMS_WORD_ID_GEN (F_WORD_ID                BigNumber_DEF NOT NULL IDENTITY(1,1), F_CREATED DATETIME DEFAULT GETUTCDATE())");
      dbManager.ExecuteNonQuery("CREATE TABLE IMS_GLOBAL_INDEX (F_OBJECT_ID          BigNumber_DEF NOT NULL,F_ATTRIBUTE_ID       int NOT NULL,F_INLIST_ID          int NOT NULL,F_WORD_ID            BigNumber_DEF NOT NULL,F_ID                 BigNumber_DEF NOT NULL,F_TF                 FLOAT NOT NULL,F_WORD_REPEAT        int NOT NULL)");
      dbManager.ExecuteNonQuery("ALTER TABLE IMS_GLOBAL_INDEX ADD CONSTRAINT IMS_GLOBAL_INDEX_PK PRIMARY KEY CLUSTERED (F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID, F_WORD_ID)");
      dbManager.ExecuteNonQuery("CREATE TABLE IMS_INDEX_RESULT (F_OBJECT_ID          BigNumber_DEF NOT NULL,F_WORD_ID            BigNumber_DEF NOT NULL,F_TF_IDF             FLOAT NOT NULL)");
      dbManager.ExecuteNonQuery("ALTER TABLE IMS_INDEX_RESULT ADD CONSTRAINT IMS_INDEX_RESULT_PK PRIMARY KEY CLUSTERED (F_OBJECT_ID, F_WORD_ID)");
      dbManager.ExecuteNonQuery("CREATE TABLE IMS_INDEX_QUEUE (F_KEY                BigNumber_DEF NOT NULL IDENTITY(1,1),F_OBJECT_ID          BigNumber_DEF NOT NULL,F_ATTRIBUTE_ID       int NOT NULL,F_INLIST_ID          int NOT NULL,F_MODIFY_DATE        datetime NOT NULL)");
      dbManager.ExecuteNonQuery("ALTER TABLE IMS_INDEX_QUEUE ADD CONSTRAINT IMS_INDEX_QUEUE_PK PRIMARY KEY CLUSTERED (F_KEY)");
    }
    else if (dbManager.DataProvider.Name == "Oracle")
    {
      dbManager.ExecuteNonQuery("CREATE TABLE IMS_INDEX_WORDS (F_WORD        NVARCHAR2(450) NOT NULL,F_WORD_ID         INTEGER NOT NULL,F_OBJECT_COUNT            INTEGER NOT NULL)");
      dbManager.ExecuteNonQuery("ALTER TABLE IMS_INDEX_WORDS ADD CONSTRAINT IMS_INDEX_WORDS_PK PRIMARY KEY (F_WORD)");
      dbManager.ExecuteNonQuery("CREATE SEQUENCE IMS_WORD_ID_GEN START WITH 1 INCREMENT BY 1 NOMAXVALUE MINVALUE 1 NOCYCLE CACHE 5 NOORDER");
      dbManager.ExecuteNonQuery("CREATE TABLE IMS_GLOBAL_INDEX (F_OBJECT_ID          INTEGER NOT NULL,F_ATTRIBUTE_ID       INTEGER NOT NULL,F_INLIST_ID          INTEGER NOT NULL,F_WORD_ID            INTEGER NOT NULL,F_ID                 INTEGER NOT NULL,F_TF                 FLOAT NOT NULL,F_WORD_REPEAT        INTEGER NOT NULL)");
      dbManager.ExecuteNonQuery("ALTER TABLE IMS_GLOBAL_INDEX ADD CONSTRAINT IMS_GLOBAL_INDEX_PK PRIMARY KEY (F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID, F_WORD_ID)");
      dbManager.ExecuteNonQuery("CREATE TABLE IMS_INDEX_RESULT (F_OBJECT_ID          INTEGER NOT NULL,F_WORD_ID            INTEGER NOT NULL,F_TF_IDF             FLOAT NOT NULL)");
      dbManager.ExecuteNonQuery("ALTER TABLE IMS_INDEX_RESULT ADD CONSTRAINT IMS_INDEX_RESULT_PK PRIMARY KEY (F_OBJECT_ID, F_WORD_ID)");
      dbManager.ExecuteNonQuery("CREATE TABLE IMS_INDEX_QUEUE (F_KEY                INTEGER NOT NULL,F_OBJECT_ID          INTEGER NOT NULL,F_ATTRIBUTE_ID       INTEGER NOT NULL,F_INLIST_ID          INTEGER NOT NULL,F_MODIFY_DATE        DATE NOT NULL)");
      dbManager.ExecuteNonQuery("ALTER TABLE IMS_INDEX_QUEUE ADD CONSTRAINT IMS_INDEX_QUEUE_PK PRIMARY KEY (F_KEY)");
      dbManager.ExecuteNonQuery("CREATE SEQUENCE IMS_WORD_QUEUE_GEN START WITH 1 INCREMENT BY 1 NOMAXVALUE MINVALUE 1 NOCYCLE CACHE 5 NOORDER");
    }
    dbManager.ExecuteNonQuery("CREATE UNIQUE INDEX IMS_WORDS_WORD_ID ON IMS_INDEX_WORDS (F_WORD_ID)");
    dbManager.ExecuteNonQuery("CREATE INDEX IMS_WORDS_TF_IDF ON IMS_INDEX_RESULT (F_TF_IDF)");
    dbManager.ExecuteNonQuery("CREATE INDEX IMS_WORDS_QUEUE_OBJ ON IMS_INDEX_QUEUE (F_OBJECT_ID)");
  }

  private void PatchDB53(IDbManager dbManager, IEventLogHelper eventLogHelper)
  {
    int int32_1 = Convert.ToInt32(dbManager.ExecuteScalar("SELECT F_OBJECT_TYPE FROM IMS_OBJECT_TYPES WHERE F_GUID = :fguid", dbManager.Parameter("fguid", (object) "cad00014-306c-11d8-b4e9-00304f19f545")));
    int int32_2 = Convert.ToInt32(dbManager.ExecuteScalar("SELECT F_ATTRIBUTE_ID FROM IMS_ATTRIBUTES WHERE F_GUID = :fguid", dbManager.Parameter("fguid", (object) "cad00028-306c-11d8-b4e9-00304f19f545")));
    DataTable dataTable = dbManager.ExecuteDataTable($"SELECT IMS_OBJECTS.F_OBJECT_ID, IMS_GUID.CAPTION, IMS_GUID.CAPTION FROM IMS_OBJECTS, IMS_GUID WHERE IMS_OBJECTS.F_OBJECT_TYPE = {int32_1.ToString()} AND IMS_GUID.F_OBJECT_ID = IMS_OBJECTS.F_OBJECT_ID AND IMS_OBJECTS.F_OBJECT_VER_TYPE = 0");
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      long int64 = Convert.ToInt64(dataTable.Rows[index][0]);
      string str1 = dataTable.Rows[index][1].ToString();
      string str2 = "IMV_A" + int32_1.ToString();
      string str3;
      try
      {
        str3 = Convert.ToString(dbManager.ExecuteScalar($"SELECT F_STRING_VALUE FROM {str2} WHERE F_OBJECT_ID = {int64} AND F_ATTRIBUTE_ID = {int32_2} AND F_INLIST_ID = 0"));
      }
      catch
      {
        str3 = Convert.ToString(dbManager.ExecuteScalar($"SELECT F_STRING_VALUE FROM {"IMS_OBJECT_ATTRS"} WHERE F_OBJECT_ID = {int64} AND F_ATTRIBUTE_ID = {int32_2} AND F_INLIST_ID = 0"));
      }
      if (str3 == string.Empty)
        str3 = "IMS_STORAGE";
      try
      {
        if (dbManager.ExecuteDataTable($"SELECT * FROM {str3} WHERE F_FILE_ID = 0").Columns.IndexOf("F_LINKTYPE") < 0)
        {
          if (dbManager.DataProvider.Name == "Sql")
            dbManager.ExecuteNonQuery($"ALTER TABLE {str3} ADD F_LINKTYPE INTEGER NOT NULL DEFAULT 0");
          else if (dbManager.DataProvider.Name == "Oracle")
            dbManager.ExecuteNonQuery($"ALTER TABLE {str3} ADD F_LINKTYPE INTEGER DEFAULT 0 NOT NULL");
        }
      }
      catch (Exception ex)
      {
        eventLogHelper.AddToTrace(string.Format(LocalizationHolder.rm.GetString("AddStorageFieldError"), (object) "F_LINKTYPE", (object) str1, (object) ex.Message), Intermech.Consts.traceAlways, string.Empty);
        if (AdminUtilsService.ServerRunMode == ServerRunModes.Console)
          Console.WriteLine(LocalizationHolder.rm.GetString("AddStorageFieldError"), (object) "F_LINKTYPE", (object) str1, (object) ex.Message);
      }
      try
      {
        if (dbManager.ExecuteDataTable($"SELECT * FROM {str3} WHERE F_FILE_ID = 0").Columns.IndexOf("F_AUTHOR") < 0)
        {
          if (dbManager.DataProvider.Name == "Sql")
            dbManager.ExecuteNonQuery($"ALTER TABLE {str3} ADD F_AUTHOR BigNumber_DEF NOT NULL DEFAULT 0");
          else if (dbManager.DataProvider.Name == "Oracle")
            dbManager.ExecuteNonQuery($"ALTER TABLE {str3} ADD F_AUTHOR INTEGER DEFAULT 0 NOT NULL");
        }
      }
      catch (Exception ex)
      {
        eventLogHelper.AddToTrace(string.Format(LocalizationHolder.rm.GetString("AddStorageFieldError"), (object) "F_AUTHOR", (object) str1, (object) ex.Message), Intermech.Consts.traceAlways, string.Empty);
        if (AdminUtilsService.ServerRunMode == ServerRunModes.Console)
          Console.WriteLine(LocalizationHolder.rm.GetString("AddStorageFieldError"), (object) "F_AUTHOR", (object) str1, (object) ex.Message);
      }
    }
  }

  private void PatchDB54(IDbManager dbManager, IEventLogHelper eventLogHelper)
  {
    if (dbManager.DataProvider.Name == "Sql")
    {
      dbManager.ExecuteNonQuery("CREATE TABLE IMS_SNAPSHOTS (F_SNAPSHOT_ID        BigNumber_DEF NOT NULL,F_OBJECT_ID          BigNumber_DEF NOT NULL,F_ID                 BigNumber_DEF NOT NULL,F_NAME               MaximumString_DEF NULL,F_USER_ID            BigNumber_DEF NOT NULL,F_SNAPSHOT_DATE      datetime NOT NULL)");
      dbManager.ExecuteNonQuery("ALTER TABLE IMS_SNAPSHOTS ADD CONSTRAINT IMS_SNAPSHOTS_PK PRIMARY KEY CLUSTERED (F_SNAPSHOT_ID)");
    }
    else if (dbManager.DataProvider.Name == "Oracle")
    {
      dbManager.ExecuteNonQuery("CREATE TABLE IMS_SNAPSHOTS (F_SNAPSHOT_ID        INTEGER NOT NULL,F_OBJECT_ID          INTEGER NOT NULL,F_ID                 INTEGER NOT NULL,F_NAME               NVARCHAR2(450) NULL,F_USER_ID            INTEGER NOT NULL,F_SNAPSHOT_DATE      DATE NOT NULL)");
      dbManager.ExecuteNonQuery("ALTER TABLE IMS_SNAPSHOTS ADD CONSTRAINT IMS_SNAPSHOTS_PK PRIMARY KEY (F_SNAPSHOT_ID)");
    }
    DataTable dataTable = dbManager.ExecuteDataTable("SELECT F_SNAPSHOT_ID, F_OBJECT_ID, F_ID, F_NOTE, F_USER_ID, F_SNAPSHOT_DATE FROM IMS_OBJ_SNAPSHOT ORDER BY F_SNAPSHOT_ID");
    long num = 0;
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      long int64 = Convert.ToInt64(dataTable.Rows[index][0]);
      if (num != int64)
        dbManager.ExecuteNonQuery("INSERT INTO IMS_SNAPSHOTS (F_SNAPSHOT_ID, F_OBJECT_ID, F_ID, F_NAME, F_USER_ID, F_SNAPSHOT_DATE) VALUES (:snapID, :param1, :param2, :param3, :param4, :param5)", dbManager.Parameter(":snapID", (object) int64), dbManager.Parameter(":param1", dataTable.Rows[index][1]), dbManager.Parameter(":param2", dataTable.Rows[index][2]), dbManager.Parameter(":param3", dataTable.Rows[index][3]), dbManager.Parameter(":param4", dataTable.Rows[index][4]), dbManager.Parameter(":param5", dataTable.Rows[index][5]));
      num = int64;
    }
    dbManager.ExecuteNonQuery("CREATE INDEX IMS_SNAPS_OBJECT_ID ON IMS_SNAPSHOTS (F_OBJECT_ID)");
    dbManager.ExecuteNonQuery("CREATE INDEX IMS_SNAPS_ID ON IMS_SNAPSHOTS (F_ID)");
  }

  private void SetCreatorID(IDbManager dbManager, string fldName, int userTypeID)
  {
    dbManager.ExecuteNonQuery(string.Format("UPDATE {0} SET F_CREATOR_ID = F_OWNER_ID WHERE {0}.F_OWNER_ID IN (SELECT A.F_OBJECT_ID FROM IMS_OBJECTS A WHERE A.F_OBJECT_TYPE = :typeID AND A.F_OBJECT_ID = {0}.F_OWNER_ID)", (object) fldName), dbManager.Parameter("typeID", (object) userTypeID));
  }

  private void PatchDB501(IDbManager dbManager, IEventLogHelper eventLogHelper)
  {
    int int32 = Convert.ToInt32(dbManager.ExecuteScalar("SELECT F_OBJECT_TYPE FROM IMS_OBJECT_TYPES WHERE F_GUID = :fguid1", dbManager.Parameter("fguid1", (object) "cad00002-306c-11d8-b4e9-00304f19f545")));
    dbManager.SetAdminCommandTimeout();
    try
    {
      if (dbManager.DataProvider.Name == "Sql")
      {
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_OBJECTS ADD F_CREATOR_ID BigNumber_DEF NOT NULL DEFAULT 0");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_OBJECTS_VIEW ADD F_CREATOR_ID BigNumber_DEF NOT NULL DEFAULT 0");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_OBJ_SNAPSHOT ADD F_CREATOR_ID BigNumber_DEF NOT NULL DEFAULT 0");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_RELATIONS ADD F_REL_CREATOR BigNumber_DEF NOT NULL DEFAULT 0");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_REL_SNAPSHOT ADD F_REL_CREATOR BigNumber_DEF NOT NULL DEFAULT 0");
      }
      else
      {
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_OBJECTS ADD F_CREATOR_ID INTEGER DEFAULT 0 NOT NULL");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_OBJECTS_VIEW ADD F_CREATOR_ID INTEGER DEFAULT 0 NOT NULL");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_OBJ_SNAPSHOT ADD F_CREATOR_ID INTEGER DEFAULT 0 NOT NULL");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_RELATIONS ADD F_REL_CREATOR INTEGER DEFAULT 0 NOT NULL");
        dbManager.ExecuteNonQuery("ALTER TABLE IMS_REL_SNAPSHOT ADD F_REL_CREATOR INTEGER DEFAULT 0 NOT NULL");
      }
      this.SetCreatorID(dbManager, "IMS_OBJECTS", int32);
      this.SetCreatorID(dbManager, "IMS_OBJECTS_VIEW", int32);
      this.SetCreatorID(dbManager, "IMS_OBJ_SNAPSHOT", int32);
      dbManager.DataProvider.CreateIndex("IMS_OBJECTS_VIEW", "F_CREATOR_ID", dbManager, SortOrders.ASC);
      DataTable dataTable1 = dbManager.ExecuteDataTable("SELECT F_OBJECT_TYPE FROM IMS_OBJECT_TYPES");
      for (int index = 0; index < dataTable1.Rows.Count; ++index)
      {
        string str = "IMV_O" + dataTable1.Rows[index][0].ToString();
        try
        {
          dbManager.ExecuteScalar($"SELECT F_OBJECT_ID FROM {str} WHERE F_OBJECT_ID = -1");
          if (dbManager.DataProvider.Name == "Sql")
            dbManager.ExecuteNonQuery($"ALTER TABLE {str} ADD F_CREATOR_ID BigNumber_DEF NOT NULL DEFAULT 0");
          else
            dbManager.ExecuteNonQuery($"ALTER TABLE {str} ADD F_CREATOR_ID INTEGER DEFAULT 0 NOT NULL");
          this.SetCreatorID(dbManager, str, int32);
          dbManager.DataProvider.CreateIndex(str, "F_CREATOR_ID", dbManager, SortOrders.ASC);
        }
        catch
        {
        }
      }
      DataTable dataTable2 = dbManager.ExecuteDataTable("SELECT F_RELATION_TYPE FROM IMS_RELATION_TYPES");
      for (int index = 0; index < dataTable2.Rows.Count; ++index)
      {
        string str = "IMV_R" + dataTable2.Rows[index][0].ToString();
        try
        {
          dbManager.ExecuteScalar($"SELECT F_PRJLINK_ID FROM {str} WHERE F_PRJLINK_ID = -1");
          if (dbManager.DataProvider.Name == "Sql")
            dbManager.ExecuteNonQuery($"ALTER TABLE {str} ADD F_REL_CREATOR BigNumber_DEF NOT NULL DEFAULT 0");
          else
            dbManager.ExecuteNonQuery($"ALTER TABLE {str} ADD F_REL_CREATOR INTEGER DEFAULT 0 NOT NULL");
        }
        catch
        {
        }
      }
    }
    finally
    {
      dbManager.SetNormalCommandTimeout();
    }
    dbManager.BeginTransaction();
    try
    {
      if (dbManager.DataProvider.Name == "Sql")
        dbManager.ExecuteNonQuery("SET IDENTITY_INSERT IMS_ATTRIBUTES ON");
      this.AddSystemAttribute(dbManager, ObligatoryObjectAttributes.F_CREATOR_ID, "Пользователь, создавший данную версию объекта.", "cadd96b7-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      this.AddSystemAttribute(dbManager, ObligatoryObjectAttributes.F_REL_CREATOR, "Пользователь, создавший данную связь.", "cadd96b8-306c-11d8-b4e9-00304f19f545", UniqueValueModes.NotUnique);
      if (dbManager.DataProvider.Name == "Sql")
        dbManager.ExecuteNonQuery("SET IDENTITY_INSERT IMS_ATTRIBUTES OFF");
      dbManager.Commit();
    }
    catch
    {
      dbManager.Rollback();
      throw;
    }
  }

  private void PatchDB500(IDbManager dbManager, IEventLogHelper eventLogHelper)
  {
    if (dbManager.DataProvider.Name == "Sql")
    {
      dbManager.ExecuteNonQuery("CREATE TABLE IMS_SEARCH_QUERIES (F_QUERY_STR MaximumString_DEF NOT NULL,F_USER_ID BigNumber_DEF NOT NULL,F_QUERY_DATE datetime NOT NULL,F_ACCESS SmallNumber_DEF NOT NULL DEFAULT 0)");
      dbManager.ExecuteNonQuery("CREATE TABLE IMS_QUERIES_RESULT (F_QUERY_STR MaximumString_DEF NOT NULL,F_QUERY_NORM MaximumString_DEF NOT NULL,F_QUERY_COUNTER int NOT NULL,F_QUERY_DATE datetime NOT NULL)");
      dbManager.ExecuteNonQuery("ALTER TABLE IMS_QUERIES_RESULT ADD PRIMARY KEY CLUSTERED (F_QUERY_STR)");
    }
    else
    {
      dbManager.ExecuteNonQuery("CREATE TABLE IMS_SEARCH_QUERIES (F_QUERY_STR NVARCHAR2(450) NOT NULL,F_USER_ID INTEGER NOT NULL,F_QUERY_DATE DATE NOT NULL,F_ACCESS SMALLINT DEFAULT 0 NOT NULL)");
      dbManager.ExecuteNonQuery("CREATE TABLE IMS_QUERIES_RESULT (F_QUERY_STR NVARCHAR2(450) NOT NULL,F_QUERY_NORM NVARCHAR2(450) NOT NULL,F_QUERY_COUNTER INTEGER NOT NULL,F_QUERY_DATE DATE NOT NULL)");
      dbManager.ExecuteNonQuery("ALTER TABLE IMS_QUERIES_RESULT ADD (PRIMARY KEY (F_QUERY_STR))");
    }
    dbManager.ExecuteNonQuery("CREATE INDEX IMS_SEARCH_QUERIES_DATE ON IMS_SEARCH_QUERIES (F_QUERY_DATE)");
    dbManager.ExecuteNonQuery("CREATE INDEX IMS_SEARCH_QUERIES_USER ON IMS_SEARCH_QUERIES (F_USER_ID)");
    dbManager.ExecuteNonQuery("CREATE INDEX IMS_QUERIES_RESULT_NORM ON IMS_QUERIES_RESULT (F_QUERY_NORM)");
    dbManager.ExecuteNonQuery("CREATE INDEX IMS_QUERIES_RESULT_COUNT ON IMS_QUERIES_RESULT (F_QUERY_COUNTER, F_QUERY_DATE)");
  }

  internal class IMSColumn
  {
    public string Name;
    public Type Type;

    public IMSColumn(string name, Type type)
    {
      this.Name = name;
      this.Type = type;
    }

    public override bool Equals(object obj)
    {
      return obj is KernelUpdate.IMSColumn imsColumn && this.Name == imsColumn.Name && this.Type == imsColumn.Type;
    }

    public override int GetHashCode()
    {
      return this.Name.GetHashCode() << 16 /*0x10*/ ^ this.Type.GetHashCode();
    }
  }
}
