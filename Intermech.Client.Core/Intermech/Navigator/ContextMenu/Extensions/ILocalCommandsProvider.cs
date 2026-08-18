
// Type: Intermech.Navigator.ContextMenu.Extensions.ILocalCommandsProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.ContextMenu.Extensions;

/// <summary>Провайдер локальных комманд</summary>
public interface ILocalCommandsProvider : ICommandsProvider
{
  /// <summary>Инициализировать заготовки локальных комманд меню, задать им заголовок, иконку, кочетания горячих клавиш и т.п.</summary>
  void InitCommandTemplates(MenuTemplate contextMenuTemplate);

  /// <summary>Подчистить за собой заготовки локальных комманд меню, задать им заголовок, иконку, кочетания горячих клавиш и т.п.
  ///          //! Должен вызываться на выходе из контекста, например в Dispose реализующего интерфейс формы/контрола/etc
  /// </summary>
  void DisposeCommandTemplates(MenuTemplate contextMenuTemplate);
}
