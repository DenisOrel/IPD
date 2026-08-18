// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.DraftDocumentConvertationSection
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Секция для описания данных, специфических для документов, полученных преобразованием из черновиков документов.
/// </summary>
internal sealed class DraftDocumentConvertationSection
{
  /// <summary>Создает объект.</summary>
  /// <param name="draftDocumentId">Идентификатор версии черновика документа</param>
  public DraftDocumentConvertationSection(long draftDocumentId)
  {
    this.DraftDocumentId = draftDocumentId;
  }

  /// <summary>Возвращает идентификатор версии черновика документа.</summary>
  public long DraftDocumentId { get; private set; }
}
