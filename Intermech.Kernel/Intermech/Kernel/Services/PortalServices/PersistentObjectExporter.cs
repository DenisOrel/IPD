// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.PersistentObjectExporter
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Server.WebPortal;
using Intermech.Interfaces.WebPortal;
using Intermech.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;


namespace Intermech.Kernel.Services.PortalServices;

internal class PersistentObjectExporter : 
  TransferedObjectExporter<PersistentObject>,
  ITransferedObjectExporter
{
  private IPackedStream _packedStream;

  public PersistentObjectExporter(long portalTaskID, PersistentObject unit)
    : base(portalTaskID, unit)
  {
    this._packedStream = ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true);
  }

  public void Publish(IUserSession session, Guid connectionGuid, IPortalConnector connector)
  {
    ExtendedTransferedObject unit = new ExtendedTransferedObject(ChangeType.ctUpdate, this.unit.IsLink ? TransferedObjectCategory.ObjectLink : TransferedObjectCategory.Object, this.unit.Tag);
    unit.GUID = this.unit.GUID;
    IDBObject dbObject = session.GetObject(this.unit.ObjectID);
    PreparedPersistentObject attributes = Publisher.GetXMLFileFormer(session, unit, (IBackupWriter) null, dbObject, new Attributes4ObjectTag(((ObjectTag) this.unit.Tag).RootType, this.unit.LinkedGuid)).GetAttributes();
    unit.DataFiles = this.GetDataFilesForPersistentObject(attributes);
    if (SiteTraceLog.Enabled)
      SiteTraceLog.Write($"PersistentObjectExporter start publish unit={this.unit.GUID} connectionGuid={connectionGuid}");
    connector.PublishUnit(connectionGuid, this.portalTaskID, unit.ToTransferedObject);
    byte[] attributesXmlBlob = this.GetAttributesXMLBlob(attributes.AttributesXML);
    FromBytesFileSender fromBytesFileSender = new FromBytesFileSender(attributesXmlBlob, this.unit.GUID);
    fromBytesFileSender.TransferFile(connectionGuid, connector, PortalConsts.AttributesXmlFileName, attributesXmlBlob.Length);
    if (attributes.InventedBlobs.Count > 0)
    {
      foreach (Tuple<string, byte[]> inventedBlob in attributes.InventedBlobs)
      {
        fromBytesFileSender.Data = inventedBlob.Item2;
        fromBytesFileSender.TransferFile(connectionGuid, connector, inventedBlob.Item1, inventedBlob.Item2.Length);
      }
    }
    if (attributes.DBBlobs.Count <= 0)
      return;
    foreach (Tuple<int, int, string> dbBlob in attributes.DBBlobs)
    {
      IDBAttribute attributeById = dbObject.GetAttributeByID(dbBlob.Item1);
      attributeById.Index = dbBlob.Item2;
      if (attributeById.DataType == FieldTypes.ftMemo)
      {
        byte[] memoData = this.GetMemoData((string) attributeById.Value);
        new FromBytesFileSender(memoData, this.unit.GUID).TransferFile(connectionGuid, connector, dbBlob.Item3, memoData != null ? memoData.Length : 0);
      }
      else
      {
        IBlobReader reader = attributeById as IBlobReader;
        BlobInformation blobInformation = reader.OpenBlob(0);
        try
        {
          new FromBlobReaderFileSender(reader, this.unit.GUID).TransferFile(connectionGuid, connector, dbBlob.Item3, Convert.ToInt32(blobInformation.PackedFileSize));
        }
        finally
        {
          reader.CloseBlob();
        }
      }
    }
  }

  protected byte[] GetMemoData(string memoValue)
  {
    if (string.IsNullOrEmpty(memoValue))
      return (byte[]) null;
    using (MemoryStream inStream = new MemoryStream(Encoding.UTF8.GetBytes(memoValue)))
    {
      inStream.Position = 0L;
      using (ImChunkedStream outStream = new ImChunkedStream())
      {
        this._packedStream.PackStream((Stream) outStream, (Stream) inStream, 5);
        return outStream.ToArray();
      }
    }
  }

  private byte[] GetAttributesXMLBlob(XmlDocument xmlDocument)
  {
    using (ImChunkedStream imChunkedStream = new ImChunkedStream())
    {
      xmlDocument.Save((Stream) imChunkedStream);
      imChunkedStream.Position = 0L;
      using (ImChunkedStream outStream = new ImChunkedStream())
      {
        this._packedStream.PackStream((Stream) outStream, (Stream) imChunkedStream, 5);
        return outStream.ToArray();
      }
    }
  }

  private string[] GetDataFilesForPersistentObject(PreparedPersistentObject data)
  {
    List<string> stringList = new List<string>()
    {
      PortalConsts.AttributesXmlFileName
    };
    if (data.DBBlobs.Count > 0)
      stringList.AddRange((IEnumerable<string>) data.DBBlobs.ConvertAll<string>((Converter<Tuple<int, int, string>, string>) (x => x.Item3)));
    if (data.InventedBlobs.Count > 0)
      stringList.AddRange((IEnumerable<string>) data.InventedBlobs.ConvertAll<string>((Converter<Tuple<string, byte[]>, string>) (x => x.Item1)));
    return stringList.ToArray();
  }
}
