// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.IDocumentNotifyService
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Сервис уведомлений действий с документом</summary>
public interface IDocumentNotifyService
{
  /// <summary>Событие перед печатью документа</summary>
  event BeforePrintDocumentEventHandler BeforePrint;

  /// <summary>Событие после печати документа</summary>
  event AfterPrintDocumentEventHandler AfterPrint;

  /// <summary>Вызвать событие перед печатью документа</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  void FireBeforePrint(object sender, BeforePrintDocumentEventArgs e);

  /// <summary>Вызвать событие после печати документа</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  void FireAfterPrint(object sender, AfterPrintDocumentEventArgs e);
}
