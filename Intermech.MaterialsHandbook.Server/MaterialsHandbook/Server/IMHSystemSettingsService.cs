// Decompiled with JetBrains decompiler
// Type: Intermech.MaterialsHandbook.Server.IMHSystemSettingsService
// Assembly: Intermech.MaterialsHandbook.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 415584AC-BDF0-4945-B0B3-EBEC9DE4A5E1
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.MaterialsHandbook.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.MaterialsHandbook;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml;

#nullable disable
namespace Intermech.MaterialsHandbook.Server;

internal class IMHSystemSettingsService : LongLifeObject, IIMHSystemSettingsService
{
  private const string ROOT_NODE_NAME = "IMHSystemSettings";
  private const string BINDING = "Binding";
  private const string CATALOGS = "Catalogs";
  private const string CATALOG = "Catalog";
  private const string FOLDERS = "Folders";
  private const string FOLDER = "Folder";
  private const string TABLES = "Tables";
  private const string TABLE = "Table";
  private const string COLUMNS = "Columns";
  private const string COLUMN = "Column";
  private const string ROWS = "Rows";
  private const string ROW = "Row";
  private const string NAME = "Name";
  private const string VALUE = "Value";
  private const string FORMULA = "Formula";
  private const string SEARCH = "Search";
  private const string ASSORTMENT_SETTINGS = "AssortmentSettings";
  private const string CLASS = "Class";
  private const string ATTRIBUTES = "Attributes";
  private const string ATTRIBUTE = "Attribute";
  private const string ABSTRACT_PARAM = "AbstractParam";
  private const string DISPLAY_SETTINGS = "DisplaySettings";
  private const string DISPLAY_SETTING = "DisplaySetting";
  private Dictionary<string, string> _dictSettings;
  private IMHCoatingsSystemSettings _coatingsSettings;
  private List<IMHAssortmentClass> _assortmentSearchSettings;

  public IMHSystemSettingsService(IUserSession session) => this.LoadSettings(session);

  public Guid GetObjectGuidByName(string name)
  {
    Guid objectGuidByName = Guid.Empty;
    if (this._dictSettings != null && this._dictSettings.ContainsKey(name))
    {
      string dictSetting = this._dictSettings[name];
      if (GuidHelper.IsGuid(dictSetting))
        objectGuidByName = new Guid(dictSetting);
    }
    return objectGuidByName;
  }

  public Dictionary<string, Guid> GetObjectGuidsByNames(List<string> names)
  {
    Dictionary<string, Guid> retValue = (Dictionary<string, Guid>) null;
    if (this._dictSettings != null && this._dictSettings.Count > 0 && names != null && names.Count > 0)
    {
      retValue = new Dictionary<string, Guid>(names.Count);
      names.ForEach((Action<string>) (x => retValue.Add(x, !this._dictSettings.ContainsKey(x) || !GuidHelper.IsGuid(this._dictSettings[x]) ? Guid.Empty : new Guid(this._dictSettings[x]))));
    }
    return retValue;
  }

  public object GetValueByName(string name)
  {
    return this._dictSettings == null || !this._dictSettings.ContainsKey(name) ? (object) null : (object) this._dictSettings[name];
  }

  public IMHSystemSettings GetSystemSettings()
  {
    if (this._dictSettings == null)
      this.CreateVoidSettings();
    return new IMHSystemSettings(this._dictSettings, this._coatingsSettings, this._assortmentSearchSettings);
  }

  public void SaveSistemSettings(IMHSystemSettings settings)
  {
    string str = this.BuildSettings(settings);
    if (!(ServerServices.GetService(typeof (IDBTimedEvents)) is IDBTimedEvents service1))
      return;
    IUserSession userSession = (IUserSession) null;
    try
    {
      userSession = service1.GetSystemSessionTemporaryClone("IMH.SaveSettings");
      if (userSession == null)
        return;
      IDBConfigurations configurations = userSession.Configurations;
      if (configurations == null)
        return;
      IPackedStream service2 = ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true);
      using (MemoryStream memoryStream = new MemoryStream(str.Length))
      {
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) memoryStream))
        {
          binaryWriter.Write(str);
          binaryWriter.Flush();
          memoryStream.Position = 0L;
          using (MemoryStream outStream = new MemoryStream((int) memoryStream.Length / 2))
          {
            service2.PackStream((Stream) outStream, (Stream) memoryStream, 9);
            BlobInformation config_info = new BlobInformation(outStream.Length, outStream.Length, DateTime.Now, "IMH.SystemSettings", ArcMethods.ZLibPacked, string.Empty);
            configurations.WriteConfigData(config_info, outStream.ToArray(), 0L);
          }
        }
      }
    }
    finally
    {
      userSession?.Logout("IMH.SaveSettings");
    }
  }

  private void AppendChildNode(XmlNode rootNode, XmlNode childNode)
  {
    if (childNode.ChildNodes.Count <= 0)
      return;
    rootNode.AppendChild(childNode);
  }

  private string BuildSettings(IMHSystemSettings settings)
  {
    this._dictSettings = settings.Dict;
    this._coatingsSettings = settings.CoatingsSettings;
    this._assortmentSearchSettings = settings.AssortmentSearchSettings;
    string empty = string.Empty;
    XmlDocument xmlDocument = new XmlDocument();
    XmlNode element1 = (XmlNode) xmlDocument.CreateElement("IMHSystemSettings");
    XmlNode element2 = (XmlNode) xmlDocument.CreateElement("Binding");
    XmlNode element3 = (XmlNode) xmlDocument.CreateElement("Catalogs");
    XmlNode element4 = (XmlNode) xmlDocument.CreateElement("Folders");
    XmlNode element5 = (XmlNode) xmlDocument.CreateElement("Tables");
    XmlNode element6 = (XmlNode) xmlDocument.CreateElement("Columns");
    XmlNode element7 = (XmlNode) xmlDocument.CreateElement("Attributes");
    XmlNode element8 = (XmlNode) xmlDocument.CreateElement("DisplaySettings");
    XmlNode element9 = (XmlNode) xmlDocument.CreateElement("Search");
    if (this._dictSettings != null)
    {
      foreach (KeyValuePair<string, string> dictSetting in this._dictSettings)
      {
        if (!string.IsNullOrEmpty(dictSetting.Value))
        {
          if (dictSetting.Key.Contains("CTL"))
          {
            XmlElement element10 = xmlDocument.CreateElement("Catalog");
            element10.SetAttribute("Name", dictSetting.Key);
            element10.SetAttribute("Value", dictSetting.Value);
            element3.AppendChild((XmlNode) element10);
          }
          else if (dictSetting.Key.Contains("FOLDER"))
          {
            XmlElement element11 = xmlDocument.CreateElement("Folder");
            element11.SetAttribute("Name", dictSetting.Key);
            element11.SetAttribute("Value", dictSetting.Value);
            element4.AppendChild((XmlNode) element11);
          }
          else if (dictSetting.Key.Contains("TABLE"))
          {
            XmlElement element12 = xmlDocument.CreateElement("Table");
            element12.SetAttribute("Name", dictSetting.Key);
            element12.SetAttribute("Value", dictSetting.Value);
            element5.AppendChild((XmlNode) element12);
            if (dictSetting.Key == "COATING_PROPERTIES_TABLE_NAME" && this._coatingsSettings != null)
            {
              XmlElement element13 = xmlDocument.CreateElement("Formula");
              element13.SetAttribute("Value", this._coatingsSettings.Formula);
              element12.AppendChild((XmlNode) element13);
              XmlElement element14 = xmlDocument.CreateElement("Table");
              DataTable dataTable = this._coatingsSettings.Params;
              if (dataTable != null && dataTable.Rows.Count > 0)
              {
                foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
                {
                  XmlElement element15 = xmlDocument.CreateElement("Row");
                  object[] objArray = new object[2]
                  {
                    row["P1"],
                    row["P2"]
                  };
                  int num = 1;
                  foreach (object obj in objArray)
                  {
                    if (obj != null && obj != DBNull.Value)
                      element15.SetAttribute($"P{num++}", obj.ToString());
                  }
                  if (element15.Attributes.Count != 0)
                    element14.AppendChild((XmlNode) element15);
                }
                if (element14.ChildNodes.Count != 0)
                  element12.AppendChild((XmlNode) element14);
              }
            }
          }
          else if (dictSetting.Key.Contains("COLUMN"))
          {
            XmlElement element16 = xmlDocument.CreateElement("Column");
            element16.SetAttribute("Name", dictSetting.Key);
            element16.SetAttribute("Value", dictSetting.Value);
            element6.AppendChild((XmlNode) element16);
          }
          else if (dictSetting.Key.Contains("ATTR"))
          {
            XmlElement element17 = xmlDocument.CreateElement("Attribute");
            element17.SetAttribute("Name", dictSetting.Key);
            element17.SetAttribute("Value", dictSetting.Value);
            element7.AppendChild((XmlNode) element17);
          }
          else
          {
            XmlElement element18 = xmlDocument.CreateElement("DisplaySetting");
            element18.SetAttribute("Name", dictSetting.Key);
            element18.SetAttribute("Value", dictSetting.Value);
            element8.AppendChild((XmlNode) element18);
          }
        }
      }
    }
    if (this._assortmentSearchSettings != null && this._assortmentSearchSettings.Count > 0)
    {
      XmlNode element19 = (XmlNode) xmlDocument.CreateElement("AssortmentSettings");
      foreach (IMHAssortmentClass assortmentSearchSetting in this._assortmentSearchSettings)
      {
        XmlElement element20 = xmlDocument.CreateElement("Class");
        element20.SetAttribute("Name", assortmentSearchSetting.Name);
        foreach (KeyValuePair<string, List<string>> parameter in assortmentSearchSetting.Parameters)
        {
          XmlElement element21 = xmlDocument.CreateElement("AbstractParam");
          element21.SetAttribute("Name", parameter.Key);
          foreach (string str in parameter.Value)
          {
            XmlElement element22 = xmlDocument.CreateElement("Attribute");
            element22.InnerText = str;
            element21.AppendChild((XmlNode) element22);
          }
          if (element21.ChildNodes.Count != 0)
            element20.AppendChild((XmlNode) element21);
        }
        if (element20.ChildNodes.Count != 0)
          element19.AppendChild((XmlNode) element20);
      }
      element9.AppendChild(element19);
    }
    this.AppendChildNode(element2, element3);
    this.AppendChildNode(element2, element4);
    this.AppendChildNode(element2, element5);
    this.AppendChildNode(element2, element6);
    this.AppendChildNode(element2, element7);
    this.AppendChildNode(element2, element8);
    this.AppendChildNode(element2, element9);
    this.AppendChildNode(element1, element2);
    return element1.OuterXml;
  }

  private void CreateVoidSettings()
  {
    this._dictSettings = new Dictionary<string, string>()
    {
      {
        "BASE_MATERIALS_CTL",
        string.Empty
      },
      {
        "ADDITION_MATERIALS_CTL",
        string.Empty
      },
      {
        "ASSORTMENT_FOLDER_NAME",
        string.Empty
      },
      {
        "GLUE_FOLDER_NAME",
        string.Empty
      },
      {
        "COATING_FOLDER_NAME",
        string.Empty
      },
      {
        "OIL_FOLDER_NAME",
        string.Empty
      },
      {
        "VARNISH_FOLDER_NAME",
        string.Empty
      },
      {
        "MATERIAL_SUBSTITUTES_TABLE_NAME",
        Intermech.MaterialsHandbook.Consts.SubstitutesForMaterialsTableGuid.ToString()
      },
      {
        "MATERIAL_SUBSTITUTES_COLUMN_MATERIAL",
        Intermech.MaterialsHandbook.Consts.SubstitutesForMaterialsMaterialFieldGuid.ToString()
      },
      {
        "MATERIAL_SUBSTITUTES_COLUMN_SUBSTITUTES",
        Intermech.MaterialsHandbook.Consts.SubstitutesForMaterialsSubstitutesFieldGuid.ToString()
      },
      {
        "MATERIAL_GROUPS_TABLE_NAME",
        Intermech.MaterialsHandbook.Consts.MaterialGroupsTableGuid.ToString()
      },
      {
        "MATERIAL_GROUPS_COLUMN_NAME",
        Intermech.MaterialsHandbook.Consts.MaterialGroupsMaterialofDetailFieldGuid.ToString()
      },
      {
        "MATERIAL_PROPERTIES_TABLE_NAME",
        Intermech.MaterialsHandbook.Consts.MaterialPropsTableGuid.ToString()
      },
      {
        "MATERIAL_PROPERTIES_COLUMN_MATERIAL",
        Intermech.MaterialsHandbook.Consts.MaterialPropsMaterialFieldGuid.ToString()
      },
      {
        "MATERIAL_PROPERTIES_COLUMN_OBJECT",
        Intermech.MaterialsHandbook.Consts.MaterialPropsObjectFieldGuid.ToString()
      },
      {
        "COATING_PROPERTIES_TABLE_NAME",
        Intermech.MaterialsHandbook.Consts.CoatingPropsTableGuid.ToString()
      },
      {
        "COATING_PROPERTIES_COLUMN_COATING",
        Intermech.MaterialsHandbook.Consts.CoatingPropsCoatingFieldGuid.ToString()
      },
      {
        "COATING_PROPERTIES_COLUMN_MATERIAL",
        Intermech.MaterialsHandbook.Consts.CoatingPropsMaterialFieldGuid.ToString()
      },
      {
        "COATING_PROPERTIES_COLUMN_PURPOSE",
        Intermech.MaterialsHandbook.Consts.CoatingPropsDestinationFieldGuid.ToString()
      },
      {
        "COATING_PROPERTIES_COLUMN_INSTRUCTIONS",
        Intermech.MaterialsHandbook.Consts.CoatingPropsAddInstructionsFieldGuid.ToString()
      },
      {
        "GLUE_MATERIAL_GROUPS_TABLE_NAME",
        Intermech.MaterialsHandbook.Consts.MaterialGroupsForGluesTableGuid.ToString()
      },
      {
        "GLUE_MATERIAL_GROUPS_COLUMN_NAME",
        Intermech.MaterialsHandbook.Consts.MaterialGroupsForGluesMaterialNameFieldGuid.ToString()
      },
      {
        "GLUE_TABLE_NAME",
        Intermech.MaterialsHandbook.Consts.GluesTableGuid.ToString()
      },
      {
        "GLUE_COLUMN_MATERIAL1",
        Intermech.MaterialsHandbook.Consts.GluesMaterial1FieldGuid.ToString()
      },
      {
        "GLUE_COLUMN_MATERIAL2",
        Intermech.MaterialsHandbook.Consts.GluesMaterial2FieldGuid.ToString()
      },
      {
        "GLUE_COLUMN_GLUE",
        Intermech.MaterialsHandbook.Consts.GluesGlueFieldGuid.ToString()
      },
      {
        "SURFACE_MATERIALS_TABLE_NAME",
        Intermech.MaterialsHandbook.Consts.MaterialsOfSurfaceTableGuid.ToString()
      },
      {
        "SURFACE_MATERIALS_COLUMN_NAME",
        Intermech.MaterialsHandbook.Consts.MaterialsOfSurfaceFieldGuid.ToString()
      },
      {
        "COATING_MATERIALS_TABLE_NAME",
        Intermech.MaterialsHandbook.Consts.CoatingMaterialsTableGuid.ToString()
      },
      {
        "COATING_MATERIALS_COLUMN_COATING",
        Intermech.MaterialsHandbook.Consts.CoatingMaterialsMaterialFieldGuid.ToString()
      },
      {
        "COATING_MATERIALS_COLUMN_MATERIALS",
        Intermech.MaterialsHandbook.Consts.CoatingMaterialsCoatingFieldGuid.ToString()
      },
      {
        "TERMS_USE_TABLE_NAME",
        Intermech.MaterialsHandbook.Consts.TermsOfUseTableGuid.ToString()
      },
      {
        "TERMS_USE_COLUMN_NAME",
        Intermech.MaterialsHandbook.Consts.TermsOfUseFieldGuid.ToString()
      },
      {
        "COATING_TERMS_USE_TABLE_NAME",
        Intermech.MaterialsHandbook.Consts.CoatingTermsOfUseTableGuid.ToString()
      },
      {
        "COATING_TERMS_USE_COLUMN_COATING",
        Intermech.MaterialsHandbook.Consts.CoatingTermsOfUseCoatingFieldGuid.ToString()
      },
      {
        "COATING_TERMS_USE_COLUMN_TERMS",
        Intermech.MaterialsHandbook.Consts.CoatingTermsOfUseTermsOfUseFieldGuid.ToString()
      },
      {
        "COATING_SPHERE_USE_TABLE_NAME",
        Intermech.MaterialsHandbook.Consts.CoatingSphereTableGuid.ToString()
      },
      {
        "COATING_SPHERE_USE_COLUMN_COATING",
        Intermech.MaterialsHandbook.Consts.CoatingSphereCoatingFieldGuid.ToString()
      },
      {
        "COATING_SPHERE_USE_COLUMN_SPHERE",
        Intermech.MaterialsHandbook.Consts.CoatingSphereSphereFieldGuid.ToString()
      },
      {
        "COATING_INTERNAL_EXTERNAL_TABLE_NAME",
        Intermech.MaterialsHandbook.Consts.InternalExternalCoatingTableGuid.ToString()
      },
      {
        "COATING_INTERNAL_EXTERNAL_INTERNAL_COLUMN",
        Intermech.MaterialsHandbook.Consts.InternalExternalCoatingInternalFieldGuid.ToString()
      },
      {
        "COATING_INTERNAL_EXTERNAL_EXTERNAL_WITH_CONDITION_COLUMN",
        Intermech.MaterialsHandbook.Consts.InternalExternalCoatingExternalWithTermsOfUseFieldGuid.ToString()
      },
      {
        "COATING_PREFERRED_DESTINATION_TABLE_NAME",
        Intermech.MaterialsHandbook.Consts.CoatingDestinationTableGuid.ToString()
      },
      {
        "COATING_PREFERRED_DESTINATION_COLUMN_COATING",
        Intermech.MaterialsHandbook.Consts.CoatingDestinationCoatingFieldGuid.ToString()
      },
      {
        "COATING_PREFERRED_DESTINATION_COLUMN_PURPOSE",
        Intermech.MaterialsHandbook.Consts.CoatingDestinationDestinationFieldGuid.ToString()
      },
      {
        "COATING_COLOR_TABLE_NAME",
        Intermech.MaterialsHandbook.Consts.CoatingColorTableGuid.ToString()
      },
      {
        "COATING_COLOR_COLUMN_COATING",
        Intermech.MaterialsHandbook.Consts.CoatingColorCoatingFieldGuid.ToString()
      },
      {
        "COATING_COLOR_COLUMN_COLOR",
        Intermech.MaterialsHandbook.Consts.CoatingColorColorFieldGuid.ToString()
      },
      {
        "COATING_COLOR_RAL_TABLE_NAME",
        Intermech.MaterialsHandbook.Consts.CoatingColorRalTableGuid.ToString()
      },
      {
        "BASE_MATERIAL_ATTR",
        string.Empty
      },
      {
        "COLOR_VARNISH_ATTR",
        Intermech.MaterialsHandbook.Consts.VarnishColorAttrTypeGuid.ToString()
      },
      {
        "DISPLAY_SETTING_SHOW_RECORDS",
        Convert.ToString(true)
      }
    };
  }

  private string GetAttrNameValue(XmlNode node)
  {
    string empty = string.Empty;
    if (node != null)
    {
      XmlAttribute attribute = node.Attributes["Name"];
      if (attribute != null)
        empty = attribute.Value;
    }
    return empty;
  }

  private void LoadSettings(IUserSession session)
  {
    bool flag = false;
    if (session != null)
    {
      BlobInformation config_info;
      byte[] config_file;
      session.Configurations.LoadConfigData("IMH.SystemSettings", out config_info, out config_file, 0L);
      if (config_info.RealFileSize != 0L && config_file != null && config_file.Length != 0)
      {
        string xml = string.Empty;
        IPackedStream service = ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true);
        lock (this)
        {
          using (MemoryStream inStream = new MemoryStream(config_file))
          {
            inStream.Position = 0L;
            using (MemoryStream memoryStream = new MemoryStream(config_file.Length / 4))
            {
              service.UnpackStream((Stream) memoryStream, (Stream) inStream);
              memoryStream.Position = 0L;
              using (BinaryReader binaryReader = new BinaryReader((Stream) memoryStream))
                xml = binaryReader.ReadString();
            }
          }
        }
        if (!string.IsNullOrEmpty(xml))
          flag = this.ParseXML(xml);
      }
    }
    if (flag)
      return;
    this.CreateVoidSettings();
  }

  private bool ParseXML(string xml)
  {
    bool xml1 = false;
    if (!string.IsNullOrEmpty(xml))
    {
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.InnerXml = xml;
      XmlNode xmlNode1 = xmlDocument.SelectSingleNode($"{"IMHSystemSettings"}/{"Binding"}");
      if (xmlNode1 != null)
      {
        xml1 = true;
        List<XmlNode> xmlNodeList1 = new List<XmlNode>(4);
        int num1 = 0;
        XmlNode xmlNode2 = xmlNode1.SelectSingleNode("Catalogs");
        if (xmlNode2 != null)
        {
          num1 += xmlNode2.ChildNodes.Count;
          xmlNodeList1.Add(xmlNode2);
        }
        XmlNode xmlNode3 = xmlNode1.SelectSingleNode("Folders");
        if (xmlNode3 != null)
        {
          num1 += xmlNode3.ChildNodes.Count;
          xmlNodeList1.Add(xmlNode3);
        }
        XmlNode xmlNode4 = xmlNode1.SelectSingleNode("Tables");
        if (xmlNode4 != null)
        {
          num1 += xmlNode4.ChildNodes.Count;
          xmlNodeList1.Add(xmlNode4);
        }
        XmlNode xmlNode5 = xmlNode1.SelectSingleNode("Columns");
        if (xmlNode5 != null)
        {
          num1 += xmlNode5.ChildNodes.Count;
          xmlNodeList1.Add(xmlNode5);
        }
        XmlNode xmlNode6 = xmlNode1.SelectSingleNode("Attributes");
        if (xmlNode6 != null)
        {
          num1 += xmlNode6.ChildNodes.Count;
          xmlNodeList1.Add(xmlNode6);
        }
        XmlNode xmlNode7 = xmlNode1.SelectSingleNode("DisplaySettings");
        if (xmlNode7 != null)
        {
          int num2 = num1 + xmlNode7.ChildNodes.Count;
          xmlNodeList1.Add(xmlNode7);
        }
        this.CreateVoidSettings();
        foreach (XmlNode xmlNode8 in xmlNodeList1)
        {
          foreach (XmlNode childNode in xmlNode8.ChildNodes)
          {
            XmlAttribute attribute1 = childNode.Attributes["Name"];
            XmlAttribute attribute2 = childNode.Attributes["Value"];
            if (attribute1 != null)
            {
              string key = attribute1.Value;
              if (!string.IsNullOrEmpty(key))
              {
                if (key == "COATING_PROPERTIES_TABLE_NAME")
                {
                  DataTable dtParams = new DataTable();
                  dtParams.Columns.AddRange(new DataColumn[2]
                  {
                    new DataColumn("P1"),
                    new DataColumn("P2")
                  });
                  XmlNodeList xmlNodeList2 = childNode.SelectNodes("Table/Row");
                  if (xmlNodeList2 != null && xmlNodeList2.Count > 0)
                  {
                    string empty = string.Empty;
                    foreach (XmlNode xmlNode9 in xmlNodeList2)
                    {
                      List<XmlAttribute> xmlAttributeList = new List<XmlAttribute>((IEnumerable<XmlAttribute>) new XmlAttribute[2]
                      {
                        xmlNode9.Attributes["P1"],
                        xmlNode9.Attributes["P2"]
                      });
                      int num3 = 0;
                      DataRow row = dtParams.NewRow();
                      bool flag = false;
                      foreach (XmlAttribute xmlAttribute in xmlAttributeList)
                      {
                        ++num3;
                        if (xmlAttribute != null)
                        {
                          string str = xmlAttribute.Value;
                          if (!string.IsNullOrEmpty(str) && GuidHelper.IsGuid(str))
                          {
                            flag = true;
                            row[num3 - 1] = (object) new Guid(str);
                          }
                        }
                      }
                      if (flag)
                        dtParams.Rows.Add(row);
                    }
                    if (dtParams.Rows.Count > 0)
                      this._coatingsSettings = new IMHCoatingsSystemSettings(dtParams);
                  }
                }
                string str1;
                if (!this._dictSettings.ContainsKey(key) || this._dictSettings.TryGetValue(key, out str1) && str1 != attribute2.Value)
                  this._dictSettings[key] = attribute2.Value;
              }
            }
          }
        }
        this.ParseAssortmentSearchSettings(xmlNode1.SelectSingleNode($"{"Search"}/{"AssortmentSettings"}"));
      }
    }
    return xml1;
  }

  private void ParseAssortmentSearchSettings(XmlNode rootSearchNode)
  {
    if (rootSearchNode == null || rootSearchNode.ChildNodes.Count <= 0)
      return;
    this._assortmentSearchSettings = new List<IMHAssortmentClass>(rootSearchNode.ChildNodes.Count);
    string empty = string.Empty;
    foreach (XmlNode childNode1 in rootSearchNode.ChildNodes)
    {
      if (childNode1.ChildNodes.Count != 0)
      {
        string attrNameValue1 = this.GetAttrNameValue(childNode1);
        if (!string.IsNullOrEmpty(attrNameValue1))
        {
          IMHAssortmentClass imhAssortmentClass = new IMHAssortmentClass(attrNameValue1);
          this._assortmentSearchSettings.Add(imhAssortmentClass);
          foreach (XmlNode childNode2 in childNode1.ChildNodes)
          {
            if (childNode2.ChildNodes.Count != 0)
            {
              string attrNameValue2 = this.GetAttrNameValue(childNode2);
              if (imhAssortmentClass.AddAbstractName(attrNameValue2))
              {
                foreach (XmlNode childNode3 in childNode2.ChildNodes)
                  imhAssortmentClass.AddAttribute(attrNameValue2, childNode3.InnerText, true);
              }
            }
          }
        }
      }
    }
  }
}
