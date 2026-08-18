// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.IVariables
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System.Collections;

#nullable disable
namespace Intermech.Interfaces.Workflow;

public interface IVariables : IEnumerable
{
  int Count { get; }

  IVariable this[int index] { get; }

  /// <summary>
  /// Ищет переменную в списке по наименованию или краткому наименованию
  /// </summary>
  /// <param name="name">Имя переменной</param>
  /// <returns>Переменная, или null, если таковая не найдена</returns>
  IVariable Find(string name);

  /// <summary>
  /// Позволяет получить доступ к значению переменной с нужным именем одной строкой.
  /// Пример: activity.Variables["SYS_TASKPERCENT"].Value = "50"
  /// В случае, если переменная с именем name не найдена, генерирует исключение.
  /// </summary>
  IVariable this[string name] { get; }
}
