// Decompiled with JetBrains decompiler
// Type: Intermech.Update.CodeFormers.ObjectCodeFormer
// Assembly: Intermech.Update, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 825FBF29-0112-4B23-8140-950E091D8F10
// Assembly location: D:\IPS\Client\Intermech.Update.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Interfaces.MetadataUpdates;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;

#nullable disable
namespace Intermech.Update.CodeFormers;

internal class ObjectCodeFormer : CodeFormer
{
  public ObjectCodeFormer()
    : base(2)
  {
  }

  public override XmlNode GenerateNode(
    IUserSession session,
    XmlDocument xmlDocument,
    Object4Script obj,
    string path4Files)
  {
    IServerBriefcase customService1 = session.GetCustomService(typeof (IServerBriefcase)) as IServerBriefcase;
    IIDLinkTranslate customService2 = session.GetCustomService(typeof (IIDLinkTranslate)) as IIDLinkTranslate;
    object Tag = (object) null;
    if (obj.CategoryID == 2)
    {
      IDBObject dbObject = session.GetObject((Guid) obj.ID);
      Tag = (object) MetaDataHelper.GetObjectTypeGuid(dbObject.ObjectType);
      long objectId = dbObject.ObjectID;
    }
    AttributableElements kind = obj.CategoryID == 2 ? AttributableElements.Object : AttributableElements.Relation;
    XmlNode node1 = this.CreateNode(xmlDocument, obj, Tag);
    if (node1 == null)
      return (XmlNode) null;
    foreach (ScriptNode property in obj.Properties)
    {
      if (property is ObjectProperty4Script)
      {
        string str1 = Convert.ToString((property as ObjectProperty4Script).PropertyID);
        object obj1 = (object) null;
        switch (str1)
        {
          case "F_ACCESS":
            IDBObject seсurity = session.GetObject((Guid) obj.ID);
            obj1 = (object) this.GetSecurity(session, seсurity as IDBSecurity);
            break;
          case "F_AREA_ID":
            obj1 = (object) this.GetSubjectAreaProperty(session, (string) (property as ObjectProperty4Script).Value);
            break;
          case "F_LC_STEP":
            obj1 = this.GetLCStepProperty(session, (int) (property as ObjectProperty4Script).Value);
            break;
          case "F_OBJECT_TYPE":
            obj1 = this.GetObjectTypeProperty(session, (int) (property as ObjectProperty4Script).Value);
            break;
          case "F_OWNER_ID":
          case "F_PARENT_ID":
          case "F_PROJECT_ID":
          case "F_PROJ_ID":
            long num = (long) (property as ObjectProperty4Script).Value;
            switch (num)
            {
              case -1:
              case 0:
                break;
              default:
                IDBObject dbObject1 = session.GetObject(num, false);
                if (dbObject1 != null)
                {
                  obj1 = (object) dbObject1.ObjectGUID;
                  if (str1 == "F_OWNER_ID" && !CodeFormer.IsGuidAllowableForScript(str1, (Guid) obj1, out string _))
                    obj1 = (object) Guid.Parse("cad00016-306c-11d8-b4e9-00304f19f545");
                }
                if (obj.CategoryID == 5)
                {
                  long relationId = session.GetRelation((Guid) obj.ID, num).RelationID;
                  break;
                }
                break;
            }
            break;
          case "F_PART_ID":
            long id = (long) (property as ObjectProperty4Script).Value;
            switch (id)
            {
              case -1:
              case 0:
                break;
              default:
                IDBObject objectById = session.GetObjectByID(id, false);
                if (objectById != null)
                {
                  obj1 = (object) objectById.GUID;
                  break;
                }
                break;
            }
            break;
          case "F_RELATION_TYPE":
            obj1 = this.GetRelationProperty(session, (int) (property as ObjectProperty4Script).Value);
            break;
          default:
            if (UpdateScriptHelper.IsAttributeNode(str1))
            {
              IDBAttributeType attributeType = session.GetAttributeType(UpdateScriptHelper.GetAttributeGuidFromNode(str1), true);
              IDBAttributable dbAttributable = (IDBAttributable) null;
              long attributableID = 0;
              string str2 = string.Empty;
              switch (obj.CategoryID)
              {
                case 2:
                  dbAttributable = (IDBAttributable) session.GetObject((Guid) obj.ID);
                  attributableID = (dbAttributable as IDBObject).ObjectID;
                  str2 = (dbAttributable as IDBObject).ObjectGUID.ToString().ToLower();
                  break;
                case 5:
                  dbAttributable = (IDBAttributable) session.GetRelation((Guid) obj.ID, -1L);
                  attributableID = (dbAttributable as IDBRelation).RelationID;
                  str2 = (dbAttributable as IDBRelation).GUID.ToString().ToLower();
                  break;
              }
              IDBAttribute attributeById = dbAttributable.GetAttributeByID(attributeType.AttributeID);
              List<UpdateScriptAttributeValue> scriptAttributeValueList = new List<UpdateScriptAttributeValue>();
              for (int index = 0; index < attributeById.ValuesCount; ++index)
              {
                object attrValueOriginal1 = attributeById.Value;
                attributeById.Index = index;
                UpdateScriptAttributeValue scriptAttributeValue = new UpdateScriptAttributeValue();
                scriptAttributeValue.InLisID = index;
                switch (attributeType.AttributeType)
                {
                  case FieldTypes.ftString:
                  case FieldTypes.ftGuid:
                    object attrValueCurrent1 = (object) null;
                    customService1.GetLinkedDataByAttribute(session.SessionGUID, 1, kind, attributableID, attributeById.AttributeID, attrValueOriginal1, ref attrValueCurrent1);
                    if (attrValueCurrent1 != null)
                    {
                      scriptAttributeValue.IsEmpty = false;
                      scriptAttributeValue.StringValue = Convert.ToString(attrValueCurrent1);
                      break;
                    }
                    scriptAttributeValue.StringValue = attributeById.AsString;
                    scriptAttributeValue.IsEmpty = false;
                    break;
                  case FieldTypes.ftInteger:
                    object attrValueCurrent2 = (object) null;
                    customService1.GetLinkedDataByAttribute(session.SessionGUID, 1, kind, attributableID, attributeById.AttributeID, attrValueOriginal1, ref attrValueCurrent2);
                    if (attrValueCurrent2 != null)
                    {
                      scriptAttributeValue.IsEmpty = false;
                      scriptAttributeValue.IntegerValue = Convert.ToInt64(attrValueCurrent2);
                      break;
                    }
                    if (customService2 != null && customService2.IsIDLink((attributeType as IDBGuid).GUID))
                    {
                      if (attributeById.AsInteger != 0L && attributeById.AsInteger != -1L)
                      {
                        IDBObject dbObject2 = session.GetObject(attributeById.AsInteger, false);
                        if (dbObject2 != null)
                        {
                          scriptAttributeValue.IsEmpty = false;
                          scriptAttributeValue.Tag = (object) dbObject2.ObjectGUID;
                          scriptAttributeValue.StringValue = dbObject2.Caption;
                          break;
                        }
                        break;
                      }
                      break;
                    }
                    scriptAttributeValue.IsEmpty = false;
                    scriptAttributeValue.IntegerValue = attributeById.AsInteger;
                    break;
                  case FieldTypes.ftDouble:
                    object attrValueCurrent3 = (object) null;
                    customService1.GetLinkedDataByAttribute(session.SessionGUID, 1, kind, attributableID, attributeById.AttributeID, attrValueOriginal1, ref attrValueCurrent3);
                    if (attrValueCurrent3 != null)
                    {
                      scriptAttributeValue.IsEmpty = false;
                      scriptAttributeValue.DoubleValue = Convert.ToDouble(attrValueCurrent3);
                      break;
                    }
                    scriptAttributeValue.DoubleValue = attributeById.AsDouble;
                    scriptAttributeValue.IsEmpty = false;
                    break;
                  case FieldTypes.ftDateTime:
                    object attrValueCurrent4 = (object) null;
                    customService1.GetLinkedDataByAttribute(session.SessionGUID, 1, kind, attributableID, attributeById.AttributeID, attrValueOriginal1, ref attrValueCurrent4);
                    if (attrValueCurrent4 != null)
                    {
                      scriptAttributeValue.IsEmpty = false;
                      scriptAttributeValue.DateTimeValue = Convert.ToDateTime(attrValueCurrent4);
                      break;
                    }
                    scriptAttributeValue.DateTimeValue = attributeById.AsDateTime;
                    scriptAttributeValue.IsEmpty = false;
                    break;
                  case FieldTypes.ftShortBlob:
                  case FieldTypes.ftFile:
                  case FieldTypes.ftBlob:
                    string path2_1 = $"blob{str2}{(attributeType as IDBGuid).GUID.ToString().ToLower()}_{index}.dat";
                    if (attributeById is IBlobReader blobReader)
                    {
                      BlobInformation blobInformation = blobReader.OpenBlob(0);
                      try
                      {
                        if (blobInformation.RealFileSize > 0L)
                        {
                          string path = Path.Combine(path4Files, path2_1);
                          FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
                          try
                          {
                            if (attributeById.DataType == FieldTypes.ftShortBlob)
                            {
                              MemoryStream attrValueOriginal2 = new MemoryStream(blobReader.ReadDataBlock(0));
                              try
                              {
                                object attrValueCurrent5 = (object) null;
                                customService1.GetLinkedDataByAttribute(session.SessionGUID, 1, kind, attributableID, attributeById.AttributeID, (object) attrValueOriginal2, ref attrValueCurrent5);
                                if (attrValueCurrent5 is MemoryStream)
                                  attrValueOriginal2 = attrValueCurrent5 as MemoryStream;
                                fileStream.Write(attrValueOriginal2.ToArray(), 0, Convert.ToInt32(attrValueOriginal2.Length));
                                scriptAttributeValue.DoubleValue = blobInformation.ArcMethod == ArcMethods.NotPacked ? (double) attrValueOriginal2.Length : 0.0;
                                this.temporaries.Enqueue(path);
                              }
                              finally
                              {
                                attrValueOriginal2.Close();
                              }
                            }
                            else
                            {
                              byte[] buffer = blobReader.ReadDataBlock(0);
                              if (buffer != null)
                                fileStream.Write(buffer, 0, buffer.Length);
                              scriptAttributeValue.DoubleValue = (double) blobInformation.RealFileSize;
                            }
                          }
                          finally
                          {
                            fileStream.Flush();
                            fileStream.Close();
                          }
                          scriptAttributeValue.IsEmpty = false;
                          scriptAttributeValue.Tag = (object) $"{path2_1}|{(int) blobInformation.ArcMethod}";
                          scriptAttributeValue.DateTimeValue = blobInformation.ModifyDate;
                          scriptAttributeValue.IntegerValue = blobInformation.BlobID;
                          scriptAttributeValue.StringValue = attributeType.AttributeType == FieldTypes.ftFile ? blobInformation.FileName : blobInformation.Note;
                          break;
                        }
                        break;
                      }
                      finally
                      {
                        blobReader.CloseBlob();
                      }
                    }
                    else
                      break;
                  case FieldTypes.ftObjectLink:
                    if (attributeById.AsInteger != 0L && attributeById.AsInteger != -1L)
                    {
                      IDBObject dbObject3 = session.GetObject(attributeById.AsInteger, false);
                      if (dbObject3 != null)
                      {
                        scriptAttributeValue.IsEmpty = false;
                        scriptAttributeValue.Tag = (object) dbObject3.ObjectGUID;
                        scriptAttributeValue.StringValue = dbObject3.Caption;
                        break;
                      }
                      break;
                    }
                    break;
                  case FieldTypes.ftMemo:
                    string path2_2 = $"blob{str2}{(attributeType as IDBGuid).GUID.ToString().ToLower()}_{index}.dat";
                    if (attributeById is IMemoReader memoReader)
                    {
                      if (memoReader.OpenMemo(0) > 0)
                      {
                        object attrValueCurrent6 = (object) memoReader.ReadDataBlock();
                        memoReader.CloseMemo();
                        char[] attrValueOriginal3 = attrValueCurrent6 != null ? (char[]) ((Array) attrValueCurrent6).Clone() : (char[]) null;
                        customService1.GetLinkedDataByAttribute(session.SessionGUID, 1, kind, attributableID, attributeById.AttributeID, (object) attrValueOriginal3, ref attrValueCurrent6);
                        string path = Path.Combine(path4Files, path2_2);
                        FileStream output = new FileStream(path, FileMode.Create, FileAccess.Write);
                        try
                        {
                          new BinaryWriter((Stream) output, Encoding.UTF8).Write((char[]) attrValueCurrent6, 0, Convert.ToInt32(((char[]) attrValueCurrent6).Length));
                          this.temporaries.Enqueue(path);
                        }
                        finally
                        {
                          output.Flush();
                          output.Close();
                        }
                      }
                      scriptAttributeValue.IsEmpty = false;
                      scriptAttributeValue.StringValue = path2_2;
                      break;
                    }
                    break;
                  case FieldTypes.ftBoolean:
                    object attrValueCurrent7 = (object) null;
                    customService1.GetLinkedDataByAttribute(session.SessionGUID, 1, kind, attributableID, attributeById.AttributeID, attrValueOriginal1, ref attrValueCurrent7);
                    if (attrValueCurrent7 != null)
                    {
                      scriptAttributeValue.IsEmpty = false;
                      scriptAttributeValue.IntegerValue = Convert.ToInt64(attrValueCurrent7);
                      break;
                    }
                    scriptAttributeValue.IntegerValue = attributeById.AsInteger;
                    scriptAttributeValue.IsEmpty = false;
                    break;
                  case FieldTypes.ftMeasured:
                    if (attributeById.Value is MeasuredValue measuredValue)
                    {
                      scriptAttributeValue.IsEmpty = false;
                      scriptAttributeValue.StringValue = measuredValue.ToString();
                      break;
                    }
                    break;
                  default:
                    scriptAttributeValue.IsEmpty = false;
                    switch (attributeType.ValueFieldName)
                    {
                      case "F_INTEGER_VALUE":
                        scriptAttributeValue.IntegerValue = attributeById.AsInteger;
                        break;
                      case "F_DOUBLE_VALUE":
                        scriptAttributeValue.DoubleValue = attributeById.AsDouble;
                        break;
                      case "F_STRING_VALUE":
                        scriptAttributeValue.StringValue = attributeById.AsString;
                        break;
                      case "F_DATE_VALUE":
                        scriptAttributeValue.DateTimeValue = attributeById.AsDateTime;
                        break;
                    }
                    break;
                }
                if (!scriptAttributeValue.IsEmpty)
                  scriptAttributeValueList.Add(scriptAttributeValue);
                else
                  scriptAttributeValueList.Add((UpdateScriptAttributeValue) null);
              }
              obj1 = (object) scriptAttributeValueList.ToArray();
              break;
            }
            obj1 = (property as ObjectProperty4Script).Value;
            break;
        }
        node1.AppendChild(this.CreateProperty(xmlDocument, (property as ObjectProperty4Script).Obligatory, str1, obj1));
      }
      else if (property is Object4Script)
      {
        XmlNode node2 = this.GenerateNode(session, xmlDocument, property as Object4Script, path4Files);
        if (node2 != null)
          node1.AppendChild(node2);
      }
    }
    return node1;
  }

  public override List<ScriptNode> GetProperties(IUserSession session, object dbObject)
  {
    IDBObject dbObject1 = dbObject as IDBObject;
    List<ScriptNode> properties = new List<ScriptNode>();
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_LC_STEP", DataSetProcessor.GetCaption("F_LC_STEP"), (object) dbObject1.LCStep));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_OWNER_ID", DataSetProcessor.GetCaption("F_OWNER_ID"), (object) dbObject1.OwnerID));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_PARENT_ID", DataSetProcessor.GetCaption("F_PARENT_ID"), (object) dbObject1.ParentVersionID));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_PROJECT_ID", DataSetProcessor.GetCaption("F_PROJECT_ID"), (object) dbObject1.ProjectID));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_OBJECT_TYPE", DataSetProcessor.GetCaption("F_OBJECT_TYPE"), (object) dbObject1.ObjectType));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_AREA_ID", DataSetProcessor.GetCaption("F_AREA_ID"), (object) dbObject1.SubjectAreas));
    Guid guid = dbObject1.GUID;
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_GUID", string.Empty, (object) dbObject1.GUID, true, false));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_ACCESS", UpdateScriptHelper.AccessNodeText, (object) null));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "CAPTION", DataSetProcessor.GetCaption("CAPTION"), (object) dbObject1.Caption));
    IDBAttribute4TypeCollection attributes = session.GetObjectType(dbObject1.ObjectType).Attributes;
    for (int AttrIndex = 0; AttrIndex < dbObject1.Attributes.Count; ++AttrIndex)
    {
      IDBAttribute attribute = dbObject1.Attributes[AttrIndex];
      if (!attribute.TemporaryAttribute)
      {
        IDBAttributeType attributeType = session.GetAttributeType(attribute.AttributeID);
        properties.Add((ScriptNode) new ObjectProperty4Script((object) UpdateScriptHelper.GetAttributeNodeNameFromGuid((attributeType as IDBGuid).GUID, true), $"Атрибут \"{attributeType.Name}\"", (object) null));
      }
    }
    IDBRelationCollection relationCollection = session.GetRelationCollection(-1);
    IDBRelationsApplicabilityCollection applicabilityCollection = session.GetRelationsApplicabilityCollection();
    DataTable applicabilitiesList1 = applicabilityCollection.GetApplicabilitiesList(-1, dbObject1.ObjectType, -1);
    List<int> parentTypeIDs1 = new List<int>(applicabilitiesList1.Rows.Count);
    for (int index = 0; index < applicabilitiesList1.Rows.Count; ++index)
    {
      if (Convert.ToInt32(applicabilitiesList1.Rows[index]["F_MIN_LINKS"]) != -1)
        parentTypeIDs1.Add(Convert.ToInt32(applicabilitiesList1.Rows[index]["F_INOBJECT_TYPE"]));
    }
    if (parentTypeIDs1.Count > 0)
    {
      relationCollection.ChildObjectTypes = (IList<int>) MetaDataHelper.GetLocalObjectTypeChildrenIDRecursive((IEnumerable<int>) parentTypeIDs1);
      DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[3]
      {
        (object) -23,
        (object) -20,
        (object) -21
      });
      DataTable dataTable = relationCollection.EntersInVersion(paramSet, dbObject1.ObjectID);
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        IDBRelationType relationType = session.GetRelationType(Convert.ToInt32(dataTable.Rows[index][0]));
        IDBRelation relation = session.GetRelation(Convert.ToInt64(dataTable.Rows[index][1]));
        IDBObject dbObject2 = session.GetObject(Convert.ToInt64(dataTable.Rows[index][2]));
        Object4Script objAttribute = new Object4Script(5, (object) relation.GUID, $"{relationType.ReverseName} {dbObject2.NameInMessages} связью типа {relationType.Description}");
        this.AddRelationAttributes(objAttribute, relation);
        properties.Add((ScriptNode) objAttribute);
      }
    }
    DataTable applicabilitiesList2 = applicabilityCollection.GetApplicabilitiesList(-1, -1, dbObject1.ObjectType);
    List<int> parentTypeIDs2 = new List<int>(applicabilitiesList2.Rows.Count);
    for (int index = 0; index < applicabilitiesList2.Rows.Count; ++index)
    {
      if (Convert.ToInt32(applicabilitiesList2.Rows[index]["F_MIN_LINKS"]) != -1)
        parentTypeIDs2.Add(Convert.ToInt32(applicabilitiesList2.Rows[index]["F_OBJECT_TYPE"]));
    }
    if (parentTypeIDs2.Count > 0)
    {
      DBRecordSetParams paramSet = new DBRecordSetParams((ConditionStructure[]) null, new object[3]
      {
        (object) -23,
        (object) -20,
        (object) -22
      });
      relationCollection.ChildObjectTypes = (IList<int>) MetaDataHelper.GetLocalObjectTypeChildrenIDRecursive((IEnumerable<int>) parentTypeIDs2);
      DataTable dataTable = relationCollection.ConsistFrom(paramSet, dbObject1.ObjectID);
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        IDBRelationType relationType = session.GetRelationType(Convert.ToInt32(dataTable.Rows[index][0]));
        IDBRelation relation = session.GetRelation(Convert.ToInt64(dataTable.Rows[index][1]));
        IDBObject objectById = session.GetObjectByID(Convert.ToInt64(dataTable.Rows[index][2]), true);
        Object4Script objAttribute = new Object4Script(5, (object) relation.GUID, $"{relationType.TypeName} {objectById.NameInMessages} связью типа {relationType.Description}");
        this.AddRelationAttributes(objAttribute, relation);
        properties.Add((ScriptNode) objAttribute);
      }
    }
    return properties;
  }

  private void AddRelationAttributes(Object4Script objAttribute, IDBRelation relation)
  {
    objAttribute.Properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_CREATE_DATE", DataSetProcessor.GetCaption("F_CREATE_DATE"), (object) relation.CreateDate));
    objAttribute.Properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_END_DATE", DataSetProcessor.GetCaption("F_END_DATE"), (object) relation.DeleteDate));
    objAttribute.Properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_RELATION_TYPE", string.Empty, (object) relation.RelationType, true, false));
    objAttribute.Properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_PROJ_ID", string.Empty, (object) relation.ProjID, true, false));
    objAttribute.Properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_PART_ID", string.Empty, (object) relation.PartID, true, false));
    for (int AttrIndex = 0; AttrIndex < relation.Attributes.Count; ++AttrIndex)
    {
      IDBAttribute attribute = relation.Attributes[AttrIndex];
      if (!attribute.TemporaryAttribute)
        objAttribute.Properties.Add((ScriptNode) new ObjectProperty4Script((object) UpdateScriptHelper.GetAttributeNodeNameFromGuid((attribute as IDBGuid).GUID, true), attribute.Name, (object) null));
    }
  }
}
