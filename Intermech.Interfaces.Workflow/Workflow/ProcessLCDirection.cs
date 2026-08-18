// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.ProcessLCDirection
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System.ComponentModel;

#nullable disable
namespace Intermech.Workflow;

public enum ProcessLCDirection
{
  /// <summary>
  /// Выполнять перевод по шагам ЖЦ вне зависимости от направления действия
  /// </summary>
  [Description("Вне зависимости от направления выполнения")] All,
  /// <summary>
  /// Выполнять перевод по шагам ЖЦ только если действие идёт вперёд
  /// </summary>
  [Description("Только при отравке вперед")] Next,
  /// <summary>
  /// Выполнять перевод по шагам ЖЦ только если действие идёт назад
  /// </summary>
  [Description("Только при возврате назад")] Back,
}
