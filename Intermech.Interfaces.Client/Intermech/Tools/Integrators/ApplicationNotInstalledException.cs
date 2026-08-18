// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.ApplicationNotInstalledException
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Это исключение сбрасывается в том случае, если приложение, с которым осуществляется интеграция, не
/// установлено (отсутствует, другая версия этого приложения, приложение поломано и не работает).
/// </summary>
/// <summary>Создает объект.</summary>
/// <param name="integratorName">Название интегратора</param>
/// <param name="message">Сообщение об ошибке</param>
public sealed class ApplicationNotInstalledException(string integratorName, string message) : 
  IntegratorException(integratorName, message)
{
}
