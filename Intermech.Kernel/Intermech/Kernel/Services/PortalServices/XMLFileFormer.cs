// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.XMLFileFormer
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
using System.Data;
using System.IO;
using System.Text;
using System.Xml;


namespace Intermech.Kernel.Services.PortalServices;

public abstract class XMLFileFormer
{
  protected IUserSession session;
  protected ExtendedTransferedObject unit;
  protected readonly IBackupWriter writer;
  protected Attributes4Tag tag;
  public bool CheckAttributes = true;
  protected IPackedStream packedStream;
  protected int countBlobFiles;

  public XMLFileFormer(IUserSession session, ExtendedTransferedObject unit, IBackupWriter writer)
    : this(session, unit, writer, (Attributes4Tag) null)
  {
  }

  public XMLFileFormer(
    IUserSession session,
    ExtendedTransferedObject unit,
    IBackupWriter writer,
    Attributes4Tag tag)
  {
    this.session = session;
    this.unit = unit;
    this.writer = writer;
    this.tag = tag;
    this.packedStream = ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true);
  }

  protected virtual string AttributeNode => PortalConsts.XmlNodeAttribute;

  public void SaveAttributes()
  {
    XmlNode xmlRootNode;
    XmlDocument attributesXml = this.CreateAttributesXML(out List<Tuple<int, int, string>> _, out xmlRootNode, false);
    this.WriteAdditionalAttributes(this.session, attributesXml, xmlRootNode, this.writer);
    this.SaveAttributesXMLFile(attributesXml, this.writer);
  }

  public PreparedPersistentObject GetAttributes()
  {
    List<Tuple<int, int, string>> blobs;
    XmlNode xmlRootNode;
    XmlDocument attributesXml = this.CreateAttributesXML(out blobs, out xmlRootNode, true);
    PreparedPersistentObject prepared = new PreparedPersistentObject(attributesXml, blobs);
    this.GetAdditionalAttributes(this.session, attributesXml, xmlRootNode, prepared);
    return prepared;
  }

  protected XmlDocument CreateAttributesXML(
    out List<Tuple<int, int, string>> blobs,
    out XmlNode xmlRootNode,
    bool notWriteToWriter)
  {
    IIDLinkTranslate customService = (IIDLinkTranslate) this.session.GetCustomService(typeof (IIDLinkTranslate));
    blobs = new List<Tuple<int, int, string>>();
    XmlDocument xml = this.CreateXML();
    xmlRootNode = (XmlNode) xml.CreateElement(PortalConsts.XmlRootNodeAttributes);
    this.WriteRootNode(xml, xmlRootNode);
    if (this.Attributes != null)
    {
      IPublishRulesService service = ServiceUtils.GetService<IPublishRulesService>((object) ServerServices.ServiceContainer, true);
      int fileAttributeId = this.session.IdentHelper.FileAttributeID;
      this.countBlobFiles = 0;
      Guid typeGuid = this.TypeGuid;
      for (int AttrIndex = 0; AttrIndex < this.Attributes.Count; ++AttrIndex)
      {
        IDBAttribute attribute = this.Attributes[AttrIndex];
        if ((this.EnableAttributes == null || this.EnableAttributes.Contains(attribute.AttributeID)) && (!this.CheckAttributes || !service.IsForbiddenAttribute(Guid.Empty, attribute.AttributeID) && (!(typeGuid != Guid.Empty) || !service.IsForbiddenAttribute(typeGuid, attribute.AttributeID))))
        {
          IDBAttributeType attributeType = this.session.GetAttributeType(attribute.AttributeID);
          bool flag = customService.IsIDLink((attributeType as IDBGuid).GUID);
          XmlNode attributeNode = this.CreateAttributeNode(xml, attributeType);
          List<string> stringList = new List<string>();
          for (int index1 = 0; index1 < attribute.ValuesCount; ++index1)
          {
            attribute.Index = index1;
            XmlNode valueNode = XMLFileHelper.CreateValueNode(xml, index1);
            if (attribute.Value != null && attribute.Value != DBNull.Value)
            {
              if (attributeType.MultipleValued == MultiValueModes.MultiValuesFromList || attributeType.MultipleValued == MultiValueModes.SingleValueFromList)
              {
                string empty = string.Empty;
                DataTable possibleValues = attributeType.GetPossibleValues();
                for (int index2 = 0; index2 < possibleValues.Rows.Count; ++index2)
                {
                  if (possibleValues.Rows[index2][1].Equals(attribute.Value))
                  {
                    if (possibleValues.Rows[index2][2].ToString() != string.Empty)
                      empty = possibleValues.Rows[index2][2].ToString();
                    else
                      break;
                  }
                }
                if (empty != string.Empty)
                  XMLFileHelper.AddAttribute(xml, valueNode, "F_DESCRIPTION", empty);
              }
              ExportAttributeHandler attributeHandler = (ServerServices.GetService(typeof (IExportAttributesHandlerService)) as AttributesHandlerService).ChangeValue(this.session, attribute);
              switch (attribute.DataType)
              {
                case FieldTypes.ftString:
                  XMLFileHelper.AddStringAttribute(xml, valueNode, attributeHandler != null ? (string) attributeHandler.Value : attribute.AsString);
                  break;
                case FieldTypes.ftInteger:
                case FieldTypes.ftBoolean:
                case FieldTypes.ftAutoInc:
                  if (flag)
                  {
                    this.AddLink(this.session, xml, valueNode, attribute.AsInteger);
                    break;
                  }
                  XMLFileHelper.AddIntegerAttribute(xml, valueNode, attribute.AsInteger);
                  break;
                case FieldTypes.ftDouble:
                  XMLFileHelper.AddDoubleAttribute(xml, valueNode, attribute.AsDouble);
                  break;
                case FieldTypes.ftDateTime:
                  XMLFileHelper.AddDateTimeAttribute(xml, valueNode, this.ToUTCDateTime(attribute.AsDateTime, this.session));
                  break;
                case FieldTypes.ftShortBlob:
                  BlobInformation blobInformation1 = this.SaveBlobData(attribute, this.writer, notWriteToWriter);
                  string str1 = this.FormingBlobNode(xml, valueNode, blobInformation1.Note, this.ToUTCDateTime(blobInformation1.ModifyDate, this.session), blobInformation1.RealFileSize, blobInformation1.PackedFileSize, blobInformation1.ArcMethod, blobInformation1.FileType, this.GetAuthor(this.session, blobInformation1.Author));
                  blobs.Add(new Tuple<int, int, string>(attribute.AttributeID, attribute.Index, str1));
                  break;
                case FieldTypes.ftFile:
                  if (attribute.AttributeID == fileAttributeId)
                  {
                    if (attribute is IBlobReader blobReader)
                    {
                      BlobInformation biFile = blobReader.OpenBlob(-1);
                      try
                      {
                        if (!this.IsEnablePublishFile(biFile))
                          continue;
                      }
                      finally
                      {
                        blobReader.CloseBlob();
                      }
                    }
                    else
                      continue;
                  }
                  BlobInformation blobInformation2 = this.SaveBlobData(attribute, this.writer, notWriteToWriter);
                  string str2 = this.FormingBlobNode(xml, valueNode, blobInformation2.FileName, this.ToUTCDateTime(blobInformation2.ModifyDate, this.session), blobInformation2.RealFileSize, blobInformation2.PackedFileSize, blobInformation2.ArcMethod, blobInformation2.FileType, this.GetAuthor(this.session, blobInformation2.Author));
                  blobs.Add(new Tuple<int, int, string>(attribute.AttributeID, attribute.Index, str2));
                  break;
                case FieldTypes.ftObjectLink:
                  this.AddLink(this.session, xml, valueNode, attribute.AsInteger);
                  break;
                case FieldTypes.ftPassword:
                  XMLFileHelper.AddStringAttribute(xml, valueNode, attribute.AsString);
                  XMLFileHelper.AddDateTimeAttribute(xml, valueNode, this.ToUTCDateTime(attribute.AsDateTime, this.session));
                  break;
                case FieldTypes.ftMemo:
                  BlobInformation blobInformation3 = this.SaveMemoData(attribute, this.writer, notWriteToWriter);
                  string str3 = this.FormingBlobNode(xml, valueNode, attribute.AsString, this.ToUTCDateTime(DateTime.Now, this.session), blobInformation3.PackedFileSize, blobInformation3.PackedFileSize, blobInformation3.ArcMethod, FileTypes.ftNormal, this.GetAuthor(this.session, blobInformation3.Author));
                  blobs.Add(new Tuple<int, int, string>(attribute.AttributeID, attribute.Index, str3));
                  break;
                case FieldTypes.ftBlob:
                  BlobInformation blobInformation4 = this.SaveBlobData(attribute, this.writer, notWriteToWriter);
                  string str4 = this.FormingBlobNode(xml, valueNode, blobInformation4.Note, this.ToUTCDateTime(blobInformation4.ModifyDate, this.session), blobInformation4.RealFileSize, blobInformation4.PackedFileSize, blobInformation4.ArcMethod, blobInformation4.FileType, this.GetAuthor(this.session, blobInformation4.Author));
                  blobs.Add(new Tuple<int, int, string>(attribute.AttributeID, attribute.Index, str4));
                  break;
                case FieldTypes.ftMeasured:
                  if (attribute.Value is MeasuredValue measuredValue && measuredValue.MeasureID != 0L)
                  {
                    IDBObject dbObject = this.session.GetObject(measuredValue.MeasureID);
                    XMLFileHelper.AddGuidAttribute(xml, valueNode, dbObject.ObjectGUID);
                    XMLFileHelper.AddDoubleAttribute(xml, valueNode, measuredValue.Value);
                    XMLFileHelper.AddStringAttribute(xml, valueNode, measuredValue.Caption);
                    break;
                  }
                  break;
                case FieldTypes.ftGuid:
                  if (attribute.AsString != string.Empty)
                  {
                    XMLFileHelper.AddStringAttribute(xml, valueNode, attribute.AsString);
                    break;
                  }
                  break;
                case FieldTypes.ftObjectLinkByID:
                  this.AddLinkByID(this.session, xml, valueNode, attribute.AsInteger);
                  break;
              }
              attributeNode.AppendChild(valueNode);
            }
          }
          if (attribute.AttributeID != fileAttributeId || attribute.AttributeID == fileAttributeId && attributeNode.ChildNodes.Count > 0)
            xmlRootNode.AppendChild(attributeNode);
        }
      }
    }
    xml.AppendChild(xmlRootNode);
    return xml;
  }

  private void SaveAttributesXMLFile(XmlDocument xmlDocument, IBackupWriter writer)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      xmlDocument.Save((Stream) memoryStream);
      memoryStream.Position = 0L;
      using (MemoryStream outStream = new MemoryStream())
      {
        this.packedStream.PackStream((Stream) outStream, (Stream) memoryStream, 9);
        byte[] array = outStream.ToArray();
        writer.WriteBlob(array);
        this.SetUnitFile(PortalConsts.AttributesXmlFileName, (long) array.Length);
      }
    }
  }

  protected virtual bool IsEnablePublishFile(BlobInformation biFile) => true;

  protected virtual void GetAdditionalAttributes(
    IUserSession session,
    XmlDocument xmlDocument,
    XmlNode xmlRootNode,
    PreparedPersistentObject prepared)
  {
  }

  protected virtual void WriteAdditionalAttributes(
    IUserSession session,
    XmlDocument xmlDocument,
    XmlNode xmlRootNode,
    IBackupWriter writer)
  {
  }

  protected virtual Guid TypeGuid => Guid.Empty;

  protected virtual IDBAttributeCollection Attributes => (IDBAttributeCollection) null;

  protected virtual List<int> EnableAttributes => (List<int>) null;

  protected virtual void WriteRootNode(XmlDocument xmlDocument, XmlNode xmlRootNode)
  {
  }

  protected string FormingBlobNode(
    XmlDocument xmlDocument,
    XmlNode attrValueNode,
    string stingValue,
    DateTime dateValue,
    long intValue,
    long blobSize,
    ArcMethods arcMethod,
    FileTypes fileType,
    Guid author)
  {
    string filename = $"blob{this.countBlobFiles++}.dat";
    XMLFileHelper.AddStringAttribute(xmlDocument, attrValueNode, stingValue);
    XMLFileHelper.AddDateTimeAttribute(xmlDocument, attrValueNode, dateValue);
    XMLFileHelper.AddIntegerAttribute(xmlDocument, attrValueNode, intValue);
    XMLFileHelper.AddAttribute(xmlDocument, attrValueNode, "F_ARC_METHOD", Convert.ToString((int) arcMethod));
    XMLFileHelper.AddAttribute(xmlDocument, attrValueNode, "F_FILE", filename);
    XMLFileHelper.AddAttribute(xmlDocument, attrValueNode, "F_FILE_TYPE", Convert.ToString((int) fileType));
    XMLFileHelper.AddAttribute(xmlDocument, attrValueNode, "F_FILE_AUTHOR", Convert.ToString((object) author));
    this.SetUnitFile(filename, blobSize);
    return filename;
  }

  private void SetUnitFile(string filename, long blobSize)
  {
    if (this.unit.DataFiles == null || this.unit.DataFiles.Length == 0)
    {
      this.unit.DataFiles = new string[1]{ filename };
      this.unit.FileSizes = new long[1]{ blobSize };
    }
    else
    {
      string[] destinationArray = new string[this.unit.DataFiles.Length + 1];
      Array.Copy((Array) this.unit.DataFiles, (Array) destinationArray, this.unit.DataFiles.Length);
      destinationArray[destinationArray.Length - 1] = filename;
      this.unit.DataFiles = destinationArray;
      Array.Resize<long>(ref this.unit.FileSizes, this.unit.FileSizes.Length + 1);
      this.unit.FileSizes[this.unit.FileSizes.Length - 1] = blobSize;
    }
  }

  protected void WriteBlob(BlobInformation blobInfo, byte[] data, IDBAttribute attrFile)
  {
    attrFile.Index = attrFile.AddValue((object) null);
    if (data == null)
      return;
    IBlobWriter blobWriter = (IBlobWriter) attrFile;
    if (!blobWriter.OpenBlob(blobInfo, false))
      return;
    blobWriter.WriteDataBlock(data);
  }

  private void AddLinkByID(
    IUserSession session,
    XmlDocument xmlDocument,
    XmlNode attrValueNode,
    long id)
  {
    if (id == 0L || id == -1L)
      return;
    IDBObject objectBaseVersionById = session.GetObjectBaseVersionByID(id, true);
    XMLFileHelper.AddGuidAttribute(xmlDocument, attrValueNode, objectBaseVersionById.GUID);
    XMLFileHelper.AddAttribute(xmlDocument, attrValueNode, "F_DESCRIPTION", objectBaseVersionById.ObjectGUID.ToString());
    XMLFileHelper.AddStringAttribute(xmlDocument, attrValueNode, objectBaseVersionById.Caption);
  }

  private void AddLink(
    IUserSession session,
    XmlDocument xmlDocument,
    XmlNode attrValueNode,
    long objectId)
  {
    if (objectId == 0L || objectId == -1L)
      return;
    QuickObjectInfo objectInfo = session.GetObjectInfo(objectId);
    XMLFileHelper.AddGuidAttribute(xmlDocument, attrValueNode, objectInfo.VersionGuid);
    XMLFileHelper.AddStringAttribute(xmlDocument, attrValueNode, objectInfo.Caption);
  }

  protected XmlNode CreateAttributeNode(XmlDocument xmlDocument, IDBAttributeType attrType)
  {
    return this.CreateAttributeNode(xmlDocument, attrType, this.AttributeNode, true);
  }

  protected XmlNode CreateAttributeNode(
    XmlDocument xmlDocument,
    IDBAttributeType attrType,
    string name,
    bool createAdditionalAttributesForNode)
  {
    XmlNode element = (XmlNode) xmlDocument.CreateElement(name);
    XMLFileHelper.AddGuidAttribute(xmlDocument, element, (attrType as IDBGuid).GUID);
    XMLFileHelper.AddAttribute(xmlDocument, element, "F_NAME", attrType.Name);
    XMLFileHelper.AddAttribute(xmlDocument, element, "F_SHORT_NAME", attrType.ShortName);
    XMLFileHelper.AddAttribute(xmlDocument, element, "F_ALIAS", attrType.Alias);
    XMLFileHelper.AddAttribute(xmlDocument, element, "F_ATTRIBUTE_TYPE", Convert.ToString((int) attrType.AttributeType));
    if (createAdditionalAttributesForNode)
      this.AdditionalAttributesForNode(xmlDocument, element, attrType);
    return element;
  }

  protected virtual void AdditionalAttributesForNode(
    XmlDocument xmlDocument,
    XmlNode xmlNode,
    IDBAttributeType attrType)
  {
  }

  protected DateTime ToUTCDateTime(DateTime localDateTime, IUserSession session)
  {
    if (localDateTime == DateTime.MinValue)
      return DateTime.MinValue;
    return localDateTime == DateTime.MaxValue ? DateTime.MaxValue : localDateTime - session.TimeZoneOffset;
  }

  protected string GetObjectGuid(IUserSession session, long objectID)
  {
    QuickObjectInfo objectInfo = session.GetObjectInfo(objectID);
    return !objectInfo.Empty ? objectInfo.VersionGuid.ToString() : string.Empty;
  }

  private XmlDocument CreateXML()
  {
    XmlDocument xml = new XmlDocument();
    xml.AppendChild((XmlNode) xml.CreateXmlDeclaration("1.0", (string) null, (string) null));
    return xml;
  }

  protected virtual BlobInformation SaveBlobData(
    IDBAttribute attrSource,
    IBackupWriter writer,
    bool notWriteToWriter)
  {
    IBlobReader blobReader = (IBlobReader) attrSource;
    BlobInformation blobInformation = blobReader.OpenBlob(notWriteToWriter ? -1 : 0);
    try
    {
      if (!notWriteToWriter)
      {
        byte[] buffer = (byte[]) null;
        if (blobInformation.RealFileSize > 0L)
          buffer = blobReader.ReadDataBlock();
        if (buffer != null)
        {
          if (buffer.Length != 0)
            writer.WriteBlob(buffer);
        }
      }
    }
    finally
    {
      blobReader.CloseBlob();
    }
    return blobInformation;
  }

  protected BlobInformation SaveMemoData(
    IDBAttribute attrSource,
    IBackupWriter writer,
    bool notWriteToWriter)
  {
    return this.SaveMemoData((string) attrSource.Value, attrSource.AsString, writer, notWriteToWriter);
  }

  protected BlobInformation SaveMemoData(
    string memoValue,
    string note,
    IBackupWriter writer,
    bool notWriteToWriter)
  {
    if (string.IsNullOrEmpty(memoValue))
      return BlobInformation.EmptyBlobInformation();
    byte[] bytes = Encoding.UTF8.GetBytes(memoValue);
    using (MemoryStream inStream = new MemoryStream(bytes))
    {
      inStream.Position = 0L;
      using (ImChunkedStream outStream = new ImChunkedStream())
      {
        this.packedStream.PackStream((Stream) outStream, (Stream) inStream, 5);
        if (!notWriteToWriter)
          writer.WriteBlob(outStream.ToArray());
        return new BlobInformation((long) bytes.Length, outStream.Length, DateTime.Now, string.Empty, ArcMethods.ZLibPacked, note);
      }
    }
  }

  private Guid GetAuthor(IUserSession session, long authorID)
  {
    return authorID != 0L ? session.GetObjectInfo(authorID).VersionGuid : Guid.Empty;
  }
}
