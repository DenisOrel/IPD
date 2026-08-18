// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Server.ExpertScriptParms
// Assembly: Intermech.Expert.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8532AAAD-1C72-4C22-AA34-A49C95D2B71F
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Expert.Server.dll

using System;
using System.Xml;

#nullable disable
namespace Intermech.Expert.Server;

public class ExpertScriptParms
{
  public Guid objTypeGuid = Guid.Empty;
  public string objTypeName = "";
  public string docName = "";
  public bool useTraceInfo;
  public bool allNodeObjects;
  public string allZamens = "C";
  public bool coWorker;
  public bool checkOut;

  public ExpertScriptParms()
  {
  }

  public ExpertScriptParms(XmlNode n)
    : this()
  {
    this.LoadFromXml(n);
  }

  public void LoadFromXml(XmlNode n)
  {
    foreach (XmlNode childNode in n.ChildNodes)
    {
      if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "DocType_GUID")
        this.objTypeGuid = new Guid(childNode.InnerText);
      else if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "DocType_Name")
        this.objTypeName = childNode.InnerText;
      else if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "DocName")
        this.docName = childNode.InnerText;
      else if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "show_info")
        this.useTraceInfo = childNode.InnerText == "Y";
      else if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "all_node")
        this.allNodeObjects = childNode.InnerText == "Y";
      else if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "all_zamens")
        this.allZamens = childNode.InnerText;
      else if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "coWorker_Template")
        this.coWorker = childNode.InnerText == "Y";
      else if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "coWorker_Template")
        this.checkOut = childNode.InnerText == "Y";
    }
  }

  public void WriteToXml(XmlTextWriter writer)
  {
    writer.WriteStartElement("DocParms");
    writer.WriteElementString("DocType_GUID", (string) null, this.objTypeGuid.ToString());
    writer.WriteElementString("DocType_Name", (string) null, this.objTypeName);
    writer.WriteElementString("DocName", (string) null, this.docName);
    writer.WriteElementString("show_info", (string) null, this.useTraceInfo ? "Y" : "N");
    writer.WriteElementString("all_node", (string) null, this.allNodeObjects ? "Y" : "N");
    writer.WriteElementString("all_zamens", (string) null, this.allZamens);
    writer.WriteEndElement();
  }
}
