// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.MiscFunx
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Expert;
using Intermech.Expressions;
using Intermech.Interfaces;
using Intermech.Interfaces.Contexts;
using Intermech.Interfaces.Expert;
using Intermech.Interfaces.WebPortal;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel.Search;
using Intermech.Signs.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.Workflow;

/// <summary>Summary description for MiscFunx.</summary>
public class MiscFunx
{
  private static Array _varTypes = Enum.GetValues(typeof (VarType));
  /// <summary>
  /// Идентификатор последнего сценария, зачитанного через GetScriptCode. Может быть неточным при многопоточном выполнении!
  /// </summary>
  public static long LastScriptID = 0;
  private static List<int> _groupingObjectsApplicableRelations = (List<int>) null;

  public static string ActivityIncomplete(string name)
  {
    return string.Format(LocalizationHolder.rm.GetString("Workflow.Design_70"), (object) name);
  }

  public static void AddNewLined(ref string s, string addition)
  {
    if (!(addition != ""))
      return;
    if (s != "")
      s += "\r\n";
    s += addition;
  }

  public static DataTable SimpleSelect(
    IUserSession sess,
    int ObjType,
    int AttributeID,
    RelationalOperators op,
    object Value)
  {
    ConditionStructure[] conds = new ConditionStructure[1]
    {
      new ConditionStructure(AttributeID, op, Value, LogicalOperators.AND, 0, false)
    };
    return MiscFunx.SimpleSelect(sess, ObjType, conds);
  }

  public static DataTable SimpleSelect(
    IUserSession sess,
    int ObjType,
    ConditionStructure[] conds,
    HybridDictionary tags = null)
  {
    object[] columns = new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    };
    return MiscFunx.SimpleSelect(sess, ObjType, columns, conds, tags);
  }

  public static DataTable SimpleSelect(
    IUserSession sess,
    int ObjType,
    object[] columns,
    ConditionStructure[] conds,
    HybridDictionary tags = null,
    int recordCount = -999)
  {
    IDBObjectCollection objectCollection = sess.GetObjectCollection(ObjType);
    object[] sortColumns = new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    };
    DBRecordSetParams paramSet = new DBRecordSetParams(conds, columns, sortColumns, new SortOrders[1]
    {
      SortOrders.ASC
    });
    paramSet.RecordCount = recordCount == -999 ? -1 : recordCount;
    if (tags != null)
      paramSet.Tags = tags;
    return objectCollection.Select(paramSet);
  }

  public static bool IsVariableUsed(IUserSession session, int VarAttrTypeID, long exceptProcessID)
  {
    return MiscFunx.GetVariableApplicability(session, VarAttrTypeID, exceptProcessID, 1, true).Length != 0;
  }

  /// <summary>
  /// Возвращает список идентификаторов процессов/шаблонов, в которых применяется данная переменная
  /// </summary>
  /// <param name="session"></param>
  /// <param name="VarAttrTypeID"></param>
  /// <param name="exceptProcessID"></param>
  /// <returns></returns>
  public static Dictionary<long, string>[] GetVariableApplicability(
    IUserSession session,
    int VarAttrTypeID,
    long exceptProcessID,
    int limit,
    bool includeIndefiniteProcesses = false)
  {
    Dictionary<long, string>[] variableApplicability = new Dictionary<long, string>[2]
    {
      new Dictionary<long, string>(),
      new Dictionary<long, string>()
    };
    string conditionValue = $",{VarAttrTypeID:X},";
    object[] columns = new object[2]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) ObligatoryObjectAttributes.CAPTION
    };
    ConditionStructure[] array = new ConditionStructure[1]
    {
      new ConditionStructure(wfConsts.AttrVariablesID, RelationalOperators.Substring, (object) conditionValue, LogicalOperators.OR, 0, true)
    };
    if (includeIndefiniteProcesses)
    {
      Array.Resize<ConditionStructure>(ref array, array.Length + 1);
      array[array.Length - 1] = new ConditionStructure(wfConsts.AttrVariablesID, RelationalOperators.Substring, (object) "..", LogicalOperators.AND, 0, true);
    }
    DBRecordSetParams paramSet = new DBRecordSetParams(array, columns, 0L, (object) null, limit == 0 ? -1 : limit);
    foreach (DataRow row in (InternalDataCollectionBase) session.GetObjectCollection(wfConsts.SchemesTypeID).Select(paramSet).Rows)
    {
      long int64 = Convert.ToInt64(row[0]);
      if (exceptProcessID != int64)
      {
        variableApplicability[0].Add(int64, row[1].ToString());
        if (limit != 0 && variableApplicability.Length >= limit)
          return variableApplicability;
      }
    }
    foreach (DataRow row in (InternalDataCollectionBase) session.GetObjectCollection(wfConsts.ProcessesTypeID).Select(paramSet).Rows)
    {
      long int64 = Convert.ToInt64(row[0]);
      if (exceptProcessID != int64)
      {
        variableApplicability[1].Add(int64, row[1].ToString());
        if (limit != 0 && variableApplicability.Length >= limit)
          return variableApplicability;
      }
    }
    return variableApplicability;
  }

  public static string GetVariableApplicabilityString(
    IUserSession sess,
    int VarAttrTypeID,
    long exceptProcessID)
  {
    string applicabilityString = string.Empty;
    Dictionary<long, string>[] variableApplicability = MiscFunx.GetVariableApplicability(sess, VarAttrTypeID, exceptProcessID, 0, true);
    string[] strArray = new string[2]
    {
      LocalizationHolder.rm.GetString("InSchemes"),
      LocalizationHolder.rm.GetString("InProcesses")
    };
    for (int index = 0; index < variableApplicability.Length; ++index)
    {
      Dictionary<long, string> dictionary = variableApplicability[index];
      if (dictionary.Count > 0)
      {
        if (applicabilityString != "")
          applicabilityString += ",\r\n";
        string str = $"{applicabilityString}{strArray[index]}: ";
        string empty = string.Empty;
        foreach (KeyValuePair<long, string> keyValuePair in dictionary)
        {
          if (empty != "")
            empty += ", ";
          empty += $"\"{keyValuePair.Value}\" ({keyValuePair.Key})";
        }
        applicabilityString = str + empty;
      }
    }
    return applicabilityString;
  }

  public static string[] GetVariableApplicabilityStrings(
    IUserSession sess,
    int VarAttrTypeID,
    long exceptProcessID)
  {
    List<string> stringList = new List<string>();
    Dictionary<long, string>[] variableApplicability = MiscFunx.GetVariableApplicability(sess, VarAttrTypeID, exceptProcessID, 0, true);
    string[] strArray = new string[2]
    {
      LocalizationHolder.rm.GetString("InSchemes"),
      LocalizationHolder.rm.GetString("InProcesses")
    };
    for (int index = 0; index < variableApplicability.Length; ++index)
    {
      Dictionary<long, string> dictionary = variableApplicability[index];
      if (dictionary.Count > 0)
      {
        stringList.Add(strArray[index] + ":");
        foreach (KeyValuePair<long, string> keyValuePair in dictionary)
          stringList.Add($"\"{keyValuePair.Value}\" ({keyValuePair.Key})");
      }
    }
    return stringList.ToArray();
  }

  public static event MiscFunx.ReloadVariablesCacheEvent ReloadVariablesCacheNeeded;

  /// <summary>
  /// Используется для перечитки кэшей атрибутов (у MetadataHelper и сессии), если находимся на клиенте, после программного создания переменных
  /// </summary>
  public static void ReloadVariablesCache(IUserSession session)
  {
    MiscFunx.ReloadVariablesCacheEvent variablesCacheNeeded = MiscFunx.ReloadVariablesCacheNeeded;
    if (variablesCacheNeeded == null)
      return;
    variablesCacheNeeded(session);
  }

  /// <summary>
  /// Возвращает непосредственный состав группы с идентификатором версии объекта GroupID
  /// </summary>
  private static DataTable ExpandGroup(IDBRelationCollection relColl, long GroupID)
  {
    DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[2]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) ObligatoryObjectAttributes.F_OBJECT_TYPE
    }, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    }, new SortOrders[1]{ SortOrders.ASC });
    return relColl.ConsistFrom(paramSet, GroupID, false);
  }

  /// <summary>получение из ID группы всего списка пользователей</summary>
  /// <param name="groupID"></param>
  /// <param name="userSession"></param>
  /// <returns></returns>
  private static DataTable ExpandGroup(long groupID, IUserSession userSession)
  {
    int objectTypeId1 = MetaDataHelper.GetObjectTypeID("cad00002-306c-11d8-b4e9-00304f19f545");
    int objectTypeId2 = MetaDataHelper.GetObjectTypeID("cad00003-306c-11d8-b4e9-00304f19f545");
    DataTable dataTable1 = new DataTable("Группы");
    DataTable dataTable2 = new DataTable("Пользователи");
    object[] columns = new object[2]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) ObligatoryObjectAttributes.F_OBJECT_TYPE
    };
    object[] sortColumns = new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    };
    IDBObjectCollection objectCollection = userSession.GetObjectCollection(objectTypeId1);
    DataTable childGroupRecursive = MiscFunx.GetChildGroupRecursive(userSession.GetObjectCollection(objectTypeId2), (object) groupID);
    List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
    if (userSession.IsSystemSession)
    {
      ConditionStructure conditionStructure = new ConditionStructure(-4, RelationalOperators.NotEqual, (object) MetaDataHelper.GetLCStep(new Guid("cadd9504-306c-11d8-b4e9-00304f19f545")).LCStepID, LogicalOperators.AND, 0, false);
      conditionStructureList.Add(conditionStructure);
    }
    conditionStructureList.Add(new ConditionStructure(wfConsts.AttrExternalUserID, RelationalOperators.NotEqual, (object) true, LogicalOperators.AND, 0, false));
    bool flag = false;
    for (int index = 0; index < childGroupRecursive.Rows.Count; ++index)
    {
      conditionStructureList.Add(new ConditionStructure((string) null, RelationalOperators.EntersIn, childGroupRecursive.Rows[index].ItemArray[0], LogicalOperators.OR, flag ? 0 : 1, false));
      flag = true;
    }
    conditionStructureList.Add(new ConditionStructure((string) null, RelationalOperators.EntersIn, (object) groupID, LogicalOperators.NONE, flag ? -1 : 0, false));
    return objectCollection.Select(new DBRecordSetParams(conditionStructureList.ToArray(), columns, sortColumns, new SortOrders[1]
    {
      SortOrders.ASC
    }));
  }

  /// <summary>
  /// Рекурсивно раскрутим и получим DataTable со списком групп которые входят в начальную группу
  /// </summary>
  /// <param name="groupCollection"></param>
  /// <param name="conditionValue"></param>
  /// <returns></returns>
  private static DataTable GetChildGroupRecursive(
    IDBObjectCollection groupCollection,
    object conditionValue)
  {
    DataTable childGroupRecursive1 = new DataTable("Список всех дочерних групп");
    DataTable table = groupCollection.Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure((string) null, RelationalOperators.EntersIn, conditionValue, LogicalOperators.NONE, 0, false)
    }, new object[2]{ (object) -2, (object) -50 }));
    for (int index = 0; index < table.Rows.Count; ++index)
    {
      DataRow row = table.Rows[index];
      DataTable childGroupRecursive2 = MiscFunx.GetChildGroupRecursive(groupCollection, row.ItemArray[0]);
      table.Merge(childGroupRecursive2);
    }
    childGroupRecursive1.Merge(table);
    return childGroupRecursive1;
  }

  /// <summary>
  /// Возвращает развернутый состав группы с идентификатором версии объекта GroupID
  /// (Группы, входящие в состав, также раскрываются)
  /// </summary>
  public static List<long> ExpandGroup(IUserSession session, long GroupID)
  {
    IDBRelationCollection relationCollection = session.GetRelationCollection(session.IdentHelper.SimpleRelationTypeID);
    List<long> longList1 = new List<long>();
    longList1.Add(GroupID);
    List<long> longList2 = new List<long>();
    for (int index = 0; index < longList1.Count; ++index)
    {
      foreach (DataRow row in (InternalDataCollectionBase) MiscFunx.ExpandGroup(relationCollection, longList1[index]).Rows)
      {
        long int64 = Convert.ToInt64(row[0]);
        if (Convert.ToInt32(row[1]) == wfConsts.GroupTypeID)
        {
          if (!longList1.Contains(int64))
            longList1.Add(int64);
        }
        else if (!longList2.Contains(int64))
          longList2.Add(int64);
      }
    }
    return longList2;
  }

  private static void ExpandParticipant(
    IDBAttributable obj,
    ParticipantList pl,
    int index,
    IDBRelationCollection relColl)
  {
    switch (pl[index].Kind)
    {
      case ParticipantKind.Group:
        long id1 = pl[index].ID;
        pl.RemoveAt(index);
        if (relColl == null)
          relColl = obj.Session.GetRelationCollection(obj.Session.IdentHelper.SimpleRelationTypeID);
        IUserSession session = obj.Session;
        IEnumerator enumerator1 = MiscFunx.ExpandGroup(id1, session).Rows.GetEnumerator();
        try
        {
          while (enumerator1.MoveNext())
          {
            DataRow current = (DataRow) enumerator1.Current;
            ParticipantKind Kind = ParticipantKind.User;
            if (Convert.ToInt32(current[1]) == wfConsts.GroupTypeID)
              Kind = ParticipantKind.Group;
            pl.AddParticipant(Kind, Convert.ToInt64(current[0]));
            if (Kind == ParticipantKind.Group)
              MiscFunx.ExpandParticipant(obj, pl, pl.Count - 1, relColl);
          }
          break;
        }
        finally
        {
          if (enumerator1 is IDisposable disposable)
            disposable.Dispose();
        }
      case ParticipantKind.Variable:
        IDBAttribute attributeById = obj.GetAttributeByID((int) pl[index].ID);
        pl.RemoveAt(index);
        if (attributeById == null)
          break;
        using (IEnumerator<Participant> enumerator2 = new ParticipantList(pl.Session)
        {
          AsString = attributeById.Value.ToString()
        }.GetEnumerator())
        {
          while (enumerator2.MoveNext())
          {
            Participant current = enumerator2.Current;
            pl.Add(current);
            if (current.Kind != ParticipantKind.User)
              MiscFunx.ExpandParticipant(obj, pl, pl.Count - 1, relColl);
          }
          break;
        }
      case ParticipantKind.Rank:
        long id2 = pl[index].ID;
        pl.RemoveAt(index);
        List<ConditionStructure> conditionStructureList = new List<ConditionStructure>()
        {
          new ConditionStructure(wfConsts.AttrExternalUserID, RelationalOperators.NotEqual, (object) true, LogicalOperators.AND, 0, false),
          new ConditionStructure(wfConsts.AttrUserRankID, RelationalOperators.Equal, (object) id2, LogicalOperators.AND, 0, false)
        };
        if (obj.Session.IsSystemSession)
        {
          ConditionStructure conditionStructure = new ConditionStructure(-4, RelationalOperators.NotEqual, (object) MetaDataHelper.GetLCStep(new Guid("cadd9504-306c-11d8-b4e9-00304f19f545")).LCStepID, LogicalOperators.AND, 0, false);
          conditionStructureList.Add(conditionStructure);
        }
        IEnumerator enumerator3 = MiscFunx.SimpleSelect(obj.Session, wfConsts.UserTypeID, conditionStructureList.ToArray()).Rows.GetEnumerator();
        try
        {
          while (enumerator3.MoveNext())
          {
            DataRow current = (DataRow) enumerator3.Current;
            pl.AddParticipant(ParticipantKind.User, Convert.ToInt64(current[0]));
          }
          break;
        }
        finally
        {
          if (enumerator3 is IDisposable disposable)
            disposable.Dispose();
        }
    }
  }

  /// <summary>Expand user groups and variables into pure users</summary>
  /// <param name="pl"></param>
  public static void ExpandParticipants(IDBAttributable obj, ParticipantList pl)
  {
    IDBRelationCollection relationCollection = obj.Session.GetRelationCollection(obj.Session.IdentHelper.SimpleRelationTypeID);
    for (int index = pl.Count - 1; index >= 0; --index)
      MiscFunx.ExpandParticipant(obj, pl, index, relationCollection);
    pl.DeleteEquals();
  }

  private static void ReplaceVariablesByParticipants(
    IUserSession session,
    long objectID,
    ParticipantList pl,
    ref int index,
    List<long> processedIDs)
  {
    if (pl[index].Kind == ParticipantKind.Variable)
    {
      int id = (int) pl[index].ID;
      IDBAttribute objectAttributeById = session.GetObjectAttributeByID(objectID, id);
      pl.RemoveAt(index);
      if (processedIDs.IndexOf((long) id) != -1 || objectAttributeById == null)
        return;
      foreach (Participant participant in new ParticipantList(pl.Session)
      {
        AsString = objectAttributeById.Value.ToString()
      })
      {
        pl.Add(participant);
        if (participant.Kind == ParticipantKind.Variable)
          MiscFunx.ReplaceVariablesByParticipants(session, objectID, pl, ref index, processedIDs);
      }
    }
    else
      ++index;
  }

  public static void ReplaceVariablesByParticipants(
    IUserSession session,
    long objectID,
    ParticipantList pl)
  {
    List<long> processedIDs = new List<long>();
    int index = 0;
    while (index < pl.Count)
      MiscFunx.ReplaceVariablesByParticipants(session, objectID, pl, ref index, processedIDs);
  }

  public static FieldTypes GetFieldType(VarType vt) => MiscFunx.GetFieldTypeEx(vt).FieldType;

  public static FType GetFieldTypeEx(VarType vt)
  {
    FType[] customAttributes = (FType[]) vt.GetType().GetField(vt.ToString()).GetCustomAttributes(typeof (FType), false);
    return customAttributes.Length != 0 ? customAttributes[0] : throw new Exception($"WF: field type for {vt} not found");
  }

  public static string VarTypeToString(VarType vt)
  {
    string enumDescription = SimpleFuncs.GetEnumDescription((Enum) vt, true);
    if (enumDescription == "")
      enumDescription = SimpleFuncs.GetEnumDescription((Enum) MiscFunx.GetFieldType(vt));
    return enumDescription;
  }

  private static VarType DetermineVarType(FieldTypes ft, MultiValueModes MultiValueModes)
  {
    foreach (VarType varType in MiscFunx._varTypes)
    {
      FType[] customAttributes = (FType[]) varType.GetType().GetField(varType.ToString()).GetCustomAttributes(typeof (FType), false);
      if (customAttributes.Length == 0)
        throw new Exception($"WF: meta information for type for {varType} not found");
      if (customAttributes[0].FieldType.Equals((object) ft) && MultiValueModes == customAttributes[0].MultiValueModes)
        return varType;
    }
    return VarType.Unknown;
  }

  public static VarType DetermineVarType(IDBAttributeType atype)
  {
    return MiscFunx.DetermineVarType(atype.AttributeType, atype.MultipleValued);
  }

  public static VarType DetermineVarType(IMSAttributeType t)
  {
    return MiscFunx.DetermineVarType(t.FieldType, t.MultiValueMode);
  }

  public static VarType DetermineVarType(DataRow row)
  {
    return MiscFunx.DetermineVarType((FieldTypes) Convert.ToInt32(row["F_ATTRIBUTE_TYPE"]), (MultiValueModes) Convert.ToInt32(row["F_MULTIPLE_VALUED"]));
  }

  public static TempFormula FormulaFromAttribute(IDBAttribute attr)
  {
    TempFormula tempFormula = (TempFormula) null;
    using (MemoryStream stream = StreamHelper.BlobReaderToStream(attr as IBlobReader))
    {
      if (stream.Length > 0L)
      {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load((Stream) stream);
        tempFormula = new TempFormula((XmlNode) xmlDocument.DocumentElement);
      }
      return tempFormula;
    }
  }

  public static ExpressionInfo GetExpressionFromAttr(IDBAttribute attr)
  {
    using (MemoryStream stream = StreamHelper.BlobReaderToStream(attr as IBlobReader))
    {
      if (stream.Length <= 0L)
        return new ExpressionInfo(-1, Guid.Empty, -1L, string.Empty);
      if (!(new XmlSerializer(typeof (ExpressionInfo)).Deserialize((Stream) stream) is ExpressionInfo expressionInfo))
        return new ExpressionInfo(-1, Guid.Empty, -1L, string.Empty);
      MiscFunx.ReplaceExpressionLinkIDIfNeeded(attr.Session, expressionInfo);
      return expressionInfo;
    }
  }

  public static ICollection<ExpressionInfo> GetExpressionListFromAttr(IDBAttribute attr)
  {
    using (MemoryStream stream = StreamHelper.BlobReaderToStream(attr as IBlobReader))
    {
      if (stream.Length <= 0L)
        return (ICollection<ExpressionInfo>) new List<ExpressionInfo>();
      if (!(new XmlSerializer(typeof (List<ExpressionInfo>)).Deserialize((Stream) stream) is List<ExpressionInfo> expressionListFromAttr))
        return (ICollection<ExpressionInfo>) new List<ExpressionInfo>();
      for (int index = 0; index < expressionListFromAttr.Count; ++index)
        MiscFunx.ReplaceExpressionLinkIDIfNeeded(attr.Session, expressionListFromAttr[index]);
      return (ICollection<ExpressionInfo>) expressionListFromAttr;
    }
  }

  private static void ReplaceExpressionLinkIDIfNeeded(
    IUserSession session,
    ExpressionInfo expressionInfo)
  {
    if (!(expressionInfo.LinkGuid != Guid.Empty))
      return;
    IDBObject dbObject = session.GetObject(expressionInfo.LinkGuid, false);
    if (dbObject == null)
      return;
    expressionInfo.LinkID = dbObject.ObjectID;
  }

  private static void SetLinkGuid(IUserSession session, ExpressionInfo expressionInfo)
  {
    IDBObject dbObject1 = session.GetObject(expressionInfo.LinkID, false);
    if (dbObject1 != null)
    {
      expressionInfo.LinkGuid = dbObject1.ObjectGUID;
    }
    else
    {
      IDBObject dbObject2 = session.GetObject(-expressionInfo.LinkID, false);
      if (dbObject2 == null)
        return;
      expressionInfo.LinkGuid = dbObject2.ObjectGUID;
    }
  }

  public static void ExpressionToAttribute(ExpressionInfo expressionInfo, IDBAttribute attr)
  {
    IBlobWriter iw = attr as IBlobWriter;
    using (MemoryStream ms = new MemoryStream())
    {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof (ExpressionInfo));
      MiscFunx.SetLinkGuid(attr.Session, expressionInfo);
      MemoryStream memoryStream = ms;
      ExpressionInfo o = expressionInfo;
      xmlSerializer.Serialize((Stream) memoryStream, (object) o);
      ms.Position = 0L;
      ms.Flush();
      StreamHelper.StreamToBlobWriter(ms, iw);
    }
  }

  public static void ExpressionsToAttribute(
    List<ExpressionInfo> expressionInfoList,
    IDBAttribute attr)
  {
    IBlobWriter iw = attr as IBlobWriter;
    using (MemoryStream ms = new MemoryStream())
    {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof (List<ExpressionInfo>));
      foreach (ExpressionInfo expressionInfo in expressionInfoList)
        MiscFunx.SetLinkGuid(attr.Session, expressionInfo);
      xmlSerializer.Serialize((Stream) ms, (object) expressionInfoList);
      ms.Position = 0L;
      ms.Flush();
      StreamHelper.StreamToBlobWriter(ms, iw);
    }
  }

  public static void FormulaToAttribute(TempFormula tf, IDBAttribute attr)
  {
    IBlobWriter iw = attr as IBlobWriter;
    MemoryStream memoryStream = new MemoryStream();
    try
    {
      XmlTextWriter writer = new XmlTextWriter((Stream) memoryStream, Encoding.UTF8);
      writer.Formatting = Formatting.Indented;
      tf.WriteToXML(ref writer);
      writer.Flush();
      StreamHelper.StreamToBlobWriter(memoryStream, iw);
    }
    finally
    {
      memoryStream.Close();
    }
  }

  public static string UserRefToString(object obj)
  {
    string str = obj.ToString();
    if (obj.Equals((object) DBNull.Value))
      str = LocalizationHolder.rm.GetString("Workflow.Design_73");
    return str;
  }

  public static bool VerifyFormula(
    IExpertServer expert,
    int taskID,
    long objID,
    TempFormula tf,
    bool AllowNonExistentAttributes)
  {
    if (tf == null)
      return true;
    if (tf.Count == 0)
      return false;
    object obj = (object) null;
    ExpertResult expertResult = expert.CalcFormulaSimpleMode(taskID, (object) tf, objID, out obj);
    if (AllowNonExistentAttributes && expertResult == ExpertResult.RuleNotFound)
      return true;
    return expertResult == ExpertResult.OK && obj is bool;
  }

  public static bool VerifyFormula(IExpertServer expert, int taskID, long objID, TempFormula tf)
  {
    return MiscFunx.VerifyFormula(expert, taskID, objID, tf, false);
  }

  public static string VerifyExpressionFormula(string formula, AttributeValues[] attrValues)
  {
    string empty = string.Empty;
    object obj = MiscFunx.VerifyExpression(formula, attrValues, false);
    string str1;
    if (obj is bool)
    {
      string str2 = !Convert.ToBoolean(obj) ? LocalizationHolder.rm.GetString("Workflow.Design_114") : LocalizationHolder.rm.GetString("Workflow.Design_113");
      str1 = LocalizationHolder.rm.GetString("Workflow.Design_115") + str2;
    }
    else
    {
      str1 = LocalizationHolder.rm.GetString("Workflow.Design_116");
      if (obj != null)
        str1 += $" ({obj})";
    }
    return str1;
  }

  public static object VerifyExpression(
    string formula,
    AttributeValues[] attrValues,
    bool isFiltered)
  {
    if (formula == "ИНАЧЕ")
      return (object) true;
    if (string.IsNullOrEmpty(formula))
      return (object) string.Empty;
    if (isFiltered)
      return (object) true;
    using (Parser parser = new Parser())
    {
      parser.AutoDetectVariables = true;
      parser.Validate = false;
      ExpressionTree expressionTree = parser.Parse(formula);
      if (expressionTree == null)
        return (object) null;
      VariableValuesCollection usedVariables = expressionTree.UsedVariables;
      for (int index1 = 0; index1 < usedVariables.Count; ++index1)
      {
        VariableValue varValue = usedVariables[index1];
        int index2 = Array.FindIndex<AttributeValues>(attrValues, (Predicate<AttributeValues>) (x => x.AttributeName == varValue.Name));
        if (index2 == -1)
          return (object) new ExpressionVerifyError($"Атрибут '{varValue.Name}' отсутствует у фильтруемого объекта!");
        usedVariables[index1].Value = attrValues[index2].Value;
      }
      return expressionTree.Evaluate(usedVariables);
    }
  }

  public static object GetDefault(Type type)
  {
    return type.IsValueType ? Activator.CreateInstance(type) : (object) string.Empty;
  }

  public static bool CheckVariableInExpression(string formula, int attributeID)
  {
    if (string.IsNullOrEmpty(formula))
      return false;
    string attributeName = MetaDataHelper.GetAttributeTypeName(attributeID);
    if (string.IsNullOrEmpty(attributeName))
      return false;
    using (Parser parser = new Parser())
    {
      parser.AutoDetectVariables = true;
      parser.Validate = false;
      ExpressionTree expressionTree = parser.Parse(formula);
      return expressionTree != null && expressionTree.Variables.Cast<Variable>().Any<Variable>((System.Func<Variable, bool>) (variable => variable.Name == attributeName));
    }
  }

  public static int StrToIntDef(string s, int def)
  {
    int.TryParse(s, out def);
    return def;
  }

  public static string GetSignGraphCaption(string id, bool returnNullIfNotFound = false)
  {
    IMSAttributeType atype = (IMSAttributeType) null;
    return MiscFunx.GetSignGraphCaption(id, ref atype, returnNullIfNotFound);
  }

  public static string GetSignGraphCaption(
    string id,
    ref IMSAttributeType atype,
    bool returnNullIfNotFound = false)
  {
    if (atype == null)
    {
      atype = MetaDataHelper.GetAttributeType(wfConsts.SignGraphID);
      if (atype != null && atype.MultiValueMode == MultiValueModes.SingleValue)
        atype = (IMSAttributeType) null;
    }
    if (atype != null)
    {
      int index = atype.PossibleValues.IndexOf((object) id);
      if (index >= 0 && index < atype.PossibleValuesDescriptions.Count)
        return atype.PossibleValuesDescriptions[index].ToString();
    }
    return !returnNullIfNotFound ? "??" : (string) null;
  }

  public static string GetGraphsSetCaption(GraphsSet gset)
  {
    IMSAttributeType atype = (IMSAttributeType) null;
    string graphsSetCaption = string.Empty;
    foreach (string key in gset)
    {
      string str = string.Empty;
      foreach (GraphClass graphClass in gset[key])
      {
        if (str != "")
          str += ", ";
        str = $"{str}\"{MiscFunx.GetSignGraphCaption(graphClass.Value, ref atype)}\"";
      }
      if (graphsSetCaption != "")
        graphsSetCaption += LocalizationHolder.rm.GetString("Workflow.Design_74");
      graphsSetCaption = $"{graphsSetCaption}({str})";
    }
    return graphsSetCaption;
  }

  /// <summary>Closes all unclosed HTML tags</summary>
  /// <param name="p"></param>
  /// <returns></returns>
  internal static string CloseTags(string s)
  {
    string str = "";
    MatchCollection matchCollection = new Regex("<[^>]*?>", RegexOptions.Singleline).Matches(s);
    if (matchCollection.Count > 0)
    {
      foreach (Match match in matchCollection)
      {
        if (match.Success)
          str += match.Value;
      }
    }
    return str == "" ? s : str;
  }

  public static long GetScriptID(
    IUserSession session,
    long ObjectID,
    ScriptKind kind,
    ScriptExecSide execSide)
  {
    IDBRelationCollection relationCollection = session.GetRelationCollection(wfConsts.ScriptRelationTypeID);
    relationCollection.LocalTypesMode = true;
    object[] columns = new object[3]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) wfConsts.AttrScriptKindID,
      (object) ObligatoryObjectAttributes.F_PRJLINK_ID
    };
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[2]
    {
      new ConditionStructure(wfConsts.AttrScriptKindID, RelationalOperators.Equal, (object) (int) kind, LogicalOperators.AND, 0, false),
      new ConditionStructure(wfConsts.AttrScriptExecSideID, RelationalOperators.Equal, (object) (int) execSide, LogicalOperators.AND, 0, false)
    }, columns);
    IEnumerator enumerator = relationCollection.ConsistFrom(paramSet, ObjectID).Rows.GetEnumerator();
    try
    {
      if (enumerator.MoveNext())
        return Convert.ToInt64(((DataRow) enumerator.Current)[0]);
    }
    finally
    {
      if (enumerator is IDisposable disposable)
        disposable.Dispose();
    }
    return 0;
  }

  public static DataTable GetScriptIDs(IUserSession session, List<long> objectIDs)
  {
    IDBRelationCollection relationCollection = session.GetRelationCollection(wfConsts.ScriptRelationTypeID);
    relationCollection.LocalTypesMode = true;
    object[] columns = new object[3]
    {
      (object) ObligatoryObjectAttributes.F_PROJ_ID,
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) ObligatoryObjectAttributes.F_OBJECT_TYPE
    };
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-21, RelationalOperators.In, (object) objectIDs.ToArray(), LogicalOperators.AND, 0, false)
    }, columns, 0L, (object) null, -1);
    return relationCollection.Select(paramSet);
  }

  public static string GetScriptCode(IUserSession session, long scriptID)
  {
    IDBObject objectActualCopy = session.GetObjectActualCopy(scriptID, false);
    if (objectActualCopy != null)
    {
      IDBAttribute attributeById = objectActualCopy.GetAttributeByID(wfConsts.AttrScriptTextID);
      if (attributeById != null)
        return attributeById.Value.ToString();
    }
    return "";
  }

  public static string GetScriptCode(
    IUserSession session,
    long ObjectID,
    ScriptKind kind,
    ScriptExecSide execSide)
  {
    return MiscFunx.GetScriptCode(session, ObjectID, kind, execSide, ref MiscFunx.LastScriptID);
  }

  /// <summary>Получить код сценария</summary>
  /// <param name="session"></param>
  /// <param name="ObjectID"></param>
  /// <param name="kind"></param>
  /// <param name="execSide"></param>
  /// <param name="scriptID"></param>
  /// <returns>null в случае отсутствия или код сценария</returns>
  public static string GetScriptCode(
    IUserSession session,
    long ObjectID,
    ScriptKind kind,
    ScriptExecSide execSide,
    ref long scriptID)
  {
    scriptID = MiscFunx.GetScriptID(session, ObjectID, kind, execSide);
    return scriptID > 0L ? MiscFunx.GetScriptCode(session, scriptID) : (string) null;
  }

  public static void ExecScript(IUserSession session, long scriptID, params object[] list)
  {
    string scriptCode = MiscFunx.GetScriptCode(session, scriptID);
    if (scriptCode == null || !(scriptCode.Trim() != ""))
      return;
    string str = ScriptExecHelper.IsolatedExecScript(scriptCode, CSharpScriptInvocationOptions.Default, list);
    if (str != "")
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Workflow.Design_121"), (object) scriptID) + str);
  }

  /// <summary>Выполнить сценарий C#</summary>
  /// <param name="code">код сценария</param>
  /// <param name="activity">действие на котором сценарий</param>
  /// <param name="options">опции выполнения сценария</param>
  /// <returns>При возникновении исключения возвращает текст ошибки. В других случаях пустую строку.</returns>
  public static string IsolatedExecScript(
    string code,
    IActivity activity,
    CSharpScriptInvocationOptions options)
  {
    return ScriptExecHelper.IsolatedExecScript(code, options, (object) activity);
  }

  /// <summary>
  /// Выполнить сценарий C#, в случае ошибки генерируется исключение
  /// </summary>
  /// <param name="code">код сценария</param>
  /// <param name="activity">действие</param>
  /// <param name="options">опции запуска</param>
  public static void IsolatedRawExecScript(
    string code,
    IActivity activity,
    CSharpScriptInvocationOptions options)
  {
    ServiceUtils.GetService<ICSharpScriptExecutor>((object) ApplicationServices.Container, false).Execute(code, options, (object) activity);
  }

  /// <summary>
  /// Проверка на транзакцию вернёт true если транзакция стартована, используем для "запоминания" текущего значения транзакции
  /// </summary>
  /// <param name="session"></param>
  /// <returns></returns>
  public static bool CheckForActiveTransaction(IUserSession session)
  {
    return ((IDBTransactions) session.GetCustomService(typeof (IDBTransactions))).InTransaction;
  }

  /// <summary>
  /// Проверка на транзакцию, в случае ошибки запишет в лог сессии
  /// </summary>
  /// <param name="session">сессия</param>
  /// <param name="activity">действие</param>
  /// <param name="msg">сообщение</param>
  /// <param name="oldStateOfTransaction">старое значение транзакции</param>
  /// <param name="where">где смотрим Client/Server</param>
  public static void CheckForActiveTransaction(
    IUserSession session,
    IActivity activity,
    string msg,
    bool oldStateOfTransaction,
    string where = "Client")
  {
    if (((IDBTransactions) session.GetCustomService(typeof (IDBTransactions))).InTransaction == oldStateOfTransaction)
      return;
    if (msg != "")
      msg = " " + msg;
    msg = oldStateOfTransaction ? "=== Обнаружено неправильное закрытие транзакции!" + msg : "=== Обнаружено неправильное открытие транзакции!" + msg;
    if (activity != null)
      msg = $"{msg} (Activity={activity.Name})";
    string text = msg + Environment.NewLine + $"[{where} ThreadID - {Thread.CurrentThread.ManagedThreadId}] :" + Environment.NewLine + Environment.StackTrace;
    session.AddToTrace(text, Intermech.Consts.traceAlways, "session_forgotten_transaction.log");
  }

  /// <summary>
  /// Возвращает идентификатор объекта, у которого есть переменные относительно переданного actID
  /// т.е. в режиме шаблона это будет шаблон, в режиме процесса это будет реальное действие
  /// </summary>
  /// <param name="actID">Идентификатор действия</param>
  /// <param name="procID">Идентификатор процесса</param>
  /// <returns></returns>
  public static long GetObjectIDWithVars(IUserSession session, long actID, long procID)
  {
    IDBObject activity = session.GetObject(actID, false);
    return activity != null ? MiscFunx.GetObjectIDWithVars(activity, procID) : procID;
  }

  public static long GetObjectIDWithVars(IDBObject activity, long procID)
  {
    IDBObject process = activity.Session.GetObject(procID, false);
    return process != null ? MiscFunx.GetObjectIDWithVars(activity, process) : 0L;
  }

  public static long GetObjectIDWithVars(IDBObject activity, IDBObject process)
  {
    if (process.ObjectType == wfConsts.SchemesTypeID)
      return process.ObjectID;
    ActivityStatus activityStatus = ActivityStatus.OnApproach;
    IDBAttribute attributeById = activity.GetAttributeByID(wfConsts.AttrActivityStatusID);
    if (attributeById != null)
      activityStatus = (ActivityStatus) attributeById.AsInteger;
    return activityStatus == ActivityStatus.OnApproach || activityStatus == ActivityStatus.Terminated ? process.ObjectID : activity.ObjectID;
  }

  /// <summary>Проверяет есть ли запрашиваемый флаг у объекта</summary>
  /// <param name="obj"></param>
  /// <param name="flag"></param>
  /// <returns></returns>
  public static bool IsFlagSet(IDBObject obj, ActivityFlags flag)
  {
    return MiscFunx.IsFlagSet(obj.GetAttributeByID(wfConsts.AttrAddIDID), flag);
  }

  /// <summary>
  /// Проверяет есть ли запрашиваемый флаг в значении атрибута
  /// </summary>
  /// <param name="attr"></param>
  /// <param name="flag"></param>
  /// <returns></returns>
  public static bool IsFlagSet(IDBAttribute attr, ActivityFlags flag)
  {
    return attr != null && ((ActivityFlags) attr.AsInteger & flag) == flag;
  }

  /// <summary>
  /// Все типы связей, которые могут применяться в группирующих объектах (in)
  /// </summary>
  public static List<int> GroupingObjectsApplicableRelations(IUserSession session)
  {
    if (MiscFunx._groupingObjectsApplicableRelations == null)
    {
      MiscFunx._groupingObjectsApplicableRelations = new List<int>();
      foreach (DataRow row in (InternalDataCollectionBase) session.GetRelationsApplicabilityCollection().GetApplicabilitiesList(-1, -1, 1644).Rows)
      {
        int int32 = Convert.ToInt32(row["F_RELATION_TYPE"]);
        if (!MiscFunx._groupingObjectsApplicableRelations.Contains(int32))
          MiscFunx._groupingObjectsApplicableRelations.Add(int32);
      }
    }
    return MiscFunx._groupingObjectsApplicableRelations;
  }

  /// <summary>
  /// Если ExpandECOs =  true, возвращает список вложений, в котором для всех группирующих объектов (ИИ и т.д.) заполнен их состав, который записывается в Attachment.InnerList
  /// </summary>
  public static AttachmentList ExpandAttachments(
    IUserSession session,
    AttachmentList src,
    bool ExpandECOs)
  {
    AttachmentList attachmentList1 = new AttachmentList();
    foreach (Attachment proto in (List<Attachment>) src)
    {
      Attachment attachment = new Attachment(proto);
      attachmentList1.Add(attachment);
      if (ExpandECOs || !wfConsts.IsECO(proto.TypeID))
      {
        IDBObject dbObject = session.GetObject(proto.ObjectID, false);
        if (dbObject != null)
        {
          if (proto.TypeID == 0)
            proto.TypeID = dbObject.ObjectType;
          if (dbObject is IDBEditingContextsObject)
          {
            AttachmentList attachmentList2 = new AttachmentList();
            List<int> intList = new List<int>();
            foreach (DataRow row in (InternalDataCollectionBase) session.GetRelationsApplicabilityCollection().GetApplicabilitiesList(-1, -1, proto.TypeID).Rows)
            {
              int int32 = Convert.ToInt32(row["F_RELATION_TYPE"]);
              if (int32 != wfConsts.SignsRelationTypeID && !intList.Contains(int32))
                intList.Add(int32);
            }
            foreach (int relationType in intList)
            {
              IDBRelationCollection relationCollection = session.GetRelationCollection(relationType);
              DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
              {
                new ConditionStructure(-21, RelationalOperators.Equal, (object) proto.ObjectID, LogicalOperators.NONE, 0, true)
              }, new object[4]
              {
                (object) -2,
                (object) -3,
                (object) -7,
                (object) -6
              });
              foreach (DataRow row in (InternalDataCollectionBase) relationCollection.Select(paramSet).Rows)
                attachmentList2.Add(new Attachment()
                {
                  ObjectID = Convert.ToInt64(row[0]),
                  ID = Convert.ToInt64(row[1]),
                  TypeID = Convert.ToInt32(row[2]),
                  CheckOutBy = Convert.ToInt64(row[3])
                });
            }
            attachment.InnerList = attachmentList2;
          }
        }
      }
    }
    return attachmentList1;
  }

  public static AttachmentList ExpandAttachments(
    IUserSession session,
    AttachmentList src,
    bool ExpandECOs,
    Dictionary<int, int> typeToLCLevel,
    Dictionary<int, int> typeToLCstep)
  {
    if (!(ApplicationServices.Container.GetService(typeof (INotifySubscriberService)) is INotifySubscriberService service))
      return MiscFunx.ExpandAttachments(session, src, ExpandECOs);
    bool flag = false;
    AttachmentList attachmentList1 = new AttachmentList();
    foreach (Attachment proto in (List<Attachment>) src)
    {
      Attachment attachment1 = new Attachment(proto);
      attachmentList1.Add(attachment1);
      if (ExpandECOs || !wfConsts.IsECO(proto.TypeID))
      {
        IDBObject dbObject = session.GetObject(proto.ObjectID, false);
        if (dbObject != null)
        {
          if (proto.TypeID == 0)
            proto.TypeID = dbObject.ObjectType;
          if (dbObject is IDBEditingContextsObject)
          {
            AttachmentList attachmentList2 = new AttachmentList();
            EcoDocumentsInAttachments attachmentsDoc = new EcoDocumentsInAttachments()
            {
              EcoObjectID = proto.ObjectID,
              TypeToLCLevel = typeToLCLevel,
              TypeToLCStep = typeToLCstep
            };
            List<ResultEcoDocumentsInformation> resultEcos = service.GetResultEcos(attachmentsDoc);
            if (resultEcos == null)
            {
              flag = true;
              break;
            }
            foreach (ResultEcoDocumentsInformation documentsInformation in resultEcos)
            {
              Attachment attachment2 = new Attachment()
              {
                ObjectID = documentsInformation.ObjectID,
                ID = documentsInformation.ID,
                TypeID = documentsInformation.ObjectType,
                CheckOutBy = documentsInformation.CheckOutBy
              };
              attachmentList2.Add(attachment2);
            }
            attachment1.InnerList = attachmentList2;
          }
        }
      }
    }
    return flag ? MiscFunx.ExpandAttachments(session, src, ExpandECOs) : attachmentList1;
  }

  /// <summary>
  /// Возвращает список вложений, в котором для всех группирующих объектов (ИИ и т.д.) заполнен их состав, который записывается в Attachment.InnerList
  /// </summary>
  public static AttachmentList ExpandAttachments(IUserSession session, AttachmentList src)
  {
    return MiscFunx.ExpandAttachments(session, src, true);
  }

  /// <summary>
  /// Возвращает наиболее близко соответствующий (частный) тип из AllowedTypeIDs к типу Type.
  /// Например, если в допустимых типах "Документы", "Чертежи", а поданный тип = "Чертежи БЧ", который наследован от "Чертежи", тогда должно выбрать "Чертежи", а не "Документы"
  /// </summary>
  /// <param name="Type"></param>
  /// <param name="AllowedTypeIDs"></param>
  /// <returns></returns>
  public static int GetMostAppropriateType(int Type, List<int> AllowedTypeIDs)
  {
    if (AllowedTypeIDs.Contains(Type))
      return Type;
    int childTypeID = Type;
    while (childTypeID != -1)
    {
      childTypeID = MetaDataHelper.GetObjectTypeParentID(childTypeID);
      if (childTypeID != -1 && AllowedTypeIDs.Contains(childTypeID))
        return childTypeID;
    }
    return AllowedTypeIDs.Contains(-1) ? -1 : 0;
  }

  /// <summary>Возвращает заголовок заданного объекта</summary>
  /// <param name="session">Сессия</param>
  /// <param name="id">Идентификатор версии объекта</param>
  /// <param name="includeType">Выводить также тип объекта</param>
  /// <returns></returns>
  public static string GetObjectCaption(IUserSession session, long id, bool includeType = true)
  {
    if (!includeType)
    {
      QuickObjectInfo objectInfo = session.GetObjectInfo(id);
      if (!objectInfo.Empty)
        return objectInfo.Caption;
    }
    else
    {
      IDBObject dbObject = session.GetObject(id, false);
      if (dbObject != null)
        return dbObject.NameInMessages;
    }
    return $"? (ID={id})";
  }

  public static long GetProcessCategory(IUserSession session, long processID)
  {
    IDBRelationCollection relationCollection = session.GetRelationCollection(wfConsts.SimpleLinkTypeID);
    DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[1]
    {
      (object) -2
    });
    relationCollection.ObjectTypeID = wfConsts.SchemeCategoriesID;
    DataTable dataTable = relationCollection.EntersInVersion(paramSet, processID);
    return dataTable.Rows.Count > 0 ? Convert.ToInt64(dataTable.Rows[0][0]) : 0L;
  }

  public static void AddProcessToCategory(IUserSession session, long processID, long catID)
  {
    IDBRelationCollection relationCollection = session.GetRelationCollection(wfConsts.SimpleLinkTypeID);
    try
    {
      relationCollection.Create(catID, processID);
    }
    catch (Exception ex)
    {
      if (ex.InnerException is KernelExceptionID innerException && innerException.ErrorID == 47)
        return;
      throw;
    }
  }

  public static void GenerateExpertTrace(IExpertServer expert, int taskID, IUserSession session)
  {
    foreach (XmlNode childNode in ZlibHelper.UnpackXmlBuffer(expert.GetTraceInfo(taskID)).DocumentElement.ChildNodes)
    {
      if (childNode.NodeType != XmlNodeType.XmlDeclaration)
        session.EventLog.AddToTrace(childNode.OuterXml, Intermech.Consts.traceAlways, "workflow.expert.log");
    }
  }

  public static List<long> GetHistoryData(
    IUserSession sess,
    bool isMessage,
    IDBAttribute histattr,
    Dictionary<long, object[]> rows,
    IDBObject act,
    bool isOneMessageMode,
    long objectID,
    long objectTypeID)
  {
    IDBObjectCollection objectCollection = sess.GetObjectCollection(wfConsts.ObjectsTypeID);
    List<long> collection;
    if (isOneMessageMode)
    {
      collection = new List<long>();
      collection.Add(objectID);
      if (objectTypeID == (long) wfConsts.RemoteSubProcessTypeID)
      {
        if (act == null)
          act = sess.GetObject(objectID, false);
        IDBAttribute attributeById = act?.GetAttributeByID(wfConsts.AttrProcessID);
        if (attributeById != null)
          collection.Add(attributeById.AsInteger);
      }
    }
    else
    {
      collection = new List<long>();
      if (!isMessage)
        collection.Add(objectID);
      if (histattr != null)
      {
        for (int index = histattr.ValuesCount - 1; index >= 0; --index)
          collection.Add(Convert.ToInt64(histattr.Values[index]));
      }
    }
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[4]
    {
      new ConditionStructure(-2, RelationalOperators.In, (object) collection.ToArray(), LogicalOperators.AND, 0, false),
      new ConditionStructure(wfConsts.AttrActivityMessageID, RelationalOperators.NotEmpty, (object) 0, LogicalOperators.OR, 1, false),
      new ConditionStructure(-7, RelationalOperators.In, (object) new object[2]
      {
        (object) wfConsts.ProcessesTypeID,
        (object) wfConsts.RemoteSubProcessTypeID
      }, LogicalOperators.AND, 1, false),
      new ConditionStructure(wfConsts.AttrAddInfoID, RelationalOperators.NotEmpty, (object) 0, LogicalOperators.NONE, -2, false)
    }, new ColumnDescriptor[11]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Auto, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 0),
      new ColumnDescriptor((object) wfConsts.AttrRecipID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 0),
      new ColumnDescriptor((object) wfConsts.AttrActivityMessageID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 0),
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Auto, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.NONE, 0),
      new ColumnDescriptor((object) wfConsts.AttrActivityResultID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 0),
      new ColumnDescriptor((object) wfConsts.AttrActivityMessageID, AttributeSourceTypes.Auto, ColumnContents.Date, ColumnNameMapping.Default, SortOrders.NONE, 0),
      new ColumnDescriptor((object) wfConsts.AttrRecipID, AttributeSourceTypes.Auto, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.NONE, 0),
      new ColumnDescriptor((object) wfConsts.AttrAddInfoID, AttributeSourceTypes.Auto, ColumnContents.String, ColumnNameMapping.Default, SortOrders.NONE, 0),
      new ColumnDescriptor((object) wfConsts.AttrIOUserID, AttributeSourceTypes.Auto, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.NONE, 0),
      new ColumnDescriptor((object) wfConsts.AttrIOUserID, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.Default, SortOrders.NONE, 0)
    });
    if (paramSet.Tags == null)
      paramSet.Tags = new HybridDictionary();
    paramSet.Tags[(object) "LocalTypesSelector"] = (object) new LocalTypesByObjectIDsSelector(collection.ToArray());
    DataTable dataTable = objectCollection.Select(paramSet);
    List<long> longList = new List<long>();
    longList.AddRange((IEnumerable<long>) collection);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      long int64 = Convert.ToInt64(row[0]);
      longList.Remove(int64);
      rows.Add(int64, row.ItemArray);
    }
    return collection;
  }

  public static List<MessageRow> GetMessageRows(
    object[] r,
    List<MessageRow> subrows,
    IUserSession sess,
    long activityID,
    ref string curSiteGuid)
  {
    subrows.Clear();
    if (r[8] != DBNull.Value && r[8].ToString().Contains('M'.ToString()))
    {
      IDBObject dbObject = sess.GetObject(activityID, false);
      if (dbObject != null)
      {
        string str1 = new ExtProperties(dbObject, wfConsts.AttrAddInfoID).Read("PortalInfo");
        StringList stringList1 = new StringList()
        {
          CommaText = str1
        };
        Guid guid = Guid.Empty;
        try
        {
          guid = new Guid(stringList1.Values["SrcSite"]);
        }
        catch
        {
        }
        if (curSiteGuid == null)
        {
          SiteInfo info = sess.GetCustomService(typeof (ISitesCacheService)) is ISitesCacheService customService ? customService.Info : (SiteInfo) null;
          if (info != null)
            curSiteGuid = info.GUID.ToString();
        }
        int num = curSiteGuid != stringList1.Values["SrcSite"] ? 1 : 0;
        string str2 = num != 0 ? stringList1.Values["SrcSiteName"] : stringList1.Values["RSiteName"];
        string ParentProcessName = num != 0 ? stringList1.Values["SrcProcessName"] : stringList1.Values["RProcessName"];
        string str3 = stringList1.Values["Src"];
        StringList stringList2 = new StringList()
        {
          CommaText = stringList1.Values["HistArray"]
        };
        string str4 = stringList1.Values["Messages"];
        stringList1.CommaText = str4;
        for (int index = stringList2.Count - 1; index >= 0; --index)
        {
          string key = stringList2[index];
          string s = stringList1.Values[key];
          if (!string.IsNullOrEmpty(s))
          {
            MessageRow messageRow = new MessageRow(ParentProcessName, StringList.CommaTextToObjectArray(s));
            subrows.Add(messageRow);
            messageRow.SrcSiteGuid = guid;
            messageRow.SrcSiteName = str2;
            if (guid != Guid.Empty)
            {
              try
              {
                ActivityInfo byKind = ActivityInfos.FindByKind(wfConsts.IntToActivityKind(Convert.ToInt32(messageRow.Data[4])));
                if (byKind != null)
                  messageRow.Data[4] = (object) byKind.Type;
              }
              catch
              {
              }
              if (messageRow.Data[6] != DBNull.Value)
              {
                DateTime dateTime = Convert.ToDateTime(messageRow.Data[6], (IFormatProvider) CultureInfo.InvariantCulture);
                dateTime = dateTime.ToLocalTime();
                messageRow.Data[6] = (object) dateTime;
              }
              if (str3 == "Search")
                messageRow.Data[7] = (object) 0;
            }
          }
        }
      }
    }
    else
      subrows.Add(new MessageRow("", r));
    return subrows;
  }

  /// <summary>
  /// Создать переменную для формул из переменной маршрутизатора
  /// </summary>
  /// <param name="actVariable">переменная маршрутизатора</param>
  /// <returns></returns>
  public static Variable CreateExpressionVariable(string variableName, VarType variableType)
  {
    FType fieldTypeEx = MiscFunx.GetFieldTypeEx(variableType);
    Type type = AttributesTypeHelper.GetTypeOfAttributeValue(fieldTypeEx.FieldType);
    if (fieldTypeEx.FieldType == FieldTypes.ftObjectLink)
      type = typeof (string);
    if (typeof (MeasuredValue) == type)
      type = typeof (double);
    return new Variable(variableName, type, fieldTypeEx.FieldType);
  }

  public static HashSet<Variable> GetAllAttributesVariables()
  {
    HashSet<Variable> attributesVariables = new HashSet<Variable>();
    foreach (IMSAttributeType attributeTypes in MetaDataHelper.GetAttributeTypesList())
    {
      Type type = AttributesTypeHelper.GetTypeOfAttributeValue(attributeTypes.RealFieldType);
      if (attributeTypes.RealFieldType == FieldTypes.ftObjectLink)
        type = typeof (string);
      if (typeof (MeasuredValue) == type)
        type = typeof (double);
      Variable variable = new Variable(attributeTypes.Name, type, attributeTypes.RealFieldType);
      attributesVariables.Add(variable);
    }
    return attributesVariables;
  }

  public static HashSet<Variable> ConvertBasicAttributePropertiesToVariable(
    BasicAttributeProperties[] basicAttributes)
  {
    HashSet<Variable> variable1 = new HashSet<Variable>(0);
    if (basicAttributes != null)
    {
      foreach (BasicAttributeProperties basicAttribute in basicAttributes)
      {
        Type type = AttributesTypeHelper.GetTypeOfAttributeValue(basicAttribute.RealFieldType);
        if (basicAttribute.RealFieldType == FieldTypes.ftObjectLink)
          type = typeof (string);
        if (typeof (MeasuredValue) == type)
          type = typeof (double);
        Variable variable2 = new Variable(basicAttribute.Name, type, basicAttribute.RealFieldType);
        variable1.Add(variable2);
      }
    }
    return variable1;
  }

  public delegate void ReloadVariablesCacheEvent(IUserSession session);
}
