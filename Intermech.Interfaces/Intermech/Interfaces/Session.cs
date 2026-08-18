
// Type: Intermech.Interfaces.Session
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Diagnostics;
using System;
using System.Runtime.CompilerServices;


namespace Intermech.Interfaces
{
    /// <summary>Статический класс для обёртывания обращений к пользовательской сессии в using SessionKeeper через делегат (предполагается
    /// прежде всего лямбда) семантика "Session.Invoke(..обработка..)" проще,
    /// чем "using (SessionKeeper sk = new SessionKeeper()) {..обработка..}" и не требует создания-уничтожения объекта, при том,
    /// что гарантирует разрыв связи с сервером даже лучше, чем using.</summary>
    public static class Session
    {
      /// <summary>Обработка пользовательской сессии переданным методом, не возвращающим никакого результата (void)</summary>
      /// <exception cref="T:System.InvalidOperationException">Thrown when the requested operation is invalid</exception>
      /// <param name="session">Сессия. В том случае если == null будет создан SessionKeeper</param>
      /// <param name="sessionHandler">Метод обработки пользовательской сессии</param>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static void Invoke([CanBeNull] IUserSession session, [NotNull] Session.SessionHandler sessionHandler)
      {
        if (session == null)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
            sessionHandler(sessionKeeper.Session);
        }
        else
          sessionHandler(session);
      }

      /// <summary>Обработка пользовательской сессии переданным методом, не возвращающим никакого результата (void)</summary>
      /// <exception cref="T:System.InvalidOperationException">Thrown when the requested operation is invalid</exception>
      /// <param name="session">Сессия. В том случае если == null будет создан SessionKeeper, session будет инициализирован на время исполнения action,
      /// после завершения исполнения action, session вернётся к значению null</param>
      /// <param name="action">Метод, в котором буде</param>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static void Invoke([CanBeNull] ref IUserSession session, [NotNull] Action action)
      {
        if (session == null)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            session = sessionKeeper.Session;
            try
            {
              action();
            }
            finally
            {
              session = (IUserSession) null;
            }
          }
        }
        else
          action();
      }

      /// <summary>Обработка пользовательской сессии переданным методом, не возвращающим никакого результата (void)</summary>
      /// <exception cref="T:System.InvalidOperationException">Thrown when the requested operation is invalid</exception>
      /// <param name="sessionHandler">Метод обработки пользовательской сессии</param>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static void Invoke([NotNull] Session.SessionHandler sessionHandler)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          sessionHandler(sessionKeeper.Session);
      }

      /// <summary>Обработка пользовательской сессии переданным методом, возвращающим типизированный результат</summary>
      /// <exception cref="T:System.InvalidOperationException">Thrown when the requested operation is invalid</exception>
      /// <typeparam name="T">Тип результата, возвращаемого методом обработки пользовательской сессии</typeparam>
      /// <param name="session">Сессия. В том случае если == null будет создан SessionKeeper</param>
      /// <param name="sessionHandler">Метод обработки пользовательской сессии</param>
      /// <returns>Результат вызова метода обработки пользовательской сессии</returns>
      [CanBeNull]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T Invoke<T>([CanBeNull] IUserSession session, [NotNull] Session.SessionHandler<T> sessionHandler)
      {
        if (session != null)
          return sessionHandler(session);
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          return sessionHandler(sessionKeeper.Session);
      }

      /// <summary>Обработка пользовательской сессии переданным методом, возвращающим типизированный результат</summary>
      /// <exception cref="T:System.InvalidOperationException">Thrown when the requested operation is invalid</exception>
      /// <typeparam name="T">Тип результата, возвращаемого методом обработки пользовательской сессии</typeparam>
      /// <param name="sessionHandler">Метод обработки пользовательской сессии</param>
      /// <returns>Результат вызова метода обработки пользовательской сессии</returns>
      [CanBeNull]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T Invoke<T>([NotNull] Session.SessionHandler<T> sessionHandler)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          return sessionHandler(sessionKeeper.Session);
      }

      /// <summary>Обработка пользовательской сессии переданным методом, возвращающим типизированный результат</summary>
      /// <exception cref="T:System.InvalidOperationException">Thrown when the requested operation is invalid</exception>
      /// <typeparam name="T">Тип результата, возвращаемого методом обработки пользовательской сессии</typeparam>
      /// <param name="sessionHandler">Метод обработки пользовательской сессии</param>
      /// <returns>Результат вызова метода обработки пользовательской сессии</returns>
      [CanBeNull]
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static T InvokeNotNull<T>([NotNull] Session.SessionHandlerNotNull<T> sessionHandler) where T : class
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          return sessionHandler(sessionKeeper.Session);
      }

      /// <summary>Возвращает краткую информацию об объекте по идентификатору его версии</summary>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static QuickObjectInfo GetObjectInfo([NotEmpty] long objectVersionID)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          return sessionKeeper.Session.GetObjectInfo(objectVersionID);
      }

      /// <summary>Возвращает краткую информацию об объекте по глобальному идентификатору его версии</summary>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static QuickObjectInfo GetObjectInfo([NotEmpty] Guid objectVersionGuid)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          return sessionKeeper.Session.GetObjectInfo(objectVersionGuid);
      }

      /// <summary>Возвращает краткую информацию об объекте по идентификатору его версии</summary>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static bool TryGetObjectInfo([NotEmpty] long objectVersionID, out QuickObjectInfo quickObjectInfo)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          quickObjectInfo = sessionKeeper.Session.GetObjectInfo(objectVersionID);
          return !quickObjectInfo.Empty;
        }
      }

      /// <summary>Возвращает краткую информацию об объекте по идентификатору его версии</summary>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public static bool TryGetObjectInfo([NotEmpty] Guid objectVersionGuid, out QuickObjectInfo quickObjectInfo)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          quickObjectInfo = sessionKeeper.Session.GetObjectInfo(objectVersionGuid);
          return !quickObjectInfo.Empty;
        }
      }

      /// <summary>Делегат метода обработки пользовательской сессии не возвращающий результата</summary>
      /// <param name="session">Пользовательская сессия</param>
      public delegate void SessionHandler([NotNull] IUserSession session);

      /// <summary>Делегат метода обработки пользовательской сессии возвращающий типизированный результат</summary>
      /// <typeparam name="T">Тип результата, возвращаемого методом обработки пользовательской сессии</typeparam>
      /// <param name="session">Пользовательская сессия</param>
      /// <returns>Результат вызова метода обработки пользовательской сессии</returns>
      [CanBeNull]
      public delegate T SessionHandler<T>([NotNull] IUserSession session);

      /// <summary>Делегат метода обработки пользовательской сессии возвращающий типизированный результат</summary>
      /// <typeparam name="T">Тип результата, возвращаемого методом обработки пользовательской сессии</typeparam>
      /// <param name="session">Пользовательская сессия</param>
      /// <returns>Результат вызова метода обработки пользовательской сессии</returns>
      [NotNull]
      public delegate T SessionHandlerNotNull<T>([NotNull] IUserSession session);
    }
}
