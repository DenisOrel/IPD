
// Type: Intermech.Tools.Integrators.IIntegratorSettingsViewModelService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Tools.Settings;


namespace Intermech.Tools.Integrators;

/// <summary>
/// Необязательный сервис интегратора для создания моделей представления настроек интегратора.
/// Модель представления используется при редактировании настроек интегратора в PropertyGrid вместо самого объекта с настройками интегратора.
/// </summary>
public interface IIntegratorSettingsViewModelService
{
  /// <summary>
  /// Создает модель представления для указанного объекта настроек.
  /// </summary>
  /// <param name="settingsObject">Объект с настройками интегратора</param>
  /// <returns>Модель представления</returns>
  /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="settingsObject" /> не должен быть равен null</exception>
  object CreateViewModel(ISettingsObject settingsObject);

  /// <summary>
  /// Восстанавливает объект с настройками из указанной модели представления.
  /// Этот метод используется после завершения редактирования настроек в PropertyGrid.
  /// </summary>
  /// <param name="viewModelObject">Модель представления</param>
  /// <returns>Объект с настройками интегратора</returns>
  /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="viewModelObject" /> не должен быть равен null</exception>
  ISettingsObject CreateSettingsFromViewModel(object viewModelObject);
}
