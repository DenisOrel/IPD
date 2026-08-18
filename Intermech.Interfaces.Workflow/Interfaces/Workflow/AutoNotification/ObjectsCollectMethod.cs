// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.AutoNotification.ObjectsCollectMethod
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

#nullable disable
namespace Intermech.Interfaces.Workflow.AutoNotification;

/// <summary>Способ определения набора объектов</summary>
public enum ObjectsCollectMethod
{
  /// <summary>Не выбран</summary>
  None,
  /// <summary>Объект-инициатор</summary>
  Initiator,
  /// <summary>
  /// Версии объектов указанных типов, в которые входит об.-иниц. указанным типом связей
  /// </summary>
  InitiatorApplicability,
  /// <summary>
  /// Версии объектов указанных типов, из которых состоит об.-иниц. указанным типом связей
  /// </summary>
  InitiatorComposition,
  /// <summary>Исполнения изделия</summary>
  InitiatorArticles,
  /// <summary>Список объектов, собранных скриптом ЭС</summary>
  FindByScriptObjects,
  /// <summary>Список объектов, собранных схемой поиска</summary>
  GetBySearchSchemeObjects,
  /// <summary>Дочерний объект (для связи)</summary>
  RelationPart,
  /// <summary>Родительский объект (для связи)</summary>
  RelationProject,
  /// <summary>Родительский и дочерний объект (для связи)</summary>
  RelationPartAndProjects,
}
