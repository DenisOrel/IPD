// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Server.ImDocumentConverter
// Assembly: Intermech.Document.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: F658B856-4DF9-439D-954C-249051C853FF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Document.Server.dll

using Intermech.Interfaces;
using Intermech.IO;
using Intermech.Kernel.GlobalIndex;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

#nullable disable
namespace Intermech.Document.Server;

internal class ImDocumentConverter : CustomFileConverter
{
  public override string Caption => "Конвертер документов Редактора документов";

  public override string[] SupportedFileExtensions
  {
    get
    {
      return new string[4]
      {
        ".IMDX",
        ".SPX",
        ".IDCX",
        ".REVX"
      };
    }
  }

  public override string GetPlainText(IDBAttribute attribute)
  {
    ImDocumentServerPlugin.Instance.ImDocumentConfig.LoadConfiguration(attribute.Session);
    IBlobReader blobReader = attribute as IBlobReader;
    BlobInformation blobInformation = blobReader.OpenBlob(0);
    if (blobInformation.PackedFileSize == 0L)
    {
      blobReader.CloseBlob();
      return string.Empty;
    }
    byte[] buffer = blobReader.ReadDataBlock();
    if (blobInformation.ArcMethod == ArcMethods.ZLibPacked)
    {
      Stream inStream = (Stream) new MemoryStream(buffer);
      inStream.Position = 0L;
      IPackedStream service = ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true);
      try
      {
        ImChunkedStream imChunkedStream = new ImChunkedStream();
        try
        {
          service.UnpackStream((Stream) imChunkedStream, inStream);
          return this.Read((Stream) imChunkedStream);
        }
        finally
        {
          imChunkedStream.Close();
        }
      }
      finally
      {
        inStream.Close();
      }
    }
    else
    {
      Stream strm = (Stream) new MemoryStream(buffer);
      try
      {
        return this.Read(strm);
      }
      finally
      {
        strm.Close();
      }
    }
  }

  private string Read(Stream strm)
  {
    strm.Position = 0L;
    string empty = string.Empty;
    XmlDocument node = new XmlDocument();
    try
    {
      node.Load(strm);
      List<XmlNode> nodes = new List<XmlNode>();
      StringBuilder sb = new StringBuilder();
      this.FindText((XmlNode) node, ref nodes, ref sb);
      return sb.ToString();
    }
    catch
    {
      throw;
    }
  }

  private void FindText(XmlNode node, ref List<XmlNode> nodes, ref StringBuilder sb)
  {
    XmlNodeList xmlNodeList = node.SelectNodes("Text");
    if (xmlNodeList != null)
    {
      foreach (XmlNode xmlNode in xmlNodeList)
      {
        nodes.Add(xmlNode);
        string innerText = xmlNode.InnerText;
        if (innerText != null && innerText != string.Empty)
        {
          sb.Append(" ; ");
          sb.Append(innerText);
        }
      }
    }
    node.SelectNodes("RefText");
    if (xmlNodeList != null)
    {
      foreach (XmlNode xmlNode in xmlNodeList)
      {
        nodes.Add(xmlNode);
        string innerText = xmlNode.InnerText;
        if (innerText != null && innerText != string.Empty)
        {
          sb.Append(" ; ");
          sb.Append(innerText);
        }
      }
    }
    if (ImDocumentServerPlugin.Instance.ImDocumentConfig.NotIndexTemplateWords && node.Name == "Template" || node.ChildNodes == null)
      return;
    foreach (XmlNode childNode in node.ChildNodes)
      this.FindText(childNode, ref nodes, ref sb);
  }
}
