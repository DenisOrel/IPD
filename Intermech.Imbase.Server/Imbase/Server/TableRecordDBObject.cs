// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.TableRecordDBObject
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Kernel;
using System;
using System.Data;

#nullable disable
namespace Intermech.Imbase.Server;

internal class TableRecordDBObject : DBObject, IContextedDBObject
{
  private long _parentId;

  internal TableRecordDBObject(UserSession uSession, DataTable objectsTable)
    : base(uSession, objectsTable)
  {
    this._parentId = -1L;
  }

  public override IDBAttributeCollection Attributes
  {
    get
    {
      if (this._Attributes == null)
      {
        IDBAttributeCollection attributes = base.Attributes;
        if (this._parentId != -1L)
          TableRecordDBObject.AddVirtualAttributes(this._parentId, this.ObjectType, attributes, this.Session);
      }
      return base.Attributes;
    }
  }

  private static void AddVirtualAttributes(
    long contextId,
    int thisObjectTypeId,
    IDBAttributeCollection attributes,
    IUserSession session)
  {
    if (contextId == -1L)
      return;
    long num = -1;
    long objectID = -1;
    QuickObjectInfo objectInfo = session.GetObjectInfo(contextId);
    if (contextId != -1L)
    {
      int objectTypeId = objectInfo.ObjectTypeID;
      if (objectTypeId != Intermech.Imbase.Consts.ImbaseTableTypeID && objectTypeId != Intermech.Imbase.Consts.ImbaseTableRefTypeID)
        return;
      if (objectTypeId == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
      {
        num = contextId;
        objectID = TableRecordDBObject.GetTableId(session, num);
      }
      else
        objectID = contextId;
    }
    session.GetObject(contextId);
    IDBAttribute4TypeCollection attributes1 = session.GetObjectType(thisObjectTypeId).Attributes;
    IDBAttributeCollection attributeCollection1 = (IDBAttributeCollection) null;
    IDBAttributeCollection attributeCollection2 = (IDBAttributeCollection) null;
    if (objectID != -1L)
      attributeCollection1 = session.GetObject(objectID).Attributes;
    if (num != -1L)
      attributeCollection2 = session.GetObject(num).Attributes;
    DataTable dataTable = attributes1.Select("");
    int count = dataTable.Rows.Count;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      int int32 = Convert.ToInt32(row["F_ATTRIBUTE_ID"]);
      if (attributes.FindByID(int32) == null)
      {
        IDBAttribute dbAttribute = (IDBAttribute) null;
        if (attributeCollection2 != null)
          dbAttribute = attributeCollection2.FindByID(int32);
        if ((dbAttribute == null || dbAttribute.IsNull) && attributeCollection1 != null)
          dbAttribute = attributeCollection1.FindByID(int32);
        if (dbAttribute != null)
          attributes.AddTemporaryAttribute(int32, false, dbAttribute.Values);
        else if ((attributes1.GetAttributeByID(int32) as IDBAttributeType4Object).Attribute4ObjectPropertiesStructure.ComputeValueMode == ComputeValueModes.JITValue)
          attributes.AddTemporaryAttribute(int32, false);
      }
    }
  }

  private static long GetTableId(IUserSession session, long linkId)
  {
    AttributeValues[] attributesValues = session.GetObject(linkId).GetAttributesValues(GetAttributeValuesModes.None);
    int length = attributesValues.Length;
    for (int index = 0; index < length; ++index)
    {
      AttributeValues attributeValues = attributesValues[index];
      if (attributeValues.AttributeID == Intermech.Imbase.Consts.ImbaseTableRefAttID)
        return Convert.ToInt64(attributeValues.Values[0]);
    }
    return -1;
  }

  public long ContextId
  {
    get => this._parentId;
    set
    {
      if (this._parentId == value)
        return;
      this._parentId = value;
      this._Attributes = (IDBAttributeCollection) null;
    }
  }
}
