// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.XMLRemarkRecordHandler
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.IO;
using System;
using System.IO;
using System.Xml;


namespace Intermech.Kernel.Briefcase;

internal sealed class XMLRemarkRecordHandler : RemarkRecordHandler
{
  public XMLRemarkRecordHandler()
    : base(MetaDataHelper.GetAttributeTypeID("cad0004b-306c-11d8-b4e9-00304f19f545"))
  {
  }

  public override bool HandleRecord(RemarkRecord record, IDBObject obj)
  {
    if (record.AttributeId != this.attributeID || !this.IsXMLRemarkFile((string) record.StringValue))
      return false;
    this.records.Add(record);
    return true;
  }

  public override void OnComplete(IDBObject obj)
  {
    IDBAttribute attributeByGuid = obj.GetAttributeByGuid(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545"), false);
    if (attributeByGuid == null)
      return;
    foreach (RemarkRecord record in this.records)
    {
      string stringValue = (string) record.StringValue;
      XmlDocument importRemarks = this.GetImportRemarks(record);
      XmlDocument baseRemark = this.GetBaseRemark(attributeByGuid, stringValue);
      if (baseRemark != null)
      {
        this.MergeRemarks(baseRemark, importRemarks);
        this.SaveRemark(attributeByGuid, baseRemark, stringValue, record);
      }
      else
        this.SaveRemark(attributeByGuid, importRemarks, stringValue, record);
    }
  }

  private void MergeRemarks(XmlDocument document, XmlDocument importRemarks)
  {
    XmlNodeList xmlNodeList1 = document.SelectNodes("/im_redlining");
    foreach (XmlNode selectNode in importRemarks.SelectNodes("/im_redlining/im_redline"))
    {
      string str = selectNode.Attributes["id"].Value;
      DateTime dateTime1 = DateTimeHelper.ToDateTime(selectNode.Attributes["date"].Value);
      XmlNodeList xmlNodeList2 = document.SelectNodes($"/im_redlining/im_redline[@id='{selectNode.Attributes["id"].Value}']");
      if (xmlNodeList2.Count == 0)
        xmlNodeList1[0].AppendChild(document.ImportNode(selectNode, true));
      else if (xmlNodeList2.Count == 1)
      {
        DateTime dateTime2 = DateTimeHelper.ToDateTime(xmlNodeList2[0].Attributes["date"].Value);
        if (dateTime1 >= dateTime2)
        {
          foreach (XmlAttribute attribute in (XmlNamedNodeMap) selectNode.Attributes)
            (xmlNodeList2[0].Attributes[attribute.Name] ?? xmlNodeList2[0].Attributes.Append(document.CreateAttribute(attribute.Name))).Value = attribute.Value;
          xmlNodeList2[0].InnerXml = selectNode.InnerXml;
        }
      }
    }
  }

  private XmlDocument GetImportRemarks(RemarkRecord record)
  {
    XmlDocument importRemarks = new XmlDocument();
    using (FileStream inStream = File.OpenRead(record.Path2File))
    {
      if (Convert.ToInt32(record.ArcMethod) == 1)
      {
        using (ImChunkedStream imChunkedStream = new ImChunkedStream())
        {
          ZLibStreamHelper.UnpackStream((Stream) inStream, (Stream) imChunkedStream);
          imChunkedStream.Position = 0L;
          importRemarks.Load((Stream) imChunkedStream);
        }
      }
      else
        importRemarks.Load((Stream) inStream);
    }
    return importRemarks;
  }

  private void SaveRemark(
    IDBAttribute attrFile,
    XmlDocument document,
    string fileName,
    RemarkRecord record)
  {
    bool flag = false;
    if (attrFile.Values != null)
    {
      for (int index = 0; index < attrFile.Values.Length; ++index)
      {
        attrFile.Index = index;
        if (attrFile.AsString == fileName)
        {
          record.InlistId = index;
          this.SaveToBase((IBlobWriter) attrFile, document, fileName, record);
          flag = true;
        }
      }
    }
    if (flag)
      return;
    int num = attrFile.AddValue(record.FileType);
    attrFile.Index = num;
    record.InlistId = num;
    this.SaveToBase((IBlobWriter) attrFile, document, fileName, record);
  }

  private void SaveToBase(
    IBlobWriter writer,
    XmlDocument document,
    string fileName,
    RemarkRecord record)
  {
    using (ImChunkedStream imChunkedStream = new ImChunkedStream())
    {
      document.Save((Stream) imChunkedStream);
      imChunkedStream.Position = 0L;
      using (ImChunkedStream outStream = new ImChunkedStream())
      {
        ZLibStreamHelper.PackStream((Stream) imChunkedStream, ZLibCompressLevels.LevelMax, (Stream) outStream);
        long fileAuthor = record.FileAuthor is long ? (long) record.FileAuthor : 0L;
        if (!writer.OpenBlob(new BlobInformation(imChunkedStream.Length, outStream.Length, DateTime.Now, fileName, ArcMethods.ZLibPacked, string.Empty, FileTypes.ftRedlining, fileAuthor), false))
          return;
        writer.WriteDataBlock(outStream.ToArray());
      }
    }
  }

  private XmlDocument GetBaseRemark(IDBAttribute attrFile, string fileName)
  {
    for (int index = 0; index < attrFile.ValuesCount; ++index)
    {
      attrFile.Index = index;
      if (attrFile.AsString == fileName)
      {
        XmlDocument baseRemark = new XmlDocument();
        IBlobReader blobReader = (IBlobReader) attrFile;
        BlobInformation blobInformation = blobReader.OpenBlob(0);
        try
        {
          if (blobInformation.RealFileSize > 0L)
          {
            using (Stream inStream = (Stream) new MemoryStream(blobReader.ReadDataBlock()))
            {
              inStream.Position = 0L;
              if (blobInformation.ArcMethod == ArcMethods.ZLibPacked)
              {
                using (ImChunkedStream imChunkedStream = new ImChunkedStream())
                {
                  ZLibStreamHelper.UnpackStream(inStream, (Stream) imChunkedStream);
                  imChunkedStream.Position = 0L;
                  baseRemark.Load((Stream) imChunkedStream);
                }
              }
              else
                baseRemark.Load(inStream);
            }
          }
        }
        finally
        {
          blobReader.CloseBlob();
        }
        return baseRemark;
      }
    }
    return (XmlDocument) null;
  }

  private bool IsXMLRemarkFile(string fileName)
  {
    return new FileInfo(fileName).Extension.ToLower() == ".rxml";
  }
}
