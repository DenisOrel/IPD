// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.IRouterService
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Signs.Interfaces;
using Intermech.Workflow;
using System;

#nullable disable
namespace Intermech.Interfaces.Workflow;

public interface IRouterService
{
  IDBObject CreateMessage(
    Guid SessionGuid,
    long ToUserID,
    string Subject,
    string Text,
    long FromUserID);

  IDBObject[] CreateMessage(
    Guid SessionGuid,
    long[] ToUserIDs,
    string Subject,
    string Text,
    long FromUserID);

  IDBObject CreateMessage(
    Guid SessionGuid,
    int TypeID,
    long ToUserID,
    string Subject,
    string Text,
    long FromUserID);

  IProcess CreateProcess(Guid SessionGuid, long SchemeID);

  void ReloadSettings(SettingsGroup Group);

  /// <summary>
  /// Возвращает набор граф для подписи, в которых требуется подписание объектов, идентификаторы которых перечислены в ObjectIDs.
  /// Для этого производится поиск всех активных действий "Утверждение" в почте текущего пользователя, содержащих данные объекты как вложения.
  /// </summary>
  GraphsSet GetGraphsToSign(Guid SessionGuid, long[] ObjectIDs, int[] objectsType);

  /// <summary>
  /// Вычисляет время, отсчитывая unitsCount единиц типа units от времени fromTime. Учитывает календарь, если он указан в настройках Workflow.
  /// </summary>
  DateTime CalcPeriod(Guid SessionGuid, DateTime fromTime, TimeUnits units, int unitsCount);
}
