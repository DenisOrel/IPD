// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.ImbaseTableRefXMLFileFormer
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Server.WebPortal;
using Intermech.IO;
using Intermech.Kernel.Search;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;


namespace Intermech.Kernel.Services.PortalServices;

internal sealed class ImbaseTableRefXMLFileFormer(
  IUserSession session,
  ExtendedTransferedObject unit,
  IBackupWriter writer,
  IDBObject obj,
  Attributes4ObjectTag tag) : ObjectXMLFileFormer(session, unit, writer, obj, tag)
{
  private string GetParentPath(IUserSession session, long id)
  {
    IDBRelationCollection relationCollection = session.GetRelationCollection(session.IdentHelper.SortedRelationTypeID);
    relationCollection.LocalTypesMode = true;
    DataTable dataTable = relationCollection.EntersIn(new DBRecordSetParams((ConditionStructure[]) null, new object[2]
    {
      (object) -2,
      (object) -3
    }), id);
    if (dataTable.Rows.Count <= 0)
      return string.Empty;
    QuickObjectInfo objectInfo = session.GetObjectInfo(Convert.ToInt64(dataTable.Rows[0][0]));
    string parentPath = this.GetParentPath(session, Convert.ToInt64(dataTable.Rows[0][1]));
    return string.IsNullOrEmpty(parentPath) ? objectInfo.Caption : $"{parentPath}\\{objectInfo.Caption}";
  }

  protected override void GetAdditionalAttributes(
    IUserSession session,
    XmlDocument xmlDocument,
    XmlNode xmlRootNode,
    PreparedPersistentObject prepared)
  {
    string parentPath = this.GetParentPath(session, this.dbObject.ID);
    if (string.IsNullOrEmpty(parentPath))
      return;
    byte[] bytes = Encoding.UTF8.GetBytes(parentPath);
    using (MemoryStream inStream = new MemoryStream(bytes))
    {
      inStream.Position = 0L;
      using (ImChunkedStream outStream = new ImChunkedStream())
      {
        this.packedStream.PackStream((Stream) outStream, (Stream) inStream, 5);
        string xml = this.SaveNodeToXml(xmlDocument, xmlRootNode, new BlobInformation((long) bytes.Length, outStream.Length, DateTime.Now, string.Empty, ArcMethods.ZLibPacked, string.Empty));
        prepared.InventedBlobs.Add(new Tuple<string, byte[]>(xml, outStream.ToArray()));
      }
    }
  }

  protected override void WriteAdditionalAttributes(
    IUserSession session,
    XmlDocument xmlDocument,
    XmlNode xmlRootNode,
    IBackupWriter writer)
  {
    string parentPath = this.GetParentPath(session, this.dbObject.ID);
    if (string.IsNullOrEmpty(parentPath))
      return;
    this.SaveNodeToXml(xmlDocument, xmlRootNode, this.SaveMemoData(parentPath, string.Empty, writer, false));
  }

  private string SaveNodeToXml(
    XmlDocument xmlDocument,
    XmlNode xmlRootNode,
    BlobInformation blobInformation)
  {
    XmlNode attributeNode = this.CreateAttributeNode(xmlDocument, this.session.GetAttributeType(new Guid("cadd9677-306c-11d8-b4e9-00304f19f545")));
    XmlNode valueNode = XMLFileHelper.CreateValueNode(xmlDocument, 0);
    string xml = this.FormingBlobNode(xmlDocument, valueNode, "Путь к ярлыку в базе-источнике", DateTime.Now, blobInformation.RealFileSize, blobInformation.PackedFileSize, blobInformation.ArcMethod, FileTypes.ftNormal, Guid.Empty);
    attributeNode.AppendChild(valueNode);
    xmlRootNode.AppendChild(attributeNode);
    return xml;
  }
}
