// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Server.EcoPropHolder
// Assembly: Intermech.ECO.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E6459663-BB12-41FD-949A-3849B46AE118
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ECO.Server.dll

using Intermech.Configuration;
using Intermech.Interfaces;
using System;
using System.IO;
using System.Xml;

#nullable disable
namespace Intermech.ECO.Server;

internal class EcoPropHolder : IEcoPropHolder
{
  private bool _autoMove = true;
  private bool _warnOnMove;
  private bool _writeComplect;
  private string _kiInventoryNumberTemplate = "{99999}#-{yy}";
  private bool _writeDesForReplace;
  private bool _leaveOTDForChange;
  private bool _autoCheckOut = true;
  private int _daysBeforeEnd;
  private bool _placeInvNum;
  private string _invNumAttr = "";
  private bool _hideHidden;
  private bool _autoOrigSize;
  private bool _createLiteraVersion;
  private bool _setLiteraForFullSostav;
  private int _maxDocNum;
  private bool _replaceEmptyDes;
  private bool _hideOnCreation;
  private bool _prohibitCustomReason;
  private bool _askOnNewOrgs;
  private bool _checkObjCreation;
  private bool _noSlashInDPIDesign;

  public void SaveToBase(IUserSession ius)
  {
    MemoryStream outStream1 = new MemoryStream();
    XmlDocument xmlDocument1 = new XmlDocument();
    XmlElement element1 = xmlDocument1.CreateElement("EcoProperties");
    XmlElement element2 = xmlDocument1.CreateElement("writeComplect");
    element2.InnerText = this._writeComplect.ToString();
    XmlElement element3 = xmlDocument1.CreateElement("autoCheckOut");
    element3.InnerText = this._autoCheckOut.ToString();
    element1.AppendChild((XmlNode) element2);
    element1.AppendChild((XmlNode) element3);
    xmlDocument1.AppendChild((XmlNode) element1);
    xmlDocument1.Save((Stream) outStream1);
    IDBConfigurations configurations = ius.Configurations;
    BlobInformation config_info = new BlobInformation(outStream1.Length, outStream1.Length, DateTime.Now, "EcoProperties", ArcMethods.NotPacked, string.Empty);
    configurations.WriteConfigData(config_info, outStream1.ToArray());
    if (!ius.IsAdmin)
      return;
    XmlDocument xmlDocument2 = new XmlDocument();
    XmlElement element4 = xmlDocument2.CreateElement("EcoAdminProperties");
    XmlElement element5 = xmlDocument2.CreateElement("AutoMoveObjects");
    element5.InnerText = this._autoMove.ToString();
    XmlElement element6 = xmlDocument2.CreateElement("warnOnMoveOfMove");
    element6.InnerText = this._warnOnMove.ToString();
    XmlElement element7 = xmlDocument2.CreateElement("kiInventoryNumberTemplate");
    element7.InnerText = this._kiInventoryNumberTemplate;
    XmlElement element8 = xmlDocument2.CreateElement("writeDes");
    element8.InnerText = this._writeDesForReplace.ToString();
    XmlElement element9 = xmlDocument2.CreateElement("leaveOTD");
    element9.InnerText = this._leaveOTDForChange.ToString();
    XmlElement element10 = xmlDocument2.CreateElement("hideHidden");
    element10.InnerText = this._hideHidden.ToString();
    XmlElement element11 = xmlDocument2.CreateElement("placeInvNum");
    element11.InnerText = this._placeInvNum.ToString();
    XmlElement element12 = xmlDocument2.CreateElement("invNumElem");
    element12.InnerText = this._invNumAttr;
    XmlElement element13 = xmlDocument2.CreateElement("autoOrigSize");
    element13.InnerText = this._autoOrigSize.ToString();
    XmlElement element14 = xmlDocument2.CreateElement("createLitVersion");
    element14.InnerText = this._createLiteraVersion.ToString();
    XmlElement element15 = xmlDocument2.CreateElement("setLitSostav");
    element15.InnerText = this._setLiteraForFullSostav.ToString();
    XmlElement element16 = xmlDocument2.CreateElement("moveAuthFiles");
    element16.InnerText = this.MoveAuthenticFiles.ToString();
    XmlElement element17 = xmlDocument2.CreateElement("daysBeforeEnd");
    XmlElement xmlElement1 = element17;
    int num = this.DaysBeforeEndTermWarning;
    string str1 = num.ToString();
    xmlElement1.InnerText = str1;
    XmlElement element18 = xmlDocument2.CreateElement("maxDocsAllowed");
    XmlElement xmlElement2 = element18;
    num = this.MaxDocNum;
    string str2 = num.ToString();
    xmlElement2.InnerText = str2;
    XmlElement element19 = xmlDocument2.CreateElement("replaceEmptyDes");
    element19.InnerText = this._replaceEmptyDes.ToString();
    XmlElement element20 = xmlDocument2.CreateElement("hideOnCreation");
    element20.InnerText = this._hideOnCreation.ToString();
    XmlElement element21 = xmlDocument2.CreateElement("prohibitCustomReason");
    element21.InnerText = this._prohibitCustomReason.ToString();
    XmlElement element22 = xmlDocument2.CreateElement("askNewOrgs");
    element22.InnerText = this._askOnNewOrgs.ToString();
    XmlElement element23 = xmlDocument2.CreateElement("checkObjCreation");
    element23.InnerText = this._checkObjCreation.ToString();
    XmlElement element24 = xmlDocument2.CreateElement("noSlashDPI");
    element24.InnerText = this._noSlashInDPIDesign.ToString();
    element4.AppendChild((XmlNode) element5);
    element4.AppendChild((XmlNode) element6);
    element4.AppendChild((XmlNode) element7);
    element4.AppendChild((XmlNode) element8);
    element4.AppendChild((XmlNode) element9);
    element4.AppendChild((XmlNode) element10);
    element4.AppendChild((XmlNode) element13);
    element4.AppendChild((XmlNode) element11);
    element4.AppendChild((XmlNode) element12);
    element4.AppendChild((XmlNode) element14);
    element4.AppendChild((XmlNode) element15);
    element4.AppendChild((XmlNode) element16);
    element4.AppendChild((XmlNode) element17);
    element4.AppendChild((XmlNode) element18);
    element4.AppendChild((XmlNode) element19);
    element4.AppendChild((XmlNode) element20);
    element4.AppendChild((XmlNode) element21);
    element4.AppendChild((XmlNode) element22);
    element4.AppendChild((XmlNode) element23);
    element4.AppendChild((XmlNode) element24);
    xmlDocument2.AppendChild((XmlNode) element4);
    MemoryStream outStream2 = new MemoryStream();
    xmlDocument2.Save((Stream) outStream2);
    config_info = new BlobInformation(outStream2.Length, outStream2.Length, DateTime.Now, "EcoAdminProperties", ArcMethods.NotPacked, string.Empty);
    configurations.WriteConfigData(config_info, outStream2.ToArray(), 0L);
  }

  public void LoadFromBase(IUserSession ius)
  {
    IDBConfigurations configurations = ius.Configurations;
    BlobInformation config_info;
    byte[] config_file;
    configurations.LoadConfigData("EcoProperties", out config_info, out config_file);
    if (config_info.RealFileSize > 0L)
    {
      MemoryStream inStream = new MemoryStream(config_file);
      XmlDocument xmlDocument = new XmlDocument();
      try
      {
        xmlDocument.Load((Stream) inStream);
        XmlNode firstChild = xmlDocument.FirstChild;
        if (firstChild.Name.Equals("EcoProperties"))
        {
          foreach (XmlNode childNode in firstChild.ChildNodes)
          {
            if (childNode.NodeType == XmlNodeType.Element)
            {
              if (childNode.Name == "writeComplect")
                this._writeComplect = AppSettingsHelper.ParseBoolean(childNode.InnerText, false);
              if (childNode.Name == "autoCheckOut")
                this._autoCheckOut = AppSettingsHelper.ParseBoolean(childNode.InnerText, true);
            }
          }
        }
      }
      catch (XmlException ex)
      {
      }
    }
    configurations.LoadConfigData("EcoAdminProperties", out config_info, out config_file, 0L);
    if (config_info.RealFileSize <= 0L)
      return;
    MemoryStream inStream1 = new MemoryStream(config_file);
    XmlDocument xmlDocument1 = new XmlDocument();
    try
    {
      xmlDocument1.Load((Stream) inStream1);
      XmlNode firstChild = xmlDocument1.FirstChild;
      if (!firstChild.Name.Equals("EcoAdminProperties"))
        return;
      foreach (XmlNode childNode in firstChild.ChildNodes)
      {
        if (childNode.NodeType == XmlNodeType.Element)
        {
          if (childNode.Name == "AutoMoveObjects")
            this._autoMove = AppSettingsHelper.ParseBoolean(childNode.InnerText, false);
          if (childNode.Name == "warnOnMoveOfMove")
            this._warnOnMove = AppSettingsHelper.ParseBoolean(childNode.InnerText, false);
          if (childNode.Name == "kiInventoryNumberTemplate")
            this._kiInventoryNumberTemplate = childNode.InnerText;
          if (childNode.Name == "writeDes")
            this._writeDesForReplace = AppSettingsHelper.ParseBoolean(childNode.InnerText, false);
          if (childNode.Name == "leaveOTD")
            this._leaveOTDForChange = AppSettingsHelper.ParseBoolean(childNode.InnerText, false);
          if (childNode.Name == "hideHidden")
            this._hideHidden = AppSettingsHelper.ParseBoolean(childNode.InnerText, false);
          if (childNode.Name == "placeInvNum")
            this._placeInvNum = AppSettingsHelper.ParseBoolean(childNode.InnerText, false);
          if (childNode.Name == "invNumElem")
            this._invNumAttr = childNode.InnerText;
          if (childNode.Name == "autoOrigSize")
            this._autoOrigSize = AppSettingsHelper.ParseBoolean(childNode.InnerText, false);
          if (childNode.Name == "createLitVersion")
            this._createLiteraVersion = AppSettingsHelper.ParseBoolean(childNode.InnerText, false);
          if (childNode.Name == "setLitSostav")
            this._setLiteraForFullSostav = AppSettingsHelper.ParseBoolean(childNode.InnerText, false);
          if (childNode.Name == "moveAuthFiles")
            this.MoveAuthenticFiles = AppSettingsHelper.ParseBoolean(childNode.InnerText, false);
          if (childNode.Name == "daysBeforeEnd")
            this._daysBeforeEnd = AppSettingsHelper.ParseInt32(childNode.InnerText, 0);
          if (childNode.Name == "maxDocsAllowed")
            this._maxDocNum = AppSettingsHelper.ParseInt32(childNode.InnerText, 0);
          if (childNode.Name == "replaceEmptyDes")
            this._replaceEmptyDes = AppSettingsHelper.ParseBoolean(childNode.InnerText, false);
          if (childNode.Name == "hideOnCreation")
            this._hideOnCreation = AppSettingsHelper.ParseBoolean(childNode.InnerText, false);
          if (childNode.Name == "prohibitCustomReason")
            this._prohibitCustomReason = AppSettingsHelper.ParseBoolean(childNode.InnerText, false);
          if (childNode.Name == "askNewOrgs")
            this._askOnNewOrgs = AppSettingsHelper.ParseBoolean(childNode.InnerText, false);
          if (childNode.Name == "checkObjCreation")
            this._checkObjCreation = AppSettingsHelper.ParseBoolean(childNode.InnerText, false);
          if (childNode.Name == "noSlashDPI")
            this._noSlashInDPIDesign = AppSettingsHelper.ParseBoolean(childNode.InnerText, false);
        }
      }
    }
    catch (XmlException ex)
    {
    }
  }

  public bool AutoMoveObjects
  {
    get => this._autoMove;
    set => this._autoMove = value;
  }

  public bool WarnOnMove
  {
    get => this._warnOnMove;
    set => this._warnOnMove = value;
  }

  public string KIInventoryNumberTemplate
  {
    get => this._kiInventoryNumberTemplate;
    set => this._kiInventoryNumberTemplate = value;
  }

  public bool WriteComplect
  {
    get => this._writeComplect;
    set => this._writeComplect = value;
  }

  public bool WriteDesOnReplace
  {
    get => this._writeDesForReplace;
    set => this._writeDesForReplace = value;
  }

  public bool LeaveOTDNumberForChange
  {
    get => this._leaveOTDForChange;
    set => this._leaveOTDForChange = value;
  }

  public bool AutoCheckOut
  {
    get => this._autoCheckOut;
    set => this._autoCheckOut = value;
  }

  public int DaysBeforeEndTermWarning
  {
    get => this._daysBeforeEnd;
    set => this._daysBeforeEnd = value;
  }

  public bool PlaceInvNum
  {
    get => this._placeInvNum;
    set => this._placeInvNum = value;
  }

  public string InvNumAttr
  {
    get => this._invNumAttr;
    set => this._invNumAttr = value;
  }

  public bool HideHiddenObjects
  {
    get => this._hideHidden;
    set => this._hideHidden = value;
  }

  public bool AutoOriginalSize
  {
    get => this._autoOrigSize;
    set => this._autoOrigSize = value;
  }

  public bool CreateLiteraVersion
  {
    get => this._createLiteraVersion;
    set => this._createLiteraVersion = value;
  }

  public bool SetLiteraForFullSostav
  {
    get => this._setLiteraForFullSostav;
    set => this._setLiteraForFullSostav = value;
  }

  public int MaxDocNum
  {
    get => this._maxDocNum;
    set => this._maxDocNum = value;
  }

  public bool MoveAuthenticFiles { get; set; }

  public bool ReplaceEmptyDesByTemplate
  {
    get => this._replaceEmptyDes;
    set => this._replaceEmptyDes = value;
  }

  public bool HideOnCreation
  {
    get => this._hideOnCreation;
    set => this._hideOnCreation = value;
  }

  public bool ProhibitCustomReason
  {
    get => this._prohibitCustomReason;
    set => this._prohibitCustomReason = value;
  }

  public bool AskOnNewOrganizations
  {
    get => this._askOnNewOrgs;
    set => this._askOnNewOrgs = value;
  }

  public bool CheckObjectCreation
  {
    get => this._checkObjCreation;
    set => this._checkObjCreation = value;
  }

  public bool NoSlashInDPIDesign
  {
    get => this._noSlashInDPIDesign;
    set => this._noSlashInDPIDesign = value;
  }

  public override int GetHashCode() => base.GetHashCode();

  public override bool Equals(object obj)
  {
    if (!(obj is EcoPropHolder))
      return base.Equals(obj);
    EcoPropHolder ecoPropHolder = obj as EcoPropHolder;
    return ecoPropHolder._autoMove.Equals(this._autoMove) && ecoPropHolder._warnOnMove.Equals(this._warnOnMove) && ecoPropHolder._writeComplect.Equals(this._writeComplect) && ecoPropHolder._writeDesForReplace.Equals(this._writeDesForReplace) && ecoPropHolder._kiInventoryNumberTemplate.Equals(this._kiInventoryNumberTemplate) && ecoPropHolder._leaveOTDForChange.Equals(this._leaveOTDForChange) && ecoPropHolder._autoCheckOut.Equals(this._autoCheckOut) && ecoPropHolder._daysBeforeEnd.Equals(this._daysBeforeEnd) && ecoPropHolder._placeInvNum.Equals(this._placeInvNum) && ecoPropHolder._invNumAttr.Equals(this._invNumAttr) && ecoPropHolder._hideHidden.Equals(this._hideHidden) && ecoPropHolder._autoOrigSize.Equals(this._autoOrigSize) && ecoPropHolder._createLiteraVersion.Equals(this._createLiteraVersion) && ecoPropHolder._setLiteraForFullSostav.Equals(this._setLiteraForFullSostav) && ecoPropHolder.MoveAuthenticFiles.Equals(this.MoveAuthenticFiles) && ecoPropHolder.MaxDocNum.Equals(this.MaxDocNum) && ecoPropHolder.ReplaceEmptyDesByTemplate.Equals(this._replaceEmptyDes) && ecoPropHolder.HideOnCreation.Equals(this._hideOnCreation) && ecoPropHolder.ProhibitCustomReason.Equals(this._prohibitCustomReason) && ecoPropHolder.AskOnNewOrganizations.Equals(this.AskOnNewOrganizations) && ecoPropHolder.CheckObjectCreation.Equals(this.CheckObjectCreation) && ecoPropHolder.NoSlashInDPIDesign.Equals(this._noSlashInDPIDesign);
  }
}
