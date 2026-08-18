// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.IArticleDocumentationService
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
/// Сервис для работы с документацией на изделие. Используется при сохранении изменений в конструкторских документах для синхронизации связей типа "Документация на изделие".
/// </summary>
public interface IArticleDocumentationService
{
  List<SectionEntity> GetDocuments(SectionEntity articleItem);

  ValueBag GetRelationAttributes(SectionEntity articleItem, SectionEntity documentItem);
}
