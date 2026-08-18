
// Type: Intermech.Tools.LaunchActions.LaunchActionSettingsValidator
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Tools.Settings;


namespace Intermech.Tools.LaunchActions;

public class LaunchActionSettingsValidator : SettingsValidator
{
  /// <summary>
  /// Генерирует исключение в случае, когда настройки содержат ошибку.
  /// </summary>
  /// <param name="errorMessage">Текст сообщения об ошибке. Не может быть null или пуст.</param>
  /// <exception cref="T:Intermech.FaultException">Объект исключения с указанным текстом</exception>
  protected override void RaiseException(string errorMessage)
  {
    throw new FaultException(errorMessage);
  }
}
