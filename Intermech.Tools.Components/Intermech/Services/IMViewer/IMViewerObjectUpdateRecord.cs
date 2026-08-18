// Decompiled with JetBrains decompiler
// Type: Intermech.Services.IMViewer.IMViewerObjectUpdateRecord
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Files;

#nullable disable
namespace Intermech.Services.IMViewer;

/// <summary>
/// Контейнер для информации об исходном документе и связанном с ним объекте IMViewer.
/// Используется в задачах обновления объектов IMViewer.
/// </summary>
internal sealed class IMViewerObjectUpdateRecord
{
  /// <summary>Создает объект.</summary>
  /// <param name="documentState">Текущее состояние исходного документа</param>
  /// <param name="documentTypeId">Идентификатор типа исходного документа</param>
  public IMViewerObjectUpdateRecord(DBObjectState documentState, int documentTypeId)
  {
    this.DocumentState = documentState;
    this.DocumentTypeId = documentTypeId;
    this.ViewerObjectId = 0L;
  }

  /// <summary>Возвращает текущее состояние исходного документа.</summary>
  public DBObjectState DocumentState { get; }

  /// <summary>Возвращает идентификатор типа исходного документа.</summary>
  public int DocumentTypeId { get; }

  /// <summary>
  /// Возвращает или задает идентификатор версии объекта IMViewer.
  /// Значение свойства может быть не задано, если связанного объекта IMViewer у
  /// исходного документа нет.
  /// </summary>
  public long ViewerObjectId { get; set; }
}
