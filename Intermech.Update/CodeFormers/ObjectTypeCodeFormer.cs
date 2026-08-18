// Decompiled with JetBrains decompiler
// Type: Intermech.Update.CodeFormers.ObjectTypeCodeFormer
// Assembly: Intermech.Update, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 825FBF29-0112-4B23-8140-950E091D8F10
// Assembly location: D:\IPS\Client\Intermech.Update.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.MetadataUpdates;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml;

#nullable disable
namespace Intermech.Update.CodeFormers;

internal class ObjectTypeCodeFormer : ExtensionCodeFormer
{
  public ObjectTypeCodeFormer()
    : base(4, "F_OBJECT_TYPE")
  {
  }

  public override XmlNode GenerateNode(
    IUserSession session,
    XmlDocument xmlDocument,
    Object4Script obj,
    string path4Files)
  {
    object obj1 = (object) null;
    if (obj.Tag != null)
      obj1 = obj.Tag;
    XmlNode node1 = this.CreateNode(xmlDocument, obj, obj1);
    if (node1 == null)
      return (XmlNode) null;
    foreach (ScriptNode property in obj.Properties)
    {
      if (property is ObjectProperty4Script)
      {
        string id = Convert.ToString((property as ObjectProperty4Script).PropertyID);
        object obj2 = (object) null;
        bool flag = true;
        switch (id)
        {
          case "F_ACCESS":
            IDBObjectType objectType = session.GetObjectType((Guid) obj.ID);
            obj2 = (object) this.GetSecurity(session, objectType as IDBSecurity);
            break;
          case "F_AREA_ID":
            obj2 = (object) this.GetSubjectAreaProperty(session, (string) (property as ObjectProperty4Script).Value);
            break;
          case "F_CAPTION_ATTRIBUTE":
            obj2 = this.GetAttributeProperty(session, (int) (property as ObjectProperty4Script).Value);
            break;
          case "F_CLASSIFY_TYPE":
            obj2 = (object) (int) ObjectsClassifyHelper.GetClassifierType(session, MetaDataHelper.GetObjectTypeID((Guid) obj.ID));
            break;
          case "F_DEFAULT_RELATION":
            obj2 = this.GetRelationProperty(session, (int) (property as ObjectProperty4Script).Value);
            break;
          case "F_DEFAULT_VALUE":
            if (obj.CategoryID == 3 && obj1 != null)
            {
              obj2 = this.GetDefaultValueProperty(session, (property as ObjectProperty4Script).Value, (FieldTypes) obj1);
              break;
            }
            break;
          case "F_EXTENSIONS":
            XmlNode newNode = this.CreateNewNode(xmlDocument, (property as ObjectProperty4Script).Obligatory, id);
            DataRow[] dataRowArray = (DataRow[]) (property as ObjectProperty4Script).Value;
            for (int index = 0; index < dataRowArray.Length; ++index)
              newNode.AppendChild(this.GetExtensionNode(session, xmlDocument, Convert.ToString(dataRowArray[index]["F_PARAM_NAME"]), Convert.ToInt32(dataRowArray[index]["F_INLIST_ID"]), Convert.ToInt32(dataRowArray[index]["F_CATEGORY_TYPE"]), Convert.ToString(dataRowArray[index]["F_VALUE"])));
            node1.AppendChild(newNode);
            flag = false;
            break;
          case "F_FORMULA":
            obj2 = (object) this.GetFormulaProperty(session, (string) (property as ObjectProperty4Script).Value);
            break;
          case "F_ICON":
            if (CompareValuesHelper.NormalizedValue((property as ObjectProperty4Script).Value) != null)
            {
              byte[] buffer = (byte[]) (property as ObjectProperty4Script).Value;
              string path2 = $"icon{4}{((Guid) obj.ID).ToString().ToLower()}.dat";
              FileStream fileStream = new FileStream(Path.Combine(path4Files, path2), FileMode.Create, FileAccess.Write);
              try
              {
                fileStream.Write(buffer, 0, buffer.Length);
              }
              finally
              {
                fileStream.Flush();
                fileStream.Close();
              }
              obj2 = (object) path2;
              this.temporaries.Enqueue(path2);
              break;
            }
            break;
          case "F_LEVEL_ID":
            obj2 = this.GetLevelProperty(session, (int) (property as ObjectProperty4Script).Value);
            break;
          case "F_MASTER_ID":
            obj2 = this.GetAttributeProperty(session, (int) (property as ObjectProperty4Script).Value);
            break;
          case "F_PARENT_ID":
            obj2 = this.GetObjectTypeProperty(session, (int) (property as ObjectProperty4Script).Value);
            break;
          case "F_SCHEMA_ID":
            obj2 = this.GetSchemaProperty(session, (int) (property as ObjectProperty4Script).Value);
            break;
          case "F_SOURCE_ID":
            obj2 = this.GetAttributeProperty(session, (int) (property as ObjectProperty4Script).Value);
            break;
          default:
            obj2 = (property as ObjectProperty4Script).Value;
            break;
        }
        if (flag)
          node1.AppendChild(this.CreateProperty(xmlDocument, (property as ObjectProperty4Script).Obligatory, id, obj2));
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
    IDBObjectType dbObjectType = dbObject as IDBObjectType;
    List<ScriptNode> properties = new List<ScriptNode>();
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_ANY_ATTRIBUTES", DataSetProcessor.GetCaption("F_ANY_ATTRIBUTES"), (object) dbObjectType.AnyAttributes));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_CAPTION_ATTRIBUTE", DataSetProcessor.GetCaption("F_CAPTION_ATTRIBUTE"), (object) dbObjectType.CaptionAttribute));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_DEFAULT_RELATION", DataSetProcessor.GetCaption("F_DEFAULT_RELATION"), (object) dbObjectType.DefaultRelation));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_ICON", DataSetProcessor.GetCaption("F_ICON"), (object) dbObjectType.Icon));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_NOTE", DataSetProcessor.GetCaption("F_NOTE"), (object) dbObjectType.Note));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_OBJ_NAME", DataSetProcessor.GetCaption("F_OBJ_NAME"), (object) dbObjectType.ObjectInstanceName));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_OBJ_TYPE_NAME", DataSetProcessor.GetCaption("F_OBJ_TYPE_NAME"), (object) dbObjectType.ObjectTypeName));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_SHORT_NAME", DataSetProcessor.GetCaption("F_SHORT_NAME"), (object) dbObjectType.ObjectTypeShortName));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_CLASSIFY_TYPE", "Классификация создаваемых объектов", (object) null));
    foreach (ObjectTypeOptions objectTypeOptions in Enum.GetValues(typeof (ObjectTypeOptions)))
    {
      if (objectTypeOptions != ObjectTypeOptions.None)
        properties.Add((ScriptNode) new ObjectProperty4Script((object) $"{"F_OPTIONS"}{(int) objectTypeOptions}", EnumDescConverter.GetEnumDescription((Enum) objectTypeOptions), (object) (int) (dbObjectType.Options & objectTypeOptions)));
    }
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_PARENT_ID", "Тип объектов, от которого унаследован данный тип", (object) dbObjectType.ParentTypeID));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_PUBLIC_LC", DataSetProcessor.GetCaption("F_PUBLIC_LC"), (object) (int) dbObjectType.PublicLC));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_SCHEMA_ID", DataSetProcessor.GetCaption("F_SCHEMA_ID"), (object) dbObjectType.SchemaID));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_VERSIONABLE", DataSetProcessor.GetCaption("F_VERSIONABLE"), (object) (int) dbObjectType.Versionable));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_AREA_ID", DataSetProcessor.GetCaption("F_AREA_ID"), (object) (dbObjectType as IDBSubjectArea).SubjectAreas));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_ACCESS", UpdateScriptHelper.AccessNodeText, (object) null));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_DEL_TIME", DataSetProcessor.GetCaption("F_DEL_TIME"), (object) dbObjectType.LifetimeReserve));
    DataRow[] extensions = this.GetExtensions(session, dbObjectType.ObjectType);
    if (extensions != null && extensions.Length != 0)
      properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_EXTENSIONS", "Расширенные метаданные", (object) extensions));
    IDBAttribute4TypeCollection attributes = dbObjectType.Attributes;
    DataTable dataTable = attributes.Select("F_ATTRIBUTE_ID");
    if (dataTable != null && dataTable.Rows.Count > 0)
    {
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        InheritModes int32 = (InheritModes) Convert.ToInt32(row["F_PUBLIC"]);
        if (int32 != InheritModes.Inherited)
        {
          IDBAttributeType4 attributeById = attributes.GetAttributeByID(Convert.ToInt32(row["F_ATTRIBUTE_ID"]));
          if (attributeById != null)
          {
            Object4Script object4Script = new Object4Script(3, (object) (attributeById as IDBGuid).GUID, attributeById.Name);
            object4Script.Tag = (object) (int) attributeById.AttributeType;
            object4Script.Properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_PUBLIC", DataSetProcessor.GetCaption("F_PUBLIC"), (object) (int) int32));
            object4Script.Properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_REQUIRED", DataSetProcessor.GetCaption("F_REQUIRED"), (object) Convert.ToInt32(row["F_REQUIRED"])));
            object4Script.Properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_VALIDATION_RULE", DataSetProcessor.GetCaption("F_VALIDATION_RULE"), (object) Convert.ToString(row["F_VALIDATION_RULE"])));
            object4Script.Properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_COMPUTED", DataSetProcessor.GetCaption("F_COMPUTED"), (object) Convert.ToInt32(row["F_COMPUTED"])));
            object4Script.Properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_FORMULA", DataSetProcessor.GetCaption("F_FORMULA"), (object) Convert.ToString(row["F_FORMULA"])));
            object4Script.Properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_UNIQUE", DataSetProcessor.GetCaption("F_UNIQUE"), (object) Convert.ToInt32(row["F_UNIQUE"])));
            object4Script.Properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_LEVEL_ID", DataSetProcessor.GetCaption("F_LEVEL_ID"), (object) Convert.ToInt32(row["F_LEVEL_ID"])));
            object4Script.Properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_DEFAULT_VALUE", DataSetProcessor.GetCaption("F_DEFAULT_VALUE"), (object) Convert.ToString(row["F_DEFAULT_VALUE"])));
            object4Script.Properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_INVIEW", DataSetProcessor.GetCaption("F_INVIEW"), (object) Convert.ToInt32(row["F_INVIEW"])));
            object4Script.Properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_CONTENT", DataSetProcessor.GetCaption("F_CONTENT"), (object) (Convert.ToInt32(row["F_CONTENT"]) == 1)));
            object4Script.Properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_MASK", DataSetProcessor.GetCaption("F_MASK"), (object) Convert.ToString(row["F_MASK"])));
            object4Script.Properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_SOURCE_ID", DataSetProcessor.GetCaption("F_SOURCE_ID"), (object) Convert.ToInt32(row["F_SOURCE_ID"])));
            object4Script.Properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_MASTER_ID", DataSetProcessor.GetCaption("F_MASTER_ID"), (object) Convert.ToInt32(row["F_MASTER_ID"])));
            object4Script.Properties.AddRange((IEnumerable<ScriptNode>) this.GetProperties4AttrOptions((AttributeOptions) Convert.ToInt32(row["F_OPTIONS"])));
            properties.Add((ScriptNode) object4Script);
          }
        }
      }
    }
    DataTable table = (session as IClientSession).ClientCache.GetTable("IMS_RELATION_TYPES");
    DataTable applicabilitiesList = session.GetRelationsApplicabilityCollection().GetApplicabilitiesList(-1, dbObjectType.ObjectType, -1);
    properties.AddRange((IEnumerable<ScriptNode>) this.GetApplicabilities(session, applicabilitiesList, table, true));
    return properties;
  }

  private List<ScriptNode> GetApplicabilities(
    IUserSession session,
    DataTable table,
    DataTable relationTypes,
    bool reverse)
  {
    List<ScriptNode> applicabilities = new List<ScriptNode>();
    for (int index = 0; index < table.Rows.Count; ++index)
    {
      InheritModes int32_1 = (InheritModes) Convert.ToInt32(table.Rows[index]["F_PUBLIC"]);
      if (int32_1 != InheritModes.Inherited)
      {
        int anObjectType = reverse ? Convert.ToInt32(table.Rows[index]["F_INOBJECT_TYPE"]) : Convert.ToInt32(table.Rows[index]["F_OBJECT_TYPE"]);
        int int32_2 = Convert.ToInt32(table.Rows[index]["F_RELATION_TYPE"]);
        IDBObjectType objectType = session.GetObjectType(anObjectType);
        DataRow dataRow = relationTypes.Rows.Find((object) int32_2);
        Object4Script object4Script = new Object4Script(6, (object) new Guid(Convert.ToString(dataRow["F_GUID"])), $"{dataRow["F_DESCRIPTION"]} - {(reverse ? dataRow["F_REVERSE_NAME"] : dataRow["F_TYPE_NAME"])} : {objectType.ObjectTypeName}");
        object4Script.Tag = (object) (reverse ? 1 : 0);
        object4Script.Properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_OBJECT_TYPE", DataSetProcessor.GetCaption("F_OBJECT_TYPE"), (object) (objectType as IDBGuid).GUID, true, false));
        object4Script.Properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_PUBLIC", DataSetProcessor.GetCaption("F_PUBLIC"), (object) (int) int32_1));
        object4Script.Properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_CONTENT", DataSetProcessor.GetCaption("F_CONTENT"), (object) (Convert.ToInt32(table.Rows[index]["F_CONTENT"]) == 1)));
        object4Script.Properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_MAX_LINKS", "Максимальное количество связей", (object) Convert.ToInt32(table.Rows[index]["F_MAX_LINKS"])));
        object4Script.Properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_CHKOUTFILE", DataSetProcessor.GetCaption("F_CHKOUTFILE"), (object) (Convert.ToInt32(table.Rows[index]["F_CHKOUTFILE"]) == 1)));
        object4Script.Properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_MIN_LINKS", "Контроль существования связи", (object) Convert.ToInt32(table.Rows[index]["F_MIN_LINKS"])));
        object4Script.Properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_CONSTRAINT_MODE", "Способ обработки удаления связанных объектов", (object) Convert.ToInt32(table.Rows[index]["F_CONSTRAINT_MODE"])));
        object4Script.Properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_CLONE_RELATIONS", "Копировать связи при создании по прототипу", (object) (Convert.ToInt32(table.Rows[index]["F_CLONE_RELATIONS"]) == 1)));
        ApplicabilityOptions int32_3 = (ApplicabilityOptions) Convert.ToInt32(table.Rows[index]["F_OPTIONS"]);
        foreach (ApplicabilityOptions applicabilityOptions in Enum.GetValues(typeof (ApplicabilityOptions)))
        {
          if (applicabilityOptions != ApplicabilityOptions.None)
            object4Script.Properties.Add((ScriptNode) new ObjectProperty4Script((object) $"{"F_OPTIONS"}{(int) applicabilityOptions}", EnumDescConverter.GetEnumDescription((Enum) applicabilityOptions), (object) (int) (int32_3 & applicabilityOptions)));
        }
        applicabilities.Add((ScriptNode) object4Script);
      }
    }
    return applicabilities;
  }
}
