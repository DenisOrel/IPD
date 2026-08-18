// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.SqlBuilder
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using Intermech.Kernel.Services;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace Intermech.Kernel;

internal class SqlBuilder : IQueryBuilder
{
  internal DBRecordSet CurrentRecordSet;
  private UserSession UserSession;
  private IDBAttributeTypeCollection attributeTypes;
  public List<IDbDataParameter> PreparedParams = new List<IDbDataParameter>();
  public string SystemTableAlias = "S";
  public string ObjectAttributesTable = "IMS_OBJECT_ATTRS";
  public string SystemTableName;
  public int OptimizedTypeID;
  public string ObjectAttributesAlias = "A";
  public string RelationAttributesAlias = "AR";
  public string TypeFilter = "";
  public Attribute4ID IDstruct = new Attribute4ID(-1);
  public Attribute4ID relationsIDstruct = new Attribute4ID(-1);
  public string RelationsTableName;
  public string RelationsTableAlias = "SR";
  public int RelationsTypeID;
  internal bool NeedDeleteTemp;
  internal bool NeedCommitTransaction;
  internal long SelectKeyValue;
  private int SelectConditionIndex = -1;
  internal string TemporaryTableName = string.Empty;
  private string EscapeString = string.Empty;
  public string WordsIDs;
  private int NestedParamsCount;
  private int NestedSubqueryCount;
  private static string[] _SQLWords = new string[15]
  {
    "/*",
    "*/",
    "DELETE",
    "UPDATE",
    "DROP",
    "ALTER",
    "TRUNCATE",
    "INSERT",
    "CREATE",
    "GRANT",
    "REVOKE",
    "DENY",
    "COMMIT",
    "ROLLBACK",
    "SAVEPOINT"
  };

  public void ConvertRankIDToUsers(ref ConditionStructure condition)
  {
    IUserSession userSession = this.UserSession.Clone(nameof (ConvertRankIDToUsers));
    try
    {
      DataTable dataTable = userSession.GetObjectCollection(this.UserSession.IdentHelper.UsersTypeID).Select(new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(new Guid("cad00142-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, condition.Value, LogicalOperators.NONE, 0)
      }, new object[1]{ (object) -2 }));
      if (dataTable.Rows.Count <= 0)
        return;
      List<long> longList = new List<long>(dataTable.Rows.Count);
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        longList.Add(Convert.ToInt64(row[0]));
      condition.Value = (object) longList.ToArray();
      if (condition.RelationalOperator == RelationalOperators.NotEqual)
        condition.RelationalOperator = RelationalOperators.NotIn;
      else
        condition.RelationalOperator = condition.RelationalOperator == RelationalOperators.Equal ? RelationalOperators.In : throw new KernelException("В функцию ConvertRankIDToUsers передали неверный оператор: " + condition.RelationalOperator.ToString());
    }
    finally
    {
      userSession.Logout(nameof (ConvertRankIDToUsers));
    }
  }

  public void ConvertGroupIDToUsers(ref ConditionStructure condition, bool includeGroups)
  {
    QuickObjectInfo objectInfo = this.UserSession.GetObjectInfo(Convert.ToInt64(condition.Value));
    if (MetaDataHelper.IsObjectTypeChildOf(objectInfo.ObjectTypeID, this.UserSession.IdentHelper.UsersTypeID))
      return;
    DBRelationCollection relationCollection = this.UserSession.GetRelationCollection(this.UserSession.IdentHelper.SimpleRelationTypeID) as DBRelationCollection;
    List<long> result = new List<long>(1);
    List<int> targetObjectTypeIDs;
    if (includeGroups)
      targetObjectTypeIDs = (List<int>) null;
    else
      targetObjectTypeIDs = new List<int>((IEnumerable<int>) new int[1]
      {
        this.UserSession.IdentHelper.UsersTypeID
      });
    if (includeGroups)
      result.Add(objectInfo.ObjectID);
    relationCollection.QuickConsistFrom(new long[1]
    {
      objectInfo.ObjectID
    }, targetObjectTypeIDs, result);
    if (result.Count <= 0)
      return;
    condition.Value = (object) result.ToArray();
    if (condition.RelationalOperator == RelationalOperators.NotEqual)
      condition.RelationalOperator = RelationalOperators.NotIn;
    else
      condition.RelationalOperator = condition.RelationalOperator == RelationalOperators.Equal ? RelationalOperators.In : throw new KernelException("В функцию ConvertGroupIDToUsers передали неверный оператор: " + condition.RelationalOperator.ToString());
  }

  internal long SelectKey
  {
    get
    {
      return this.UserSession.DataManager.DataProvider.NextGeneratorValue("IMS_TMP_GEN", this.UserSession.DataManager);
    }
  }

  private string GetAttributeSQLName(
    IDBAttributeType attr_t,
    ConditionStructure condition,
    int typeID)
  {
    if (attr_t.MultipleValued == MultiValueModes.MultiValues || attr_t.MultipleValued == MultiValueModes.MultiValuesFromList)
      throw new KernelExceptionID(sc_13066.ssp_appserver_13070(874876284), (object) attr_t.Name);
    Attribute4ID attrStruct;
    string str1;
    string str2;
    string str3;
    string str4;
    if (condition.AttributeSource == AttributeSourceTypes.Relation)
    {
      attrStruct = this.relationsIDstruct;
      if (typeID != -1)
      {
        attrStruct.RelationTypeID = typeID;
        str1 = (this.UserSession.GetRelationType(typeID) as DBRelationType).ViewName;
      }
      else
        str1 = this.RelationsTableAlias;
      str2 = "F_PRJLINK_ID";
      str3 = this.RelationAttributesAlias;
      str4 = "IMS_RELATION_ATTRS";
    }
    else
    {
      attrStruct = this.IDstruct;
      if (typeID != -1)
      {
        attrStruct.ObjectTypeID = typeID;
        str1 = (this.UserSession.GetObjectType(typeID) as DBObjectType).ViewName;
      }
      else
        str1 = this.SystemTableAlias;
      str2 = "F_OBJECT_ID";
      str3 = this.ObjectAttributesAlias;
      str4 = this.ObjectAttributesTable;
    }
    attrStruct.AttributeID = attr_t.AttributeID;
    OptimizationModes optimizationMode = this.UserSession.DBCache.GetOptimizationMode(attrStruct);
    string str5 = attr_t.ValueFieldName;
    bool flag = true;
    string attributeSqlName;
    if (attr_t.AttributeID > 0)
    {
      if (optimizationMode == OptimizationModes.Read || optimizationMode == OptimizationModes.Seek)
      {
        switch (condition.Content)
        {
          case ColumnContents.Text:
            str5 = "F" + attr_t.AttributeID.ToString();
            break;
          case ColumnContents.ID:
            str5 = $"F{attr_t.AttributeID}ID";
            break;
          case ColumnContents.Date:
            str5 = $"F{attr_t.AttributeID}ID3";
            break;
          case ColumnContents.Value:
            str5 = $"F{attr_t.AttributeID}ID2";
            break;
          case ColumnContents.String:
            str5 = "F" + attr_t.AttributeID.ToString();
            break;
        }
      }
      else
        flag = false;
      if (!flag)
      {
        switch (condition.Content)
        {
          case ColumnContents.Text:
            str5 = attr_t.TextFieldName;
            break;
          case ColumnContents.ID:
            str5 = "F_INTEGER_VALUE";
            break;
          case ColumnContents.Date:
            str5 = "F_DATE_VALUE";
            break;
          case ColumnContents.Value:
            str5 = "F_DOUBLE_VALUE";
            break;
          case ColumnContents.String:
            str5 = "F_STRING_VALUE";
            break;
        }
        attributeSqlName = string.Format("(SELECT {3}.{0} FROM {2} {3} WHERE {3}.F_ATTRIBUTE_ID = {4} AND {3}.{5} = {1}.{5} AND {3}.F_INLIST_ID = 0)", (object) str5, (object) str1, (object) str4, (object) str3, (object) attr_t.AttributeID, (object) str2);
      }
      else
        attributeSqlName = $"{str1}.{str5}";
    }
    else
      attributeSqlName = $"{str1}.{str5}";
    return attributeSqlName;
  }

  private string PrepareConditionFormula(ref ConditionStructure cond)
  {
    ConditionFormula conditionFormula = cond.Value as ConditionFormula;
    if (conditionFormula.Formula == null || conditionFormula.Formula == string.Empty)
      throw new KernelException(sc_13066.ssp_appserver_13071());
    StringBuilder stringBuilder1 = new StringBuilder();
    if (cond.Attribute != null)
    {
      int attributeId = this.UserSession.EventLogHelper.GetAttributeID(cond.Attribute);
      if (attributeId == 0)
      {
        cond.Attribute = (object) null;
      }
      else
      {
        DBAttributeType attributeType = this.attributeTypes.GetAttributeType((object) attributeId, true) as DBAttributeType;
        attributeType.ValidateRelationalOperator(cond.RelationalOperator, false, cond.Content);
        stringBuilder1.Append(this.GetAttributeSQLName((IDBAttributeType) attributeType, cond, -1) + " ");
        stringBuilder1.AppendFormat(RelationalOperatorsHelper.SQLOperator(cond.RelationalOperator), (object) string.Empty);
      }
    }
    bool flag = false;
    StringBuilder stringBuilder2 = new StringBuilder();
    string str1 = conditionFormula.Formula.Replace("SystemTableAlias", this.SystemTableAlias);
    int result1 = -1;
    for (int index = 0; index < str1.Length; ++index)
    {
      if (flag)
      {
        if (str1[index] == ':')
        {
          string str2 = stringBuilder2.ToString();
          Guid result2;
          if (Guid.TryParse(str2, out result2))
          {
            result1 = cond.AttributeSource != AttributeSourceTypes.Relation ? this.UserSession.GetObjectType(result2, true).ObjectType : this.UserSession.GetRelationType(result2, true).RelationType;
          }
          else
          {
            if (!int.TryParse(str2, out result1))
              throw new KernelException($"Неизвестный идентификатор типа объектов или связей '{stringBuilder2}' в строке '{str1}'.");
            if (cond.AttributeSource == AttributeSourceTypes.Relation)
              this.UserSession.GetRelationType(result1, true);
            else
              this.UserSession.GetObjectType(result1, true);
          }
          stringBuilder2.Clear();
        }
        else if (str1[index] == ']')
        {
          string str3 = stringBuilder2.ToString();
          Guid result3;
          if (Guid.TryParse(str3, out result3))
            stringBuilder1.Append(this.GetAttributeSQLName(this.UserSession.GetAttributeType(result3), cond, result1));
          else
            stringBuilder1.Append(this.GetAttributeSQLName(this.UserSession.GetAttributeType(str3), cond, result1));
          flag = false;
          stringBuilder2.Clear();
          result1 = -1;
        }
        else
          stringBuilder2.Append(str1[index]);
      }
      else if (str1[index] == '[')
        flag = true;
      else
        stringBuilder1.Append(str1[index]);
    }
    if (flag)
      throw new KernelExceptionID(sc_13066.ssp_appserver_13072(252104101), (object) str1);
    if (conditionFormula.Values != null)
    {
      for (int index = 0; index < conditionFormula.Values.Length; ++index)
        this.PreparedParams.Add(this.UserSession.DataManager.Parameter(conditionFormula.Values[index].ParamName, conditionFormula.Values[index].ParamValue));
    }
    return stringBuilder1.ToString();
  }

  private void ValidateSQLinjection(string p)
  {
    for (int index = 0; index < SqlBuilder._SQLWords.Length; ++index)
    {
      if (p.ToUpper().IndexOf(SqlBuilder._SQLWords[index]) >= 0)
      {
        string message = string.Format(sc_13066.ssp_appserver_13073(), (object) SqlBuilder._SQLWords[index], (object) p);
        this.UserSession.EventLogHelper.AddToTrace($"{message}     Пользователь: {this.UserSession.UserName}", Consts.traceAlways, string.Empty);
        throw new KernelException(message);
      }
    }
  }

  private object ReplaceLIKESymbols(object val, IDbManager db)
  {
    if (!(val is string))
      return val;
    string str = val.ToString();
    char[] likeSymbols = db.DataProvider.LIKE_Symbols;
    if (str.IndexOfAny(likeSymbols) > -1)
    {
      if (db.DataProvider.Name == "Sql")
      {
        for (int index = 0; index < likeSymbols.Length; ++index)
          str = str.Replace(likeSymbols[index].ToString(), $"[{likeSymbols[index].ToString()}]");
      }
      else if (db.DataProvider.Name == "Oracle" || db.DataProvider.Name == "Linter" || db.DataProvider.Name == "PostgreSQL")
      {
        this.EscapeString = str.IndexOf('/') <= -1 ? "/" : "|";
        for (int index = 0; index < likeSymbols.Length; ++index)
          str = str.Replace(likeSymbols[index].ToString(), this.EscapeString + likeSymbols[index].ToString());
        this.EscapeString = $"ESCAPE N'{this.EscapeString}'";
      }
    }
    return (object) str;
  }

  internal bool RebuildTypedView(
    string viewName,
    DataTable attributes,
    AttributeSourceTypes sourceType,
    IDbManager db,
    bool isLocalType,
    bool needDrop,
    bool createIndexes,
    List<string> indexes)
  {
    StringBuilder stringBuilder = new StringBuilder();
    foreach (DataRow row in (InternalDataCollectionBase) attributes.Rows)
    {
      OptimizationModes int32 = (OptimizationModes) Convert.ToInt32(row["F_INVIEW"]);
      if (int32 != OptimizationModes.Write)
      {
        DBAttributeType attributeType = this.UserSession.GetAttributeType(Convert.ToInt32(row["F_ATTRIBUTE_ID"])) as DBAttributeType;
        if (attributeType.AttributeID > 0 && attributeType.FieldNames != null)
        {
          stringBuilder.Append("," + attributeType.ColumnSQL);
          if (int32 == OptimizationModes.Seek)
          {
            foreach (string indexFieldName in attributeType.IndexFieldNames)
              indexes.Add(db.DataProvider.GetIndexSQL(viewName, indexFieldName, SortOrders.ASC));
          }
        }
      }
    }
    if (needDrop)
    {
      try
      {
        db.SetAdminCommandTimeout();
        db.DataProvider.DropTableIfExists(db, viewName);
      }
      finally
      {
        db.SetNormalCommandTimeout();
      }
    }
    if (!isLocalType && stringBuilder.Length <= 0 && !(viewName == "IMS_OBJECTS_VIEW"))
      return false;
    if (sourceType == AttributeSourceTypes.Object)
      db.DataProvider.CreateObjectTypeView(viewName, stringBuilder.ToString(), db, indexes);
    else
      db.DataProvider.CreateRelationTypeView(viewName, stringBuilder.ToString(), db, indexes);
    if (createIndexes)
    {
      foreach (string index in indexes)
        db.ExecuteNonQuery(index);
    }
    return true;
  }

  public SqlBuilder(IUserSession uSession)
  {
    this.UserSession = uSession as UserSession;
    this.attributeTypes = uSession.GetAttributeTypeCollection(0);
  }

  private string GetEntersInTypeSql(
    List<IDbDataParameter> pars,
    bool isEnter,
    string paramName,
    ConditionStructure condition,
    IDbManager db,
    int nestedLevel,
    string mainTableAlias)
  {
    int num = this.NestedSubqueryCount++;
    DateTime dateTime = !(condition.Value2 is DateTime) ? DateTime.UtcNow : (DateTime) condition.Value2 - this.UserSession.TimeZoneOffset;
    pars.Add(db.Parameter(paramName, (object) dateTime));
    string str1 = !isEnter ? "NOT" : string.Empty;
    string str2 = "IMS_RELATIONS";
    string str3 = string.Empty;
    string str4;
    if (condition.TypeID == null)
    {
      str4 = string.Empty;
    }
    else
    {
      string[] updateTables = this.UserSession.DBCache.GetUpdateTables(-1, -1, Convert.ToInt32(condition.TypeID));
      if (updateTables != null)
      {
        str2 = updateTables[0];
        str4 = string.Empty;
        IMSAttribute4RelationType attribute4RelationType = MetaDataHelper.GetAttribute4RelationType(Convert.ToInt32(condition.TypeID), this.UserSession.IdentHelper.CompositionVersionID);
        if (attribute4RelationType != null && attribute4RelationType.OptimizationMode != OptimizationModes.Write)
          str3 = string.Format((attribute4RelationType.Required == RequiredModes.AutoRequired ? "(R{0}.F{1} = ABS({2}.F_OBJECT_ID))" : "((R{0}.F{1} = ABS({2}.F_OBJECT_ID)) OR (R{0}.F{1} IS NULL))") + " AND ", (object) num, (object) this.UserSession.IdentHelper.CompositionVersionID, (object) mainTableAlias);
      }
      else
        str4 = $"AND R{num}.F_RELATION_TYPE = {condition.TypeID} ";
    }
    if (condition.Value is Array && (condition.Value as Array).Length == 1)
      condition.Value = (condition.Value as Array).GetValue(0);
    string str5;
    string str6;
    if (condition.Value is Array)
    {
      Array array = (Array) condition.Value;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("IN (");
      for (int index = 0; index < array.Length; ++index)
      {
        if (index > 0)
          stringBuilder.Append(",");
        stringBuilder.Append(array.GetValue(index).ToString());
      }
      stringBuilder.Append(")");
      str5 = "IMS_OBJECTS";
      str6 = string.Format(" AND O{1}.F_OBJECT_TYPE {0}", (object) stringBuilder.ToString(), (object) num);
    }
    else
    {
      if (condition.Value == null || condition.Value.ToString() == string.Empty)
        throw new KernelExceptionID(sc_13066.ssp_appserver_13074(1954275710));
      string[] updateTables = this.UserSession.DBCache.GetUpdateTables(-1, Convert.ToInt32(condition.Value), -1);
      if (updateTables == null || Array.IndexOf<string>(updateTables, "IMV_O" + condition.Value.ToString().Trim()) < 0)
      {
        str5 = "IMS_OBJECTS_VIEW";
        str6 = $" AND O{num}.F_OBJECT_TYPE = {condition.Value.ToString().Trim()}";
      }
      else
      {
        str5 = "IMV_O" + condition.Value.ToString().Trim();
        str6 = string.Empty;
      }
    }
    string str7 = condition.NestedConditions == null ? string.Empty : $" AND ({this.GetNestedSQL(pars, condition.NestedConditions, db, nestedLevel, condition)})";
    string str8 = this.CurrentRecordSet == null || !this.CurrentRecordSet.ShowNotOwnedWorkCopies ? string.Format(" AND ((O{0}.F_OBJECT_ID > 0 AND O{0}.F_CHKOUT_BY <> :chckoutID) OR (O{0}.F_OBJECT_ID < 0 AND O{0}.F_CHKOUT_BY = :chckoutID))", (object) num) : string.Empty;
    return string.Format("({1}.F_ID {3} IN (SELECT R{5}.F_PART_ID FROM {8} R{5}, {6} O{5} WHERE {9}(R{5}.F_PART_ID = {1}.F_ID){7} {4}AND (R{5}.F_CREATE_DATE <= {2}) AND (O{5}.F_OBJECT_ID = R{5}.F_PROJ_ID{0}){10}))", (object) str6, (object) mainTableAlias, (object) paramName, (object) str1, (object) str4, (object) num, (object) str5, (object) str7, (object) str2, (object) str3, (object) str8);
  }

  private string GetConsistFromTypeSql(
    List<IDbDataParameter> pars,
    bool isEnter,
    string paramName,
    ConditionStructure condition,
    IDbManager db,
    int nestedLevel,
    string mainTableAlias)
  {
    int num1 = this.NestedSubqueryCount++;
    DateTime dateTime = !(condition.Value2 is DateTime) ? DateTime.UtcNow : (DateTime) condition.Value2 - this.UserSession.TimeZoneOffset;
    pars.Add(db.Parameter(paramName, (object) dateTime));
    string str1 = !isEnter ? "NOT" : "";
    string str2 = "IMS_RELATIONS";
    string str3 = string.Empty;
    string str4;
    if (condition.TypeID == null)
    {
      str4 = string.Empty;
    }
    else
    {
      string[] updateTables = this.UserSession.DBCache.GetUpdateTables(-1, -1, Convert.ToInt32(condition.TypeID));
      if (updateTables != null)
      {
        str2 = updateTables[0];
        str4 = string.Empty;
        IMSAttribute4RelationType attribute4RelationType = MetaDataHelper.GetAttribute4RelationType(Convert.ToInt32(condition.TypeID), this.UserSession.IdentHelper.CompositionVersionID);
        if (attribute4RelationType != null && attribute4RelationType.OptimizationMode != OptimizationModes.Write)
          str3 = string.Format((attribute4RelationType.Required == RequiredModes.AutoRequired ? "(R{0}.F{1} = ABS(O{0}.F_OBJECT_ID))" : "((R{0}.F{1} = ABS(O{0}.F_OBJECT_ID)) OR (R{0}.F{1} IS NULL))") + " AND ", (object) num1, (object) this.UserSession.IdentHelper.CompositionVersionID);
      }
      else
        str4 = $"AND R{num1}.F_RELATION_TYPE = {condition.TypeID} ";
    }
    if (condition.Value == null || condition.Value.ToString() == string.Empty)
      throw new KernelExceptionID(sc_13066.ssp_appserver_13075(1197109923));
    string str5;
    string str6;
    if (condition.Value is int[])
    {
      str5 = "IMS_OBJECTS_VIEW";
      int[] numArray = condition.Value as int[];
      if (numArray.Length == 0)
        throw new KernelException("В условие передан пустой список типов объектов.");
      StringBuilder stringBuilder = new StringBuilder();
      foreach (int num2 in numArray)
        stringBuilder.Append(num2.ToString() + ",");
      --stringBuilder.Length;
      str6 = $" AND O{num1}.F_OBJECT_TYPE IN ({stringBuilder.ToString()})";
    }
    else
    {
      string[] updateTables = this.UserSession.DBCache.GetUpdateTables(-1, Convert.ToInt32(condition.Value), -1);
      if (updateTables == null || Array.IndexOf<string>(updateTables, "IMV_O" + condition.Value.ToString().Trim()) < 0)
      {
        str5 = "IMS_OBJECTS_VIEW";
        str6 = $" AND O{num1}.F_OBJECT_TYPE = {condition.Value.ToString().Trim()}";
      }
      else
      {
        str5 = "IMV_O" + condition.Value.ToString().Trim();
        str6 = string.Empty;
      }
    }
    string str7 = condition.NestedConditions == null ? string.Empty : $" AND ({this.GetNestedSQL(pars, condition.NestedConditions, db, nestedLevel, condition)})";
    bool flag = false;
    for (int index = 0; index < pars.Count; ++index)
    {
      if (pars[index].ParameterName.IndexOf("chckoutID") > -1)
      {
        flag = true;
        break;
      }
    }
    if (!flag)
      pars.Add(db.Parameter("chckoutID", (object) this.UserSession.UserID));
    string str8 = this.CurrentRecordSet == null || !this.CurrentRecordSet.ShowNotOwnedWorkCopies ? string.Format(" AND ((O{0}.F_OBJECT_ID > 0 AND O{0}.F_CHKOUT_BY <> :chckoutID) OR (O{0}.F_OBJECT_ID < 0 AND O{0}.F_CHKOUT_BY = :chckoutID))", (object) num1) : string.Empty;
    return string.Format("({1}.F_OBJECT_ID {3} IN (SELECT R{7}.F_PROJ_ID FROM {8} R{7}, {0} O{7} WHERE {9}(R{7}.F_PROJ_ID = {1}.F_OBJECT_ID) {4}AND (R{7}.F_CREATE_DATE <= {2}) AND O{7}.F_ID = R{7}.F_PART_ID{5}{6}{10}))", (object) str5, (object) mainTableAlias, (object) paramName, (object) str1, (object) str4, (object) str6, (object) str7, (object) num1, (object) str2, (object) str3, (object) str8);
  }

  private string GetNestedSQL(
    List<IDbDataParameter> pars,
    ConditionStructure[] conditions,
    IDbManager db,
    int nestedLevel,
    ConditionStructure parentCondition)
  {
    StringBuilder stringBuilder1 = new StringBuilder();
    int nestedSubqueryCount = this.NestedSubqueryCount;
    string mainTableAlias = "O" + (nestedSubqueryCount - 1).ToString();
    int num1 = 0;
    LogicalOperators logicalOperators = LogicalOperators.NONE;
    for (int index1 = 0; index1 < conditions.Length; ++index1)
    {
      ConditionStructure condition = conditions[index1];
      if (condition.RelationalOperator == RelationalOperators.ObjectTypeFilter)
        throw new KernelExceptionID(399, (object) condition.RelationalOperator.ToString());
      if (this.PrepareCondition(ref condition))
      {
        if (index1 == conditions.Length - 1 && condition.LogicalOperator != LogicalOperators.NONE)
          condition.LogicalOperator = LogicalOperators.NONE;
        if (logicalOperators != LogicalOperators.NONE)
          stringBuilder1.Append($" {logicalOperators.ToString()} ");
        int num2 = this.NestedParamsCount++;
        string str1 = ":nested_p" + num2.ToString();
        if (condition.GroupID > 0)
        {
          for (int index2 = 0; index2 < condition.GroupID; ++index2)
          {
            stringBuilder1.Append("(");
            ++num1;
          }
          condition.GroupID = 0;
        }
        if (condition.RelationalOperator == RelationalOperators.NotEntersInType)
          stringBuilder1.Append(this.GetEntersInTypeSql(this.PreparedParams, false, str1, condition, db, nestedLevel + 1, mainTableAlias));
        else if (condition.RelationalOperator == RelationalOperators.EntersInType)
          stringBuilder1.Append(this.GetEntersInTypeSql(this.PreparedParams, true, str1, condition, db, nestedLevel + 1, mainTableAlias));
        else if (condition.RelationalOperator == RelationalOperators.NotConsistFromType)
          stringBuilder1.Append(this.GetConsistFromTypeSql(this.PreparedParams, false, str1, condition, db, nestedLevel + 1, mainTableAlias));
        else if (condition.RelationalOperator == RelationalOperators.ConsistFromType)
        {
          stringBuilder1.Append(this.GetConsistFromTypeSql(this.PreparedParams, true, str1, condition, db, nestedLevel + 1, mainTableAlias));
        }
        else
        {
          if (!(this.attributeTypes.GetAttributeType(condition.Attribute, true) is DBAttributeType attributeType))
            throw new KernelExceptionID(sc_13066.ssp_appserver_13076(580238128));
          this.PrepareConditionValue(ref condition, attributeType);
          stringBuilder1.Append("(");
          Attribute4ID attrStruct = new Attribute4ID(attributeType.AttributeID);
          string str2;
          string str3;
          string str4;
          string str5;
          if (condition.AttributeSource == AttributeSourceTypes.Relation)
          {
            num2 = nestedSubqueryCount - 1;
            str2 = "R" + num2.ToString();
            attrStruct.RelationTypeID = Convert.ToInt32(parentCondition.TypeID);
            str3 = "F_PRJLINK_ID";
            string relationAttributesAlias = this.RelationAttributesAlias;
            num2 = nestedSubqueryCount - 1;
            string str6 = num2.ToString();
            str4 = relationAttributesAlias + str6;
            str5 = "IMS_RELATION_ATTRS";
          }
          else
          {
            if (parentCondition.Value is int)
            {
              attrStruct.ObjectTypeID = Convert.ToInt32(parentCondition.Value);
            }
            else
            {
              attrStruct.ObjectTypeID = -1;
              if (attributeType.AttributeID > 0 || attributeType.AttributeID == -50)
                throw new KernelExceptionID(452, (object) attributeType.Name);
            }
            num2 = nestedSubqueryCount - 1;
            str2 = "O" + num2.ToString();
            str3 = "F_OBJECT_ID";
            string objectAttributesAlias = this.ObjectAttributesAlias;
            num2 = nestedSubqueryCount - 1;
            string str7 = num2.ToString();
            str4 = objectAttributesAlias + str7;
            str5 = parentCondition.RelationalOperator == RelationalOperators.ConsistFromType || parentCondition.RelationalOperator == RelationalOperators.EntersInType ? (!(parentCondition.Value is int) || !MetaDataHelper.IsLocalObjectType(Convert.ToInt32(parentCondition.Value)) ? "IMS_OBJECT_ATTRS" : this.UserSession.DBCache.GetAttributesTableName(Convert.ToInt32(parentCondition.Value))) : "IMS_OBJECT_ATTRS";
          }
          OptimizationModes optimizationMode = this.UserSession.DBCache.GetOptimizationMode(attrStruct);
          string str8 = attributeType.ValueFieldName;
          bool flag1 = true;
          bool flag2 = false;
          bool flag3 = str8 == "F_STRING_VALUE";
          if (attributeType.AttributeID > 0)
          {
            if (condition.RelationalOperator == RelationalOperators.Empty)
              flag1 = false;
            else if (attributeType.MultipleValued != MultiValueModes.MultiValues && attributeType.MultipleValued != MultiValueModes.MultiValuesFromList || condition.RelationalOperator == RelationalOperators.NotExistsOrEmpty)
            {
              if (optimizationMode == OptimizationModes.Read || optimizationMode == OptimizationModes.Seek)
              {
                switch (condition.Content)
                {
                  case ColumnContents.Text:
                    num2 = attributeType.AttributeID;
                    str8 = "F" + num2.ToString();
                    break;
                  case ColumnContents.ID:
                    str8 = $"F{attributeType.AttributeID}ID";
                    flag3 = false;
                    break;
                  case ColumnContents.Date:
                    str8 = $"F{attributeType.AttributeID}ID3";
                    flag3 = false;
                    break;
                  case ColumnContents.Value:
                    str8 = $"F{attributeType.AttributeID}ID2";
                    flag3 = false;
                    break;
                  case ColumnContents.String:
                    num2 = attributeType.AttributeID;
                    str8 = "F" + num2.ToString();
                    break;
                }
              }
              else
                flag1 = false;
            }
            else
              flag1 = false;
            if (!flag1)
            {
              if (condition.RelationalOperator == RelationalOperators.NotExistsOrEmpty)
              {
                string str9 = attrStruct.ObjectTypeID < 0 ? (attrStruct.RelationTypeID < 0 ? " списка атрибутов " : $" типа связей  '{this.UserSession.GetRelationType(attrStruct.RelationTypeID, true).Description}' ") : $" типа объектов '{this.UserSession.GetObjectType(attrStruct.ObjectTypeID, true).ObjectTypeName}' ";
                throw new KernelExceptionID(305, (object) RelationalOperatorsHelper.GetCaption(RelationalOperators.NotExistsOrEmpty), (object) attributeType.Name, (object) str9);
              }
              switch (condition.Content)
              {
                case ColumnContents.Text:
                  str8 = attributeType.TextFieldName;
                  break;
                case ColumnContents.ID:
                  str8 = "F_INTEGER_VALUE";
                  flag3 = false;
                  break;
                case ColumnContents.Date:
                  str8 = "F_DATE_VALUE";
                  flag3 = false;
                  break;
                case ColumnContents.Value:
                  str8 = "F_DOUBLE_VALUE";
                  flag3 = false;
                  break;
                case ColumnContents.String:
                  str8 = "F_STRING_VALUE";
                  break;
              }
              string str10;
              if (condition.RelationalOperator == RelationalOperators.NotEndString || condition.RelationalOperator == RelationalOperators.NotStartString || condition.RelationalOperator == RelationalOperators.NotSubstring)
              {
                str10 = "NOT ";
                flag2 = true;
              }
              else
                str10 = " ";
              stringBuilder1.AppendFormat("{0}.{1} {5}IN (SELECT {2}.{1} FROM {3} {2} WHERE {2}.F_ATTRIBUTE_ID = {4} AND ", (object) str2, (object) str3, (object) str4, (object) str5, (object) attributeType.AttributeID, (object) str10);
              str2 = str4;
              --condition.GroupID;
              ++num1;
            }
          }
          else if (attributeType.AttributeID == -18)
          {
            stringBuilder1.AppendFormat("{0}.F_ID IN (SELECT IMS_GUID_RESOLVE.F_ID FROM IMS_GUID_RESOLVE WHERE IMS_GUID_RESOLVE.F_CATEGORY_TYPE = 2 AND ", (object) str2);
            str2 = "IMS_GUID_RESOLVE";
            str8 = "F_GUID";
            --condition.GroupID;
            ++num1;
          }
          if (attributeType.AttributeID == -10)
            throw new KernelException("Атрибут 'Дата модификации объекта' устарел и не может использоваться в поисковых запросах.");
          if (ObligatoryObjectAttributesHelper.GetDataType((ObligatoryObjectAttributes) attributeType.AttributeID) == FieldTypes.ftString)
            flag3 = true;
          string str11 = string.Empty;
          string str12;
          if (condition.RelationalOperator == RelationalOperators.Between || condition.RelationalOperator == RelationalOperators.NotBetween)
          {
            if (condition.Value2 is MeasuredValue)
            {
              MeasuredValue mValue = condition.Value2 as MeasuredValue;
              MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(mValue);
              if (condition.Value is MeasuredValue && mValue.MeasureID != (condition.Value as MeasuredValue).MeasureID)
                throw new KernelExceptionID(308, (object) (condition.Value as MeasuredValue).Caption, (object) mValue.Caption);
              condition.Value2 = (object) (mValue.Value * descriptor.K);
            }
            string parameterName = str1;
            num2 = this.NestedParamsCount++;
            str1 = ":nested_p" + num2.ToString();
            str12 = string.Format(RelationalOperatorsHelper.SQLOperator(condition.RelationalOperator), (object) str1, (object) parameterName);
            this.PreparedParams.Add(db.Parameter(parameterName, condition.Value2));
          }
          else if (((condition.RelationalOperator == RelationalOperators.Empty || condition.RelationalOperator == RelationalOperators.NotExistsOrEmpty ? 1 : (condition.RelationalOperator == RelationalOperators.NotEmpty ? 1 : 0)) & (flag3 ? 1 : 0)) != 0)
          {
            str1 = string.Empty;
            str12 = string.Format(RelationalOperatorsHelper.SQLOperator(condition.RelationalOperator), (object) str1);
            if (db.DataProvider.CanStoreEmptyString)
            {
              str12 = condition.RelationalOperator == RelationalOperators.Empty || condition.RelationalOperator == RelationalOperators.NotExistsOrEmpty ? $"{str12} OR {str2}.{str8} = '')" : $"{str12} AND {str2}.{str8} <> '')";
              str11 = "(";
            }
          }
          else if (condition.RelationalOperator == RelationalOperators.In || condition.RelationalOperator == RelationalOperators.NotIn)
          {
            StringBuilder stringBuilder2 = new StringBuilder();
            Array array = (Array) condition.Value;
            for (int index3 = 0; index3 < array.Length; ++index3)
            {
              if (index3 > 0)
                stringBuilder2.Append(",");
              num2 = this.NestedParamsCount++;
              string parameterName = ":nested_p" + num2.ToString();
              stringBuilder2.Append(parameterName);
              this.PreparedParams.Add(db.Parameter(parameterName, array.GetValue(index3)));
            }
            str12 = string.Format(RelationalOperatorsHelper.SQLOperator(condition.RelationalOperator), (object) stringBuilder2.ToString());
            str1 = string.Empty;
          }
          else
            str12 = !flag2 ? string.Format(RelationalOperatorsHelper.SQLOperator(condition.RelationalOperator), (object) str1) : string.Format(RelationalOperatorsHelper.SQLOperator(RelationalOperators.Substring), (object) str1);
          if (attributeType.AttributeType == FieldTypes.ftObjectLink)
            this.CorrectObjectLinkCondition(ref condition);
          if (attributeType.AttributeType == FieldTypes.ftMeasured && condition.Value is MeasuredValue)
          {
            MeasuredValue mValue = condition.Value as MeasuredValue;
            MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(mValue);
            condition.Value = (object) (mValue.Value * descriptor.K);
            if (attributeType.SizeType <= 0L)
            {
              MeasureDescriptor baseValue = MeasureHelper.FindBaseValue(descriptor);
              num2 = this.NestedParamsCount++;
              string parameterName = ":nested_p" + num2.ToString();
              this.PreparedParams.Add(db.Parameter(parameterName, (object) baseValue.MeasureID));
              string str13 = !(str8 == "F_DOUBLE_VALUE") ? $"F{attributeType.AttributeID}ID" : "F_INTEGER_VALUE";
              stringBuilder1.AppendFormat("{0}.{1} {2} AND {3} = {4}", (object) str2, (object) str8, (object) str12, (object) str13, (object) parameterName);
            }
            else
              stringBuilder1.AppendFormat("{0}.{1} {2}", (object) str2, (object) str8, (object) str12);
          }
          else if (attributeType.AttributeType == FieldTypes.ftMemo)
          {
            if (!condition.CaseSensitive && condition.RelationalOperator != RelationalOperators.NotExistsOrEmpty && condition.RelationalOperator != RelationalOperators.NotEmpty && condition.RelationalOperator != RelationalOperators.Empty)
            {
              if (!db.DataProvider.CanUpperMemo)
                throw new KernelExceptionID(sc_13066.ssp_appserver_13077(1328643632));
              stringBuilder1.AppendFormat("{0}.F_INTEGER_VALUE IN (SELECT F_KEY FROM IMS_MEMOS WHERE IMS_MEMOS.F_KEY = {0}.F_INTEGER_VALUE AND UPPER(IMS_MEMOS.F_VALUE) {1})", (object) str2, (object) str12);
            }
            else
              stringBuilder1.AppendFormat("{0}.F_INTEGER_VALUE IN (SELECT F_KEY FROM IMS_MEMOS WHERE IMS_MEMOS.F_KEY = {0}.F_INTEGER_VALUE AND IMS_MEMOS.F_VALUE {1})", (object) str2, (object) str12);
          }
          else if (!condition.CaseSensitive && SqlHelper.IsVarcharField(attributeType.TextFieldName) && condition.Value is string)
            stringBuilder1.AppendFormat("{0}UPPER({1}.{2}) {3}", (object) str11, (object) str2, (object) str8, (object) str12);
          else
            stringBuilder1.AppendFormat("{0}{1}.{2} {3}", (object) str11, (object) str2, (object) str8, (object) str12);
          object correctedValue = this.GetCorrectedValue(attributeType, condition, db);
          if (str1 != string.Empty && condition.RelationalOperator != RelationalOperators.Empty && condition.RelationalOperator != RelationalOperators.NotExistsOrEmpty && condition.RelationalOperator != RelationalOperators.NotEmpty)
            this.PreparedParams.Add(db.Parameter(str1, correctedValue));
          if (this.EscapeString != string.Empty)
          {
            stringBuilder1.Append(" " + this.EscapeString);
            this.EscapeString = string.Empty;
          }
          stringBuilder1.Append(")");
        }
        if (condition.GroupID < 0)
        {
          for (int groupId = condition.GroupID; groupId < 0; ++groupId)
          {
            stringBuilder1.Append(")");
            --num1;
          }
        }
        logicalOperators = condition.LogicalOperator;
      }
    }
    for (int index = 0; index < num1; ++index)
      stringBuilder1.Append(")");
    return stringBuilder1.ToString();
  }

  public INConditionValue StartINCondition(
    object attributeID,
    Array inlist_array,
    bool enForceSave = false)
  {
    INConditionValue inConditionValue = new INConditionValue();
    IDbManager dataManager = this.UserSession.DataManager;
    if (enForceSave || inlist_array.Length > dataManager.DataProvider.MaximumINOperands)
    {
      bool flag = false;
      if (!this.UserSession.InTransaction)
      {
        flag = true;
        inConditionValue.IsInsertData = true;
        this.UserSession.StartTransaction();
      }
      try
      {
        this.SelectKeyValue = this.UserSession.QueryBuilder.SelectKey;
        this.InsertIntoTemporaryTable(attributeID, inlist_array);
        inConditionValue.TmpTableName = this.TemporaryTableName;
        inConditionValue.SelectKey = this.SelectKeyValue;
      }
      catch
      {
        if (flag)
          this.UserSession.Rollback();
        throw;
      }
    }
    else
      inConditionValue.Values = inlist_array;
    return inConditionValue;
  }

  public void StopINCondition(INConditionValue cValue)
  {
    if (cValue == null)
      return;
    IDbManager dataManager = this.UserSession.DataManager;
    if (cValue.IsInsertData)
    {
      try
      {
        dataManager.ExecuteNonQuery($"DELETE FROM {cValue.TmpTableName} WHERE F_KEY = :keyID", dataManager.Parameter("keyID", (object) cValue.SelectKey));
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
      if (cValue.SelectKey <= 0L)
        return;
      dataManager.ExecuteNonQuery($"DELETE FROM {cValue.TmpTableName} WHERE F_KEY = :keyID", dataManager.Parameter("keyID", (object) cValue.SelectKey));
    }
  }

  private void InsertIntoTemporaryTable(object attribute_id, Array vals)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    DbType d_type;
    switch ((this.attributeTypes.GetAttributeType(attribute_id, true) as DBAttributeType).ValueFieldName)
    {
      case "F_DOUBLE_VALUE":
        this.TemporaryTableName = "IMS_TMP_DOUBLE";
        d_type = DbType.Double;
        break;
      case "F_OBJ_GUID":
      case "F_GUID":
      case "CAPTION":
        this.TemporaryTableName = "IMS_TMP_STRING";
        d_type = DbType.String;
        break;
      case "F_STRING_VALUE":
        this.TemporaryTableName = "IMS_TMP_STRING";
        d_type = DbType.String;
        break;
      case "F_DATE_VALUE":
        this.TemporaryTableName = "IMS_TMP_DATE";
        d_type = DbType.Date;
        break;
      default:
        this.TemporaryTableName = "IMS_TMP_INTEGER";
        d_type = DbType.Int64;
        break;
    }
    if (dataManager.DataProvider.Name == "Oracle" && !this.UserSession.InTransaction)
    {
      this.NeedCommitTransaction = true;
      this.UserSession.StartTransaction();
    }
    dataManager.DataProvider.InsertIntoTemporaryTable(this.TemporaryTableName, d_type, this.SelectKeyValue, dataManager, vals);
  }

  private void CheckINConditions(ConditionStructure[] conditions, IDbManager db)
  {
    for (int index = 0; index < conditions.Length; ++index)
    {
      if ((conditions[index].RelationalOperator == RelationalOperators.In || conditions[index].RelationalOperator == RelationalOperators.NotIn) && !(conditions[index].Value is INConditionValue))
      {
        if (!(conditions[index].Value is Array))
          throw new KernelExceptionID(sc_13066.ssp_appserver_13078(13060863));
        Array vals = (Array) conditions[index].Value;
        if (vals.Length == 0)
          throw new KernelExceptionID(sc_13066.ssp_appserver_13079(782032726));
        if (vals.Length > db.DataProvider.MaximumINOperands)
        {
          this.NeedDeleteTemp = true;
          this.SelectKeyValue = this.UserSession.QueryBuilder.SelectKey;
          this.SelectConditionIndex = this.SelectConditionIndex <= -1 ? index : throw new KernelException("Too many IN operators with too many operands.");
          this.InsertIntoTemporaryTable(conditions[index].Attribute, vals);
        }
      }
    }
  }

  private bool PrepareCondition(ref ConditionStructure condition)
  {
    if (condition.RelationalOperator == RelationalOperators.LastNDays)
    {
      condition.RelationalOperator = RelationalOperators.Between;
      ref ConditionStructure local1 = ref condition;
      DateTime utcNow = DateTime.UtcNow;
      // ISSUE: variable of a boxed type
      __Boxed<DateTime> local2 = (System.ValueType) (utcNow.Date + TimeSpan.FromDays((double) (1L - Convert.ToInt64(condition.Value))));
      local1.Value = (object) local2;
      ref ConditionStructure local3 = ref condition;
      utcNow = DateTime.UtcNow;
      // ISSUE: variable of a boxed type
      __Boxed<DateTime> date = (System.ValueType) utcNow.Date;
      local3.Value2 = (object) date;
    }
    else if (condition.RelationalOperator == RelationalOperators.NextNDays)
    {
      condition.RelationalOperator = RelationalOperators.Between;
      ref ConditionStructure local4 = ref condition;
      DateTime utcNow = DateTime.UtcNow;
      // ISSUE: variable of a boxed type
      __Boxed<DateTime> local5 = (System.ValueType) (utcNow.Date + TimeSpan.FromDays((double) (Convert.ToInt64(condition.Value) - 1L)));
      local4.Value2 = (object) local5;
      ref ConditionStructure local6 = ref condition;
      utcNow = DateTime.UtcNow;
      // ISSUE: variable of a boxed type
      __Boxed<DateTime> date = (System.ValueType) utcNow.Date;
      local6.Value = (object) date;
    }
    if (condition.RelationalOperator == RelationalOperators.NOP || condition.RelationalOperator == RelationalOperators.None)
      return false;
    if (condition.RelationalOperator == RelationalOperators.StringTemplate)
    {
      if (condition.Value != null)
        condition.Value = (object) condition.Value.ToString().Replace('?', '_').Replace('*', '%');
    }
    else if (condition.RelationalOperator == RelationalOperators.InGlobalIndex)
    {
      GlobalIndexSearchValue indexSearchValue = condition.Value as GlobalIndexSearchValue;
      if (indexSearchValue.Value.Length > 1 && indexSearchValue.Value[0] == '?')
      {
        condition.RelationalOperator = RelationalOperators.Equal;
        ConditionFormula conditionFormula = new ConditionFormula(indexSearchValue.Value.Substring(1), Array.Empty<DBDataParam>());
        condition.Value = (object) conditionFormula;
        condition.Attribute = (object) null;
      }
    }
    if ((condition.RelationalOperator == RelationalOperators.Between || condition.RelationalOperator == RelationalOperators.NotBetween) && condition.Value2 == null)
      throw new KernelExceptionID(sc_13066.ssp_appserver_13080(108941977));
    return true;
  }

  private void PrepareConditionValue(ref ConditionStructure condition, DBAttributeType attribute)
  {
    bool flag = false;
    if (attribute.AttributeID == -7)
    {
      if (condition.RelationalOperator == RelationalOperators.Equal || condition.RelationalOperator == RelationalOperators.NotEqual)
      {
        int int32 = Convert.ToInt32(condition.Value);
        if (int32 == -1)
          throw new KernelException(LocalizationHolder.rm.GetString("Kernel_1171"));
        if (MetaDataHelper.GetObjectType(int32).VersionsMode == ObjectVersionModes.Abstract)
        {
          List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(int32);
          if (childrenIdRecursive.Count > 1)
          {
            condition.RelationalOperator = condition.RelationalOperator != RelationalOperators.Equal ? RelationalOperators.NotIn : RelationalOperators.In;
            condition.Value = (object) childrenIdRecursive.ToArray();
          }
        }
      }
      else if ((condition.RelationalOperator == RelationalOperators.In || condition.RelationalOperator == RelationalOperators.NotIn) && condition.Value is int[])
      {
        int[] collection = condition.Value as int[];
        List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) new List<int>((IEnumerable<int>) collection));
        GenericListHelper.MakeUnique<int>(childrenIdRecursive);
        if (childrenIdRecursive.Count != collection.Length)
          condition.Value = (object) childrenIdRecursive.ToArray();
      }
    }
    if (attribute.AttributeType == FieldTypes.ftDateTime || attribute.AttributeType == FieldTypes.ftSystem || condition.Content == ColumnContents.Date)
    {
      flag = true;
      if (condition.Value != null && condition.Value.ToString() == Consts.CurrentDateFunction)
      {
        condition.Value = (object) DateTime.Today;
        flag = false;
      }
      if (condition.Value2 != null && condition.Value2.ToString() == Consts.CurrentDateFunction)
      {
        condition.Value2 = (object) DateTime.Now;
        flag = false;
      }
      if (condition.Value is DateTime)
      {
        DateTime date1 = (DateTime) condition.Value;
        if (attribute.Mask == Consts.OnlyDateFunction)
        {
          date1 = date1.Date;
          flag = false;
        }
        if (date1.TimeOfDay == TimeSpan.Zero)
        {
          switch (condition.RelationalOperator)
          {
            case RelationalOperators.Equal:
              if (attribute.Mask != Consts.OnlyDateFunction)
              {
                condition.RelationalOperator = RelationalOperators.Between;
                condition.Value = (object) date1;
                condition.Value2 = (object) (date1 + TimeSpan.FromDays(1.0) - TimeSpan.FromSeconds(1.0));
                break;
              }
              break;
            case RelationalOperators.NotEqual:
              if (attribute.Mask != Consts.OnlyDateFunction)
              {
                condition.RelationalOperator = RelationalOperators.NotBetween;
                condition.Value = (object) date1;
                condition.Value2 = (object) (date1 + TimeSpan.FromDays(1.0) - TimeSpan.FromSeconds(1.0));
                break;
              }
              break;
            case RelationalOperators.Greater:
              if (attribute.Mask != Consts.OnlyDateFunction)
              {
                condition.Value = (object) (date1 + TimeSpan.FromDays(1.0) - TimeSpan.FromSeconds(1.0));
                break;
              }
              break;
            case RelationalOperators.LessOrEqual:
              if (attribute.Mask != Consts.OnlyDateFunction)
              {
                condition.Value = (object) (date1 + TimeSpan.FromDays(1.0) - TimeSpan.FromSeconds(1.0));
                break;
              }
              break;
            case RelationalOperators.Between:
              DateTime dateTime1 = (DateTime) condition.Value2;
              if (attribute.Mask == Consts.OnlyDateFunction)
              {
                DateTime date2 = dateTime1.Date;
                break;
              }
              if (dateTime1.TimeOfDay == TimeSpan.Zero)
              {
                condition.Value2 = (object) (dateTime1 + TimeSpan.FromDays(1.0) - TimeSpan.FromSeconds(1.0));
                break;
              }
              break;
            case RelationalOperators.NotBetween:
              DateTime dateTime2 = (DateTime) condition.Value2;
              if (attribute.Mask == Consts.OnlyDateFunction)
              {
                DateTime date3 = dateTime2.Date;
                break;
              }
              if (dateTime2.TimeOfDay == TimeSpan.Zero)
              {
                condition.Value2 = (object) (dateTime2 + TimeSpan.FromDays(1.0) - TimeSpan.FromSeconds(1.0));
                break;
              }
              break;
          }
        }
      }
    }
    if (flag)
    {
      if (condition.Value is DateTime)
      {
        DateTime dateTime = (DateTime) condition.Value;
        condition.Value = (object) (dateTime - this.UserSession.TimeZoneOffset);
      }
      if (condition.Value2 is DateTime)
      {
        DateTime dateTime = (DateTime) condition.Value2;
        condition.Value2 = (object) (dateTime - this.UserSession.TimeZoneOffset);
      }
    }
    if (attribute.AttributeType == FieldTypes.ftMeasured)
    {
      if (condition.Value is MeasuredValue)
      {
        condition.Value = (object) MeasureHelper.ConvertToBaseMeasure((MeasuredValue) condition.Value);
        condition.Content = ColumnContents.Value;
      }
      if (condition.Value2 is MeasuredValue)
        condition.Value2 = (object) MeasureHelper.ConvertToBaseMeasure((MeasuredValue) condition.Value2);
    }
    if ((attribute.AttributeType == FieldTypes.ftObjectLink || attribute.AttributeID == -8 || attribute.AttributeID == -6 || attribute.AttributeID == -81) && condition.Value is string && condition.Value.ToString() == Consts.CurrentUserFunction)
    {
      condition.Value = (object) this.UserSession.UserID;
      condition.Content = ColumnContents.ID;
    }
    if (this.UserSession.DataManager.DataProvider.Name == "PostgreSQL")
    {
      if (attribute.AttributeType == FieldTypes.ftGuid)
      {
        if (condition.Value != null)
        {
          if (condition.Value is Array)
          {
            Array array = (Array) condition.Value;
            GuidValue[] guidValueArray = new GuidValue[array.Length];
            for (int index = 0; index < array.Length; ++index)
              guidValueArray[index] = new GuidValue(array.GetValue(index));
            condition.Value = (object) guidValueArray;
          }
          else
            condition.Value = (object) new GuidValue(condition.Value);
        }
        if (condition.Value2 != null)
          condition.Value2 = (object) new GuidValue(condition.Value2);
      }
      else if (attribute.AttributeType == FieldTypes.ftString)
      {
        if (condition.Value is Guid)
        {
          condition.Value = (object) new GuidValue(condition.Value);
        }
        else
        {
          Guid result;
          if (condition.Value is string && condition.RelationalOperator != RelationalOperators.NotSubstring && condition.RelationalOperator != RelationalOperators.NotStartString && condition.RelationalOperator != RelationalOperators.NotEndString && condition.RelationalOperator != RelationalOperators.StringTemplate && condition.RelationalOperator != RelationalOperators.StartString && condition.RelationalOperator != RelationalOperators.Substring && condition.RelationalOperator != RelationalOperators.EndString && Guid.TryParse(condition.Value.ToString(), out result))
            condition.Value = (object) new GuidValue((object) result);
        }
      }
    }
    if (attribute.Computed != ComputeValueModes.IndexValue)
    {
      if (!attribute.IsGridable)
        throw new KernelExceptionID(sc_13066.ssp_appserver_13081(1727095091), (object) attribute.Name, (object) attribute.TypeCaption);
    }
    else
    {
      if (condition.Value is string)
        condition.Value = (object) this.UserSession.StringNormalizer.GetIndexedString(condition.Value.ToString());
      if (condition.Value2 is string)
        condition.Value2 = (object) this.UserSession.StringNormalizer.GetIndexedString(condition.Value2.ToString());
    }
    if (attribute.AttributeType == FieldTypes.ftObjectLink && condition.Content != ColumnContents.ID && (condition.Value is long || condition.Value is long[]))
      condition.Content = ColumnContents.ID;
    attribute.ValidateRelationalOperator(condition.RelationalOperator, true, condition.Content);
  }

  private void CorrectObjectLinkCondition(ref ConditionStructure condition)
  {
    if (condition.Value is Array)
    {
      foreach (object obj in (Array) condition.Value)
      {
        if (condition.Content == ColumnContents.ID || obj is int || obj is long)
          condition.Value = (object) Math.Abs(Convert.ToInt64(obj));
      }
    }
    else
    {
      if (condition.Content != ColumnContents.ID && !(condition.Value is int) && !(condition.Value is long) || condition.Value == null || condition.Value == DBNull.Value)
        return;
      condition.Value = (object) Math.Abs(Convert.ToInt64(condition.Value));
    }
  }

  private object GetCorrectedValue(
    DBAttributeType attribute,
    ConditionStructure condition,
    IDbManager db)
  {
    object val;
    if (condition.Value is string || condition.Value is char)
    {
      val = condition.CaseSensitive ? condition.Value : (object) Convert.ToString(condition.Value).ToUpper();
      switch (condition.RelationalOperator)
      {
        case RelationalOperators.Substring:
          val = (object) $"%{this.ReplaceLIKESymbols(val, db)}%";
          break;
        case RelationalOperators.StartString:
          val = (object) $"{this.ReplaceLIKESymbols(val, db)}%";
          break;
        case RelationalOperators.EndString:
          val = (object) $"%{this.ReplaceLIKESymbols(val, db)}";
          break;
        case RelationalOperators.NotSubstring:
          val = (object) $"%{this.ReplaceLIKESymbols(val, db)}%";
          break;
        case RelationalOperators.NotStartString:
          val = (object) $"{this.ReplaceLIKESymbols(val, db)}%";
          break;
        case RelationalOperators.NotEndString:
          val = (object) $"%{this.ReplaceLIKESymbols(val, db)}";
          break;
      }
      if (attribute.Computed == ComputeValueModes.IndexValue)
        val = (object) this.UserSession.StringNormalizer.GetIndexedString(val.ToString());
    }
    else
      val = attribute.AttributeType != FieldTypes.ftGuid || !(condition.Value is Guid) ? condition.Value : (object) condition.Value.ToString();
    return val;
  }

  public string GetWhereSQL(ConditionStructure[] conditions, int recordsCount, IDbManager db)
  {
    StringBuilder stringBuilder1 = new StringBuilder(1024 /*0x0400*/);
    this.WordsIDs = string.Empty;
    this.NeedDeleteTemp = false;
    this.SelectConditionIndex = -1;
    this.NestedParamsCount = 0;
    this.NestedSubqueryCount = 0;
    this.EscapeString = string.Empty;
    int num1 = 0;
    if (conditions != null)
    {
      this.CheckINConditions(conditions, db);
      int num2 = 0;
      LogicalOperators logicalOperators = LogicalOperators.NONE;
      for (int ndx = 0; ndx < conditions.Length; ++ndx)
      {
        ConditionStructure condition = conditions[ndx];
        if (this.PrepareCondition(ref condition))
        {
          if (ndx == conditions.Length - 1 && condition.LogicalOperator != LogicalOperators.NONE)
            condition.LogicalOperator = LogicalOperators.NONE;
          else if (ndx < conditions.Length - 1 && condition.LogicalOperator == LogicalOperators.NONE)
            condition.LogicalOperator = LogicalOperators.AND;
          if (logicalOperators != LogicalOperators.NONE)
            stringBuilder1.AppendFormat(" {0} ", (object) logicalOperators.ToString());
          bool flag1 = condition.Value is ConditionFormula;
          string str1;
          int index1;
          if (flag1)
          {
            str1 = this.PrepareConditionFormula(ref condition);
            condition.SQL = string.Format(str1, (object) this.SystemTableAlias);
            this.UserSession.DelayedUpdater.AddSearchQuery(new SearchQueryProperties("?" + (condition.Value as ConditionFormula).Formula, this.UserSession.UserID, DateTime.UtcNow, this.UserSession.SecurityLevel));
          }
          else if (ObligatoryObjectAttributesHelper.IsGuidField(condition.Attribute))
          {
            index1 = num1++;
            str1 = ":guidPar" + index1.ToString();
          }
          else
          {
            index1 = num1++;
            str1 = ":p" + index1.ToString();
          }
          if (condition.GroupID > 0)
          {
            for (int index2 = 0; index2 < condition.GroupID; ++index2)
            {
              stringBuilder1.Append("(");
              ++num2;
            }
            condition.GroupID = 0;
          }
          int num3;
          if (condition.RelationalOperator == RelationalOperators.EntersIn)
          {
            DateTime dateTime = !(condition.Value2 is DateTime) ? DateTime.UtcNow : (DateTime) condition.Value2 - this.UserSession.TimeZoneOffset;
            this.PreparedParams.Add(db.Parameter(str1, (object) dateTime));
            string str2 = condition.TypeID != null ? $"R.F_RELATION_TYPE = {condition.TypeID} AND " : "";
            string str3;
            if (condition.Value is long[])
            {
              if ((condition.Value as long[]).Length > db.DataProvider.MaximumINOperands)
                throw new KernelExceptionID(sc_13066.ssp_appserver_13082(836395270), (object) db.DataProvider.MaximumINOperands);
              StringBuilder stringBuilder2 = new StringBuilder();
              long[] numArray = condition.Value as long[];
              for (index1 = 0; index1 < numArray.Length; ++index1)
              {
                long num4 = numArray[index1];
                num3 = num1++;
                string parameterName = ":p" + num3.ToString();
                this.PreparedParams.Add(db.Parameter(parameterName, (object) num4));
                stringBuilder2.Append(parameterName);
                stringBuilder2.Append(",");
              }
              --stringBuilder2.Length;
              str3 = $"(R.F_PROJ_ID IN ({stringBuilder2.ToString()}))";
            }
            else
            {
              index1 = num1++;
              string parameterName = ":p" + index1.ToString();
              this.PreparedParams.Add(db.Parameter(parameterName, condition.Value));
              str3 = "R.F_PROJ_ID = " + parameterName;
            }
            stringBuilder1.AppendFormat("({1}.F_ID IN (SELECT R.F_PART_ID FROM IMS_RELATIONS R WHERE {0} AND {3}(R.F_PART_ID = {1}.F_ID) AND (R.F_CREATE_DATE <= {2})))", (object) str3, (object) this.SystemTableAlias, (object) str1, (object) str2);
          }
          else if (condition.RelationalOperator == RelationalOperators.NotEntersInType)
            stringBuilder1.Append(this.GetEntersInTypeSql(this.PreparedParams, false, str1, condition, db, 1, this.SystemTableAlias));
          else if (condition.RelationalOperator == RelationalOperators.EntersInType)
            stringBuilder1.Append(this.GetEntersInTypeSql(this.PreparedParams, true, str1, condition, db, 1, this.SystemTableAlias));
          else if (condition.RelationalOperator == RelationalOperators.NotConsistFromType)
            stringBuilder1.Append(this.GetConsistFromTypeSql(this.PreparedParams, false, str1, condition, db, 1, this.SystemTableAlias));
          else if (condition.RelationalOperator == RelationalOperators.ConsistFromType)
            stringBuilder1.Append(this.GetConsistFromTypeSql(this.PreparedParams, true, str1, condition, db, 1, this.SystemTableAlias));
          else if (condition.RelationalOperator == RelationalOperators.ExistsInVersionContext)
          {
            string str4;
            if (condition.Value is long[])
            {
              StringBuilder stringBuilder3 = new StringBuilder();
              long[] numArray = condition.Value as long[];
              for (index1 = 0; index1 < numArray.Length; ++index1)
              {
                long num5 = numArray[index1];
                num3 = num1++;
                string parameterName = ":p" + num3.ToString();
                this.PreparedParams.Add(db.Parameter(parameterName, (object) Math.Abs(num5)));
                stringBuilder3.Append(parameterName);
                stringBuilder3.Append(",");
              }
              --stringBuilder3.Length;
              str4 = $"IN ({stringBuilder3.ToString()})";
            }
            else
            {
              str4 = "= " + str1;
              this.PreparedParams.Add(db.Parameter(str1, (object) Math.Abs(Convert.ToInt64(condition.Value))));
            }
            stringBuilder1.AppendFormat("(EXISTS (SELECT VC.F_CONTEXT_ID FROM IMS_VERSIONS_CONTEXT VC WHERE VC.F_CONTEXT_ID {0} AND (VC.F_OBJECT_ID = abs(S.F_OBJECT_ID))))", (object) str4);
          }
          else if (condition.RelationalOperator == RelationalOperators.InGlobalIndex)
            stringBuilder1.Append(this.GetGlobalIndexSQL(condition, ndx));
          else if (condition.RelationalOperator == RelationalOperators.ConsistFrom)
          {
            DateTime dateTime = !(condition.Value2 is DateTime) ? DateTime.UtcNow : (DateTime) condition.Value2 - this.UserSession.TimeZoneOffset;
            this.PreparedParams.Add(db.Parameter(str1, (object) dateTime));
            string str5 = condition.TypeID != null ? $"R.F_RELATION_TYPE = {condition.TypeID} AND " : "";
            long[] numArray1;
            if (condition.Value is long[])
              numArray1 = condition.Value as long[];
            else
              numArray1 = new long[1]
              {
                Convert.ToInt64(condition.Value)
              };
            StringBuilder stringBuilder4 = new StringBuilder();
            stringBuilder4.Append("(R.F_PART_ID IN (");
            long[] numArray2 = numArray1;
            for (index1 = 0; index1 < numArray2.Length; ++index1)
            {
              long num6 = numArray2[index1];
              num3 = num1++;
              string parameterName1 = ":p" + num3.ToString();
              this.PreparedParams.Add(db.Parameter(parameterName1, (object) num6));
              stringBuilder4.Append(parameterName1);
              stringBuilder4.Append(",");
              if (num6 < 0L)
              {
                num3 = num1++;
                string parameterName2 = ":p" + num3.ToString();
                this.PreparedParams.Add(db.Parameter(parameterName2, (object) -num6));
                stringBuilder4.Append(parameterName2);
                stringBuilder4.Append(",");
              }
            }
            --stringBuilder4.Length;
            stringBuilder4.Append("))");
            stringBuilder1.AppendFormat("(ABS({1}.F_OBJECT_ID) IN (SELECT ABS(R.F_PROJ_ID) FROM IMS_RELATIONS R WHERE {0} AND {3}R.F_PROJ_ID = {1}.F_OBJECT_ID AND (R.F_CREATE_DATE <= {2})))", (object) stringBuilder4.ToString(), (object) this.SystemTableAlias, (object) str1, (object) str5);
          }
          else if (condition.RelationalOperator == RelationalOperators.ParentVersionID)
          {
            if (Convert.ToInt64(condition.Value) != -1L)
            {
              stringBuilder1.AppendFormat("(ABS({1}.F_OBJECT_ID) IN (SELECT VT.F_OBJECT_ID FROM IMS_VERSIONS_TREE VT WHERE VT.F_PARENT_ID = {0} AND VT.F_OBJECT_ID = ABS({1}.F_OBJECT_ID)))", (object) str1, (object) this.SystemTableAlias);
              this.PreparedParams.Add(db.Parameter(str1, (object) Math.Abs(Convert.ToInt64(condition.Value))));
            }
            else
              stringBuilder1.AppendFormat("(ABS({0}.F_OBJECT_ID) NOT IN (SELECT VT.F_OBJECT_ID FROM IMS_VERSIONS_TREE VT WHERE VT.F_OBJECT_ID = ABS({0}.F_OBJECT_ID)))", (object) this.SystemTableAlias);
          }
          else if (condition.RelationalOperator == RelationalOperators.AttributeExists)
          {
            DBAttributeType attributeType = this.attributeTypes.GetAttributeType(condition.Attribute, true) as DBAttributeType;
            if (attributeType.AttributeType == FieldTypes.ftSystem)
              throw new KernelExceptionID(sc_13066.ssp_appserver_13083(1022907502));
            string str6;
            string str7;
            if (condition.AttributeSource == AttributeSourceTypes.Relation)
            {
              str6 = "F_PRJLINK_ID";
              str7 = "IMS_RELATION_ATTRS";
            }
            else
            {
              str6 = "F_OBJECT_ID";
              str7 = this.ObjectAttributesTable;
            }
            stringBuilder1.AppendFormat("({0}.{1} IN (SELECT {2}.{1} FROM {2} WHERE {2}.{1} = {0}.{1} AND {2}.F_ATTRIBUTE_ID = {3}))", (object) this.SystemTableAlias, (object) str6, (object) str7, (object) attributeType.AttributeID);
          }
          else if (condition.RelationalOperator == RelationalOperators.InSelection)
          {
            string str8 = "F_ID";
            string str9 = "({0}.{2}";
            if (condition.Value2 is bool && (bool) condition.Value2)
            {
              str8 = "F_OBJECT_ID";
              str9 = "(ABS({0}.{2})";
            }
            if (condition.CaseSensitive)
            {
              string str10 = condition.TypeID == null ? "IMS_OBJECT_ATTRS" : this.UserSession.DBCache.GetAttributesTableName(Convert.ToInt32(condition.TypeID));
              object val = db.ExecuteScalar($"SELECT F_STRING_VALUE FROM {str10} WHERE F_OBJECT_ID = :objID AND F_ATTRIBUTE_ID = :keyID", db.Parameter("objID", condition.Value), db.Parameter("keyID", (object) this.UserSession.IdentHelper.FolderKeyID));
              if (val == null || val == DBNull.Value)
                throw new KernelExceptionID(sc_13066.ssp_appserver_13084(1056201959), condition.Value);
              stringBuilder1.AppendFormat(str9 + " IN (SELECT {2} FROM IMS_SELECTIONS WHERE F_FOLDER_KEY LIKE {1}{3}))", (object) this.SystemTableAlias, (object) str1, (object) str8, (object) db.DataProvider.GetEscapeSQL(val.ToString()));
              object obj = this.ReplaceLIKESymbols(val, db);
              this.PreparedParams.Add(db.Parameter(str1, (object) (obj.ToString() + "%")));
            }
            else
            {
              stringBuilder1.AppendFormat(str9 + " IN (SELECT {2} FROM IMS_SELECTIONS WHERE F_FOLDER_ID = {1}))", (object) this.SystemTableAlias, (object) str1, (object) str8);
              this.PreparedParams.Add(db.Parameter(str1, condition.Value));
            }
          }
          else if (condition.RelationalOperator == RelationalOperators.InFiltrationTable)
          {
            stringBuilder1.AppendFormat("((ABS({0}.F_OBJECT_ID) IN (SELECT F_OBJECT_ID FROM IMS_ATTRFILTER_VALUE WHERE F_FILTER_ID = {1} AND IMS_ATTRFILTER_VALUE.F_OBJECT_ID = ABS({0}.F_OBJECT_ID))) OR (ABS({0}.F_OBJECT_ID) NOT IN (SELECT F_OBJECT_ID FROM IMS_ATTRFILTER_VALUE WHERE IMS_ATTRFILTER_VALUE.F_OBJECT_ID = ABS({0}.F_OBJECT_ID))))", (object) this.SystemTableAlias, (object) str1);
            this.PreparedParams.Add(db.Parameter(str1, condition.Value));
          }
          else if (condition.RelationalOperator == RelationalOperators.Linked || condition.RelationalOperator == RelationalOperators.NotLinked)
          {
            string str11 = condition.RelationalOperator != RelationalOperators.NotLinked ? string.Empty : "NOT";
            if (condition.Value is int)
            {
              List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive((int) condition.Value);
              string str12;
              if (childrenIdRecursive.Count == 1)
              {
                index1 = childrenIdRecursive[0];
                str12 = "= " + index1.ToString();
              }
              else
              {
                StringBuilder stringBuilder5 = new StringBuilder();
                stringBuilder5.Append("IN (");
                for (int index3 = 0; index3 < childrenIdRecursive.Count; ++index3)
                {
                  StringBuilder stringBuilder6 = stringBuilder5;
                  index1 = childrenIdRecursive[index3];
                  string str13 = index1.ToString() + ",";
                  stringBuilder6.Append(str13);
                }
                --stringBuilder5.Length;
                stringBuilder5.Append(")");
                str12 = stringBuilder5.ToString();
              }
              stringBuilder1.AppendFormat("(ABS({0}.F_OBJECT_ID) {1} IN (SELECT LNK1.F_TOOBJECT_ID FROM IMS_OBJECT_LINKS LNK1, IMS_OBJECTS OLNK2 WHERE LNK1.F_TOOBJECT_ID = ABS({0}.F_OBJECT_ID) AND OLNK2.F_OBJECT_ID = LNK1.F_OBJECT_ID AND OLNK2.F_OBJECT_TYPE {2})) OR ({0}.F_ID {1} IN (SELECT LNK1.F_TO_ID FROM IMS_ID_LINKS IDLNK1, IMS_OBJECTS IDOLNK2 WHERE IDLNK1.F_TO_ID = {0}.F_ID AND IDOLNK2.F_OBJECT_ID = IDLNK1.F_OBJECT_ID AND IDOLNK2.F_OBJECT_TYPE {2}))", (object) this.SystemTableAlias, (object) str11, (object) str12);
            }
            else
              stringBuilder1.AppendFormat("(ABS({0}.F_OBJECT_ID) {1} IN (SELECT F_TOOBJECT_ID FROM IMS_OBJECT_LINKS WHERE IMS_OBJECT_LINKS.F_TOOBJECT_ID = ABS({0}.F_OBJECT_ID))) OR ({0}.F_ID {1} IN (SELECT F_TO_ID FROM IMS_ID_LINKS WHERE IMS_ID_LINKS.F_TO_ID = {0}.F_ID))", (object) this.SystemTableAlias, (object) str11);
          }
          else if (condition.RelationalOperator == RelationalOperators.InLCHistory)
          {
            if (!(condition.Value is LC_ConditionParams lcConditionParams))
              throw new KernelException("Для оператора RelationalOperators.InLCHistory значение Value должно быть типа LC_ConditionParams");
            string str14;
            if (lcConditionParams.LCStepID.HasValue)
            {
              str14 = "LCD1.F_LC_STEP = " + str1;
              this.PreparedParams.Add(db.Parameter(str1, (object) lcConditionParams.LCStepID.Value));
            }
            else
            {
              str14 = $"(LCD1.F_LC_STEP IN (SELECT IMS_LC_STEPS.F_LC_STEP FROM IMS_LC_STEPS WHERE IMS_LC_STEPS.F_LEVEL_ID = {str1}))";
              this.PreparedParams.Add(db.Parameter(str1, (object) lcConditionParams.LevelID.Value));
            }
            index1 = num1++;
            string parameterName3 = ":p" + index1.ToString();
            string str15;
            switch (lcConditionParams.DateOperator)
            {
              case RelationalOperators.Equal:
                index1 = num1++;
                string parameterName4 = ":p" + index1.ToString();
                this.PreparedParams.Add(db.Parameter(parameterName4, (object) (lcConditionParams.BeginDate + TimeSpan.FromDays(1.0))));
                str15 = $"BETWEEN {parameterName3} AND {parameterName4}";
                break;
              case RelationalOperators.NotEqual:
                index1 = num1++;
                string parameterName5 = ":p" + index1.ToString();
                this.PreparedParams.Add(db.Parameter(parameterName5, (object) (lcConditionParams.BeginDate + TimeSpan.FromDays(1.0))));
                str15 = $"NOT BETWEEN {parameterName3} AND {parameterName5}";
                break;
              case RelationalOperators.Greater:
              case RelationalOperators.GreaterOrEqual:
              case RelationalOperators.Less:
              case RelationalOperators.LessOrEqual:
                str15 = string.Format(RelationalOperatorsHelper.SQLOperator(lcConditionParams.DateOperator), (object) parameterName3);
                break;
              case RelationalOperators.Between:
                index1 = num1++;
                string parameterName6 = ":p" + index1.ToString();
                this.PreparedParams.Add(db.Parameter(parameterName6, (object) (lcConditionParams.EndDate.Value + TimeSpan.FromDays(1.0))));
                str15 = $"BETWEEN {parameterName3} AND {parameterName6}";
                break;
              case RelationalOperators.LastNDays:
                lcConditionParams.BeginDate = DateTime.Today - TimeSpan.FromDays((double) (lcConditionParams.LastNDays - 1));
                str15 = ">= " + parameterName3;
                break;
              default:
                throw new KernelException("В условии поиска по истории ЖЦ применено неверное условие поиска по дате: " + lcConditionParams.DateOperator.ToString());
            }
            this.PreparedParams.Add(db.Parameter(parameterName3, (object) lcConditionParams.BeginDate));
            string str16 = "LCD1.F_START_DATE " + str15;
            stringBuilder1.AppendFormat("(ABS({0}.F_OBJECT_ID) IN (SELECT F_OBJECT_ID FROM IMS_LCSTART_DATE LCD1 WHERE LCD1.F_OBJECT_ID = ABS({0}.F_OBJECT_ID) AND {1} AND {2}))", (object) this.SystemTableAlias, (object) str14, (object) str16);
          }
          else if (condition.SQL == "")
          {
            DBAttributeType attributeType = this.attributeTypes.GetAttributeType(condition.Attribute, true) as DBAttributeType;
            if (flag1 && attributeType != null && (attributeType.MultipleValued == MultiValueModes.MultiValues || attributeType.MultipleValued == MultiValueModes.MultiValuesFromList))
              throw new KernelExceptionID(sc_13066.ssp_appserver_13085(511885588), (object) attributeType.Name);
            this.PrepareConditionValue(ref condition, attributeType);
            stringBuilder1.Append("(");
            string str17;
            Attribute4ID attrStruct;
            string str18;
            string str19;
            string str20;
            if (condition.AttributeSource == AttributeSourceTypes.Relation)
            {
              str17 = this.RelationsTableAlias;
              attrStruct = this.relationsIDstruct;
              str18 = "F_PRJLINK_ID";
              str19 = this.RelationAttributesAlias;
              str20 = "IMS_RELATION_ATTRS";
            }
            else
            {
              attrStruct = this.IDstruct;
              str17 = this.SystemTableAlias;
              str18 = "F_OBJECT_ID";
              str19 = this.ObjectAttributesAlias;
              str20 = this.ObjectAttributesTable;
            }
            attrStruct.AttributeID = attributeType.AttributeID;
            OptimizationModes optimizationMode = this.UserSession.DBCache.GetOptimizationMode(attrStruct);
            string str21 = attributeType.ValueFieldName;
            bool flag2 = true;
            bool flag3 = false;
            bool flag4 = str21 == "F_STRING_VALUE";
            if (attributeType.AttributeID > 0)
            {
              if (condition.RelationalOperator == RelationalOperators.Empty)
                flag2 = false;
              else if (attributeType.MultipleValued != MultiValueModes.MultiValues && attributeType.MultipleValued != MultiValueModes.MultiValuesFromList || condition.RelationalOperator == RelationalOperators.NotExistsOrEmpty || condition.Value2 is PacketCondition)
              {
                if (optimizationMode == OptimizationModes.Read || optimizationMode == OptimizationModes.Seek)
                {
                  switch (condition.Content)
                  {
                    case ColumnContents.Text:
                      index1 = attributeType.AttributeID;
                      str21 = "F" + index1.ToString();
                      break;
                    case ColumnContents.ID:
                      str21 = $"F{attributeType.AttributeID}ID";
                      flag4 = false;
                      break;
                    case ColumnContents.Date:
                      str21 = $"F{attributeType.AttributeID}ID3";
                      flag4 = false;
                      break;
                    case ColumnContents.Value:
                      str21 = $"F{attributeType.AttributeID}ID2";
                      flag4 = false;
                      break;
                    case ColumnContents.String:
                      index1 = attributeType.AttributeID;
                      str21 = "F" + index1.ToString();
                      break;
                  }
                }
                else
                  flag2 = false;
              }
              else
                flag2 = false;
              if (!flag2)
              {
                if (flag1)
                  throw new KernelExceptionID(449, (object) attributeType.Name);
                if (condition.Value2 is PacketCondition)
                  throw new KernelException($"Атрибут '{attributeType.Name}' не может быть использован для пакетного чтения в данном контексте. Измените сортировку в запросе или обратитесь к администратору системы.");
                if (condition.RelationalOperator == RelationalOperators.NotExistsOrEmpty)
                {
                  string str22 = attrStruct.ObjectTypeID < 0 ? (attrStruct.RelationTypeID < 0 ? " списка атрибутов " : $" типа связей  '{this.UserSession.GetRelationType(attrStruct.RelationTypeID, true).Description}' ") : $" типа объектов '{this.UserSession.GetObjectType(attrStruct.ObjectTypeID, true).ObjectTypeName}' ";
                  throw new KernelExceptionID(305, (object) RelationalOperatorsHelper.GetCaption(RelationalOperators.NotExistsOrEmpty), (object) attributeType.Name, (object) str22);
                }
                switch (condition.Content)
                {
                  case ColumnContents.Text:
                    str21 = attributeType.TextFieldName;
                    break;
                  case ColumnContents.ID:
                    str21 = "F_INTEGER_VALUE";
                    flag4 = false;
                    break;
                  case ColumnContents.Date:
                    str21 = "F_DATE_VALUE";
                    flag4 = false;
                    break;
                  case ColumnContents.Value:
                    str21 = "F_DOUBLE_VALUE";
                    flag4 = false;
                    break;
                  case ColumnContents.String:
                    str21 = "F_STRING_VALUE";
                    break;
                }
                string str23;
                if (condition.RelationalOperator == RelationalOperators.NotEndString || condition.RelationalOperator == RelationalOperators.NotStartString || condition.RelationalOperator == RelationalOperators.NotSubstring)
                {
                  str23 = "NOT ";
                  flag3 = true;
                }
                else
                  str23 = " ";
                stringBuilder1.AppendFormat("{0}.{1} {5}IN (SELECT {2}.{1} FROM {3} {2} WHERE {2}.F_ATTRIBUTE_ID = {4} AND ", (object) str17, (object) str18, (object) str19, (object) str20, (object) attributeType.AttributeID, (object) str23);
                str17 = str19;
                --condition.GroupID;
                ++num2;
              }
            }
            else if (attributeType.AttributeID == -50 && this.SystemTableName == "IMS_OBJECTS")
            {
              stringBuilder1.AppendFormat("{0}.{1} IN (SELECT IMS_GUID.{1} FROM IMS_GUID WHERE IMS_GUID.{1} = {0}.{1} AND ", (object) str17, (object) str18);
              str17 = "IMS_GUID";
              str21 = "CAPTION";
              --condition.GroupID;
              ++num2;
            }
            else if (attributeType.AttributeID == -12 && this.SystemTableName == "IMS_OBJECTS")
            {
              stringBuilder1.AppendFormat("{0}.{1} IN (SELECT IMS_GUID.{1} FROM IMS_GUID WHERE IMS_GUID.{1} = {0}.{1} AND ", (object) str17, (object) str18);
              str17 = "IMS_GUID";
              str21 = "F_GUID";
              --condition.GroupID;
              ++num2;
            }
            else if (attributeType.AttributeID == -18)
            {
              stringBuilder1.AppendFormat("{0}.F_ID IN (SELECT IMS_GUID_RESOLVE.F_ID FROM IMS_GUID_RESOLVE WHERE IMS_GUID_RESOLVE.F_CATEGORY_TYPE = 2 AND ", (object) str17);
              str17 = "IMS_GUID_RESOLVE";
              str21 = "F_GUID";
              --condition.GroupID;
              ++num2;
            }
            else
            {
              if (attributeType.AttributeID == -10)
                throw new KernelException("Атрибут 'Дата модификации объекта' устарел и не может использоваться в поисковых запросах.");
              if (attributeType.AttributeID == -83)
              {
                stringBuilder1.AppendFormat("{0}.{1} IN (SELECT IMS_VERSIONS_TREE.{1} FROM IMS_VERSIONS_TREE WHERE IMS_VERSIONS_TREE.{1} = {0}.{1} AND ", (object) str17, (object) str18);
                str17 = "IMS_VERSIONS_TREE";
                str21 = "F_PARENT_ID";
                --condition.GroupID;
                ++num2;
              }
              else if (ObligatoryObjectAttributesHelper.GetDataType((ObligatoryObjectAttributes) attributeType.AttributeID) == FieldTypes.ftString)
                flag4 = true;
            }
            string str24 = string.Empty;
            string str25;
            if (condition.RelationalOperator == RelationalOperators.Between || condition.RelationalOperator == RelationalOperators.NotBetween)
            {
              if (condition.Value2 is MeasuredValue)
              {
                MeasuredValue mValue = condition.Value2 as MeasuredValue;
                MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(mValue);
                if (condition.Value is MeasuredValue && mValue.MeasureID != (condition.Value as MeasuredValue).MeasureID)
                  throw new KernelExceptionID(308, (object) (condition.Value as MeasuredValue).Caption, (object) mValue.Caption);
                condition.Value2 = (object) (mValue.Value * descriptor.K);
              }
              string parameterName = str1;
              index1 = num1++;
              str1 = ":p" + index1.ToString();
              str25 = string.Format(RelationalOperatorsHelper.SQLOperator(condition.RelationalOperator), (object) str1, (object) parameterName);
              this.PreparedParams.Add(db.Parameter(parameterName, condition.Value2));
            }
            else if (((condition.RelationalOperator == RelationalOperators.Empty || condition.RelationalOperator == RelationalOperators.NotExistsOrEmpty ? 1 : (condition.RelationalOperator == RelationalOperators.NotEmpty ? 1 : 0)) & (flag4 ? 1 : 0)) != 0)
            {
              str1 = string.Empty;
              str25 = string.Format(RelationalOperatorsHelper.SQLOperator(condition.RelationalOperator), (object) str1);
              if (db.DataProvider.CanStoreEmptyString)
              {
                str25 = condition.RelationalOperator == RelationalOperators.Empty || condition.RelationalOperator == RelationalOperators.NotExistsOrEmpty ? $"{str25} OR {str17}.{str21} = '')" : $"{str25} AND {str17}.{str21} <> '')";
                str24 = "(";
              }
            }
            else if (condition.RelationalOperator == RelationalOperators.In || condition.RelationalOperator == RelationalOperators.NotIn)
            {
              if (condition.Value is INConditionValue inConditionValue1 && inConditionValue1.SelectKey > 0L)
              {
                INConditionValue inConditionValue = condition.Value as INConditionValue;
                index1 = num1++;
                string parameterName = ":p" + index1.ToString();
                this.PreparedParams.Add(db.Parameter(parameterName, (object) inConditionValue.SelectKey));
                str25 = string.Format(RelationalOperatorsHelper.SQLOperator(condition.RelationalOperator), (object) $"SELECT F_VALUE FROM {inConditionValue.TmpTableName} WHERE F_KEY = {parameterName}");
                str1 = string.Empty;
              }
              else if (this.SelectConditionIndex == ndx)
              {
                index1 = num1++;
                string parameterName = ":p" + index1.ToString();
                this.PreparedParams.Add(db.Parameter(parameterName, (object) this.SelectKeyValue));
                str25 = string.Format(RelationalOperatorsHelper.SQLOperator(condition.RelationalOperator), (object) $"SELECT F_VALUE FROM {this.TemporaryTableName} WHERE F_KEY = {parameterName}");
                str1 = string.Empty;
              }
              else
              {
                StringBuilder stringBuilder7 = new StringBuilder();
                Array array = inConditionValue1 == null ? (Array) condition.Value : inConditionValue1.Values;
                for (int index4 = 0; index4 < array.Length; ++index4)
                {
                  if (index4 > 0)
                    stringBuilder7.Append(",");
                  index1 = num1++;
                  string parameterName = ":p" + index1.ToString();
                  stringBuilder7.Append(parameterName);
                  object obj = array.GetValue(index4);
                  if (attributeType.AttributeType == FieldTypes.ftGuid && obj is Guid)
                    obj = (object) obj.ToString();
                  this.PreparedParams.Add(db.Parameter(parameterName, obj));
                }
                str25 = string.Format(RelationalOperatorsHelper.SQLOperator(condition.RelationalOperator), (object) stringBuilder7.ToString());
                str1 = string.Empty;
              }
            }
            else
              str25 = !flag3 ? string.Format(RelationalOperatorsHelper.SQLOperator(condition.RelationalOperator), (object) str1) : string.Format(RelationalOperatorsHelper.SQLOperator(RelationalOperators.Substring), (object) str1);
            if (attributeType.AttributeType == FieldTypes.ftObjectLink)
              this.CorrectObjectLinkCondition(ref condition);
            if (attributeType.AttributeType == FieldTypes.ftMeasured && condition.Value is MeasuredValue)
            {
              MeasuredValue mValue = condition.Value as MeasuredValue;
              MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(mValue);
              condition.Value = (object) (mValue.Value * descriptor.K);
              if (attributeType.SizeType <= 0L)
              {
                MeasureDescriptor baseValue = MeasureHelper.FindBaseValue(descriptor);
                index1 = num1++;
                string parameterName = ":p" + index1.ToString();
                this.PreparedParams.Add(db.Parameter(parameterName, (object) baseValue.MeasureID));
                string str26 = !(str21 == "F_DOUBLE_VALUE") ? $"F{attributeType.AttributeID}ID" : "F_INTEGER_VALUE";
                stringBuilder1.AppendFormat("{0}.{1} {2} AND {3} = {4}", (object) str17, (object) str21, (object) str25, (object) str26, (object) parameterName);
              }
              else
                stringBuilder1.AppendFormat("{0}.{1} {2}", (object) str17, (object) str21, (object) str25);
            }
            else if (attributeType.AttributeType == FieldTypes.ftMemo && condition.RelationalOperator != RelationalOperators.Equal && condition.RelationalOperator != RelationalOperators.Greater && condition.RelationalOperator != RelationalOperators.Less && condition.RelationalOperator != RelationalOperators.NotEmpty && condition.RelationalOperator != RelationalOperators.Empty && condition.RelationalOperator != RelationalOperators.NotExistsOrEmpty && condition.RelationalOperator != RelationalOperators.GreaterOrEqual && condition.RelationalOperator != RelationalOperators.LessOrEqual)
            {
              string str27 = optimizationMode == OptimizationModes.Read || optimizationMode == OptimizationModes.Seek ? $"F{attributeType.AttributeID}ID" : "F_INTEGER_VALUE";
              if (!condition.CaseSensitive && condition.RelationalOperator != RelationalOperators.NotExistsOrEmpty && condition.RelationalOperator != RelationalOperators.NotEmpty && condition.RelationalOperator != RelationalOperators.Empty)
              {
                if (!db.DataProvider.CanUpperMemo)
                  throw new KernelExceptionID(sc_13066.ssp_appserver_13086(371919984));
                stringBuilder1.AppendFormat("{0}.{3} IN (SELECT F_KEY FROM IMS_MEMOS WHERE IMS_MEMOS.F_KEY = {0}.{3} AND UPPER(IMS_MEMOS.F_VALUE) {2})", (object) str17, (object) str21, (object) str25, (object) str27);
              }
              else
                stringBuilder1.AppendFormat("{0}.{3} IN (SELECT F_KEY FROM IMS_MEMOS WHERE IMS_MEMOS.F_KEY = {0}.{3} AND IMS_MEMOS.F_VALUE {2})", (object) str17, (object) str21, (object) str25, (object) str27);
            }
            else if (!condition.CaseSensitive && SqlHelper.IsVarcharField(attributeType.TextFieldName) && condition.Value is string)
              stringBuilder1.AppendFormat("{0}UPPER({1}.{2}) {3}", (object) str24, (object) str17, (object) str21, (object) str25);
            else if (attributeType.IsVirtualAttribute)
              stringBuilder1.AppendFormat("{0} {1}", (object) attributeType.GetSQL(this.UserSession.QueryBuilder.SystemTableAlias), (object) str25);
            else
              stringBuilder1.AppendFormat("{0}{1}.{2} {3}", (object) str24, (object) str17, (object) str21, (object) str25);
            if (!flag1)
            {
              object correctedValue = this.GetCorrectedValue(attributeType, condition, db);
              if (str1 != string.Empty && condition.RelationalOperator != RelationalOperators.Empty && condition.RelationalOperator != RelationalOperators.NotExistsOrEmpty && condition.RelationalOperator != RelationalOperators.NotEmpty)
                this.PreparedParams.Add(db.Parameter(str1, correctedValue));
            }
            if (this.EscapeString != string.Empty)
            {
              stringBuilder1.Append(" " + this.EscapeString);
              this.EscapeString = string.Empty;
            }
            stringBuilder1.Append(")");
          }
          else
            stringBuilder1.AppendFormat("({0})", (object) condition.SQL);
          if (condition.GroupID < 0)
          {
            for (int groupId = condition.GroupID; groupId < 0; ++groupId)
            {
              stringBuilder1.Append(")");
              --num2;
            }
          }
          logicalOperators = condition.LogicalOperator;
        }
      }
      for (int index = 0; index < num2; ++index)
        stringBuilder1.Append(")");
    }
    if (this.TypeFilter != "")
    {
      string str = "";
      if (stringBuilder1.Length > 0)
      {
        str = " AND ";
        if (conditions != null && conditions.Length > 1)
        {
          stringBuilder1.Append(")");
          str += "(";
        }
      }
      stringBuilder1.Insert(0, this.TypeFilter + str);
    }
    this.TypeFilter = "";
    if (stringBuilder1.Length > 0)
      stringBuilder1.Insert(0, " WHERE ");
    return stringBuilder1.ToString();
  }

  private string GetGlobalIndexSQL(ConditionStructure condition, int ndx)
  {
    List<long> words_id = new List<long>();
    GlobalIndexSearchValue condition1 = condition.Value as GlobalIndexSearchValue;
    bool flag1 = condition1.Value.Length > 1 && condition1.Value.ToUpper()[0] == 'N';
    StringBuilder stringBuilder1 = new StringBuilder();
    StringBuilder stringBuilder2 = (StringBuilder) null;
    IDbManager dataManager = this.UserSession.DataManager;
    if ((condition1.SearchOptions & GlobalIndexSearchOptions.OrderByRelevance) == GlobalIndexSearchOptions.OrderByRelevance)
      stringBuilder2 = new StringBuilder();
    string[] strArray;
    if (flag1)
      strArray = new string[1]{ condition1.Value };
    else
      strArray = this.UserSession.GlobalIndex.GetWords(condition);
    if (strArray.Length == 1 & flag1)
    {
      string str = strArray[0].Substring(1);
      long result1;
      if (long.TryParse(str, out result1))
        return string.Format("(({0}.F_OBJECT_ID IN ({1}, -{1})) OR ({0}.F_ID = {1}))", (object) this.SystemTableAlias, (object) Math.Abs(result1));
      Guid result2;
      if (Guid.TryParse(str, out result2))
        return string.Format("(({0}.F_GUID = '{1}') OR ({0}.F_ID IN (SELECT IMS_GUID_RESOLVE.F_ID FROM IMS_GUID_RESOLVE WHERE IMS_GUID_RESOLVE.F_CATEGORY_TYPE = 2 AND IMS_GUID_RESOLVE.F_GUID = '{1}')))", (object) this.SystemTableAlias, (object) result2);
    }
    if (strArray.Length > 1)
    {
      string word1 = condition1.Value.Trim();
      if (word1.Length >= this.UserSession.GlobalIndex.MinWordLength)
      {
        this.UserSession.GlobalIndex.GetSQLforWord((IUserSession) this.UserSession, word1, condition1, words_id);
        if (words_id.Count > 0 && words_id.Count < dataManager.DataProvider.MaximumINOperands)
        {
          stringBuilder1.AppendFormat("({0}.F_OBJECT_ID IN (SELECT IR.F_OBJECT_ID FROM IMS_INDEX_RESULT IR WHERE IR.F_OBJECT_ID = {0}.F_OBJECT_ID AND IR.F_WORD_ID IN (", (object) this.SystemTableAlias);
          for (int index = 0; index < words_id.Count; ++index)
          {
            StringBuilder stringBuilder3 = stringBuilder1;
            long num = words_id[index];
            string str1 = num.ToString() + ",";
            stringBuilder3.Append(str1);
            if (stringBuilder2 != null)
            {
              StringBuilder stringBuilder4 = stringBuilder2;
              num = words_id[index];
              string str2 = num.ToString() + ",";
              stringBuilder4.Append(str2);
            }
          }
          --stringBuilder1.Length;
          stringBuilder1.Append(")))");
          (ServerServices.GetService(typeof (IDelayedUpdaterService)) as DelayedUpdaterService).AddSearchQuery(new SearchQueryProperties((condition.Value as GlobalIndexSearchValue).Value, this.UserSession.UserID, DateTime.UtcNow, this.UserSession.SecurityLevel));
          return stringBuilder1.ToString();
        }
        words_id.Clear();
      }
    }
    bool flag2 = false;
    bool flag3 = true;
    for (int index1 = 0; index1 < strArray.Length; ++index1)
    {
      if (strArray[index1].Length >= this.UserSession.GlobalIndex.MinWordLength)
      {
        this.UserSession.GlobalIndex.GetSQLforWord((IUserSession) this.UserSession, strArray[index1], condition1, words_id);
        flag3 = false;
        if (words_id.Count >= dataManager.DataProvider.MaximumINOperands)
        {
          this.NeedDeleteTemp = true;
          if ((condition1.SearchOptions & GlobalIndexSearchOptions.OrderByRelevance) == GlobalIndexSearchOptions.OrderByRelevance)
            throw new KernelExceptionID(sc_13066.ssp_appserver_13087(485553891));
          flag2 = !flag2 ? true : throw new KernelExceptionID(sc_13066.ssp_appserver_13088(609201890), (object) condition1.Value);
          this.SelectConditionIndex = this.SelectConditionIndex <= -1 ? ndx : throw new KernelException("Too many IN or InGlobalIndex operators with too many operands.");
          this.SelectKeyValue = this.UserSession.QueryBuilder.SelectKey;
          this.TemporaryTableName = "IMS_TMP_INTEGER";
          IDbDataParameter dbDataParameter1 = dataManager.Parameter("fkey", (object) this.SelectKeyValue);
          IDbDataParameter dbDataParameter2 = dataManager.Parameter("fvalue", (object) words_id[0]);
          string commandText = $"INSERT INTO {this.TemporaryTableName} (F_KEY, F_VALUE) VALUES (:fkey, :fvalue)";
          for (int index2 = 0; index2 < words_id.Count; ++index2)
          {
            dbDataParameter2.Value = (object) words_id[index2];
            dataManager.ExecuteNonQuery(commandText, dbDataParameter1, dbDataParameter2);
          }
          stringBuilder1.AppendFormat("({0}.F_OBJECT_ID IN (SELECT IR.F_OBJECT_ID FROM IMS_INDEX_RESULT IR WHERE IR.F_OBJECT_ID = {0}.F_OBJECT_ID AND IR.F_WORD_ID IN (SELECT F_VALUE FROM IMS_TMP_INTEGER WHERE F_KEY = {1})))", (object) this.SystemTableAlias, (object) this.SelectKeyValue);
        }
        else
        {
          if (words_id.Count == 0)
            words_id.Add(-1L);
          StringBuilder stringBuilder5 = new StringBuilder(string.Format("({0}.F_OBJECT_ID IN (SELECT IR.F_OBJECT_ID FROM IMS_INDEX_RESULT IR WHERE IR.F_OBJECT_ID = {0}.F_OBJECT_ID AND IR.F_WORD_ID IN (", (object) this.SystemTableAlias));
          for (int index3 = 0; index3 < words_id.Count; ++index3)
          {
            StringBuilder stringBuilder6 = stringBuilder5;
            long num = words_id[index3];
            string str3 = num.ToString() + ",";
            stringBuilder6.Append(str3);
            if (stringBuilder2 != null)
            {
              StringBuilder stringBuilder7 = stringBuilder2;
              num = words_id[index3];
              string str4 = num.ToString() + ",";
              stringBuilder7.Append(str4);
            }
          }
          --stringBuilder5.Length;
          stringBuilder1.Append(stringBuilder5.ToString() + ")))");
        }
        stringBuilder1.Append(" AND ");
      }
    }
    if (flag3)
    {
      if (condition1.Value.Trim().Length < this.UserSession.GlobalIndex.MinWordLength)
        throw new KernelExceptionID(sc_13066.ssp_appserver_13089(1073901108), (object) this.UserSession.GlobalIndex.MinWordLength);
      throw new KernelException(string.Format(sc_13066.ssp_appserver_13090(), (object) this.UserSession.GlobalIndex.MinWordLength));
    }
    stringBuilder1.Length -= 5;
    if (stringBuilder2 != null)
    {
      --stringBuilder2.Length;
      this.WordsIDs = stringBuilder2.ToString();
    }
    else
      this.WordsIDs = string.Empty;
    (ServerServices.GetService(typeof (IDelayedUpdaterService)) as DelayedUpdaterService).AddSearchQuery(new SearchQueryProperties((condition.Value as GlobalIndexSearchValue).Value, this.UserSession.UserID, DateTime.UtcNow, this.UserSession.SecurityLevel));
    return stringBuilder1.ToString();
  }
}
