// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ImportRelation
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace Intermech.Kernel.Briefcase;

public class ImportRelation : Importer
{
  protected bool createLinksArray = true;
  protected ImportingRelation BriefRelation;
  protected bool IsValid = true;
  private readonly List<int> _deletedAttributes = new List<int>();

  private string GetUniIdentifiler()
  {
    string empty = string.Empty;
    int conformityRelationType = Helper.GetConformityRelationType((IUserSession) this.session, this.metadata.Tables["IMS_RELATION_TYPES"], this.BriefRelation.Relation.RelationType);
    string str1;
    if (conformityRelationType == -1)
    {
      DataRow dataRow = this.metadata.Tables["IMS_RELATION_TYPES"].Rows.Find((object) this.BriefRelation.Relation.RelationType);
      str1 = empty + (dataRow != null ? string.Format(LocalizationHolder.rm.GetString("Kernel_328"), dataRow["F_GUID"]) : string.Format(LocalizationHolder.rm.GetString("Kernel_329"), (object) this.BriefRelation.Relation.RelationType));
      this.IsValid = false;
    }
    else
    {
      this.BriefRelation.Relation.RelationType = conformityRelationType;
      IDBRelationType relationType = this.session.GetRelationType(conformityRelationType, false);
      str1 = empty + $"Связь типа \"{relationType.Description}\"";
    }
    string str2 = Convert.ToString(this.BriefRelation.Relation.ProjId);
    string str3;
    if (GuidHelper.IsGuid(str2))
    {
      long objectId = this.FindObjectID(new Guid(str2));
      if (objectId != 0L)
      {
        str3 = str1 + string.Format(LocalizationHolder.rm.GetString("Kernel_330"), (object) objectId);
        this.BriefRelation.Relation.ProjId = (object) objectId;
      }
      else
      {
        str3 = str1 + string.Format(LocalizationHolder.rm.GetString("Kernel_331"), this.BriefRelation.Relation.ProjId);
        this.IsValid = false;
      }
    }
    else
    {
      str3 = str1 + string.Format(LocalizationHolder.rm.GetString("Kernel_331"), this.BriefRelation.Relation.ProjId);
      this.IsValid = false;
    }
    string str4 = Convert.ToString(this.BriefRelation.Relation.PartId);
    string uniIdentifiler;
    if (GuidHelper.IsGuid(str4))
    {
      long id = this.FindID(new Guid(str4));
      if (id != 0L)
      {
        uniIdentifiler = str3 + string.Format(LocalizationHolder.rm.GetString("Kernel_332"), (object) id);
        this.BriefRelation.Relation.PartId = (object) id;
      }
      else
      {
        uniIdentifiler = str3 + string.Format(LocalizationHolder.rm.GetString("Kernel_333"), this.BriefRelation.Relation.PartId);
        this.IsValid = false;
      }
    }
    else
    {
      uniIdentifiler = str3 + string.Format(LocalizationHolder.rm.GetString("Kernel_333"), this.BriefRelation.Relation.PartId);
      this.IsValid = false;
    }
    return uniIdentifiler;
  }

  public ImportRelation(UserSession session, DataSet metadata, ImportingRelation briefRelation)
    : base(session, "IMS_RELATION_ATTRS", "F_PRJLINK_ID", false)
  {
    this.metadata = metadata;
    this.BriefRelation = briefRelation;
    this.uniIdentifiler = this.GetUniIdentifiler();
  }

  public ImportRelation(
    UserSession session,
    ImportingRelation briefRelation,
    bool createLinksArray)
    : this(session, briefRelation, createLinksArray, false, true)
  {
  }

  public ImportRelation(
    UserSession session,
    ImportingRelation briefRelation,
    bool createLinksArray,
    bool packetMode,
    bool createUniIdentifiler)
    : base(session, "IMS_RELATION_ATTRS", "F_PRJLINK_ID", packetMode)
  {
    this.createLinksArray = createLinksArray;
    this.BriefRelation = briefRelation;
    if (!createUniIdentifiler)
      return;
    this.uniIdentifiler = $"Тип связи ID ={briefRelation.Relation.RelationType} от версии объекта ID = {briefRelation.Relation.ProjId} к объекту ID = {briefRelation.Relation.PartId}";
  }

  public virtual long Import(bool langEquals, bool throwException)
  {
    try
    {
      if (!this.IsValid)
        throw new Exception($"{LocalizationHolder.rm.GetString("Kernel_310")}: {this.uniIdentifiler}");
      IDBRelation relation1 = this.FindRelation();
      if (relation1 != null)
        return relation1.RelationID;
      IDBRelation relation2 = this.BriefRelation.Relation.PrjLinkGuid == null ? this.session.GetRelation(Convert.ToInt64(this.BriefRelation.Relation.ProjId), Convert.ToInt64(this.BriefRelation.Relation.PartId), Convert.ToInt32(this.BriefRelation.Relation.RelationType)) : this.session.GetRelation((Guid) this.BriefRelation.Relation.PrjLinkGuid, Convert.ToInt64(this.BriefRelation.Relation.ProjId), false);
      if (relation2 != null)
      {
        if (!langEquals)
          return relation2.RelationID;
        this.session.StartTransaction();
        try
        {
          this.RefreshRelation(relation2);
          this.session.Commit();
          return relation2.RelationID;
        }
        catch
        {
          this.session.Rollback();
          throw;
        }
      }
      else
      {
        this.session.StartTransaction();
        long num;
        try
        {
          num = this.AddRelation();
          if (num == 0L)
          {
            this.session.Rollback();
            return num;
          }
          this.session.Commit();
        }
        catch
        {
          this.session.Rollback();
          throw;
        }
        this.AddIntoLog(LocalizationHolder.rm.GetString("Kernel_311"));
        return num;
      }
    }
    catch (Exception ex)
    {
      if (throwException)
        throw;
      this.ErrorException = ex;
      return 0;
    }
  }

  protected virtual IDBRelation FindRelation() => (IDBRelation) null;

  private void RefreshRelation(IDBRelation relation)
  {
    this.UpdateRelationProperties(false, relation.RelationID, relation.RelationType);
    this.RefreshAttributes(relation.RelationType, relation.RelationID, relation, false);
    this.UpdateViews(relation.RelationID);
  }

  protected void UpdateViews(long RelationID)
  {
    IDBRelation relation = this.session.GetRelation(RelationID, false);
    if (relation == null)
      throw new Exception(string.Format(BriefcaseConsts.ImportedRelationNotFound, (object) RelationID));
    ArrayList arrayList1 = new ArrayList();
    ArrayList arrayList2 = new ArrayList();
    for (int AttrIndex = 0; AttrIndex < relation.Attributes.Count; ++AttrIndex)
    {
      IDBAttribute attribute = relation.Attributes[AttrIndex];
      if (!this.updatedAttributes.Contains((object) attribute.AttributeID) || this.temporaryAttributes.Contains((object) attribute.AttributeID))
      {
        if (!this.calculatedAttributes.Contains(attribute.AttributeID))
        {
          (attribute as DBAttribute).Compute(true);
          this.calculatedAttributes.Add(attribute.AttributeID);
        }
      }
      else
      {
        AttributeRecord updatedAttribute = (AttributeRecord) this.updatedAttributes[(object) attribute.AttributeID];
        MetaDataHelper.GetAttributeType(attribute.AttributeID);
        string[] updateTables = this.session.DBCache.GetUpdateTables(attribute.AttributeID, -1, relation.RelationType);
        if (updateTables != null && updateTables.Length != 0)
        {
          arrayList1.Add((object) this.AddViewFieldsToSQL(updateTables, updatedAttribute));
          foreach (string str in updateTables)
          {
            if (arrayList2.BinarySearch((object) str) < 0)
              arrayList2.Add((object) str);
          }
        }
      }
    }
    foreach (string str in arrayList2)
    {
      StringBuilder stringBuilder = new StringBuilder();
      ArrayList arrayList3 = new ArrayList();
      foreach (Importer.UpdatingAttribute updatingAttribute in arrayList1)
      {
        if (updatingAttribute.Tables.Contains(str))
        {
          foreach (Tuple<string, DbType, object> fieldsAndValue in updatingAttribute.FieldsAndValues)
          {
            stringBuilder.Append(fieldsAndValue.Item1);
            stringBuilder.Append(" = :");
            stringBuilder.Append(fieldsAndValue.Item1);
            stringBuilder.Append(',');
            if (this.packetMode)
              arrayList3.Add((object) this.session.DataManager.BatchParameter(fieldsAndValue.Item1, fieldsAndValue.Item2, fieldsAndValue.Item3));
            else
              arrayList3.Add((object) this.session.DataManager.Parameter(fieldsAndValue.Item1, fieldsAndValue.Item3));
          }
        }
      }
      if (stringBuilder.Length > 1 && arrayList3.Count > 0)
      {
        arrayList3.Add((object) this.session.DataManager.Parameter("relID", (object) RelationID));
        stringBuilder.Remove(stringBuilder.Length - 1, 1);
        this.session.DataManager.ExecuteNonQuery($"UPDATE {str} SET {stringBuilder.ToString()} WHERE F_PRJLINK_ID = :relID", (IDbDataParameter[]) arrayList3.ToArray(typeof (IDbDataParameter)));
      }
    }
  }

  protected void RefreshAttributes(
    int relationType,
    long relationID,
    IDBRelation relation,
    bool isNewRelation)
  {
    ISpecHandleAttributes service1 = ServerServices.GetService(typeof (ISpecHandleAttributes)) as ISpecHandleAttributes;
    ISitesCacheService customService = (ISitesCacheService) this.session.GetCustomService(typeof (ISitesCacheService));
    IIDLinkTranslate service2 = ServerServices.GetService(typeof (IIDLinkTranslate)) as IIDLinkTranslate;
    for (int index = 0; index < this.BriefRelation.Attributes.Count; ++index)
    {
      AttributeRecord attribute = this.BriefRelation.Attributes[index];
      DBAttributeType baseAttr = new DBAttributeType(this.session, Helper.GetAttributeTypeRow((IUserSession) this.session, attribute.AttributeId) ?? throw new Exception($"Атрибут с идентификатором {attribute.AttributeId} не найден в базе назначения"));
      if (isNewRelation || !service1.IsNotUpdatingAttribute(baseAttr.GUID) || relation.Attributes.FindByGUID(baseAttr.GUID) == null)
      {
        if (!isNewRelation && !this._deletedAttributes.Contains(baseAttr.AttributeID))
        {
          this.DeleteAttributeValues(attribute, baseAttr.AttributeType, baseAttr.AttributeID, relationID);
          this._deletedAttributes.Add(baseAttr.AttributeID);
        }
        SpecHandleAttributeEventArgs e = new SpecHandleAttributeEventArgs((IUserSession) this.session, relationID, (IDBAttributable) null, -1, baseAttr.AttributeID, baseAttr.GUID, attribute, isNewRelation, false);
        if (this.withAttributesCustomHandlers)
          service1.FireEventForObjectAttribute(e);
        if (!this.withAttributesCustomHandlers || !e.Handled)
        {
          if (baseAttr.AttributeType == FieldTypes.ftBlob || baseAttr.AttributeType == FieldTypes.ftFile || baseAttr.AttributeType == FieldTypes.ftMemo || baseAttr.AttributeType == FieldTypes.ftShortBlob)
          {
            attribute.IntegerValue = (object) 0L;
            bool flag = false;
            string str = (string) null;
            if (attribute.FileAuthor != null)
            {
              str = Convert.ToString(attribute.FileAuthor);
              if (GuidHelper.IsGuid(str))
              {
                QuickObjectInfo objectInfo = this.session.GetObjectInfo(new Guid(str));
                attribute.FileAuthor = (object) (objectInfo.Empty ? 0L : objectInfo.ObjectID);
                if (objectInfo.Empty)
                  flag = true;
              }
            }
            if (baseAttr.AttributeID == this.session.IdentHelper.FileAttributeID)
              this.CheckFileName(attribute, relationID, !isNewRelation, true);
            ImportBlob importBlob = new ImportBlob(this.session, this.hintAppendEnable);
            attribute.IntegerValue = (object) importBlob.Import(relationID, attribute, baseAttr.AttributeType, isNewRelation);
            foreach (string warning in (IEnumerable<string>) importBlob.GetWarnings())
              this.AddWarningMessage(warning);
            if (flag)
              this.AddWarningMessage($"Для файлового атрибута {baseAttr.Name} FileID={attribute.IntegerValue} не удалость установить автора по значению FileAuthor={{{str}}}");
            if (baseAttr.AttributeID == this.session.IdentHelper.FileAttributeID)
              this.AddFileNameIntoTable(attribute, relationID, relationID, !isNewRelation);
          }
          if (this.createLinksArray && (baseAttr.AttributeType == FieldTypes.ftObjectLink || baseAttr.AttributeType == FieldTypes.ftObjectLinkByID))
          {
            this.ObjectLinks.Add((object) new RelationLinks(relationID, baseAttr.AttributeID, attribute.InlistId, Convert.ToInt64(attribute.IntegerValue), Convert.ToString(attribute.StringValue), this.BriefRelation.Relation.RelationType, baseAttr.AttributeType == FieldTypes.ftObjectLinkByID));
            attribute.IntegerValue = (object) null;
            attribute.StringValue = (object) null;
          }
          if (baseAttr.AttributeType == FieldTypes.ftInteger && this.createLinksArray && service2 != null && service2.IsIDLink(baseAttr.GUID))
          {
            this.ObjectLinks.Add((object) new RelationLinks(relationID, baseAttr.AttributeID, attribute.InlistId, Convert.ToInt64(attribute.IntegerValue), string.Empty, this.BriefRelation.Relation.RelationType, false));
            attribute.IntegerValue = (object) null;
          }
        }
        this.InsertAttribute((IDBAttributeType) baseAttr, attribute, relationID);
        if (attribute.InlistId == 0)
          this.updatedAttributes.Add((object) baseAttr.AttributeID, (object) attribute);
      }
    }
    if (isNewRelation)
      return;
    IDBRelationType relationType1 = this.session.GetRelationType(relationType);
    for (int AttrIndex = 0; AttrIndex < relation.Attributes.Count; ++AttrIndex)
    {
      IDBAttribute attribute = relation.Attributes[AttrIndex];
      IDBAttributeType4 attributeById = relationType1.Attributes.GetAttributeByID(attribute.AttributeID);
      if ((!this.updatedAttributes.ContainsKey((object) attribute.AttributeID) || this.temporaryAttributes.Contains((object) attribute.AttributeID)) && attributeById != null && (attributeById.Computed == ComputeValueModes.StoredValue || attributeById.Computed == ComputeValueModes.IndexValue) && !this.calculatedAttributes.Contains(attribute.AttributeID))
      {
        (attribute as DBAttribute).Compute(false);
        this.calculatedAttributes.Add(attribute.AttributeID);
      }
    }
  }

  protected long ExecAddImportedRelation(Guid guid)
  {
    long num = 0;
    this.session.DataManager.ExecuteSpNonQuery("IMS_ADD_RELATION", this.session.DataManager.Parameter("inPRJLINK_ID", (object) 0L), this.session.DataManager.Parameter("inPROJ_ID", this.BriefRelation.Relation.ProjId), this.session.DataManager.Parameter("inPART_ID", this.BriefRelation.Relation.PartId), this.session.DataManager.Parameter("inRELATION_TYPE", (object) this.BriefRelation.Relation.RelationType), this.session.DataManager.Parameter("inCREATE_DATE", this.BriefRelation.Relation.CreateDate), this.session.DataManager.Parameter("inPRJ_GUID", (object) guid.ToString()), this.session.DataManager.Parameter("inREL_CREATOR", (object) this.BriefRelation.Relation.CreatorID), this.session.DataManager.OutputParameter("outPRJLINK_ID", (object) num));
    return Convert.ToInt64(this.session.DataManager.GetOutputParameterValue("outPRJLINK_ID"));
  }

  protected void UpdateRelationProperties(bool IsCreationMode, long relationID, int relationType)
  {
    string commandText = "UPDATE IMS_RELATIONS SET F_CREATE_DATE = :createDate WHERE F_PRJLINK_ID = :relID";
    string format1 = "UPDATE {0} SET F_CREATE_DATE = :createDate WHERE F_PRJLINK_ID = :relID";
    string format2 = "INSERT INTO {0} (F_PRJLINK_ID, F_PROJ_ID, F_PART_ID, F_RELATION_TYPE, F_CREATE_DATE, F_PRJ_GUID, F_REL_CREATOR) SELECT F_PRJLINK_ID, F_PROJ_ID, F_PART_ID, F_RELATION_TYPE, F_CREATE_DATE, F_PRJ_GUID, F_REL_CREATOR FROM IMS_RELATIONS WHERE F_PRJLINK_ID = :relID";
    if (this.packetMode)
    {
      DbCommandParam dbCommandParam1 = this.session.DataManager.BatchParameter("createDate", DbType.DateTime, this.BriefRelation.Relation.CreateDate);
      DbCommandParam dbCommandParam2 = this.session.DataManager.BatchParameter("relID", DbType.Int64, (object) relationID);
      this.session.DataManager.AddBatchSQL(commandText, new DbCommandParam[2]
      {
        dbCommandParam1,
        dbCommandParam2
      });
      string[] updateTables = this.session.DBCache.GetUpdateTables(-1, -1, relationType);
      if (updateTables == null)
        return;
      foreach (string str in updateTables)
      {
        if (!IsCreationMode)
          this.session.DataManager.AddBatchSQL(string.Format(format1, (object) str), new DbCommandParam[2]
          {
            dbCommandParam1,
            dbCommandParam2
          });
        else
          DBHelper.AddBatchSQL((IUserSession) this.session, (this.hintAppendEnable ? 1 : 0) != 0, string.Format(format2, (object) str), new DbCommandParam[1]
          {
            dbCommandParam2
          });
      }
    }
    else
    {
      IDbDataParameter dbDataParameter1 = this.session.DataManager.Parameter("createDate", this.BriefRelation.Relation.CreateDate);
      IDbDataParameter dbDataParameter2 = this.session.DataManager.Parameter("relID", (object) relationID);
      this.session.DataManager.ExecuteNonQuery(commandText, dbDataParameter1, dbDataParameter2);
      string[] updateTables = this.session.DBCache.GetUpdateTables(-1, -1, relationType);
      if (updateTables == null)
        return;
      foreach (string str in updateTables)
      {
        if (!IsCreationMode)
          this.session.DataManager.ExecuteNonQuery(string.Format(format1, (object) str), dbDataParameter1, dbDataParameter2);
        else
          DBHelper.ExecuteNonQuery((IUserSession) this.session, (this.hintAppendEnable ? 1 : 0) != 0, string.Format(format2, (object) str), dbDataParameter2);
      }
    }
  }

  protected long AddRelation()
  {
    try
    {
      Guid guid = this.BriefRelation.Relation.PrjLinkGuid != null ? (Guid) this.BriefRelation.Relation.PrjLinkGuid : Guid.NewGuid();
      long oldCreatorID;
      if (this.BriefRelation.Relation.CreatorID >= 0L)
      {
        oldCreatorID = this.BriefRelation.Relation.CreatorID;
        this.BriefRelation.Relation.CreatorID = this.session.UserID;
      }
      else
      {
        this.BriefRelation.Relation.CreatorID = -1L * this.BriefRelation.Relation.CreatorID;
        oldCreatorID = -1L;
      }
      long num = this.ExecAddImportedRelation(guid);
      if (this.createLinksArray && oldCreatorID != -1L)
        this.ObjectLinks.Add((object) new RelationPropertiesLinks(num, oldCreatorID, this.BriefRelation.Relation.RelationType));
      this.UpdateRelationProperties(true, num, this.BriefRelation.Relation.RelationType);
      Importer.AppendObligatoryAttributes(Helper.GetAttributesForRelationType((IUserSession) this.session, this.BriefRelation.Relation.RelationType), (ImportingAttributable) this.BriefRelation, num, this.temporaryAttributes);
      this.RefreshAttributes(this.BriefRelation.Relation.RelationType, num, (IDBRelation) null, true);
      this.UpdateViews(num);
      this.AddIntoLog(BriefcaseConsts.logOKImported);
      return num;
    }
    catch (Exception ex)
    {
      this.AddErrorMessage(ex);
      throw;
    }
  }

  protected override IDBAttribute4TypeCollection GetAttributesCollection(int typeID)
  {
    return this.session.GetRelationType(typeID).Attributes;
  }
}
