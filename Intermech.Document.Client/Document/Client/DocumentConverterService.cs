// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.DocumentConverterService
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Document;
using System;
using System.IO;

#nullable disable
namespace Intermech.Document.Client;

public class DocumentConverterService : IDocumentConverter
{
  /// <summary>Загрузить файл и сохранить его в формате xml</summary>
  /// <param name="fileAttribute">Атрибут объекта, из которого сохраняем файл</param>
  /// <param name="fileName">Имя файла при сохранении</param>
  /// <param name="updateLinks">Обновлять ссылки в документе при сохранении на диск</param>
  public void ConvertToXml(IDBAttribute fileAttribute, string fileName, bool updateLinks)
  {
    if (fileAttribute == null)
      throw new ArgumentNullException(nameof (fileAttribute));
    if (updateLinks)
    {
      DocumentEditorPlugin.LoadDocumentFromDBObject(fileAttribute.DBObjectID, fileAttribute.Index).SaveToXml(fileName, false, true);
    }
    else
    {
      using (FileStream aDestStream = new FileStream(fileName, FileMode.Create))
        new BlobProcReader(fileAttribute, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData(fileAttribute.Session);
    }
  }

  /// <summary>Загрузить файл и сохранить его в формате Wmf</summary>
  /// <param name="fileAttribute">Атрибут объекта, из которого сохраняем файл</param>
  /// <param name="fileName">Базовое имя файла</param>
  public void ConvertToWmf(IDBAttribute fileAttribute, string fileName)
  {
    DocumentEditorPlugin.LoadDocumentFromDBObject(fileAttribute.DBObjectID, fileAttribute.Index).GeneratePageMetafiles((int[]) null, fileName);
  }

  /// <summary>Загрузить файл и сохранить его в формате Pdf</summary>
  /// <param name="fileAttribute">Атрибут объекта, из которого сохраняем файл</param>
  /// <param name="fileName">Базовое имя файла</param>
  public void ConvertToPdf(IDBAttribute fileAttribute, string fileName)
  {
    this.ConvertToPdf(fileAttribute, fileName, true);
  }

  /// <summary>Загрузить файл и сохранить его в формате Pdf</summary>
  /// <param name="fileAttribute">Атрибут объекта, из которого сохраняем файл</param>
  /// <param name="fileName">Базовое имя файла</param>
  public void ConvertToPdf(IDBAttribute fileAttribute, string fileName, bool autostart)
  {
    DocumentEditorPlugin.LoadDocumentFromDBObject(fileAttribute.DBObjectID).SaveToPdf(fileName, autostart);
  }
}
