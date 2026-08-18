// Decompiled with JetBrains decompiler
// Type: Intermech.Services.WeldingJoints.IWeldingSeamsModelRoot
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities;

#nullable disable
namespace Intermech.Services.WeldingJoints;

/// <summary>
/// Интерфейс основного объекта для доменной модели сварных швов.
/// Он предоставляет все необходимые средства для чтения изменения объектов доменной модели.
/// Реализация не является thread safe.
/// </summary>
internal interface IWeldingSeamsModelRoot : IModelRoot
{
  IEntityDataService<MechanicalArticleEntity> Articles { get; }

  IEntityDataService<MechanicalDocumentEntity> Documents { get; }

  IEntityDataService<WeldingSeamEntity> WeldingSeams { get; }

  IWeldingSeamsSpecialQueries SpecialQueries { get; }
}
