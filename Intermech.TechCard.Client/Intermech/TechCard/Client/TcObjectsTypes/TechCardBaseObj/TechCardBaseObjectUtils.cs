// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.TechCardBaseObj.TechCardBaseObjectUtils
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.TechCardBaseObj;

/// <summary>TechCard utilities for base objects</summary>
internal static class TechCardBaseObjectUtils
{
  /// <summary>Utilities for attributes</summary>
  internal static class Attributes
  {
    /// <summary>Get imbase attributes list</summary>
    /// <param name="objectTypeId">Object type id</param>
    /// <param name="session">User session</param>
    /// <returns> Dictionary (key = attribute id, value = imbase catalog id)</returns>
    public static Dictionary<int, long> GetImbaseAttributes(int objectTypeId, IUserSession session)
    {
      Dictionary<int, long> imbaseAttributes = new Dictionary<int, long>();
      if (objectTypeId == 0 || session == null || !(TechCardClient.ServiceProvider.GetService(typeof (IImbaseSelector)) is IImbaseSelector service))
        return imbaseAttributes;
      List<ImbaseObjectAttrLink> imbaseObjectAttrLinks = service.GetImbaseObjectAttrLinks(objectTypeId);
      if (imbaseObjectAttrLinks == null || imbaseObjectAttrLinks.Count == 0)
        return imbaseAttributes;
      foreach (ImbaseObjectAttrLink imbaseObjectAttrLink in imbaseObjectAttrLinks)
      {
        if (imbaseObjectAttrLink._objectTypeID != -1 && !imbaseAttributes.ContainsKey(imbaseObjectAttrLink._attribiteID))
          imbaseAttributes.Add(imbaseObjectAttrLink._attribiteID, imbaseObjectAttrLink._imbaseObjID);
      }
      return imbaseAttributes;
    }

    /// <summary>Get link attributes list</summary>
    /// <param name="objectTypeId">Object type id</param>
    /// <param name="session">User session</param>
    /// <returns> </returns>
    public static List<int> GetLinkAttributes(int objectTypeId, IUserSession session)
    {
      List<int> linkAttributes = new List<int>();
      if (objectTypeId == 0 || session == null)
        return linkAttributes;
      IDBObjectType objectType = session.GetObjectType(objectTypeId);
      if (objectType == null)
        return linkAttributes;
      foreach (DataRow row in (InternalDataCollectionBase) objectType.Attributes.Select("", (object[]) null).Rows)
      {
        int result;
        int.TryParse(row["F_ATTRIBUTE_ID"].ToString(), out result);
        if (result != 0)
        {
          IDBAttributeType attributeType = session.GetAttributeType(result, false);
          if (attributeType != null && attributeType.AttributeType == FieldTypes.ftObjectLink)
            linkAttributes.Add(result);
        }
      }
      return linkAttributes;
    }

    /// <summary>Copy all imbase attributes (for the same catalogs)</summary>
    /// <param name="sourceObjId">Source object ID</param>
    /// <param name="destObjId">Destination object ID</param>
    /// <param name="copyAll">Copy all available attributes ( not assigned directly for destination object type)</param>
    /// <param name="session">User session</param>
    public static void CopyImbaseAttributes(
      long sourceObjId,
      long destObjId,
      bool copyAll,
      IUserSession session)
    {
      if (session == null || sourceObjId == 0L || destObjId == 0L)
        return;
      IDBObject dbObject1 = session.GetObject(sourceObjId, false);
      IDBObject dbObject2 = session.GetObject(destObjId, false);
      if (dbObject1 == null || dbObject2 == null)
        return;
      Dictionary<int, long> imbaseAttributes1 = TechCardBaseObjectUtils.Attributes.GetImbaseAttributes(dbObject1.ObjectType, session);
      if (imbaseAttributes1 == null || imbaseAttributes1.Count == 0)
        return;
      if (copyAll)
      {
        IDBObjectType objectType = session.GetObjectType(dbObject2.ObjectType, false);
        if (objectType == null)
          return;
        copyAll = objectType.AnyAttributes;
      }
      Dictionary<int, long> imbaseAttributes2 = TechCardBaseObjectUtils.Attributes.GetImbaseAttributes(dbObject2.ObjectType, session);
      if (!copyAll && (imbaseAttributes2 == null || imbaseAttributes2.Count == 0))
        return;
      Dictionary<int, int> dictionary = new Dictionary<int, int>();
      List<int> intList = new List<int>((IEnumerable<int>) imbaseAttributes2.Keys);
      List<long> longList = new List<long>((IEnumerable<long>) imbaseAttributes2.Values);
      foreach (KeyValuePair<int, long> keyValuePair in imbaseAttributes1)
      {
        long num = keyValuePair.Value;
        int index1 = -1;
        for (int index2 = 0; index2 < imbaseAttributes2.Values.Count; ++index2)
        {
          if (longList[index2] == num)
          {
            index1 = index2;
            break;
          }
        }
        if (index1 != -1 || copyAll)
          dictionary.Add(keyValuePair.Key, index1 != -1 ? intList[index1] : keyValuePair.Key);
      }
      if (dictionary.Count == 0)
        return;
      IImbaseServer customService = session.GetCustomService(typeof (IImbaseServer)) as IImbaseServer;
      foreach (KeyValuePair<int, int> keyValuePair in dictionary)
      {
        int key = keyValuePair.Key;
        int masterAttributeID = keyValuePair.Value;
        IDBAttribute attributeById = dbObject1.GetAttributeByID(key);
        if (attributeById != null)
        {
          long result = 0;
          if (attributeById.Value != null)
            long.TryParse(attributeById.Value.ToString(), out result);
          if (result != 0L && customService != null)
            customService.FillObjectLinkAttributes(session.SessionGUID, destObjId, masterAttributeID, result);
        }
      }
    }

    /// <summary>Copy all link attributes</summary>
    /// <param name="sourceObjId">Source object ID</param>
    /// <param name="destObjId">Destination object ID</param>
    /// <param name="copyAll">Copy all available attributes ( not assigned directly for destination object type)</param>
    /// <param name="session">User session</param>
    public static void CopyLinkAttributes(
      long sourceObjId,
      long destObjId,
      bool copyAll,
      IUserSession session)
    {
      if (sourceObjId == 0L || destObjId == 0L)
        return;
      IDBObject dbObject1 = session.GetObject(destObjId, false);
      IDBObject dbObject2 = session.GetObject(sourceObjId, false);
      if (dbObject1 == null || dbObject2 == null)
        return;
      List<int> intList1 = new List<int>();
      List<int> linkAttributes1 = TechCardBaseObjectUtils.Attributes.GetLinkAttributes(dbObject2.ObjectType, session);
      if (copyAll)
      {
        IDBObjectType objectType = session.GetObjectType(dbObject2.ObjectType, false);
        if (objectType == null)
          return;
        copyAll = objectType.AnyAttributes;
      }
      if (!copyAll)
      {
        List<int> linkAttributes2 = TechCardBaseObjectUtils.Attributes.GetLinkAttributes(dbObject1.ObjectType, session);
        foreach (int num in linkAttributes1)
        {
          if (linkAttributes2.Contains(num))
            intList1.Add(num);
        }
      }
      else
        intList1.AddRange((IEnumerable<int>) linkAttributes1);
      if (intList1.Count == 0)
        return;
      List<int> intList2 = new List<int>();
      List<AttributeValues> attributeValuesList = new List<AttributeValues>();
      foreach (int attributeID in intList1)
      {
        IDBAttribute attributeById = dbObject2.GetAttributeByID(attributeID);
        if (attributeById != null)
        {
          attributeValuesList.Add(new AttributeValues(attributeID, attributeById.Value));
          intList2.Add(attributeID);
        }
      }
      dbObject1.SetAttributesValues(attributeValuesList.ToArray());
      if (intList2.Count <= 0)
        return;
      dbObject1.Attributes.SetDependentAttributes(intList2.ToArray());
    }
  }
}
