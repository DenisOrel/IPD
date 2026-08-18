// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.ExpertTraceNode
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Xml;

#nullable disable
namespace Intermech.Expert;

/// <summary>Узел дерева отладочной информации экспертной системы</summary>
public class ExpertTraceNode : ISaveLoad, ISerializable, ICloneable
{
  public string Name;
  public Dictionary<string, string> attribs;
  public List<long> objList;
  public string text = "";
  public List<ExpertTraceNode> childs;

  public ExpertTraceNode()
  {
  }

  public ExpertTraceNode(string N) => this.Name = N;

  public ExpertTraceNode(string N, string T)
    : this(N)
  {
    this.text = T;
  }

  public string this[string attrName]
  {
    get
    {
      return this.attribs == null || !this.attribs.ContainsKey(attrName) ? "" : this.attribs[attrName];
    }
    set
    {
      if (this.attribs == null)
        this.attribs = new Dictionary<string, string>();
      this.attribs[attrName] = value;
    }
  }

  public void Load(BinaryReader br, int Version)
  {
    this.Name = br.ReadString();
    int num1 = br.ReadInt32();
    if (num1 > 0 && this.attribs == null)
      this.attribs = new Dictionary<string, string>();
    for (int index = 0; index < num1; ++index)
      this.attribs.Add(br.ReadString(), br.ReadString());
    int num2 = br.ReadInt32();
    if (num2 > 0 && this.objList == null)
      this.objList = new List<long>();
    for (int index = 0; index < num2; ++index)
      this.objList.Add(br.ReadInt64());
    this.text = br.ReadString();
    int num3 = br.ReadInt32();
    if (num3 > 0 && this.childs == null)
      this.childs = new List<ExpertTraceNode>();
    for (int index = 0; index < num3; ++index)
    {
      ExpertTraceNode expertTraceNode = new ExpertTraceNode();
      expertTraceNode.Load(br, Version);
      this.childs.Add(expertTraceNode);
    }
  }

  public void Save(BinaryWriter bw) => this.SaveAsVer(bw, ExpertConsts.TraceVersion);

  public void SaveAsVer(BinaryWriter bw, int Version)
  {
    bw.Write(this.Name);
    int count1 = this.attribs != null ? this.attribs.Count : 0;
    bw.Write(count1);
    if (this.attribs != null)
    {
      foreach (string key in this.attribs.Keys)
      {
        bw.Write(key);
        bw.Write(this.attribs[key]);
      }
    }
    int count2 = this.objList != null ? this.objList.Count : 0;
    bw.Write(count2);
    if (this.objList != null)
    {
      foreach (long num in this.objList)
        bw.Write(num);
    }
    bw.Write(this.text);
    int count3 = this.childs != null ? this.childs.Count : 0;
    bw.Write(count3);
    if (this.childs == null)
      return;
    foreach (ExpertTraceNode child in this.childs)
      child.SaveAsVer(bw, Version);
  }

  protected ExpertTraceNode(SerializationInfo info, StreamingContext context)
  {
    Dictionary<string, object> paramsValue = SerializationInfoHelper.GetParamsValue(info);
    int BaseId = 0;
    this.LoadObjectData(paramsValue, ref BaseId);
  }

  [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    int BaseId = 0;
    this.SaveObjectData(info, context, ref BaseId);
  }

  private void SaveObjectData(SerializationInfo info, StreamingContext context, ref int BaseId)
  {
    info.AddValue("Name" + BaseId.ToString(), (object) this.Name);
    ++BaseId;
    int count1 = this.attribs != null ? this.attribs.Count : 0;
    info.AddValue("AT_" + BaseId.ToString(), count1);
    ++BaseId;
    if (count1 != 0)
    {
      foreach (string key in this.attribs.Keys)
      {
        info.AddValue("AT_K" + BaseId.ToString(), (object) key);
        info.AddValue("AT_V" + BaseId.ToString(), (object) this.attribs[key]);
        ++BaseId;
      }
    }
    int count2 = this.attribs != null ? this.objList.Count : 0;
    info.AddValue("OL_N" + BaseId.ToString(), count2);
    ++BaseId;
    if (count2 != 0)
    {
      foreach (long num in this.objList)
      {
        info.AddValue("OL_" + BaseId.ToString(), num);
        ++BaseId;
      }
    }
    info.AddValue("TX_" + BaseId.ToString(), (object) this.text);
    ++BaseId;
    int count3 = this.childs != null ? this.childs.Count : 0;
    info.AddValue("CC_" + BaseId.ToString(), count3);
    ++BaseId;
    foreach (ExpertTraceNode child in this.childs)
      child.SaveObjectData(info, context, ref BaseId);
  }

  private void LoadObjectData(Dictionary<string, object> sinfo, ref int BaseId)
  {
    this.Name = Convert.ToString(sinfo["Name" + BaseId.ToString()]);
    ++BaseId;
    int int32_1 = Convert.ToInt32(sinfo["AT_" + BaseId.ToString()]);
    ++BaseId;
    if (int32_1 != 0)
    {
      this.attribs = new Dictionary<string, string>();
      for (int index = 0; index < int32_1; ++index)
      {
        string key = Convert.ToString(sinfo["AT_K" + BaseId.ToString()]);
        string str = Convert.ToString(sinfo["AT_V" + BaseId.ToString()]);
        ++BaseId;
        this.attribs.Add(key, str);
      }
    }
    int int32_2 = Convert.ToInt32(sinfo["OL_N" + BaseId.ToString()]);
    ++BaseId;
    if (int32_2 != 0)
    {
      this.objList = new List<long>();
      for (int index = 0; index < int32_2; ++index)
      {
        long int64 = Convert.ToInt64(sinfo["OL_" + BaseId.ToString()]);
        ++BaseId;
        this.objList.Add(int64);
      }
    }
    this.text = Convert.ToString(sinfo["TX_" + BaseId.ToString()]);
    ++BaseId;
    int int32_3 = Convert.ToInt32(sinfo["CC_" + BaseId.ToString()]);
    ++BaseId;
    if (int32_3 <= 0)
      return;
    this.childs = new List<ExpertTraceNode>();
    for (int index = 0; index < int32_3; ++index)
    {
      ExpertTraceNode expertTraceNode = new ExpertTraceNode();
      expertTraceNode.LoadObjectData(sinfo, ref BaseId);
      this.childs.Add(expertTraceNode);
    }
  }

  public object Clone()
  {
    ExpertTraceNode expertTraceNode = new ExpertTraceNode(this.Name);
    if (this.attribs != null)
    {
      expertTraceNode.attribs = new Dictionary<string, string>();
      foreach (string key in this.attribs.Keys)
        expertTraceNode.attribs[key] = this.attribs[key];
    }
    if (this.objList != null)
    {
      expertTraceNode.objList = new List<long>();
      foreach (long num in this.objList)
        expertTraceNode.objList.Add(num);
    }
    expertTraceNode.text = this.text;
    if (this.childs != null)
    {
      expertTraceNode.childs = new List<ExpertTraceNode>();
      foreach (ExpertTraceNode child in this.childs)
        expertTraceNode.childs.Add((ExpertTraceNode) child.Clone());
    }
    return (object) expertTraceNode;
  }

  public void WriteToXML(ref XmlTextWriter writer) => this.WriteToXML(ref writer, "TraceNode");

  public void WriteToXML(ref XmlTextWriter writer, string root)
  {
    writer.WriteStartElement(root);
    writer.WriteElementString("Name", this.Name);
    writer.WriteAttributeString("text", this.text);
    if (this.attribs != null)
    {
      writer.WriteStartElement("attribs");
      foreach (string key in this.attribs.Keys)
      {
        writer.WriteStartElement("attr");
        writer.WriteAttributeString("Name", key);
        writer.WriteAttributeString("Value", this.attribs[key]);
        writer.WriteEndElement();
      }
      writer.WriteEndElement();
    }
    if (this.objList != null)
    {
      writer.WriteStartElement("objList");
      foreach (long num in this.objList)
      {
        writer.WriteStartElement("attr");
        writer.WriteAttributeString("Id", Convert.ToString(num));
        writer.WriteEndElement();
      }
      writer.WriteEndElement();
    }
    if (this.childs != null)
    {
      writer.WriteStartElement("Childs");
      foreach (ExpertTraceNode child in this.childs)
        child.WriteToXML(ref writer, root);
      writer.WriteEndElement();
    }
    writer.WriteEndElement();
  }

  public ExpertTraceNode(XmlNode node)
  {
    if (node.NodeType != XmlNodeType.Element || !node.Name.StartsWith("TraceNode"))
      throw new AbortException("Wrong XML node for ExpertTraceNode!");
    if (!node.HasChildNodes)
      return;
    foreach (XmlNode childNode in node.ChildNodes)
    {
      if (childNode.NodeType == XmlNodeType.Element && childNode.Name == nameof (Name))
        this.Name = childNode.InnerText;
      else if (childNode.NodeType == XmlNodeType.Element && childNode.Name == nameof (text))
        this.text = childNode.InnerText;
      else if (childNode.NodeType == XmlNodeType.Element && childNode.Name == nameof (attribs))
      {
        this.attribs = new Dictionary<string, string>();
        if (childNode.HasChildNodes)
        {
          for (int i = 0; i < childNode.ChildNodes.Count; ++i)
          {
            if (childNode.ChildNodes[i].NodeType == XmlNodeType.Element)
            {
              int num = childNode.ChildNodes[i].Name == "attr" ? 1 : 0;
            }
          }
        }
      }
    }
  }
}
