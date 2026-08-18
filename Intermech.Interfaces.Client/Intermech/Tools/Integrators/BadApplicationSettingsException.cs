// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.BadApplicationSettingsException
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Это исключение сбрасывается в том случае, если при настройке приложения на работу в паре с IPS
/// произошла ошибка.
/// </summary>
public sealed class BadApplicationSettingsException : IntegratorException
{
  private readonly string applicationName;

  /// <summary>Создает объект.</summary>
  /// <param name="integratorName">Название интегратора с приложением</param>
  /// <param name="applicationName">Название приложения, с которым осуществляется интеграция</param>
  /// <param name="message">Подробное описание проблемы</param>
  public BadApplicationSettingsException(
    string integratorName,
    string applicationName,
    string message)
    : base(integratorName, message)
  {
    this.applicationName = !string.IsNullOrEmpty(applicationName) ? applicationName : throw new ArgumentException();
  }

  public string ApplicationName => this.applicationName;
}
