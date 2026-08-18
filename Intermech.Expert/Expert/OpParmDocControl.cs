// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.OpParmDocControl
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.Expert;

public class OpParmDocControl : OpParm
{
  public bool newList = true;
  public string listId = "";
  public string listName = "";
  public bool makeListCurrent;

  public OpParmDocControl()
  {
  }

  public OpParmDocControl(ref OpParmData opData)
    : base(ref opData)
  {
    this.SetData(ref opData);
  }

  public override void SetData(ref OpParmData opData)
  {
    this.newList = opData.b1;
    this.listId = opData.s1;
    this.listName = opData.s2;
    this.makeListCurrent = opData.b2;
  }

  public override void FillOpParmData(ref OpParmData opData)
  {
    opData.Clear();
    opData.b1 = this.newList;
    opData.s1 = this.listId;
    opData.s2 = this.listName;
    opData.b2 = this.makeListCurrent;
  }

  public override void WriteToXML(ref XmlTextWriter writer)
  {
    writer.WriteElementString("new-list", this.newList ? "yes" : "no");
    if (this.listId != "")
      writer.WriteElementString("list-id", this.listId);
    if (this.listName != "")
      writer.WriteElementString("list-name", this.listName);
    writer.WriteElementString("select-list", this.makeListCurrent ? "yes" : "no");
  }

  public override void LoadFromXML(XmlNode node, int opTag)
  {
    if (!node.HasChildNodes)
      return;
    foreach (XmlNode childNode in node.ChildNodes)
    {
      if (childNode.Name == "new-list")
        this.newList = childNode.InnerText == "yes";
      else if (childNode.Name == "select-list")
        this.makeListCurrent = childNode.InnerText == "yes";
      else if (childNode.Name == "list-id")
        this.listId = childNode.InnerText;
      else if (childNode.Name == "list-name")
        this.listName = childNode.InnerText;
    }
  }

  public override bool FixIdents(List<IdGuid> attrs, List<IdGuid> objs) => false;

  public override bool CollectGUIDs(List<IdGuid> attrs, List<IdGuid> objs) => false;

  public override bool FixIdentsComplete(IUserSession ius) => false;

  public override bool CollectExpObjInfo(ExpObjInfo eoi, IUserSession ius) => true;
}
