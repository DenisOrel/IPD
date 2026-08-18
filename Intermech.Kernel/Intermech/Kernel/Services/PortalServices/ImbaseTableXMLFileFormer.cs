// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.ImbaseTableXMLFileFormer
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Imbase;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Server.WebPortal;
using Intermech.Interfaces.WebPortal;
using Intermech.IO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;


namespace Intermech.Kernel.Services.PortalServices;

internal class ImbaseTableXMLFileFormer(
  IUserSession session,
  ExtendedTransferedObject unit,
  IBackupWriter writer,
  IDBObject obj,
  Attributes4ObjectTag tag) : ObjectXMLFileFormer(session, unit, writer, obj, tag)
{
  private Tuple<byte[], long> GetData()
  {
    IDBAttribute dbAttribute = this.dbObject.GetAttributeByGuid(Intermech.Imbase.Consts.ImbaseTableDataAttGUID);
    if (dbAttribute == null || dbAttribute.IsNull)
      dbAttribute = this.dbObject.GetAttributeByID(TableLoadHelper.LongBlobTableDataAttId);
    if (dbAttribute == null || dbAttribute.IsNull)
      return (Tuple<byte[], long>) null;
    DataSet dataSet = (DataSet) null;
    BinaryFormatter binaryFormatter = new BinaryFormatter();
    IPackedStream service = ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true);
    IBlobReader blobReader = (IBlobReader) dbAttribute;
    BlobInformation blobInformation = blobReader.OpenBlob(0);
    try
    {
      if (blobInformation.RealFileSize == 0L)
        return (Tuple<byte[], long>) null;
      using (ImChunkedStream imChunkedStream = new ImChunkedStream())
      {
        byte[] buffer = blobReader.ReadDataBlock();
        if (blobInformation.ArcMethod == ArcMethods.ZLibPacked)
        {
          using (Stream inStream = (Stream) new MemoryStream(buffer))
          {
            inStream.Position = 0L;
            service.UnpackStream((Stream) imChunkedStream, inStream);
          }
        }
        else
          imChunkedStream.Write(buffer, 0, buffer.Length);
        imChunkedStream.Position = 0L;
        dataSet = (DataSet) binaryFormatter.Deserialize((Stream) imChunkedStream);
      }
    }
    finally
    {
      blobReader.CloseBlob();
    }
    if (dataSet == null)
      return (Tuple<byte[], long>) null;
    DataTable table = dataSet.Tables["IMS_ATTR_TYPES"];
    DataTable[] cacheTables = this.session.GetCacheTables("IMS_ATTRIBUTES", "IMS_POSSIBLE_VALUES");
    DataTable dataTable1 = cacheTables[0].Clone();
    DataTable dataTable2 = cacheTables[1].Clone();
    foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
    {
      string Guid = Convert.ToString(row["F_ATTRIBUTE_GUID"]);
      int attributeTypeId = MetaDataHelper.GetAttributeTypeID(Guid);
      if (attributeTypeId == -10000)
        throw new Exception($"В метаданных {this.dbObject.NameInMessages} (ObjectID={this.dbObject.ObjectID}) присутствует атрибут {Guid}, который не зарегистрирован в системе. Дальнейшее формирование данных для публикации невозможно!");
      DataRow fromRow = cacheTables[0].Rows.Find((object) attributeTypeId);
      DataSetProcessor.AddRow(dataTable1, fromRow, false);
      switch ((MultiValueModes) Convert.ToInt32(fromRow["F_MULTIPLE_VALUED"]))
      {
        case MultiValueModes.SingleValueFromList:
        case MultiValueModes.MultiValuesFromList:
          DataRow[] fromRows = cacheTables[1].Select($"F_ATTRIBUTE_ID={Convert.ToInt32(fromRow["F_ATTRIBUTE_ID"])}");
          if (fromRows != null && fromRows.Length != 0)
          {
            DataSetProcessor.AssignRows(dataTable2, (IEnumerable<DataRow>) fromRows);
            continue;
          }
          continue;
        default:
          continue;
      }
    }
    DataSet graph = new DataSet();
    graph.Tables.Add(dataTable1);
    graph.Tables.Add(dataTable2);
    graph.RemotingFormat = SerializationFormat.Binary;
    using (ImChunkedStream imChunkedStream = new ImChunkedStream())
    {
      binaryFormatter.Serialize((Stream) imChunkedStream, (object) graph);
      imChunkedStream.Position = 0L;
      using (ImChunkedStream outStream = new ImChunkedStream())
      {
        service.PackStream((Stream) outStream, (Stream) imChunkedStream, 9);
        return new Tuple<byte[], long>(outStream.ToArray(), imChunkedStream.Length);
      }
    }
  }

  protected override void GetAdditionalAttributes(
    IUserSession session,
    XmlDocument xmlDocument,
    XmlNode xmlRootNode,
    PreparedPersistentObject prepared)
  {
    Tuple<byte[], long> data = this.GetData();
    XmlNode attributeNode = this.CreateAttributeNode(xmlDocument, session.GetAttributeType(PortalConsts.attributeTableAttributes));
    XmlNode valueNode = XMLFileHelper.CreateValueNode(xmlDocument, 0);
    string str = this.FormingBlobNode(xmlDocument, valueNode, "Описание атрибутов таблицы", this.ToUTCDateTime(DateTime.Now, session), data.Item2, (long) data.Item1.Length, ArcMethods.ZLibPacked, FileTypes.ftNormal, Guid.Empty);
    attributeNode.AppendChild(valueNode);
    xmlRootNode.AppendChild(attributeNode);
    prepared.InventedBlobs.Add(new Tuple<string, byte[]>(str, data.Item1));
  }

  protected override void WriteAdditionalAttributes(
    IUserSession session,
    XmlDocument xmlDocument,
    XmlNode xmlRootNode,
    IBackupWriter writer)
  {
    Tuple<byte[], long> data = this.GetData();
    writer.WriteBlob(data.Item1);
    XmlNode attributeNode = this.CreateAttributeNode(xmlDocument, session.GetAttributeType(PortalConsts.attributeTableAttributes));
    XmlNode valueNode = XMLFileHelper.CreateValueNode(xmlDocument, 0);
    this.FormingBlobNode(xmlDocument, valueNode, "Описание атрибутов таблицы", this.ToUTCDateTime(DateTime.Now, session), data.Item2, (long) data.Item1.Length, ArcMethods.ZLibPacked, FileTypes.ftNormal, Guid.Empty);
    attributeNode.AppendChild(valueNode);
    xmlRootNode.AppendChild(attributeNode);
  }
}
