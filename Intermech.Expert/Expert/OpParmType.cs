// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.OpParmType
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.Expert;

/// <summary>Used to search attribs, starting from the type</summary>
public class OpParmType : OpParm
{
  public string objTypeGUID = "";
  public string objTypeText = "";
  public TempFormula cond;

  public OpParmType()
  {
  }

  public OpParmType(ref OpParmData opData)
    : base(ref opData)
  {
    this.SetData(ref opData);
  }

  public override void SetData(ref OpParmData opData)
  {
    this.objTypeGUID = opData.s1;
    this.objTypeText = opData.s2;
    if (opData.tf.Count == 0)
      this.cond = (TempFormula) null;
    else
      this.cond = (TempFormula) opData.tf.Clone();
  }

  public override void FillOpParmData(ref OpParmData opData)
  {
    opData.Clear();
    opData.s1 = this.objTypeGUID;
    opData.s2 = this.objTypeText;
    if (this.cond == null)
      return;
    opData.tf = (TempFormula) this.cond.Clone();
  }

  public override void WriteToXML(ref XmlTextWriter writer)
  {
    writer.WriteElementString("GUID", this.objTypeGUID);
    writer.WriteElementString("Text", this.objTypeText);
    if (this.cond == null)
      return;
    this.cond.FillObjectLinks();
    this.cond.WriteToXML(ref writer);
  }

  public override void LoadFromXML(XmlNode node, int opTag)
  {
    if (!node.HasChildNodes)
      return;
    foreach (XmlNode childNode in node.ChildNodes)
    {
      if (childNode.Name == "GUID")
        this.objTypeGUID = childNode.InnerText;
      else if (childNode.Name == "Text")
        this.objTypeText = childNode.InnerText;
      else if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "Formula")
        this.cond = new TempFormula(childNode);
    }
  }

  public override bool FixIdents(List<IdGuid> attrs, List<IdGuid> objs)
  {
    return this.cond != null && this.cond.FixIDs(attrs, objs);
  }

  public override bool CollectGUIDs(List<IdGuid> attrs, List<IdGuid> objs)
  {
    return this.cond != null && this.cond.CollectGUIDs(attrs, objs);
  }

  public override bool FixIdentsComplete(IUserSession ius)
  {
    return this.cond != null && this.cond.FixIdentsComplete(ius);
  }

  public override bool CollectExpObjInfo(ExpObjInfo eoi, IUserSession ius)
  {
    bool flag = eoi.AddObjType(this.objTypeGUID);
    if (this.cond != null)
      flag = flag && this.cond.CollectExpObjInfo(eoi, ius);
    return flag;
  }
}
