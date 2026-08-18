// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.ISidecarObjectsCaptureChangesExtension
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.SectionEntities;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

/// <summary>
/// Интерфейс обработчика для создания/обновления ассоциированных объектов IPS.
/// Ассоциированные объекты - это вспомогательные объекты, связанные с исходными объектами
/// косвенной связью (например, через содержимое файла исходного объекта).
/// </summary>
public interface ISidecarObjectsCaptureChangesExtension
{
  bool EnableSanityChecks { get; set; }

  void Initialize();

  void Cleanup();

  bool IsSourceDocument(SectionEntity documentEntity);

  ICollection<Tuple<SectionEntity, long>> FindExisting(IList<SectionEntity> documentEntities);

  bool CanCreate(SectionEntity documentEntity);

  void Create(SectionEntity documentEntity);

  void Update(SectionEntity documentEntity, long sidecarObjectId);
}
