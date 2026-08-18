// Decompiled with JetBrains decompiler
// Type: Intermech.MaterialsHandbook.Server.IMHUserSettingsService
// Assembly: Intermech.MaterialsHandbook.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 415584AC-BDF0-4945-B0B3-EBEC9DE4A5E1
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.MaterialsHandbook.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.MaterialsHandbook;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

#nullable disable
namespace Intermech.MaterialsHandbook.Server;

public class IMHUserSettingsService : LongLifeObject, IIMHUserSettingsService
{
  private XmlDocument _doc = new XmlDocument();
  private Dictionary<Guid, IMHUserSettingsService.IMHUserSettings> _settings = new Dictionary<Guid, IMHUserSettingsService.IMHUserSettings>();
  private const string ROOT_NODE_NAME = "IMHUserSettings";
  private const string USER = "User";
  private const string FAVOURITES_SETTINGS = "FavouritesSettings";
  private const string CATEGORY = "Category";
  private const string MATERIALS = "Materials";
  private const string ASSORTMENTS = "Assortments";
  private const string MATERIAL = "Material";
  private const string ASSORTMENT = "Assortment";
  private const string GUID_ATTR = "Guid";
  private const string FOLDER_ID_ATTR = "FolderID";
  private const string TABLE_REF_ID_ATTR = "TableRefID";
  private const string RECORD_ID_ATTR = "RecordID";
  private const string COATINGS = "Coatings";
  private const string COATING = "Coating";
  private const string COATING_KEY = "CoatingKey";
  private const string MATERIAL_KEY = "MaterialKey";
  private const string PARAM = "Param";
  private const string CAPTION = "Caption";

  public IMHUserSettingsService(IUserSession session)
  {
    try
    {
      this.LoadUserSettings(session);
    }
    catch (Exception ex)
    {
      if (!(ServerServices.GetService(typeof (IOutputView)) is IOutputView service))
        return;
      service.WriteString("IMBASE", "Раздел: считывание настроек отображения таблиц");
      service.WriteString("IMBASE", ex.Message);
    }
  }

  public List<FavouriteData> GetAssortmentFavourites(Guid userGuid, Guid categoryGuid)
  {
    List<FavouriteData> favouriteDataList = (List<FavouriteData>) null;
    if (this._settings.ContainsKey(userGuid))
    {
      IMHUserSettingsService.IMHUserSettings setting = this._settings[userGuid];
      if (setting.Favourites.ContainsKey(categoryGuid))
        favouriteDataList = setting.Favourites[categoryGuid]?.Assortment;
    }
    return favouriteDataList ?? new List<FavouriteData>(0);
  }

  public List<CoatingsFavouriteData> GetCoatingFavourites(Guid userGuid, Guid categoryGuid)
  {
    List<CoatingsFavouriteData> coatingsFavouriteDataList = (List<CoatingsFavouriteData>) null;
    if (this._settings.ContainsKey(userGuid))
    {
      IMHUserSettingsService.IMHUserSettings setting = this._settings[userGuid];
      if (setting.Favourites.ContainsKey(categoryGuid))
        coatingsFavouriteDataList = setting.Favourites[categoryGuid]?.Coatings;
    }
    return coatingsFavouriteDataList ?? new List<CoatingsFavouriteData>(0);
  }

  public List<FavouriteData> GetMaterialFavourites(Guid userGuid, Guid categoryGuid)
  {
    List<FavouriteData> favouriteDataList = (List<FavouriteData>) null;
    if (this._settings.ContainsKey(userGuid))
    {
      IMHUserSettingsService.IMHUserSettings setting = this._settings[userGuid];
      if (setting.Favourites.ContainsKey(categoryGuid))
        favouriteDataList = setting.Favourites[categoryGuid]?.Materials;
    }
    return favouriteDataList ?? new List<FavouriteData>(0);
  }

  public void RemoveAssortmentFavourites(
    Guid categoryGuid,
    List<FavouriteData> assortmentFavourites)
  {
    if (assortmentFavourites == null || assortmentFavourites.Count <= 0)
      return;
    List<Guid> guidList = new List<Guid>(this._settings.Count);
    foreach (KeyValuePair<Guid, IMHUserSettingsService.IMHUserSettings> setting in this._settings)
    {
      if (setting.Value != null && setting.Value.Favourites.ContainsKey(categoryGuid))
      {
        IMHUserSettingsService.FavouritesList favourite = setting.Value.Favourites[categoryGuid];
        if (favourite != null && favourite.Assortment != null && favourite.Assortment.Count != 0)
        {
          foreach (FavouriteData assortmentFavourite in assortmentFavourites)
          {
            if (assortmentFavourite != null && favourite.Assortment.Contains(assortmentFavourite))
              favourite.Assortment.Remove(assortmentFavourite);
          }
          if (favourite.Empty)
            setting.Value.Favourites.Remove(categoryGuid);
          if (setting.Value.Empty)
            guidList.Add(setting.Key);
        }
      }
    }
    if (guidList.Count <= 0)
      return;
    foreach (Guid key in guidList)
      this._settings.Remove(key);
  }

  public void SaveAssortmentFavourites(
    Guid userGuid,
    Guid categoryGuid,
    List<FavouriteData> assortmentFavourites)
  {
    if (!(userGuid != Guid.Empty) || !(categoryGuid != Guid.Empty))
      return;
    if (assortmentFavourites != null && assortmentFavourites.Count > 0)
    {
      IMHUserSettingsService.IMHUserSettings imhUserSettings;
      if (this._settings.ContainsKey(userGuid))
      {
        imhUserSettings = this._settings[userGuid];
      }
      else
      {
        imhUserSettings = new IMHUserSettingsService.IMHUserSettings(userGuid);
        this._settings[userGuid] = imhUserSettings;
      }
      IMHUserSettingsService.FavouritesList favouritesList = imhUserSettings.Favourites.ContainsKey(categoryGuid) ? imhUserSettings.Favourites[categoryGuid] : new IMHUserSettingsService.FavouritesList();
      favouritesList.Assortment = assortmentFavourites;
      imhUserSettings.Favourites[categoryGuid] = favouritesList;
    }
    else
    {
      if (!this._settings.ContainsKey(userGuid))
        return;
      IMHUserSettingsService.IMHUserSettings setting = this._settings[userGuid];
      if (setting.Favourites.ContainsKey(categoryGuid))
      {
        IMHUserSettingsService.FavouritesList favourite = setting.Favourites[categoryGuid];
        if (favourite != null && favourite.Assortment != null)
          favourite.Assortment.Clear();
      }
      if (!setting.Empty)
        return;
      this._settings.Remove(userGuid);
    }
  }

  public void SaveCoatingFavourites(
    Guid userGuid,
    Guid categoryGuid,
    List<CoatingsFavouriteData> coatingFavourites)
  {
    if (!(userGuid != Guid.Empty) || !(categoryGuid != Guid.Empty) || coatingFavourites == null)
      return;
    IMHUserSettingsService.IMHUserSettings imhUserSettings;
    if (this._settings.ContainsKey(userGuid))
    {
      imhUserSettings = this._settings[userGuid];
    }
    else
    {
      imhUserSettings = new IMHUserSettingsService.IMHUserSettings(userGuid);
      this._settings[userGuid] = imhUserSettings;
    }
    IMHUserSettingsService.FavouritesList favouritesList = imhUserSettings.Favourites.ContainsKey(categoryGuid) ? imhUserSettings.Favourites[categoryGuid] : new IMHUserSettingsService.FavouritesList();
    favouritesList.Coatings = coatingFavourites;
    imhUserSettings.Favourites[categoryGuid] = favouritesList;
  }

  public void SaveMaterialFavourites(
    Guid userGuid,
    Guid categoryGuid,
    List<FavouriteData> materialFavourites)
  {
    if (!(userGuid != Guid.Empty) || !(categoryGuid != Guid.Empty))
      return;
    if (materialFavourites != null && materialFavourites.Count > 0)
    {
      IMHUserSettingsService.IMHUserSettings imhUserSettings;
      if (this._settings.ContainsKey(userGuid))
      {
        imhUserSettings = this._settings[userGuid];
      }
      else
      {
        imhUserSettings = new IMHUserSettingsService.IMHUserSettings(userGuid);
        this._settings[userGuid] = imhUserSettings;
      }
      IMHUserSettingsService.FavouritesList favouritesList = imhUserSettings.Favourites.ContainsKey(categoryGuid) ? imhUserSettings.Favourites[categoryGuid] : new IMHUserSettingsService.FavouritesList();
      favouritesList.Materials = materialFavourites;
      imhUserSettings.Favourites[categoryGuid] = favouritesList;
    }
    else
    {
      if (!this._settings.ContainsKey(userGuid))
        return;
      IMHUserSettingsService.IMHUserSettings setting = this._settings[userGuid];
      if (setting.Favourites.ContainsKey(categoryGuid))
      {
        IMHUserSettingsService.FavouritesList favourite = setting.Favourites[categoryGuid];
        if (favourite != null && favourite.Materials != null)
          favourite.Materials.Clear();
      }
      if (!setting.Empty)
        return;
      this._settings.Remove(userGuid);
    }
  }

  public void SaveUserSettings()
  {
    if (!(ServerServices.GetService(typeof (IDBTimedEvents)) is IDBTimedEvents service1))
      return;
    IUserSession userSession = (IUserSession) null;
    try
    {
      userSession = service1.GetSystemSessionTemporaryClone("IMH.SaveUserSettings");
      if (userSession == null)
        return;
      string userSettings = this.GetUserSettings();
      if (!(userSettings != string.Empty))
        return;
      IDBConfigurations configurations = userSession.Configurations;
      if (configurations == null)
        return;
      IPackedStream service2 = ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true);
      using (MemoryStream memoryStream = new MemoryStream(userSettings.Length))
      {
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) memoryStream))
        {
          binaryWriter.Write(userSettings);
          binaryWriter.Flush();
          memoryStream.Position = 0L;
          using (MemoryStream outStream = new MemoryStream((int) memoryStream.Length / 2))
          {
            service2.PackStream((Stream) outStream, (Stream) memoryStream, 9);
            BlobInformation config_info = new BlobInformation(outStream.Length, outStream.Length, DateTime.Now, "IMH.IMHUserSettings", ArcMethods.ZLibPacked, string.Empty);
            configurations.WriteConfigData(config_info, outStream.ToArray(), 0L);
          }
        }
      }
    }
    finally
    {
      userSession?.Logout("IMH.SaveUserSettings");
    }
  }

  private string GetUserSettings()
  {
    XmlNode element1 = (XmlNode) this._doc.CreateElement("IMHUserSettings");
    foreach (KeyValuePair<Guid, IMHUserSettingsService.IMHUserSettings> setting in this._settings)
    {
      if (setting.Value != null)
      {
        XmlElement element2 = this._doc.CreateElement("User");
        element2.SetAttribute("Guid", setting.Key.ToString());
        XmlNode favourites = this.GetFavourites(setting.Value.Favourites);
        if (favourites != null)
          element2.AppendChild(favourites);
        if (element2.HasChildNodes)
          element1.AppendChild((XmlNode) element2);
      }
    }
    return !element1.HasChildNodes ? string.Empty : element1.OuterXml;
  }

  private XmlNode GetFavourites(
    Dictionary<Guid, IMHUserSettingsService.FavouritesList> categoryList)
  {
    XmlNode favourites = (XmlNode) null;
    if (categoryList.Count > 0)
    {
      favourites = (XmlNode) this._doc.CreateElement("FavouritesSettings");
      foreach (KeyValuePair<Guid, IMHUserSettingsService.FavouritesList> category in categoryList)
      {
        XmlElement element1 = this._doc.CreateElement("Category");
        element1.SetAttribute("Guid", category.Key.ToString());
        IMHUserSettingsService.FavouritesList favouritesList = category.Value;
        if (favouritesList != null && !favouritesList.Empty)
        {
          if (favouritesList.Materials != null && favouritesList.Materials.Count > 0)
          {
            XmlNode element2 = (XmlNode) this._doc.CreateElement("Materials");
            foreach (FavouriteData material in favouritesList.Materials)
            {
              XmlElement element3 = this._doc.CreateElement("Material");
              XmlElement xmlElement1 = element3;
              long num = material.TableRefID;
              string str1 = num.ToString();
              xmlElement1.SetAttribute("TableRefID", str1);
              if (material.RecordID > -1L)
              {
                XmlElement xmlElement2 = element3;
                num = material.RecordID;
                string str2 = num.ToString();
                xmlElement2.SetAttribute("RecordID", str2);
              }
              element3.InnerText = material.Caption;
              element2.AppendChild((XmlNode) element3);
            }
            element1.AppendChild(element2);
          }
          if (favouritesList.Assortment != null && favouritesList.Assortment.Count > 0)
          {
            XmlNode element4 = (XmlNode) this._doc.CreateElement("Assortments");
            foreach (FavouriteData favouriteData in favouritesList.Assortment)
            {
              XmlElement element5 = this._doc.CreateElement("Assortment");
              if (favouriteData.FolderID != 0L)
                element5.SetAttribute("FolderID", favouriteData.FolderID.ToString());
              XmlElement xmlElement3 = element5;
              long num = favouriteData.TableRefID;
              string str3 = num.ToString();
              xmlElement3.SetAttribute("TableRefID", str3);
              XmlElement xmlElement4 = element5;
              num = favouriteData.RecordID;
              string str4 = num.ToString();
              xmlElement4.SetAttribute("RecordID", str4);
              element5.InnerText = favouriteData.Caption;
              element4.AppendChild((XmlNode) element5);
            }
            element1.AppendChild(element4);
          }
          if (favouritesList.Coatings != null && favouritesList.Coatings.Count > 0)
          {
            XmlNode element6 = (XmlNode) this._doc.CreateElement("Coatings");
            foreach (CoatingsFavouriteData coating in favouritesList.Coatings)
            {
              if (coating.Params != null && coating.Params.Count != 0)
              {
                XmlElement element7 = this._doc.CreateElement("Coating");
                element7.SetAttribute("CoatingKey", coating.CoatingsKey.ToString());
                element7.SetAttribute("MaterialKey", coating.MaterialsKey.ToString());
                foreach (object obj in coating.Params)
                {
                  XmlElement element8 = this._doc.CreateElement("Param");
                  element8.InnerText = obj.ToString();
                  element7.AppendChild((XmlNode) element8);
                }
                if (element7.HasChildNodes)
                {
                  XmlNode element9 = (XmlNode) this._doc.CreateElement("Caption");
                  element9.InnerText = coating.Caption;
                  element7.AppendChild(element9);
                  element6.AppendChild((XmlNode) element7);
                }
              }
            }
            if (element6.HasChildNodes)
              element1.AppendChild(element6);
          }
          if (element1.HasChildNodes)
            favourites.AppendChild((XmlNode) element1);
        }
      }
    }
    return favourites;
  }

  private Guid GuidFromString(string strGuid)
  {
    return !GuidHelper.IsGuid(strGuid) ? Guid.Empty : new Guid(strGuid);
  }

  private Dictionary<Guid, IMHUserSettingsService.FavouritesList> LoadFavourites(
    XmlNode favouritesNode)
  {
    Dictionary<Guid, IMHUserSettingsService.FavouritesList> dictionary = (Dictionary<Guid, IMHUserSettingsService.FavouritesList>) null;
    XmlNodeList xmlNodeList = favouritesNode.SelectNodes("Category");
    if (xmlNodeList != null)
    {
      dictionary = new Dictionary<Guid, IMHUserSettingsService.FavouritesList>(xmlNodeList.Count);
      foreach (XmlNode xmlNode1 in xmlNodeList)
      {
        XmlAttribute attribute = xmlNode1.Attributes["Guid"];
        if (attribute != null)
        {
          Guid key = this.GuidFromString(attribute.Value);
          if (!(key == Guid.Empty))
          {
            IMHUserSettingsService.FavouritesList favouritesList = new IMHUserSettingsService.FavouritesList();
            XmlNode xmlNode2 = xmlNode1.SelectSingleNode($"{"Materials"}");
            if (xmlNode2 != null)
              favouritesList.Materials = this.ParseFavourites(xmlNode2.ChildNodes);
            XmlNode xmlNode3 = xmlNode1.SelectSingleNode($"{"Assortments"}");
            if (xmlNode3 != null)
              favouritesList.Assortment = this.ParseFavourites(xmlNode3.ChildNodes);
            XmlNode xmlNode4 = xmlNode1.SelectSingleNode($"{"Coatings"}");
            if (xmlNode4 != null)
              favouritesList.Coatings = this.ParseCoatingsFavourites(xmlNode4.ChildNodes);
            if (!favouritesList.Empty)
              dictionary[key] = favouritesList;
          }
        }
      }
    }
    return dictionary == null || dictionary.Count <= 0 ? (Dictionary<Guid, IMHUserSettingsService.FavouritesList>) null : dictionary;
  }

  private void LoadUserSettings(IUserSession session)
  {
    if (session == null)
      return;
    BlobInformation config_info;
    byte[] config_file;
    session.Configurations.LoadConfigData("IMH.IMHUserSettings", out config_info, out config_file, 0L);
    if (config_info.RealFileSize == 0L || config_file == null || config_file.Length == 0)
      return;
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
    this.ParseXML(xml);
  }

  private long ObjIDFromString(string strObjID)
  {
    long result = 0;
    return !long.TryParse(strObjID, out result) ? 0L : result;
  }

  private List<CoatingsFavouriteData> ParseCoatingsFavourites(XmlNodeList nodes)
  {
    List<CoatingsFavouriteData> coatingsFavouriteDataList = (List<CoatingsFavouriteData>) null;
    if (nodes != null)
    {
      coatingsFavouriteDataList = new List<CoatingsFavouriteData>(nodes.Count);
      foreach (XmlNode node in nodes)
      {
        string empty1 = string.Empty;
        string empty2 = string.Empty;
        List<object> parameters = new List<object>(node.ChildNodes.Count);
        XmlAttribute attribute1 = node.Attributes["CoatingKey"];
        if (attribute1 != null && !string.IsNullOrEmpty(attribute1.Value))
          empty1 = attribute1.Value;
        XmlAttribute attribute2 = node.Attributes["MaterialKey"];
        if (attribute2 != null && !string.IsNullOrEmpty(attribute2.Value))
          empty2 = attribute2.Value;
        string caption = string.Empty;
        XmlNode xmlNode1 = node.SelectSingleNode("Caption");
        if (xmlNode1 != null)
          caption = xmlNode1.InnerText;
        XmlNodeList xmlNodeList = node.SelectNodes(string.Format("Param"));
        if (xmlNodeList != null)
        {
          foreach (XmlNode xmlNode2 in xmlNodeList)
            parameters.Add((object) xmlNode2.InnerText);
        }
        if (!string.IsNullOrEmpty(empty1) && !string.IsNullOrEmpty(empty2) && parameters.Count != 0)
          coatingsFavouriteDataList.Add(new CoatingsFavouriteData(empty1, empty2, parameters, caption));
      }
    }
    return coatingsFavouriteDataList == null || coatingsFavouriteDataList.Count <= 0 ? (List<CoatingsFavouriteData>) null : coatingsFavouriteDataList;
  }

  private List<FavouriteData> ParseFavourites(XmlNodeList nodes)
  {
    List<FavouriteData> favouriteDataList = (List<FavouriteData>) null;
    if (nodes != null)
    {
      favouriteDataList = new List<FavouriteData>(nodes.Count);
      foreach (XmlNode node in nodes)
      {
        XmlAttribute attribute1 = node.Attributes["FolderID"];
        long folderID = 0;
        if (attribute1 != null && !string.IsNullOrEmpty(attribute1.Value))
          folderID = this.ObjIDFromString(attribute1.Value);
        XmlAttribute attribute2 = node.Attributes["TableRefID"];
        if (attribute2 != null && !string.IsNullOrEmpty(attribute2.Value))
        {
          long tableRefID = this.ObjIDFromString(attribute2.Value);
          if (tableRefID != 0L)
          {
            XmlAttribute attribute3 = node.Attributes["RecordID"];
            int recID = -1;
            if (attribute3 != null && !string.IsNullOrEmpty(attribute3.Value))
              recID = this.RecordNumFromString(attribute3.Value);
            favouriteDataList.Add(new FavouriteData(folderID, tableRefID, (long) recID, node.InnerText));
          }
        }
      }
    }
    return favouriteDataList == null || favouriteDataList.Count <= 0 ? (List<FavouriteData>) null : favouriteDataList;
  }

  private void ParseXML(string xml)
  {
    if (string.IsNullOrEmpty(xml))
      return;
    this._doc.InnerXml = xml;
    XmlNodeList xmlNodeList = this._doc.SelectNodes($"{"IMHUserSettings"}/{"User"}");
    if (xmlNodeList == null)
      return;
    foreach (XmlNode xmlNode in xmlNodeList)
    {
      XmlAttribute attribute = xmlNode.Attributes["Guid"];
      if (attribute != null)
      {
        Guid guid = this.GuidFromString(attribute.Value);
        if (!(guid == Guid.Empty))
        {
          IMHUserSettingsService.IMHUserSettings imhUserSettings = new IMHUserSettingsService.IMHUserSettings(guid);
          XmlNode favouritesNode = xmlNode.SelectSingleNode("FavouritesSettings");
          if (favouritesNode != null)
          {
            Dictionary<Guid, IMHUserSettingsService.FavouritesList> dictionary = this.LoadFavourites(favouritesNode);
            if (dictionary != null)
              imhUserSettings.Favourites = dictionary;
          }
          if (!imhUserSettings.Empty)
            this._settings.Add(guid, imhUserSettings);
        }
      }
    }
  }

  private int RecordNumFromString(string strRecID)
  {
    int result = -1;
    return !int.TryParse(strRecID, out result) ? -1 : result;
  }

  private class FavouritesList
  {
    internal List<FavouriteData> Assortment;
    internal List<FavouriteData> Materials;
    internal List<CoatingsFavouriteData> Coatings;

    internal bool Empty
    {
      get
      {
        if (this.Assortment != null && this.Assortment.Count != 0 || this.Materials != null && this.Materials.Count != 0)
          return false;
        return this.Coatings == null || this.Coatings.Count == 0;
      }
    }
  }

  private class IMHUserSettings
  {
    private Guid _userGuid = Guid.Empty;
    internal Dictionary<Guid, IMHUserSettingsService.FavouritesList> Favourites = new Dictionary<Guid, IMHUserSettingsService.FavouritesList>();

    internal bool Empty
    {
      get
      {
        bool empty = this.Favourites == null || this.Favourites.Count == 0;
        if (!empty)
        {
          empty = true;
          foreach (KeyValuePair<Guid, IMHUserSettingsService.FavouritesList> favourite in this.Favourites)
          {
            if (favourite.Value != null && !favourite.Value.Empty)
            {
              empty = false;
              break;
            }
          }
        }
        return empty;
      }
    }

    public IMHUserSettings(Guid userGuid) => this._userGuid = userGuid;
  }
}
