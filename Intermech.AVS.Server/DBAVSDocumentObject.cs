// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Server.DBAVSDocumentObject
// Assembly: Intermech.AVS.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: DD9587A9-B8FC-4A8A-AB7E-E4D2C61BABE8
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.AVS.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.BlobStream;
using Intermech.Kernel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

#nullable disable
namespace Intermech.AVS.Server;

public class DBAVSDocumentObject : 
  DBObject,
  IDBAVSDocumentObject,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData
{
  public DBAVSDocumentObject(UserSession uSession, DataTable objectsTable)
    : base(uSession, objectsTable)
  {
  }

  public DBAVSDocumentObject(UserSession uSession)
    : base(uSession)
  {
  }

  public AttributeValues[] SetAttributesValues(
    AttributeValues[] valuesList,
    bool deleteNotExistingAttributes,
    bool dontDeleteBlobs,
    bool returnDelta,
    GetAttributeValuesModes modes,
    bool calledFromAVS)
  {
    if (valuesList != null)
    {
      int specificationForm = AvsIDCache.Attr_SpecificationForm;
      if (!calledFromAVS)
      {
        for (int index = 0; index < valuesList.Length; ++index)
        {
          if (valuesList[index].AttributeID == specificationForm)
          {
            IDBAttribute attributeById = this.GetAttributeByID(specificationForm);
            object objB = (object) null;
            if (attributeById != null && attributeById.Values != null && attributeById.Values.Length != 0 && attributeById.Values[0] != null && !(attributeById.Values[0] is DBNull))
              objB = attributeById.Values[0];
            object objA = (object) null;
            if (valuesList[index].Values != null && valuesList[index].Values.Length != 0 && valuesList[index].Values[0] != null && !(valuesList[index].Values[0] is DBNull))
              objA = valuesList[index].Values[0];
            if (!object.Equals(objA, objB))
              throw new KernelException("Атрибут \"Форма спецификации\" можно изменять только в редакторе спецификаций!");
          }
          if (valuesList[index].AttributeID == AvsIDCache.Attr_ScanDocument)
          {
            IDBAttribute byId1 = this.Attributes.FindByID(AvsIDCache.Attr_DocumentFile);
            if (!Convert.ToBoolean(valuesList[index].Values[0]))
            {
              if (byId1 != null)
              {
                MemoryStream memoryStream = (MemoryStream) null;
                BlobInformation info = BlobInformation.EmptyBlobInformation();
                IDBAttribute byId2 = this.Attributes.FindByID(AvsIDCache.Attr_File);
                if (byId2 != null)
                {
                  MemoryStream aDestStream = new MemoryStream();
                  BlobProcReader blobProcReader = new BlobProcReader(this.ObjectID, AttributableElements.Object, AvsIDCache.Attr_File, 0, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null);
                  blobProcReader.ReadData(this.Session);
                  aDestStream.Position = 0L;
                  if (aDestStream != null && aDestStream.Length > 0L)
                  {
                    if (byId2.ValuesCount == 1)
                      byId2.AddValue((object) null);
                    memoryStream = aDestStream;
                    info = blobProcReader.BlobInformation;
                  }
                }
                if (byId1 != null)
                {
                  MemoryStream aDestStream = new MemoryStream();
                  BlobProcReader blobProcReader = new BlobProcReader(this.ObjectID, AttributableElements.Object, AvsIDCache.Attr_DocumentFile, 0, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null);
                  blobProcReader.ReadData(this.Session);
                  aDestStream.Position = 0L;
                  BlobWriterStream destination = new BlobWriterStream(this.ObjectID, AttributableElements.Object, AvsIDCache.Attr_File, 0, 0, new BlobInformation(0L, 0L, DateTime.Now, this.Caption + ".spx", blobProcReader.BlobInformation.ArcMethod, string.Empty), this.Session);
                  try
                  {
                    aDestStream.CopyTo((Stream) destination);
                  }
                  finally
                  {
                    destination.Commit();
                    aDestStream.Close();
                  }
                  byId1.Delete(0L);
                }
                if (memoryStream != null)
                {
                  BlobWriterStream destination = new BlobWriterStream(this.ObjectID, AttributableElements.Object, AvsIDCache.Attr_File, 1, 0, info, this.Session);
                  try
                  {
                    memoryStream.CopyTo((Stream) destination);
                  }
                  finally
                  {
                    destination.Commit();
                    memoryStream.Close();
                  }
                }
              }
            }
            else
            {
              IDBAttribute byId3 = this.Attributes.FindByID(AvsIDCache.Attr_File);
              if (byId3 != null)
              {
                this.Attributes.AddAttribute(AvsIDCache.Attr_DocumentFile, false);
                MemoryStream aDestStream1 = new MemoryStream();
                BlobProcReader blobProcReader1 = new BlobProcReader(this.ObjectID, AttributableElements.Object, AvsIDCache.Attr_File, 0, 0, (Stream) aDestStream1, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null);
                blobProcReader1.ReadData(this.Session);
                aDestStream1.Position = 0L;
                if (blobProcReader1.BlobInformation.FileName.EndsWith(".spx"))
                {
                  BlobInformation info = new BlobInformation(0L, 0L, DateTime.Now, this.Caption + ".spx", blobProcReader1.BlobInformation.ArcMethod, string.Empty);
                  BlobWriterStream destination1 = new BlobWriterStream(this.ObjectID, AttributableElements.Object, AvsIDCache.Attr_DocumentFile, 0, 0, info, this.Session);
                  try
                  {
                    aDestStream1.CopyTo((Stream) destination1);
                  }
                  finally
                  {
                    destination1.Commit();
                    aDestStream1.Close();
                  }
                  if (byId3.ValuesCount > 1)
                  {
                    MemoryStream aDestStream2 = new MemoryStream();
                    BlobProcReader blobProcReader2 = new BlobProcReader(this.ObjectID, AttributableElements.Object, AvsIDCache.Attr_File, 1, 0, (Stream) aDestStream2, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null);
                    blobProcReader2.ReadData(this.Session);
                    aDestStream2.Position = 0L;
                    info = blobProcReader2.BlobInformation;
                    byId3.Index = 1;
                    byId3.DeleteValue();
                    BlobWriterStream destination2 = new BlobWriterStream(this.ObjectID, AttributableElements.Object, AvsIDCache.Attr_File, 0, 0, info, this.Session);
                    try
                    {
                      aDestStream2.CopyTo((Stream) destination2);
                    }
                    finally
                    {
                      destination2.Commit();
                      aDestStream2.Close();
                    }
                  }
                }
              }
              if (byId3 != null && byId3.ValuesCount > 1)
              {
                MemoryStream aDestStream = new MemoryStream();
                BlobProcReader blobProcReader = new BlobProcReader(this.ObjectID, AttributableElements.Object, AvsIDCache.Attr_File, 0, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null);
                blobProcReader.ReadData(this.Session);
                aDestStream.Position = 0L;
                BlobInformation blobInformation = blobProcReader.BlobInformation;
                if (blobInformation.FileName.ToLower().EndsWith(".sp"))
                {
                  byId3.Index = 0;
                  byId3.DeleteValue();
                  byId3.AddValue((object) null);
                  blobInformation.FileType = FileTypes.ftOTD;
                  BlobWriterStream destination = new BlobWriterStream(this.ObjectID, AttributableElements.Object, AvsIDCache.Attr_File, byId3.ValuesCount - 1, 0, blobInformation, this.Session);
                  try
                  {
                    aDestStream.CopyTo((Stream) destination);
                  }
                  finally
                  {
                    destination.Commit();
                    aDestStream.Close();
                  }
                }
              }
            }
          }
        }
      }
    }
    return base.SetAttributesValues(valuesList, deleteNotExistingAttributes, dontDeleteBlobs, returnDelta, modes, (Dictionary<string, Exception>) null);
  }

  public AttributeValues[] SetAttributesValues(AttributeValues[] valuesList, bool calledFromAVS)
  {
    return this.SetAttributesValues(valuesList, false, true, false, GetAttributeValuesModes.None, calledFromAVS);
  }

  public override AttributeValues[] SetAttributesValues(
    AttributeValues[] valuesList,
    bool deleteNotExistingAttributes,
    bool dontDeleteBlobs,
    bool returnDelta,
    GetAttributeValuesModes modes,
    Dictionary<string, Exception> exceptionsList)
  {
    return this.SetAttributesValues(valuesList, deleteNotExistingAttributes, dontDeleteBlobs, returnDelta, modes, false);
  }

  protected override void DoCheckIn()
  {
    if (MetaDataHelper.IsObjectTypeChildOf(this.ObjectType, AvsIDCache.ObjType_Specification))
    {
      long specId = -1;
      List<string> reasonList;
      if (AvsIDCache.SpecificationIsNeedUpdate((IUserSession) this.UserSession, this.ObjectID, this.ObjectType, out specId, out reasonList))
        throw new AVSCheckInException($"Спецификация может не соответствовать изделию!\r\n{string.Join(Environment.NewLine, (IEnumerable<string>) reasonList)}\r\n\r\nЧтобы обновить спецификацию, нужно открыть её в редакторе AVS.");
    }
    base.DoCheckIn();
  }
}
