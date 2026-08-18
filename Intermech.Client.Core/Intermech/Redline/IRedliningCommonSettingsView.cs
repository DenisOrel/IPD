
// Type: Intermech.Redline.IRedliningCommonSettingsView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Mvp;
using System;


namespace Intermech.Redline;

/// <summary>
/// Интерфейс контрола "Общие настройки" для системы красного карандаша. В соответствии с паттерном MVP все взаимодействие с контролом
/// выполняется только через этот интерфейс.
/// </summary>
internal interface IRedliningCommonSettingsView : IView
{
  /// <summary>
  /// Управляет значением флажка режима, при котором по команде "Смотреть" автоматически запускается приложение для снятия скриншотов.
  /// </summary>
  bool LaunchScreenShooter { get; set; }

  /// <summary>Событие изменения какого-либо элемента управления.</summary>
  event EventHandler EditableStateChanged;
}
