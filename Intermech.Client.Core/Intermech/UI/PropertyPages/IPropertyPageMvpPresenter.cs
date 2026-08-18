
// Type: Intermech.UI.PropertyPages.IPropertyPageMvpPresenter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Mvp;
using System;


namespace Intermech.UI.PropertyPages;

/// <summary>
/// Интерфейс MVP-посредника для страниц окна настройки параметров.
/// </summary>
public interface IPropertyPageMvpPresenter : IPresenter
{
  /// <summary>
  /// Сохраняет сделанные пользователем изменения в хранилище настроек.
  /// </summary>
  void AcceptChanges();

  /// <summary>
  /// Отменяет сделанные пользователем изменения и восстанавливает настройки из хранилища.
  /// </summary>
  void RevertChanges();

  /// <summary>Событие изменения параметров пользователем.</summary>
  event EventHandler SettingsChanged;
}
