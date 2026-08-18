// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.ScriptTreeNode
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Xml;

#nullable disable
namespace Intermech.Expert;

public class ScriptTreeNode : IComparable<ScriptTreeNode>
{
  public ScriptTreeNode parent;
  public ModParm mod;
  public OpParm op;
  public ExpertScriptMod modTag = ExpertScriptMod.modUnknown;
  public ExpertScriptOp opTag = ExpertScriptOp.opUnknown;
  public string label = "";
  public ArrayList Items = new ArrayList();
  public static int NewId;
  public int Id;
  /// <summary>Был ли на этом узле восклицательный знак</summary>
  public bool ExclamationMarked;

  public ScriptTreeNode() => this.Id = ScriptTreeNode.NewId++;

  public bool IsCondSuppressed()
  {
    return this.parent != null && this.parent.opTag == ExpertScriptOp.opSelFolder;
  }

  public void LoadXML(XmlNode elem, int modTag, int opTag)
  {
    this.modTag = (ExpertScriptMod) modTag;
    this.opTag = (ExpertScriptOp) opTag;
    Type modNodeType = NodeData.GetModNodeType(modTag);
    if (modNodeType != (Type) null)
      this.mod = (ModParm) Activator.CreateInstance(modNodeType);
    Type opNodeType = NodeData.GetOpNodeType(opTag);
    if (opNodeType != (Type) null)
      this.op = (OpParm) Activator.CreateInstance(opNodeType);
    if (!elem.HasChildNodes)
      return;
    XmlNode childNode1 = elem.ChildNodes[0];
    if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "Mod-Parms")
      this.mod?.LoadFromXML(childNode1, modTag);
    if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "Op-Parms")
    {
      this.op.LoadFromXML(childNode1, opTag);
    }
    else
    {
      if (elem.ChildNodes.Count <= 1)
        return;
      XmlNode childNode2 = elem.ChildNodes[1];
      if (childNode2.NodeType != XmlNodeType.Element || !(childNode2.Name == "Op-Parms"))
        return;
      this.op.LoadFromXML(childNode2, opTag);
    }
  }

  public override int GetHashCode() => this.Id;

  public int CompareTo(ScriptTreeNode other) => this.Id.CompareTo(other.Id);

  public virtual bool HasExclamation() => this.label.StartsWith("!");

  public virtual string GetNodeDescr()
  {
    StringBuilder stringBuilder = new StringBuilder();
    int modTag = (int) this.modTag;
    stringBuilder.Append(NodeData.GetShortMod((int) this.modTag, this.mod));
    stringBuilder.Append("->");
    stringBuilder.Append(NodeData.GetShortOp((int) this.opTag, this.op));
    return stringBuilder.ToString();
  }

  public ScriptTreeNode(ExpertScriptMod modTag, ExpertScriptOp opTag, string label)
  {
    this.label = label;
    this.modTag = modTag;
    this.opTag = opTag;
    Type modNodeType = NodeData.GetModNodeType((int) modTag);
    if (modNodeType != (Type) null)
      this.mod = (ModParm) Activator.CreateInstance(modNodeType);
    Type opNodeType = NodeData.GetOpNodeType((int) opTag);
    if (!(opNodeType != (Type) null))
      return;
    this.op = (OpParm) Activator.CreateInstance(opNodeType);
  }

  public void WriteToXML(ref XmlTextWriter writer)
  {
    writer.WriteAttributeString("modTag", Convert.ToString((int) this.modTag));
    writer.WriteAttributeString("opTag", Convert.ToString((int) this.opTag));
    if (this.modTag >= ExpertScriptMod.modForEach && this.mod != null)
    {
      writer.WriteStartElement("Mod-Parms");
      this.mod.WriteToXML(ref writer);
      writer.WriteEndElement();
    }
    if (this.op == null)
      return;
    writer.WriteStartElement("Op-Parms");
    this.op.WriteToXML(ref writer);
    writer.WriteEndElement();
  }

  public static void WriteNodeToXML(ref XmlTextWriter writer, ScriptTreeNode d)
  {
    writer.WriteStartElement("node");
    writer.WriteAttributeString("label", d.label);
    d.WriteToXML(ref writer);
    if (d.Items.Count > 0)
    {
      for (int index = 0; index < d.Items.Count; ++index)
        ScriptTreeNode.WriteNodeToXML(ref writer, (ScriptTreeNode) d.Items[index]);
    }
    writer.WriteEndElement();
  }

  public static byte[] SaveToBuffer(ScriptTreeNode rootNode)
  {
    return ScriptTreeNode.SaveToBuffer((ScriptTreeNode[]) rootNode.Items.ToArray(typeof (ScriptTreeNode)));
  }

  public static byte[] SaveToBuffer(ScriptTreeNode[] elems)
  {
    using (MemoryStream w = new MemoryStream())
    {
      using (MemoryStream baseOutputStream = new MemoryStream())
      {
        XmlTextWriter writer = (XmlTextWriter) null;
        try
        {
          writer = new XmlTextWriter((Stream) w, Encoding.Unicode);
          writer.Formatting = Formatting.Indented;
          writer.WriteStartDocument();
          writer.WriteStartElement("ExpScript");
          writer.WriteAttributeString("xmlns", (string) null, "http://www.intermech.ru/Expert-System");
          for (int index = 0; index < elems.Length; ++index)
            ScriptTreeNode.WriteNodeToXML(ref writer, elems[index]);
          writer.WriteEndElement();
          writer.WriteEndDocument();
          writer.Flush();
          w.Position = 0L;
          Deflater deflater = new Deflater(3);
          using (DeflaterOutputStream deflaterOutputStream = new DeflaterOutputStream((Stream) baseOutputStream, deflater))
          {
            deflaterOutputStream.Write(w.GetBuffer(), 0, Convert.ToInt32(w.Length));
            deflaterOutputStream.Flush();
            deflaterOutputStream.Finish();
          }
        }
        finally
        {
          writer?.Close();
        }
        return baseOutputStream.ToArray();
      }
    }
  }
}
