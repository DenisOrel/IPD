// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.OpParmVersionRule
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.Expert;

/// <summary>Set version rule for all subelements</summary>
public class OpParmVersionRule : OpParm
{
  public long ruleId;
  public string ruleGuid = "";
  public string ruleCapt = "";
  public string showZamens = "C";

  public OpParmVersionRule()
  {
  }

  public OpParmVersionRule(ref OpParmData opData)
    : base(ref opData)
  {
    this.SetData(ref opData);
  }

  public override void SetData(ref OpParmData opData)
  {
    this.ruleId = Convert.ToInt64(opData.exID);
    this.ruleGuid = opData.s1;
    this.ruleCapt = opData.s2;
    this.showZamens = opData.s3;
  }

  public override void FillOpParmData(ref OpParmData opData)
  {
    opData.Clear();
    opData.exID = this.ruleId;
    opData.s1 = this.ruleGuid;
    opData.s2 = this.ruleCapt;
    opData.s3 = this.showZamens;
  }

  public override void WriteToXML(ref XmlTextWriter writer)
  {
    writer.WriteElementString("ID", Convert.ToString(this.ruleId));
    if (this.ruleGuid != "")
      writer.WriteElementString("Guid", this.ruleGuid);
    if (this.ruleCapt != "")
      writer.WriteElementString("Capt", this.ruleCapt);
    writer.WriteElementString("ShowZamens", this.showZamens);
  }

  public override void LoadFromXML(XmlNode node, int opTag)
  {
    if (!node.HasChildNodes)
      return;
    foreach (XmlNode childNode in node.ChildNodes)
    {
      if (childNode.Name == "ID")
        this.ruleId = Convert.ToInt64(childNode.InnerText);
      else if (childNode.Name == "Guid")
        this.ruleGuid = childNode.InnerText;
      else if (childNode.Name == "Capt")
        this.ruleCapt = childNode.InnerText;
      else if (childNode.Name == "ShowZamens")
        this.showZamens = childNode.InnerText;
    }
  }

  public override bool FixIdents(List<IdGuid> attrs, List<IdGuid> objs) => false;

  public override bool CollectGUIDs(List<IdGuid> attrs, List<IdGuid> objs) => false;

  public override bool FixIdentsComplete(IUserSession ius) => false;

  public override bool CollectExpObjInfo(ExpObjInfo eoi, IUserSession ius) => true;
}
