// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.AfterLoadDocumentEventHandlerArgs
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Document.Model;
using System;

#nullable disable
namespace Intermech.Document.Client;

/// <summary>Аргументы для обработчиков события после загрузки файла документа</summary>
public class AfterLoadDocumentEventHandlerArgs
{
  /// <summary>Идентификатор версии объекта БД</summary>
  public long DocumentID;
  /// <summary>Глобальный идентификатор версии объекта</summary>
  public Guid DocumentGuid;
  /// <summary>Идентификатор типа объекта</summary>
  public int DocumentTypeID;
  /// <summary>Документ</summary>
  public ImDocument Document;

  /// <summary>Конструктор</summary>
  /// <param name="documentID">Идентификатор версии объекта БД</param>
  /// <param name="DocumentGuid">Глобальный идентификатор версии объекта</param>
  /// <param name="DocumentTypeID">Идентификатор типа объекта</param>
  /// <param name="document">Документ</param>
  public AfterLoadDocumentEventHandlerArgs(
    long documentID,
    Guid documentGuid,
    int documentTypeID,
    ImDocument document)
  {
    this.DocumentID = documentID;
    this.DocumentGuid = documentGuid;
    this.DocumentTypeID = documentTypeID;
    this.Document = document;
  }
}
