// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TechCard.TechNumeration.ITechNumerationSession
// Assembly: Intermech.Interfaces.TechCard, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B0F892EF-B72A-4A7D-8F43-9EB461AAC859
// Assembly location: D:\IPS\Client\Intermech.Interfaces.TechCard.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.TechCard.xml

using System;

#nullable disable
namespace Intermech.Interfaces.TechCard.TechNumeration;

/// <summary>Interface for TechNumetationSession</summary>
public interface ITechNumerationSession
{
  /// <summary>Session's guid</summary>
  Guid SessionGuid { get; }

  /// <summary>
  /// Cписок родительских объектов для которых необходимо запретить нумерацию
  /// </summary>
  ITechNumerationSuppressList ProjObjToSuppress { get; }

  /// <summary>
  /// Cписок дочерних объектов для которых необходимо запретить нумерацию
  /// </summary>
  ITechNumerationSuppressList PartObjToSuppress { get; }

  /// <summary>
  /// Cписок связей для которых необходимо запретить нумерацию
  /// </summary>
  ITechNumerationSuppressList RelationsToSuppress { get; }

  /// <summary>Запуск протоколирования нумерации для сессии</summary>
  void BeginLogging();

  /// <summary>Остановка протоколирования нумерации для сессии</summary>
  void EndLogging();

  /// <summary>Получение протокола нумерации</summary>
  /// <returns></returns>
  ITechNumerationLog GetNumerationLog();

  /// <summary>Вызов нумерации объекта</summary>
  /// <param name="partObjId">Идентификатор дочернего объекта</param>
  /// <param name="projObjId">Идентификатор родительского объекта</param>
  /// <param name="numObj">Начальный объект для нумерации</param>
  /// <param name="session">Guid cессии</param>
  /// <param name="method">Режим (метод) нумерации объекта</param>
  void NumerateObject(
    long partObjId,
    long projObjId,
    TechNumerationObjectModes numObj,
    Guid session,
    TechNumerationMethods method = TechNumerationMethods.Auto);

  /// <summary>Вызов нумерации объекта</summary>
  /// <param name="partObjId">Идентификатор дочернего объекта</param>
  /// <param name="projObjId">Идентификатор родительского объекта</param>
  /// <param name="numRule">Правило нумерации</param>
  /// <param name="numNode">Элемент правила нумерации</param>
  /// <param name="numObj">Начальный объект для нумерации</param>
  /// <param name="session">Guid cессии</param>
  void NumerateObject(
    long partObjId,
    long projObjId,
    ITechNumerationRule numRule,
    ITechNumerationNode numNode,
    TechNumerationObjectModes numObj,
    Guid session);

  /// <summary>Вызов нумерации объекта</summary>
  /// <param name="relationId">Идентификатор связи</param>
  /// <param name="numObj">Начальный объект для нумерации</param>
  /// <param name="session">Guid cессии</param>
  /// <param name="method">Режим (метод) нумерации объекта</param>
  void NumerateObject(
    long relationId,
    TechNumerationObjectModes numObj,
    Guid session,
    TechNumerationMethods method = TechNumerationMethods.Auto);

  /// <summary>Вызов нумерации объекта</summary>
  /// <param name="relationId">Идентификатор связи</param>
  /// <param name="numRule">Правило нумерации</param>
  /// <param name="numNode">Элемент правила нумерации</param>
  /// <param name="numObj">Начальный объект для нумерации</param>
  /// <param name="session">Guid cессии</param>
  void NumerateObject(
    long relationId,
    ITechNumerationRule numRule,
    ITechNumerationNode numNode,
    TechNumerationObjectModes numObj,
    Guid session);
}
