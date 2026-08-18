// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Settings.SettingsValidator
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Settings;

/// <summary>Реализует базовый класс для валидаторов настроек.</summary>
public abstract class SettingsValidator
{
  private List<ISettingsValidatorCheck> checks;

  /// <summary>
  /// Добавляет в последовательность тестов настроек новый тест.
  /// </summary>
  /// <param name="check">Добавляемый тест</param>
  /// <exception cref="T:System.ArgumentNullException">Объект теста не может быть null</exception>
  public void AddCheck(ISettingsValidatorCheck check)
  {
    if (check == null)
      throw new ArgumentNullException(nameof (check));
    if (this.checks == null)
      this.checks = new List<ISettingsValidatorCheck>();
    this.checks.Add(check);
  }

  /// <summary>
  /// Добавляет в последовательность тестов настроек несколько новых тестов.
  /// </summary>
  /// <param name="checks">Добавляемые тесты</param>
  /// <exception cref="T:System.ArgumentNullException">Коллекция добавляемых тестов не может быть null</exception>
  public void AddChecks(IEnumerable<ISettingsValidatorCheck> checks)
  {
    if (checks == null)
      throw new ArgumentNullException(nameof (checks));
    foreach (ISettingsValidatorCheck check in checks)
    {
      if (check != null)
        this.AddCheck(check);
    }
  }

  /// <summary>Выполняет проверку корректности настроек.</summary>
  /// <param name="settingsObject">Объект с настройками</param>
  /// <param name="context">Контекст проверки настроек</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на объект с настройками не может быть null</exception>
  /// <exception cref="T:System.Exception">Настройки содержат ошибку</exception>
  public void Validate(ISettingsObject settingsObject, SettingsValidatorContext context)
  {
    string errorMessage = settingsObject != null ? this.DoValidate(settingsObject, context) : throw new ArgumentNullException(nameof (settingsObject));
    if (string.IsNullOrEmpty(errorMessage))
      return;
    this.RaiseException(errorMessage);
  }

  /// <summary>
  /// Позволяет реализовать или расширить алгоритм проверки корректности настроек. Базовая реализация проверяет настройки, выполняя последовательность тестов,
  /// зарегистрированных с помощью метода <see cref="M:Intermech.Tools.Settings.SettingsValidator.AddCheck" />.
  /// </summary>
  /// <param name="settingsObject">Объект с настройками. Не может быть null</param>
  /// <param name="context">Контекст проверки настроек</param>
  /// <returns>null или String.Empty, если настройки не содержат ошибок, иначе - сообщение об ошибке в настройках</returns>
  protected virtual string DoValidate(
    ISettingsObject settingsObject,
    SettingsValidatorContext context)
  {
    if (this.checks != null)
    {
      foreach (ISettingsValidatorCheck check in this.checks)
      {
        string str = check.PerformCheck(settingsObject, context);
        if (!string.IsNullOrEmpty(str))
          return str;
      }
    }
    return (string) null;
  }

  /// <summary>
  /// Генерирует исключение в случае, когда настройки содержат ошибку.
  /// </summary>
  /// <param name="errorMessage">Текст сообщения об ошибке. Не может быть null или пуст.</param>
  protected abstract void RaiseException(string errorMessage);
}
