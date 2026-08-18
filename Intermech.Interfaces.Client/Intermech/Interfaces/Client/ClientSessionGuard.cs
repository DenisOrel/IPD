// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ClientSessionGuard
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Защищает обращения к <see cref="T:Intermech.Interfaces.Client.ClientSession" /> от использования вне <see cref="T:Intermech.Interfaces.SessionKeeper" />
/// </summary>
internal sealed class ClientSessionGuard : SessionGuardClientValidator
{
  private readonly ClientSession clientSession;

  /// <summary>Создает объект.</summary>
  /// <param name="clientSession">Клиентская обертка для <see cref="T:Intermech.Interfaces.IUserSession" /></param>
  public ClientSessionGuard(ClientSession clientSession) => this.clientSession = clientSession;

  /// <summary>
  /// Проверяет, выполняется ли обращение к сессии или сессионному объекту из SessionKeeper. Если это не так, то метод сбрасывает исключение.
  /// </summary>
  /// <exception cref="T:Intermech.Interfaces.SessionGuardException">Использование объектов сервера приложений вне SessionKeeper строжайше запрещено</exception>
  public void ValidateCall() => this.ValidateCall((IUserSession) this.clientSession);
}
