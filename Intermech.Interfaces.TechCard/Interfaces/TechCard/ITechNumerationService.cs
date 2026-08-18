// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.ITechNumerationService
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using Intermech.Interfaces.TechCard.TechNumeration;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.TechCard;

/// <summary>Интерфейс сервиса нумерации объектов Техкард</summary>
public interface ITechNumerationService
{
  /// <summary>Проверяет наличие правил нумерации для типов объетов</summary>
  /// <remarks>Оставили в целях совместимости</remarks>
  /// <param name="partObjTypeId">Идентификатор типа дочернего объекта  </param>
  /// <param name="projObjTypeId">Идентификатор типа родительского объекта </param>
  /// <param name="session">Guid cессии</param>
  /// <returns></returns>
  bool NumerationRuleExists(int partObjTypeId, int projObjTypeId, Guid session);

  /// <summary>Проверяет наличие правил нумерации для типов объетов</summary>
  /// <remarks>Оставили в целях совместимости</remarks>
  /// <param name="partObjTypeId">Идентификатор типа дочернего объекта  </param>
  /// <param name="projObjTypeId">Идентификатор типа родительского объекта </param>
  /// <param name="session">Guid cессии</param>
  /// <param name="searchMode">Режим поиска</param>
  /// <returns></returns>
  bool NumerationRuleExists(
    int partObjTypeId,
    int projObjTypeId,
    Guid session,
    NumSearchMode searchMode);

  /// <summary>Проверяет наличие правил нумерации для типов объетов</summary>
  /// <remarks>Оставили в целях совместимости</remarks>
  /// <param name="partObjTypeId">Идентификатор типа дочернего объекта  </param>
  /// <param name="projObjTypeId">Идентификатор типа родительского объекта </param>
  /// <param name="session">Guid cессии</param>
  /// <param name="numRule">Правило нумерации, при его наличии</param>
  /// <param name="numNode">Элемент правила нумерации, при его наличии</param>
  /// <returns></returns>
  bool GetNumerationRule(
    int partObjTypeId,
    int projObjTypeId,
    Guid session,
    out ITechNumerationRule numRule,
    out ITechNumerationNode numNode);

  /// <summary>Проверяет наличие правил нумерации для типов объетов</summary>
  /// <remarks>Оставили в целях совместимости</remarks>
  /// <param name="partObjTypeId">Идентификатор типа дочернего объекта  </param>
  /// <param name="projObjTypeId">Идентификатор типа родительского объекта </param>
  /// <param name="session">Guid cессии</param>
  /// <param name="searchMode">Режим поиска</param>
  /// <param name="numRule">Правило нумерации, при его наличии</param>
  /// <param name="numNode">Элемент правила нумерации, при его наличии</param>
  /// <returns></returns>
  bool GetNumerationRule(
    int partObjTypeId,
    int projObjTypeId,
    Guid session,
    NumSearchMode searchMode,
    out ITechNumerationRule numRule,
    out ITechNumerationNode numNode);

  /// <summary>Поиск элементов нумерации по заданным типам</summary>
  /// <param name="partObjTypeId">Идентификатор типа дочернего объекта</param>
  /// <param name="projObjTypeId">Идентификатор типа родительского объекта</param>
  /// <param name="session">Guid cессии</param>
  /// <param name="searchMode">Режим поиска</param>
  /// <param name="nodes">Результат</param>
  /// <returns></returns>
  bool GetNumerationNode(
    int partObjTypeId,
    int projObjTypeId,
    Guid session,
    NumSearchMode searchMode,
    out List<ITechNumerationNode> nodes);

  /// <summary>Создать сессию для нумерации</summary>
  /// <remarks>Не забывать уничтожать сессию через DisposeSession !</remarks>
  /// <param name="sessionGuid"></param>
  /// <returns></returns>
  ITechNumerationSession CreateSession(Guid sessionGuid);

  /// <summary>Проверка наличия сессии</summary>
  /// <param name="sessionGuid"></param>
  /// <returns></returns>
  bool IsSessionPresent(Guid sessionGuid);

  /// <summary>Уничтожение/освобождение сессии нумерации</summary>
  /// <param name="techNumSession"></param>
  void DisposeSession(ITechNumerationSession techNumSession);

  /// <summary>Уничтожение/освобождение сессии нумерации</summary>
  /// <param name="sessionGuid"></param>
  void DisposeSession(Guid sessionGuid);
}
