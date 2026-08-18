// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.ExtensionNode
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal sealed class ExtensionNode(IUserSession session, XmlNode node) : 
  XMLPropertyNode<Dictionary<string, List<MetadataExtension>>>(session, node, "F_EXTENSIONS")
{
  protected override void ReadValue(IUserSession session, XmlNode node)
  {
    Dictionary<string, List<MetadataExtension>> dictionary = new Dictionary<string, List<MetadataExtension>>(node.ChildNodes.Count);
    foreach (XmlNode childNode in node.ChildNodes)
    {
      if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "Extension")
      {
        string str = childNode.Attributes["ParamName"].Value;
        List<MetadataExtension> metadataExtensionList;
        if (!dictionary.TryGetValue(str, out metadataExtensionList))
        {
          metadataExtensionList = new List<MetadataExtension>();
          dictionary.Add(str, metadataExtensionList);
        }
        int int32 = Convert.ToInt32(childNode.Attributes["CategoryType"].Value);
        string languageId = childNode.Attributes["Value"].Value;
        int num;
        switch (int32)
        {
          case 1:
            if (GuidHelper.IsGuid(languageId))
            {
              languageId = session.GetObjectInfo(new Guid(languageId)).ObjectID.ToString();
              break;
            }
            break;
          case 3:
            if (GuidHelper.IsGuid(languageId))
            {
              num = MetaDataHelper.GetAttributeTypeID(new Guid(languageId));
              languageId = num.ToString();
              break;
            }
            break;
          case 4:
            if (GuidHelper.IsGuid(languageId))
            {
              num = MetaDataHelper.GetObjectTypeID(new Guid(languageId));
              languageId = num.ToString();
              break;
            }
            break;
          case 6:
            if (GuidHelper.IsGuid(languageId))
            {
              num = MetaDataHelper.GetRelationTypeID(new Guid(languageId));
              languageId = num.ToString();
              break;
            }
            break;
          case 7:
            if (GuidHelper.IsGuid(languageId))
            {
              num = MetaDataHelper.GetLCStepID(new Guid(languageId));
              languageId = num.ToString();
              break;
            }
            break;
          case 8:
            if (GuidHelper.IsGuid(languageId))
            {
              num = MetaDataHelper.GetLCLevelID(new Guid(languageId));
              languageId = num.ToString();
              break;
            }
            break;
          case 9:
            if (GuidHelper.IsGuid(languageId))
            {
              languageId = session.GetLanguage(new Guid(languageId)).LanguageID;
              break;
            }
            break;
          case 11:
            if (GuidHelper.IsGuid(languageId))
            {
              languageId = session.GetSubjectAreaType(new Guid(languageId)).AreaID.ToString();
              break;
            }
            break;
          case 16 /*0x10*/:
            if (GuidHelper.IsGuid(languageId))
            {
              num = MetaDataHelper.GetLCSchemaID(new Guid(languageId));
              languageId = num.ToString();
              break;
            }
            break;
        }
        metadataExtensionList.Add(new MetadataExtension(str, Convert.ToInt32(childNode.Attributes["CategoryType"].Value), Convert.ToInt32(childNode.Attributes["InListID"].Value), languageId));
      }
    }
    this.Value = (object) dictionary;
  }
}
