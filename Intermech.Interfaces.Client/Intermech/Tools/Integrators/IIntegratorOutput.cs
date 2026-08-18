// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.IIntegratorOutput
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Позволяет реализовать сервис интегратора для вывода сообщений в окно "Вывод". Интегратор пользуется этим сервисом для
/// общения с пользователем, когда интегратору требуется пояснить принятые им решения или выполненные действия.
/// </summary>
public interface IIntegratorOutput : IIntegratorService
{
  void WriteLine(string text);
}
