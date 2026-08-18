// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.CheckAttribute4TypeCollection`1
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel.Briefcase;

internal abstract class CheckAttribute4TypeCollection<T> : CheckCollection where T : IDBAttributableType
{
  protected List<int> briefAttributes;
  protected T type;

  public CheckAttribute4TypeCollection(
    UserSession session,
    List<int> briefAttributes,
    T type,
    string category,
    string uniIdentifiler,
    CheckOptions options)
    : base(session, category, options)
  {
    this.briefAttributes = briefAttributes;
    this.type = type;
    this.UniIdentifiler = uniIdentifiler;
  }

  public override void Compare()
  {
    DataTable dataTable = this.type.Attributes.Select(string.Empty);
    CheckArraysResult checkArraysResult = new CheckArraysResult(new string[1]
    {
      "notFoundInBriefObjType"
    });
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      int int32 = Convert.ToInt32(row["F_ATTRIBUTE_ID"]);
      if (this.briefAttributes.IndexOf(int32) < 0)
      {
        IDBAttributeType attributeType = this.session.GetAttributeType(int32, false);
        checkArraysResult.Add("notFoundInBriefObjType", (object) Helper.ValueToLog((object) attributeType.Name, (object) (attributeType as IDBGuid).GUID, true));
      }
    }
    if (checkArraysResult["notFoundInBriefObjType"].Count <= 0)
      return;
    this.AddInfoInLog(this.noneSynhronizingError ? CheckMetadataLogItemType.Error : CheckMetadataLogItemType.Warning, BriefcaseConsts.logRelationTypeAttributeNotPresentInBriefCase, string.Empty, checkArraysResult.ToString("notFoundInBriefObjType"));
  }
}
