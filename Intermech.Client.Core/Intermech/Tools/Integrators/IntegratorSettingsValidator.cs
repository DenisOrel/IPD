
// Type: Intermech.Tools.Integrators.IntegratorSettingsValidator
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Tools.Settings;
using System;


namespace Intermech.Tools.Integrators;

/// <summary>
/// Реализует базовый класс для валидаторов настроек интеграторов.
/// </summary>
public class IntegratorSettingsValidator : SettingsValidator
{
  private readonly string integratorName;

  /// <summary>Создает объект.</summary>
  /// <param name="integratorName">Название интегратора</param>
  public IntegratorSettingsValidator(string integratorName)
  {
    this.integratorName = integratorName != null ? integratorName : throw new ArgumentNullException(nameof (integratorName));
  }

  /// <summary>
  /// Генерирует исключение в случае, когда настройки содержат ошибку.
  /// </summary>
  /// <param name="errorMessage">Текст сообщения об ошибке. Не может быть null или пуст.</param>
  /// <exception cref="T:Intermech.Tools.Integrators.BadIntegratorSettingsException">Объект исключения с указанным текстом</exception>
  protected override void RaiseException(string errorMessage)
  {
    throw new BadIntegratorSettingsException(this.integratorName, errorMessage);
  }
}
