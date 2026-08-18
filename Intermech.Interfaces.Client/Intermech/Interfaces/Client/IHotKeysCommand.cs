// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IHotKeysCommand
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Windows.Forms;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс, позволяющий определить, какая команда назначена комбинации "горячих клавиш"
/// </summary>
public interface IHotKeysCommand
{
  /// <summary>
  /// Комбинация "горячих клавиш", которой назначена определённая команда
  /// </summary>
  Keys Shortcut { get; }

  /// <summary>
  /// Название команды (уникальное в пределах всей системы строковое значение)
  /// </summary>
  string Command { get; }

  /// <summary>
  /// Обработчик указанной команды - контекстное меню или менеджер команд
  /// </summary>
  DefaultCommandHandler Handler { get; }
}
