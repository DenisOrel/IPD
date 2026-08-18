// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPContextHelper
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Вспомогательный класс, позволяющий извлекать из контекста MRP требуемую информацию
/// </summary>
public static class MRPContextHelper
{
  /// <summary>
  /// Получить номер текущего производственного заказа из контекста, если в последнем есть необходимые данные
  /// </summary>
  /// <param name="context">Контекст, из которого требуется извлечь номер производственного заказа</param>
  /// <returns>Номер производственного заказа или String.Empty</returns>
  public static string GetOrderNumber(IMRPContext context)
  {
    return context == null || context.Services == null || !(context.Services.GetService(typeof (ManufactureOrderHolder)) is ManufactureOrderHolder service) ? string.Empty : service.OrderNumber;
  }

  /// <summary>
  /// Сессия, которая назначена контексту.
  /// Внимание! При необходимости использования данной сессии
  /// в фоновых потоках требуется клонировать её и использовать
  /// клон; по завершении клон требуется удалить с помощью Logout
  /// </summary>
  /// <param name="context">Контекст, из которого требуется извлечь сессию</param>
  /// <returns>Ссылка на сессию или null</returns>
  public static IUserSession GetContextSession(IMRPContext context)
  {
    if (context == null || context.Services == null)
      return (IUserSession) null;
    if (context.Services.GetService(typeof (IUserSession)) is IUserSession service1)
      return service1;
    MRPSessionGuidHolder service2 = context.Services.GetService(typeof (MRPSessionGuidHolder)) as MRPSessionGuidHolder;
    return !(context.Services.GetService(typeof (IMRPUserSessionHelper)) is IMRPUserSessionHelper service3) || service2 == null ? (IUserSession) null : service3.GetUserSession(service2.SessionGuid);
  }

  /// <summary>
  /// Получить сессию по указанному идентификатору, либо извлечь её из контекста.
  /// Внимание! При необходимости использования данной сессии
  /// в фоновых потоках требуется клонировать её и использовать
  /// клон; по завершении клон требуется удалить с помощью Logout
  /// </summary>
  /// <param name="sessionGuid">Идентификатор сессии</param>
  /// <param name="context">Контекст, из которого требуется извлечь сессию</param>
  /// <returns>Ссылка на сессию или null</returns>
  public static IUserSession GetContextSession(Guid sessionGuid, IMRPContext context)
  {
    if (context == null || context.Services == null)
      return (IUserSession) null;
    if (context.Services.GetService(typeof (IUserSession)) is IUserSession service1)
      return service1;
    if (!(context.Services.GetService(typeof (IMRPUserSessionHelper)) is IMRPUserSessionHelper service2))
      return (IUserSession) null;
    IUserSession contextSession = (IUserSession) null;
    if (!sessionGuid.Equals(Guid.Empty))
      contextSession = service2.GetUserSession(sessionGuid);
    if (contextSession != null)
      return contextSession;
    return !(context.Services.GetService(typeof (MRPSessionGuidHolder)) is MRPSessionGuidHolder service3) ? (IUserSession) null : service2.GetUserSession(service3.SessionGuid);
  }

  /// <summary>
  /// </summary>
  /// <param name="context">Контекст, из которого требуется извлечь идентификатор настроек фильтрации составов</param>
  /// <returns>Идентификатор настроек фильтрации составов или String.Empty</returns>
  public static string GetContextFiltration(IMRPContext context)
  {
    if (context == null || context.Services == null)
      return (string) null;
    return !(context.Services.GetService(typeof (ManufactureOrderHolder)) is ManufactureOrderHolder service) || service.FiltrationSettings == null ? string.Empty : service.FiltrationSettings.OwnerID;
  }
}
