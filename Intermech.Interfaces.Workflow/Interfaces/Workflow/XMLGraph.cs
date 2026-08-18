// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.XMLGraph
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Workflow;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Workflow;

public abstract class XMLGraph
{
  public readonly List<NameValueCollection> Nodes = new List<NameValueCollection>();
  public readonly List<NameValueCollection> Links = new List<NameValueCollection>();
  public VersionFlags VersionFlags;

  public void Load(Stream stream)
  {
    stream.Position = 0L;
    XmlTextReader xmlTextReader = new XmlTextReader(stream);
    xmlTextReader.ReadStartElement();
    this.VersionFlags = VersionFlags.None;
    int content = (int) xmlTextReader.MoveToContent();
    if (xmlTextReader.NodeType == XmlNodeType.Element)
    {
      if (xmlTextReader.MoveToAttribute("v") && xmlTextReader.ReadAttributeValue())
        this.VersionFlags = (VersionFlags) Convert.ToInt32(xmlTextReader.Value);
      xmlTextReader.Read();
    }
    xmlTextReader.ReadStartElement("Nodes");
    NameValueCollection nameValueCollection = (NameValueCollection) null;
    string name = "";
    while (xmlTextReader.Read())
    {
      if (xmlTextReader.NodeType == XmlNodeType.Element)
        name = xmlTextReader.Name;
      if (xmlTextReader.Name == "Node" || xmlTextReader.Name == "Link")
      {
        if (xmlTextReader.NodeType == XmlNodeType.Element)
        {
          nameValueCollection = new NameValueCollection();
          if (xmlTextReader.Name == "Node")
          {
            this.Nodes.Add(nameValueCollection);
            xmlTextReader.MoveToAttribute("id");
            if (xmlTextReader.ReadAttributeValue())
              nameValueCollection["id"] = xmlTextReader.Value;
          }
          else if (xmlTextReader.Name == "Link")
          {
            this.Links.Add(nameValueCollection);
            xmlTextReader.MoveToAttribute("id");
            if (xmlTextReader.ReadAttributeValue())
              nameValueCollection["id"] = xmlTextReader.Value;
          }
        }
        else if (xmlTextReader.NodeType == XmlNodeType.EndElement)
          nameValueCollection = (NameValueCollection) null;
      }
      if (xmlTextReader.NodeType == XmlNodeType.Text && nameValueCollection != null)
        nameValueCollection[name] = xmlTextReader.Value;
    }
  }

  private void WriteList(XmlTextWriter writer, string itemName, List<NameValueCollection> list)
  {
    for (int index = 0; index < list.Count; ++index)
    {
      writer.WriteStartElement(itemName);
      writer.WriteAttributeString("id", (index + 1).ToString());
      NameValueCollection nameValueCollection = list[index];
      foreach (string key in nameValueCollection.Keys)
      {
        switch (key)
        {
          case "Node":
          case "id":
          case "ObjectID":
            continue;
          case "Guid":
            if ((this.VersionFlags & VersionFlags.IncludeObjectGuids) == VersionFlags.None)
              continue;
            break;
        }
        writer.WriteStartElement(key);
        writer.WriteString(nameValueCollection[key]);
        writer.WriteEndElement();
      }
      writer.WriteEndElement();
    }
  }

  public void Save(Stream stream)
  {
    XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8);
    writer.Formatting = Formatting.Indented;
    writer.WriteStartElement("Intermech.Workflow");
    writer.WriteStartElement("Process");
    writer.WriteAttributeString("v", Convert.ToInt32((object) this.VersionFlags).ToString());
    writer.WriteStartElement("Nodes");
    this.WriteList(writer, "Node", this.Nodes);
    writer.WriteEndElement();
    writer.WriteStartElement("Links");
    this.WriteList(writer, "Link", this.Links);
    writer.WriteEndElement();
    writer.WriteEndElement();
    writer.WriteEndElement();
    writer.Flush();
  }
}
