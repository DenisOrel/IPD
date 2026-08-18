// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.ISidecarObjectsEmbedAttributesExtension
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

/// <summary>
/// Интерфейс обработчика для обновления ассоциированных объектов IPS.
/// Ассоциированные объекты - это вспомогательные объекты, связанные с исходными объектами
/// косвенной связью (например, через содержимое файла исходного объекта).
/// Данный тип расширений используется при записи измененных атрибутов в
/// файлы исходного документа.
/// </summary>
public interface ISidecarObjectsEmbedAttributesExtension
{
  void Initialize(long documentId, int documentTypeId);

  void Cleanup();

  void AfterEmbedAttributes(
    long documentId,
    int documentType,
    string documentFilePath,
    ValueBag documentAttributes);

  void AfterSaveModifiedDocument(IOpenDocument document);

  void AfterFlushChanges();
}
