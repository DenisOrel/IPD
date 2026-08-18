// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.SidecarObjectSection
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.EntityDb;
using Intermech.Data.SectionEntities;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

/// <summary>
/// Секция для хранения сведений об ассоциированном объекте IPS.
/// Ассоциированные объекты - это вспомогательные объекты, связанные с исходными объектами
/// косвенной связью (например, через содержимое файла исходного объекта).
/// </summary>
internal sealed class SidecarObjectSection
{
  private long sourceDocumentId;
  /// <summary>
  /// Ссылка на свойство SourceDocumentId для использования в условиях поиска по базе данных анализатора изменений.
  /// </summary>
  public static readonly SectionPropertyReference SourceDocumentIdRef = new SectionPropertyReference(typeof (SidecarObjectSection), nameof (SourceDocumentId));

  /// <summary>Создает объект.</summary>
  public SidecarObjectSection() => this.sourceDocumentId = 0L;

  /// <summary>
  /// Возвращает или задает идентификатор версии исходного документа.
  /// Значение свойства должно быть задано.
  /// </summary>
  [Indexable(IndexType.Equality, false)]
  public long SourceDocumentId
  {
    get => this.sourceDocumentId;
    set
    {
      this.sourceDocumentId = !Consts.IsUndefinedObjectId(value) ? value : throw new ArgumentException("Не задан идентификатор версии объекта IPS.", nameof (value));
    }
  }
}
