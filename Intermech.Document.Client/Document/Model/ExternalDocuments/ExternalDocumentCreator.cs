// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ExternalDocuments.ExternalDocumentCreator
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Document.DBCore;
using Intermech.Interfaces;
using Intermech.Interfaces.BlobStream;
using Intermech.Interfaces.Document;
using System;
using System.IO;
using System.Linq;

#nullable disable
namespace Intermech.Document.Model.ExternalDocuments;

/// <summary>Базовый класс создания внешних документов</summary>
public class ExternalDocumentCreator
{
  /// <summary>Создает внешний документ на основе объекта</summary>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <param name="updateLinks">Требуется ли обновлять атрибуты в документе</param>
  /// <returns>Возвращает внешний документ</returns>
  public virtual ImExternalDocument CreateDocument(long objectId, bool updateLinks)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dBObject = sessionKeeper.Session.GetObject(objectId);
      using (BlobReaderStream blobReaderStream = new BlobReaderStream(objectId, AttributableElements.Object, DocIDCache.Attr_File, 0, 0, sessionKeeper.Session))
      {
        BlobInformation blobInfo = blobReaderStream.BlobInformation;
        return ImDocumentData.ImDocumentExternalFileExtensions.Any<string>((Func<string, bool>) (ext => blobInfo.FileName.EndsWith("." + ext))) ? new ImMSWordExternalDocumentCreator().CreateDocument((Stream) blobReaderStream, dBObject, updateLinks) : (ImExternalDocument) null;
      }
    }
  }

  public virtual void UpdateDocumentDBObject(
    ImExternalDocument doc,
    long docObjID,
    bool updateDocumentLinks)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject docObject = sessionKeeper.Session.GetObject(docObjID);
      this.UpdateDocumentDBObject(doc, docObject, updateDocumentLinks);
    }
  }

  public virtual void UpdateDocumentDBObject(
    ImExternalDocument doc,
    IDBObject docObject,
    bool updateDocumentLinks)
  {
    if (!(doc.ExternalDocumentType == "MSWord"))
      return;
    new ImMSWordExternalDocumentCreator().UpdateDocumentDBObject(doc, docObject, updateDocumentLinks);
  }

  protected virtual ImExternalDocument CreateDocument(
    Stream stream,
    IDBObject dBObject,
    bool updateLinks)
  {
    return (ImExternalDocument) null;
  }
}
