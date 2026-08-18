// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.ImportReceipt
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.WebPortal;
using Intermech.IO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;


namespace Intermech.Kernel.Services.PortalServices;

public class ImportReceipt : Receipt
{
  private readonly int _attributeChangeNoID;
  private readonly int _attributeDesignationID;
  private readonly int _attributeNameID;

  public string EnableSites { get; private set; }

  public long PacketID { get; private set; }

  public long ReceiptID { get; set; }

  public ImportReceipt(
    long receiptID,
    DataTable table,
    string enableSites,
    long packetID,
    Guid packetGuid)
    : base(ReceiptTypes.Import, table == null, packetGuid)
  {
    this.ReceiptID = receiptID;
    this.EnableSites = enableSites;
    this.PacketID = packetID;
    if (table != null)
      this.content = table;
    this._attributeChangeNoID = MetaDataHelper.GetAttributeTypeID("cad00770-306c-11d8-b4e9-00304f19f545");
    this._attributeDesignationID = MetaDataHelper.GetAttributeTypeID("cad0001f-306c-11d8-b4e9-00304f19f545");
    this._attributeNameID = MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545");
  }

  public static ImportReceipt Create(
    IUserSession session,
    long taskID,
    string caption,
    string enableSites,
    long packetID,
    Guid packetGuid)
  {
    ISitesCacheService customService = (ISitesCacheService) session.GetCustomService(typeof (ISitesCacheService));
    IDBObject dbObject = DBReceiptCreator.Create(session, customService.Info, $"Импорт пакета {caption} узлом {customService.Info.Code}({customService.Info.Caption})", packetGuid, ReceiptTypes.Import);
    dbObject.CommitCreation(true);
    session.GetRelationCollection(session.IdentHelper.SimpleRelationTypeID).Create(taskID, dbObject.ObjectID);
    return new ImportReceipt(dbObject.ObjectID, (DataTable) null, enableSites, packetID, packetGuid);
  }

  private static void SetReceiptAttribute(IDBObject receipt, Guid attributeGuid, object value)
  {
    (receipt.GetAttributeByGuid(attributeGuid) ?? receipt.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(attributeGuid), false)).Value = value;
  }

  public static ImportReceipt Open(IUserSession session, long receiptID)
  {
    IDBObject dbObject = session.GetObject(receiptID);
    IDBAttribute attributeByGuid1 = dbObject.GetAttributeByGuid(PortalConsts.attributeEnabledSites);
    IDBAttribute attributeByGuid2 = dbObject.GetAttributeByGuid(PortalConsts.attributePacketID);
    IDBAttribute attributeByGuid3 = dbObject.GetAttributeByGuid(PortalConsts.attributePacketGUID);
    IBlobReader attributeByGuid4 = dbObject.GetAttributeByGuid(PortalConsts.attributeReceiptFile) as IBlobReader;
    attributeByGuid4.OpenBlob(0);
    try
    {
      using (Stream inStream = (Stream) new MemoryStream(attributeByGuid4.ReadDataBlock()))
      {
        using (ImChunkedStream imChunkedStream = new ImChunkedStream())
        {
          ZLibStreamHelper.UnpackStream(inStream, (Stream) imChunkedStream);
          imChunkedStream.Position = 0L;
          BinaryFormatter binaryFormatter = new BinaryFormatter();
          return new ImportReceipt(receiptID, (DataTable) binaryFormatter.Deserialize((Stream) imChunkedStream), attributeByGuid1.AsString, attributeByGuid2.AsInteger, new Guid(attributeByGuid3.AsString));
        }
      }
    }
    finally
    {
      attributeByGuid4.CloseBlob();
    }
  }

  public void Save(IUserSession session)
  {
    if (this.content == null)
      return;
    this.SaveContent(session.GetObject(this.ReceiptID));
  }

  public void SaveContent(IDBObject receipt)
  {
    IBlobWriter attributeByGuid = receipt.GetAttributeByGuid(PortalConsts.attributeReceiptFile) as IBlobWriter;
    using (ImChunkedStream imChunkedStream = new ImChunkedStream())
    {
      new BinaryFormatter().Serialize((Stream) imChunkedStream, (object) this.content);
      imChunkedStream.Position = 0L;
      using (ImChunkedStream outStream = new ImChunkedStream())
      {
        ZLibStreamHelper.PackStream((Stream) imChunkedStream, ZLibCompressLevels.LevelMax, (Stream) outStream);
        if (!attributeByGuid.OpenBlob(new BlobInformation(imChunkedStream.Length, outStream.Length, DateTime.Now, DBReceiptCreator.contentFileName, ArcMethods.ZLibPacked, DBReceiptCreator.contentFileNote), false))
          return;
        attributeByGuid.WriteDataBlock(outStream.ToArray());
      }
    }
  }

  protected override DataTable CreateTable()
  {
    DataTable table = base.CreateTable();
    table.Columns.Add("F_NAME", typeof (string));
    table.Columns.Add("F_INLIST_ID", typeof (int));
    table.Columns.Add("F_ORIGINAL_VALUE", typeof (string));
    table.Columns.Add("F_VALUE", typeof (string));
    table.Columns.Add("F_NOTE", typeof (string));
    return table;
  }

  public void AddObjectRecord(
    IUserSession session,
    IDBObject importObj,
    ImportingObject briefObject,
    string note)
  {
    DataRow row = this.AddObjectRow(briefObject);
    row["F_NOTE"] = (object) note;
    this.content.Rows.Add(row);
  }

  public void AddAttributeRecord(ImportingObject briefObject, string attributeName, string note)
  {
    this.AddAttributeRecord(briefObject, attributeName, 0, (string) null, (string) null, note);
  }

  public void AddAttributeRecord(
    ImportingObject briefObject,
    string attributeName,
    int inlistID,
    string originalValue,
    string newValue)
  {
    this.AddAttributeRecord(briefObject, attributeName, inlistID, originalValue, newValue, string.Empty);
  }

  public void AddAttributeRecord(
    ImportingObject briefObject,
    string attributeName,
    int inlistID,
    string originalValue,
    string newValue,
    string note)
  {
    DataRow row = this.AddObjectRow(briefObject);
    row["F_NAME"] = (object) attributeName;
    row["F_INLIST_ID"] = (object) inlistID;
    row["F_ORIGINAL_VALUE"] = (object) originalValue;
    row["F_VALUE"] = (object) newValue;
    row["F_NOTE"] = (object) note;
    this.content.Rows.Add(row);
  }

  public DataRow AddObjectRow(ImportingObject briefObject)
  {
    DataRow dataRow = this.content.NewRow();
    dataRow["CAPTION"] = (object) briefObject.Object.Caption;
    AttributeRecord attribute1 = this.GetAttribute(this._attributeDesignationID, briefObject.Attributes);
    if (attribute1 != null)
      dataRow["cad0001f-306c-11d8-b4e9-00304f19f545"] = attribute1.StringValue;
    AttributeRecord attribute2 = this.GetAttribute(this._attributeNameID, briefObject.Attributes);
    if (attribute2 != null)
      dataRow["cad00020-306c-11d8-b4e9-00304f19f545"] = attribute2.StringValue;
    dataRow["F_OBJECT_TYPE"] = briefObject.Object.ObjectType > 0 ? (object) MetaDataHelper.GetObjectTypeName(briefObject.Object.ObjectType) : (object) string.Empty;
    dataRow["F_VERSION_ID"] = (object) briefObject.Object.VersionId;
    dataRow["F_OBJECT_ID"] = (object) briefObject.Object.Object_id;
    dataRow["F_GUID"] = (object) Convert.ToString(briefObject.Object.ObjectGuid);
    dataRow["F_OBJ_GUID"] = (object) Convert.ToString(briefObject.Object.IdGuid);
    dataRow["F_FILENAME"] = (object) this.FormingFileNames(briefObject.Attributes);
    AttributeRecord attribute3 = this.GetAttribute(this._attributeChangeNoID, briefObject.Attributes);
    if (attribute3 != null)
      dataRow["cad00770-306c-11d8-b4e9-00304f19f545"] = (object) Convert.ToInt32(attribute3.IntegerValue);
    return dataRow;
  }

  private AttributeRecord GetAttribute(int attributeID, List<AttributeRecord> attributes)
  {
    return attributes.Find((Predicate<AttributeRecord>) (x => x.AttributeId == attributeID));
  }

  private string FormingFileNames(List<AttributeRecord> attributes)
  {
    StringBuilder stringBuilder = new StringBuilder();
    foreach (AttributeRecord attribute in attributes)
    {
      if (MetaDataHelper.GetAttributeType(attribute.AttributeId).RealFieldType == FieldTypes.ftFile)
        stringBuilder.Append($"\"{attribute.StringValue}\"({StringsHelper.GetSizeString((long) attribute.IntegerValue)})");
    }
    return stringBuilder.ToString();
  }
}
