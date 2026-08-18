// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.EcoProperties
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.Interfaces;
using Intermech.Interfaces.ECO;
using Intermech.Localization;
using Intermech.Search;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.ECO.Client;

internal class EcoProperties : IEcoProperties
{
  private bool _autoMove = true;
  private bool _warnOnMove;
  private bool _writeComplect;
  private string _kiInventoryNumberTemplate = "{99999}#-{yy}";
  private bool _writeDesOnReplace;
  private bool _leaveOTDNumberForChange;
  private bool _autoCheckOut = true;
  private int _daysBeforeEnd;
  private bool _placeInvNum;
  private string _invNumAttr = "";
  private bool _hideHidden;
  private bool _autoOrigSize;
  private bool _createLiteraVersion;
  private bool _setLiteraForFullSostav;
  private bool _moveAuthenticFiles;
  private int _maxDocsAllowed;
  private bool _replaceEmptyDesignByTemplate;
  private bool _hideOnCreation;
  private bool _prohibitCustomReason;
  private bool _askOnNewOrganizations;
  private bool _checkObjectCreation;
  private bool _noSlashInDPIDesign;

  public void SaveToBase()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IECOServer)) is IECOServer customService))
        return;
      customService.SaveProps(sessionKeeper.Session.SessionGUID, this._autoMove, this._warnOnMove, this._writeComplect, this._kiInventoryNumberTemplate, this._writeDesOnReplace, this._leaveOTDNumberForChange, this._autoCheckOut, this._daysBeforeEnd, this._placeInvNum, this._invNumAttr, this._hideHidden, this._autoOrigSize, this._createLiteraVersion, this._setLiteraForFullSostav, this._moveAuthenticFiles, this._maxDocsAllowed, this._replaceEmptyDesignByTemplate, this._hideOnCreation, this._prohibitCustomReason, this._askOnNewOrganizations, this._checkObjectCreation, this._noSlashInDPIDesign);
    }
  }

  public static EcoProperties LoadFromBase()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IECOServer customService = sessionKeeper.Session.GetCustomService(typeof (IECOServer)) as IECOServer;
      EcoProperties ecoProperties = new EcoProperties();
      if (customService != null)
      {
        bool AutoMove;
        bool WarnInstead;
        bool WriteComplect;
        string kiTemplate;
        bool DesOnReplace;
        bool leaveOTD;
        bool autoCO;
        int daysBefore;
        bool placeInvNum;
        string invNumAttr;
        bool HideHidden;
        bool AutoOrigSize;
        bool createLiteraVersion;
        bool setLiteraFullSostav;
        bool moveAuthFiles;
        int maxDocsAllowed;
        bool replaceEmptyDesign;
        bool hideOnCreation;
        bool prohibitCustomReason;
        bool askOnNewOrganizations;
        bool checkObjectCreation;
        bool noSlashInDPI;
        customService.LoadProps(sessionKeeper.Session.SessionGUID, out AutoMove, out WarnInstead, out WriteComplect, out kiTemplate, out DesOnReplace, out leaveOTD, out autoCO, out daysBefore, out placeInvNum, out invNumAttr, out HideHidden, out AutoOrigSize, out createLiteraVersion, out setLiteraFullSostav, out moveAuthFiles, out maxDocsAllowed, out replaceEmptyDesign, out hideOnCreation, out prohibitCustomReason, out askOnNewOrganizations, out checkObjectCreation, out noSlashInDPI);
        ecoProperties._autoMove = AutoMove;
        ecoProperties._warnOnMove = WarnInstead;
        ecoProperties._writeComplect = WriteComplect;
        ecoProperties._kiInventoryNumberTemplate = kiTemplate;
        ecoProperties._writeDesOnReplace = DesOnReplace;
        ecoProperties._leaveOTDNumberForChange = leaveOTD;
        ecoProperties._autoCheckOut = autoCO;
        ecoProperties._daysBeforeEnd = daysBefore;
        ecoProperties._placeInvNum = placeInvNum;
        ecoProperties._invNumAttr = invNumAttr;
        ecoProperties._hideHidden = HideHidden;
        ecoProperties._autoOrigSize = AutoOrigSize;
        ecoProperties._createLiteraVersion = createLiteraVersion;
        ecoProperties._setLiteraForFullSostav = setLiteraFullSostav;
        ecoProperties.MoveAuthenticFiles = moveAuthFiles;
        ecoProperties._maxDocsAllowed = maxDocsAllowed;
        ecoProperties._replaceEmptyDesignByTemplate = replaceEmptyDesign;
        ecoProperties._hideOnCreation = hideOnCreation;
        ecoProperties._prohibitCustomReason = prohibitCustomReason;
        ecoProperties._askOnNewOrganizations = askOnNewOrganizations;
        ecoProperties._checkObjectCreation = checkObjectCreation;
        ecoProperties._noSlashInDPIDesign = noSlashInDPI;
      }
      return ecoProperties;
    }
  }

  private void OnChanged()
  {
    EventHandler changed = this.Changed;
    if (changed == null)
      return;
    changed((object) this, EventArgs.Empty);
  }

  [CustomDisplayName("Attribute.ECO.Client_12")]
  [TypeConverter(typeof (Intermech.Client.Core.YesNoBooleanConverter))]
  [IsAdmin]
  public bool AutoMoveObjects
  {
    get => this._autoMove;
    set
    {
      this._autoMove = value;
      this.OnChanged();
    }
  }

  [CustomDisplayName("Attribute.ECO.Client_13")]
  [TypeConverter(typeof (Intermech.Client.Core.YesNoBooleanConverter))]
  [IsAdmin]
  public bool WarnOnMove
  {
    get => this._warnOnMove;
    set
    {
      this._warnOnMove = value;
      this.OnChanged();
    }
  }

  [DisplayName("Шаблон обозначения КИ")]
  [Description("'#' - место для установки порядкового номера извещения")]
  [IsAdmin]
  public string KIInventoryNumberTemplate
  {
    get => this._kiInventoryNumberTemplate;
    set
    {
      this._kiInventoryNumberTemplate = value;
      this.OnChanged();
    }
  }

  [CustomDisplayName("Attribute.ECO.Client_14")]
  [TypeConverter(typeof (Intermech.Client.Core.YesNoBooleanConverter))]
  public bool WriteComplect
  {
    get => this._writeComplect;
    set
    {
      this._writeComplect = value;
      this.OnChanged();
    }
  }

  [CustomDisplayName("Attribute.ECO.Client_15")]
  [TypeConverter(typeof (Intermech.Client.Core.YesNoBooleanConverter))]
  [IsAdmin]
  public bool WriteDesOnReplace
  {
    get => this._writeDesOnReplace;
    set
    {
      this._writeDesOnReplace = value;
      this.OnChanged();
    }
  }

  [CustomDisplayName("Attribute.ECO.Client_19")]
  [TypeConverter(typeof (Intermech.Client.Core.YesNoBooleanConverter))]
  [IsAdmin]
  public bool LeaveOTDNumberForChange
  {
    get => this._leaveOTDNumberForChange;
    set
    {
      this._leaveOTDNumberForChange = value;
      this.OnChanged();
    }
  }

  [CustomDisplayName("Attribute.ECO.Client_20")]
  [TypeConverter(typeof (Intermech.Client.Core.YesNoBooleanConverter))]
  public bool AutoCheckOut
  {
    get => this._autoCheckOut;
    set
    {
      this._autoCheckOut = value;
      this.OnChanged();
    }
  }

  [CustomDisplayName("Attribute.ECO.Client_21")]
  [CustomDescription("Attribute.ECO.Client_22")]
  public int DaysBeforeEndTermWarning
  {
    get => this._daysBeforeEnd;
    set
    {
      this._daysBeforeEnd = value;
      this.OnChanged();
    }
  }

  [CustomDisplayName("Attribute.ECO.Client_27")]
  [TypeConverter(typeof (Intermech.Client.Core.YesNoBooleanConverter))]
  [CustomDescription("Attribute.ECO.Client_28")]
  public bool PlaceInvNum
  {
    get => this._placeInvNum;
    set
    {
      this._placeInvNum = value;
      this.OnChanged();
    }
  }

  [CustomDisplayName("Attribute.ECO.Client_41")]
  [TypeConverter(typeof (Intermech.Client.Core.YesNoBooleanConverter))]
  [CustomDescription("Attribute.ECO.Client_42")]
  public bool ReplaceEmptyDesignByTemplate
  {
    get => this._replaceEmptyDesignByTemplate;
    set
    {
      this._replaceEmptyDesignByTemplate = value;
      this.OnChanged();
    }
  }

  [CustomDisplayName("Attribute.ECO.Client_29")]
  [CustomDescription("Attribute.ECO.Client_30")]
  public string InvNumAttr
  {
    get => this._invNumAttr;
    set
    {
      this._invNumAttr = value;
      this.OnChanged();
    }
  }

  [CustomDisplayName("Attribute.ECO.Client_31")]
  [TypeConverter(typeof (Intermech.Client.Core.YesNoBooleanConverter))]
  [IsAdmin]
  public bool ShowHidden
  {
    get => !this._hideHidden;
    set
    {
      this._hideHidden = !value;
      this.OnChanged();
    }
  }

  [CustomDisplayName("Attribute.ECO.Client_32")]
  [TypeConverter(typeof (Intermech.Client.Core.YesNoBooleanConverter))]
  [IsAdmin]
  public bool AutoOrigSize
  {
    get => this._autoOrigSize;
    set
    {
      this._autoOrigSize = value;
      this.OnChanged();
    }
  }

  [CustomDisplayName("Attribute.ECO.Client_33")]
  [TypeConverter(typeof (Intermech.Client.Core.YesNoBooleanConverter))]
  [CustomDescription("Attribute.ECO.Client_34")]
  public bool CreateLiteraVersion
  {
    get => this._createLiteraVersion;
    set
    {
      this._createLiteraVersion = value;
      this.OnChanged();
    }
  }

  [CustomDisplayName("Attribute.ECO.Client_35")]
  [TypeConverter(typeof (Intermech.Client.Core.YesNoBooleanConverter))]
  [CustomDescription("Attribute.ECO.Client_36")]
  public bool SetLiteraForFullSostav
  {
    get => this._setLiteraForFullSostav;
    set
    {
      this._setLiteraForFullSostav = value;
      this.OnChanged();
    }
  }

  [CustomDisplayName("Attribute.ECO.Client_37")]
  [TypeConverter(typeof (Intermech.Client.Core.YesNoBooleanConverter))]
  [CustomDescription("Attribute.ECO.Client_38")]
  public bool MoveAuthenticFiles
  {
    get => this._moveAuthenticFiles;
    set
    {
      this._moveAuthenticFiles = value;
      this.OnChanged();
    }
  }

  [CustomDisplayName("Attribute.ECO.Client_39")]
  [CustomDescription("Attribute.ECO.Client_40")]
  public int MaxDocsAllowed
  {
    get => this._maxDocsAllowed;
    set
    {
      this._maxDocsAllowed = value;
      this.OnChanged();
    }
  }

  [CustomDisplayName("Attribute.ECO.Client_43")]
  [TypeConverter(typeof (Intermech.Client.Core.YesNoBooleanConverter))]
  [CustomDescription("Attribute.ECO.Client_44")]
  public bool HideOnCreation
  {
    get => this._hideOnCreation;
    set
    {
      this._hideOnCreation = value;
      this.OnChanged();
    }
  }

  [CustomDisplayName("Attribute.ECO.Client_45")]
  [TypeConverter(typeof (Intermech.Client.Core.YesNoBooleanConverter))]
  [CustomDescription("Attribute.ECO.Client_46")]
  public bool ProhibitCustomReason
  {
    get => this._prohibitCustomReason;
    set
    {
      this._prohibitCustomReason = value;
      this.OnChanged();
    }
  }

  [CustomDisplayName("Attribute.ECO.Client_47")]
  [TypeConverter(typeof (Intermech.Client.Core.YesNoBooleanConverter))]
  [CustomDescription("Attribute.ECO.Client_48")]
  public bool AskOnNewOrganizations
  {
    get => this._askOnNewOrganizations;
    set
    {
      this._askOnNewOrganizations = value;
      this.OnChanged();
    }
  }

  [CustomDisplayName("Attribute.ECO.Client_49")]
  [TypeConverter(typeof (Intermech.Client.Core.YesNoBooleanConverter))]
  [CustomDescription("Attribute.ECO.Client_50")]
  public bool CheckObjectCreation
  {
    get => this._checkObjectCreation;
    set
    {
      this._checkObjectCreation = value;
      this.OnChanged();
    }
  }

  [CustomDisplayName("Attribute.ECO.Client_51")]
  [TypeConverter(typeof (Intermech.Client.Core.YesNoBooleanConverter))]
  [CustomDescription("Attribute.ECO.Client_52")]
  public bool NoSlashInDPIDesign
  {
    get => this._noSlashInDPIDesign;
    set
    {
      this._noSlashInDPIDesign = value;
      this.OnChanged();
    }
  }

  public event EventHandler Changed;

  public override int GetHashCode() => base.GetHashCode();

  public override bool Equals(object obj)
  {
    if (!(obj is EcoProperties))
      return base.Equals(obj);
    EcoProperties ecoProperties = obj as EcoProperties;
    return ecoProperties._autoMove.Equals(this._autoMove) && ecoProperties._warnOnMove.Equals(this._warnOnMove) && ecoProperties._writeComplect.Equals(this._writeComplect) && ecoProperties._writeDesOnReplace.Equals(this._writeDesOnReplace) && ecoProperties._kiInventoryNumberTemplate.Equals(this._kiInventoryNumberTemplate) && ecoProperties._leaveOTDNumberForChange.Equals(this._leaveOTDNumberForChange) && ecoProperties._autoCheckOut.Equals(this._autoCheckOut) && ecoProperties._daysBeforeEnd.Equals(this._daysBeforeEnd) && ecoProperties._placeInvNum.Equals(this._placeInvNum) && ecoProperties._invNumAttr.Equals(this._invNumAttr) && ecoProperties._hideHidden.Equals(this._hideHidden) && ecoProperties._autoOrigSize.Equals(this._autoOrigSize) && ecoProperties._createLiteraVersion.Equals(this._createLiteraVersion) && ecoProperties._setLiteraForFullSostav.Equals(this._setLiteraForFullSostav) && ecoProperties._moveAuthenticFiles.Equals(this._moveAuthenticFiles) && ecoProperties._maxDocsAllowed.Equals(this._maxDocsAllowed) && ecoProperties._replaceEmptyDesignByTemplate.Equals(this._replaceEmptyDesignByTemplate) && ecoProperties._prohibitCustomReason.Equals(this._prohibitCustomReason) && ecoProperties._askOnNewOrganizations.Equals(this._askOnNewOrganizations) && ecoProperties._checkObjectCreation.Equals(this._checkObjectCreation) && ecoProperties._noSlashInDPIDesign.Equals(this._noSlashInDPIDesign);
  }
}
