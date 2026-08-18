// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.CustomObjectsPublisher
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
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;


namespace Intermech.Kernel.Services.PortalServices;

public class CustomObjectsPublisher : ObjectsCompositionPublisher
{
  private readonly Packet4Publish _packet;
  private readonly bool _createReceipt;
  private ExportReceipt _factory;
  private long _createReceiptID;

  public CustomObjectsPublisher(
    PublishComposition composition,
    ExtendedPublishOptions options,
    Packet4Publish packet,
    bool createReceipt)
    : base(composition, options, packet != null ? PublishType.Packet : PublishType.Simple)
  {
    this._packet = packet;
    this._createReceipt = createReceipt;
  }

  protected override void BeforeCompositionPack(
    IUserSession session,
    SiteInfo info,
    IBackupWriter writer,
    List<ITransferedObject> transObjs)
  {
    if (!this._createReceipt)
      return;
    this._factory = new ExportReceipt(this._packet.GUID);
  }

  protected override void AfterObjectPack(IDBObject obj, PublishCompositionObject pco)
  {
    if (!this._createReceipt)
      return;
    this._factory.AddObject(obj, pco);
  }

  protected override void AfterCompositionPack(
    IUserSession session,
    SiteInfo info,
    IBackupWriter writer,
    List<ITransferedObject> transObjs)
  {
    if (!this._createReceipt)
      return;
    ExtendedTransferedObject unit = new ExtendedTransferedObject(ChangeType.ctUpdate, TransferedObjectCategory.Receipt, (string[]) null, (TransferedObjectTag) new ObjectTag(false, false, info.Code, PublishObjectRootType.rtUnknown));
    IDBObject receiptObject = this.CreateReceiptObject(session, info);
    new ObjectXMLFileFormer(session, unit, writer, receiptObject, new Attributes4ObjectTag(PublishObjectRootType.rtUnknown, string.Empty)).SaveAttributes();
    transObjs.Add((ITransferedObject) unit);
  }

  protected virtual void CreateAdditionalReceiptAttributes(
    IUserSession session,
    SiteInfo info,
    IDBObject receipt)
  {
  }

  private IDBObject CreateReceiptObject(IUserSession session, SiteInfo info)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendFormat("Публикация пакета {0}", (object) Packet4Publish.Caption(this._packet.Designation, this._packet.Name, this._packet.GUID));
    IDBObject receipt = DBReceiptCreator.Create(session, info, stringBuilder.ToString(), this._packet.GUID, ReceiptTypes.Export);
    this.CreateAdditionalReceiptAttributes(session, info, receipt);
    using (ImChunkedStream imChunkedStream = new ImChunkedStream())
    {
      new BinaryFormatter().Serialize((Stream) imChunkedStream, (object) this._factory.Content);
      using (ImChunkedStream outStream = new ImChunkedStream())
      {
        imChunkedStream.Position = 0L;
        ZLibStreamHelper.PackStream((Stream) imChunkedStream, ZLibCompressLevels.LevelMax, (Stream) outStream);
        IBlobWriter blobWriter = (receipt.GetAttributeByGuid(PortalConsts.attributeReceiptFile) ?? receipt.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(PortalConsts.attributeReceiptFile), false)) as IBlobWriter;
        if (blobWriter.OpenBlob(new BlobInformation(imChunkedStream.Length, outStream.Length, DateTime.Now, DBReceiptCreator.contentFileName, ArcMethods.ZLibPacked, DBReceiptCreator.contentFileNote), false))
          blobWriter.WriteDataBlock(outStream.ToArray());
      }
    }
    receipt.CommitCreation(true);
    this._createReceiptID = receipt.ObjectID;
    return receipt;
  }

  protected override long ReceiptID => this._createReceiptID;

  protected override bool FCAttributesOnlyEnable => this._packet == null;

  protected override Packet4Publish Packet => this._packet;
}
