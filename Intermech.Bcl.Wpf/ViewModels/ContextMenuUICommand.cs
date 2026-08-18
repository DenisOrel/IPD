
// Type: Intermech.UI.Wpf.ViewModels.ContextMenuUICommand
// Assembly: Intermech.Bcl.Wpf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 91600B17-2177-4703-BAB9-56FCFFBCBBA2
:\IPS\Client\Intermech.Bcl.Wpf.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.Wpf.xml

using System;
using System.Diagnostics;
using System.Windows.Input;


namespace Intermech.UI.Wpf.ViewModels;

/// <summary>
/// Команда для контекстного меню, которая является элементом логического дерева WPF,
/// а не компонентом другой модели вида.
/// </summary>
public class ContextMenuUICommand : ICommand
{
  private string text;
  private Action<object> executeAction;
  private Predicate<object> canExecuteFunc;

  /// <summary>Создает объект.</summary>
  /// <param name="text">Текст для отображения в интерфейсе пользователя</param>
  /// <param name="execute">Обработчик команды</param>
  /// <param name="canExecute">Необязательный обработчик для проверки доступности команды</param>
  public ContextMenuUICommand(string text, Action<object> execute, Predicate<object> canExecute = null)
  {
    if (text == null)
      throw new ArgumentNullException(nameof (text));
    if (execute == null)
      throw new ArgumentNullException(nameof (execute));
    this.text = text;
    this.executeAction = execute;
    this.canExecuteFunc = canExecute;
  }

  /// <summary>Текст для отображения в интерфейсе пользователя.</summary>
  public string Text
  {
    [DebuggerStepThrough] get => this.text;
  }

  /// <summary>
  /// Возвращает строковое представление текущего объекта,
  /// которое совпадает со значением свойства <see cref="P:Intermech.UI.Wpf.ViewModels.ContextMenuUICommand.Text" />.
  /// </summary>
  /// <returns>Строковое представление текущего объекта</returns>
  public override string ToString() => this.text;

  /// <summary>Проверяет, можно ли выполнить команду.</summary>
  /// <param name="parameter">Параметр выполнения команды</param>
  /// <returns>true - если команда может быть выполнена; false - если команда не может быть выполнена</returns>
  bool ICommand.CanExecute(object parameter)
  {
    return this.canExecuteFunc == null || this.canExecuteFunc(parameter);
  }

  /// <summary>Выполняет команду.</summary>
  /// <param name="parameter">Параметр выполнения команды</param>
  void ICommand.Execute(object parameter) => this.executeAction(parameter);

  /// <summary>Событие изменения доступности команды для выполнения.</summary>
  /// <remarks>
  /// Все реализации UI-команд (т.е. команды без собственного состояния) должны
  /// иметь такую реализацию. Иначе метод CanExecute будет вызываться только один
  /// раз сразу после создания команды.
  /// </remarks>
  event EventHandler ICommand.CanExecuteChanged
  {
    add => CommandManager.RequerySuggested += value;
    remove => CommandManager.RequerySuggested -= value;
  }
}
