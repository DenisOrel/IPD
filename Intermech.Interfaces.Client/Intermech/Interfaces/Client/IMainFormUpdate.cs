// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IMainFormUpdate
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс, который реализует главная форма для того,
/// чтобы можно было вызвать её полную перерисовку
/// </summary>
public interface IMainFormUpdate
{
  /// <summary>Отображать ли текст у кнопки "Перечитать все окна"</summary>
  bool AllWindowsRefreshButtonTextVisible { get; set; }

  /// <summary>
  /// Метод вызывает принудительное обновление всех окон Навигатора
  /// </summary>
  /// <param name="sender">Отправитель события</param>
  void ReloadAllWindows(object sender);

  /// <summary>
  /// Проверить блокировку панелей инструментов, связанную с настройками роли
  /// </summary>
  void CheckToolbarsBlocking();

  /// <summary>Обновить главную форму</summary>
  void UpdateWindow();

  /// <summary>
  /// Изучить текущее окно "Навигатора", добавить в историю контекстов все найденные контексты редактирования
  /// (метод будет удалён после реализации окнозависимых контекстов редактирования в IPS)
  /// </summary>
  void CollectCurrentContextsHistory();

  /// <summary>
  /// Обновить состояние тулбара "Контекст редактировани" в зависимости от текущего контекста
  /// </summary>
  void RefreshEditingContextToolbar();

  /// <summary>
  /// История выбранных ранее контекстов редактирования
  /// (свойство будет удалено после реализации окнозависимых контекстов редактирования в IPS)
  /// </summary>
  List<long> EditingContextHistory { get; set; }

  /// <summary>Возвращает объект главной формы приложения.</summary>
  Form MainForm { get; }

  /// <summary>Возвращает экран главной формы</summary>
  Screen MainFormScreen { get; }

  /// <summary>
  /// Возвращает основную рабочую область, в которой размещается главное окно приложения
  /// </summary>
  Rectangle PrimaryWorkingArea { get; }

  /// <summary>
  /// 
  /// </summary>
  SizeF ScaleFactor { get; }

  /// <summary>Открыты ли модальные окна у приложения</summary>
  bool ApplicationHasOpenedModalForms { get; }
}
