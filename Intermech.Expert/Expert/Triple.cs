// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Triple
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using System.Xml;

#nullable disable
namespace Intermech.Expert;

public class Triple
{
  public string From;
  public string To;
  public string Result;

  public Triple(string F, string T, string R)
  {
    this.From = F;
    this.To = T;
    this.Result = R;
  }

  public Triple(string F, string R)
  {
    this.From = this.To = F;
    this.Result = R;
  }

  public Triple() => this.From = this.To = this.Result = "";

  public Triple(XmlNode node)
  {
    foreach (XmlNode childNode in node.ChildNodes)
    {
      if (childNode.NodeType == XmlNodeType.Element && childNode.Name == nameof (From))
        this.From = childNode.InnerText;
      else if (childNode.NodeType == XmlNodeType.Element && childNode.Name == nameof (To))
        this.To = childNode.InnerText;
      else if (childNode.NodeType == XmlNodeType.Element && childNode.Name == nameof (Result))
        this.Result = childNode.InnerText;
    }
  }
}
