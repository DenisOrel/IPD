// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.SaveAsEventHandlerArgs
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Document.Model;

#nullable disable
namespace Intermech.Document.Client;

/// <summary>Аргументы для обработчиков событий перед и после сохранения файлов</summary>
public class SaveAsEventHandlerArgs
{
  /// <summary>Идентификатор версии объекта БД</summary>
  public long DocumentID;
  /// <summary>Имя файла. Доступно только после сохранения</summary>
  public string FileName;
  /// <summary>Документ</summary>
  public ImDocument Document;

  /// <summary>Конструктор</summary>
  /// <param name="documentID">Идентификатор версии объекта БД</param>
  /// <param name="fileName">Имя файла. Доступно только после сохранения</param>
  /// <param name="document">Документ</param>
  public SaveAsEventHandlerArgs(long documentID, string fileName, ImDocument document)
  {
    this.DocumentID = documentID;
    this.FileName = fileName;
    this.Document = document;
  }
}
