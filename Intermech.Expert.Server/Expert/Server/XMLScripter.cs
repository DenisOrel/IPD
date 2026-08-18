// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Server.XMLScripter
// Assembly: Intermech.Expert.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8532AAAD-1C72-4C22-AA34-A49C95D2B71F
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Expert.Server.dll

using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System;
using System.IO;
using System.Text;
using System.Xml;

#nullable disable
namespace Intermech.Expert.Server;

public class XMLScripter
{
  public static ScriptTreeNode LoadScript(byte[] zipScr, out ExpertScriptParms parms)
  {
    parms = (ExpertScriptParms) null;
    XmlDocument xDoc = ZlibHelper.UnpackXmlBuffer(zipScr);
    ScriptTreeNode scriptTreeNode = ExpertServer.LoadScriptTree(xDoc);
    XmlElement documentElement = xDoc.DocumentElement;
    if (documentElement.HasChildNodes)
    {
      foreach (XmlNode childNode in documentElement.ChildNodes)
      {
        if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "DocParms")
          parms = new ExpertScriptParms(childNode);
      }
    }
    return scriptTreeNode;
  }

  public static ScriptTreeNode LoadScript(byte[] zipScr)
  {
    return ExpertServer.LoadScriptTree(ZlibHelper.UnpackXmlBuffer(zipScr));
  }

  public static byte[] SaveScript(ScriptTreeNode root, ExpertScriptParms parms = null)
  {
    XmlTextWriter writer = (XmlTextWriter) null;
    try
    {
      MemoryStream w = new MemoryStream();
      writer = new XmlTextWriter((Stream) w, Encoding.UTF8);
      writer.Formatting = Formatting.Indented;
      writer.WriteStartDocument();
      writer.WriteStartElement("WholeScript");
      parms?.WriteToXml(writer);
      writer.WriteStartElement("ExpScript");
      writer.WriteAttributeString("xmlns", (string) null, "http://www.intermech.ru/Expert-System");
      for (int index = 0; index < root.Items.Count; ++index)
        ScriptTreeNode.WriteNodeToXML(ref writer, (ScriptTreeNode) root.Items[index]);
      writer.WriteEndElement();
      writer.WriteEndElement();
      writer.WriteEndDocument();
      writer.Flush();
      w.Position = 0L;
      MemoryStream baseOutputStream = new MemoryStream();
      DeflaterOutputStream deflaterOutputStream = new DeflaterOutputStream((Stream) baseOutputStream, new Deflater(3));
      deflaterOutputStream.Write(w.GetBuffer(), 0, Convert.ToInt32(w.Length));
      deflaterOutputStream.Flush();
      deflaterOutputStream.Finish();
      return baseOutputStream.ToArray();
    }
    finally
    {
      writer?.Close();
    }
  }
}
