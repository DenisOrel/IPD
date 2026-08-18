// Decompiled with JetBrains decompiler
// Type: Intermech.Update.CodeFormers.AttributeCodeFormer
// Assembly: Intermech.Update, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 825FBF29-0112-4B23-8140-950E091D8F10
// Assembly location: D:\IPS\Client\Intermech.Update.dll

using Intermech.Interfaces;
using Intermech.Interfaces.MetadataUpdates;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;

#nullable disable
namespace Intermech.Update.CodeFormers;

internal class AttributeCodeFormer : ExtensionCodeFormer
{
  public AttributeCodeFormer()
    : base(3, "F_ATTRIBUTE_ID")
  {
  }

  public override XmlNode GenerateNode(
    IUserSession session,
    XmlDocument xmlDocument,
    Object4Script obj,
    string path4Files)
  {
    IDBAttributeType attributeType = session.GetAttributeType((Guid) obj.ID);
    XmlNode node = this.CreateNode(xmlDocument, obj, (object) (int) attributeType.AttributeType);
    if (node == null)
      return (XmlNode) null;
    foreach (ObjectProperty4Script property in obj.Properties)
    {
      string id = Convert.ToString(property.PropertyID);
      object obj1 = (object) null;
      bool flag = true;
      switch (id)
      {
        case "F_ACCESS":
          obj1 = (object) this.GetSecurity(session, attributeType as IDBSecurity);
          break;
        case "F_AREA_ID":
          obj1 = (object) this.GetSubjectAreaProperty(session, attributeType.PropertiesStructure.AreaID);
          break;
        case "F_DEFAULT_VALUE":
          obj1 = this.GetDefaultValueProperty(session, attributeType.DefaultValue, attributeType.AttributeType);
          break;
        case "F_EXTENSIONS":
          XmlNode newNode = this.CreateNewNode(xmlDocument, property.Obligatory, id);
          DataRow[] dataRowArray = (DataRow[]) property.Value;
          for (int index = 0; index < dataRowArray.Length; ++index)
            newNode.AppendChild(this.GetExtensionNode(session, xmlDocument, Convert.ToString(dataRowArray[index]["F_PARAM_NAME"]), Convert.ToInt32(dataRowArray[index]["F_INLIST_ID"]), Convert.ToInt32(dataRowArray[index]["F_CATEGORY_TYPE"]), Convert.ToString(dataRowArray[index]["F_VALUE"])));
          node.AppendChild(newNode);
          flag = false;
          break;
        case "F_FORMULA":
          obj1 = (object) this.GetFormulaProperty(session, attributeType.Formula);
          break;
        case "F_GROUP_ID":
          int[] groupsList = attributeType.GetGroupsList();
          obj1 = (object) string.Empty;
          if (groupsList != null && groupsList.Length != 0)
          {
            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index < groupsList.Length; ++index)
            {
              if (index > 0)
                stringBuilder.Append("|");
              IDBAttributesGroup attributesGroup = session.GetAttributesGroup(groupsList[index]);
              stringBuilder.Append((attributesGroup as IDBGuid).GUID.ToString());
            }
            obj1 = (object) stringBuilder.ToString();
            break;
          }
          break;
        case "F_LANGUAGE_ID":
          obj1 = this.GetLanguageProperty(session, attributeType.PropertiesStructure.LanguageID);
          break;
        case "F_LEVEL_ID":
          obj1 = this.GetLevelProperty(session, attributeType.LevelID);
          break;
        case "F_MASTER_ID":
          obj1 = this.GetAttributeProperty(session, attributeType.MasterAttributeID);
          break;
        case "F_POSSIBLE_VALUES":
          if (property.Value != null && property.Value is DataTable graph && graph.Rows.Count > 0)
          {
            string path2 = $"pv{(attributeType as IDBGuid).GUID.ToString().ToLower()}.tbl";
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            graph.RemotingFormat = SerializationFormat.Binary;
            using (FileStream serializationStream = new FileStream(Path.Combine(path4Files, path2), FileMode.Create, FileAccess.Write))
            {
              binaryFormatter.Serialize((Stream) serializationStream, (object) graph);
              serializationStream.Flush();
              serializationStream.Close();
            }
            this.temporaries.Enqueue(path2);
            obj1 = (object) path2;
            break;
          }
          break;
        case "F_SIZE_TYPE":
          obj1 = (object) 0;
          if (attributeType.SizeType > 0L || attributeType.SizeType == -1L)
          {
            if (attributeType.AttributeType == FieldTypes.ftMeasured)
            {
              if (attributeType.SizeType != -1L)
              {
                IDBObject dbObject = session.GetObject(attributeType.SizeType, false);
                if (dbObject != null)
                {
                  obj1 = (object) dbObject.ObjectGUID;
                  break;
                }
                break;
              }
              obj1 = (object) -1;
              break;
            }
            if (attributeType.AttributeType == FieldTypes.ftObjectLink)
            {
              IDBObjectType objectType = session.GetObjectType(Convert.ToInt32(attributeType.SizeType), false);
              if (objectType != null)
              {
                obj1 = (object) (objectType as IDBGuid).GUID;
                break;
              }
              break;
            }
            obj1 = (object) attributeType.SizeType;
            break;
          }
          break;
        case "F_SOURCE_ID":
          obj1 = this.GetAttributeProperty(session, attributeType.SourceAttributeID);
          break;
        default:
          obj1 = property.Value;
          break;
      }
      if (flag)
        node.AppendChild(this.CreateProperty(xmlDocument, property.Obligatory, id, obj1));
    }
    return node;
  }

  public override List<ScriptNode> GetProperties(IUserSession session, object dbObject)
  {
    List<ScriptNode> properties = new List<ScriptNode>();
    IDBAttributeType dbAttributeType = dbObject as IDBAttributeType;
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_GROUP_ID", "Группы атрибутов", (object) dbAttributeType.GetGroupsList()));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_SOURCE_ID", DataSetProcessor.GetCaption("F_SOURCE_ID"), (object) dbAttributeType.SourceAttributeID));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_CONTENT", DataSetProcessor.GetCaption("F_CONTENT"), (object) dbAttributeType.IsContent));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_COMPUTED", DataSetProcessor.GetCaption("F_COMPUTED"), (object) (int) dbAttributeType.Computed));
    if (dbAttributeType.MultipleValued == MultiValueModes.MultiValuesFromList || dbAttributeType.MultipleValued == MultiValueModes.SingleValueFromList)
      properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_POSSIBLE_VALUES", "Допустимые значения", (object) dbAttributeType.GetPossibleValues()));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_DEFAULT_VALUE", DataSetProcessor.GetCaption("F_DEFAULT_VALUE"), dbAttributeType.DefaultValue));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_NOTE", DataSetProcessor.GetCaption("F_NOTE"), (object) dbAttributeType.Note));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_SHORT_NAME", DataSetProcessor.GetCaption("F_SHORT_NAME"), (object) dbAttributeType.ShortName));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_MASK", DataSetProcessor.GetCaption("F_MASK"), (object) dbAttributeType.Mask));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_MASTER_ID", DataSetProcessor.GetCaption("F_MASTER_ID"), (object) dbAttributeType.MasterAttributeID));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_NAME", DataSetProcessor.GetCaption("F_NAME"), (object) dbAttributeType.Name));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_INVIEW", DataSetProcessor.GetCaption("F_INVIEW"), (object) (int) dbAttributeType.OptimizationMode));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_AREA_ID", DataSetProcessor.GetCaption("F_AREA_ID"), (object) dbAttributeType.PropertiesStructure.AreaID));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_ALIAS", DataSetProcessor.GetCaption("F_ALIAS"), (object) dbAttributeType.Alias));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_SIZE_TYPE", DataSetProcessor.GetCaption("F_SIZE_TYPE"), (object) dbAttributeType.SizeType));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_MULTIPLE_VALUED", DataSetProcessor.GetCaption("F_MULTIPLE_VALUED"), (object) (int) dbAttributeType.MultipleValued));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_ATTRIBUTE_TYPE", DataSetProcessor.GetCaption("F_ATTRIBUTE_TYPE"), (object) (int) dbAttributeType.AttributeType));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_UNIQUE", DataSetProcessor.GetCaption("F_UNIQUE"), (object) (int) dbAttributeType.UniqueMode));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_LEVEL_ID", DataSetProcessor.GetCaption("F_LEVEL_ID"), (object) dbAttributeType.LevelID));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_FORMULA", DataSetProcessor.GetCaption("F_FORMULA"), (object) dbAttributeType.Formula));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_LANGUAGE_ID", DataSetProcessor.GetCaption("F_LANGUAGE_ID"), (object) dbAttributeType.PropertiesStructure.LanguageID));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_ACCESS", UpdateScriptHelper.AccessNodeText, (object) null));
    DataRow[] extensions = this.GetExtensions(session, dbAttributeType.AttributeID);
    if (extensions != null && extensions.Length != 0)
      properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_EXTENSIONS", "Расширенные метаданные", (object) extensions));
    properties.AddRange((IEnumerable<ScriptNode>) this.GetProperties4AttrOptions(dbAttributeType.Options));
    return properties;
  }
}
