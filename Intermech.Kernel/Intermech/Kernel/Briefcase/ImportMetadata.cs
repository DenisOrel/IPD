// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ImportMetadata
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Expressions;
using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel.Briefcase;

internal sealed class ImportMetadata(
  UserSession session,
  ImportEventLog eventLog,
  SetImportProgressEventHandler setImportProgressEvent) : ImportBriefcaseBase(session, eventLog, setImportProgressEvent)
{
  public bool Import(
    DataSet metadata,
    DataSet metadataImportList,
    Guid briefcase,
    ImportStore store,
    IgnoringErrors ignoringErrors,
    bool langsEquals,
    bool createOnly)
  {
    BriefcaseImportProgress importProgress = new BriefcaseImportProgress(OperationType.ImportingMetaData);
    this.SetImportProgress(briefcase, importProgress);
    IDBLanguageType defaultLanguage = this.session.DefaultLanguage;
    MetaDataHelper.Locked = true;
    this.ReloadServerCache();
    this.session.StartTransaction();
    try
    {
      int[] numArray = new int[8]
      {
        9,
        11,
        8,
        12,
        3,
        6,
        16 /*0x10*/,
        4
      };
      ImportAttributesStore importAttributesStore = new ImportAttributesStore();
      ImportObjectTypesStore objTypeStore = new ImportObjectTypesStore();
      Hashtable ImportedObjectTypes = new Hashtable();
      List<SaveImportValues> attributesOptimizationMode = new List<SaveImportValues>();
      double num1 = (double) metadataImportList.Tables[BriefcaseConsts.XmlMetadataTableName].Select().Length / 100.0;
      double num2 = 0.0;
      ImportItemOptions options = ImportItemOptions.None;
      if (langsEquals)
        options |= ImportItemOptions.LangEquals;
      if (createOnly)
        options |= ImportItemOptions.CreateOnly;
      string columnName = "LEVEL";
      foreach (int num3 in numArray)
      {
        DataRow[] dataRowArray1;
        if (num3 == 4)
        {
          Hashtable hashtable = new Hashtable();
          dataRowArray1 = metadataImportList.Tables[BriefcaseConsts.XmlMetadataTableName].Select($"{BriefcaseConsts.XmlCategoryTag}={(object) 4}");
          foreach (DataRow dataRow in dataRowArray1)
            hashtable.Add(dataRow[BriefcaseConsts.XmlIdTag], (object) Helper.GetObjectTypeLevel(metadata.Tables["IMS_OBJTYPES_TREE"], 0, Convert.ToInt32(dataRow[BriefcaseConsts.XmlIdTag])));
          DataTable dataTable = metadata.Tables["IMS_OBJECT_TYPES"].Clone();
          dataTable.Columns.Add(new DataColumn(columnName));
          IDictionaryEnumerator enumerator = hashtable.GetEnumerator();
          while (enumerator.MoveNext())
          {
            DataRow dataRow = metadata.Tables["IMS_OBJECT_TYPES"].Rows.Find(enumerator.Key);
            DataRow row = dataTable.NewRow();
            for (int index = 0; index < dataRow.Table.Columns.Count; ++index)
              row[dataRow.Table.Columns[index].ColumnName] = dataRow[dataRow.Table.Columns[index].ColumnName];
            row[columnName] = (object) Convert.ToInt32(enumerator.Value);
            dataTable.Rows.Add(row);
          }
          dataTable.AcceptChanges();
          DataRow[] dataRowArray2 = dataTable.Select(string.Empty, columnName + " ASC");
          int num4 = 0;
          List<int> newObjectTypes = new List<int>();
          DBObjectTypeCollection objectTypeCollection = this.session.GetObjectTypeCollection(-2) as DBObjectTypeCollection;
          int num5 = 0;
          foreach (DataRow briefRow in dataRowArray2)
          {
            int int32 = Convert.ToInt32(briefRow[columnName]);
            if (int32 != num5 && newObjectTypes.Count > 0)
            {
              objectTypeCollection.CommitTypesCreation(newObjectTypes.ToArray());
              newObjectTypes = new List<int>();
              num5 = int32;
            }
            ++num4;
            this.eventLog.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_335"), (object) num4.ToString(), (object) dataRowArray2.Length));
            bool flag = true;
            if (!langsEquals)
            {
              DataRow[] dataRowArray3 = metadata.Tables["IMS_LOCALIZATION"].Select($"{"F_GUID"} = {DataSetProcessor.QString(Convert.ToString(briefRow["F_GUID"]))}");
              if (dataRowArray3 != null && dataRowArray3.Length != 0)
                flag = Convert.ToString(dataRowArray3[0]["F_LANGUAGES"]).IndexOf(defaultLanguage.LanguageID) >= 0;
            }
            if (!flag)
            {
              this.eventLog.AddToTrace(string.Format(BriefcaseConsts.logObjectNotImportedLocalization, (object) $"Category = {num3}, object = {briefRow["F_GUID"]}"));
            }
            else
            {
              ImportObjectType importObjectType = new ImportObjectType(this.session, briefRow, metadata, ImportedObjectTypes, store.DefaultValueObjectLink, store.MeasureValueObjectLink, attributesOptimizationMode, ignoringErrors, newObjectTypes, objectTypeCollection, objTypeStore, options);
              if (!importObjectType.Import())
              {
                this.session.Rollback();
                if (importObjectType.Log.Count > 0)
                {
                  foreach (string eventString in importObjectType.Log)
                    this.eventLog.AddToTrace(eventString);
                }
                if (importObjectType.ErrorException != null)
                {
                  importProgress.ErrorException = importObjectType.ErrorException;
                  this.eventLog.AddToTrace($"{importObjectType.ErrorException.Message}: {importObjectType.ErrorException.InnerException.Message}");
                }
                this.eventLog.AddToTrace(string.Format(BriefcaseConsts.logObjectNotImported, (object) importObjectType.UniIdentifiler));
                importProgress.Operation = OperationType.Error;
                this.SetImportProgress(briefcase, importProgress);
                return false;
              }
              if (importObjectType.Log.Count > 0)
              {
                foreach (string eventString in importObjectType.Log)
                  this.eventLog.AddToTrace(eventString);
              }
              this.eventLog.AddToTrace(string.Format(BriefcaseConsts.logObjectImported, (object) importObjectType.UniIdentifiler));
              ++num2;
              importProgress.Percent = (int) Math.Ceiling(num2 / num1);
              this.SetImportProgress(briefcase, importProgress);
            }
          }
          if (newObjectTypes.Count > 0)
            objectTypeCollection.CommitTypesCreation(newObjectTypes.ToArray());
        }
        else
          dataRowArray1 = metadataImportList.Tables[BriefcaseConsts.XmlMetadataTableName].Select($"{BriefcaseConsts.XmlCategoryTag}={num3.ToString()}");
        foreach (DataRow dataRow in dataRowArray1)
        {
          try
          {
            bool flag = true;
            if (!langsEquals)
            {
              DataRow[] dataRowArray4 = metadata.Tables["IMS_LOCALIZATION"].Select($"{"F_GUID"} = {DataSetProcessor.QString(Convert.ToString(dataRow[BriefcaseConsts.XmlExternalTag]))}");
              if (dataRowArray4 != null && dataRowArray4.Length != 0)
                flag = Convert.ToString(dataRowArray4[0]["F_LANGUAGES"]).IndexOf(defaultLanguage.LanguageID) >= 0;
            }
            if (!flag)
            {
              this.eventLog.AddToTrace(string.Format(BriefcaseConsts.logObjectNotImportedLocalization, (object) $"Category = {num3}, object = {dataRow[BriefcaseConsts.XmlExternalTag]}"));
            }
            else
            {
              ImportItem importItem = (ImportItem) null;
              switch (num3)
              {
                case 3:
                  int int32 = Convert.ToInt32(dataRow[BriefcaseConsts.XmlIdTag]);
                  if (int32 >= 0)
                  {
                    importItem = (ImportItem) new ImportAttributeType(this.session, metadata.Tables["IMS_ATTRIBUTES"].Rows.Find((object) int32), metadata, importAttributesStore, store, attributesOptimizationMode, options);
                    break;
                  }
                  continue;
                case 6:
                  importItem = (ImportItem) new ImportRelationType(this.session, metadata.Tables["IMS_RELATION_TYPES"].Rows.Find(dataRow[BriefcaseConsts.XmlIdTag]), metadata, store.DefaultValueObjectLink, store.MeasureValueObjectLink, attributesOptimizationMode, options);
                  break;
                case 8:
                  importItem = (ImportItem) new ImportLCLevel(this.session, metadata.Tables["IMS_LEVELS"].Rows.Find(dataRow[BriefcaseConsts.XmlIdTag]), metadata, options);
                  break;
                case 9:
                  importItem = (ImportItem) new ImportLanguage(this.session, metadata.Tables["IMS_LANGUAGES"].Rows.Find(dataRow[BriefcaseConsts.XmlIdTag]), metadata, options);
                  break;
                case 11:
                  importItem = (ImportItem) new ImportSubjectArea(this.session, metadata.Tables["IMS_SUBJECT_AREAS"].Rows.Find(dataRow[BriefcaseConsts.XmlIdTag]), metadata, options);
                  break;
                case 12:
                  importItem = (ImportItem) new ImportAttributeGroup(this.session, metadata.Tables["IMS_ATTR_GROUPS"].Rows.Find(dataRow[BriefcaseConsts.XmlIdTag]), metadata, options);
                  break;
                case 16 /*0x10*/:
                  importItem = (ImportItem) new ImportLCSheme(this.session, metadata.Tables["IMS_LC_SCHEMAS"].Rows.Find(dataRow[BriefcaseConsts.XmlIdTag]), metadata, options);
                  break;
              }
              if (importItem != null)
              {
                if (!importItem.Import())
                {
                  this.session.Rollback();
                  if (importItem.Log.Count > 0)
                  {
                    foreach (string eventString in importItem.Log)
                      this.eventLog.AddToTrace(eventString);
                  }
                  if (importItem.ErrorException != null)
                  {
                    importProgress.ErrorException = importItem.ErrorException;
                    this.eventLog.AddToTrace($"{importItem.ErrorException.Message}: {importItem.ErrorException.InnerException.Message}");
                  }
                  this.eventLog.AddToTrace(string.Format(BriefcaseConsts.logObjectNotImported, (object) importItem.UniIdentifiler));
                  importProgress.Operation = OperationType.Error;
                  this.SetImportProgress(briefcase, importProgress);
                  return false;
                }
                if (importItem.Log.Count > 0)
                {
                  foreach (string eventString in importItem.Log)
                    this.eventLog.AddToTrace(eventString);
                }
                this.eventLog.AddToTrace(string.Format(BriefcaseConsts.logObjectImported, (object) importItem.UniIdentifiler));
              }
            }
          }
          finally
          {
            ++num2;
            importProgress.Percent = (int) Math.Ceiling(num2 / num1);
            this.SetImportProgress(briefcase, importProgress);
          }
        }
        if (num3 == 3)
          (this.session.GetAttributeTypeCollection(-1) as DBAttributeTypeCollection).CommitFastCreation();
      }
      if (importAttributesStore.AttributeFormules.Count > 0)
      {
        IDictionaryEnumerator enumerator = importAttributesStore.AttributeFormules.GetEnumerator();
        while (enumerator.MoveNext())
        {
          IDBAttributeType attributeType = this.session.GetAttributeType(Convert.ToInt32(enumerator.Key));
          try
          {
            string str = enumerator.Value.ToString();
            ArrayList consistAttrs = new ArrayList();
            if (!langsEquals)
            {
              using (Parser parser = new Parser())
              {
                parser.AutoDetectVariables = true;
                parser.Validate = false;
                ExpressionTree expressionTree = parser.Parse(str);
                if (expressionTree != null)
                {
                  ExpressionVariablesCollection variables = expressionTree.Variables;
                  for (int index = 0; index < variables.Count; ++index)
                  {
                    if (!(variables[index].Name.ToUpper() == "VALUE"))
                    {
                      DataRow[] dataRowArray = metadata.Tables["IMS_ATTRIBUTES"].Select("F_NAME = " + SqlHelper.QString(variables[index].Name));
                      IDBAttributeType dbAttributeType = dataRowArray.Length != 0 ? this.session.GetAttributeType(new Guid(Convert.ToString(dataRowArray[0]["F_GUID"]))) : throw new KernelExceptionID(sc_12948.ssp_appserver_12949(2045420232), (object) variables[index].Name.ToString());
                      str = str.Replace($"[{variables[index].Name.ToString()}]", $"[{dbAttributeType.Name}]");
                    }
                  }
                }
              }
            }
            if (str != string.Empty)
              (attributeType as DBAttributeType).ValidateFormula(str, (ArrayList) null, consistAttrs, Consts.Attribute4Formula);
            if (attributeType != null)
              attributeType.Formula = str;
          }
          catch (Exception ex)
          {
            if ((ignoringErrors & IgnoringErrors.IgnoreFormulaErrors) == IgnoringErrors.IgnoreFormulaErrors)
              this.eventLog.AddToTrace(string.Format(BriefcaseConsts.ImportAttributeFormulaError, (object) attributeType.Name, (object) ex.Message));
            else
              throw;
          }
        }
      }
      if (objTypeStore.AttributeFormules.Count > 0)
      {
        for (int index1 = 0; index1 < objTypeStore.AttributeFormules.Count; ++index1)
        {
          TypeFormules attributeFormule = objTypeStore.AttributeFormules[index1];
          IDBObjectType objectType = this.session.GetObjectType(attributeFormule.TypeGuid);
          ArrayList enabledAttrs = new ArrayList();
          IDBAttribute4ObjectTypeCollection attributes = objectType.Attributes as IDBAttribute4ObjectTypeCollection;
          foreach (DataRow row in (InternalDataCollectionBase) attributes.Select(string.Empty).Rows)
            enabledAttrs.Add((object) Convert.ToInt32(row["F_ATTRIBUTE_ID"]));
          foreach (KeyValuePair<int, string> formule in attributeFormule.Formules)
          {
            IDBAttributeType4Object attributeById = attributes.GetAttributeByID(formule.Key) as IDBAttributeType4Object;
            if (attributeById.InheritMode != InheritModes.Inherited)
            {
              try
              {
                ArrayList consistAttrs = new ArrayList();
                string str = formule.Value;
                if (!langsEquals)
                {
                  using (Parser parser = new Parser())
                  {
                    parser.AutoDetectVariables = true;
                    parser.Validate = false;
                    ExpressionTree expressionTree = parser.Parse(str);
                    if (expressionTree != null)
                    {
                      ExpressionVariablesCollection variables = expressionTree.Variables;
                      for (int index2 = 0; index2 < variables.Count; ++index2)
                      {
                        if (!(variables[index2].Name.ToUpper() == "VALUE"))
                        {
                          DataRow[] dataRowArray = metadata.Tables["IMS_ATTRIBUTES"].Select("F_NAME = " + SqlHelper.QString(variables[index2].Name));
                          IDBAttributeType dbAttributeType = dataRowArray.Length != 0 ? this.session.GetAttributeType(new Guid(Convert.ToString(dataRowArray[0]["F_GUID"]))) : throw new KernelExceptionID(sc_12948.ssp_appserver_12950(1138181619), (object) variables[index2].Name.ToString());
                          str = str.Replace($"[{variables[index2].Name.ToString()}]", $"[{dbAttributeType.Name}]");
                        }
                      }
                    }
                  }
                }
                if (str != string.Empty)
                  new DBAttributeType(this.session, this.session.DBCache.GetTable("IMS_ATTRIBUTES").Rows.Find((object) attributeById.AttributeID)).ValidateFormula(str, enabledAttrs, consistAttrs, Consts.Attribute4Formula);
                attributeById.Formula = str;
              }
              catch (Exception ex)
              {
                if ((ignoringErrors & IgnoringErrors.IgnoreFormulaErrors) == IgnoringErrors.IgnoreFormulaErrors)
                  this.eventLog.AddToTrace(string.Format(BriefcaseConsts.ImportAttribute4ObjTypeFormulaError, (object) this.session.GetAttributeType(attributeById.AttributeID).Name, (object) objectType.ObjectTypeName, (object) ex.Message));
                else
                  throw;
              }
            }
          }
        }
      }
      foreach (Tuple<int, int> captionAttribute in objTypeStore.CaptionAttributes)
        this.session.GetObjectType(captionAttribute.Item1).CaptionAttribute = captionAttribute.Item2;
      ImportObjectToObject importObjectToObject = new ImportObjectToObject();
      if (importObjectToObject.Import(metadata, this.session, importAttributesStore, ImportedObjectTypes))
      {
        this.SetAttributesOptimizationMode((IUserSession) this.session, attributesOptimizationMode);
        this.session.Commit();
        this.ReloadServerCache();
        importProgress.Operation = OperationType.TerminateCurrent;
        importProgress.Percent = 100;
        this.SetImportProgress(briefcase, importProgress);
        return true;
      }
      this.session.Rollback();
      if (importObjectToObject.ErrorException != null)
      {
        importProgress.ErrorException = importObjectToObject.ErrorException;
        this.eventLog.AddToTrace($"{importObjectToObject.ErrorException.Message}: {importObjectToObject.ErrorException.InnerException.Message}");
      }
      this.eventLog.AddToTrace($"{importObjectToObject.ErrorException.Message}: {importObjectToObject.ErrorException.InnerException.Message}");
      importProgress.Operation = OperationType.Error;
      this.SetImportProgress(briefcase, importProgress);
      return false;
    }
    catch (Exception ex)
    {
      this.session.Rollback();
      importProgress.ErrorException = new Exception(LocalizationHolder.rm.GetString("Kernel_337") + ex.Message, ex);
      importProgress.Operation = OperationType.Error;
      this.SetImportProgress(briefcase, importProgress);
      this.eventLog.AddToTrace(LocalizationHolder.rm.GetString("Kernel_337") + ex.Message);
      return false;
    }
    finally
    {
      MetaDataHelper.Locked = false;
      MetaDataHelper.SyncMetadata(this.session.CacheDataSet, true);
    }
  }

  private void ReloadServerCache()
  {
    (this.session.DBCache as CacheDataset).CacheLoaded = false;
    this.session.DBCache.LoadTables(this.session.DataManager);
  }

  private void SetAttributesOptimizationMode(
    IUserSession session,
    List<SaveImportValues> attributesOptimizationMode)
  {
    BriefcaseImportProgress briefcaseImportProgress = new BriefcaseImportProgress(OperationType.Importing);
    foreach (SaveImportValues saveImportValues in attributesOptimizationMode)
    {
      if (saveImportValues.ObjectTypeID == -1)
      {
        if (saveImportValues.RelationTypeID == -1)
        {
          try
          {
            session.GetAttributeType(Convert.ToInt32(saveImportValues.AttributeTypeID), true).OptimizationMode = (OptimizationModes) saveImportValues.Value;
            continue;
          }
          catch (Exception ex)
          {
            this.eventLog.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_349"), (object) saveImportValues.AttributeTypeID, (object) ex.Message));
            continue;
          }
        }
      }
      if (saveImportValues.RelationTypeID == -1)
      {
        if (saveImportValues.ObjectTypeID != -1)
        {
          try
          {
            IDBAttributeType4 attributeById = session.GetObjectType(saveImportValues.ObjectTypeID, true).Attributes.GetAttributeByID(saveImportValues.AttributeTypeID);
            if (attributeById != null)
              attributeById.OptimizationMode = (OptimizationModes) saveImportValues.Value;
          }
          catch (Exception ex)
          {
            this.eventLog.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_350"), (object) saveImportValues.AttributeTypeID, (object) saveImportValues.ObjectTypeID, (object) ex.Message));
          }
        }
      }
      if (saveImportValues.RelationTypeID >= 0)
      {
        if (saveImportValues.ObjectTypeID == -1)
        {
          try
          {
            IDBAttributeType4 attributeById = session.GetRelationType(saveImportValues.RelationTypeID, true).Attributes.GetAttributeByID(saveImportValues.AttributeTypeID);
            if (attributeById != null)
              attributeById.OptimizationMode = (OptimizationModes) saveImportValues.Value;
          }
          catch (Exception ex)
          {
            this.eventLog.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_351"), (object) saveImportValues.AttributeTypeID, (object) saveImportValues.RelationTypeID, (object) ex.Message));
          }
        }
      }
    }
  }
}
