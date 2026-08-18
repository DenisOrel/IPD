
// Type: Intermech.Navigator.ContextMenu.IStepwiseCommandsProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Navigator.ContextMenu;

/// <summary>
/// Предназначен для облегчения процесса создания провайдеров команд контекстного меню,
/// которым надо проверять выполнение некоторого условия для каждого элемента навигации
/// прежде чем определить, какие команды для них возможны.
/// </summary>
public interface IStepwiseCommandsProvider
{
  /// <summary>
  /// Инициализирует провайдер перед перебором коллекции элементов навигации.
  /// </summary>
  /// <param name="items">Коллекция элементов навигации, представляющая область действия команд</param>
  /// <param name="viewServices">Контейнер с дополнительными сервисами</param>
  void Preprocess(ISelectedItems items, IServiceProvider viewServices);

  /// <summary>
  /// Выполняет проверку условия для элемента навигации, находящегося в коллекции в
  /// указанной позиции.
  /// </summary>
  /// <param name="items">Коллекция элементов навигации, представляющая область действия команд</param>
  /// <param name="index">Порядковый индекс тестируемого элемента навигации в коллекции</param>
  void Process(ISelectedItems items, int index);

  /// <summary>
  /// Заполняет контейнер информацией о командах после проверки всех элементов навигации
  /// из коллекции.
  /// </summary>
  /// <param name="commandsInfo">Контейнер с информацией о командах контекстного меню</param>
  void Postprocess(CommandsInfo commandsInfo);

  /// <summary>
  /// Возвращает признак, надо ли продолжать проверку условий на оставшихся
  /// элементах коллекции.
  /// </summary>
  bool CanContinue { get; }
}
