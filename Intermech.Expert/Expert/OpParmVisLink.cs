// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.OpParmVisLink
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.Expert;

public class OpParmVisLink : OpParm
{
  public OpParmVisLink()
  {
  }

  public OpParmVisLink(ref OpParmData opData)
    : base(ref opData)
  {
    this.SetData(ref opData);
  }

  public override void SetData(ref OpParmData opData)
  {
  }

  public override void FillOpParmData(ref OpParmData opData) => opData.Clear();

  public override void WriteToXML(ref XmlTextWriter writer)
  {
  }

  public override void LoadFromXML(XmlNode node, int opTag)
  {
  }

  public override bool FixIdents(List<IdGuid> attrs, List<IdGuid> objs) => false;

  public override bool CollectGUIDs(List<IdGuid> attrs, List<IdGuid> objs) => false;

  public override bool FixIdentsComplete(IUserSession ius) => false;

  public override bool CollectExpObjInfo(ExpObjInfo eoi, IUserSession ius) => true;
}
