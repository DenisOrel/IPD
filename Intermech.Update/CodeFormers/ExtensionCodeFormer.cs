// Decompiled with JetBrains decompiler
// Type: Intermech.Update.CodeFormers.ExtensionCodeFormer
// Assembly: Intermech.Update, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 825FBF29-0112-4B23-8140-950E091D8F10
// Assembly location: D:\IPS\Client\Intermech.Update.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Data;
using System.Xml;

#nullable disable
namespace Intermech.Update.CodeFormers;

internal class ExtensionCodeFormer : CodeFormer
{
  protected string IdField;

  public ExtensionCodeFormer(int categoryID, string idField)
    : base(categoryID)
  {
    this.IdField = idField;
  }

  protected DataRow[] GetExtensions(IUserSession session, int id)
  {
    return (session as IClientSession).ClientCache.GetTable("IMS_MD_EXTENSIONS")?.Select(string.Format($"{this.IdField}={id}"));
  }

  protected XmlNode GetExtensionNode(
    IUserSession session,
    XmlDocument xmlDocument,
    string paramName,
    int inListID,
    int categoryType,
    string value)
  {
    XmlNode element = (XmlNode) xmlDocument.CreateElement("Extension");
    XmlAttribute attribute1 = xmlDocument.CreateAttribute("ParamName");
    attribute1.Value = paramName;
    element.Attributes.Append(attribute1);
    XmlAttribute attribute2 = xmlDocument.CreateAttribute("InListID");
    attribute2.Value = inListID.ToString();
    element.Attributes.Append(attribute2);
    XmlAttribute attribute3 = xmlDocument.CreateAttribute("CategoryType");
    attribute3.Value = categoryType.ToString();
    element.Attributes.Append(attribute3);
    switch (categoryType)
    {
      case 1:
        value = session.GetObject(Convert.ToInt64(value)).ObjectGUID.ToString();
        break;
      case 3:
        value = MetaDataHelper.GetAttributeTypeGuid(Convert.ToInt32(value)).ToString();
        break;
      case 4:
        value = MetaDataHelper.GetObjectTypeGuid(Convert.ToInt32(value)).ToString();
        break;
      case 6:
        value = MetaDataHelper.GetRelationTypeGuid(Convert.ToInt32(value)).ToString();
        break;
      case 7:
        value = MetaDataHelper.GetLCStepGuid(Convert.ToInt32(value)).ToString();
        break;
      case 8:
        value = MetaDataHelper.GetLCLevelGuid(Convert.ToInt32(value)).ToString();
        break;
      case 9:
        value = (session.GetLanguage(value) as IDBGuid).GUID.ToString();
        break;
      case 11:
        if (value.Length > 0)
        {
          value = (session.GetSubjectAreaType(value[0]) as IDBGuid).GUID.ToString();
          break;
        }
        break;
      case 16 /*0x10*/:
        value = MetaDataHelper.GetLCSchemaGuid(Convert.ToInt32(value)).ToString();
        break;
    }
    XmlAttribute attribute4 = xmlDocument.CreateAttribute("Value");
    attribute4.Value = value;
    element.Attributes.Append(attribute4);
    return element;
  }
}
