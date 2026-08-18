// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPActionsExecutor
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Вспомогательный статический класс, позволяющий выполнять
/// произвольные действия в рамках указанной пользовательской сессии
/// без явного управления транзакциями
/// </summary>
public static class MRPActionsExecutor
{
  /// <summary>
  /// Выполнить указанное действие в рамках текущей сессии.
  /// Для исполнения будет создан временный контекст, содержащий сессию и контекст действия
  /// </summary>
  /// <param name="session">Сессия</param>
  /// <param name="action">Действие</param>
  public static void Execute(IUserSession session, IMRPAction action)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (action == null)
      throw new ArgumentNullException(nameof (action));
    using (MRPContext mrpContext = new MRPContext(session))
      action.Execute(mrpContext.Services);
  }

  /// <summary>
  /// Выполнить указанное действие в рамках текущей сессии.
  /// Для исполнения будет создан временный контекст, содержащий сессию и контекст действия
  /// </summary>
  /// <param name="session">Сессия</param>
  /// <param name="action">Действие</param>
  /// <param name="services">Контейнер сервисов</param>
  public static void Execute(IUserSession session, IMRPAction action, IServiceProvider services)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (action == null)
      throw new ArgumentNullException(nameof (action));
    using (MRPContext mrpContext = new MRPContext(services, session))
      action.Execute(mrpContext.Services);
  }
}
