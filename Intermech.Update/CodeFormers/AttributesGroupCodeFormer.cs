// Decompiled with JetBrains decompiler
// Type: Intermech.Update.CodeFormers.AttributesGroupCodeFormer
// Assembly: Intermech.Update, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 825FBF29-0112-4B23-8140-950E091D8F10
// Assembly location: D:\IPS\Client\Intermech.Update.dll

using Intermech.Interfaces;
using Intermech.Interfaces.MetadataUpdates;
using System;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.Update.CodeFormers;

internal class AttributesGroupCodeFormer : CodeFormer
{
  public AttributesGroupCodeFormer()
    : base(12)
  {
  }

  public override XmlNode GenerateNode(
    IUserSession session,
    XmlDocument xmlDocument,
    Object4Script obj,
    string path4Files)
  {
    XmlNode node = this.CreateNode(xmlDocument, obj);
    if (node == null)
      return (XmlNode) null;
    foreach (ObjectProperty4Script property in obj.Properties)
    {
      string id = Convert.ToString(property.PropertyID);
      object obj1;
      switch (id)
      {
        case "F_LANGUAGE_ID":
          obj1 = (object) string.Empty;
          string aLanguageID = Convert.ToString(property.Value);
          if (aLanguageID != string.Empty)
          {
            IDBLanguageType language = session.GetLanguage(aLanguageID, false);
            if (language != null)
            {
              obj1 = (object) language.GUID;
              break;
            }
            break;
          }
          break;
        case "F_AREA_ID":
          obj1 = (object) Convert.ToString(property.Value).Trim();
          break;
        case "F_ACCESS":
          IDBAttributesGroup attributesGroup = session.GetAttributesGroup((Guid) obj.ID);
          obj1 = (object) this.GetSecurity(session, attributesGroup as IDBSecurity);
          break;
        default:
          obj1 = property.Value;
          break;
      }
      node.AppendChild(this.CreateProperty(xmlDocument, property.Obligatory, id, obj1));
    }
    return node;
  }

  public override List<ScriptNode> GetProperties(IUserSession session, object dbObject)
  {
    List<ScriptNode> properties = new List<ScriptNode>();
    IDBAttributesGroup dbAttributesGroup = dbObject as IDBAttributesGroup;
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_GROUP_NAME", DataSetProcessor.GetCaption("F_GROUP_NAME"), (object) dbAttributesGroup.GroupName));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_NOTE", DataSetProcessor.GetCaption("F_NOTE"), (object) dbAttributesGroup.Note));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_LANGUAGE_ID", DataSetProcessor.GetCaption("F_LANGUAGE_ID"), (object) (dbAttributesGroup as IDBLanguage).LanguageID));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_AREA_ID", DataSetProcessor.GetCaption("F_AREA_ID"), (object) (dbAttributesGroup as IDBSubjectArea).SubjectAreas));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_ACCESS", UpdateScriptHelper.AccessNodeText, (object) null));
    return properties;
  }
}
