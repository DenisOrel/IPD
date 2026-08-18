// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.BeforePrintDocumentEventArgs
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Аргументы для обработчиков событий перед и после сохранения файлов</summary>
public class BeforePrintDocumentEventArgs
{
  /// <summary>Документ</summary>
  public DocumentTreeNode Document;

  /// <summary>Конструктор</summary>
  /// <param name="document">Документ</param>
  public BeforePrintDocumentEventArgs(DocumentTreeNode document) => this.Document = document;
}
