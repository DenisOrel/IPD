
// Type: Intermech.Client.Core.ClientCommons
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.Data;
using System.Windows.Forms;


namespace Intermech.Client.Core;

public class ClientCommons
{
  /// <summary>Копирует данные из строк fromRows в таблицу toTable</summary>
  public static void AssignRows(DataTable toTable, DataRow[] fromRows)
  {
    foreach (DataRow fromRow in fromRows)
    {
      DataRow row = toTable.NewRow();
      for (int index = 0; index < toTable.Columns.Count; ++index)
        row[toTable.Columns[index].ColumnName] = fromRow[toTable.Columns[index].ColumnName];
      toTable.Rows.Add(row);
    }
    toTable.AcceptChanges();
  }

  public static IDBAttribute GetAttributeInterface(
    long aElementID,
    AttributableElements aAttributableElement,
    int aAttributeID,
    int aIndex,
    IUserSession iSession)
  {
    IDBAttribute attributeInterface = (IDBAttribute) null;
    switch (aAttributableElement)
    {
      case AttributableElements.Object:
        attributeInterface = iSession.GetObjectAttributeByID(aElementID, aAttributeID);
        break;
      case AttributableElements.Relation:
        attributeInterface = iSession.GetRelationAttributeByID(aElementID, aAttributeID);
        break;
    }
    if (attributeInterface != null)
      attributeInterface.Index = aIndex;
    return attributeInterface;
  }

  public static IDBAttributable GetAttributable(
    long aId,
    AttributableElements aAttributableElement,
    IUserSession iSession)
  {
    int aType = 0;
    return ClientCommons.GetAttributable(aId, aAttributableElement, out aType, iSession);
  }

  public static IDBAttributable GetAttributable(
    long aId,
    AttributableElements aAttributableElement,
    out int aType,
    IUserSession iSession)
  {
    aType = 0;
    IDBAttributable attributable = (IDBAttributable) null;
    switch (aAttributableElement)
    {
      case AttributableElements.Object:
        IDBObject dbObject = iSession.GetObject(aId, false);
        if (dbObject == null)
          return (IDBAttributable) null;
        attributable = (IDBAttributable) dbObject;
        aType = dbObject.ObjectType;
        break;
      case AttributableElements.Relation:
        IDBRelation relation = iSession.GetRelation(aId, false);
        if (relation == null)
          return (IDBAttributable) null;
        attributable = (IDBAttributable) relation;
        aType = relation.RelationType;
        break;
    }
    return attributable;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="aTypeId"></param>
  /// <param name="aAttributableElement"></param>
  /// <param name="iSession"></param>
  /// <returns></returns>
  public static IDBAttributableTypeInfo GetAttributableType(
    int aTypeId,
    AttributableElements aAttributableElement)
  {
    IDBAttributableTypeInfo attributableType = (IDBAttributableTypeInfo) null;
    IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
    switch (aAttributableElement)
    {
      case AttributableElements.Object:
        attributableType = (IDBAttributableTypeInfo) service.GetObjectType(aTypeId);
        break;
      case AttributableElements.Relation:
        attributableType = (IDBAttributableTypeInfo) service.GetRelationType(aTypeId);
        break;
    }
    return attributableType;
  }

  public static int GetElementType(long aId, AttributableElements aAttributableElement)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      int elementType = -1;
      if (aAttributableElement == AttributableElements.Object)
        elementType = sessionKeeper.Session.GetObject(aId).ObjectType;
      if (aAttributableElement == AttributableElements.Relation)
        elementType = sessionKeeper.Session.GetRelation(aId).RelationType;
      return elementType;
    }
  }

  public static bool GetAnyAttributesFlag(int aTypeId, AttributableElements aAttributableElement)
  {
    switch (aAttributableElement)
    {
      case AttributableElements.Object:
        IMSObjectType objectType = MetaDataHelper.GetObjectType(aTypeId);
        return objectType != null && objectType.AnyAttributes;
      case AttributableElements.Relation:
        IMSRelationType relationType = MetaDataHelper.GetRelationType(aTypeId);
        return relationType != null && relationType.AnyAttributes;
      default:
        return false;
    }
  }

  /// <summary>Получить допустимые значения для атрибута attributeId</summary>
  /// <param name="attributeId">id атрибута</param>
  /// <returns>DataTable с допустимыми значениями</returns>
  public static DataTable GetPossibleValues(int attributeId)
  {
    DataTable possibleValues = (DataTable) null;
    IDBAttributeTypeInfo attributeType = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetAttributeType(attributeId);
    if (attributeType != null)
      possibleValues = attributeType.GetPossibleValues();
    return possibleValues;
  }

  /// <summary>
  /// Вернуть имя поля, содержащего значения в таблице допустимых значений
  /// </summary>
  /// <param name="possibleValuesDataTable"></param>
  /// <returns></returns>
  public static string ExtractValueFieldName(DataTable possibleValuesDataTable)
  {
    if (possibleValuesDataTable == null)
      return string.Empty;
    bool flag = false;
    string valueFieldName = string.Empty;
    for (int index = 0; index < possibleValuesDataTable.Columns.Count; ++index)
    {
      if (possibleValuesDataTable.Columns[index].ColumnName == "F_INLIST_ID")
        possibleValuesDataTable.Columns[index].Caption = LocalizationHolder.rm.GetString("Client.Core_1061");
      else if (possibleValuesDataTable.Columns[index].ColumnName == "F_DESCRIPTION")
      {
        possibleValuesDataTable.Columns[index].Caption = LocalizationHolder.rm.GetString("Client.Core_1062");
      }
      else
      {
        possibleValuesDataTable.Columns[index].Caption = LocalizationHolder.rm.GetString("Client.Core_1063");
        if (!flag)
          valueFieldName = possibleValuesDataTable.Columns[index].ColumnName;
        flag = true;
      }
    }
    return valueFieldName;
  }

  /// <summary>Вернуть объект по категории и идентификатору</summary>
  /// <param name="category">категория</param>
  /// <param name="id">идентификатор</param>
  /// <returns></returns>
  public static object GetServerObjectByCategory(IUserSession session, int category, object id)
  {
    object objectByCategory = (object) null;
    switch (category)
    {
      case 1:
        objectByCategory = (object) session.GetObject(Convert.ToInt64(id));
        break;
      case 3:
        objectByCategory = (object) session.GetAttributeType(Convert.ToInt32(id));
        break;
      case 4:
        objectByCategory = (object) session.GetObjectType(Convert.ToInt32(id));
        break;
      case 6:
        objectByCategory = (object) session.GetRelationType(Convert.ToInt32(id));
        break;
      case 8:
        objectByCategory = (object) session.GetLifecycleLevel(Convert.ToInt32(id));
        break;
      case 9:
        objectByCategory = (object) session.GetLanguage((string) id);
        break;
      case 11:
        objectByCategory = (object) session.GetSubjectAreaType(Convert.ToChar(id));
        break;
      case 12:
        objectByCategory = (object) session.GetAttributesGroup(Convert.ToInt32(id));
        break;
      case 16 /*0x10*/:
        objectByCategory = (object) session.GetLCSchema(Convert.ToInt32(id));
        break;
    }
    return objectByCategory;
  }

  public static TreeNode FindNodeByIdCustom(TreeNode node, object id)
  {
    TreeNode nodeByIdCustom = (TreeNode) null;
    for (int index = 0; index < node.Nodes.Count; ++index)
    {
      if ((node.Nodes[index].Tag as IFolder).Id.Equals(id))
      {
        nodeByIdCustom = node.Nodes[index];
        break;
      }
    }
    return nodeByIdCustom;
  }

  public static TreeNode FindNodeById(TreeNodeCollection tnc, object id)
  {
    TreeNode nodeById = (TreeNode) null;
    for (int index = 0; index < tnc.Count && !(tnc[index].Text == ClientConsts.FakeNodeString) && tnc[index].Tag != null; ++index)
    {
      if ((tnc[index].Tag as IFolder).Id.Equals(id))
      {
        nodeById = tnc[index];
        break;
      }
      nodeById = ClientCommons.FindNodeById(tnc[index].Nodes, id);
      if (nodeById != null)
        break;
    }
    return nodeById;
  }

  /// <summary>TimeSpan в строку дней часов минут секунд миллисекунд</summary>
  /// <param name="ts"></param>
  /// <returns></returns>
  public static string StrFormatTimeSpan(TimeSpan ts)
  {
    string str = string.Empty;
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = false;
    if (ts.TotalMilliseconds == 0.0)
    {
      str = "0";
    }
    else
    {
      if (ts.Days > 0)
      {
        str = str + ts.Days.ToString() + LocalizationHolder.rm.GetString("Client.Core_1064");
        flag1 = true;
      }
      if (ts.Hours > 0 | flag1)
      {
        str = str + ts.Hours.ToString() + LocalizationHolder.rm.GetString("Client.Core_1065");
        flag2 = true;
      }
      if (ts.Minutes > 0 | flag2)
      {
        str = str + ts.Minutes.ToString() + LocalizationHolder.rm.GetString("Client.Core_1066");
        flag3 = true;
      }
      if (ts.Seconds > 0 | flag3)
        str = str + ts.Seconds.ToString() + LocalizationHolder.rm.GetString("Client.Core_1067");
      if (ts.Milliseconds > 0)
        str = str + ts.Milliseconds.ToString() + LocalizationHolder.rm.GetString("Client.Core_1068");
    }
    return str;
  }
}
