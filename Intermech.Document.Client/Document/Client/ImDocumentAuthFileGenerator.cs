// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.ImDocumentAuthFileGenerator
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Document.DBCore;
using Intermech.Document.Model;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Document;
using System;
using System.IO;

#nullable disable
namespace Intermech.Document.Client;

/// <summary>Класс генерации аутентичных файлов</summary>
internal class ImDocumentAuthFileGenerator
{
  internal bool NeedGenerate(
    AuthFileNeedGenerateEventArgs args,
    out int mainIndex,
    out int existIndex)
  {
    bool flag = false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      existIndex = -1;
      mainIndex = -1;
      IDBObject dbObject = sessionKeeper.Session.GetObject(args.ObjectId);
      if (dbObject == null)
        return false;
      IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545"));
      if (attributeByGuid == null)
        return false;
      DateTime dateTime1 = DateTime.MinValue;
      DateTime dateTime2 = DateTime.MinValue;
      IDBAttribute attributeById = dbObject.GetAttributeByID(DocIDCache.Attr_ContentModifyDate);
      if (attributeById != null)
        dateTime2 = attributeById.AsDateTime;
      for (int index = 0; index < attributeByGuid.ValuesCount; ++index)
      {
        attributeByGuid.Index = index;
        if (attributeByGuid is IBlobReader blobReader)
        {
          BlobInformation blobInformation = blobReader.OpenBlob(-1);
          if (blobInformation.FileType == FileTypes.ftAuthentical)
            existIndex = index;
          if (blobInformation.FileType == FileTypes.ftNormal)
          {
            if (blobInformation.ModifyDate > dateTime1)
              dateTime1 = blobInformation.ModifyDate;
            string str = Path.GetExtension(blobInformation.FileName).Replace(".", "");
            if (ImDocumentData.ImDocumentFileExtensions.Contains(str) || ImDocumentData.OldBlankExtensions.Contains(str))
            {
              args.InternalDocument = true;
              args.IsHandled = true;
              flag = true;
              mainIndex = index;
            }
          }
          blobReader.CloseBlob();
        }
      }
      if (flag)
      {
        if (existIndex != -1)
        {
          attributeByGuid.Index = existIndex;
          BlobInformation blobInformation = (attributeByGuid as IBlobReader).OpenBlob(-1);
          if (!(blobInformation.ModifyDate < dateTime2))
          {
            if (!(blobInformation.ModifyDate < dateTime1))
              goto label_28;
          }
          args.NeedGenerate = true;
        }
        else
          args.NeedGenerate = true;
      }
    }
label_28:
    return flag;
  }

  internal bool Generate(AuthFileAssignEventArgs args)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(args.ObjectId);
      if (dbObject == null)
        return false;
      IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad0004b-306c-11d8-b4e9-00304f19f545"));
      if (attributeByGuid == null)
        return false;
      int existIndex = -1;
      int mainIndex = -1;
      bool flag = this.NeedGenerate(new AuthFileNeedGenerateEventArgs(args.ObjectType, args.ObjectId, args.PDFOnly), out mainIndex, out existIndex);
      if (!(mainIndex != -1 & flag))
        return false;
      try
      {
        ImDocument imDocument = DocumentEditorPlugin.LoadDocumentFromDBObject(args.ObjectId, mainIndex);
        if (imDocument != null)
        {
          attributeByGuid.Index = mainIndex;
          string str = FileNameHelper.ReplaceInvalidFileNameChars((attributeByGuid as IBlobReader).OpenBlob(-1).FileName + ".pdf");
          MemoryStream memoryStream = new MemoryStream();
          imDocument.SaveToPdf((Stream) memoryStream);
          if (existIndex != -1)
          {
            attributeByGuid.Index = existIndex;
            IBlobReader blobReader = attributeByGuid as IBlobReader;
            BlobInformation aBlobInformation = blobReader.OpenBlob(-1) with
            {
              ModifyDate = DateTime.Now
            };
            memoryStream.Position = 0L;
            new BlobProcWriter(attributeByGuid, 0, aBlobInformation, (Stream) memoryStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
            blobReader.CloseBlob();
          }
          else
          {
            int num = attributeByGuid.AddValue((object) FileTypes.ftAuthentical);
            attributeByGuid.Index = num;
            if (attributeByGuid is IBlobReader blobReader)
            {
              try
              {
                BlobInformation aBlobInformation = blobReader.OpenBlob(-1) with
                {
                  FileType = FileTypes.ftAuthentical,
                  FileName = str,
                  ArcMethod = ArcMethods.ZLibPacked
                };
                memoryStream.Position = 0L;
                new BlobProcWriter(attributeByGuid, 0, aBlobInformation, (Stream) memoryStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
              }
              catch (Exception ex)
              {
                attributeByGuid.DeleteValue();
                ExceptionHelper.ExceptionService.ShowException(ex);
              }
            }
            blobReader?.CloseBlob();
          }
          args.IsHandled = true;
        }
      }
      catch
      {
        throw;
      }
    }
    return true;
  }
}
