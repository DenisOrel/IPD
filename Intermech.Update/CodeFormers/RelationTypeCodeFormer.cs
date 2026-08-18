// Decompiled with JetBrains decompiler
// Type: Intermech.Update.CodeFormers.RelationTypeCodeFormer
// Assembly: Intermech.Update, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 825FBF29-0112-4B23-8140-950E091D8F10
// Assembly location: D:\IPS\Client\Intermech.Update.dll

using Intermech.Interfaces;
using Intermech.Interfaces.MetadataUpdates;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml;

#nullable disable
namespace Intermech.Update.CodeFormers;

internal class RelationTypeCodeFormer : ExtensionCodeFormer
{
  public RelationTypeCodeFormer()
    : base(6, "F_RELATION_TYPE")
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
            IDBRelationType relationType1 = session.GetRelationType((Guid) obj.ID);
            obj2 = (object) this.GetSecurity(session, relationType1 as IDBSecurity);
            break;
          case "F_AREA_ID":
            obj2 = (object) this.GetSubjectAreaProperty(session, (string) (property as ObjectProperty4Script).Value);
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
              IDBRelationType relationType2 = session.GetRelationType((Guid) obj.ID);
              byte[] buffer = (byte[]) (property as ObjectProperty4Script).Value;
              string path2 = $"icon{6}{(relationType2 as IDBGuid).GUID.ToString().ToLower()}.dat";
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
    IDBRelationType dbRelationType = dbObject as IDBRelationType;
    List<ScriptNode> properties = new List<ScriptNode>();
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_ANY_ATTRIBUTES", DataSetProcessor.GetCaption("F_ANY_ATTRIBUTES"), (object) dbRelationType.AnyAttributes));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_CHKOUTFILE", DataSetProcessor.GetCaption("F_CHKOUTFILE"), (object) dbRelationType.CheckoutFile));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_DESCRIPTION", DataSetProcessor.GetCaption("F_DESCRIPTION"), (object) dbRelationType.Description));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_ICON", DataSetProcessor.GetCaption("F_ICON"), (object) dbRelationType.Icon));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_NOTE", DataSetProcessor.GetCaption("F_NOTE"), (object) dbRelationType.Note));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_REVERSE_NAME", DataSetProcessor.GetCaption("F_REVERSE_NAME"), (object) dbRelationType.ReverseName));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_SAVE_HISTORY", DataSetProcessor.GetCaption("F_SAVE_HISTORY"), (object) dbRelationType.SaveHistory));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_SHORT_NAME", DataSetProcessor.GetCaption("F_SHORT_NAME"), (object) dbRelationType.ShortName));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_TYPE_NAME", DataSetProcessor.GetCaption("F_TYPE_NAME"), (object) dbRelationType.TypeName));
    foreach (RelationTypeOptions relationTypeOptions in Enum.GetValues(typeof (RelationTypeOptions)))
    {
      if (relationTypeOptions != RelationTypeOptions.None)
        properties.Add((ScriptNode) new ObjectProperty4Script((object) $"{"F_OPTIONS"}{(int) relationTypeOptions}", EnumDescConverter.GetEnumDescription((Enum) relationTypeOptions), (object) (int) (dbRelationType.Options & relationTypeOptions)));
    }
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_AREA_ID", DataSetProcessor.GetCaption("F_AREA_ID"), (object) (dbRelationType as IDBSubjectArea).SubjectAreas));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_ACCESS", UpdateScriptHelper.AccessNodeText, (object) null));
    DataRow[] extensions = this.GetExtensions(session, dbRelationType.RelationType);
    if (extensions != null && extensions.Length != 0)
      properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_EXTENSIONS", "Расширенные метаданные", (object) extensions));
    IDBAttribute4TypeCollection attributes = dbRelationType.Attributes;
    DataTable dataTable = attributes.Select("F_ATTRIBUTE_ID");
    if (dataTable != null && dataTable.Rows.Count > 0)
    {
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        IDBAttributeType4 attributeById = attributes.GetAttributeByID(Convert.ToInt32(row["F_ATTRIBUTE_ID"]));
        if (attributeById != null)
        {
          Object4Script object4Script = new Object4Script(3, (object) (attributeById as IDBGuid).GUID, attributeById.Name);
          object4Script.Tag = (object) (int) attributeById.AttributeType;
          object4Script.Properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_REQUIRED", DataSetProcessor.GetCaption("F_REQUIRED"), (object) Convert.ToInt32(row["F_REQUIRED"])));
          object4Script.Properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_VALIDATION_RULE", DataSetProcessor.GetCaption("F_VALIDATION_RULE"), (object) Convert.ToString(row["F_VALIDATION_RULE"])));
          object4Script.Properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_COMPUTED", DataSetProcessor.GetCaption("F_COMPUTED"), (object) Convert.ToInt32(row["F_COMPUTED"])));
          object4Script.Properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_FORMULA", DataSetProcessor.GetCaption("F_FORMULA"), (object) Convert.ToString(row["F_FORMULA"])));
          object4Script.Properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_DEFAULT_VALUE", DataSetProcessor.GetCaption("F_DEFAULT_VALUE"), (object) Convert.ToString(row["F_DEFAULT_VALUE"])));
          object4Script.Properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_INVIEW", DataSetProcessor.GetCaption("F_INVIEW"), (object) Convert.ToInt32(row["F_INVIEW"])));
          object4Script.Properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_CONTENT", DataSetProcessor.GetCaption("F_CONTENT"), (object) (Convert.ToInt32(row["F_CONTENT"]) == 1)));
          object4Script.Properties.AddRange((IEnumerable<ScriptNode>) this.GetProperties4AttrOptions((AttributeOptions) Convert.ToInt32(row["F_OPTIONS"])));
          object4Script.Properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_MASK", DataSetProcessor.GetCaption("F_MASK"), (object) Convert.ToString(row["F_MASK"])));
          object4Script.Properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_MASTER_ID", DataSetProcessor.GetCaption("F_MASTER_ID"), (object) Convert.ToInt32(row["F_MASTER_ID"])));
          object4Script.Properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_SOURCE_ID", DataSetProcessor.GetCaption("F_SOURCE_ID"), (object) Convert.ToInt32(row["F_SOURCE_ID"])));
          properties.Add((ScriptNode) object4Script);
        }
      }
    }
    return properties;
  }
}
