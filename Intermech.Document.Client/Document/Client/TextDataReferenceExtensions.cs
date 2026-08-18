// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.TextDataReferenceExtensions
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Interfaces.Document;
using System;

#nullable disable
namespace Intermech.Document.Client;

public static class TextDataReferenceExtensions
{
  public static void SetReferenceToDBObject(
    this TableData docRow,
    Guid objectVersionGuid,
    bool passiveLink = true)
  {
    ReferenceToDBObject referenceToDbObject = new ReferenceToDBObject(RefToDBObjectType.rtSelectedObject, objectVersionGuid, passiveLink);
    docRow.AssignReference((ReferenceBase) referenceToDbObject, false, false);
  }

  public static void SetReferenceToDBRelation(
    this TableData docRow,
    Guid relationGuid,
    Guid objectVersionGuid,
    bool passiveLink = true)
  {
    ReferenceToDBObject referenceToDbObject = new ReferenceToDBObject(RefToDBObjectType.rtSelectedRelation, relationGuid, objectVersionGuid, passiveLink);
    docRow.AssignReference((ReferenceBase) referenceToDbObject, false, false);
  }

  public static void SetReferenceToDBObjectAttribute(
    this TextData textDocNode,
    Guid objectVersionGuid,
    Guid attributeGuid,
    bool passiveLink = true)
  {
    ReferenceToDBObjectAttribute dbObjectAttribute = new ReferenceToDBObjectAttribute((DocumentTreeNode) textDocNode, RefToDBObjectType.rtSelectedObject, (DBObjectInfoBase) new DBObjectInfo(objectVersionGuid), attributeGuid, -1, "", passiveLink);
    textDocNode.AssignReferenceToTextSource((ReferenceBase) dbObjectAttribute, true, false, false);
  }

  public static void SetReferenceToDBRelationAttribute(
    this TextData textDocNode,
    Guid relationGuid,
    Guid objectVersionGuid,
    Guid attributeGuid,
    bool passiveLink = true)
  {
    ReferenceToDBObjectAttribute dbObjectAttribute = new ReferenceToDBObjectAttribute((DocumentTreeNode) textDocNode, RefToDBObjectType.rtSelectedRelation, (DBObjectInfoBase) new DBRelationInfo(relationGuid, objectVersionGuid), attributeGuid, -1, "", passiveLink);
    textDocNode.AssignReferenceToTextSource((ReferenceBase) dbObjectAttribute, true, false, false);
  }
}
