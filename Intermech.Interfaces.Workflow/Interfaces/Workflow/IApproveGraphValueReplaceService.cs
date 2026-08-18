// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.IApproveGraphValueReplaceService
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Workflow;

public interface IApproveGraphValueReplaceService
{
  /// <summary>
  /// Заменить значения граф для подписей во всех действиях 'Утверждение'
  /// </summary>
  /// <param name="changedGraphValues">Словарь значений Было-Стало для граф подписей</param>
  /// <param name="currentSessionGuid"></param>
  /// <returns>строка с информацией о количестве произведённых замен</returns>
  string ReplaceGraphsInAllApprove(
    Dictionary<string, string> changedGraphValues,
    Guid currentSessionGuid);

  /// <summary>
  /// Заменить значения граф для подписей во всех действиях 'Утверждение' принадлежащих заданному процессу.
  /// Если процесс не в отладке, произведёт замену и в родительском шаблоне.
  /// </summary>
  /// <param name="changedGraphValues">Словарь значений Было-Стало для граф подписей</param>
  /// <param name="processID">процесс для замены</param>
  /// <param name="currentSessionGuid"></param>
  /// <returns>строка с информацией о количестве произведённых замен</returns>
  string ReplaceApproveGraphsByProcess(
    Dictionary<string, string> changedGraphValues,
    long processID,
    Guid currentSessionGuid);

  /// <summary>
  /// Заменить значения граф для подписей во всех действиях 'Утверждение' принадлежащих заданному шаблону
  /// </summary>
  /// <param name="changedGraphValues">Словарь значений Было-Стало для граф подписей</param>
  /// <param name="schemeID">шаблон для замены</param>
  /// <param name="currentSessionGuid"></param>
  /// <returns>строка с информацией о количестве произведённых замен</returns>
  string ReplaceApproveGraphsByScheme(
    Dictionary<string, string> changedGraphValues,
    long schemeID,
    Guid currentSessionGuid);

  /// <summary>
  /// Заменить значения граф для подписей в действиях 'Утверждение' для всех выполняющихся процессов, а так же для всех шаблонов в системе
  /// </summary>
  /// <param name="changedGraphValues">Словарь значений Было-Стало для граф подписей</param>
  /// <param name="currentSessionGuid"></param>
  /// <returns>строка с информацией о количестве произведённых замен</returns>
  string ReplaceGraphsInApproveExecutedProcessAndAllSchemes(
    Dictionary<string, string> changedGraphValues,
    Guid currentSessionGuid);
}
