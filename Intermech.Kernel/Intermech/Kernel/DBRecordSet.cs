// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBRecordSet
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Projects;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Helpers;
using Intermech.Kernel.Search;
using Intermech.Kernel.Services;
using Intermech.Localization;
using Intermech.Pools;
using Intermech.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;


namespace Intermech.Kernel;

public class DBRecordSet : DBSessionable
{
  private bool _LocalTypesMode;
  protected bool _CheckAccess;
  protected bool _ShowNotOwnedWorkCopies;
  protected int _RecordsTypeID = -1;
  protected string _DBObjectTableName;
  protected string _DBAttributesTableName;
  protected string _DBKeyField;
  protected int _DBKeyFieldID;
  protected bool _TrashMode;
  internal DateTime _ActualDate;
  protected static Dictionary<int, int> steps2levels = new Dictionary<int, int>();
  protected static DateTime _cacheModifyDate = DateTime.MinValue;
  protected string _CaptionAttributeName;
  protected string _SortOrder;
  protected string _objectName;
  internal bool _ShowPersonalObjects;
  internal bool _ShowAllModifications;
  internal bool _MustTurnAllModificationsModeOFF;
  internal long _CurrentModificationID;
  private OptimizationInfo optimStat;
  private bool _GlobalSelectMode;
  internal bool _RevertAccessFiltration;
  private bool _ExistsMode;
  protected bool _EntersInUseAttrCompositionVersionID;
  protected bool _UseAttrCompositionVersionID;
  internal static int _attrVisibility = 0;
  protected bool _visibilityFiltration;
  protected List<int> _AddedColumns = new List<int>(0);
  private static IDBEditingContextsServerService _editingContextsServerService;

  public virtual DataTable GetAllValues(int attributeID) => throw new NotImplementedException();

  protected virtual AttributeSourceTypes AutoAttributeSourceTypes
  {
    [DebuggerStepThrough] get => AttributeSourceTypes.Auto;
  }

  public bool ShowNotOwnedWorkCopies => this._ShowNotOwnedWorkCopies;

  public bool ShowAllModifications
  {
    [DebuggerStepThrough] get => this._ShowAllModifications;
    set => this._ShowAllModifications = value;
  }

  public virtual bool LocalTypesMode
  {
    get => this._LocalTypesMode;
    set
    {
      if (value == this._LocalTypesMode)
        return;
      this._LocalTypesMode = value;
    }
  }

  protected bool UseVersionInComposition
  {
    [DebuggerStepThrough] get
    {
      return this._EntersInUseAttrCompositionVersionID | this._UseAttrCompositionVersionID;
    }
  }

  internal virtual IDbManager DBManager => this.UserSession.DataManager;

  internal static IDBEditingContextsServerService EditingContextsServerService
  {
    get
    {
      if (DBRecordSet._editingContextsServerService == null)
        DBRecordSet._editingContextsServerService = ServerServices.GetService(typeof (IDBEditingContextsServerService)) as IDBEditingContextsServerService;
      return DBRecordSet._editingContextsServerService;
    }
  }

  public DBRecordSet(UserSession uSession, int recordsTypeID)
    : base(uSession)
  {
    this._RecordsTypeID = recordsTypeID;
    if (uSession.IsSystemSession)
    {
      this._ShowNotOwnedWorkCopies = !(this is DBRelationCollection);
      this._CheckAccess = false;
    }
    else
      this._CheckAccess = true;
    if (!AdminUtilsService.OptimizerStatisticsON)
      return;
    this.optimStat = new OptimizationInfo(uSession.DataManager);
  }

  public bool GlobalSelectMode
  {
    [DebuggerStepThrough] get => this._GlobalSelectMode;
    set => this._GlobalSelectMode = value;
  }

  public override string ObjectName
  {
    [DebuggerStepThrough] get => this.ToString();
  }

  public string DBAttributesTableName
  {
    [DebuggerStepThrough] get => this._DBAttributesTableName;
  }

  public string DBKeyField
  {
    [DebuggerStepThrough] get => this._DBKeyField;
  }

  protected virtual string GetFromSQL()
  {
    return $" FROM {this.UserSession.QueryBuilder.SystemTableName} {this.UserSession.QueryBuilder.SystemTableAlias} ";
  }

  private bool IsObligatoryDateTime(int attributeID)
  {
    return ObligatoryObjectAttributesHelper.GetDataType((ObligatoryObjectAttributes) attributeID) == FieldTypes.ftDateTime;
  }

  private string GetFieldName(IDBAttributeType attrType, string fldName, bool isDateField)
  {
    if (isDateField)
      return attrType.Mask == Consts.OnlyDateFunction ? fldName : this.DBManager.DataProvider.GetUTCSelect(fldName, this.UserSession.TimeZoneOffset);
    switch (attrType.AttributeType)
    {
      case FieldTypes.ftDateTime:
        return attrType.Mask == Consts.OnlyDateFunction ? fldName : this.DBManager.DataProvider.GetUTCSelect(fldName, this.UserSession.TimeZoneOffset);
      case FieldTypes.ftSystem:
        if (!this.IsObligatoryDateTime(attrType.AttributeID))
          break;
        goto case FieldTypes.ftDateTime;
    }
    return fldName;
  }

  internal virtual string GetColumnsSQL(
    IDBAttributeType[] columns,
    ColumnContents[] contents,
    Intermech.Kernel.Search.ColumnInfo[] cinfo,
    int recordsCount)
  {
    if (columns.Length == 0)
      throw new KernelExceptionID(sc_12743.ssp_appserver_12744(39389234));
    if (cinfo != null && columns.Length != cinfo.Length)
      throw new KernelException(sc_12743.ssp_appserver_12745());
    string topRecordsInSelect = recordsCount <= 0 || recordsCount >= int.MaxValue ? "" : this.DBManager.DataProvider.GetTopRecordsInSelect(recordsCount);
    StringBuilder stringBuilder = new StringBuilder(256 /*0x0100*/);
    stringBuilder.Append("SELECT " + topRecordsInSelect);
    for (int index1 = 0; index1 < columns.Length; ++index1)
    {
      string str1 = string.Empty;
      for (int index2 = 0; index2 < index1; ++index2)
      {
        if (columns[index1].AttributeID == columns[index2].AttributeID && (contents == null || contents[index1] == contents[index2]))
        {
          str1 = "IND" + index1.ToString();
          break;
        }
      }
      IDBAttributeType column = columns[index1];
      if (column is DBSystemAttributeType)
      {
        if (column.AttributeID == -43 && this is IDBRelationCollection && this._ActualDate > DateTime.MinValue)
        {
          stringBuilder.Append(string.Format("(SELECT MAX(F_LC_STEP) FROM IMS_LCSTART_DATE WHERE IMS_LCSTART_DATE.F_START_DATE = (SELECT MAX(IMS_LCSTART_DATE.F_START_DATE) FROM IMS_LCSTART_DATE WHERE (IMS_LCSTART_DATE.F_OBJECT_ID = {0}.F_OBJECT_ID AND IMS_LCSTART_DATE.F_START_DATE < :ActualDatePar{1}))) F_ACTUAL_DATE{1}", (object) this.UserSession.QueryBuilder.SystemTableAlias, (object) str1));
          this.UserSession.QueryBuilder.PreparedParams.Add(this.DBManager.Parameter("ActualDatePar" + str1, (object) this._ActualDate));
        }
        else if (column.AttributeID == -18)
        {
          switch (this)
          {
            case IDBObjectCollection _:
            case IDBRelationCollection _:
              stringBuilder.Append($"(SELECT F_GUID FROM IMS_GUID_RESOLVE WHERE IMS_GUID_RESOLVE.F_ID = {this.UserSession.QueryBuilder.SystemTableAlias}.F_ID AND F_CATEGORY_TYPE = 2) F_OBJ_GUID{str1}");
              break;
            default:
              throw new KernelExceptionID(sc_12743.ssp_appserver_12746(301660203));
          }
        }
        else if (column.AttributeID == -50 && this.LocalTypesMode)
        {
          if (this is DBHistoryCollection)
            stringBuilder.Append($"(SELECT CAPTION FROM IMS_GUID WHERE IMS_GUID.F_OBJECT_ID = ABS({this.UserSession.QueryBuilder.SystemTableAlias}.F_ID)) CAPTION{str1}");
          else
            stringBuilder.Append($"(SELECT CAPTION FROM IMS_GUID WHERE IMS_GUID.F_OBJECT_ID = ABS({this.UserSession.QueryBuilder.SystemTableAlias}.F_OBJECT_ID)) CAPTION{str1}");
        }
        else if (column.AttributeID == -12 && this.LocalTypesMode)
          stringBuilder.Append($"(SELECT F_GUID FROM IMS_GUID WHERE IMS_GUID.F_OBJECT_ID = ABS({this.UserSession.QueryBuilder.SystemTableAlias}.F_OBJECT_ID)) F_GUID{str1}");
        else if (column.AttributeID == -10 && this.UserSession.QueryBuilder.SystemTableName != "IMS_OBJECTS")
        {
          switch (this)
          {
            case IDBObjectCollection _:
            case IDBRelationCollection _:
              stringBuilder.Append($"(SELECT {this.DBManager.DataProvider.GetUTCSelect("F_MODIFY_DATE", this.UserSession.TimeZoneOffset)} FROM IMS_OBJECTS WHERE IMS_OBJECTS.F_OBJECT_ID = {this.UserSession.QueryBuilder.SystemTableAlias}.F_OBJECT_ID) F_MODIFY_DATE{str1}");
              break;
            default:
              throw new KernelExceptionID(sc_12743.ssp_appserver_12747(1268494078));
          }
        }
        else if (column.AttributeID == -83)
        {
          switch (this)
          {
            case IDBObjectCollection _:
            case IDBRelationCollection _:
              stringBuilder.Append($"(SELECT F_PARENT_ID FROM IMS_VERSIONS_TREE WHERE IMS_VERSIONS_TREE.F_OBJECT_ID = ABS({this.UserSession.QueryBuilder.SystemTableAlias}.F_OBJECT_ID)) F_PARENT_OBJECT_ID{str1}");
              break;
            default:
              throw new KernelExceptionID(sc_12743.ssp_appserver_12748(914496414));
          }
        }
        else if ((column as DBAttributeType).IsVirtualAttribute)
        {
          switch (this)
          {
            case IDBObjectCollection _:
            case IDBRelationCollection _:
              stringBuilder.Append($"{(column as DBAttributeType).GetSQL(this.UserSession.QueryBuilder.SystemTableAlias)} {((ObligatoryObjectAttributes) column.AttributeID).ToString()}{str1}");
              break;
            default:
              throw new KernelExceptionID(sc_12743.ssp_appserver_12749(1384069278));
          }
        }
        else if (ObligatoryObjectAttributesHelper.GetAttributeSourceType((ObligatoryObjectAttributes) column.AttributeID) == AttributeSourceTypes.Relation && !(this is DBHistoryCollection))
        {
          int attributeId = column.AttributeID;
          if (attributeId == KernelConsts.Attributes.VirtualAttributeFiltrationResultsId || attributeId == KernelConsts.Attributes.VirtualAttributeElementStatusesId)
            stringBuilder.Append("0");
          else
            stringBuilder.Append(this.GetFieldName(column, $"{this.UserSession.QueryBuilder.RelationsTableAlias}.{column.TextFieldName} {str1}", false));
        }
        else
        {
          int attributeId = column.AttributeID;
          if (attributeId == KernelConsts.Attributes.VirtualAttributeFiltrationResultsId || attributeId == KernelConsts.Attributes.VirtualAttributeElementStatusesId)
            stringBuilder.Append("0 V_STATUS");
          else
            stringBuilder.Append(this.GetFieldName(column, $"{this.UserSession.QueryBuilder.SystemTableAlias}.{column.TextFieldName} {str1}", false));
        }
        stringBuilder.Append(",");
      }
      else
      {
        AttributeSourceTypes attributeSourceTypes = AttributeSourceTypes.Auto;
        if (cinfo != null)
          attributeSourceTypes = cinfo[index1].AttributeSource;
        if (attributeSourceTypes == AttributeSourceTypes.Auto)
        {
          switch (this)
          {
            case DBRelationCollection _:
              attributeSourceTypes = AttributeSourceTypes.Relation;
              break;
            case EventLog _:
              attributeSourceTypes = AttributeSourceTypes.Events;
              break;
            default:
              attributeSourceTypes = AttributeSourceTypes.Object;
              break;
          }
        }
        Attribute4ID attrStruct = new Attribute4ID(column.AttributeID);
        if (attributeSourceTypes == AttributeSourceTypes.Relation)
          attrStruct.RelationTypeID = this.UserSession.QueryBuilder.RelationsTypeID;
        else
          attrStruct.ObjectTypeID = this.UserSession.QueryBuilder.OptimizedTypeID;
        OptimizationModes optimizationModes;
        if (attrStruct.ObjectTypeID < 0 && attrStruct.RelationTypeID < 0)
        {
          optimizationModes = OptimizationModes.Write;
        }
        else
        {
          optimizationModes = this.UserSession.DBCache.GetOptimizationMode(attrStruct);
          if (optimizationModes == OptimizationModes.NotFound)
            optimizationModes = OptimizationModes.Write;
        }
        char ch1 = 'T';
        string str2 = optimizationModes != OptimizationModes.Write ? "F" + column.AttributeID.ToString() : column.TextFieldName;
        bool isDateField = false;
        if (contents != null)
        {
          switch (contents.Length > index1 ? (int) contents[index1] : 0)
          {
            case 1:
              if (column.AttributeType != FieldTypes.ftFile && column.AttributeType != FieldTypes.ftBlob && column.AttributeType != FieldTypes.ftShortBlob && column.AttributeType != FieldTypes.ftObjectLink && column.AttributeType != FieldTypes.ftObjectLinkByID && column.AttributeType != FieldTypes.ftMeasured && column.AttributeType != FieldTypes.ftExternalLink && column.AttributeType != FieldTypes.ftMemo)
                throw new KernelExceptionID(sc_12743.ssp_appserver_12751(336787864), (object) "ColumnContents.ID", (object) AttributesTypeHelper.GetCaption(new FieldTypes[6]
                {
                  FieldTypes.ftFile,
                  FieldTypes.ftExternalLink,
                  FieldTypes.ftMeasured,
                  FieldTypes.ftShortBlob,
                  FieldTypes.ftBlob,
                  FieldTypes.ftObjectLink
                }));
              str2 = optimizationModes != OptimizationModes.Write ? $"F{column.AttributeID}ID" : "F_INTEGER_VALUE";
              ch1 = 'I';
              break;
            case 2:
              if (column.AttributeType != FieldTypes.ftFile && column.AttributeType != FieldTypes.ftBlob && column.AttributeType != FieldTypes.ftMemo && column.AttributeType != FieldTypes.ftShortBlob)
                throw new KernelExceptionID(sc_12743.ssp_appserver_12750(186072131), (object) "ColumnContents.Date", (object) AttributesTypeHelper.GetCaption(new FieldTypes[4]
                {
                  FieldTypes.ftFile,
                  FieldTypes.ftShortBlob,
                  FieldTypes.ftBlob,
                  FieldTypes.ftMemo
                }));
              str2 = optimizationModes != OptimizationModes.Write ? $"F{column.AttributeID}ID3" : "F_DATE_VALUE";
              ch1 = 'D';
              isDateField = true;
              break;
            case 3:
              if (column.AttributeType != FieldTypes.ftFile && column.AttributeType != FieldTypes.ftBlob && column.AttributeType != FieldTypes.ftMeasured)
                throw new KernelExceptionID(sc_12743.ssp_appserver_12752(805163629), (object) "ColumnContents.Value", (object) AttributesTypeHelper.GetCaption(new FieldTypes[3]
                {
                  FieldTypes.ftFile,
                  FieldTypes.ftShortBlob,
                  FieldTypes.ftMeasured
                }));
              str2 = optimizationModes != OptimizationModes.Write ? $"F{column.AttributeID}ID2" : "F_DOUBLE_VALUE";
              ch1 = 'V';
              break;
          }
        }
        if (column.AttributeID == this.UserSession.IdentHelper.InternalRegNumber && this.UserSession.InternalDepartmentID > 0L)
        {
          char ch2 = attributeSourceTypes != AttributeSourceTypes.Relation ? 'O' : 'R';
          stringBuilder.Append($"(SELECT F_STRING_VALUE FROM IMS_ATTRFILTER_VALUE WHERE IMS_ATTRFILTER_VALUE.F_OBJECT_ID = {this.UserSession.QueryBuilder.SystemTableAlias}.F_OBJECT_ID AND F_FILTER_ID = {this.UserSession.InternalDepartmentID}) {str2}{ch1}{ch2}{str1},");
        }
        else if (optimizationModes == OptimizationModes.Write)
        {
          string fieldName = this.GetFieldName(column, $"T{column.AttributeID}.{str2}", isDateField);
          bool flag = false;
          string str3;
          string str4;
          string str5;
          char ch3;
          if (attributeSourceTypes == AttributeSourceTypes.Relation)
          {
            str3 = "IMS_RELATION_ATTRS";
            str4 = this.UserSession.QueryBuilder.RelationsTableAlias;
            str5 = "F_PRJLINK_ID";
            ch3 = 'R';
            if (this._RecordsTypeID > -1 && !MetaDataHelper.GetRelationType(this._RecordsTypeID).AnyAttributes && MetaDataHelper.GetAttribute4RelationType(this._RecordsTypeID, column.AttributeID) == null)
              flag = true;
          }
          else
          {
            int anObjectTypeID = !(this is DBRelationCollection) ? this._RecordsTypeID : (this as DBRelationCollection)._ObjectTypeID;
            if (anObjectTypeID > -1)
            {
              DBObjectType objectType = this.UserSession.GetObjectType(anObjectTypeID) as DBObjectType;
              if (objectType.IsLocalType)
              {
                str3 = objectType.AttributesTableName;
                if (!objectType.AnyAttributes && !objectType.HasAttribute(column.AttributeID))
                  flag = true;
              }
              else
                str3 = "IMS_OBJECT_ATTRS";
            }
            else
              str3 = "IMS_OBJECT_ATTRS";
            str4 = this.UserSession.QueryBuilder.SystemTableAlias;
            str5 = "F_OBJECT_ID";
            ch3 = 'O';
          }
          if (flag)
          {
            string str6 = string.Empty;
            string str7 = column.TextFieldName;
            if (contents != null && contents[index1] != ColumnContents.Text)
            {
              if (contents[index1] == ColumnContents.Value)
                str7 = column.ValueFieldName;
              else if (contents[index1] == ColumnContents.Date)
                str7 = "F_DATE_VALUE";
              else if (contents[index1] == ColumnContents.ID)
                str7 = "F_INTEGER_VALUE";
              else if (contents[index1] == ColumnContents.String)
                str7 = "F_STRING_VALUE";
            }
            switch (str7)
            {
              case "F_STRING_VALUE":
                str6 = this.DBManager.DataProvider.NVARCHARType(Consts.MaxStringSize);
                break;
              case "F_INTEGER_VALUE":
                str6 = this.DBManager.DataProvider.INTEGERType;
                break;
              case "F_DATE_VALUE":
                str6 = this.DBManager.DataProvider.DATEType;
                break;
              case "F_DOUBLE_VALUE":
                str6 = this.DBManager.DataProvider.FLOATType;
                break;
            }
            stringBuilder.AppendFormat("CAST(NULL AS {4}) F{0}{1}{2}{3},", (object) column.AttributeID, (object) ch1, (object) ch3, (object) str1, (object) str6);
          }
          else
            stringBuilder.AppendFormat("(SELECT {1} FROM {2} T{0} WHERE T{0}.{3}={4}.{3} AND T{0}.F_ATTRIBUTE_ID={0} AND T{0}.F_INLIST_ID = 0) F{0}{5}{6}{7},", (object) column.AttributeID, (object) fieldName, (object) str3, (object) str5, (object) str4, (object) ch1, (object) ch3, (object) str1);
        }
        else if (attributeSourceTypes == AttributeSourceTypes.Relation)
          stringBuilder.Append(this.GetFieldName(column, $"{this.UserSession.QueryBuilder.RelationsTableAlias}.{str2}", isDateField) + $" F{column.AttributeID}{ch1}R{str1},");
        else
          stringBuilder.Append(this.GetFieldName(column, $"{this.UserSession.QueryBuilder.SystemTableAlias}.{str2}", isDateField) + $" F{column.AttributeID}{ch1}O{str1},");
      }
    }
    --stringBuilder.Length;
    if (this.UserSession.QueryBuilder.WordsIDs != string.Empty)
      stringBuilder.AppendFormat(", (SELECT MAX(F_TF_IDF) FROM IMS_INDEX_RESULT WHERE IMS_INDEX_RESULT.F_OBJECT_ID = {0}.F_OBJECT_ID AND IMS_INDEX_RESULT.F_WORD_ID IN ({1})) F_TF_IDF ", (object) this.UserSession.QueryBuilder.SystemTableAlias, (object) this.UserSession.QueryBuilder.WordsIDs);
    stringBuilder.Append(this.GetFromSQL());
    return stringBuilder.ToString();
  }

  protected virtual void ConfigureQueryBuilder(ConditionStructure[] conditions)
  {
    this.UserSession.QueryBuilder.PreparedParams.Clear();
    this.UserSession.QueryBuilder.CurrentRecordSet = this;
  }

  internal static int GetElementsStatusesColumnIdx(ref DBRecordSetParams pars)
  {
    int statusesColumnIdx = -1;
    Guid guid = new Guid("cad005f1-306c-11d8-b4e9-00304f19f545");
    if (pars.Columns != null && pars.Columns.Length != 0)
    {
      for (int index = 0; index < pars.Columns.Length; ++index)
      {
        object column = pars.Columns[index];
        if (column.GetType() == typeof (Guid) && (Guid) column == guid)
        {
          statusesColumnIdx = index;
          break;
        }
        if (column.GetType() == typeof (int) && (int) column == -77)
        {
          statusesColumnIdx = index;
          break;
        }
        if (column.GetType() == typeof (string) && (string) column == ObligatoryObjectAttributesHelper.GetCaption(ObligatoryObjectAttributes.F_ELEMENT_STATUSES))
        {
          statusesColumnIdx = index;
          break;
        }
      }
    }
    return statusesColumnIdx;
  }

  protected virtual bool AddDeletedObjectsFilter
  {
    get => !this.UserSession.ShowDeletedObjects && !this._TrashMode;
  }

  internal string GetObjectsFilter(int objectTypeID, string objectTableName)
  {
    string objectsFilter = "";
    string str1 = "";
    bool flag;
    if (objectTableName == string.Empty)
    {
      objectTableName = this.UserSession.QueryBuilder.SystemTableAlias;
      flag = true;
    }
    else
      flag = false;
    int num;
    if (this.AddDeletedObjectsFilter)
    {
      string str2 = objectTableName;
      num = this.UserSession.IdentHelper.DeletedID;
      string str3 = num.ToString();
      objectsFilter = $"({str2}.F_LEVEL_ID <> {str3})";
      str1 = " AND ";
    }
    if (!this.GlobalSelectMode)
    {
      if (this._CurrentModificationID != 0L)
      {
        objectsFilter += $"{str1}({objectTableName}.F_MODIFICATION_ID = {this._CurrentModificationID})";
        str1 = " AND ";
      }
      else if (!this._ShowAllModifications && !this.UseVersionInComposition)
      {
        if (this.UserSession.EditingContextID != 0L)
        {
          objectsFilter += string.Format("{0}({1}.F_MODIFICATION_ID IN (0, :modifyID) OR {1}.F_BASE_VERSION = 1)", (object) str1, (object) objectTableName);
          if (flag)
            this.UserSession.QueryBuilder.PreparedParams.Add(this.DBManager.Parameter("modifyID", (object) this.UserSession.EditingContextModificationID));
        }
        else
          objectsFilter += string.Format("{0}({1}.F_MODIFICATION_ID = 0 OR {1}.F_BASE_VERSION = 1)", (object) str1, (object) objectTableName);
        str1 = " AND ";
      }
      if (!this.UserSession.ShowPersonalObjects && !this._ShowPersonalObjects)
      {
        string str4 = objectsFilter;
        object[] objArray = new object[4]
        {
          (object) str1,
          (object) objectTableName,
          null,
          null
        };
        num = this.UserSession.IdentHelper.PersonalLevelID;
        objArray[2] = (object) num.ToString();
        objArray[3] = (object) this.UserSession.DBSecurity._OwnerGroupsSQL;
        string str5 = string.Format("{0}({1}.F_LEVEL_ID <> {2} OR {1}.F_OWNER_ID IN ({3}))", objArray);
        objectsFilter = str4 + str5;
        str1 = " AND ";
      }
      if (this is DBObjectCollection)
      {
        if (this._RecordsTypeID == this.UserSession.IdentHelper.UsersTypeID && !this.UserSession.IsAdmin && !this._TrashMode)
        {
          objectsFilter += $"{str1}({objectTableName}.F_LEVEL_ID <> {this.UserSession.IdentHelper.AnnulmentLevelID})";
          str1 = " AND ";
        }
        if (this.UserSession.ProjectFiltrationMode == ProjectFiltrationModes.CurrentProject)
        {
          objectsFilter += string.Format("{0}({1}.F_PROJECT_ID = :prjID OR {1}.F_PROJECT_ID = 0)", (object) str1, (object) objectTableName);
          if (flag)
            this.UserSession.QueryBuilder.PreparedParams.Add(this.DBManager.Parameter("prjID", (object) this.UserSession.CurrentProjectID));
          str1 = " AND ";
        }
        else if (this.UserSession.ProjectFiltrationMode == ProjectFiltrationModes.OnlyCurrentProject)
        {
          objectsFilter += $"{str1}({objectTableName}.F_PROJECT_ID = :prjID)";
          if (flag)
            this.UserSession.QueryBuilder.PreparedParams.Add(this.DBManager.Parameter("prjID", (object) this.UserSession.CurrentProjectID));
          str1 = " AND ";
        }
        else if (this.UserSession.ProjectFiltrationMode == ProjectFiltrationModes.UserProjects)
        {
          objectsFilter += string.Format("{0}(({1}.F_PROJECT_ID IN (SELECT P_TEAM.F_PROJECT_ID FROM IMS_PROJECT_TEAM P_TEAM WHERE P_TEAM.F_PROJECT_ID = {1}.F_PROJECT_ID AND P_TEAM.F_USER_ID IN ({2}))) OR {1}.F_PROJECT_ID = 0)", (object) str1, (object) objectTableName, (object) this.UserSession.DBSecurity._GroupsSQL);
          str1 = " AND ";
        }
      }
      if (this.UserSession.SecurityLevel < int.MaxValue && ServerConsts.MandateAccess)
      {
        string str6 = !this._RevertAccessFiltration ? "<=" : ">";
        objectsFilter += $"{str1}({objectTableName}.F_ACCESS {str6} :usrLevel)";
        if (flag)
          this.UserSession.QueryBuilder.PreparedParams.Add(this.DBManager.Parameter("usrLevel", (object) this.UserSession.SecurityLevel));
        str1 = " AND ";
      }
      if (!this._ShowNotOwnedWorkCopies)
      {
        objectsFilter += string.Format("{0}(({1}.F_OBJECT_ID > 0 AND {1}.F_CHKOUT_BY <> :chckoutID) OR ({1}.F_OBJECT_ID < 0 AND {1}.F_CHKOUT_BY = :chckoutID))", (object) str1, (object) objectTableName);
        if (flag)
          this.UserSession.QueryBuilder.PreparedParams.Add(this.DBManager.Parameter("chckoutID", (object) this.UserSession.UserID));
      }
    }
    if (((objectTypeID < 0 ? 0 : (this.UserSession.QueryBuilder.OptimizedTypeID < 0 ? 1 : 0)) & (flag ? 1 : 0)) != 0)
    {
      DBObjectType objectType = this.UserSession.GetObjectType(objectTypeID) as DBObjectType;
      ArrayList arrayList = new ArrayList();
      ArrayList objsTreeList = arrayList;
      objectType.FillChildrenList(objsTreeList);
      StringBuilder stringBuilder = new StringBuilder();
      if (arrayList.Count != 1)
      {
        stringBuilder.Append($"({this.UserSession.QueryBuilder.SystemTableAlias}.F_OBJECT_TYPE IN (");
        for (int index = 0; index < arrayList.Count; ++index)
        {
          stringBuilder.AppendFormat($":objTypeID{index.ToString()},");
          this.UserSession.QueryBuilder.PreparedParams.Add(this.DBManager.Parameter(":objTypeID" + index.ToString(), (object) Convert.ToInt32(arrayList[index])));
        }
        stringBuilder[stringBuilder.Length - 1] = ')';
        stringBuilder.Append(")");
      }
      else
      {
        stringBuilder.Append($"({this.UserSession.QueryBuilder.SystemTableAlias}.F_OBJECT_TYPE = :objTypeID0)");
        this.UserSession.QueryBuilder.PreparedParams.Add(this.DBManager.Parameter(":objTypeID0", (object) Convert.ToInt32(arrayList[0])));
      }
      if (objectsFilter != string.Empty)
      {
        stringBuilder.Append(" AND ");
        stringBuilder.Append(objectsFilter);
      }
      objectsFilter = stringBuilder.ToString();
    }
    return objectsFilter;
  }

  protected virtual string GetWhereSQL(ConditionStructure[] conditions, int recordsCount)
  {
    this.ConfigureQueryBuilder(conditions);
    return this.UserSession.QueryBuilder.GetWhereSQL(conditions, recordsCount, this.DBManager);
  }

  protected virtual IDBAttributeType[] GetColumnsCollection(
    ref DBRecordSetParams pars,
    bool failIfNotFound)
  {
    return this.UserSession.GetAttributeTypeCollection(0).GetAttributeTypeList(pars.Columns, failIfNotFound);
  }

  private DBRecordSetParams PreparePacketParams(DBRecordSetParams paramSet)
  {
    if (paramSet.RecordCount == -2)
      paramSet.RecordCount = this.UserSession.MaxRows + 1;
    bool flag1 = false;
    RelationalOperators relationalOperator = RelationalOperators.Greater;
    EventLogHelper service = ServerServices.GetService(typeof (IEventLogHelper)) as EventLogHelper;
    if (paramSet.SortColumns == null)
    {
      paramSet.SortColumns = new object[1]
      {
        (object) this._DBKeyFieldID
      };
      paramSet.Orders = new SortOrders[1]{ SortOrders.ASC };
      flag1 = true;
    }
    else
    {
      bool flag2 = false;
      for (int index = 0; index < paramSet.SortColumns.Length; ++index)
      {
        if (this._DBKeyFieldID == service.GetAttributeID(paramSet.SortColumns[index]))
        {
          if (index == 0)
            flag1 = true;
          flag2 = true;
          if (paramSet.Orders[index] == SortOrders.DESC)
          {
            relationalOperator = RelationalOperators.Less;
            break;
          }
          break;
        }
      }
      if (!flag2)
      {
        object[] sortColumns = paramSet.SortColumns;
        SortOrders[] orders = paramSet.Orders;
        AttributeSourceTypes[] sortSources = paramSet.SortSources;
        paramSet.SortColumns = new object[sortColumns.Length + 1];
        paramSet.Orders = new SortOrders[sortColumns.Length + 1];
        if (sortSources != null)
          paramSet.SortSources = new AttributeSourceTypes[sortColumns.Length + 1];
        for (int index = 0; index < sortColumns.Length; ++index)
        {
          paramSet.SortColumns[index] = sortColumns[index];
          paramSet.Orders[index] = orders[index];
          if (sortSources != null)
            paramSet.SortSources[index] = sortSources[index];
        }
        paramSet.SortColumns[sortColumns.Length] = (object) this._DBKeyFieldID;
        paramSet.Orders[sortColumns.Length] = SortOrders.ASC;
        if (sortSources != null)
          paramSet.SortSources[sortColumns.Length] = this._DBKeyFieldID != Convert.ToInt32((object) ObligatoryObjectAttributes.F_OBJECT_ID) ? (this._DBKeyFieldID != Convert.ToInt32((object) ObligatoryObjectAttributes.F_PRJLINK_ID) ? AttributeSourceTypes.Auto : AttributeSourceTypes.Relation) : AttributeSourceTypes.Object;
      }
    }
    if (paramSet.LastKeyValue != 0L && paramSet.LastKeyValue != -1L)
    {
      List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
      if (flag1)
      {
        AttributeSourceTypes attributeSourceType = ObligatoryObjectAttributesHelper.GetAttributeSourceType((ObligatoryObjectAttributes) this._DBKeyFieldID);
        conditionStructureList.Add(new ConditionStructure(this._DBKeyFieldID, relationalOperator, (object) paramSet.LastKeyValue, (object) null, LogicalOperators.AND, 0, true, attributeSourceType));
      }
      else
      {
        IList list = paramSet.LastOrderValue != null ? paramSet.LastOrderValue as IList : throw new KernelExceptionID(sc_12743.ssp_appserver_12753(645292122));
        if (paramSet.SortColumns.Length > 2)
        {
          if (list == null)
            throw new KernelExceptionID(sc_12743.ssp_appserver_12754(1711489405));
          list.Add((object) paramSet.LastKeyValue);
        }
        else if (list == null)
          list = (IList) new object[2]
          {
            paramSet.LastOrderValue,
            (object) paramSet.LastKeyValue
          };
        else
          list.Add((object) paramSet.LastKeyValue);
        int attributeId1 = service.GetAttributeID(paramSet.SortColumns[0]);
        AttributeSourceTypes attributeSource1 = paramSet.SortSources != null ? paramSet.SortSources[0] : AttributeSourceTypes.Auto;
        if (list[0] == DBNull.Value)
        {
          if (paramSet.Orders[0] == SortOrders.ASC)
            conditionStructureList.Add(new ConditionStructure(attributeId1, RelationalOperators.NotEmpty, (object) null, (object) null, LogicalOperators.OR, 1, true, attributeSource1));
        }
        else if (paramSet.Orders[0] == SortOrders.ASC)
        {
          conditionStructureList.Add(new ConditionStructure(attributeId1, RelationalOperators.Greater, list[0], (object) QueryConsts.IsPacketCondition, LogicalOperators.OR, 2, true, attributeSource1));
        }
        else
        {
          conditionStructureList.Add(new ConditionStructure(attributeId1, RelationalOperators.Less, list[0], (object) QueryConsts.IsPacketCondition, LogicalOperators.OR, 2, true, attributeSource1));
          conditionStructureList.Add(new ConditionStructure(attributeId1, RelationalOperators.NotExistsOrEmpty, (object) null, (object) null, LogicalOperators.OR, -1, true, attributeSource1));
        }
        for (int index1 = 1; index1 < paramSet.SortColumns.Length; ++index1)
        {
          int attributeId2 = service.GetAttributeID(paramSet.SortColumns[index1]);
          for (int index2 = 0; index2 < index1; ++index2)
          {
            int attributeId3 = service.GetAttributeID(paramSet.SortColumns[index2]);
            int groupID = index2 != 0 ? 0 : 1;
            AttributeSourceTypes attributeSource2 = paramSet.SortSources != null ? paramSet.SortSources[index2] : AttributeSourceTypes.Auto;
            if (list[index2] == DBNull.Value)
              conditionStructureList.Add(new ConditionStructure(attributeId3, RelationalOperators.NotExistsOrEmpty, (object) null, (object) null, LogicalOperators.AND, groupID, true, attributeSource2));
            else
              conditionStructureList.Add(new ConditionStructure(attributeId3, RelationalOperators.Equal, list[index2], (object) QueryConsts.IsPacketCondition, LogicalOperators.AND, groupID, true, attributeSource2));
          }
          AttributeSourceTypes attributeSource3 = paramSet.SortSources != null ? paramSet.SortSources[index1] : AttributeSourceTypes.Auto;
          if (list[index1] == DBNull.Value)
          {
            if (paramSet.Orders[index1] == SortOrders.ASC)
              conditionStructureList.Add(new ConditionStructure(attributeId2, RelationalOperators.NotEmpty, (object) null, (object) null, LogicalOperators.OR, -1, true, attributeSource3));
          }
          else if (paramSet.Orders[index1] == SortOrders.ASC)
          {
            conditionStructureList.Add(new ConditionStructure(attributeId2, RelationalOperators.Greater, list[index1], (object) QueryConsts.IsPacketCondition, LogicalOperators.OR, -1, true, attributeSource3));
          }
          else
          {
            conditionStructureList.Add(new ConditionStructure(attributeId2, RelationalOperators.Less, list[index1], (object) QueryConsts.IsPacketCondition, LogicalOperators.OR, 1, true, attributeSource3));
            conditionStructureList.Add(new ConditionStructure(attributeId2, RelationalOperators.NotExistsOrEmpty, (object) null, (object) null, LogicalOperators.OR, -2, true, attributeSource3));
          }
        }
        ConditionStructure conditionStructure = conditionStructureList[conditionStructureList.Count - 1] with
        {
          LogicalOperator = LogicalOperators.AND
        };
        conditionStructureList[conditionStructureList.Count - 1] = conditionStructure;
      }
      ConditionStructure[] conditionStructureArray;
      if (paramSet.Conditions != null && paramSet.Conditions.Length != 0)
      {
        paramSet.Conditions[paramSet.Conditions.Length - 1].LogicalOperator = LogicalOperators.AND;
        conditionStructureArray = paramSet.Conditions;
      }
      else
        conditionStructureArray = new ConditionStructure[0];
      paramSet.Conditions = new ConditionStructure[conditionStructureArray.Length + conditionStructureList.Count];
      int num = 0;
      for (int index = 0; index < conditionStructureArray.Length; ++index)
        paramSet.Conditions[num++] = conditionStructureArray[index];
      if (num > 0)
        paramSet.Conditions[num - 1].LogicalOperator = LogicalOperators.AND;
      for (int index = 0; index < conditionStructureList.Count; ++index)
        paramSet.Conditions[num++] = conditionStructureList[index];
    }
    return paramSet;
  }

  protected virtual DBRecordSetParams PrepareAttributes(DBRecordSetParams paramSet)
  {
    if (paramSet.Conditions != null)
    {
      bool flag = false;
      for (int index = 0; index < paramSet.Conditions.Length; ++index)
      {
        paramSet.Conditions[index] = this.PrepareCondition(paramSet.Conditions[index]);
        int int32 = Convert.ToInt32(paramSet.Conditions[index].Attribute);
        if (int32 == -77)
          throw new KernelExceptionID(sc_12743.ssp_appserver_12755(710768962), (object) this.UserSession.GetAttributeType(Convert.ToInt32(paramSet.Conditions[index].Attribute)).Name);
        if (int32 > 0 && paramSet.Conditions[index].RelationalOperator == RelationalOperators.NotEqual)
          flag = true;
      }
      if (flag)
      {
        List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
        for (int index = 0; index < paramSet.Conditions.Length; ++index)
        {
          object obj = (object) null;
          int int32 = Convert.ToInt32(paramSet.Conditions[index].Attribute);
          if (int32 > 0 && paramSet.Conditions[index].RelationalOperator == RelationalOperators.NotEqual)
          {
            int num;
            int groupID;
            if (paramSet.Conditions[index].GroupID <= 0)
            {
              num = 1;
              groupID = paramSet.Conditions[index].GroupID - 1;
            }
            else
            {
              num = paramSet.Conditions[index].GroupID + 1;
              groupID = -1;
            }
            ConditionStructure conditionStructure = new ConditionStructure(int32, RelationalOperators.NotExistsOrEmpty, (object) null, (object) null, paramSet.Conditions[index].LogicalOperator, groupID, false, paramSet.Conditions[index].AttributeSource, paramSet.Conditions[index].Content);
            paramSet.Conditions[index].LogicalOperator = LogicalOperators.OR;
            paramSet.Conditions[index].GroupID = num;
            obj = (object) conditionStructure;
          }
          conditionStructureList.Add(paramSet.Conditions[index]);
          if (obj != null)
            conditionStructureList.Add((ConditionStructure) obj);
        }
        paramSet.Conditions = conditionStructureList.ToArray();
      }
    }
    return paramSet;
  }

  protected virtual void CheckRecorsSetParams(ref DBRecordSetParams paramSet)
  {
    if (paramSet.Columns != null)
    {
      if (paramSet.Contents != null && paramSet.Contents.Length != paramSet.Columns.Length)
        throw new KernelExceptionID(sc_12743.ssp_appserver_12756(1095784093));
      if (paramSet.ColumnsInfo != null && paramSet.ColumnsInfo.Length != paramSet.Columns.Length)
        throw new KernelExceptionID(sc_12743.ssp_appserver_12757(1487992165));
    }
    if (paramSet.SortColumns != null)
    {
      if (paramSet.Orders != null && paramSet.SortColumns.Length != paramSet.Orders.Length)
        throw new KernelExceptionID(sc_12743.ssp_appserver_12758(323502811));
      if (paramSet.SortContents != null && paramSet.SortColumns.Length != paramSet.SortContents.Length)
        throw new KernelExceptionID(sc_12743.ssp_appserver_12759(1403986921));
      if (paramSet.SortSources != null && paramSet.SortColumns.Length != paramSet.SortSources.Length)
        throw new KernelExceptionID(sc_12743.ssp_appserver_12760(43532341));
    }
    if (paramSet.Tags == null || !(paramSet.Tags[(object) "ShowNotOwnedWorkCopies"] is bool))
      return;
    this._ShowNotOwnedWorkCopies = (bool) paramSet.Tags[(object) "ShowNotOwnedWorkCopies"];
  }

  public virtual DataTable Select(DBRecordSetParams paramSet) => this.Select(ref paramSet);

  protected virtual DBRecordSetParams? OnBeforeRecordsSelect(DBRecordSetParams oldParameters)
  {
    BeforeRecordsSelectEventArgs args = new BeforeRecordsSelectEventArgs(oldParameters, (IUserSession) this.UserSession);
    (this.EventHelper as EventLogHelper).OnBeforeRecordsSelect((object) this, args);
    return args.NewParameters;
  }

  protected virtual DataTable Select(ref DBRecordSetParams paramSet)
  {
    this.CheckRecorsSetParams(ref paramSet);
    object tag;
    if (paramSet.Tags?[(object) "ShowAllModifications"] != null && (tag = paramSet.Tags[(object) "ShowAllModifications"]) is bool)
      this.ShowAllModifications = (bool) tag;
    int num1 = 0;
    DBRecordSetParams? nullable = this.OnBeforeRecordsSelect(paramSet);
    if (nullable.HasValue)
      paramSet = nullable.Value;
    int num2 = paramSet.RecordCount;
    if (paramSet.RecordCount == -1)
    {
      num2 = 2147483646;
    }
    else
    {
      if (paramSet.RecordCount == -2)
        num2 = this.UserSession.MaxRows;
      if (paramSet.RecordCount != 0)
        paramSet = this.PreparePacketParams(paramSet);
    }
    IDBAttributeType[] attributeTypeList = this.UserSession.GetAttributeTypeCollection(0).GetAttributeTypeList(paramSet.SortColumns, paramSet.FailIfNotFound);
    if (paramSet.RecordCount != 0)
    {
      for (int index1 = 0; index1 < attributeTypeList.Length; ++index1)
      {
        if (attributeTypeList[index1].AttributeID == -77)
          throw new KernelExceptionID(sc_12743.ssp_appserver_12761(1065660815), (object) attributeTypeList[index1].Name);
        if (attributeTypeList[index1].AttributeType == FieldTypes.ftMeasured)
        {
          object[] columns = paramSet.Columns;
          paramSet.Columns = new object[paramSet.Columns.Length + 1];
          columns.CopyTo((Array) paramSet.Columns, 0);
          paramSet.Columns[columns.Length] = (object) attributeTypeList[index1].AttributeID;
          if (paramSet.Contents == null)
          {
            paramSet.Contents = new ColumnContents[paramSet.Columns.Length];
            for (int index2 = 0; index2 < paramSet.Columns.Length - 1; ++index2)
              paramSet.Contents[index2] = ColumnContents.Text;
            paramSet.Contents[paramSet.Columns.Length - 1] = ColumnContents.Value;
          }
          else
          {
            ColumnContents[] contents = paramSet.Contents;
            paramSet.Contents = new ColumnContents[paramSet.Contents.Length + 1];
            contents.CopyTo((Array) paramSet.Contents, 0);
            paramSet.Contents[contents.Length] = ColumnContents.Value;
          }
          if (paramSet.ColumnNames != null)
          {
            ColumnNameMapping[] columnNames = paramSet.ColumnNames;
            paramSet.ColumnNames = new ColumnNameMapping[paramSet.ColumnNames.Length + 1];
            columnNames.CopyTo((Array) paramSet.ColumnNames, 0);
            paramSet.ColumnNames[columnNames.Length] = ColumnNameMapping.Index;
          }
          if (paramSet.ColumnsInfo == null)
          {
            paramSet.ColumnsInfo = new Intermech.Kernel.Search.ColumnInfo[paramSet.Columns.Length];
            for (int index3 = 0; index3 < paramSet.Columns.Length; ++index3)
              paramSet.ColumnsInfo[index3] = new Intermech.Kernel.Search.ColumnInfo(paramSet.Columns[index3], AttributeSourceTypes.Auto, (object) null);
          }
          else
          {
            Intermech.Kernel.Search.ColumnInfo[] columnsInfo = paramSet.ColumnsInfo;
            paramSet.ColumnsInfo = new Intermech.Kernel.Search.ColumnInfo[paramSet.ColumnsInfo.Length + 1];
            columnsInfo.CopyTo((Array) paramSet.ColumnsInfo, 0);
            if (paramSet.SortSources != null)
            {
              paramSet.ColumnsInfo[columnsInfo.Length] = new Intermech.Kernel.Search.ColumnInfo((object) attributeTypeList[index1].AttributeID, paramSet.SortSources[index1], (object) null);
            }
            else
            {
              bool flag = false;
              DataTable table = this.UserSession.DBCache.GetTable("IMS_ATTRIBUTES");
              for (int index4 = 0; index4 < paramSet.Columns.Length; ++index4)
              {
                if (attributeTypeList[index1].AttributeID == AttributeCacheHelper.GetAttributeID(paramSet.Columns[index4], table, true))
                {
                  paramSet.ColumnsInfo[columnsInfo.Length] = new Intermech.Kernel.Search.ColumnInfo((object) attributeTypeList[index1].AttributeID, paramSet.ColumnsInfo[index4].AttributeSource, (object) null);
                  flag = true;
                  break;
                }
              }
              if (!flag)
                paramSet.ColumnsInfo[columnsInfo.Length] = new Intermech.Kernel.Search.ColumnInfo((object) attributeTypeList[index1].AttributeID, AttributeSourceTypes.Auto, (object) null);
            }
          }
          if (paramSet.LastOrderValue != null && paramSet.LastOrderValue is IList lastOrderValue && lastOrderValue[index1] != null && lastOrderValue[index1].ToString().Trim() != string.Empty)
          {
            MeasuredValue measuredValue = MeasureHelper.ConvertToMeasuredValue(lastOrderValue[index1].ToString());
            lastOrderValue[index1] = (object) MeasureHelper.ConvertToBaseMeasure(measuredValue).Value;
          }
          ++num1;
        }
      }
    }
    IDBAttributeType[] columnsCollection = this.GetColumnsCollection(ref paramSet, paramSet.FailIfNotFound);
    if (this._CheckAccess)
    {
      for (int index = 0; index < columnsCollection.Length; ++index)
      {
        if (columnsCollection[index].AttributeID > 0)
          (columnsCollection[index] as IDBSecurity).CheckAccess(ActionType.List, true, true);
      }
    }
    if (paramSet.TableName == "")
      paramSet.TableName = this.ObjectName;
    paramSet = this.PrepareAttributes(paramSet);
    if (this.LocalTypesMode && this is DBObjectCollection)
    {
      for (int index = 0; index < columnsCollection.Length; ++index)
        this.ValidateLocalTypeAttribute((object) columnsCollection[index].AttributeID);
    }
    DataTable source;
    try
    {
      string whereSql = this.GetWhereSQL(paramSet.Conditions, num2 + 1);
      if (num2 == 0)
      {
        source = this.DBManager.ExecuteDataTable($"SELECT COUNT(*) {this.GetFromSQL()}{whereSql}", this.UserSession.QueryBuilder.PreparedParams.ToArray());
        source.TableName = paramSet.TableName;
      }
      else
      {
        string columnsSql = this.GetColumnsSQL(columnsCollection, paramSet.Contents, paramSet.ColumnsInfo, num2 + 1);
        string commandText;
        using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate())
        {
          StringBuilder stringBuilder = objectPoolScope.Object;
          if (this._SortOrder != null)
            this._SortOrder = string.Empty;
          if (attributeTypeList.Length != 0)
          {
            stringBuilder.Append(" ORDER BY ");
            if (this.UserSession.QueryBuilder.WordsIDs != string.Empty)
              stringBuilder.Append("F_TF_IDF DESC,");
            for (int index5 = 0; index5 < attributeTypeList.Length; ++index5)
            {
              SortOrders sortOrders = paramSet.Orders == null || paramSet.Orders.Length <= index5 || paramSet.Orders[index5] == SortOrders.NONE ? SortOrders.ASC : paramSet.Orders[index5];
              for (int index6 = 0; index6 < index5; ++index6)
              {
                if (attributeTypeList[index5].AttributeID == attributeTypeList[index6].AttributeID)
                  throw new KernelExceptionID(sc_12743.ssp_appserver_12762(336127854), (object) attributeTypeList[index5].Name);
              }
              if (!attributeTypeList[index5].IsGridable)
                throw new KernelExceptionID(sc_12743.ssp_appserver_12763(880189834), (object) attributeTypeList[index5].Name, (object) (attributeTypeList[index5] as DBAttributeType).TypeCaption);
              if (attributeTypeList[index5] is DBSystemAttributeType)
              {
                stringBuilder.AppendFormat("{0} {1} {2},", (object) attributeTypeList[index5].TextFieldName, (object) sortOrders, (object) this.DBManager.DataProvider.NullsOrder);
                if (this._SortOrder != null)
                  this._SortOrder += $"{attributeTypeList[index5].TextFieldName} {sortOrders},";
              }
              else
              {
                char ch1;
                if (paramSet.SortSources == null)
                {
                  if (paramSet.ColumnsInfo != null)
                  {
                    ch1 = 'N';
                    foreach (Intermech.Kernel.Search.ColumnInfo columnInfo in paramSet.ColumnsInfo)
                    {
                      if (Convert.ToInt32(columnInfo.AttributeID) == attributeTypeList[index5].AttributeID)
                      {
                        AttributeSourceTypes attributeSourceTypes = columnInfo.AttributeSource;
                        if (columnInfo.AttributeSource == AttributeSourceTypes.Auto)
                        {
                          switch (this)
                          {
                            case DBRelationCollection _:
                              attributeSourceTypes = AttributeSourceTypes.Relation;
                              break;
                            case EventLog _:
                              attributeSourceTypes = AttributeSourceTypes.Events;
                              break;
                            default:
                              attributeSourceTypes = AttributeSourceTypes.Object;
                              break;
                          }
                        }
                        ch1 = attributeSourceTypes != AttributeSourceTypes.Relation ? 'O' : 'R';
                        break;
                      }
                    }
                    if (ch1 == 'N')
                      ch1 = !(this is DBRelationCollection) ? 'O' : 'R';
                  }
                  else
                    ch1 = !(this is DBRelationCollection) ? 'O' : 'R';
                }
                else
                  ch1 = paramSet.SortSources[index5] != AttributeSourceTypes.Relation ? (paramSet.SortSources[index5] != AttributeSourceTypes.Auto || !(this is DBRelationCollection) ? 'O' : 'R') : 'R';
                char ch2;
                if (paramSet.SortContents == null)
                {
                  ch2 = attributeTypeList[index5].AttributeType != FieldTypes.ftMeasured ? 'T' : 'V';
                }
                else
                {
                  switch (paramSet.SortContents[index5])
                  {
                    case ColumnContents.ID:
                      ch2 = 'I';
                      break;
                    case ColumnContents.Date:
                      ch2 = 'D';
                      break;
                    case ColumnContents.Value:
                      ch2 = 'V';
                      break;
                    default:
                      ch2 = attributeTypeList[index5].AttributeType != FieldTypes.ftMeasured ? 'T' : 'V';
                      break;
                  }
                }
                bool flag = false;
                foreach (IDBAttributeType dbAttributeType in columnsCollection)
                {
                  if (dbAttributeType.AttributeID == attributeTypeList[index5].AttributeID)
                  {
                    flag = true;
                    break;
                  }
                }
                if (!flag)
                  throw new KernelExceptionID(sc_12743.ssp_appserver_12764(1571872432), (object) attributeTypeList[index5].Name);
                stringBuilder.AppendFormat("F{0}{1}{2} {3} {4},", (object) attributeTypeList[index5].AttributeID, (object) ch2, (object) ch1, (object) sortOrders, (object) this.DBManager.DataProvider.NullsOrder);
                if (this._SortOrder != null)
                  this._SortOrder += $"F{attributeTypeList[index5].AttributeID}{ch2}{ch1} {sortOrders},";
              }
            }
            --stringBuilder.Length;
            if (this._SortOrder != null)
              this._SortOrder = this._SortOrder.Substring(0, this._SortOrder.Length - 1);
          }
          commandText = $"{columnsSql} {whereSql}{stringBuilder.ToString()}";
        }
        if (num2 > 0 && num2 < 2147483646)
        {
          if (this.DBManager.DataProvider.GetTopRecordsInWhere(num2 + 1) != string.Empty)
            commandText += this.DBManager.DataProvider.GetTopRecordsInWhere(num2 + 1);
          commandText += this.DBManager.DataProvider.GetFetchSQL(num2 + 1);
        }
        if (this.optimStat != null)
        {
          for (int index = 0; index < columnsCollection.Length; ++index)
            this.optimStat.Records.Add((object) (paramSet.ColumnsInfo != null ? this.GetOptimizationValue(columnsCollection[index].AttributeID, RequestOperations.Read, paramSet.ColumnsInfo[index].AttributeSource) : this.GetOptimizationValue(columnsCollection[index].AttributeID, RequestOperations.Read, AttributeSourceTypes.Auto)));
          if (paramSet.Conditions != null)
          {
            for (int index = 0; index < paramSet.Conditions.Length; ++index)
            {
              if (paramSet.Conditions[index].Attribute is int)
              {
                int attribute = (int) paramSet.Conditions[index].Attribute;
                if (attribute != 0 && paramSet.Conditions[index].RelationalOperator != RelationalOperators.ConsistFrom && paramSet.Conditions[index].RelationalOperator != RelationalOperators.ConsistFromType && paramSet.Conditions[index].RelationalOperator != RelationalOperators.EntersIn && paramSet.Conditions[index].RelationalOperator != RelationalOperators.EntersInType && paramSet.Conditions[index].RelationalOperator != RelationalOperators.NOP && paramSet.Conditions[index].RelationalOperator != RelationalOperators.None && paramSet.Conditions[index].RelationalOperator != RelationalOperators.ObjectTypeFilter)
                  this.optimStat.Records.Add((object) this.GetOptimizationValue(attribute, RequestOperations.Seek, paramSet.Conditions[index].AttributeSource));
              }
            }
          }
          for (int index = 0; index < attributeTypeList.Length; ++index)
            this.optimStat.Records.Add((object) (paramSet.SortSources != null ? this.GetOptimizationValue(attributeTypeList[index].AttributeID, RequestOperations.Read, paramSet.SortSources[index]) : this.GetOptimizationValue(attributeTypeList[index].AttributeID, RequestOperations.Read, AttributeSourceTypes.Auto)));
          this.optimStat.StartOperation();
        }
        bool flag1 = false;
        if (this.DBManager.DataProvider.Name == "Oracle" && paramSet.Conditions != null)
        {
          for (int index = 0; index < paramSet.Conditions.Length; ++index)
          {
            if (paramSet.Conditions[index].Attribute is int && (int) paramSet.Conditions[index].Attribute != 0 && (paramSet.Conditions[index].RelationalOperator == RelationalOperators.Less || paramSet.Conditions[index].RelationalOperator == RelationalOperators.LessOrEqual || paramSet.Conditions[index].RelationalOperator == RelationalOperators.Greater || paramSet.Conditions[index].RelationalOperator == RelationalOperators.GreaterOrEqual) && paramSet.Conditions[index].Value is string)
            {
              flag1 = true;
              this.DBManager.ExecuteNonQuery("ALTER SESSION SET NLS_COMP = ANSI");
              break;
            }
          }
        }
        DataSet dataSet = this.DBManager.ExecuteDataSet((DataSet) null, 0, num2 + 1, paramSet.TableName, commandText, this.UserSession.QueryBuilder.PreparedParams.ToArray());
        if (flag1)
          this.DBManager.ExecuteNonQuery("ALTER SESSION SET NLS_COMP = BINARY");
        if (dataSet != null)
          dataSet.RemotingFormat = SerializationFormat.Binary;
        if (this.optimStat != null)
          this.optimStat.SaveToCache();
        source = dataSet.Tables[0];
        if (this.UserSession.QueryBuilder.WordsIDs != string.Empty)
          source.Columns.Remove("F_TF_IDF");
        dataSet.Tables.RemoveAt(0);
        if (this._MustTurnAllModificationsModeOFF)
          this._ShowAllModifications = false;
        for (; num1 > 0; --num1)
          source.Columns.RemoveAt(source.Columns.Count - 1);
        if (source.Rows.Count > num2)
        {
          source.ExtendedProperties[(object) "Eof"] = (object) false;
          source.Rows.RemoveAt(source.Rows.Count - 1);
        }
        else
          source.ExtendedProperties[(object) "Eof"] = (object) true;
        this.RenameColumns(source, paramSet, columnsCollection);
      }
      if (this.UserSession.QueryBuilder.NeedDeleteTemp)
      {
        if (this.DBManager.DataProvider.Name == "Sql" || this.DBManager.DataProvider.Name == "PostgreSQL")
          this.DBManager.ExecuteNonQuery($"DELETE FROM {this.UserSession.QueryBuilder.TemporaryTableName} WHERE F_KEY = :prim_keyID", this.DBManager.Parameter("prim_keyID", (object) this.UserSession.QueryBuilder.SelectKeyValue));
        this.UserSession.QueryBuilder.NeedDeleteTemp = false;
      }
      if (this.UserSession.QueryBuilder.NeedCommitTransaction)
      {
        this.UserSession.QueryBuilder.NeedCommitTransaction = false;
        this.UserSession.Commit();
      }
    }
    catch
    {
      if (this.UserSession.QueryBuilder.NeedCommitTransaction)
      {
        this.UserSession.QueryBuilder.NeedCommitTransaction = false;
        this.UserSession.Rollback();
      }
      throw;
    }
    int statusesColumnIdx = DBRecordSet.GetElementsStatusesColumnIdx(ref paramSet);
    ElementStatusesService.PrepareElementStatusesColumn(ref source, statusesColumnIdx);
    (this.EventHelper as EventLogHelper).OnGetRecordsList(source, (object) this, paramSet, (IUserSession) this.UserSession);
    if (source != null)
      source.RemotingFormat = SerializationFormat.Binary;
    return source;
  }

  protected bool ExistsMode
  {
    [DebuggerStepThrough] get => this._ExistsMode;
  }

  public bool RecordsExists(ConditionStructure[] conditions, HybridDictionary tags)
  {
    DBRecordSetParams paramSet = new DBRecordSetParams(conditions, new object[1]
    {
      (object) this._DBKeyFieldID
    });
    paramSet.Tags = tags;
    paramSet.RecordCount = 1;
    this._ExistsMode = true;
    DataTable dataTable;
    try
    {
      dataTable = this.Select(paramSet);
    }
    finally
    {
      this._ExistsMode = false;
    }
    return dataTable.Rows.Count > 0;
  }

  public bool RecordsExists(ConditionStructure[] conditions)
  {
    return this.RecordsExists(conditions, (HybridDictionary) null);
  }

  private OptimizationValue GetOptimizationValue(
    int attributeID,
    RequestOperations operation,
    AttributeSourceTypes attributeSource)
  {
    OptimizationValue optimizationValue = new OptimizationValue(attributeID, -1, -1, operation);
    switch (attributeSource)
    {
      case AttributeSourceTypes.Auto:
        if (this is DBObjectCollection && this._RecordsTypeID > -1)
          optimizationValue.ObjectTypeID = this._RecordsTypeID;
        if (this is DBRelationCollection && this._RecordsTypeID > -1)
        {
          optimizationValue.RelationTypeID = this._RecordsTypeID;
          break;
        }
        break;
      case AttributeSourceTypes.Object:
        if (this is DBObjectCollection && this._RecordsTypeID > -1)
          optimizationValue.ObjectTypeID = this._RecordsTypeID;
        if (this is DBRelationCollection && (this as DBRelationCollection)._ObjectTypeID > -1)
        {
          optimizationValue.ObjectTypeID = (this as DBRelationCollection)._ObjectTypeID;
          break;
        }
        break;
      case AttributeSourceTypes.Relation:
        optimizationValue.RelationTypeID = this._RecordsTypeID;
        break;
    }
    return optimizationValue;
  }

  protected virtual void RenameColumns(
    DataTable tbl,
    DBRecordSetParams paramSet,
    IDBAttributeType[] columnsList)
  {
    DBRecordSet.StaticRenameColumns(tbl, paramSet, columnsList);
  }

  public static void StaticRenameColumns(
    DataTable tbl,
    DBRecordSetParams paramSet,
    IDBAttributeType[] columnsList)
  {
    for (int index = 0; index < tbl.Columns.Count; ++index)
    {
      DataColumn column = tbl.Columns[index];
      if (column.DataType == typeof (DateTime))
        column.DateTimeMode = DataSetDateTime.Unspecified;
      ColumnContents content = paramSet.Contents == null || paramSet.Contents.Length <= index ? ColumnContents.Text : paramSet.Contents[index];
      if (paramSet.ColumnNames == null)
      {
        column.ColumnName = DBRecordSet.StaticGetDefaultColumnName(columnsList[index], content);
      }
      else
      {
        switch (paramSet.ColumnNames[index])
        {
          case ColumnNameMapping.Default:
            column.ColumnName = DBRecordSet.StaticGetDefaultColumnName(columnsList[index], content);
            continue;
          case ColumnNameMapping.ID:
            column.ColumnName = columnsList[index].AttributeID.ToString();
            continue;
          case ColumnNameMapping.Guid:
            column.ColumnName = (columnsList[index] as IDBGuid).GUID.ToString();
            continue;
          case ColumnNameMapping.Alias:
            column.ColumnName = columnsList[index].Alias;
            continue;
          case ColumnNameMapping.ShortName:
            column.ColumnName = columnsList[index].ShortName;
            continue;
          case ColumnNameMapping.Name:
            column.ColumnName = columnsList[index].Name;
            continue;
          case ColumnNameMapping.FieldName:
            if (columnsList[index].AttributeID > 0)
              throw new KernelExceptionID(sc_12743.ssp_appserver_12765(1891686556), (object) columnsList[index].Name);
            column.ColumnName = columnsList[index].ValueFieldName;
            continue;
          case ColumnNameMapping.Index:
            column.ColumnName = index.ToString();
            continue;
          default:
            continue;
        }
      }
    }
  }

  protected virtual string GetDefaultColumnName(
    IDBAttributeType attribyteType,
    ColumnContents content)
  {
    return DBRecordSet.StaticGetDefaultColumnName(attribyteType, content);
  }

  public static string StaticGetDefaultColumnName(
    IDBAttributeType attribyteType,
    ColumnContents content)
  {
    string name = attribyteType.Name;
    switch (content)
    {
      case ColumnContents.ID:
        name += LocalizationHolder.rm.GetString("Kernel_252");
        break;
      case ColumnContents.Date:
        name += LocalizationHolder.rm.GetString("Kernel_253");
        break;
      case ColumnContents.Value:
        if (attribyteType.AttributeType == FieldTypes.ftFile || attribyteType.AttributeType == FieldTypes.ftBlob)
        {
          name += LocalizationHolder.rm.GetString("Kernel_254");
          break;
        }
        if (attribyteType.AttributeType == FieldTypes.ftMeasured)
        {
          name += LocalizationHolder.rm.GetString("Kernel_255");
          break;
        }
        break;
    }
    return name;
  }

  public static string StaticGetDefaultColumnName(
    IMSAttributeType attribyteType,
    ColumnContents content)
  {
    string name = attribyteType.Name;
    switch (content)
    {
      case ColumnContents.ID:
        name += LocalizationHolder.rm.GetString("Kernel_252");
        break;
      case ColumnContents.Date:
        name += LocalizationHolder.rm.GetString("Kernel_253");
        break;
      case ColumnContents.Value:
        if (attribyteType.RealFieldType == FieldTypes.ftFile || attribyteType.RealFieldType == FieldTypes.ftBlob)
        {
          name += LocalizationHolder.rm.GetString("Kernel_254");
          break;
        }
        if (attribyteType.RealFieldType == FieldTypes.ftMeasured)
        {
          name += LocalizationHolder.rm.GetString("Kernel_255");
          break;
        }
        break;
    }
    return name;
  }

  public static string StaticGetColumnName(
    int columnIndex,
    int attributeID,
    ColumnContents contents,
    ColumnNameMapping columNames)
  {
    string columnName = string.Empty;
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attributeID);
    if (attributeType == null)
      return columnName;
    switch (columNames)
    {
      case ColumnNameMapping.Default:
        columnName = DBRecordSet.StaticGetDefaultColumnName(attributeType, contents);
        break;
      case ColumnNameMapping.ID:
        columnName = attributeType.AttributeID.ToString();
        break;
      case ColumnNameMapping.Guid:
        columnName = attributeType.AttributeGuid.ToString();
        break;
      case ColumnNameMapping.Alias:
        columnName = attributeType.Alias;
        break;
      case ColumnNameMapping.ShortName:
        columnName = attributeType.ShortName;
        break;
      case ColumnNameMapping.Name:
        columnName = attributeType.Name;
        break;
      case ColumnNameMapping.FieldName:
        columnName = attributeType.AttributeID <= 0 ? attributeType.ValueFieldName : throw new KernelExceptionID(sc_12743.ssp_appserver_12766(473290459), (object) attributeType.Name);
        break;
      case ColumnNameMapping.Index:
        columnName = columnIndex.ToString();
        break;
    }
    return columnName;
  }

  protected void ValidateLocalTypeAttribute(object attributeID)
  {
    if (attributeID == null)
      return;
    if (!(attributeID is int anAttributeType))
      anAttributeType = (ServerServices.GetService(typeof (IEventLogHelper)) as EventLogHelper).GetAttributeID(attributeID, true);
    if (anAttributeType > 0)
      throw new KernelExceptionID(358, (object) this.UserSession.GetAttributeType(anAttributeType).Name);
  }

  public virtual int Delete(long[] idList, bool throwException, long deleteMode)
  {
    int num = 0;
    if (throwException)
      this.UserSession.StartTransaction();
    try
    {
      bool flag;
      if (this.UserSession.IsStartedLogHistory)
      {
        flag = false;
      }
      else
      {
        this.UserSession.StartLogHistory();
        flag = true;
      }
      try
      {
        for (int index = 0; index < idList.Length; ++index)
        {
          object element = this.GetElement(idList[index]);
          if (!(element is IDeletable))
            throw new KernelException(sc_12743.ssp_appserver_12767());
          (element as IDeletable).Delete(deleteMode);
          ++num;
        }
        if (throwException)
          this.UserSession.Commit();
      }
      finally
      {
        if (flag)
          this.UserSession.StopLogHistory();
      }
    }
    catch
    {
      if (throwException)
      {
        this.UserSession.Rollback();
        throw;
      }
    }
    return num;
  }

  protected virtual object GetElement(long id) => (object) null;

  public CheckAttributeValueResult[] CheckAttributesValues(
    Dictionary<long, AttributeValues[]> ckeckedValues)
  {
    List<CheckAttributeValueResult> attributeValueResultList = new List<CheckAttributeValueResult>();
    foreach (KeyValuePair<long, AttributeValues[]> ckeckedValue in ckeckedValues)
    {
      DBAttributable element = this.GetElement(ckeckedValue.Key) as DBAttributable;
      foreach (AttributeValues attributeValues in ckeckedValue.Value)
      {
        IDBAttributeType attributeType = element.GetAttributeType(attributeValues.AttributeID);
        if (attributeType.Computed != ComputeValueModes.NotComputableValue)
          attributeValueResultList.Add(new CheckAttributeValueResult(ckeckedValue.Key, attributeValues.AttributeID, $"Атрибуту '{attributeType.Name}' нельзя присваивать значения, т.к. он является вычисляемым."));
        else if ((attributeType.Options & AttributeOptions.DisableNulls) == AttributeOptions.DisableNulls && (attributeValues.Value == null || attributeValues.Value == DBNull.Value || attributeValues.Value.ToString().Trim() == string.Empty))
          attributeValueResultList.Add(new CheckAttributeValueResult(ckeckedValue.Key, attributeValues.AttributeID, $"Атрибуту '{attributeType.Name}' нельзя присваивать пустое значение."));
        else if (attributeType.UniqueMode != UniqueValueModes.NotUnique)
        {
          if (element is DBObject)
          {
            try
            {
              (((element as DBObject).GetAttributeByID(attributeValues.AttributeID) ?? element.Attributes.AddTemporaryAttribute(attributeValues.AttributeID, false, attributeValues.Values)) as DBAdditionalAttribute).CheckUniqueValue(attributeValues.Values, false);
            }
            catch (ObjectAlreadyExists ex)
            {
              IDBObject dbObject = this.UserSession.GetObject(ex.ObjectID);
              attributeValueResultList.Add(new CheckAttributeValueResult(ckeckedValue.Key, attributeValues.AttributeID, $"Атрибут '{attributeType.Name}' должен содержать уникальное значение. В базе данных найден объект '{dbObject.NameInMessages}' с этим значением атрибута.", (object) ex.ObjectID));
            }
          }
        }
      }
    }
    return attributeValueResultList.ToArray();
  }

  protected virtual ConditionStructure PrepareCondition(ConditionStructure condition)
  {
    if (condition.RelationalOperator == RelationalOperators.LocalObjectTypes)
    {
      this.LocalTypesMode = true;
      condition.RelationalOperator = RelationalOperators.NOP;
    }
    if (!(condition.Attribute is int))
      condition.Attribute = (object) (this.EventHelper as EventLogHelper).GetAttributeID(condition.Attribute, false);
    if (Convert.ToInt32(condition.Attribute) == -9 && condition.RelationalOperator == RelationalOperators.Equal && Convert.ToInt32(condition.Value) == this.UserSession.IdentHelper.DeletedID)
      this._TrashMode = true;
    if (condition.Value != null)
    {
      if (condition.Value is bool)
        condition.Value = !(bool) condition.Value ? (object) 0 : (object) 1;
      else if (condition.Attribute != null && condition.Value is ConditionGroupIDReplacer)
      {
        ConditionGroupIDReplacer conditionGroupIdReplacer = condition.Value as ConditionGroupIDReplacer;
        condition.Value = (object) conditionGroupIdReplacer.GroupID;
        this.UserSession.QueryBuilder.ConvertGroupIDToUsers(ref condition, conditionGroupIdReplacer.IncludeGroupsID);
      }
      else if (condition.Attribute != null && condition.Value is ConditionRankIDReplacer)
      {
        ConditionRankIDReplacer conditionRankIdReplacer = condition.Value as ConditionRankIDReplacer;
        condition.Value = (object) conditionRankIdReplacer.RankID;
        this.UserSession.QueryBuilder.ConvertRankIDToUsers(ref condition);
      }
      else if (condition.Value is ConditionWorkflowTemplate)
        this.ConvertWorkflowTemplateCondition(ref condition);
      else if (condition.Value is Enum)
        condition.Value = (object) (int) condition.Value;
      else if (condition.Value is Array)
      {
        Array array = (Array) condition.Value;
        if (array.Length > 0 && array.GetValue(0) is Enum)
        {
          object[] objArray = new object[array.Length];
          for (int index = 0; index < array.Length; ++index)
          {
            object obj = array.GetValue(index);
            objArray[index] = !(obj is Enum) ? obj : (object) (int) obj;
          }
          condition.Value = (object) objArray;
        }
      }
    }
    return condition;
  }

  private void ConvertWorkflowTemplateCondition(ref ConditionStructure condition)
  {
    ConditionWorkflowTemplate workflowTemplate = condition.Value as ConditionWorkflowTemplate;
    condition.RelationalOperator = RelationalOperators.EntersInType;
    condition.TypeID = (object) MetaDataHelper.GetRelationTypeID(SystemGUIDs.relationTypeAttachments);
    condition.Value = (object) workflowTemplate.ActivityTypeID;
    List<ConditionStructure> conditionStructureList = new List<ConditionStructure>(1);
    if (workflowTemplate.Conditions != null)
    {
      workflowTemplate.Conditions[workflowTemplate.Conditions.Length - 1].LogicalOperator = LogicalOperators.AND;
      for (int index = 0; index < workflowTemplate.Conditions.Length; ++index)
        conditionStructureList.Add(workflowTemplate.Conditions[index]);
    }
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID(SystemGUIDs.AttrParentActivityGuid);
    ConditionStructure conditionStructure = new ConditionStructure();
    conditionStructure.Attribute = (object) attributeTypeId;
    conditionStructure.Content = ColumnContents.ID;
    if (workflowTemplate.ActivitiesID != null)
    {
      if (workflowTemplate.ActivitiesID.Length == 1)
      {
        conditionStructure.RelationalOperator = RelationalOperators.Equal;
        conditionStructure.Value = (object) workflowTemplate.ActivitiesID[0];
      }
      else
      {
        conditionStructure.RelationalOperator = RelationalOperators.In;
        conditionStructure.Value = (object) workflowTemplate.ActivitiesID;
      }
    }
    else
    {
      List<long> longList;
      if (workflowTemplate.AllVersions)
      {
        longList = this.UserSession.GetAllObjectVersionsList(workflowTemplate.TemplateObjectID, false, false, false);
      }
      else
      {
        longList = new List<long>(1);
        longList.Add(workflowTemplate.TemplateObjectID);
      }
      string commandText;
      using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate())
      {
        StringBuilder stringBuilder = objectPoolScope.Object;
        for (int index = 0; index < longList.Count; ++index)
          stringBuilder.Append(longList[index].ToString() + ",");
        --stringBuilder.Length;
        commandText = $"SELECT F_OBJECT_ID FROM IMV_O{workflowTemplate.ActivityTypeID} WHERE F{MetaDataHelper.GetAttributeTypeID(SystemGUIDs.attributeProcessGuid)}ID IN ({stringBuilder.ToString()})";
      }
      DataTable dataTable = this.UserSession.DataManager.ExecuteDataTable(commandText);
      if (dataTable.Rows.Count == 0)
        throw new KernelException(string.Format(sc_12743.ssp_appserver_12768(), (object) this.UserSession.GetObjectType(workflowTemplate.ActivityTypeID).ObjectTypeName));
      if (dataTable.Rows.Count == 1)
      {
        conditionStructure.RelationalOperator = RelationalOperators.Equal;
        conditionStructure.Value = (object) Convert.ToInt64(dataTable.Rows[0][0]);
      }
      else
      {
        long[] numArray = new long[dataTable.Rows.Count];
        for (int index = 0; index < dataTable.Rows.Count; ++index)
          numArray[index] = Convert.ToInt64(dataTable.Rows[index][0]);
        conditionStructure.RelationalOperator = RelationalOperators.In;
        conditionStructure.Value = (object) numArray;
      }
    }
    conditionStructureList.Add(conditionStructure);
    condition.NestedConditions = conditionStructureList.ToArray();
  }

  public static void AttributeFindSortOrders(
    DBRecordSetParams selectParams,
    ref Dictionary<int, SortOrders> sortedAttrs)
  {
    if (sortedAttrs == null)
      sortedAttrs = new Dictionary<int, SortOrders>();
    sortedAttrs.Clear();
    if (selectParams.Columns == null || selectParams.ColumnNames == null || selectParams.SortColumns == null)
      return;
    List<SortOrders> sortOrdersList = new List<SortOrders>(selectParams.SortColumns.Length);
    if (selectParams.Orders == null)
    {
      for (int index = 0; index < selectParams.SortColumns.Length; ++index)
        sortOrdersList.Add(SortOrders.ASC);
    }
    else
    {
      for (int index = 0; index < selectParams.Orders.Length; ++index)
        sortOrdersList.Add(selectParams.Orders[index]);
    }
    if (selectParams.SortColumns.Length != sortOrdersList.Count)
      return;
    EventLogHelper service = ServerServices.GetService(typeof (IEventLogHelper)) as EventLogHelper;
    for (int index = 0; index < selectParams.SortColumns.Length; ++index)
    {
      int attributeId = service.GetAttributeID(selectParams.SortColumns[index], false);
      if (!sortedAttrs.ContainsKey(attributeId))
        sortedAttrs.Add(attributeId, sortOrdersList[index]);
    }
  }

  public static void AttributeFindColumns(
    DBRecordSetParams selectParams,
    DataTable table,
    object attrID,
    AttributeSourceTypes AttributeSource,
    ref Dictionary<object, List<int>> columnsAttrs)
  {
    if (columnsAttrs == null)
      columnsAttrs = new Dictionary<object, List<int>>();
    if (selectParams.Columns == null || selectParams.ColumnNames == null || columnsAttrs.ContainsKey(attrID))
      return;
    List<int> intList = new List<int>();
    columnsAttrs.Add(attrID, intList);
    EventLogHelper service = ServerServices.GetService(typeof (IEventLogHelper)) as EventLogHelper;
    int attributeId = service.GetAttributeID(attrID, false);
    if (attributeId == -1)
      return;
    for (int index = 0; index < selectParams.Columns.Length; ++index)
    {
      if (service.GetAttributeID(selectParams.Columns[index], false) == attributeId)
        intList.Add(index);
    }
  }

  public virtual int AttributeReplaceValue(
    DBRecordSetParams selectParams,
    object attrID,
    AttributeSourceTypes attributeSource,
    DataRow row,
    object newValue,
    ref Dictionary<object, List<int>> columnsAttrs)
  {
    if (attrID == null || row == null)
      return 0;
    DataTable table = row.Table;
    if (table == null || table.Columns.Count == 0 || table.Rows.Count == 0)
      return 0;
    DBRecordSet.AttributeFindColumns(selectParams, table, attrID, attributeSource, ref columnsAttrs);
    if (!columnsAttrs.ContainsKey(attrID))
      return 0;
    List<int> intList = columnsAttrs[attrID];
    for (int index = 0; index < intList.Count; ++index)
      row[intList[index]] = newValue;
    return intList.Count;
  }

  public static int AttributeColumnIndex(
    DBRecordSetParams pars,
    object AttributeID,
    AttributeSourceTypes AttributeSource)
  {
    return DBRecordSet.AttributeColumnIndex(pars, AttributeID, AttributeSource, (DataTable) null);
  }

  public static int AttributeColumnIndex(
    DBRecordSetParams pars,
    object AttributeID,
    AttributeSourceTypes AttributeSource,
    DataTable table)
  {
    if (pars.Columns == null || pars.Columns.Length == 0 || AttributeID == null)
      return -1;
    EventLogHelper service = ServerServices.GetService(typeof (IEventLogHelper)) as EventLogHelper;
    int attributeId1 = service.GetAttributeID(AttributeID, false);
    if (attributeId1 == -1)
      return -1;
    int num1 = table != null ? Math.Min(table.Columns.Count - pars.Columns.Length, 0) : 0;
    int num2 = table != null ? Math.Min(table.Columns.Count, pars.Columns.Length) : pars.Columns.Length;
    for (int index = 0; index < pars.Columns.Length; ++index)
    {
      object column = pars.Columns[index];
      int attributeId2 = service.GetAttributeID(column, false);
      if (attributeId2 != -1 && attributeId2 == attributeId1)
      {
        AttributeSourceTypes attributeSourceTypes = AttributeSourceTypes.Auto;
        if (pars.ColumnsInfo != null && pars.ColumnsInfo.Length > index)
          attributeSourceTypes = pars.ColumnsInfo[index].AttributeSource;
        if (attributeSourceTypes == AttributeSource)
        {
          int num3 = index;
          if (num3 >= num2)
            num3 += num1;
          return num3;
        }
      }
    }
    return -1;
  }

  public static bool AttributeColumnExists(
    DBRecordSetParams pars,
    object AttributeID,
    AttributeSourceTypes AttributeSource)
  {
    if (pars.Columns == null || pars.Columns.Length == 0 || AttributeID == null)
      return false;
    EventLogHelper service = ServerServices.GetService(typeof (IEventLogHelper)) as EventLogHelper;
    int attributeId1 = service.GetAttributeID(AttributeID, false);
    if (attributeId1 == -1)
      return false;
    for (int index = 0; index < pars.Columns.Length; ++index)
    {
      object column = pars.Columns[index];
      int attributeId2 = service.GetAttributeID(column, false);
      if (attributeId2 != -1 && attributeId2 == attributeId1)
      {
        AttributeSourceTypes attributeSourceTypes = AttributeSourceTypes.Auto;
        if (pars.ColumnsInfo != null && pars.ColumnsInfo.Length > index)
          attributeSourceTypes = pars.ColumnsInfo[index].AttributeSource;
        if (attributeSourceTypes == AttributeSource || attributeId2 < 0 && attributeSourceTypes == AttributeSourceTypes.Auto && ObligatoryObjectAttributesHelper.GetAttributeSourceType((ObligatoryObjectAttributes) attributeId2) == AttributeSource)
          return true;
      }
    }
    return false;
  }

  public static int AttributeColumnID(DataTable source, object attribute)
  {
    int num1 = -1;
    if (source == null || attribute == null)
      return num1;
    Type type = attribute.GetType();
    if (type == typeof (int))
    {
      int num2 = (int) attribute;
      return source.Columns.IndexOf(num2.ToString());
    }
    if (type == typeof (ObligatoryObjectAttributes))
    {
      ObligatoryObjectAttributes attr = (ObligatoryObjectAttributes) attribute;
      int num3 = source.Columns.IndexOf(attr.ToString());
      if (num3 >= 0)
        return num3;
      string caption = ObligatoryObjectAttributesHelper.GetCaption(attr);
      return source.Columns.IndexOf(caption);
    }
    if (type == typeof (string))
    {
      string columnName = (string) attribute;
      return source.Columns.IndexOf(columnName);
    }
    if (!(type == typeof (Guid)))
      return num1;
    string columnName1 = ((Guid) attribute).ToString();
    return source.Columns.IndexOf(columnName1);
  }

  public bool AttributeColumnExists(
    DBRecordSetParams pars,
    object AttributeID,
    ColumnNameMapping AttributeName,
    AttributeSourceTypes AttributeSource)
  {
    if (pars.Columns == null || pars.Columns.Length == 0 || AttributeID == null)
      return false;
    EventLogHelper service = ServerServices.GetService(typeof (IEventLogHelper)) as EventLogHelper;
    int attributeId1 = service.GetAttributeID(AttributeID, false);
    if (attributeId1 == -1)
      return false;
    for (int index = 0; index < pars.Columns.Length; ++index)
    {
      object column = pars.Columns[index];
      int attributeId2 = service.GetAttributeID(column, false);
      if (attributeId2 != -1 && attributeId2 == attributeId1)
      {
        ColumnNameMapping columnNameMapping = ColumnNameMapping.Default;
        if (pars.ColumnNames != null && pars.ColumnNames.Length > index)
          columnNameMapping = pars.ColumnNames[index];
        AttributeSourceTypes attributeSourceTypes = AttributeSourceTypes.Auto;
        if (pars.ColumnsInfo != null && pars.ColumnsInfo.Length > index)
          attributeSourceTypes = pars.ColumnsInfo[index].AttributeSource;
        if (attributeSourceTypes == AttributeSourceTypes.Auto)
          attributeSourceTypes = this.AutoAttributeSourceTypes;
        if (attributeSourceTypes != AttributeSource && columnNameMapping == AttributeName)
          throw new KernelExceptionID(sc_12743.ssp_appserver_12769(1822237830), (object) attributeId1, (object) AttributeName);
        if (attributeSourceTypes == AttributeSource && columnNameMapping == AttributeName)
          return true;
      }
    }
    return false;
  }

  public virtual long ActualDateObjectVersion(
    long objectID,
    VersionsRule rule,
    out ObjectFiltrationState state)
  {
    state = ObjectFiltrationState.fsInvalidRule;
    if (this.UserSession == null || rule == null || rule.ActualDate == DateTime.MinValue)
      return -1;
    List<IDbDataParameter> dbDataParameterList = new List<IDbDataParameter>();
    string commandText = string.Format("SELECT {0}.{1}, {0}.{4} FROM {0} WHERE ({0}.{2} IN (SELECT {0}.{2} FROM {0} WHERE {0}.{1} = :F_OBJECT_ID)) AND ({0}.{3} < :F_OBJ_CREATE)", (object) "IMS_OBJECTS", (object) "F_OBJECT_ID", (object) "F_ID", (object) "F_OBJ_CREATE", (object) "F_OBJECT_TYPE");
    dbDataParameterList.Add(this.DBManager.Parameter(":F_OBJECT_ID", (object) objectID));
    dbDataParameterList.Add(this.DBManager.Parameter(":F_OBJ_CREATE", (object) rule.ActualDate));
    DataTable dataTable = this.DBManager.ExecuteDataTable(commandText, dbDataParameterList.ToArray());
    if (dataTable == null)
      return -1;
    long result1 = -1;
    int result2 = -1;
    List<long> longList = new List<long>();
    List<int> intList = new List<int>();
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      object obj1 = dataTable.Rows[index][0];
      object obj2 = dataTable.Rows[index][1];
      if (obj1 != DBNull.Value && obj1 != null && long.TryParse(obj1.ToString(), out result1) && obj2 != DBNull.Value && obj2 != null && int.TryParse(obj2.ToString(), out result2))
      {
        longList.Add(result1);
        intList.Add(result2);
      }
    }
    if (longList.Count == 0)
      return -1;
    if (longList.Count == 1)
    {
      state = ObjectFiltrationState.fsCorrespondingSingle;
      return longList[0];
    }
    int objectType = intList[0];
    for (int index = 0; index < intList.Count; ++index)
    {
      if (objectType != intList[index])
      {
        objectType = -1;
        break;
      }
    }
    IDBObjectCollection objectCollection = this.UserSession.GetObjectCollection(objectType);
    if (objectCollection == null)
      return -1;
    ColumnDescriptor[] attrsColumns4Obj = rule.GetRuleAttrsColumns4Obj(0);
    object[] objArray = new object[0];
    SortOrders[] sortOrdersArray = new SortOrders[0];
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-2, RelationalOperators.In, (object) longList.ToArray(), LogicalOperators.NONE, 0, true)
    }, attrsColumns4Obj);
    DataTable ObjVersions;
    try
    {
      ObjVersions = objectCollection.Select(paramSet);
    }
    catch
    {
      ObjVersions = (DataTable) null;
    }
    if (ObjVersions == null)
      return -1;
    return rule.FiltrateVersions((IUserSession) this.UserSession, new Tuple<long, RequiredModes>(-1L, RequiredModes.Auto), ref ObjVersions, out state, (IServiceProvider) null);
  }

  public virtual ObjectFiltrationState GetObjectVersionFiltrationState(
    long objectID,
    VersionsRule rule)
  {
    if (this.UserSession == null || rule == null)
      return ObjectFiltrationState.fsVersionNotFound;
    List<IDbDataParameter> dbDataParameterList = new List<IDbDataParameter>();
    string commandText = string.Format("SELECT {0}.{1}, {0}.{3}, {0}.{2} FROM {0} WHERE ({0}.{2} IN (SELECT {0}.{2} FROM {0} WHERE {0}.{1} = :F_OBJECT_ID))", (object) "IMS_OBJECTS", (object) "F_OBJECT_ID", (object) "F_ID", (object) "F_OBJECT_TYPE");
    dbDataParameterList.Add(this.DBManager.Parameter(":F_OBJECT_ID", (object) objectID));
    DataTable dataTable = this.DBManager.ExecuteDataTable(commandText, dbDataParameterList.ToArray());
    if (dataTable == null)
      return ObjectFiltrationState.fsVersionNotFound;
    long fID = -1;
    List<long> longList = new List<long>();
    List<int> intList = new List<int>();
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      long int64Value = DataSetProcessor.GetInt64Value(dataTable.Rows[index], 0, 0L);
      int int32Value = DataSetProcessor.GetInt32Value(dataTable.Rows[index], 1, -1);
      fID = DataSetProcessor.GetInt64Value(dataTable.Rows[index], 2, 0L);
      if (!longList.Contains(int64Value) && !longList.Contains(-int64Value) && (Math.Abs(objectID) != Math.Abs(int64Value) || (objectID >= 0L || int64Value <= 0L) && (objectID < 0L || int64Value >= 0L)))
      {
        longList.Add(int64Value);
        intList.Add(int32Value);
      }
    }
    if (longList.Count == 0)
      return ObjectFiltrationState.fsVersionNotFound;
    if (longList.Count == 1)
      return ObjectFiltrationState.fsCorrespondingSingle;
    int objectType = intList[0];
    for (int index = 0; index < intList.Count; ++index)
    {
      if (objectType != intList[index])
      {
        objectType = -1;
        break;
      }
    }
    IDBObjectCollection objectCollection = this.UserSession.GetObjectCollection(objectType);
    if (objectCollection == null)
      return ObjectFiltrationState.fsVersionNotFound;
    ColumnDescriptor[] attrsColumns4Obj = rule.GetRuleAttrsColumns4Obj(0);
    object[] objArray = new object[0];
    SortOrders[] sortOrdersArray = new SortOrders[0];
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-2, RelationalOperators.In, (object) longList.ToArray(), LogicalOperators.NONE, 0, true)
    }, attrsColumns4Obj);
    DataTable ObjVersions;
    try
    {
      objectCollection.ShowAllModifications = true;
      ObjVersions = objectCollection.Select(paramSet);
    }
    catch
    {
      ObjVersions = (DataTable) null;
    }
    if (ObjVersions == null)
      return ObjectFiltrationState.fsVersionNotFound;
    return rule.GetObjectVersionState((IUserSession) this.UserSession, objectID, fID, ref ObjVersions);
  }

  public void CorrectCompositionLCStepsAndLevels(
    DBRecordSetParams selectParams,
    DataTable composition,
    VersionsRule rule)
  {
    if (composition == null || composition.Rows.Count == 0 || composition.Columns.Count == 0 || rule == null || rule.ActualDate == DateTime.MinValue)
      return;
    int num1 = DBRecordSet.AttributeColumnIndex(selectParams, (object) ObligatoryObjectAttributes.F_LEVEL_ID, AttributeSourceTypes.Object);
    int num2 = DBRecordSet.AttributeColumnIndex(selectParams, (object) ObligatoryObjectAttributes.F_LC_STEP, AttributeSourceTypes.Object);
    int columnIndex1 = DBRecordSet.AttributeColumnIndex(selectParams, (object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object);
    int columnIndex2 = DBRecordSet.AttributeColumnIndex(selectParams, (object) ObligatoryObjectAttributes.F_ID, AttributeSourceTypes.Object);
    int columnIndex3 = DBRecordSet.AttributeColumnIndex(selectParams, (object) ObligatoryObjectAttributes.F_BASE_VERSION, AttributeSourceTypes.Object);
    if (num1 == -1 && num2 == -1 || columnIndex1 == -1 || columnIndex2 == -1)
      return;
    DateTime actualDate = rule.ActualDate;
    List<object> attributeValue1 = rule.GetAttributeValue(-4, true);
    List<object> attributeValue2 = rule.GetAttributeValue(-9, true);
    if (attributeValue1.Count == 0 && attributeValue2.Count == 0)
      return;
    Dictionary<long, Dictionary<long, DataRow>> dictionary1 = new Dictionary<long, Dictionary<long, DataRow>>();
    StringBuilder stringBuilder1 = new StringBuilder();
    StringBuilder stringBuilder2 = new StringBuilder();
    List<IDbDataParameter> dbDataParameterList = new List<IDbDataParameter>();
    Dictionary<long, bool> dictionary2 = new Dictionary<long, bool>();
    if (DBRecordSet._cacheModifyDate != this.UserSession.DBCache.ModifyDate)
    {
      lock (DBRecordSet.steps2levels)
      {
        DataTable table = this.UserSession.DBCache.GetTable("IMS_LC_STEPS");
        if (table == null)
          return;
        for (int index = 0; index < table.Rows.Count; ++index)
        {
          DataRow row = table.Rows[index];
          int result1;
          int result2;
          if (int.TryParse(row["F_LC_STEP"].ToString(), out result1) && int.TryParse(row["F_LEVEL_ID"].ToString(), out result2) && !DBRecordSet.steps2levels.ContainsKey(result1))
            DBRecordSet.steps2levels.Add(result1, result2);
        }
        DBRecordSet._cacheModifyDate = this.UserSession.DBCache.ModifyDate;
      }
    }
    for (int index = 0; index < composition.Rows.Count; ++index)
    {
      DataRow row = composition.Rows[index];
      long result3 = -1;
      object obj1 = row[columnIndex2];
      if (obj1 != null && obj1 != DBNull.Value && long.TryParse(obj1.ToString(), out result3))
      {
        long result4 = -1;
        object obj2 = row[columnIndex1];
        if (obj2 != null && obj2 != DBNull.Value && long.TryParse(obj2.ToString(), out result4))
        {
          if (!dictionary1.ContainsKey(result3))
            dictionary1.Add(result3, new Dictionary<long, DataRow>());
          Dictionary<long, DataRow> dictionary3 = dictionary1[result3];
          if (!dictionary3.ContainsKey(result4))
            dictionary3.Add(result4, row);
        }
      }
    }
    if (dictionary1.Count == 0)
      return;
    foreach (KeyValuePair<long, Dictionary<long, DataRow>> keyValuePair in dictionary1)
    {
      stringBuilder1.Length = 0;
      dbDataParameterList.Clear();
      long[] array = new long[keyValuePair.Value.Keys.Count];
      keyValuePair.Value.Keys.CopyTo(array, 0);
      for (int index = 0; index < array.Length; ++index)
      {
        stringBuilder1.Append(index < array.Length - 1 ? $":F_OBJECT_ID_Par{index.ToString()}," : $":F_OBJECT_ID_Par{index.ToString()}");
        dbDataParameterList.Add(this.DBManager.Parameter($":F_OBJECT_ID_Par{index.ToString()}", (object) array[index]));
      }
      dbDataParameterList.Add(this.DBManager.Parameter(":F_ACTUAL_DATE_Par", (object) (rule.ActualDate.Date + new TimeSpan(0, 23, 59, 59, 999))));
      DataTable dataTable = this.DBManager.ExecuteDataTable(string.Format("SELECT {0}.{1}, {0}.{3} FROM {0} WHERE ({0}.{2} IN (SELECT MAX({0}.{2}) FROM {0} WHERE ({0}.{2} <= :F_ACTUAL_DATE_Par) AND ({0}.{1} IN ({4})) GROUP BY {0}.{1})) AND ({0}.{1} IN ({4}))", (object) "IMS_LCSTART_DATE", (object) "F_OBJECT_ID", (object) "F_START_DATE", (object) "F_LC_STEP", (object) stringBuilder1.ToString()), dbDataParameterList.ToArray());
      Dictionary<object, List<int>> columnsAttrs = new Dictionary<object, List<int>>();
      if (dataTable != null)
      {
        lock (DBRecordSet.steps2levels)
        {
          for (int index = 0; index < dataTable.Rows.Count; ++index)
          {
            DataRow row1 = dataTable.Rows[index];
            object obj3 = row1[0];
            long result5;
            if (obj3 != null && obj3 != DBNull.Value && long.TryParse(obj3.ToString(), out result5) && keyValuePair.Value.ContainsKey(result5))
            {
              dictionary2[result5] = true;
              object obj4 = row1[1];
              int result6;
              if (obj4 != null && obj4 != DBNull.Value && int.TryParse(obj4.ToString(), out result6))
              {
                int newValue = DBRecordSet.steps2levels.ContainsKey(result6) ? DBRecordSet.steps2levels[result6] : -1;
                DataRow row2 = keyValuePair.Value[result5];
                this.AttributeReplaceValue(selectParams, (object) ObligatoryObjectAttributes.F_LC_STEP, AttributeSourceTypes.Object, row2, (object) result6, ref columnsAttrs);
                this.AttributeReplaceValue(selectParams, (object) ObligatoryObjectAttributes.F_LEVEL_ID, AttributeSourceTypes.Object, row2, (object) newValue, ref columnsAttrs);
              }
            }
          }
        }
      }
    }
    bool flag1 = false;
    bool flag2 = rule.GetAdvancedCriterion().CompareFunction == "BASEVERSION";
    for (int index = composition.Rows.Count - 1; index >= 0; --index)
    {
      DataRow row = composition.Rows[index];
      long int64Value = DataSetProcessor.GetInt64Value(row, columnIndex1, 0L);
      bool flag3 = columnIndex3 >= 0 && DataSetProcessor.GetInt64Value(row, columnIndex3, 0L) == 1L;
      if (!dictionary2.ContainsKey(int64Value) && !(flag3 & flag2))
      {
        composition.Rows.RemoveAt(index);
        flag1 = true;
      }
    }
    if (!flag1)
      return;
    composition.AcceptChanges();
  }

  public int SetAttributesValues(
    long[] idList,
    AttributeValues[] valuesList,
    bool addIfNotExists,
    bool throwException)
  {
    int num = 0;
    if (throwException)
      this.UserSession.StartTransaction();
    try
    {
      for (int index1 = 0; index1 < idList.Length; ++index1)
      {
        if (!(this.GetElement(idList[index1]) is IDBAttributable element))
          throw new KernelException(sc_12743.ssp_appserver_12770());
        for (int index2 = 0; index2 < valuesList.Length; ++index2)
        {
          IDBAttribute attributeById = element.GetAttributeByID(valuesList[index2].AttributeID);
          if (attributeById != null)
            attributeById.Values = valuesList[index2].Values;
          else if (addIfNotExists)
            element.Attributes.AddAttribute(valuesList[index2].AttributeID, false, valuesList[index2].Values);
        }
        ++num;
      }
      if (throwException)
        this.UserSession.Commit();
    }
    catch
    {
      if (throwException)
      {
        this.UserSession.Rollback();
        throw;
      }
    }
    return num;
  }

  private CommandResult AddAttribute(
    long[] objectIDs,
    object attributeID,
    object[] values,
    bool ignoreExceptions,
    bool editMode)
  {
    CommandResult commandResult = new CommandResult((long[]) null);
    List<long> longList = new List<long>(objectIDs.Length);
    int attributeId = (this.EventHelper as EventLogHelper).GetAttributeID(attributeID);
    for (long index = 0; index < (long) objectIDs.Length; ++index)
    {
      IDBAttributable element = this.GetElement(objectIDs[index]) as IDBAttributable;
      try
      {
        if (editMode)
        {
          IDBAttribute attributeById = element.GetAttributeByID(attributeId);
          if (attributeById != null)
          {
            if (values == null)
              attributeById.Clear();
            else
              attributeById.Values = values;
            longList.Add(objectIDs[index]);
          }
        }
        else
        {
          if (values != null)
            element.Attributes.AddAttribute(attributeId, false, values);
          else
            element.Attributes.AddAttribute(attributeId, false);
          longList.Add(objectIDs[index]);
        }
      }
      catch (Exception ex)
      {
        commandResult.ErrorMessage = ex.Message;
        commandResult.ErrorObjectID = objectIDs[index];
        if (!ignoreExceptions)
          break;
      }
    }
    commandResult.ProcessedObjects = longList.ToArray();
    return commandResult;
  }

  public CommandResult AddAttribute(
    long[] objectIDs,
    object attributeID,
    object[] values,
    bool ignoreExceptions)
  {
    return this.AddAttribute(objectIDs, attributeID, values, ignoreExceptions, false);
  }

  public CommandResult EditAttribute(
    long[] objectIDs,
    object attributeID,
    object[] values,
    bool ignoreExceptions)
  {
    return this.AddAttribute(objectIDs, attributeID, values, ignoreExceptions, true);
  }

  public CommandResult DeleteAttribute(long[] objectIDs, object attributeID, bool ignoreExceptions)
  {
    CommandResult commandResult = new CommandResult((long[]) null);
    List<long> longList = new List<long>(objectIDs.Length);
    int attributeId = (this.EventHelper as EventLogHelper).GetAttributeID(attributeID);
    for (long index = 0; index < (long) objectIDs.Length; ++index)
    {
      IDBAttributable element = this.GetElement(objectIDs[index]) as IDBAttributable;
      try
      {
        IDBAttribute attributeById = element.GetAttributeByID(attributeId);
        if (attributeById != null)
        {
          attributeById.Delete(0L);
          longList.Add(objectIDs[index]);
        }
      }
      catch (Exception ex)
      {
        commandResult.ErrorMessage = ex.Message;
        commandResult.ErrorObjectID = objectIDs[index];
        if (!ignoreExceptions)
          break;
      }
    }
    commandResult.ProcessedObjects = longList.ToArray();
    return commandResult;
  }

  public DataTable GetAttributeValues(ICollection<long> idList, int attrID, bool allFields)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    IDBAttributeType attributeType = this.UserSession.GetAttributeType(attrID);
    (attributeType as IDBSecurity).CheckAccess(ActionType.List);
    string str = !allFields ? $"{this.DBKeyField}, F_ATTRIBUTE_ID, F_INLIST_ID, {attributeType.ValueFieldName}" : "*";
    StringBuilder stringBuilder = new StringBuilder();
    IDbDataParameter[] dbDataParameterArray = new IDbDataParameter[idList.Count];
    int index = 0;
    foreach (long id in (IEnumerable<long>) idList)
    {
      dbDataParameterArray[index] = dataManager.Parameter("recID" + index.ToString(), (object) id);
      stringBuilder.AppendFormat(":recID{0},", (object) index++);
    }
    --stringBuilder.Length;
    return dataManager.ExecuteDataTable($"SELECT {str} FROM {this.DBAttributesTableName} WHERE {this.DBKeyField} IN ({stringBuilder.ToString()}) AND F_ATTRIBUTE_ID = {attrID}", dbDataParameterArray);
  }

  public static DataTable CreateEmptyDataTable(string tableName, ColumnDescriptor[] columns)
  {
    DataTable emptyDataTable = new DataTable(tableName);
    if (columns == null || columns.Length == 0)
      return emptyDataTable;
    IEventLogHelper service = ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper;
    for (int columnIndex = 0; columnIndex < columns.Length; ++columnIndex)
    {
      int attributeId = service.GetAttributeID(columns[columnIndex].AttributeID, false);
      string columnName = DBRecordSet.StaticGetColumnName(columnIndex, attributeId, columns[columnIndex].Contents, columns[columnIndex].ColumnName);
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attributeId);
      if (attributeType == null)
      {
        emptyDataTable.Columns.Add(columnName, typeof (object));
      }
      else
      {
        Type type;
        if (attributeType.FieldType == FieldTypes.ftSystem)
        {
          type = AttributesTypeHelper.GetRDBMSTypeOfAttributeValue(attributeType.RealFieldType);
        }
        else
        {
          switch (columns[columnIndex].Contents)
          {
            case ColumnContents.Text:
              type = AttributesTypeHelper.IsComplexAttributeType(attributeType.RealFieldType) ? typeof (string) : AttributesTypeHelper.GetRDBMSTypeOfAttributeValue(attributeType.RealFieldType);
              break;
            case ColumnContents.ID:
              type = typeof (long);
              break;
            case ColumnContents.Date:
              type = typeof (DateTime);
              break;
            case ColumnContents.Value:
              type = typeof (double);
              break;
            case ColumnContents.String:
              type = typeof (string);
              break;
            default:
              type = typeof (string);
              break;
          }
        }
        DataColumn dataColumn = emptyDataTable.Columns.Add(columnName, type);
        if (dataColumn.DataType == typeof (DateTime))
          dataColumn.DateTimeMode = DataSetDateTime.Unspecified;
      }
    }
    emptyDataTable.AcceptChanges();
    return emptyDataTable;
  }

  public DataTable SelectWithDescriptions(DBRecordSetParams paramSet)
  {
    DataTable dataTable1 = this.Select(paramSet);
    DataTable dataTable2 = new DataTable();
    dataTable2.RemotingFormat = SerializationFormat.Binary;
    EventLogHelper service = ServerServices.GetService(typeof (IEventLogHelper)) as EventLogHelper;
    for (int index = 0; index < dataTable1.Columns.Count; ++index)
    {
      int attributeId = service.GetAttributeID(paramSet.Columns[index]);
      if (paramSet.Columns.Length > index)
      {
        if (attributeId == -8 || attributeId == -6 || attributeId == -14 || attributeId == -81 || attributeId == -4 || attributeId == -7 || attributeId == -9 || attributeId == -80 || attributeId == -23 || attributeId == -9)
          dataTable2.Columns.Add(dataTable1.Columns[index].ColumnName, typeof (ValueWithDescription));
        else
          dataTable2.Columns.Add(dataTable1.Columns[index].ColumnName, dataTable1.Columns[index].DataType);
        if (!(paramSet.Columns[index] is int))
          paramSet.Columns[index] = (object) attributeId;
      }
    }
    for (int index1 = 0; index1 < dataTable1.Rows.Count; ++index1)
    {
      DataRow row = dataTable2.NewRow();
      for (int index2 = 0; index2 < dataTable1.Columns.Count; ++index2)
      {
        object obj = dataTable1.Rows[index1][index2];
        string description = string.Empty;
        if (obj != null && obj != DBNull.Value)
        {
          switch ((int) paramSet.Columns[index2])
          {
            case -82:
            case -81:
            case -14:
            case -8:
            case -6:
              long int64 = Convert.ToInt64(obj);
              if (int64 > 0L)
              {
                description = this.UserSession.GetObjectInfo(int64).Caption;
                break;
              }
              break;
            case -80:
              description = this.UserSession.DBCache.GetAccessCaption(Convert.ToInt32(obj));
              break;
            case -23:
              description = MetaDataHelper.GetRelationTypeName(Convert.ToInt32(obj));
              break;
            case -9:
              description = MetaDataHelper.GetLCLevelName(Convert.ToInt32(obj));
              break;
            case -7:
              description = MetaDataHelper.GetObjectTypeName(Convert.ToInt32(obj));
              break;
            case -4:
              description = MetaDataHelper.GetLCStepName(Convert.ToInt32(obj));
              break;
          }
        }
        row[index2] = !(dataTable2.Columns[index2].DataType == typeof (ValueWithDescription)) ? obj : (object) new ValueWithDescription(obj, description);
      }
      dataTable2.Rows.Add(row);
    }
    return dataTable2;
  }

  private sealed class ObjectID_LevelStep : 
    ICloneable,
    IComparable,
    IComparable<DBRecordSet.ObjectID_LevelStep>
  {
    private long ObjectID;
    private int LCStep;
    private int LevelID;
    private DataRow Row;

    public ObjectID_LevelStep(long objectID, int lCStep, int levelID, DataRow row)
    {
      this.ObjectID = objectID;
      this.LCStep = lCStep;
      this.LevelID = levelID;
      this.Row = row;
    }

    public override bool Equals(object obj)
    {
      return !(obj is DBRecordSet.ObjectID_LevelStep objectIdLevelStep) ? base.Equals(obj) : this.ObjectID == objectIdLevelStep.ObjectID;
    }

    public override int GetHashCode()
    {
      return this.ObjectID.GetHashCode() ^ this.LCStep.GetHashCode() ^ this.LevelID.GetHashCode();
    }

    public object Clone()
    {
      return (object) new DBRecordSet.ObjectID_LevelStep(this.ObjectID, this.LCStep, this.LevelID, this.Row);
    }

    public int CompareTo(object obj) => this.CompareTo(obj as DBRecordSet.ObjectID_LevelStep);

    public int CompareTo(DBRecordSet.ObjectID_LevelStep other)
    {
      return other == null ? -1 : this.ObjectID.CompareTo(other.ObjectID);
    }
  }

  internal static class ObjectsVisibilityFiltration
  {
    internal static void Filtrate(
      UserSession session,
      DataTable table,
      DBRecordSet owner,
      int attrIdx,
      int attrOwnerIdx)
    {
      if (session == null || !session.EnabledVisibilityFiltration || table == null || table.Rows.Count == 0 || owner == null || attrIdx < 0 || attrIdx >= table.Columns.Count || attrOwnerIdx < 0 || attrOwnerIdx >= table.Columns.Count)
        return;
      long[] groupsListRecursive = session.DBSecurity.GetGroupsListRecursive();
      long ownerGroupId = session.IdentHelper.OwnerGroupID;
      long userId = session.UserID;
      List<int> intList = new List<int>();
      ObjectsVisibility settings = new ObjectsVisibility();
      for (int index = 0; index < table.Rows.Count; ++index)
      {
        DataRow row = table.Rows[index];
        object obj1 = row[attrIdx];
        if (obj1 != null && obj1 != DBNull.Value)
        {
          object obj2 = row[attrOwnerIdx];
          if (obj2 != null && obj2 != DBNull.Value)
          {
            settings.Assign((object) obj1.ToString());
            if (settings.Rights.Count != 0)
            {
              long result = 0;
              if (long.TryParse(obj2.ToString(), out result) && !DBRecordSet.ObjectsVisibilityFiltration.Visible(settings, ownerGroupId, result, userId, groupsListRecursive, session.IdentHelper.AllUsersGroupID))
                intList.Add(index);
            }
          }
        }
      }
      intList.Sort();
      for (int index = intList.Count - 1; index >= 0; --index)
        table.Rows.RemoveAt(intList[index]);
      if (intList.Count <= 0)
        return;
      table.AcceptChanges();
    }

    public static bool Visible(IUserSession session, ObjectsVisibility settings, long ownerID)
    {
      long[] groupsListRecursive = (session as UserSession).DBSecurity.GetGroupsListRecursive();
      long ownerGroupId = session.IdentHelper.OwnerGroupID;
      long userId = session.UserID;
      return DBRecordSet.ObjectsVisibilityFiltration.Visible(settings, ownerGroupId, ownerID, userId, groupsListRecursive, session.IdentHelper.AllUsersGroupID);
    }

    private static bool Visible(
      ObjectsVisibility settings,
      long ownerGroup,
      long ownerID,
      long userID,
      long[] userIDs,
      long allUsersGroupVersionID)
    {
      if (settings.Rights.ContainsKey(ownerGroup) && userID == ownerID)
        return !settings.Rights[ownerGroup].HasFlag((Enum) ObjectsVisibilityFlags.Hidden);
      return !((IEnumerable<long>) userIDs).Where<long>((System.Func<long, bool>) (o => o != allUsersGroupVersionID)).Any<long>((System.Func<long, bool>) (o => settings.IsHidden(o))) && ((IEnumerable<long>) userIDs).Any<long>((System.Func<long, bool>) (o => settings.IsVisible(o)));
    }
  }

  internal static class ObjectsStatuses
  {
    internal static void FillStatuses(
      UserSession session,
      DataTable table,
      DBRecordSet owner,
      int statusesColumn,
      int levelIDColumn)
    {
      if (session == null || table == null || table.Rows.Count == 0 || owner == null || statusesColumn < 0 || levelIDColumn < 0 || statusesColumn >= table.Columns.Count || levelIDColumn >= table.Columns.Count)
        return;
      IElementStatusesService service = ServerServices.GetService(typeof (IElementStatusesService)) as IElementStatusesService;
      List<IMSLifeCycleLevel> lcLevelsList = MetaDataHelper.GetLCLevelsList();
      lcLevelsList.Sort();
      for (int index = 0; index < table.Rows.Count; ++index)
      {
        DataRow row = table.Rows[index];
        long levelID = DataSetProcessor.GetInt64Value(row, levelIDColumn, 0L);
        if (levelID != 0L)
        {
          IMSLifeCycleLevel imsLifeCycleLevel = lcLevelsList.Find((Predicate<IMSLifeCycleLevel>) (item => (long) item.LevelID == levelID));
          int num = lcLevelsList.IndexOf(imsLifeCycleLevel);
          if (num >= 0)
          {
            short int16 = Convert.ToInt16(num + 1);
            service.SetElementStatuses32("{7074E0E4-B3AB-4B3E-AD56-050CD256AF10}", row[statusesColumn] as byte[], (int) int16);
          }
        }
      }
    }
  }
}
