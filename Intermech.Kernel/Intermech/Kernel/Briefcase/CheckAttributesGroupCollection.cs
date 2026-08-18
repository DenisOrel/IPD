// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.CheckAttributesGroupCollection
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using System;
using System.Data;


namespace Intermech.Kernel.Briefcase;

internal sealed class CheckAttributesGroupCollection : CheckCollection
{
  private IDBAttributesGroup _attrGroup;
  private DataSet _metaData;
  private int _briefGroupID;

  public CheckAttributesGroupCollection(
    UserSession session,
    DataSet metaData,
    IDBAttributesGroup attrGroup,
    int briefGroupID,
    string uniIdentifiler,
    CheckOptions options)
    : base(session, BriefcaseConsts.logAttributesGroupCategory, options)
  {
    this._attrGroup = attrGroup;
    this._metaData = metaData;
    this._briefGroupID = briefGroupID;
    this.UniIdentifiler = string.Format(BriefcaseConsts.logAttributesGroupAddUniIdentifiler, (object) uniIdentifiler);
  }

  public override void Compare()
  {
    if (this._attrGroup == null)
      return;
    foreach (DataRow dataRow1 in this._metaData.Tables["IMS_ATTR_IN_GROUPS"].Select($"{"F_GROUP_ID"} = {this._briefGroupID}"))
    {
      DataRow dataRow2 = this._metaData.Tables["IMS_ATTRIBUTES"].Rows.Find(dataRow1["F_ATTRIBUTE_ID"]);
      IDBAttributeType attrType;
      int attribute = (int) Helper.FindAttribute(this.session, out attrType, new Guid(Convert.ToString(dataRow2["F_GUID"])), Convert.ToString(dataRow2["F_ALIAS"]), Convert.ToString(dataRow2["F_NAME"]));
      if (attrType == null)
      {
        if (this.noneSynhronizingError)
          this.AddWarningToLog(BriefcaseConsts.logAttributeNotFound, Helper.ValueToLog(dataRow2["F_NAME"], dataRow2["F_GUID"], true), string.Empty);
      }
      else if (this._attrGroup.Attributes.GetAttributeType((object) attrType.AttributeID, false) == null)
      {
        string briefValue = Convert.ToString(dataRow2["F_NAME"]) == attrType.Name ? string.Format(BriefcaseConsts.logFormatName, (object) attrType.Name) : string.Format(BriefcaseConsts.logFormatGUID, (object) Convert.ToString(dataRow2["F_GUID"]));
        this.AddWarningToLog(BriefcaseConsts.logAttributeInAttributesGroupNotInGroup, briefValue, string.Empty);
      }
    }
  }
}
