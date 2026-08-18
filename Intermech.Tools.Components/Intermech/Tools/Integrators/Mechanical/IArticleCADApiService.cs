// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.IArticleCADApiService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using Intermech.Data.SectionEntities;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

/// <summary>
/// Сервис фасада для API изделий, предоставляемого интегрируемым приложением.
/// </summary>
public interface IArticleCADApiService
{
  ContainerValues ReadArticleProperties(SectionEntity articleItem);

  bool WriteArticleProperties(SectionEntity articleItem, ContainerValues fileProperties);

  ValueBag DecodeArticleAttributes(SectionEntity articleItem, ContainerValues fileProperties);

  void EncodeArticleAttributes(
    SectionEntity articleItem,
    ICollection<StringKey> attributeKeys,
    ValueBag attributes,
    ContainerValues fileProperties);

  ICollection<StringKey> GetArticleSyncAttributes(SectionEntity articleItem);
}
